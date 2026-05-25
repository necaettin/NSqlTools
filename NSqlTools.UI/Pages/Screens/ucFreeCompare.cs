using NSqlTools.Types.BaseTypes;
using NSqlTools.Types.FormDataContracts;
using NSqlTools.Types.Properties;
using ScintillaNET;
using System;
using System.Collections.Generic;

namespace NSqlTools.UI.Pages
{
	public partial class ucFreeTextCompare : BaseUserControl
	{
		#region Properties
		public override List<Object> TabProviders
		{
			get
			{
				return new List<Object>
				{
					ucFreeNotePadCompare
				};
			}
		}
		#endregion

		#region Constructor
		public ucFreeTextCompare()
		{
			InitializeComponent();
		}

		#endregion

		#region Events
		private void ucFreeNotePadCompare_Load(object sender, EventArgs e)
		{
			ucFreeNotePadCompare.sdcCompare.TextLeft = String.Empty;
			ucFreeNotePadCompare.sdcCompare.TextRight = String.Empty;
			ucFreeNotePadCompare.sdcCompare.DiffTexts();
		}
		#endregion

		#region Override Methods
		public override void InitForm()
		{
		}

		public override BaseScreenDataContract GetFormData()
		{
			return new FreeTextCompareScreenDataContract
			{
				Name = CommonResource.FreeTextCompare,
				Lexer = ucFreeNotePadCompare.SelectedCompareType,
				LeftText = ucFreeNotePadCompare.sdcCompare.LeftScintilla.Text,
				RightText = ucFreeNotePadCompare.sdcCompare.RightScintilla.Text
			};
		}

		public override void SetFormData(BaseScreenDataContract formDataBaseContract)
		{
			var data = formDataBaseContract as FreeTextCompareScreenDataContract;
			if(data == null)
				return;

			ucFreeNotePadCompare.SetCompareType(data.Lexer ?? Lexer.Sql);
			ucFreeNotePadCompare.InitScintilla();
			if (data.Lexer != null)
			{
				ucFreeNotePadCompare.PrepareBothNotePads(data.LeftText, null, null, data.RightText, null, null, data.Lexer.Value);
			}
		}
		#endregion
	}
}
