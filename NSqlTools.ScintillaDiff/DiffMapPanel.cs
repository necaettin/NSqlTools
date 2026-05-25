#region License
/*
MIT License

Copyright(c) 2020 Petteri Kautonen

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion

using DiffPlex.DiffBuilder.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ScintillaDiff
{
    /// <summary>
    /// A panel that draws a minimap of diff changes between two texts.
    /// </summary>
    public class DiffMapPanel : Panel
    {
        private List<ChangeType> lineChangeTypes = new List<ChangeType>();
        private Color colorDeleted = Color.FromArgb(0xFF, 0XFF, 0XB2, 0XB2);
        private Color colorAdded = Color.FromArgb(0xFF, 0XD4, 0XF2, 0XC4);
        private Color colorModified = Color.FromArgb(0xFF, 0XFC, 0XFF, 0X8C);
        private Color colorImaginary = Color.FromArgb(0xFF, 0XC0, 0XC0, 0XC0);

        /// <summary>
        /// Initializes a new instance of the <see cref="DiffMapPanel"/> class.
        /// </summary>
        public DiffMapPanel()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            BackColor = Color.White;
        }

        /// <summary>
        /// Gets or sets the color for deleted lines.
        /// </summary>
        public Color ColorDeleted
        {
            get => colorDeleted;
            set { colorDeleted = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the color for added/inserted lines.
        /// </summary>
        public Color ColorAdded
        {
            get => colorAdded;
            set { colorAdded = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the color for modified lines.
        /// </summary>
        public Color ColorModified
        {
            get => colorModified;
            set { colorModified = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the color for imaginary (filler) lines.
        /// </summary>
        public Color ColorImaginary
        {
            get => colorImaginary;
            set { colorImaginary = value; Invalidate(); }
        }

        /// <summary>
        /// Sets the change types for each line and repaints the minimap.
        /// </summary>
        /// <param name="changes">A list of <see cref="ChangeType"/> values, one per line.</param>
        public void SetLineChanges(List<ChangeType> changes)
        {
            lineChangeTypes = changes ?? new List<ChangeType>();
            Invalidate();
        }

        /// <summary>
        /// Saturates a color for better minimap visibility by blending it towards a stronger version.
        /// </summary>
        private static Color SaturateForMinimap(Color c)
        {
            int r = Math.Max(0, c.R - 40);
            int g = Math.Max(0, c.G - 40);
            int b = Math.Max(0, c.B - 40);
            return Color.FromArgb(c.A, r, g, b);
        }

        /// <summary>
        /// Paints the diff minimap.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (lineChangeTypes == null || lineChangeTypes.Count == 0 || Height <= 0)
                return;

            int totalLines = lineChangeTypes.Count;
            float lineHeight = (float)Height / totalLines;

            // ensure each change mark is at least 2 pixels tall so it's visible
            float minHeight = Math.Max(lineHeight, 2f);

            for (int i = 0; i < totalLines; i++)
            {
                Color? color = null;
                switch (lineChangeTypes[i])
                {
                    case ChangeType.Deleted:
                        color = SaturateForMinimap(colorDeleted);
                        break;
                    case ChangeType.Inserted:
                        color = SaturateForMinimap(colorAdded);
                        break;
                    case ChangeType.Modified:
                        color = SaturateForMinimap(colorModified);
                        break;
                    case ChangeType.Imaginary:
                        color = colorImaginary;
                        break;
                }

                if (color.HasValue)
                {
                    float y = i * lineHeight;
                    using (var brush = new SolidBrush(color.Value))
                    {
                        e.Graphics.FillRectangle(brush, 0, y, Width, minHeight);
                    }
                }
            }
        }
    }
}
