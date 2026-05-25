using ScintillaDiff;
using System.Windows.Forms;

namespace NSqlTools.UI.UserControls
{
	partial class ucNotePadCompare
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucNotePadCompare));
			this.panel3 = new System.Windows.Forms.Panel();
			this.scNotePadCompare = new System.Windows.Forms.SplitContainer();
			this.sdcCompare = new ScintillaDiffControl();
			this.pnlStatusBar = new System.Windows.Forms.Panel();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.splitter2 = new System.Windows.Forms.Splitter();
			this.tsMenu = new System.Windows.Forms.ToolStrip();
			this.tsbSaveLeft = new System.Windows.Forms.ToolStripButton();
			this.tsbSaveRight = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbOpenLeft = new System.Windows.Forms.ToolStripButton();
			this.tsbOpenRight = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbFirst = new System.Windows.Forms.ToolStripButton();
			this.tsbPrevious = new System.Windows.Forms.ToolStripButton();
			this.tsbNext = new System.Windows.Forms.ToolStripButton();
			this.tsbLast = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbBiggerChars = new System.Windows.Forms.ToolStripButton();
			this.tsbSmallerChars = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbCaseSensitive = new System.Windows.Forms.ToolStripButton();
			this.txtFind = new System.Windows.Forms.ToolStripTextBox();
			this.tsbFindLeft = new System.Windows.Forms.ToolStripButton();
			this.tsbFindRight = new System.Windows.Forms.ToolStripButton();
			this.tsbFindResultPanelOpenClose = new System.Windows.Forms.ToolStripButton();
			this.tsbDown = new System.Windows.Forms.ToolStripButton();
			this.tsbUp = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbWrap = new System.Windows.Forms.ToolStripButton();
			this.tsbFullScreen = new System.Windows.Forms.ToolStripButton();
			this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
			this.tsbSingleView = new System.Windows.Forms.ToolStripButton();
			this.tsbSwap = new System.Windows.Forms.ToolStripButton();
			this.cbCompareType = new System.Windows.Forms.ToolStripComboBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.panel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.scNotePadCompare)).BeginInit();
			this.scNotePadCompare.Panel1.SuspendLayout();
			this.scNotePadCompare.SuspendLayout();
			this.pnlStatusBar.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.tsMenu.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.scNotePadCompare);
			this.panel3.Controls.Add(this.pnlStatusBar);
			this.panel3.Controls.Add(this.splitter2);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(1042, 447);
			this.panel3.TabIndex = 18;
			// 
			// scNotePadCompare
			// 
			this.scNotePadCompare.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scNotePadCompare.Location = new System.Drawing.Point(0, 0);
			this.scNotePadCompare.Name = "scNotePadCompare";
			this.scNotePadCompare.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// scNotePadCompare.Panel1
			// 
			this.scNotePadCompare.Panel1.Controls.Add(this.sdcCompare);
			this.scNotePadCompare.Size = new System.Drawing.Size(1042, 422);
			this.scNotePadCompare.SplitterDistance = 298;
			this.scNotePadCompare.TabIndex = 20;
			// 
			// sdcCompare
			// 
			this.sdcCompare.AddedCharacterSymbol = '+';
			this.sdcCompare.AutoScroll = true;
			this.sdcCompare.CharacterComparison = false;
			this.sdcCompare.CharacterComparisonMarkAddRemove = false;
			this.sdcCompare.DiffColorAdded = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(242)))), ((int)(((byte)(196)))));
			this.sdcCompare.DiffColorChangeBackground = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(255)))), ((int)(((byte)(140)))));
			this.sdcCompare.DiffColorCharAdded = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(234)))), ((int)(((byte)(111)))));
			this.sdcCompare.DiffColorCharDeleted = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(125)))), ((int)(((byte)(125)))));
			this.sdcCompare.DiffColorDeleted = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(178)))), ((int)(((byte)(178)))));
			this.sdcCompare.DiffStyle = ScintillaDiffStyles.DiffStyle.DiffSideBySide;
			this.sdcCompare.Dock = System.Windows.Forms.DockStyle.Fill;
			this.sdcCompare.ImageRowAdded = ((System.Drawing.Bitmap)(resources.GetObject("sdcCompare.ImageRowAdded")));
			this.sdcCompare.ImageRowAddedScintillaIndex = 28;
			this.sdcCompare.ImageRowDeleted = ((System.Drawing.Bitmap)(resources.GetObject("sdcCompare.ImageRowDeleted")));
			this.sdcCompare.ImageRowDeletedScintillaIndex = 29;
			this.sdcCompare.ImageRowDiff = ((System.Drawing.Bitmap)(resources.GetObject("sdcCompare.ImageRowDiff")));
			this.sdcCompare.ImageRowDiffScintillaIndex = 31;
			this.sdcCompare.ImageRowOk = ((System.Drawing.Bitmap)(resources.GetObject("sdcCompare.ImageRowOk")));
			this.sdcCompare.ImageRowOkScintillaIndex = 30;
			this.sdcCompare.IsEntireLineHighlighted = false;
			this.sdcCompare.Location = new System.Drawing.Point(0, 0);
			this.sdcCompare.MarkColorIndexModifiedBackground = 31;
			this.sdcCompare.MarkColorIndexRemovedOrAdded = 30;
			this.sdcCompare.Name = "sdcCompare";
			this.sdcCompare.RemovedCharacterSymbol = '-';
			this.sdcCompare.Size = new System.Drawing.Size(1042, 298);
			this.sdcCompare.TabIndex = 15;
			this.sdcCompare.TextLeft = "";
			this.sdcCompare.TextRight = "";
			this.sdcCompare.UseRowOkSign = false;
			this.sdcCompare.BindingContextChanged += new System.EventHandler(this.scintillaDiffControl_BindingContextChanged);
			// 
			// pnlStatusBar
			// 
			this.pnlStatusBar.AutoSize = true;
			this.pnlStatusBar.Controls.Add(this.statusStrip1);
			this.pnlStatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlStatusBar.Location = new System.Drawing.Point(0, 422);
			this.pnlStatusBar.Name = "pnlStatusBar";
			this.pnlStatusBar.Size = new System.Drawing.Size(1042, 22);
			this.pnlStatusBar.TabIndex = 19;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
			this.statusStrip1.Location = new System.Drawing.Point(0, 0);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(1042, 22);
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// lblStatus
			// 
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(10, 17);
			this.lblStatus.Text = " ";
			// 
			// splitter2
			// 
			this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitter2.Location = new System.Drawing.Point(0, 444);
			this.splitter2.Name = "splitter2";
			this.splitter2.Size = new System.Drawing.Size(1042, 3);
			this.splitter2.TabIndex = 18;
			this.splitter2.TabStop = false;
			// 
			// tsMenu
			// 
			this.tsMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSaveLeft,
            this.tsbSaveRight,
            this.toolStripSeparator4,
            this.tsbOpenLeft,
            this.tsbOpenRight,
            this.toolStripSeparator2,
            this.tsbFirst,
            this.tsbPrevious,
            this.tsbNext,
            this.tsbLast,
            this.toolStripSeparator1,
            this.tsbBiggerChars,
            this.tsbSmallerChars,
            this.toolStripSeparator5,
            this.tsbCaseSensitive,
            this.txtFind,
            this.tsbFindLeft,
            this.tsbFindRight,
            this.tsbFindResultPanelOpenClose,
            this.tsbDown,
            this.tsbUp,
            this.toolStripSeparator6,
            this.tsbWrap,
            this.tsbFullScreen,
            this.tsbRefresh,
            this.tsbSingleView,
            this.tsbSwap,
            this.cbCompareType});
			this.tsMenu.Location = new System.Drawing.Point(0, 0);
			this.tsMenu.Name = "tsMenu";
			this.tsMenu.Size = new System.Drawing.Size(1042, 31);
			this.tsMenu.TabIndex = 19;
			this.tsMenu.Text = "Navigation";
			// 
			// tsbSaveLeft
			// 
			this.tsbSaveLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbSaveLeft.Image = global::NSqlTools.UI.Properties.Resources.Save_Left;
			this.tsbSaveLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbSaveLeft.Name = "tsbSaveLeft";
			this.tsbSaveLeft.Size = new System.Drawing.Size(28, 28);
			this.tsbSaveLeft.Text = "Save Left NotePad";
			this.tsbSaveLeft.Click += new System.EventHandler(this.tsbSaveLeft_Click);
			// 
			// tsbSaveRight
			// 
			this.tsbSaveRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbSaveRight.Image = global::NSqlTools.UI.Properties.Resources.Save_Right;
			this.tsbSaveRight.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbSaveRight.Name = "tsbSaveRight";
			this.tsbSaveRight.Size = new System.Drawing.Size(28, 28);
			this.tsbSaveRight.Text = "Save Right NotePad";
			this.tsbSaveRight.Click += new System.EventHandler(this.tsbSaveRight_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
			// 
			// tsbOpenLeft
			// 
			this.tsbOpenLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbOpenLeft.Image = global::NSqlTools.UI.Properties.Resources.OpenLeft;
			this.tsbOpenLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbOpenLeft.Name = "tsbOpenLeft";
			this.tsbOpenLeft.Size = new System.Drawing.Size(28, 28);
			this.tsbOpenLeft.Text = "Open Left";
			this.tsbOpenLeft.Click += new System.EventHandler(this.tsbOpenLeft_Click);
			// 
			// tsbOpenRight
			// 
			this.tsbOpenRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbOpenRight.Image = global::NSqlTools.UI.Properties.Resources.OpenRight;
			this.tsbOpenRight.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbOpenRight.Name = "tsbOpenRight";
			this.tsbOpenRight.Size = new System.Drawing.Size(28, 28);
			this.tsbOpenRight.Text = "tsbOpenRight";
			this.tsbOpenRight.Click += new System.EventHandler(this.tsbOpenRight_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			// 
			// tsbFirst
			// 
			this.tsbFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbFirst.Image = global::NSqlTools.UI.Properties.Resources.First;
			this.tsbFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbFirst.Name = "tsbFirst";
			this.tsbFirst.Size = new System.Drawing.Size(28, 28);
			this.tsbFirst.Text = "First";
			this.tsbFirst.Click += new System.EventHandler(this.tsbFirst_Click);
			// 
			// tsbPrevious
			// 
			this.tsbPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbPrevious.Image = global::NSqlTools.UI.Properties.Resources.Left;
			this.tsbPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbPrevious.Name = "tsbPrevious";
			this.tsbPrevious.Size = new System.Drawing.Size(28, 28);
			this.tsbPrevious.Text = "Previous";
			this.tsbPrevious.Click += new System.EventHandler(this.tsbPrevious_Click);
			// 
			// tsbNext
			// 
			this.tsbNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbNext.Image = global::NSqlTools.UI.Properties.Resources.Right;
			this.tsbNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbNext.Name = "tsbNext";
			this.tsbNext.Size = new System.Drawing.Size(28, 28);
			this.tsbNext.Text = "Next";
			this.tsbNext.Click += new System.EventHandler(this.tsbNext_Click);
			// 
			// tsbLast
			// 
			this.tsbLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbLast.Image = global::NSqlTools.UI.Properties.Resources.Last;
			this.tsbLast.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbLast.Name = "tsbLast";
			this.tsbLast.Size = new System.Drawing.Size(28, 28);
			this.tsbLast.Text = "Last";
			this.tsbLast.Click += new System.EventHandler(this.tsbLast_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
			// 
			// tsbBiggerChars
			// 
			this.tsbBiggerChars.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbBiggerChars.Image = global::NSqlTools.UI.Properties.Resources.BiggerChars;
			this.tsbBiggerChars.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbBiggerChars.Name = "tsbBiggerChars";
			this.tsbBiggerChars.Size = new System.Drawing.Size(28, 28);
			this.tsbBiggerChars.Text = "Bigger Chars";
			this.tsbBiggerChars.Click += new System.EventHandler(this.tsbBiggerChars_Click);
			// 
			// tsbSmallerChars
			// 
			this.tsbSmallerChars.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbSmallerChars.Image = global::NSqlTools.UI.Properties.Resources.SmallerChars;
			this.tsbSmallerChars.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbSmallerChars.Name = "tsbSmallerChars";
			this.tsbSmallerChars.Size = new System.Drawing.Size(28, 28);
			this.tsbSmallerChars.Text = "Smaller Chars";
			this.tsbSmallerChars.Click += new System.EventHandler(this.tsbSmallerChars_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
			// 
			// tsbCaseSensitive
			// 
			this.tsbCaseSensitive.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbCaseSensitive.Image = global::NSqlTools.UI.Properties.Resources.CaseSensitive;
			this.tsbCaseSensitive.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbCaseSensitive.Name = "tsbCaseSensitive";
			this.tsbCaseSensitive.Size = new System.Drawing.Size(28, 28);
			this.tsbCaseSensitive.Text = "Case Sensistive";
			this.tsbCaseSensitive.Click += new System.EventHandler(this.tsbCaseSensitive_Click);
			// 
			// txtFind
			// 
			this.txtFind.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.txtFind.Name = "txtFind";
			this.txtFind.Size = new System.Drawing.Size(100, 31);
			this.txtFind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFind_KeyDown);
			// 
			// tsbFindLeft
			// 
			this.tsbFindLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbFindLeft.Image = global::NSqlTools.UI.Properties.Resources.SearchLeft;
			this.tsbFindLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbFindLeft.Name = "tsbFindLeft";
			this.tsbFindLeft.Size = new System.Drawing.Size(28, 28);
			this.tsbFindLeft.Text = "Find Left";
			this.tsbFindLeft.Click += new System.EventHandler(this.tsbFindLeft_Click);
			// 
			// tsbFindRight
			// 
			this.tsbFindRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbFindRight.Image = global::NSqlTools.UI.Properties.Resources.SearchRight;
			this.tsbFindRight.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbFindRight.Name = "tsbFindRight";
			this.tsbFindRight.Size = new System.Drawing.Size(28, 28);
			this.tsbFindRight.Text = "Find";
			this.tsbFindRight.Click += new System.EventHandler(this.tsbFindRight_Click);
			// 
			// tsbFindResultPanelOpenClose
			// 
			this.tsbFindResultPanelOpenClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbFindResultPanelOpenClose.Image = global::NSqlTools.UI.Properties.Resources.SearchResultOpen;
			this.tsbFindResultPanelOpenClose.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbFindResultPanelOpenClose.Name = "tsbFindResultPanelOpenClose";
			this.tsbFindResultPanelOpenClose.Size = new System.Drawing.Size(28, 28);
			this.tsbFindResultPanelOpenClose.Text = "Open\\\\Close Find Results Panel";
			this.tsbFindResultPanelOpenClose.Click += new System.EventHandler(this.tsbFindResultPanelOpenClose_Click);
			// 
			// tsbDown
			// 
			this.tsbDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbDown.Image = global::NSqlTools.UI.Properties.Resources.Down;
			this.tsbDown.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbDown.Name = "tsbDown";
			this.tsbDown.Size = new System.Drawing.Size(28, 28);
			this.tsbDown.Text = "Find next";
			this.tsbDown.Click += new System.EventHandler(this.tsbDown_Click);
			// 
			// tsbUp
			// 
			this.tsbUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbUp.Image = global::NSqlTools.UI.Properties.Resources.Up;
			this.tsbUp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbUp.Name = "tsbUp";
			this.tsbUp.Size = new System.Drawing.Size(28, 28);
			this.tsbUp.Text = "Find previous";
			this.tsbUp.Click += new System.EventHandler(this.tsbUp_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(6, 31);
			// 
			// tsbWrap
			// 
			this.tsbWrap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbWrap.Image = global::NSqlTools.UI.Properties.Resources.Wrap;
			this.tsbWrap.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbWrap.Name = "tsbWrap";
			this.tsbWrap.Size = new System.Drawing.Size(28, 28);
			this.tsbWrap.Text = "Wrap";
			this.tsbWrap.Click += new System.EventHandler(this.tsbWrap_Click);
			// 
			// tsbFullScreen
			// 
			this.tsbFullScreen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbFullScreen.Image = global::NSqlTools.UI.Properties.Resources.FullScreen;
			this.tsbFullScreen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbFullScreen.Name = "tsbFullScreen";
			this.tsbFullScreen.Size = new System.Drawing.Size(28, 28);
			this.tsbFullScreen.Text = "Full Screen";
			this.tsbFullScreen.Click += new System.EventHandler(this.tsbFullScreen_Click);
			// 
			// tsbRefresh
			// 
			this.tsbRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbRefresh.Image = global::NSqlTools.UI.Properties.Resources.Refresh;
			this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbRefresh.Name = "tsbRefresh";
			this.tsbRefresh.Size = new System.Drawing.Size(28, 28);
			this.tsbRefresh.Text = "Refresh";
			this.tsbRefresh.Click += new System.EventHandler(this.tsbRefresh_Click);
			// 
			// tsbSingleView
			// 
			this.tsbSingleView.CheckOnClick = true;
			this.tsbSingleView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbSingleView.Image = global::NSqlTools.UI.Properties.Resources.Page;
			this.tsbSingleView.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbSingleView.Name = "tsbSingleView";
			this.tsbSingleView.Size = new System.Drawing.Size(28, 28);
			this.tsbSingleView.Text = "Single View";
			this.tsbSingleView.Click += new System.EventHandler(this.tsbSingleView_Click);
			// 
			// tsbSwap
			// 
			this.tsbSwap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbSwap.Image = global::NSqlTools.UI.Properties.Resources.Swap;
			this.tsbSwap.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbSwap.Name = "tsbSwap";
			this.tsbSwap.Size = new System.Drawing.Size(28, 28);
			this.tsbSwap.Text = "Swap";
			this.tsbSwap.Click += new System.EventHandler(this.tsbSwap_Click);
			// 
			// cbCompareType
			// 
			this.cbCompareType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbCompareType.Name = "cbCompareType";
			this.cbCompareType.Size = new System.Drawing.Size(121, 31);
			this.cbCompareType.Visible = false;
			this.cbCompareType.SelectedIndexChanged += new System.EventHandler(this.cbCompareType_SelectedIndexChanged);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.panel3);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 31);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1042, 447);
			this.panel1.TabIndex = 20;
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.Filter = "Text files (*.txt)|*.txt|Sql files (*.sql)|*.sql|All files (*.*)|*.*";
			// 
			// openFileDialog
			// 
			this.openFileDialog.Filter = "All files (*.*)|*.*";
			// 
			// ucNotePadCompare
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.tsMenu);
			this.Name = "ucNotePadCompare";
			this.Size = new System.Drawing.Size(1042, 478);
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.scNotePadCompare.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.scNotePadCompare)).EndInit();
			this.scNotePadCompare.ResumeLayout(false);
			this.pnlStatusBar.ResumeLayout(false);
			this.pnlStatusBar.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.tsMenu.ResumeLayout(false);
			this.tsMenu.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel3;
		public ScintillaDiffControl sdcCompare;
		private System.Windows.Forms.Splitter splitter2;
		private System.Windows.Forms.ToolStrip tsMenu;
		private System.Windows.Forms.ToolStripButton tsbFirst;
		private System.Windows.Forms.ToolStripButton tsbPrevious;
		private System.Windows.Forms.ToolStripButton tsbNext;
		private System.Windows.Forms.ToolStripButton tsbLast;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton tsbWrap;
		private System.Windows.Forms.ToolStripButton tsbRefresh;
		private System.Windows.Forms.ToolStripButton tsbSingleView;
		private System.Windows.Forms.ToolStripButton tsbSwap;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton tsbSaveLeft;
		private System.Windows.Forms.ToolStripButton tsbSaveRight;
		private System.Windows.Forms.ToolStripButton tsbFullScreen;
		public System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.Panel pnlStatusBar;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel lblStatus;
		public System.Windows.Forms.ToolStripComboBox cbCompareType;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripButton tsbOpenRight;
		private System.Windows.Forms.ToolStripButton tsbOpenLeft;
		public System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.ToolStripButton tsbBiggerChars;
		private System.Windows.Forms.ToolStripButton tsbSmallerChars;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private SplitContainer scNotePadCompare;
		private System.Windows.Forms.ToolStripTextBox txtFind;
		private System.Windows.Forms.ToolStripButton tsbFindRight;
		private System.Windows.Forms.ToolStripButton tsbFindResultPanelOpenClose;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripButton tsbUp;
		private System.Windows.Forms.ToolStripButton tsbDown;
		private ToolStripButton tsbFindLeft;
		private ToolStripButton tsbCaseSensitive;
	}
}
