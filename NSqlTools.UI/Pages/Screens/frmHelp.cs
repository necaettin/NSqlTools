using NSqlTools.Types.Properties;
using System.Windows.Forms;

namespace NSqlTools.UI.Pages
{
	public partial class frmHelp : Form
	{
		#region Constructors
		public frmHelp()
		{
			InitializeComponent();

			setTextFromResource();
		}
		#endregion

		#region Methods
		private void setTextFromResource()
		{
			this.label1.Text = CommonResource.sqlViewerHelp;
			this.label2.Text = CommonResource.RunQueryHelp;
			this.label3.Text = CommonResource.DBSearchHelp;
			this.label4.Text = CommonResource.SqlCompareHelp;
			this.label5.Text = CommonResource.SqlBatchCompareHelp;
			this.label6.Text = CommonResource.InsertScriptGeneratorHelp;
			this.label7.Text = CommonResource.ConnectionStringsHelp;
			this.label8.Text = CommonResource.FreeTextCompareHelp;
			this.label9.Text = CommonResource.DataCompareHelp;
			this.label10.Text = NSqlTools.Types.Properties.CommonResource.TFSSearchHelp;

			this.Text = CommonResource.Help;
		}
		#endregion
	}
}
