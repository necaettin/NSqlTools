using NSqlTools.Lib;
using NSqlTools.Types.Properties;
using System;
using System.Windows.Forms;

namespace NSqlTools.UI.Pages
{
	partial class frmAboutBox : Form
	{
		#region Constructors
		public frmAboutBox()
		{
			InitializeComponent();
			setTextFromResource();

			initForm();
		}
		#endregion

		#region Methods
		private void initForm()
		{
			this.Text = String.Format(CommonResource.AboutTitle, UIHelper.AssemblyTitle);
			this.labelProductName.Text = UIHelper.AssemblyProduct;
			this.labelVersion.Text = String.Format(CommonResource.VersionNumber, UIHelper.AssemblyVersion);
		}

		private void setTextFromResource()
		{
			this.okButton.Text = CommonResource.OkButton;
			this.label4.Text = CommonResource.ForFeedbackAndSuggestions;
			this.label1.Text = CommonResource.CreatedByNecaettinKeskin;
			this.Text = CommonResource.About;
		}
		#endregion

		#region Events
		private void llEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			string email = "necaettin.keskin@architecht.com";
			string subject = Uri.EscapeDataString(CommonResource.FeedbackAndSuggestions);
			string body = Uri.EscapeDataString(CommonResource.HelloNNIHaveTheFollowingFeedbackOrSuggestions);

			string mailtoLink = $"mailto:{email}?subject={subject}&body={body}";

			try
			{
				System.Diagnostics.Process.Start(mailtoLink);
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				MessageBox.Show(String.Format(CommonResource.UnableToOpenEmailClient, ex.Message));
			}
		}
		#endregion
	}
}
