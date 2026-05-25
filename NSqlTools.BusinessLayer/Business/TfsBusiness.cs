using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using NSqlTools.Types.Contracts;
using NSqlTools.Types.FormDataContracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace NSqlTools.BusinessLayer.Business
{
	public class TfsBusiness
	{
		public List<TFSChangesetContract> SearchChangesets(
			string tfsUrl, string basePath, string commentFilter, string ownerFilter,
			DateTime? startDate, DateTime? endDate,
			bool showOnlyUnmergedToTest, bool showOnlyUnmergedToMain,
			int? changesetId = null,
			CancellationToken cancellationToken = default)
		{
			var results = new List<TFSChangesetContract>();
			try
			{
				using (var tfsCollection = CreateTfsConnection(tfsUrl))
				{
					var versionControl = tfsCollection.GetService<VersionControlServer>();

					// Check for cancellation
					cancellationToken.ThrowIfCancellationRequested();

					// If a specific changeset ID is provided, fetch it directly
					if (changesetId.HasValue)
					{
						return SearchByChangesetId(versionControl, changesetId.Value, basePath,
							showOnlyUnmergedToTest, showOnlyUnmergedToMain, cancellationToken);
					}

					var branchStructures = DiscoverBranchStructures(versionControl, basePath);

					if (branchStructures.Count == 0)
						throw new Exception(String.Format(NSqlTools.Types.Properties.CommonResource.CouldNtFoundAnyFirmBranchUnder0, basePath));

					foreach (var branchStructure in branchStructures)
					{
						// Check for cancellation before processing each branch
						cancellationToken.ThrowIfCancellationRequested();

						var history = versionControl.QueryHistory(
							branchStructure.DevPath, VersionSpec.Latest, 0, RecursionType.Full, null,
							new DateVersionSpec(startDate ?? new DateTime(2010, 1, 1)),
							endDate.HasValue ? new DateVersionSpec(endDate.Value) : VersionSpec.Latest,
							int.MaxValue, false, false, false, false);

						foreach (Changeset changeset in history)
						{
							// Check for cancellation periodically (every changeset)
							cancellationToken.ThrowIfCancellationRequested();

							if (!string.IsNullOrEmpty(commentFilter) &&
								(string.IsNullOrEmpty(changeset.Comment) || !changeset.Comment.Contains(commentFilter)))
								continue;
							if (!string.IsNullOrEmpty(ownerFilter) && !changeset.Owner.Contains(ownerFilter))
								continue;

							List<String> solutions = GetSolutionsFromChangeset(versionControl, changeset.ChangesetId, branchStructure.DevPath);
							TFSChangesetContract tfsChangesetContract = new TFSChangesetContract
							{
								ChangesetId = changeset.ChangesetId,
								Comment = changeset.Comment,
								Owner = changeset.Owner,
								CreationDate = changeset.CreationDate,
								Branch = branchStructure.CompanyName + "/Dev",
								Solutions = string.Join("\n\r", solutions)
							};

							if (!string.IsNullOrEmpty(branchStructure.TestPath))
							{
								var mergedToTest = IsMergedToBranch(versionControl, changeset.ChangesetId, branchStructure.DevPath, branchStructure.TestPath);
								tfsChangesetContract.MergedToTest = mergedToTest.isMerged;
								tfsChangesetContract.TestChangesetId = mergedToTest.changesetId;
								tfsChangesetContract.TestMergeDate = mergedToTest.createDate;
								tfsChangesetContract.TestMergeUser = mergedToTest.user;
							}
							if (showOnlyUnmergedToTest && tfsChangesetContract.MergedToTest) continue;

							if (!string.IsNullOrEmpty(branchStructure.MainPath))
							{
								var mergedToMain = IsMergedToBranch(versionControl, changeset.ChangesetId, branchStructure.DevPath, branchStructure.MainPath);
								tfsChangesetContract.MergedToMain = mergedToMain.isMerged;
								tfsChangesetContract.MainChangesetId = mergedToMain.changesetId;
								tfsChangesetContract.MainMergeDate = mergedToMain.createDate;
								tfsChangesetContract.MainMergeUser = mergedToMain.user;
							}
							if (showOnlyUnmergedToMain && tfsChangesetContract.MergedToMain) continue;

							results.Add(tfsChangesetContract);
						}
					}
				}
			}
			catch (OperationCanceledException)
			{
				throw; // Re-throw to let caller handle cancellation
			}
			catch (Exception ex)
			{
				throw new Exception(String.Format(NSqlTools.Types.Properties.CommonResource.TFSError0, ex.Message), ex);
			}
			return results.OrderByDescending(x => x.ChangesetId).ToList();
		}

		private List<TFSChangesetContract> SearchByChangesetId(
			VersionControlServer versionControl, 
			int changesetId, 
			string basePath,
			bool showOnlyUnmergedToTest, 
			bool showOnlyUnmergedToMain,
			CancellationToken cancellationToken = default)
		{
			var results = new List<TFSChangesetContract>();

			// Check for cancellation
			cancellationToken.ThrowIfCancellationRequested();

			var changeset = versionControl.GetChangeset(changesetId);
			if (changeset == null)
				return results;

			var branchStructures = DiscoverBranchStructures(versionControl, basePath);

			// Determine which branch this changeset belongs to
			TFSBranchStructure matchedStructure = null;
			if (changeset.Changes != null && changeset.Changes.Length > 0)
			{
				string firstServerItem = changeset.Changes[0].Item.ServerItem;
				matchedStructure = branchStructures.FirstOrDefault(b =>
					(!string.IsNullOrEmpty(b.DevPath) && firstServerItem.StartsWith(b.DevPath, StringComparison.OrdinalIgnoreCase)) ||
					(!string.IsNullOrEmpty(b.TestPath) && firstServerItem.StartsWith(b.TestPath, StringComparison.OrdinalIgnoreCase)) ||
					(!string.IsNullOrEmpty(b.MainPath) && firstServerItem.StartsWith(b.MainPath, StringComparison.OrdinalIgnoreCase)));
			}

			List<String> solutions = matchedStructure != null && !string.IsNullOrEmpty(matchedStructure.DevPath)
				? GetSolutionsFromChangeset(versionControl, changesetId, matchedStructure.DevPath)
				: new List<String>();

			var contract = new TFSChangesetContract
			{
				ChangesetId = changeset.ChangesetId,
				Comment = changeset.Comment,
				Owner = changeset.Owner,
				CreationDate = changeset.CreationDate,
				Branch = matchedStructure?.CompanyName != null ? matchedStructure.CompanyName + "/Dev" : string.Empty,
				Solutions = string.Join("\n\r", solutions)
			};

			if (matchedStructure != null)
			{
				if (!string.IsNullOrEmpty(matchedStructure.TestPath))
				{
					var mergedToTest = IsMergedToBranch(versionControl, changesetId, matchedStructure.DevPath, matchedStructure.TestPath);
					contract.MergedToTest = mergedToTest.isMerged;
					contract.TestChangesetId = mergedToTest.changesetId;
					contract.TestMergeDate = mergedToTest.createDate;
					contract.TestMergeUser = mergedToTest.user;
				}

				if (!string.IsNullOrEmpty(matchedStructure.MainPath))
				{
					var mergedToMain = IsMergedToBranch(versionControl, changesetId, matchedStructure.DevPath, matchedStructure.MainPath);
					contract.MergedToMain = mergedToMain.isMerged;
					contract.MainChangesetId = mergedToMain.changesetId;
					contract.MainMergeDate = mergedToMain.createDate;
					contract.MainMergeUser = mergedToMain.user;
				}
			}

			if (showOnlyUnmergedToTest && contract.MergedToTest) return results;
			if (showOnlyUnmergedToMain && contract.MergedToMain) return results;

			results.Add(contract);
			return results;
		}

		private List<TFSBranchStructure> DiscoverBranchStructures(VersionControlServer versionControl, string basePath)
		{
			var structures = new List<TFSBranchStructure>();
			try
			{
				if (!basePath.StartsWith("$/"))
					basePath = "$/" + basePath;
				basePath = basePath.TrimEnd('/');

				var items = versionControl.GetItems(basePath, VersionSpec.Latest, RecursionType.OneLevel);
				foreach (Item item in items.Items)
				{
					if (item.ServerItem.Equals(basePath, StringComparison.OrdinalIgnoreCase)) continue;
					if (item.ItemType != ItemType.Folder) continue;

					string companyName = System.IO.Path.GetFileName(item.ServerItem);
					var structure = new TFSBranchStructure { CompanyName = companyName };

					try
					{
						var companyItems = versionControl.GetItems(item.ServerItem, VersionSpec.Latest, RecursionType.OneLevel);
						foreach (Item subItem in companyItems.Items)
						{
							if (subItem.ItemType != ItemType.Folder) continue;
							string folderName = System.IO.Path.GetFileName(subItem.ServerItem);

							if (folderName.Equals("Dev", StringComparison.OrdinalIgnoreCase))
								structure.DevPath = subItem.ServerItem;
							else if (folderName.Equals("Test", StringComparison.OrdinalIgnoreCase))
								structure.TestPath = subItem.ServerItem;
							else if (folderName.Equals("Main", StringComparison.OrdinalIgnoreCase))
								structure.MainPath = subItem.ServerItem;
						}
						if (!string.IsNullOrEmpty(structure.DevPath))
							structures.Add(structure);
					}
					catch { continue; }
				}
			}
			catch (Exception ex)
			{
				throw new Exception(String.Format(NSqlTools.Types.Properties.CommonResource.BranchStructureCouldNtBeDiscovered0, ex.Message), ex);
			}
			return structures;
		}

		private static TfsTeamProjectCollection CreateTfsConnection(string tfsUrl)
		{
			var credentials = new Microsoft.VisualStudio.Services.Client.VssClientCredentials();
			credentials.PromptType = Microsoft.VisualStudio.Services.Common.CredentialPromptType.PromptIfNeeded;

			var tfsCollection = new TfsTeamProjectCollection(new Uri(tfsUrl), credentials);
			tfsCollection.EnsureAuthenticated();
			return tfsCollection;
		}

		private (Boolean isMerged, Int32? changesetId, DateTime? createDate, String user) IsMergedToBranch(VersionControlServer versionControl, int changesetId, string sourceBranch, string targetBranch)
		{
			try
			{
				var trackResults = versionControl.TrackMerges(
					new int[] { changesetId },
					new ItemIdentifier(sourceBranch),
					new ItemIdentifier[] { new ItemIdentifier(targetBranch) },
					null);

				if (trackResults != null)
				{
					foreach (var result in trackResults)
					{
						int tgtId = result.TargetChangeset != null ? result.TargetChangeset.ChangesetId : 0;
						if (tgtId > 0)
						{
							try
							{
								var tgtChangeset = versionControl.GetChangeset(tgtId);
								if (tgtChangeset.Changes != null)
								{
									foreach (var change in tgtChangeset.Changes)
									{
										if (change.Item.ServerItem.StartsWith(targetBranch, StringComparison.OrdinalIgnoreCase))
										{
											return (true, tgtChangeset.ChangesetId, tgtChangeset.CreationDate, tgtChangeset.Owner);
										}
									}
								}
							}
							catch
							{
							}
						}
					}
				}

				return (false, null, null, null);
			}
			catch
			{
				return (false, null, null, null);
			}
		}

		private List<String> GetSolutionsFromChangeset(VersionControlServer versionControl, int changesetId, string devPath)
		{
			var solutions = new List<String>();
			try
			{
				var changeset = versionControl.GetChangeset(changesetId);
				if (changeset.Changes == null || changeset.Changes.Length == 0)
					return solutions;

				string devPathWithSlash = devPath.EndsWith("/") ? devPath : devPath + "/";
				var solutionNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

				foreach (var change in changeset.Changes)
				{
					string serverItem = change.Item.ServerItem;
					if (!serverItem.StartsWith(devPathWithSlash, StringComparison.OrdinalIgnoreCase))
						continue;

					string relativePath = serverItem.Substring(devPathWithSlash.Length);
					int slashIndex = relativePath.IndexOf('/');
					if (slashIndex > 0)
						solutionNames.Add(relativePath.Substring(0, slashIndex));
				}

				solutions = solutionNames.ToList().OrderBy(s => s).ToList();
			}
			catch
			{
				return solutions;
			}

			return solutions;
		}

		public List<TFSFileChangeContract> GetFileChanges(string tfsUrl, int changesetId)
		{
			var fileChanges = new List<TFSFileChangeContract>();
			try
			{
				using (var tfsCollection = CreateTfsConnection(tfsUrl))
				{
					var versionControl = tfsCollection.GetService<VersionControlServer>();
					var changeset = versionControl.GetChangeset(changesetId);

					if (changeset.Changes == null || changeset.Changes.Length == 0)
						return fileChanges;

					foreach (var change in changeset.Changes)
					{
						if (change.Item.ItemType != ItemType.File)
							continue;

						fileChanges.Add(new TFSFileChangeContract
						{
							FileName = Path.GetFileName(change.Item.ServerItem),
							ServerPath = change.Item.ServerItem,
							ChangeType = change.ChangeType.ToString(),
							ItemChangesetId = change.Item.ChangesetId,
							IsAdd = change.ChangeType.HasFlag(ChangeType.Add),
							IsDelete = change.ChangeType.HasFlag(ChangeType.Delete)
						});
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception(String.Format(NSqlTools.Types.Properties.CommonResource.TFSError0, ex.Message), ex);
			}
			return fileChanges;
		}

		public void LoadFileContent(string tfsUrl, TFSFileChangeContract fileChange)
		{
			try
			{
				using (var tfsCollection = CreateTfsConnection(tfsUrl))
				{
					var versionControl = tfsCollection.GetService<VersionControlServer>();

					if (!fileChange.IsAdd)
					{
						try
						{
							// Get the actual previous version of THIS file using QueryHistory
							var history = versionControl.QueryHistory(
								fileChange.ServerPath,
								VersionSpec.Latest,
								0,
								RecursionType.None,
								null,
								null,
								new ChangesetVersionSpec(fileChange.ItemChangesetId),
								2, // Get current + previous version
								true,
								false);

							var historyArray = history.Cast<Changeset>().ToArray();
							if (historyArray.Length > 1)
							{
								// historyArray[0] is current, historyArray[1] is the actual previous version
								int previousChangesetId = historyArray[1].ChangesetId;
								var oldItem = versionControl.GetItem(fileChange.ServerPath, new ChangesetVersionSpec(previousChangesetId));
								if (oldItem != null)
								{
									using (var stream = oldItem.DownloadFile())
									using (var reader = new StreamReader(stream))
									{
										fileChange.OldContent = reader.ReadToEnd();
									}
								}
							}
						}
						catch { }
					}

					if (!fileChange.IsDelete)
					{
						try
						{
							var newItem = versionControl.GetItem(fileChange.ServerPath, new ChangesetVersionSpec(fileChange.ItemChangesetId));
							if (newItem != null)
							{
								using (var stream = newItem.DownloadFile())
								using (var reader = new StreamReader(stream))
								{
									fileChange.NewContent = reader.ReadToEnd();
								}
							}
						}
						catch { }
					}
				}
			}
			catch (Exception ex)
			{
						throw new Exception(String.Format(NSqlTools.Types.Properties.CommonResource.TFSError0, ex.Message), ex);
						}
					}

					public string GetFileContentAtChangeset(string tfsUrl, string serverPath, int changesetId, bool isOldVersion = false)
					{
						try
						{
							using (var tfsCollection = CreateTfsConnection(tfsUrl))
							{
								var versionControl = tfsCollection.GetService<VersionControlServer>();

								try
								{
									if (isOldVersion)
									{
										// Get the version BEFORE this changeset
										var history = versionControl.QueryHistory(
											serverPath,
											VersionSpec.Latest,
											0,
											RecursionType.None,
											null,
											null,
											new ChangesetVersionSpec(changesetId),
											2, // Get current + previous version
											true,
											false);

										var historyArray = history.Cast<Changeset>().ToArray();
										if (historyArray.Length > 1)
										{
											// historyArray[1] is the version before this changeset
											int previousChangesetId = historyArray[1].ChangesetId;
											var oldItem = versionControl.GetItem(serverPath, new ChangesetVersionSpec(previousChangesetId));
											if (oldItem != null)
											{
												using (var stream = oldItem.DownloadFile())
												using (var reader = new StreamReader(stream))
												{
													return reader.ReadToEnd();
												}
											}
										}
										else
										{
											// No previous version, return empty
											return string.Empty;
										}
									}
									else
									{
										// Get the version AT this changeset
										var item = versionControl.GetItem(serverPath, new ChangesetVersionSpec(changesetId));
										if (item != null)
										{
											using (var stream = item.DownloadFile())
											using (var reader = new StreamReader(stream))
											{
												return reader.ReadToEnd();
											}
										}
									}
								}
								catch
								{
									return string.Empty;
								}
							}
						}
						catch (Exception ex)
						{
							throw new Exception(String.Format(NSqlTools.Types.Properties.CommonResource.TFSError0, ex.Message), ex);
						}

						return string.Empty;
					}

					public List<TFSUser> GetDistinctOwners(
			string tfsUrl, 
			string basePath, 
			DateTime? startDate, 
			DateTime? endDate,
			CancellationToken cancellationToken = default)
		{
			List<TFSUser> owners = new List<TFSUser>();
			try
			{
				using (var tfsCollection = CreateTfsConnection(tfsUrl))
				{
					var versionControl = tfsCollection.GetService<VersionControlServer>();
					var branchStructures = DiscoverBranchStructures(versionControl, basePath);

					foreach (var branchStructure in branchStructures)
					{
						// Check for cancellation before processing each branch
						cancellationToken.ThrowIfCancellationRequested();

						var history = versionControl.QueryHistory(
							branchStructure.DevPath, VersionSpec.Latest, 0, RecursionType.Full, null,
							new DateVersionSpec(startDate ?? new DateTime(2010, 1, 1)),
							endDate.HasValue ? new DateVersionSpec(endDate.Value) : VersionSpec.Latest,
							int.MaxValue, false, false, false, false);

						foreach (Changeset cs in history)
						{
							// Check for cancellation periodically
							cancellationToken.ThrowIfCancellationRequested();

							if (!owners.Any(o => o.UserName == cs.Owner))
								owners.Add(
									new TFSUser
									{
										DisplayName = cs.OwnerDisplayName ?? cs.Owner,
										UserName = cs.Owner
									});
						}
					}
				}
			}
			catch (OperationCanceledException)
			{
				throw; // Re-throw to let caller handle cancellation
			}
			catch (Exception ex)
			{
				throw new Exception(String.Format(NSqlTools.Types.Properties.CommonResource.ErrorOccuredWhileGettingOwners, ex.Message), ex);
			}

			return owners.OrderBy(o => o.DisplayName).ToList();
		}

		public List<string> GetCompaniesUnderPath(string tfsUrl, string basePath)
		{
			var companies = new List<string>();
			try
			{
				using (var tfsCollection = CreateTfsConnection(tfsUrl))
				{
					var versionControl = tfsCollection.GetService<VersionControlServer>();
					var branchStructures = DiscoverBranchStructures(versionControl, basePath);
					companies = branchStructures.Select(b => b.CompanyName).ToList();
				}
			}
			catch { }
			return companies;
		}
	}
}
