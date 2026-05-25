using System.Drawing;
using System.Windows.Forms;

public class ClosableMetroTabControl : MetroFramework.Controls.MetroTabControl
{
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e); // Metro kendi header'larını çizsin

        for (int i = 0; i < TabPages.Count; i++)
        {
            Rectangle tabRect = GetTabRect(i);

            // X alanı - daha küçük ve biraz içerde
            int size = 8; // öncekinden küçük
            Rectangle closeRect = new Rectangle(
                tabRect.Right - size - 6,
                tabRect.Top + (tabRect.Height - size) / 2,
                size,
                size);

            using (Pen pen = new Pen(Color.Red, 1)) // kalınlığı da 1 yap
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.DrawLine(pen, closeRect.Left,  closeRect.Top,    closeRect.Right, closeRect.Bottom);
                e.Graphics.DrawLine(pen, closeRect.Right, closeRect.Top,    closeRect.Left,  closeRect.Bottom);
            }
        }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);

        if (e.Button != MouseButtons.Left)
            return;

        for (int i = 0; i < TabPages.Count; i++)
        {
            Rectangle tabRect = GetTabRect(i);

            int size = 8;
            Rectangle closeRect = new Rectangle(
                tabRect.Right - size - 6,
                tabRect.Top + (tabRect.Height - size) / 2,
                size,
                size);

            if (closeRect.Contains(e.Location))
            {
                TabPages.RemoveAt(i);
                break;
            }
        }
    }
}