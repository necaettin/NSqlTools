using NSqlTools.Lib;
using NSqlTools.Lib.Helpers;
using NSqlTools.Types;
using NSqlTools.Types.Properties;
using NSqlTools.Types.RepoContracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static NSqlTools.Types.Enums;

namespace NSqlTools.BusinessLayer
{
	public class RepoBusiness
	{
		#region Public Methods
		public async Task<DepotResponse> GetDepots()
		{
			DepotResponse result;
			List<DepotValue> depotValueCacheList = GetDepotCache();
			if (depotValueCacheList != null)
			{
				return new DepotResponse()
				{
					value = depotValueCacheList,
					count = depotValueCacheList.Count
				};
			}

			HttpResponseMessage response = null;
			try
			{
				using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30) })
				{
					addHeadersToHttpClient(client);

					String depotUrl = ConfigurationManager.AppSettings["DepotURL"];
					response = await client.GetAsync(depotUrl).ConfigureAwait(false);
					response.EnsureSuccessStatusCode();

					String responseBody = await response.Content.ReadAsStringAsync();
					result = System.Text.Json.JsonSerializer.Deserialize<DepotResponse>(responseBody);
				}

				if(result != null)
					AddDepotCache(result.value);
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				throw new Exception(CommonResource.GetDepotsError, ex);
			}
			finally
			{
				response?.Dispose();
			}

			return result;
		}

		public async Task<RepoSearchResponse> GetRepoSearchResult(String searchText, String pathFilter, Int32 takeResults = 100)
		{
			RepoSearchResponse result;
			HttpResponseMessage response = null;
			try
			{
				using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30) })
				{
					addHeadersToHttpClient(client);

					String project = ConfigurationManager.AppSettings["Project"];
					RepoSearchRequest searchRequest = new RepoSearchRequest()
					{
						searchText = searchText,
						skipResults = 0,
						takeResults = takeResults,
						filters = new[] {
						new RepoSearchRequestFilter() { name= "ProjectFilters", values = new [] { $"{project}" } } ,
						new RepoSearchRequestFilter() { name= "RepositoryFilters", values = new [] { $"$/{project}" } }
					},
						searchFilters = new RepoSearchRequestSearchFilters()
						{
							PathFilters = new [] { pathFilter }, // "$/ProductAndDelivery/Destek"
							ProjectFilters = new [] { project },
							RepositoryFilters = new [] { $"$/{project}" }
						},
						summarizedHitCountsNeeded = true,
						includeSuggestions = false,
						isInstantSearch = false
					};
					String searchRequestJson = System.Text.Json.JsonSerializer.Serialize(searchRequest);
					StringContent newcontent = new StringContent(searchRequestJson, Encoding.UTF8, "application/json");

					String searchURL = ConfigurationManager.AppSettings["SearchURL"];
					using (response = await client.PostAsync(searchURL, newcontent))
					{
						response.EnsureSuccessStatusCode();
						String responseBody = await response.Content.ReadAsStringAsync();

						result = System.Text.Json.JsonSerializer.Deserialize<RepoSearchResponse>(responseBody);
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				throw new Exception(CommonResource.GetRepoSearchResultError, ex);
			}
			finally
			{
				response?.Dispose();
			}

			return result;
		}

		public async Task<ContentResponse> GetRepoFileContent(String filePath)
		{
			ContentResponse contentData;
			HttpResponseMessage response = null;
			try
			{
				using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30)})
				{
					addHeadersToHttpClient(client);

					ContentRequest contentRequest = new ContentRequest()
					{
						path = filePath,
						recursionLevel = 0,
						includeContent = true,
						versionDescriptor = new ContentVersionDescriptor()
						{
							versionOption = 0,
							version = "",
							versionType = 5
						}
					};
					String contentURLBase = ConfigurationManager.AppSettings["ContentURL"];
					String requestQuery = await QueryFormater.ObjectQueryFormatter(contentRequest);
					String contentURL = String.Concat(contentURLBase, "?", requestQuery);
					response = await client.GetAsync(contentURL).ConfigureAwait(false);
					response.EnsureSuccessStatusCode();

					String responseBody = await response.Content.ReadAsStringAsync();
					contentData = System.Text.Json.JsonSerializer.Deserialize<ContentResponse>(responseBody);
				}
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				throw new Exception("GetRepoFileContent error", ex);
			}
			finally
			{
				response?.Dispose();
			}

			return contentData;
		}
		#endregion

		#region Private Methods
		private void addHeadersToHttpClient(HttpClient client)
		{
			client.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/json"));

			String personalAccessToken = ConfigurationManager.AppSettings["PersonalAccessToken"];
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
				Convert.ToBase64String(
					ASCIIEncoding.ASCII.GetBytes(
						$"{""}:{personalAccessToken}")));
		}
		#endregion

		#region Depot Cache Methods
		public List<DepotValue> GetDepotCache()
		{
			List<DepotValue> result = null;

			String key = $"{CacheTypeEnum.Depot}";

			List<DepotValue> list = MemoryCacheHelper.Get<List<DepotValue>>(key);
			if (list != null)
			{
				result = new List<DepotValue>();
				foreach (var item in list)
					result.Add(new DepotValue()
					{
						changeDate = item.changeDate,
						encoding = item.encoding,
						isFolder = item.isFolder,
						path = item.path,	
						url = item.url,
						version = item.version						 
					});
			}

			return result;
		}

		public void AddDepotCache(List<DepotValue> DepotValueList)
		{
			String key = $"{CacheTypeEnum.Depot}";

			MemoryCacheHelper.Add(key, DepotValueList, TimeSpan.FromMinutes(Constants.CacheDuration));
		}
		#endregion
	}
}
