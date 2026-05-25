using System;
using System.Drawing;
using System.Windows.Forms;
using NSqlTools.Types.BaseTypes;
using NSqlTools.UI.Properties;

namespace NSqlTools.UI.UserControls
{
    public class ScreenCardControl : UserControl
    {
        public Label lblIndex;
        public Label lblName;
        public TextBox txtDescription;
        public Button btnUp;
        public Button btnDown;
        public Button btnDelete;
        public Panel pnlButtons;
        public BaseScreenDataContract ScreenData;
        public event EventHandler UpClicked;
        public event EventHandler DownClicked;
        public event EventHandler DeleteClicked;

        private int cardIndex = 1;
        public int CardIndex
        {
            get => cardIndex;
            set
            {
                cardIndex = value;
                if (lblIndex != null)
                    lblIndex.Text = cardIndex.ToString() + ".";
            }
        }

        public ScreenCardControl(BaseScreenDataContract data)
        {
            this.ScreenData = data;
            this.Height = 60;
            this.Width = 850;
            this.Margin = new Padding(5);
            this.BorderStyle = BorderStyle.FixedSingle;

			lblIndex = new Label
			{
				Text = cardIndex.ToString(),
				Location = new Point(5, 10),
				AutoSize = true,
				Width = 30,
				TextAlign = ContentAlignment.MiddleRight,
				Anchor = AnchorStyles.Top | AnchorStyles.Left,
				Font = new Font("Arial", 9, FontStyle.Bold)
			};

			lblName = new Label
            {
                Text = data.Name,
                Location = new Point(40, 10),
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            txtDescription = new TextBox
            {
                Text = data.Description,
                Location = new Point(40, 30),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BorderStyle = BorderStyle.FixedSingle,
                Width = 600
            };
            txtDescription.TextChanged += (s, e) => { ScreenData.Description = txtDescription.Text; };

            pnlButtons = new Panel
            {
                Width = 80,
                Height = 32,
                Dock = DockStyle.Right,
				Padding = new Padding(0, 0, 10, 0)
            };
            int btnSize = 24;
            int btnSpace = 4;
            int y = (pnlButtons.Height - btnSize) / 2;
            btnUp = new Button { Width = btnSize, Height = btnSize, Location = new Point(0, y), Image = Resources.Up, Text = "", ImageAlign = ContentAlignment.MiddleCenter, FlatStyle = FlatStyle.Flat };
            btnDown = new Button { Width = btnSize, Height = btnSize, Location = new Point(btnSize + btnSpace, y), Image = Resources.Down, Text = "", ImageAlign = ContentAlignment.MiddleCenter, FlatStyle = FlatStyle.Flat };
            btnDelete = new Button { Width = btnSize, Height = btnSize, Location = new Point(2 * (btnSize + btnSpace), y), Image = Resources.Close, Text = "", ImageAlign = ContentAlignment.MiddleCenter, FlatStyle = FlatStyle.Flat };

            btnUp.Click += (s, e) => UpClicked?.Invoke(this, EventArgs.Empty);
            btnDown.Click += (s, e) => DownClicked?.Invoke(this, EventArgs.Empty);
            btnDelete.Click += (s, e) => DeleteClicked?.Invoke(this, EventArgs.Empty);

            pnlButtons.Controls.Add(btnUp);
            pnlButtons.Controls.Add(btnDown);
            pnlButtons.Controls.Add(btnDelete);

            this.Controls.Add(lblIndex);
            this.Controls.Add(lblName);
            this.Controls.Add(txtDescription);
            this.Controls.Add(pnlButtons);
        }

        public void UpdateUpDownButtons(int index, int totalCount)
        {
            // Kart numarasını güncelle
            CardIndex = index + 1;

			// Hiç kart yoksa veya tek kart varsa, ikisi de disable
			if (totalCount <= 1)
			{
				btnUp.Enabled = false;
				btnDown.Enabled = false;
			}
			else
			{
				btnUp.Enabled = index > 0;
				btnDown.Enabled = index < totalCount - 1;
			}
        }
    }
}
