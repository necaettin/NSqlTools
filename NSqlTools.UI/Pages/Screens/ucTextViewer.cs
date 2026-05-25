using NSqlTools.Types;
using NSqlTools.Types.BaseTypes;
using NSqlTools.Types.FormDataContracts;
using NSqlTools.Types.Properties;
using System;
using System.Collections.Generic;

namespace NSqlTools.UI.Pages
{
	public partial class ucTextViewer : BaseUserControl
	{
		#region Constructor
		public ucTextViewer()
		{
			InitializeComponent();
		}

		#endregion

		#region Properties	
		public override List<Object> TabProviders
		{
			get
			{
				return new List<Object>
				{
					ucNotePadControl
				};
			}
		}
		#endregion

		#region Override Methods
		public override void InitForm()
		{
		}

		public override BaseScreenDataContract GetFormData()
		{
			return new TextViewerScreenDataContract
			{
				Name = CommonResource.TextViewer,
				ViewerText = ucNotePadControl.NotePadText,
				Lexer = ucNotePadControl.SelectedCompareType
			};
		}

		public override void SetFormData(BaseScreenDataContract formDataBaseContract)
		{
			var data = formDataBaseContract as TextViewerScreenDataContract;
			if (data == null)
				return;

			ScintillaNET.Lexer lexer = data.Lexer ?? ScintillaNET.Lexer.Sql;
			ucNotePadControl.SetCompareType(lexer);
			ucNotePadControl.InitialiseScintilla(lexer);
			ucNotePadControl.SetDBObject(new DBObjectContract() { Definition = data.ViewerText });
		}
		#endregion
	}
}
