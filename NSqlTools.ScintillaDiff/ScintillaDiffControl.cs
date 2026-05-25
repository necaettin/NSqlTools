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

using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using DiffPlex.Model;
using NSqlCompare.ScintillaDiff.Properties;
using ScintillaDiff.Enumerations;
using ScintillaDiff.UtilityClasses;
using ScintillaNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static ScintillaDiff.ScintillaDiffStyles;

namespace ScintillaDiff
{
    /// <summary>
    /// A control for comparing two text files using <see cref="Scintilla"/> controls.
    /// Implements the <see cref="System.Windows.Forms.UserControl" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />
    public partial class ScintillaDiffControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScintillaDiffControl"/> class.
        /// </summary>
        public ScintillaDiffControl()
        {
            InitializeComponent();

            // this tag values are for line number length counting..
            scintillaOne.Tag = -1;
            scintillaTwo.Tag = -1;

            SetSymbolMasks();

            InitScintillaMargins();

            SetSymbols();

			SetLineBackgroundColors();
        }


        #region PrivateEvents
        // if the control size has changed, set the splitter to the middle..
        private void ScintillaDiffer_SizeChanged(object sender, EventArgs e)
        {
            RecalculateSize();
        }

        // ReSharper disable once CommentTypo
        #region https://github.com/jacobslusser/ScintillaNET/wiki/Displaying-Line-Numbers

        private void Scintilla_TextChanged(object sender, EventArgs e)
        {
            Scintilla scintilla = (Scintilla) sender;

            int maxLineNumberCharLengthFromTag = (int) scintilla.Tag;

            // ReSharper disable once CommentTypo
            // Did the number of characters in the line number display change?
            // i.e. nnn VS nn, or nnnn VS nn, etc...
            var maxLineNumberCharLength = scintilla.Lines.Count.ToString().Length;
            if (maxLineNumberCharLength == maxLineNumberCharLengthFromTag)
                return;

            // Calculate the width required to display the last line number
            // and include some padding for good measure.
            const int padding = 2;
            scintilla.Margins[0].Width =
                scintilla.TextWidth(Style.LineNumber, new string('9', maxLineNumberCharLength + 1)) + padding;
            scintilla.Tag = maxLineNumberCharLength;
        }
        #endregion
        #endregion

        #region PrivateFields
        public string textLeft = string.Empty;
        public string textRight = string.Empty;
        private Bitmap imageRowAdded = Resources.plus;
        private Bitmap imageRowDeleted = Resources.minus;
        private Bitmap imageRowOk = Resources.ok;
        private Bitmap imageRowDiff = Resources.diff;

        private int imageRowAddedScintillaIndex = 28;
        private int imageRowDeletedScintillaIndex = 29;
        private int imageRowOkScintillaIndex = 30;
        private int imageRowDiffScintillaIndex = 31;

        private const int markerIndexDeleted = 21;
        private const int markerIndexInserted = 22;
        private const int markerIndexImaginary = 23;
        private const int markerIndexModified = 24;

        private const int indicatorIndexDeleted = 8;
        private const int indicatorIndexInserted = 9;
        private const int indicatorIndexImaginary = 10;
        private const int indicatorIndexModified = 11;

        private readonly int markColorCharacterChanged = 27;
        private readonly int markColorCharacterRemoved = 28;
        private readonly int markColorCharacterAdded = 29;
        private int markColorIndexRemovedOrAdded = 30;
        private int markColorIndexModifiedBackground = 31;

        private bool useRowOkSign;
        private DiffStyle diffStyle = DiffStyle.DiffList;
        private Color diffColorDeleted = Color.FromArgb(0xFF, 0XFF, 0XB2, 0XB2);
        private Color diffColorAdded = Color.FromArgb(0xFF, 0XD4, 0XF2, 0XC4);

        private Color diffColorCharDeleted = Color.FromArgb(0xFF, 0XE1, 0X7D, 0X7D);
        private Color diffColorCharAdded = Color.FromArgb(0xFF, 0X9A, 0XEA, 0X6F);

        private Color diffColorChangeBackground = Color.FromArgb(0xFF, 0XFC, 0XFF, 0X8C);
        private Color diffColorImaginary = Color.FromArgb(0xFF, 0XD0, 0XD0, 0XD0);
        private int diffIndex;
        private readonly StringBuilder builderLeft = new StringBuilder();
        private readonly StringBuilder builderRight = new StringBuilder();

        private bool characterComparison;
        private bool characterComparisonMarkAddRemove;
        #endregion

        #region PublicProperties
        /// <summary>
        /// Gets the left <see cref="Scintilla"/> control.
        /// </summary>
        [Browsable(false)]
        // ReSharper disable once ConvertToAutoPropertyWhenPossible
        // ReSharper disable once UnusedMember.Global
        public Scintilla LeftScintilla => scintillaOne;

        /// <summary>
        /// Gets the right <see cref="Scintilla"/> control.
        /// </summary>
        [Browsable(false)]
        // ReSharper disable once ConvertToAutoPropertyWhenPossible
        // ReSharper disable once UnusedMember.Global
        public Scintilla RightScintilla => scintillaTwo;

        /// <summary>
        /// Gets the difference locations found by the <see cref="Differ"/> class.
        /// </summary>
        [Browsable(false)]
        public List<int> DiffLocations { get; internal set; } = new List<int>();

		/// <summary>
		/// Gets or sets the value indicating whether the entire line of a change should be highlighted, or just the text within that line.
		/// </summary>
        [Browsable(false)]
		public bool IsEntireLineHighlighted { get; set; } = false;

        /// <summary>
        /// Gets or sets the value indicating whether the entire line of a change should be highlighted, or just the text within that line.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        // ReSharper disable once IdentifierTypo :: this is left behind to maintain backwards compatibility..
        // ReSharper disable once UnusedMember.Global :: this is left behind to maintain backwards compatibility..
        public bool IsEntireLineHighligted => IsEntireLineHighlighted; // don't

        #endregion

        #region PublicEvents        
        /// <summary>
        /// A delegate for the <see cref="ExternalStyleNeeded"/> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="StyleRefreshEventArgs"/> instance containing the event data.</param>
        public delegate void OnExternalStyleNeeded(object sender, StyleRefreshEventArgs e);

        /// <summary>
        /// Occurs when external styling is needed to keep the document style up to date
        /// (i.e. a class property change has caused the diff to update thus clearing the document's style).
        /// </summary>
        public event OnExternalStyleNeeded ExternalStyleNeeded;
        #endregion

        #region PublicVisualProperties

        /// <summary>
        /// Gets or sets a value indicating whether to use character comparison on lines.
        /// </summary>
        /// <value><c>true</c> if to use character comparison on lines; otherwise, <c>false</c>.</value>
        [Browsable(true)]
        [Category("Behaviour")]
        [Description("Gets or sets a value indicating whether to use character comparison on lines.")]
        public bool CharacterComparison
        {
            get => characterComparison;
            set
            {
                if (value != characterComparison)
                {
                    characterComparison = value;
                    DiffTexts();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the character comparison should mark added and removed characters.
        /// </summary>
        /// <value><c>true</c> if the character comparison should mark added and removed characters; otherwise, <c>false</c>.</value>
        [Browsable(true)]
        [Category("Behaviour")]
        [Description("Gets or sets a value indicating whether the character comparison should mark added and removed characters.")]
        public bool CharacterComparisonMarkAddRemove 
        {
            get => characterComparisonMarkAddRemove;
            set
            {
                if (value != characterComparisonMarkAddRemove)
                {
                    characterComparisonMarkAddRemove = value;
                    DiffTexts();
                }
            }
        }

        /// <summary>
        /// Gets or sets the symbol for a removed character.
        /// </summary>
        /// <value>The removed character symbol.</value>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Gets or sets the symbol for a removed character.")]
        public char RemovedCharacterSymbol { get; set; } = '-';

        /// <summary>
        /// Gets or sets the symbol for an added character.
        /// </summary>
        /// <value>The added character symbol.</value>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Gets or sets the symbol for an added character.")]
        public char AddedCharacterSymbol { get; set; } = '+';


        /// <summary>
        /// Gets or sets the index for the style for a mark color used by the <see cref="Scintilla"/> control to indicate a addition or a deletion difference.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">The value must be between 0 and 31.</exception>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Gets or sets the index for the style index for a mark color used by the Scintilla control to indicate a addition or a deletion difference.")]
        public int MarkColorIndexModifiedBackground
        {
            get => markColorIndexModifiedBackground;

            set
            {
                if (value != markColorIndexModifiedBackground)
                {
                    if (value < 0 || value > 31)
                    {
                        // ReSharper disable once LocalizableElement
                        throw new ArgumentOutOfRangeException(nameof(value), "The value must be between 0 and 31.");
                    }

                    markColorIndexModifiedBackground = value;
                    DiffTexts();
                }
            }
        }

        /// <summary>
        /// Gets or sets the index for the style for a background color color used by the <see cref="Scintilla"/> control to indicate a change in file line.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">The value must be between 0 and 31.</exception>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Gets or sets the index for the style for a background color color used by the Scintilla control to indicate a change in file line.")]
        public int MarkColorIndexRemovedOrAdded
        {
            get => markColorIndexRemovedOrAdded;

            set
            {
                if (value != markColorIndexRemovedOrAdded)
                {
                    if (value < 0 || value > 31)
                    {
                        // ReSharper disable once LocalizableElement
                        throw new ArgumentOutOfRangeException(nameof(value), "The value must be between 0 and 31.");
                    }

                    markColorIndexRemovedOrAdded = value;
                    DiffTexts();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to mark unchanged lines with an ok sign.
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Gets or sets a value indicating whether to mark unchanged lines with an ok sign.")]
        public bool UseRowOkSign
        {
            get => useRowOkSign;

            set
            {
                if (useRowOkSign != value)
                {
                    useRowOkSign = value;
                    DiffTexts();
                }
            }
        }

        /// <summary>
        /// Gets or sets a diff style of the control.
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Gets or sets a diff style of the control.")]
        public DiffStyle DiffStyle
        {
            get => diffStyle;

            set
            {
                if (diffStyle != value)
                {
                    diffStyle = value;

                    // don't synchronize the scroll bars with list view..
                    if (diffStyle == DiffStyle.DiffList)
                    {
                        scintillaOne.ScrollSync = null;
                        scintillaTwo.ScrollSync = null;
                    }
                    else
                    {
                        // synchronize the scroll bars with side-by-side view..
                        scintillaOne.ScrollSync = scintillaTwo;
                        scintillaTwo.ScrollSync = scintillaOne;
                    }

                    DiffTexts();
                }
            }
        }

        /// <summary>
        /// Gets or sets the indicator for the diff that a row was added.
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Gets or sets the indicator for the diff that a row was added.")]
        public Bitmap ImageRowAdded
        {
            get => imageRowAdded;
            set
            {
                if (value != imageRowAdded && value != null)
                {
                    imageRowAdded = value;
                    SetSymbols();
                }
            }
        }

        /// <summary>
        /// Gets or sets the index for the <see cref="ImageRowAdded"/> used by the <see cref="Scintilla"/> control.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">The value must be between 0 and 31.</exception>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Gets or sets the index for the ImageRowAdded used by the Scintilla control.")]
        public int ImageRowAddedScintillaIndex
        {
            get => imageRowAddedScintillaIndex;

            set
            {
                if (value != imageRowAddedScintillaIndex)
                {
                    if (value < 0 || value > 31)
                    {
                        // ReSharper disable once LocalizableElement
                        throw new ArgumentOutOfRangeException(nameof(value), "The value must be between 0 and 31.");
                    }

                    imageRowAddedScintillaIndex = value;
                    SetSymbolMasks();
                }
            }
        }

        /// <summary>
        /// Gets or sets the indicator for the diff that a row was deleted.
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Gets or sets the indicator for the diff that a row was deleted.")]
        public Bitmap ImageRowDeleted
        {
            get => imageRowDeleted;
            set
            {
                if (value != imageRowDeleted && value != null)
                {
                    imageRowDeleted = value;
                    SetSymbols();
                }
            }
        }

        /// <summary>
        /// Gets or sets the index for the <see cref="ImageRowDeleted"/> used by the <see cref="Scintilla"/> control.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">The value must be between 0 and 31.</exception>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Gets or sets the index for the ImageRowDeleted used by the Scintilla control.")]
        public int ImageRowDeletedScintillaIndex
        {
            get => imageRowDeletedScintillaIndex;

            set
            {
                if (value != imageRowDeletedScintillaIndex)
                {
                    if (value < 0 || value > 31)
                    {
                        // ReSharper disable once LocalizableElement
                        throw new ArgumentOutOfRangeException(nameof(value), "The value must be between 0 and 31.");
                    }

                    imageRowDeletedScintillaIndex = value;
                    SetSymbolMasks();
                }
            }
        }

        /// <summary>
        /// Gets or sets the indicator for the diff that a row hasn't changed.
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Gets or sets the indicator for the diff that a row hasn't changed.")]
        public Bitmap ImageRowOk
        {
            get => imageRowOk;
            set
            {
                if (value != imageRowOk && value != null)
                {
                    imageRowOk = value;
                    SetSymbols();
                }
            }
        }

        /// <summary>
        /// Gets or sets the index for the <see cref="ImageRowOk"/> used by the <see cref="Scintilla"/> control.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">The value must be between 0 and 31.</exception>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Gets or sets the index for the ImageRowOk used by the Scintilla control.")]
        public int ImageRowOkScintillaIndex
        {
            get => imageRowOkScintillaIndex;

            set
            {
                if (value != imageRowOkScintillaIndex)
                {
                    if (value < 0 || value > 31)
                    {
                        // ReSharper disable once LocalizableElement
                        throw new ArgumentOutOfRangeException(nameof(value), "The value must be between 0 and 31.");
                    }

                    imageRowOkScintillaIndex = value;
                    SetSymbolMasks();
                }
            }
        }

        /// <summary>
        /// Gets or sets the indicator for the diff that two rows have some differences.
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Gets or sets the indicator for the diff that two rows have some differences.")]
        public Bitmap ImageRowDiff
        {
            get => imageRowDiff;
            set
            {
                if (value != imageRowDiff && value != null)
                {
                    imageRowDiff = value;
                    SetSymbols();
                }
            }
        }

        /// <summary>
        /// Gets or sets the index for the <see cref="ImageRowDiff"/> used by the <see cref="Scintilla"/> control.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">The value must be between 0 and 31.</exception>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Gets or sets the index for the ImageRowDiff used by the Scintilla control.")]
        public int ImageRowDiffScintillaIndex
        {
            get => imageRowDiffScintillaIndex;

            set
            {
                if (value != imageRowDiffScintillaIndex)
                {
                    if (value < 0 || value > 31)
                    {
                        // ReSharper disable once LocalizableElement
                        throw new ArgumentOutOfRangeException(nameof(value), "The value must be between 0 and 31.");
                    }

                    imageRowDiffScintillaIndex = value;
                    SetSymbolMasks();
                }
            }
        }

        /// <summary>
        /// Gets or sets the text on the left <see cref="Scintilla"/> control.
        /// </summary>
        [Browsable(true)]
        [Category("Diff")]
        [Description("Gets or sets the text on the left Scintilla control.")]
        public string TextLeft
        {
            get => textLeft;
            set
            {
                textLeft = value;
                DiffTexts();
            }
        }

        /// <summary>
        /// Gets or sets the text on the right <see cref="Scintilla"/> control.
        /// </summary>
        [Browsable(true)]
        [Category("Diff")]
        [Description("Gets or sets the text on the right Scintilla control.")]
        public string TextRight
        {
            get => textRight;
            set
            {
                textRight = value;
                DiffTexts();
            }
        }

        /// <summary>
        /// Gets or sets the text deleted color for the <see cref="Scintilla"/> control.
        /// </summary>
        [Browsable(true)]
        [Category("Diff")]
        [Description("Gets or sets the text deleted color for the Scintilla control.")]
        public Color DiffColorDeleted
        {
            get => diffColorDeleted;
            set
            {
                if (value != diffColorDeleted)
                {
                    diffColorDeleted = value;
                    DiffTexts();
                }
            }
        }

        /// <summary>
        /// Gets or sets the text inserted color for the <see cref="Scintilla"/> control.
        /// </summary>
        [Browsable(true)]
        [Category("Diff")]
        [Description("Gets or sets the text inserted color for the Scintilla control.")]
        public Color DiffColorAdded
        {
            get => diffColorAdded;
            set
            {
                if (value != diffColorAdded)
                {
                    diffColorAdded = value;
                    DiffTexts();
                }
            }
        }

        /// <summary>
        /// Gets or sets the text deleted color for the <see cref="Scintilla"/> control.
        /// </summary>
        [Browsable(true)]
        [Category("Diff")]
        [Description("Gets or sets the character deleted color for the Scintilla control.")]
        public Color DiffColorCharDeleted
        {
            get => diffColorCharDeleted;
            set
            {
                if (value != diffColorCharDeleted)
                {
                    diffColorCharDeleted = value;
                    DiffTexts();
                }
            }
        }

        /// <summary>
        /// Gets or sets the text inserted color for the <see cref="Scintilla"/> control.
        /// </summary>
        [Browsable(true)]
        [Category("Diff")]
        [Description("Gets or sets the character inserted color for the Scintilla control.")]
        public Color DiffColorCharAdded
        {
            get => diffColorCharAdded;
            set
            {
                if (value != diffColorCharAdded)
                {
                    diffColorCharAdded = value;
                    DiffTexts();
                }
            }
        }

        /// <summary>
        /// Gets or sets the background color for a changed text row for the <see cref="Scintilla"/> control.
        /// </summary>
        [Browsable(true)]
        [Category("Diff")]
        [Description("Gets or sets the background color for a changed text row for the Scintilla control.")]
        public Color DiffColorChangeBackground
        {
            get => diffColorChangeBackground;
            set
            {
                if (value != diffColorChangeBackground)
                {
                    diffColorChangeBackground = value;
                    DiffTexts();
                }
            }
        }
        #endregion


        #region PrivateMethods
        /// <summary>
        /// Re-calculates the layout. With TableLayoutPanel this is handled automatically.
        /// </summary>
        private void RecalculateSize()
        {
        }

        /// <summary>
        /// Sets the margin styles of both the <see cref="Scintilla"/> controls.
        /// </summary>
        private void InitScintillaMargins()
        {
            scintillaOne.Margins[0].Type = MarginType.Number;
            scintillaTwo.Margins[0].Type = MarginType.Number;

            scintillaOne.Margins[1].Width = 25;
            scintillaOne.Margins[1].Type = MarginType.Symbol;

            scintillaTwo.Margins[1].Width = 25;
            scintillaTwo.Margins[1].Type = MarginType.Symbol;
        }

        /// <summary>
        /// Sets the bit masks to the second margin symbols.
        /// </summary>
        private void SetSymbolMasks()
        {
            // the Scintilla does like this bit masks or "bitmaps"..
            scintillaOne.Margins[1].Mask =
                GetScintillaSymbolIndex(imageRowAddedScintillaIndex) | 
                GetScintillaSymbolIndex(imageRowDeletedScintillaIndex) |
                GetScintillaSymbolIndex(imageRowDiffScintillaIndex) |
                GetScintillaSymbolIndex(imageRowOkScintillaIndex);
            scintillaTwo.Margins[1].Mask = scintillaOne.Margins[1].Mask;
        }

        /// <summary>
        /// Shifts value of one with the given amount to the left.
        /// </summary>
        /// <param name="amount">The amount of how much to shift number one to the left.</param>
        /// <returns>An unsigned integer containing the value shifted to the left.</returns>
        uint GetScintillaSymbolIndex(int amount)
        {
            return (uint) 1 << amount;
        }

        /// <summary>
        /// Sets the symbol images for both the left and right <see cref="Scintilla"/> controls.
        /// </summary>
        private void SetSymbols()
        {
            // the plus-symbol..
            scintillaOne.Markers[imageRowAddedScintillaIndex].Symbol = MarkerSymbol.RgbaImage;
            scintillaOne.Markers[imageRowAddedScintillaIndex].DefineRgbaImage(imageRowAdded);

            // the minus-symbol..
            scintillaOne.Markers[imageRowDeletedScintillaIndex].Symbol = MarkerSymbol.RgbaImage;
            scintillaOne.Markers[imageRowDeletedScintillaIndex].DefineRgbaImage(imageRowDeleted);

            // the plus-symbol..
            scintillaTwo.Markers[imageRowAddedScintillaIndex].Symbol = MarkerSymbol.RgbaImage;
            scintillaTwo.Markers[imageRowAddedScintillaIndex].DefineRgbaImage(imageRowAdded);

            // the minus-symbol..
            scintillaTwo.Markers[imageRowDeletedScintillaIndex].Symbol = MarkerSymbol.RgbaImage;
            scintillaTwo.Markers[imageRowDeletedScintillaIndex].DefineRgbaImage(imageRowDeleted);

            // the row ok symbol..
            scintillaOne.Markers[imageRowOkScintillaIndex].Symbol = MarkerSymbol.RgbaImage;
            scintillaOne.Markers[imageRowOkScintillaIndex].DefineRgbaImage(imageRowOk);

            // the row ok symbol..
            scintillaTwo.Markers[imageRowOkScintillaIndex].Symbol = MarkerSymbol.RgbaImage;
            scintillaTwo.Markers[imageRowOkScintillaIndex].DefineRgbaImage(imageRowOk);

            // the row diff symbol..
            scintillaOne.Markers[imageRowDiffScintillaIndex].Symbol = MarkerSymbol.RgbaImage;
            scintillaOne.Markers[imageRowDiffScintillaIndex].DefineRgbaImage(imageRowDiff);

            // the row diff symbol..
            scintillaTwo.Markers[imageRowDiffScintillaIndex].Symbol = MarkerSymbol.RgbaImage;
            scintillaTwo.Markers[imageRowDiffScintillaIndex].DefineRgbaImage(imageRowDiff);
        }

    /// <summary>
		/// Sets the colors for the background of lines, depending on change type.
		/// Uses Scintilla markers for full-line background coloring, combined with
		/// per-change-type indicators for reliable text-area coloring on all lines.
		/// </summary>
		private void SetLineBackgroundColors()
		{
			scintillaOne.Markers[markerIndexDeleted].Symbol = MarkerSymbol.Background;
			scintillaOne.Markers[markerIndexDeleted].SetBackColor(DiffColorDeleted);
			scintillaTwo.Markers[markerIndexDeleted].Symbol = MarkerSymbol.Background;
			scintillaTwo.Markers[markerIndexDeleted].SetBackColor(DiffColorDeleted);

			scintillaOne.Markers[markerIndexInserted].Symbol = MarkerSymbol.Background;
			scintillaOne.Markers[markerIndexInserted].SetBackColor(DiffColorAdded);
			scintillaTwo.Markers[markerIndexInserted].Symbol = MarkerSymbol.Background;
			scintillaTwo.Markers[markerIndexInserted].SetBackColor(DiffColorAdded);

			scintillaOne.Markers[markerIndexImaginary].Symbol = MarkerSymbol.Background;
			scintillaOne.Markers[markerIndexImaginary].SetBackColor(diffColorImaginary);
			scintillaTwo.Markers[markerIndexImaginary].Symbol = MarkerSymbol.Background;
			scintillaTwo.Markers[markerIndexImaginary].SetBackColor(diffColorImaginary);

			scintillaOne.Markers[markerIndexModified].Symbol = MarkerSymbol.Background;
			scintillaOne.Markers[markerIndexModified].SetBackColor(DiffColorChangeBackground);
			scintillaTwo.Markers[markerIndexModified].Symbol = MarkerSymbol.Background;
			scintillaTwo.Markers[markerIndexModified].SetBackColor(DiffColorChangeBackground);

			SetupLineIndicator(scintillaOne, indicatorIndexDeleted, DiffColorDeleted);
			SetupLineIndicator(scintillaTwo, indicatorIndexDeleted, DiffColorDeleted);
			SetupLineIndicator(scintillaOne, indicatorIndexInserted, DiffColorAdded);
			SetupLineIndicator(scintillaTwo, indicatorIndexInserted, DiffColorAdded);
			SetupLineIndicator(scintillaOne, indicatorIndexImaginary, diffColorImaginary);
			SetupLineIndicator(scintillaTwo, indicatorIndexImaginary, diffColorImaginary);
			SetupLineIndicator(scintillaOne, indicatorIndexModified, DiffColorChangeBackground);
			SetupLineIndicator(scintillaTwo, indicatorIndexModified, DiffColorChangeBackground);
		}

		/// <summary>
		/// Configures an indicator for line-level background coloring.
		/// </summary>
		private static void SetupLineIndicator(Scintilla scintilla, int indicatorIndex, Color color)
		{
			scintilla.Indicators[indicatorIndex].Style = IndicatorStyle.StraightBox;
			scintilla.Indicators[indicatorIndex].Under = true;
			scintilla.Indicators[indicatorIndex].ForeColor = color;
			scintilla.Indicators[indicatorIndex].OutlineAlpha = 0;
			scintilla.Indicators[indicatorIndex].Alpha = 255;
		}

		/// <summary>
		/// Appends a row to the left <see cref="Scintilla"/> control.
		/// </summary>
		/// <param name="rowText">The text to append to the left <see cref="Scintilla"/> document row.</param>
		private void AppendRowAdded(string rowText)
        {
            builderLeft.AppendLine(rowText);
//            scintillaOne.Text += rowText + Environment.NewLine;
        }

        /// <summary>
        /// Sets a row added marker based on the given <paramref name="index"/> row amount.
        /// </summary>
        /// <param name="index">The index of the row to set the marker to.</param>
        private void AppendRowAddedMarker(int index)
        {
            scintillaOne.Lines[index].MarkerAdd(imageRowAddedScintillaIndex);
        }

        /// <summary>
        /// Sets a row added marker based on the given <paramref name="index"/> row amount to either the left side or the right side <see cref="Scintilla"/> document.
        /// </summary>
        /// <param name="index">The index of the row to set the marker to.</param>
        /// <param name="left">A value indicating whether to append the marker to the left or to the right side <see cref="Scintilla"/> document.</param>
        private void AppendRowAddedMarker(int index, bool left)
        {
            if (left)
            {
                scintillaOne.Lines[index].MarkerAdd(imageRowAddedScintillaIndex);
            }
            else
            {
                scintillaTwo.Lines[index].MarkerAdd(imageRowAddedScintillaIndex);
            }
        }

        /// <summary>
        /// Appends a row to the left <see cref="Scintilla"/> control.
        /// </summary>
        /// <param name="rowText">The text to append to the right <see cref="Scintilla"/> document row.</param>
        private void AppendRowDeleted(string rowText)
        {
            builderLeft.AppendLine(rowText);
            //scintillaOne.Text += rowText + Environment.NewLine;
        }

        /// <summary>
        /// Sets a row deleted marker based on the given <paramref name="index"/> row amount.
        /// </summary>
        /// <param name="index">The index of the row to set the marker to.</param>
        private void AppendRowDeletedMarker(int index)
        {
            scintillaOne.Lines[index].MarkerAdd(imageRowDeletedScintillaIndex);
        }

        /// <summary>
        /// Sets a row deleted marker based on the given <paramref name="index"/> row amount to either the left side or the right side <see cref="Scintilla"/> document.
        /// </summary>
        /// <param name="index">The index of the row to set the marker to.</param>
        /// <param name="left">A value indicating whether to append the marker to the left or to the right side <see cref="Scintilla"/> document.</param>
        private void AppendRowDeletedMarker(int index, bool left)
        {
            if (left)
            {
                scintillaOne.Lines[index].MarkerAdd(imageRowDeletedScintillaIndex);
            }
            else
            {
                scintillaTwo.Lines[index].MarkerAdd(imageRowDeletedScintillaIndex);
            }
        }

        /// <summary>
        /// Sets a row ok marker based on the given <paramref name="index"/> row amount.
        /// </summary>
        /// <param name="index">The index of the row to set the marker to.</param>
        private void AppendRowOkMarker(int index)
        {
            if (!UseRowOkSign)
            {
                return;
            }
            scintillaOne.Lines[index].MarkerAdd(imageRowOkScintillaIndex);
        }

        /// <summary>
        /// Sets a row ok marker based on the given <paramref name="index"/> row amount to either the left side or the right side <see cref="Scintilla"/> document.
        /// </summary>
        /// <param name="index">The index of the row to set the marker to.</param>
        /// <param name="left">A value indicating whether to append the marker to the left or to the right side <see cref="Scintilla"/> document.</param>
        private void AppendRowOkMarker(int index, bool left)
        {
            if (!UseRowOkSign)
            {
                return;
            }

            if (left)
            {
                scintillaOne.Lines[index].MarkerAdd(imageRowOkScintillaIndex);
            }
            else
            {
                scintillaTwo.Lines[index].MarkerAdd(imageRowOkScintillaIndex);
            }
        }

        /// <summary>
        /// Sets a row differ marker based on the given <paramref name="index"/> row amount.
        /// </summary>
        /// <param name="index">The index of the row to set the marker to.</param>
        private void AppendRowDiffMarker(int index)
        {
            scintillaOne.Lines[index].MarkerAdd(imageRowDiffScintillaIndex);
        }

        /// <summary>
        /// Sets a row differ marker based on the given <paramref name="index"/> row amount to either the left side or the right side <see cref="Scintilla"/> document.
        /// </summary>
        /// <param name="index">The index of the row to set the marker to.</param>
        /// <param name="left">A value indicating whether to append the marker to the left or to the right side <see cref="Scintilla"/> document.</param>
        private void AppendRowDiffMarker(int index, bool left)
        {
            if (left)
            {
                scintillaOne.Lines[index].MarkerAdd(imageRowDiffScintillaIndex);
            }
            else
            {
                scintillaTwo.Lines[index].MarkerAdd(imageRowDiffScintillaIndex);
            }
        }

        /// <summary>
        /// Appends the same row to the left and right <see cref="Scintilla"/> controls.
        /// </summary>
        /// <param name="rowText">The text to append to the right and to the left <see cref="Scintilla"/> document rows.</param>
        private void AppendRow(string rowText)
        {
            builderLeft.AppendLine(rowText);
            builderRight.AppendLine(rowText);
            //scintillaOne.Text += rowText + Environment.NewLine;
            //scintillaTwo.Text += rowText + Environment.NewLine;
        }

        /// <summary>
        /// Appends different rows to the left and right side <see cref="Scintilla"/> controls.
        /// </summary>
        /// <param name="rowTextLeft">The text to append to the the left <see cref="Scintilla"/> document rows.</param>
        /// <param name="rowTextRight">The text to append to the right <see cref="Scintilla"/> document rows.</param>
        private void AppendRow(string rowTextLeft, string rowTextRight)
        {
            builderLeft.AppendLine(rowTextLeft);
            builderRight.AppendLine(rowTextRight);
//            scintillaOne.Text += rowTextLeft + Environment.NewLine;
//            scintillaTwo.Text += rowTextRight + Environment.NewLine;
        }

        /// <summary>
        /// Compares to contents of the two texts if both are assigned in a list style view.
        /// </summary>
        private void DiffTextsList()
        {
            SuspendLayout();

            // hide the right panel and diff map for list view..
            scintillaTwo.Visible = false;
            diffMapPanel.Visible = false;
            diffMapPanel.SetLineChanges(new List<ChangeType>());
            tlpMain.ColumnStyles[1].Width = 0;
            tlpMain.ColumnStyles[2].Width = 0;
            tlpMain.ColumnStyles[0].SizeType = System.Windows.Forms.SizeType.Percent;
            tlpMain.ColumnStyles[0].Width = 100;

            // validate that there is text to compare..
            if (!string.IsNullOrEmpty(TextLeft)  || !string.IsNullOrEmpty(TextRight))
            {
                // clear the two Scintilla control contents..
                scintillaOne.Text = string.Empty;
                scintillaTwo.Text = string.Empty;
                
                // clear the two StringBuilder instance contents..
                builderLeft.Clear();
                builderRight.Clear();

                // create a diff for a list style text comparison..
                var diffBuilder = new InlineDiffBuilder(new Differ());

                // compare the two texts..
                var diff = diffBuilder.BuildDiffModel(TextLeft, TextRight);

                // output the diff data to the left side Scintilla control;
                // first the rows so the style can be appended afterwards..
                foreach (var line in diff.Lines)
                {
                    switch (line.Type)
                    {
                        case ChangeType.Inserted:
                            AppendRowAdded(line.Text);
                            break;
                        case ChangeType.Deleted:
                            AppendRowDeleted(line.Text);
                            break;
                        case ChangeType.Unchanged:
                            AppendRow(line.Text);
                            break;
                    }
                }

                scintillaOne.Text = builderLeft.ToString();

                // set a variable for the line index..
                int lineIndex = 0;

                // set the style for the lines now that the Scintilla document's
                // contents have been set..
                foreach (var line in diff.Lines)
                {
					if (IsEntireLineHighlighted)
						SetLineBackgroundColor(lineIndex, line.Type);

					switch (line.Type)
                    {
                        case ChangeType.Inserted:
                            // save the line location..
                            SaveLineLocation(lineIndex);
							AppendRowAddedMarker(lineIndex); 
                            break;
                        case ChangeType.Deleted:
                            // save the line location..
                            SaveLineLocation(lineIndex);
							AppendRowDeletedMarker(lineIndex);
                            break;
                        case ChangeType.Unchanged:
                            AppendRowOkMarker(lineIndex);
                            break;
                        case ChangeType.Modified:
                            // save the line location..
                            SaveLineLocation(lineIndex);
							AppendRowDiffMarker(lineIndex);
                            break;
                    }

                    lineIndex++;
                }
                
                // reset the index of the next difference..
                diffIndex = -1;

                // raise the ExternalStyleNeeded event if it's subscribed..
                ExternalStyleNeeded?.Invoke(this, new StyleRefreshEventArgs {Scintilla = LeftScintilla});

                // sort the jump locations..
                DiffLocations.Sort();
            }

            ResumeLayout(true);
        }

        /// <summary>
        /// Saves the line location to the <see cref="DiffLocations"/> property.
        /// </summary>
        /// <param name="lineIndex">The index of the row which location to save.</param>
        private void SaveLineLocation(int lineIndex)
        {
            if (DiffLocations.Contains(lineIndex))
            {
                return;
            }

            DiffLocations.Add(lineIndex);
        }

        private KeyValuePair<string, string> SpanLeftRightChars(DiffResult diffResult, int line, string oldLine,
            string newLine, ref List<CharacterChangeType> charChangedList)
        {
            if (diffResult == null || diffResult.DiffBlocks.Count == 0)
            {
                return new KeyValuePair<string, string>(oldLine, newLine);
            }

            oldLine = oldLine ?? string.Empty;
            newLine = newLine ?? string.Empty;

            if (oldLine.Length == newLine.Length)
            {
                for (int i = 0; i < oldLine.Length; i++)
                {
                    if (oldLine[i] != newLine[i])
                    {
                        charChangedList.Add(new CharacterChangeType
                            {ChangeType = CharChangedType.Modified, Length = 1, LineIndex = line, Position = i});
                    }
                }

                return new KeyValuePair<string, string>(oldLine, newLine);
            }

            foreach (var diffBlock in diffResult.DiffBlocks)
            {
                if (diffBlock.DeleteCountA == diffBlock.InsertCountB &&
                    diffBlock.DeleteStartA == diffBlock.InsertStartB)
                {
                    charChangedList.Add(new CharacterChangeType
                    {
                        ChangeType = CharChangedType.Modified, Length = diffBlock.DeleteCountA, LineIndex = line,
                        Position = diffBlock.DeleteStartA
                    });
                    continue;
                }

                if (diffBlock.DeleteCountA > 0)
                {
                    if (CharacterComparisonMarkAddRemove)
                    {
                        oldLine = oldLine.Insert(diffBlock.DeleteStartA,
                            new string(AddedCharacterSymbol, diffBlock.DeleteCountA));

                        charChangedList.Add(new CharacterChangeType
                        {
                            ChangeType = CharChangedType.Added, Length = diffBlock.DeleteCountA, LineIndex = line,
                            Position = diffBlock.DeleteStartA
                        });
                    }
                }

                if (diffBlock.InsertCountB > 0)
                {
                    if (CharacterComparisonMarkAddRemove)
                    {
                        newLine = newLine.Insert(diffBlock.InsertStartB,
                            new string(RemovedCharacterSymbol, diffBlock.InsertCountB));

                        charChangedList.Add(new CharacterChangeType
                        {
                            ChangeType = CharChangedType.Removed, Length = diffBlock.InsertCountB, LineIndex = line,
                            Position = diffBlock.InsertStartB
                        });
                    }
                }
            }
            return new KeyValuePair<string, string>(oldLine, newLine);
        }

        /// <summary>
        /// Compares to contents of the two texts if both are assigned in a side by side style view.
        /// </summary>
        private void DiffTextsSideBySide()
        {
            SuspendLayout();

            // show all three columns for side-by-side view..
            scintillaTwo.Visible = true;
            diffMapPanel.Visible = true;
            tlpMain.ColumnStyles[0].SizeType = System.Windows.Forms.SizeType.Percent;
            tlpMain.ColumnStyles[0].Width = 50;
            tlpMain.ColumnStyles[1].SizeType = System.Windows.Forms.SizeType.Absolute;
            tlpMain.ColumnStyles[1].Width = 14;
            tlpMain.ColumnStyles[2].SizeType = System.Windows.Forms.SizeType.Percent;
            tlpMain.ColumnStyles[2].Width = 50;

            // recalculate the position of the split control's
            // splitter..
            RecalculateSize();

            // validate that there is text to compare..
            if (!string.IsNullOrEmpty(TextLeft)  || !string.IsNullOrEmpty(TextRight))
            {
                // clear the two Scintilla control contents..
                scintillaOne.Text = string.Empty;
                scintillaTwo.Text = string.Empty;

                scintillaOne.ClearAll();
                scintillaTwo.ClearAll();

                // clear the two StringBuilder instance contents..
                builderLeft.Clear();
                builderRight.Clear();

                // initialize a new instance of the Differ class..
                var differ = new Differ();

                // create a diff for a side by side text comparison..
                var diffBuilder = new SideBySideDiffBuilder(differ);

                // compare the two texts..
                var diff = diffBuilder.BuildDiffModel(TextLeft, TextRight);
                List<CharacterChangeType> changedCharacters =
                    new List<CharacterChangeType>();

                // output the diff data to the left and to the right side Scintilla controls;
                // first the rows so the style can be appended afterwards..
                for (int i = 0; i < diff.OldText.Lines.Count; i++)
                {
                    if (CharacterComparison)
                    {
                        var diffResult = Differ.Instance.CreateCharacterDiffs(diff.NewText.Lines[i].Text ?? string.Empty,
                            diff.OldText.Lines[i].Text ?? string.Empty, false, false);

                        var span = SpanLeftRightChars(diffResult, i,
                            diff.OldText.Lines[i].Text, diff.NewText.Lines[i].Text, ref changedCharacters);
                        AppendRow(span.Key, span.Value);
                    }
                    else // no character comparison..
                    {
                        AppendRow(diff.OldText.Lines[i].Text, diff.NewText.Lines[i].Text);
                    }
                }

                if (CharacterComparison)
                {
                    diff = diffBuilder.BuildDiffModel(builderLeft.ToString(), builderRight.ToString());
                }

                var leftText = builderLeft.ToString().TrimEnd('\r', '\n');
				var rightText = builderRight.ToString().TrimEnd('\r', '\n');

				scintillaOne.Text = leftText;
				scintillaTwo.Text = rightText;

				// clear the list of difference locations..
				DiffLocations.Clear();

				if (IsEntireLineHighlighted)
				{
					for (int i = 0; i < diff.OldText.Lines.Count; i++)
					{
						var leftType = diff.OldText.Lines[i].Type;
						var rightType = diff.NewText.Lines[i].Type;

						if (leftType == ChangeType.Modified || rightType == ChangeType.Modified)
						{
							SetLineBackgroundColor(i, ChangeType.Modified);
							SaveLineLocation(i);

							HandleDiffSubPieces(diff.NewText.Lines[i].SubPieces, i, false);
							HandleDiffSubPieces(diff.OldText.Lines[i].SubPieces, i, true);
                        }
						else
						{
							// Handle left side
							switch (leftType)
							{
								case ChangeType.Deleted:
									SetLineBackgroundColor(i, ChangeType.Deleted, true);
									AppendRowDeletedMarker(i, left: true);
									SaveLineLocation(i);
									break;
								case ChangeType.Inserted:
									SetLineBackgroundColor(i, ChangeType.Inserted, true);
									AppendRowAddedMarker(i, left: true);
									SaveLineLocation(i);
									break;
								case ChangeType.Imaginary:
									SetLineBackgroundColor(i, ChangeType.Imaginary, true);
									break;
							}

							// Handle right side
							switch (rightType)
							{
								case ChangeType.Deleted:
									SetLineBackgroundColor(i, ChangeType.Deleted, false);
									AppendRowDeletedMarker(i, left: false);
									SaveLineLocation(i);
									break;
								case ChangeType.Inserted:
									SetLineBackgroundColor(i, ChangeType.Inserted, false);
									AppendRowAddedMarker(i, left: false);
									SaveLineLocation(i);
									break;
								case ChangeType.Imaginary:
									SetLineBackgroundColor(i, ChangeType.Imaginary, false);
									break;
							}
						}
					}
				}
				else
				{
					// loop through the meta-data of the diff result and set the styling
					// for the Scintilla controls accordingly..
					for (int i = 0; i < diff.OldText.Lines.Count; i++)
					{
						var leftType = diff.OldText.Lines[i].Type;
						var rightType = diff.NewText.Lines[i].Type;

						// Handle left side
						switch (leftType)
						{
							case ChangeType.Inserted:
								AppendRowAddedMarker(i, true);
								SetLineBackgroundColor(i, ChangeType.Inserted, true);
								SaveLineLocation(i);
								break;
							case ChangeType.Deleted:
								AppendRowDeletedMarker(i, true);
								SetLineBackgroundColor(i, ChangeType.Deleted, true);
								SaveLineLocation(i);
								break;
							case ChangeType.Imaginary:
								SetLineBackgroundColor(i, ChangeType.Imaginary, true);
								break;
							case ChangeType.Unchanged:
								AppendRowOkMarker(i, true);
								break;
							case ChangeType.Modified:
								SaveLineLocation(i);
								AppendRowDiffMarker(i, true);
								SetLineBackgroundColor(i, ChangeType.Modified, true);
								HandleDiffSubPieces(diff.OldText.Lines[i].SubPieces, i, true);
								break;
						}

						// Handle right side
						switch (rightType)
						{
							case ChangeType.Inserted:
								AppendRowAddedMarker(i, false);
								SetLineBackgroundColor(i, ChangeType.Inserted, false);
								SaveLineLocation(i);
								break;
							case ChangeType.Deleted:
								AppendRowDeletedMarker(i, false);
								SetLineBackgroundColor(i, ChangeType.Deleted, false);
								SaveLineLocation(i);
								break;
							case ChangeType.Imaginary:
								SetLineBackgroundColor(i, ChangeType.Imaginary, false);
								break;
							case ChangeType.Unchanged:
								AppendRowOkMarker(i, false);
								break;
							case ChangeType.Modified:
								SaveLineLocation(i);
								AppendRowDiffMarker(i, false);
								SetLineBackgroundColor(i, ChangeType.Modified, false);
								HandleDiffSubPieces(diff.NewText.Lines[i].SubPieces, i, false);
								break;
						}
					}
				}

                MarkWithBackgroundColor(changedCharacters, diffColorCharAdded,
                    diffColorCharDeleted);

                // reset the index of the next difference..
                diffIndex = -1;

                // raise the ExternalStyleNeeded event if it's subscribed for both of the Scintilla controls..
                ExternalStyleNeeded?.Invoke(this, new StyleRefreshEventArgs {Scintilla = LeftScintilla});
                ExternalStyleNeeded?.Invoke(this, new StyleRefreshEventArgs {Scintilla = RightScintilla});

                // sort the jump locations..
                DiffLocations.Sort();

                // update the diff map panel with line change types..
                var lineChanges = new List<ChangeType>();
                for (int i = 0; i < diff.OldText.Lines.Count; i++)
                {
                    var leftType = diff.OldText.Lines[i].Type;
                    var rightType = diff.NewText.Lines[i].Type;

                    if (leftType == ChangeType.Modified || rightType == ChangeType.Modified)
                        lineChanges.Add(ChangeType.Modified);
                    else if (leftType == ChangeType.Deleted || rightType == ChangeType.Deleted)
                        lineChanges.Add(ChangeType.Deleted);
                    else if (leftType == ChangeType.Inserted || rightType == ChangeType.Inserted)
                        lineChanges.Add(ChangeType.Inserted);
                    else if (leftType == ChangeType.Imaginary || rightType == ChangeType.Imaginary)
                        lineChanges.Add(ChangeType.Imaginary);
                    else
                        lineChanges.Add(ChangeType.Unchanged);
                }
                diffMapPanel.ColorDeleted = DiffColorDeleted;
                diffMapPanel.ColorAdded = DiffColorAdded;
                diffMapPanel.ColorModified = DiffColorChangeBackground;
                diffMapPanel.ColorImaginary = diffColorImaginary;
                diffMapPanel.SetLineChanges(lineChanges);
            }

            ResumeLayout(true);
        }

        /// <summary>
        /// Marks a given position of a row with a given background color.
        /// </summary>
        /// <param name="lineIndex">The index of the line to mark with the <see cref="Scintilla"/> control.</param>
        /// <param name="subPosition">The position in the line to mark with the given color.</param>
        /// <param name="left">A value indicating whether to use the left or the right side <see cref="Scintilla"/> control.</param>
        /// <param name="diffPiece">A <see cref="DiffPiece"/> class instance to get the length of the text.</param>
        /// <param name="color">A <see cref="Color"/> to use with the marking.</param>
        private void MarkWithBackgroundColor(int lineIndex, int subPosition, bool left, DiffPiece diffPiece, Color color)
        {
            if (diffPiece.Position == null)
            {
                return;
            }

            int start = left
                ? scintillaOne.Lines[lineIndex].Position + subPosition
                : scintillaTwo.Lines[lineIndex].Position + subPosition;

            Highlight.HighlightRange(left ? scintillaOne : scintillaTwo, MarkColorIndexModifiedBackground, start,
                diffPiece.Text.Length, color);
        }

        private void ClearStylesArea(Scintilla scintilla, int line, int position, int length)
        {
            for (int i = markColorCharacterChanged; i <= markColorIndexModifiedBackground; i++)
            {
                Highlight.ClearStyleArea(scintilla, line, position, length,
                    i);
            }
        }

        private void MarkWithBackgroundColor(List<CharacterChangeType> changedCharacters,
            Color colorAdd, Color colorDeleted)
        {
            foreach (var changedCharacter in changedCharacters)
            {
                int startOne = scintillaOne.Lines[changedCharacter.LineIndex].Position + changedCharacter.Position;
                int startTwo = scintillaTwo.Lines[changedCharacter.LineIndex].Position + changedCharacter.Position;

                ClearStylesArea(scintillaOne, changedCharacter.LineIndex, changedCharacter.Position, changedCharacter.Length);
                ClearStylesArea(scintillaTwo, changedCharacter.LineIndex, changedCharacter.Position, changedCharacter.Length);
                
                switch (changedCharacter.ChangeType)
                {
                    case CharChangedType.Modified:
                        Highlight.HighlightRange(scintillaOne, markColorCharacterChanged, startOne, changedCharacter.Length,
                            colorDeleted);
                        Highlight.HighlightRange(scintillaTwo, markColorCharacterChanged, startTwo, changedCharacter.Length,
                            colorAdd);
                        break;

                    case CharChangedType.Added:
                        Highlight.HighlightRange(scintillaOne, markColorCharacterAdded, startOne, changedCharacter.Length,
                            colorDeleted);
                        Highlight.HighlightRange(scintillaTwo, markColorCharacterRemoved, startTwo, changedCharacter.Length,
                            colorAdd);
                        break;

                    case CharChangedType.Removed:
                        Highlight.HighlightRange(scintillaOne, markColorCharacterRemoved, startOne, changedCharacter.Length,
                            colorDeleted);
                        Highlight.HighlightRange(scintillaTwo, markColorCharacterAdded, startTwo, changedCharacter.Length,
                            colorAdd);
                        break;
                }
            }
        }

        /// <summary>
        /// Marks a line of a <see cref="Scintilla"/> control with a given <paramref name="color"/>.
        /// </summary>
        /// <param name="lineIndex">The index of the line to mark with the <see cref="Scintilla"/> control.</param>
        /// <param name="color">A <see cref="Color"/> to use with the marking.</param>
        /// <param name="left">A value indicating whether to use the left or the right side <see cref="Scintilla"/> control.</param>
        private void MarkLineWithColor(int lineIndex, Color color, bool left)
        {
            int start = left
                ? scintillaOne.Lines[lineIndex].Position
                : scintillaTwo.Lines[lineIndex].Position;

            int length = left
                ? scintillaOne.Lines[lineIndex].Length
                : scintillaTwo.Lines[lineIndex].Length;

            Highlight.HighlightRange(left ? scintillaOne : scintillaTwo, MarkColorIndexRemovedOrAdded, start, length,
                color);
        }

		/// <summary>
		/// Gets the marker index for a given <see cref="ChangeType"/>.
		/// </summary>
		private static int GetMarkerIndexForChangeType(ChangeType changeType)
		{
			switch (changeType)
			{
				case ChangeType.Deleted: return markerIndexDeleted;
				case ChangeType.Inserted: return markerIndexInserted;
				case ChangeType.Imaginary: return markerIndexImaginary;
				case ChangeType.Modified: return markerIndexModified;
				default: return -1;
			}
		}

		/// <summary>
		/// Gets the indicator index for a given <see cref="ChangeType"/>.
		/// </summary>
		private static int GetIndicatorIndexForChangeType(ChangeType changeType)
		{
			switch (changeType)
			{
				case ChangeType.Deleted: return indicatorIndexDeleted;
				case ChangeType.Inserted: return indicatorIndexInserted;
				case ChangeType.Imaginary: return indicatorIndexImaginary;
				case ChangeType.Modified: return indicatorIndexModified;
				default: return -1;
			}
		}

		/// <summary>
		/// Sets the background color for the entire line of a <see cref="Scintilla"/> control.
		/// </summary>
		private void SetLineBackgroundColor(int lineIndex, ChangeType changeType)
		{
			ApplyLineHighlight(scintillaOne, lineIndex, changeType);
			ApplyLineHighlight(scintillaTwo, lineIndex, changeType);
		}

		/// <summary>
		/// Sets the background color for a specific line on either the left or the right <see cref="Scintilla"/> control.
		/// </summary>
		private void SetLineBackgroundColor(int lineIndex, ChangeType changeType, bool left)
		{
			ApplyLineHighlight(left ? scintillaOne : scintillaTwo, lineIndex, changeType);
		}

		/// <summary>
		/// Applies both a marker (full-line background) and an indicator (text-area fill) to a line.
		/// The dual approach ensures coloring is visible on both empty and non-empty lines.
		/// </summary>
		private static void ApplyLineHighlight(Scintilla scintilla, int lineIndex, ChangeType changeType)
		{
			int markerIndex = GetMarkerIndexForChangeType(changeType);
			if (markerIndex >= 0)
				scintilla.Lines[lineIndex].MarkerAdd(markerIndex);

			int indicatorIndex = GetIndicatorIndexForChangeType(changeType);
			if (indicatorIndex >= 0)
			{
				int start = scintilla.Lines[lineIndex].Position;
				int length = scintilla.Lines[lineIndex].Length;
				if (length > 0)
				{
					scintilla.IndicatorCurrent = indicatorIndex;
					scintilla.IndicatorFillRange(start, length);
				}
			}
		}

        /// <summary>
        /// Handles the difference sub-pieces in a side-by-side comparison to set a color for a word.
        /// </summary>
        /// <param name="subPieces">A list of <see cref="DiffPiece"/> class instances to be marked.</param>
        /// <param name="lineIndex">The index of the line to mark with the <see cref="Scintilla"/> control.</param>
        /// <param name="left">A value indicating whether to use the left or the right side <see cref="Scintilla"/> control.</param>
        private void HandleDiffSubPieces(List<DiffPiece> subPieces, int lineIndex, bool left)
        {
            int calcPosition = 0;
            foreach (var subPiece in subPieces)
            {
                switch (subPiece.Type)
                {
                    case ChangeType.Deleted:
                        MarkWithBackgroundColor(lineIndex, calcPosition, left, subPiece,
                            DiffColorDeleted);
                        break;

                    case ChangeType.Inserted:
                        MarkWithBackgroundColor(lineIndex, calcPosition, left, subPiece,
                            DiffColorAdded);
                        break;
                }

                if (subPiece.Position != null)
                {
                    calcPosition += subPiece.Text.Length;
                }
            }
        }

        /// <summary>
        /// Jumps the view into a given line index.
        /// </summary>
        /// <param name="lineIndex">Index of the line to jump the view to.</param>
        /// <param name="backwards">if set to <c>true</c> the call was made from the <see cref="Previous"/> method.</param>
        /// <returns><c>true</c> if the line index was valid and the view was scrolled, <c>false</c> otherwise.</returns>
        private bool JumpToLine(int lineIndex, bool backwards)
        {
            if (lineIndex < 0 || lineIndex >= DiffLocations.Count)
            {
                return false;
            }
            
            if (diffStyle == DiffStyle.DiffList)
            {
                int linePos1 = scintillaOne.Lines[DiffLocations[lineIndex]].Position;
                scintillaOne.GotoPosition(linePos1);
                scintillaOne.ScrollCaret();
            }
            else if (diffStyle == DiffStyle.DiffSideBySide)
            {
                int linePos1 = scintillaOne.Lines[DiffLocations[lineIndex]].Position;
                scintillaOne.GotoPosition(linePos1);

                int linePos2 = scintillaTwo.Lines[DiffLocations[lineIndex]].Position;
                scintillaTwo.GotoPosition(linePos2);

                scintillaOne.ScrollCaret();
                scintillaTwo.ScrollCaret();
            }

            if (!backwards && lineIndex + 1 >= DiffLocations.Count)
            {
                return false;
            }

            return true;
        }
        #endregion

        #region PublicMethods        
        /// <summary>
        /// Compares to contents of the two texts based on the <see cref="DiffStyle"/> property value.
        /// </summary>
        public void DiffTexts()
        {
            SetLineBackgroundColors();

            if (diffStyle == DiffStyle.DiffList)
            {
                DiffTextsList();
            }
            else
            {
                DiffTextsSideBySide();
            }
        }

        /// <summary>
        /// Swaps the texts to compare.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        public void SwapDiff()
        {
            string temp = textLeft;
            textLeft = textRight;
            textRight = temp;
            DiffTexts();
        }

        /// <summary>
        /// Jumps to the next difference within the diff view.
        /// </summary>
        /// <returns><c>true</c> if the navigation to the next position was possible, <c>false</c> otherwise.</returns>
        public bool Next()
        {
            if (diffIndex >= DiffLocations.Count)
            {
                return false;
            }

            diffIndex++;
            return JumpToLine(diffIndex, false);
        }

        /// <summary>
        /// Jumps to the previous difference within the diff view.
        /// </summary>
        /// <returns><c>true</c> if the navigation to the previous position was possible, <c>false</c> otherwise.</returns>
        public bool Previous()
        {
            if (diffIndex < 0)
            {
                return false;
            }

            diffIndex--;

            return JumpToLine(diffIndex, true);
        }
        #endregion
    }
}
