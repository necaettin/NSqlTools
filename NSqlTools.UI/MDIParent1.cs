using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NSqlTools.UI.Pages;

namespace NSqlTools.UI
{
    public partial class MDIParent1 : Form
    {
        private int childFormNumber = 0;
        private bool isUpdatingTabs = false;

        public MDIParent1()
        {
            InitializeComponent();
            tabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl.DrawItem += TabControl_DrawItem;
            tabControl.SizeMode = TabSizeMode.Fixed;
            tabControl.ItemSize = new Size(150, 21);
            this.MdiChildActivate += MDIParent1_MdiChildActivate;
        }

        private void MDIParent1_MdiChildActivate(object sender, EventArgs e)
        {
            if (isUpdatingTabs) return;

            if (this.ActiveMdiChild != null)
            {
                for (int i = 0; i < tabControl.TabPages.Count; i++)
                {
                    if (tabControl.TabPages[i].Tag == this.ActiveMdiChild)
                    {
                        isUpdatingTabs = true;
                        tabControl.SelectedIndex = i;
                        isUpdatingTabs = false;
                        break;
                    }
                }
            }
        }

        private void TabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= tabControl.TabPages.Count) return;

            TabPage tabPage = tabControl.TabPages[e.Index];
            Rectangle tabRect = tabControl.GetTabRect(e.Index);

            bool isSelected = (e.Index == tabControl.SelectedIndex);
            Brush backBrush = isSelected ? SystemBrushes.Window : SystemBrushes.Control;
            e.Graphics.FillRectangle(backBrush, e.Bounds);

            Rectangle textRect = new Rectangle(tabRect.X + 3, tabRect.Y + 3, tabRect.Width - 20, tabRect.Height);
            TextRenderer.DrawText(e.Graphics, tabPage.Text, tabControl.Font, textRect, Color.Black);

            Rectangle closeRect = new Rectangle(tabRect.Right - 18, tabRect.Top + 4, 12, 12);
            e.Graphics.DrawString("×", new Font(tabControl.Font.FontFamily, 10, FontStyle.Bold), Brushes.Black, closeRect);
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isUpdatingTabs) return;
            if (tabControl.SelectedIndex >= 0 && tabControl.SelectedIndex < tabControl.TabPages.Count)
            {
                Form childForm = tabControl.TabPages[tabControl.SelectedIndex].Tag as Form;
                if (childForm != null)
                {
                    isUpdatingTabs = true;
                    childForm.Activate();
                    isUpdatingTabs = false;
                }
            }
        }

        private void tabControl_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < tabControl.TabPages.Count; i++)
            {
                Rectangle tabRect = tabControl.GetTabRect(i);
                Rectangle closeRect = new Rectangle(tabRect.Right - 18, tabRect.Top + 4, 15, 15);

                if (closeRect.Contains(e.Location))
                {
                    Form childForm = tabControl.TabPages[i].Tag as Form;
                    if (childForm != null)
                    {
                        childForm.Close();
                    }
                    break;
                }
            }
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;

            ucRunQuery queryControl = new ucRunQuery();
            queryControl.Dock = DockStyle.Fill;
            childForm.Controls.Add(queryControl);

            TabPage tabPage = new TabPage(childForm.Text);
            tabPage.Tag = childForm;

            childForm.FormClosed += (s, args) =>
            {
                tabControl.TabPages.Remove(tabPage);
            };

            tabControl.TabPages.Add(tabPage);
            tabControl.SelectedTab = tabPage;

            childForm.WindowState = FormWindowState.Maximized;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }
    }
}
