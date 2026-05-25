using NSqlTools.Types;
using NSqlTools.UI.UserControls;

namespace NSqlTools.UI.Pages
{
	partial class ucDBObjectCompare
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDBObjectCompare));
			this.scDBObjectCompare = new System.Windows.Forms.SplitContainer();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel4 = new System.Windows.Forms.Panel();
			this.ucDBObjectSelectTarget = new NSqlTools.UI.UserControls.ucDBObjectSelect();
			this.ucDBObjectSelectSource = new NSqlTools.UI.UserControls.ucDBObjectSelect();
			this.pnlObjectType = new System.Windows.Forms.Panel();
			this.gbObjectType = new System.Windows.Forms.GroupBox();
			this._ucObjectType = new NSqlTools.UI.UserControls.ucObjectType();
			this.ucNotePadCompareControl = new NSqlTools.UI.UserControls.ucNotePadCompare();
			this.ucTableViewCompareControl = new NSqlTools.UI.UserControls.ucTableViewCompare();
			this.miniToolStrip = new System.Windows.Forms.StatusStrip();
			((System.ComponentModel.ISupportInitialize)(this.scDBObjectCompare)).BeginInit();
			this.scDBObjectCompare.Panel1.SuspendLayout();
			this.scDBObjectCompare.Panel2.SuspendLayout();
			this.scDBObjectCompare.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel4.SuspendLayout();
			this.pnlObjectType.SuspendLayout();
			this.gbObjectType.SuspendLayout();
			this.SuspendLayout();
			// 
			// scDBObjectCompare
			// 
			this.scDBObjectCompare.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scDBObjectCompare.Location = new System.Drawing.Point(0, 0);
			this.scDBObjectCompare.Name = "scDBObjectCompare";
			// 
			// scDBObjectCompare.Panel1
			// 
			this.scDBObjectCompare.Panel1.Controls.Add(this.panel2);
			// 
			// scDBObjectCompare.Panel2
			// 
			this.scDBObjectCompare.Panel2.Controls.Add(this.ucNotePadCompareControl);
			this.scDBObjectCompare.Panel2.Controls.Add(this.ucTableViewCompareControl);
			this.scDBObjectCompare.Size = new System.Drawing.Size(973, 798);
			this.scDBObjectCompare.SplitterDistance = 313;
			this.scDBObjectCompare.TabIndex = 17;
			// 
			// panel2
			// 
			this.panel2.AutoScroll = true;
			this.panel2.Controls.Add(this.panel4);
			this.panel2.Controls.Add(this.ucDBObjectSelectSource);
			this.panel2.Controls.Add(this.pnlObjectType);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(313, 798);
			this.panel2.TabIndex = 17;
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.ucDBObjectSelectTarget);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel4.Location = new System.Drawing.Point(0, 365);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(313, 433);
			this.panel4.TabIndex = 21;
			// 
			// ucDBObjectSelectTarget
			// 
			this.ucDBObjectSelectTarget.AllowOnlyOneDBSelection = true;
			this.ucDBObjectSelectTarget.Caption = "Target DB Object";
			this.ucDBObjectSelectTarget.DBObjectContractList = null;
			this.ucDBObjectSelectTarget.DBObjectVisibility = true;
			this.ucDBObjectSelectTarget.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ucDBObjectSelectTarget.IsRequiredConnectionString = true;
			this.ucDBObjectSelectTarget.IsRequiredDB = true;
			this.ucDBObjectSelectTarget.IsRequiredDBObject = true;
			this.ucDBObjectSelectTarget.IsRequiredObjectType = false;
			this.ucDBObjectSelectTarget.IsRequiredSchema = true;
			this.ucDBObjectSelectTarget.Location = new System.Drawing.Point(0, 0);
			this.ucDBObjectSelectTarget.MainForm = null;
			this.ucDBObjectSelectTarget.Name = "ucDBObjectSelectTarget";
			this.ucDBObjectSelectTarget.ObjectTypeVisibility = false;
			this.ucDBObjectSelectTarget.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
			this.ucDBObjectSelectTarget.ParentTabPage = null;
			this.ucDBObjectSelectTarget.SchemaVisibility = true;
			this.ucDBObjectSelectTarget.SelectedConnectionNameValue = null;
			this.ucDBObjectSelectTarget.SelectedDBIndexes = null;
			this.ucDBObjectSelectTarget.SelectedDBObjectObjectId = null;
			this.ucDBObjectSelectTarget.SelectedObjectType = null;
			this.ucDBObjectSelectTarget.SelectedObjectType2 = null;
			this.ucDBObjectSelectTarget.SelectedSchemaId = null;
			this.ucDBObjectSelectTarget.Size = new System.Drawing.Size(313, 433);
			this.ucDBObjectSelectTarget.TabIndex = 3;
			this.ucDBObjectSelectTarget.TabIndexConnectionString = 1;
			this.ucDBObjectSelectTarget.TabIndexDB = 7;
			this.ucDBObjectSelectTarget.TabIndexDBObject = 6;
			this.ucDBObjectSelectTarget.TabIndexDBObjectFilter = 5;
			this.ucDBObjectSelectTarget.TabIndexObjectType = 3;
			this.ucDBObjectSelectTarget.TabIndexSchema = 4;
			this.ucDBObjectSelectTarget.TitleVisibility = null;
			this.ucDBObjectSelectTarget.OnDBObjectChanged += new System.EventHandler<NSqlTools.Types.DBObjectChangedEventArgs>(this.ucDBObjectSelectTarget_OnDBObjectChanged);
			this.ucDBObjectSelectTarget.OnDBObjectClear += new System.EventHandler(this.ucDBObjectSelectTarget_OnDBObjectClear);
			// 
			// ucDBObjectSelectSource
			// 
			this.ucDBObjectSelectSource.AllowOnlyOneDBSelection = true;
			this.ucDBObjectSelectSource.Caption = "Source DB Object";
			this.ucDBObjectSelectSource.DBObjectContractList = null;
			this.ucDBObjectSelectSource.DBObjectVisibility = true;
			this.ucDBObjectSelectSource.Dock = System.Windows.Forms.DockStyle.Top;
			this.ucDBObjectSelectSource.IsRequiredConnectionString = true;
			this.ucDBObjectSelectSource.IsRequiredDB = true;
			this.ucDBObjectSelectSource.IsRequiredDBObject = false;
			this.ucDBObjectSelectSource.IsRequiredObjectType = true;
			this.ucDBObjectSelectSource.IsRequiredSchema = true;
			this.ucDBObjectSelectSource.Location = new System.Drawing.Point(0, 47);
			this.ucDBObjectSelectSource.MainForm = null;
			this.ucDBObjectSelectSource.Name = "ucDBObjectSelectSource";
			this.ucDBObjectSelectSource.ObjectTypeVisibility = false;
			this.ucDBObjectSelectSource.ParentTabPage = null;
			this.ucDBObjectSelectSource.SchemaVisibility = true;
			this.ucDBObjectSelectSource.SelectedConnectionNameValue = null;
			this.ucDBObjectSelectSource.SelectedDBIndexes = null;
			this.ucDBObjectSelectSource.SelectedDBObjectObjectId = null;
			this.ucDBObjectSelectSource.SelectedObjectType = null;
			this.ucDBObjectSelectSource.SelectedObjectType2 = null;
			this.ucDBObjectSelectSource.SelectedSchemaId = null;
			this.ucDBObjectSelectSource.Size = new System.Drawing.Size(313, 318);
			this.ucDBObjectSelectSource.TabIndex = 2;
			this.ucDBObjectSelectSource.TabIndexConnectionString = 1;
			this.ucDBObjectSelectSource.TabIndexDB = 7;
			this.ucDBObjectSelectSource.TabIndexDBObject = 6;
			this.ucDBObjectSelectSource.TabIndexDBObjectFilter = 5;
			this.ucDBObjectSelectSource.TabIndexObjectType = 3;
			this.ucDBObjectSelectSource.TabIndexSchema = 4;
			this.ucDBObjectSelectSource.TitleVisibility = null;
			this.ucDBObjectSelectSource.OnDBObjectChanged += new System.EventHandler<NSqlTools.Types.DBObjectChangedEventArgs>(this.ucDBObjectSelectSource_OnDBObjectChanged);
			this.ucDBObjectSelectSource.OnDBObjectClear += new System.EventHandler(this.ucDBObjectSelectSource_OnDBObjectClear);
			this.ucDBObjectSelectSource.OnObjectTypeChanged += new System.EventHandler(this.ucDBObjectSelectSource_OnObjectTypeChanged);
			// 
			// pnlObjectType
			// 
			this.pnlObjectType.Controls.Add(this.gbObjectType);
			this.pnlObjectType.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlObjectType.Location = new System.Drawing.Point(0, 0);
			this.pnlObjectType.Name = "pnlObjectType";
			this.pnlObjectType.Size = new System.Drawing.Size(313, 47);
			this.pnlObjectType.TabIndex = 18;
			// 
			// gbObjectType
			// 
			this.gbObjectType.Controls.Add(this._ucObjectType);
			this.gbObjectType.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbObjectType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gbObjectType.Location = new System.Drawing.Point(0, 0);
			this.gbObjectType.Name = "gbObjectType";
			this.gbObjectType.Size = new System.Drawing.Size(313, 47);
			this.gbObjectType.TabIndex = 10;
			this.gbObjectType.TabStop = false;
			this.gbObjectType.Text = "Object Type";
			// 
			// _ucObjectType
			// 
			this._ucObjectType.IsNullable = false;
			this._ucObjectType.Location = new System.Drawing.Point(7, 17);
			this._ucObjectType.MainForm = null;
			this._ucObjectType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this._ucObjectType.Name = "_ucObjectType";
			this._ucObjectType.ParentTabPage = null;
			this._ucObjectType.Size = new System.Drawing.Size(302, 27);
			this._ucObjectType.TabIndex = 2;
			this._ucObjectType.OnObjectTypeChanged += new System.EventHandler(this._ucObjectType_OnObjectTypeChanged);
			// 
			// ucNotePadCompareControl
			// 
			this.ucNotePadCompareControl.CaseSensitive = false;
			this.ucNotePadCompareControl.CompareTypeVisible = false;
			this.ucNotePadCompareControl.DisplayFullScreen = true;
			this.ucNotePadCompareControl.FindResultCollapsed = true;
			this.ucNotePadCompareControl.FontSize = 12;
			this.ucNotePadCompareControl.Location = new System.Drawing.Point(53, 19);
			this.ucNotePadCompareControl.MainForm = null;
			this.ucNotePadCompareControl.Name = "ucNotePadCompareControl";
			this.ucNotePadCompareControl.ParentTabPage = null;
			this.ucNotePadCompareControl.Size = new System.Drawing.Size(399, 217);
			this.ucNotePadCompareControl.SourceDBObjectName = null;
			this.ucNotePadCompareControl.SourceSchemaName = null;
			this.ucNotePadCompareControl.StatusBarPanelIsVisible = true;
			this.ucNotePadCompareControl.TabIndex = 21;
			this.ucNotePadCompareControl.TargetDBObjectName = null;
			this.ucNotePadCompareControl.TargetSchemaName = null;
			this.ucNotePadCompareControl.Visible = false;
			// 
			// ucTableViewCompareControl
			// 
			this.ucTableViewCompareControl.DataSource = null;
			this.ucTableViewCompareControl.Location = new System.Drawing.Point(32, 242);
			this.ucTableViewCompareControl.MainForm = null;
			this.ucTableViewCompareControl.Name = "ucTableViewCompareControl";
			this.ucTableViewCompareControl.ParentTabPage = null;
			this.ucTableViewCompareControl.Size = new System.Drawing.Size(850, 474);
			this.ucTableViewCompareControl.TabIndex = 22;
			this.ucTableViewCompareControl.Visible = false;
			// 
			// miniToolStrip
			// 
			this.miniToolStrip.AccessibleName = "New item selection";
			this.miniToolStrip.AccessibleRole = System.Windows.Forms.AccessibleRole.ButtonDropDown;
			this.miniToolStrip.AutoSize = false;
			this.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.miniToolStrip.Location = new System.Drawing.Point(11, 1);
			this.miniToolStrip.Name = "miniToolStrip";
			this.miniToolStrip.Size = new System.Drawing.Size(444, 22);
			this.miniToolStrip.TabIndex = 0;
			// 
			// ucDBObjectCompare
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.scDBObjectCompare);
			this.Name = "ucDBObjectCompare";
			this.Size = new System.Drawing.Size(973, 798);
			this.scDBObjectCompare.Panel1.ResumeLayout(false);
			this.scDBObjectCompare.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.scDBObjectCompare)).EndInit();
			this.scDBObjectCompare.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.pnlObjectType.ResumeLayout(false);
			this.gbObjectType.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer scDBObjectCompare;
		private System.Windows.Forms.Panel panel2;
		private ucDBObjectSelect ucDBObjectSelectTarget;
		private ucDBObjectSelect ucDBObjectSelectSource;
		private System.Windows.Forms.Panel pnlObjectType;
		private System.Windows.Forms.GroupBox gbObjectType;
		private ucNotePadCompare ucNotePadCompareControl;
		private ucTableViewCompare ucTableViewCompareControl;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.StatusStrip miniToolStrip;
		private ucObjectType _ucObjectType;
	}
}
