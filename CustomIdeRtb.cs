using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System.Linq;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;

namespace Json_Editor
{
    public partial class CustomIdeRtb : UserControl
    {
        public event MouseEventHandler MouseHover;

        private readonly LineNumberStrip _strip;

        private Stack<string> undoStack = new Stack<string>();
        private Stack<string> redoStack = new Stack<string>();

        public CustomIdeRtb()
        {
            InitializeComponent();
            _strip = new LineNumberStrip(richTextBox);
            Controls.Add(_strip);
            BorderStyle = BorderStyle.None;
            base.BackColor = richTextBox.BackColor;
        }

        public RichTextBox RichTextBox
        {
            get { return richTextBox; }
        }

        public LineNumberStrip Strip
        {
            get { return _strip; }
        }

        private void InitializeComponent()
        {
            richTextBox = new RichTextBox();
            SuspendLayout();
            //
            // richTextBox
            //
            richTextBox.BackColor = Color.FromArgb(32, 32, 32);
            richTextBox.BorderStyle = BorderStyle.None;
            richTextBox.Dock = DockStyle.Fill;
            richTextBox.ForeColor = Color.White;
            richTextBox.HideSelection = false;
            richTextBox.Location = new Point(0, 0);
            richTextBox.Name = "richTextBox";
            richTextBox.Size = new Size(781, 777);
            richTextBox.TabIndex = 0;
            richTextBox.Text = "";
            richTextBox.TextChanged += richTextBox_TextChanged;
            richTextBox.MouseUp += richTextBox_MouseUp;
            //
            // CustomIdeRtb
            //
            Controls.Add(richTextBox);
            Name = "CustomIdeRtb";
            Size = new Size(781, 777);
            ResumeLayout(false);
        }

        private void richTextBox_TextChanged(object sender, EventArgs e)
        {
            // This event handler is called whenever the text changes
            // If no undo action was performed, populate the redo stack
            if (ActiveControl is RichTextBox richTextBox && undoStack.Count == 0)
            {
                redoStack.Push(richTextBox.Rtf);
            }
        }

        private void CutAction()
        {
            if (!string.IsNullOrEmpty(richTextBox.SelectedText))
            {
                // cut the selected text
                Clipboard.SetText(richTextBox.SelectedText);
                richTextBox.SelectedText = string.Empty;
            }
        }

        private void CopyAction()
        {
            // Check if there is selected text
            if (!string.IsNullOrEmpty(richTextBox.SelectedText))
            {
                // Copy the selected text to the clipboard
                Clipboard.SetText(richTextBox.SelectedText);
            }
        }

        private void PasteAction()
        {
            // Paste the text from the clipboard
            string clipboardText = Clipboard.GetText();
            if (!string.IsNullOrEmpty(clipboardText))
            {
                richTextBox.SelectedText = clipboardText;
            }
        }

        private void RedoAction()
        {
            if (ActiveControl is RichTextBox richTextBox)
            {
                if (redoStack.Count > 0)
                {
                    if (undoStack.Count > 0) { undoStack.Push(richTextBox.Rtf); }
                    else redoStack.Push(richTextBox.Rtf);

                    richTextBox.Rtf = redoStack.Pop();
                }
            }
        }

        private void richTextBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.ContextMenuStrip = new ContextMenuStrip();
                ContextMenuStrip.Items.Add("Cut");
                ContextMenuStrip.Items.Add("Copy");
                ContextMenuStrip.Items.Add("Paste");
                ContextMenuStrip.Items.Add("Undo");

                ContextMenuStrip.Items[0].Click += (sender, e) => CutAction();
                ContextMenuStrip.Items[1].Click += (sender, e) => CopyAction();
                ContextMenuStrip.Items[2].Click += (sender, e) => PasteAction();
                // ContextMenuStrip.Items[3].Click += (sender, e) => UndoAction(sender, e);
                ContextMenuStrip.Show(this, new Point(e.X, e.Y));
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Simulate KeyEventArgs for the pressed key
            KeyEventArgs e = new KeyEventArgs(keyData);

            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.C:
                        CopyAction();
                        return true;

                    case Keys.X:
                        CutAction();
                        return true;

                    case Keys.V:
                        PasteAction();
                        return true;

                    case Keys.Z:
                        // UndoAction();
                        return true;

                    case Keys.Y:
                        RedoAction();
                        //MessageBox.Show("Redo");
                        return true;
                }
            }

            // Call the base class implementation for all other keys
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public enum LineNumberStyle
        { None, OffsetColors, Boxed };

        public class LineNumberStrip : Control
        {
            private BufferedGraphics _bufferedGraphics;
            private readonly BufferedGraphicsContext _bufferContext = BufferedGraphicsManager.Current;
            private readonly RichTextBox _richTextBox;
            private Brush _fontBrush;
            private Brush _offsetBrush = new SolidBrush(Color.DarkSlateGray);
            private LineNumberStyle _style;
            private Pen _penBoxedLine = Pens.LightGray;
            private float _fontHeight;
            private const float _FONT_MODIFIER = 0.09f;
            private bool _hideWhenNoLines, _speedBump;
            private const int _DRAWING_OFFSET = 1;
            private int _lastYPos = -1, _dragDistance, _lastLineCount;
            private int _scrollingLineIncrement = 5, _numPadding = 10;

            public LineNumberStrip(RichTextBox plainTextBox)
            {
                _richTextBox = plainTextBox;
                plainTextBox.TextChanged += _richTextBox_TextChanged;
                plainTextBox.FontChanged += _richTextBox_FontChanged;
                plainTextBox.VScroll += _richTextBox_VScroll;

                SetStyle(ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

                Size = new Size(10, 10);
                base.BackColor = Color.FromArgb(32, 32, 32);
                base.Dock = DockStyle.Left;
                base.ForeColor = Color.OrangeRed; //System.Drawing.ColorTranslator.FromHtml("#ddd");
                OffsetColor = ColorTranslator.FromHtml("#222E33");
                Style = LineNumberStyle.None;

                _fontBrush = new SolidBrush(base.ForeColor);

                SetFontHeight();
                UpdateBackBuffer();
                SendToBack();
            }

            protected override void OnMouseDown(MouseEventArgs e)
            {
                base.OnMouseDown(e);

                if (e.Button.Equals(MouseButtons.Left) && _scrollingLineIncrement != 0)
                {
                    _lastYPos = Cursor.Position.Y;
                    Cursor = Cursors.NoMoveVert;
                }
            }

            protected override void OnParentChanged(EventArgs e)
            {
                base.OnParentChanged(e);
                SetControlWidth();
            }

            protected override void OnMouseUp(MouseEventArgs e)
            {
                base.OnMouseUp(e);
                Cursor = Cursors.Default;
            }

            protected override void OnMouseMove(MouseEventArgs e)
            {
                if (e.Button.Equals(MouseButtons.Left) && _scrollingLineIncrement != 0)
                {
                    _dragDistance += Cursor.Position.Y - _lastYPos;

                    if (_dragDistance > _fontHeight)
                    {
                        int selectionStart = _richTextBox.GetFirstCharIndexFromLine(NextLineDown);
                        _richTextBox.Select(selectionStart, 0);
                        _dragDistance = 0;
                    }
                    else if (_dragDistance < _fontHeight * -1)
                    {
                        int selectionStart = _richTextBox.GetFirstCharIndexFromLine(NextLineUp);
                        _richTextBox.Select(selectionStart, 0);
                        _dragDistance = 0;
                    }

                    _lastYPos = Cursor.Position.Y;
                }
            }

            #region Functions

            private void UpdateBackBuffer()
            {
                if (Width > 0)
                {
                    try
                    {
                        _bufferContext.MaximumBuffer = new Size(Width + 1, Height + 1);
                        _bufferedGraphics = _bufferContext.Allocate(CreateGraphics(), ClientRectangle);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message.ToString() + Environment.NewLine);
                    }
                }
            }

            private int GetPositionOfRtbLine(int lineNumber)
            {
                int index = _richTextBox.GetFirstCharIndexFromLine(lineNumber);
                Point pos = _richTextBox.GetPositionFromCharIndex(index);
                return index.Equals(-1) ? -1 : pos.Y;
            }

            private void SetFontHeight()
            {
                Font = new Font(_richTextBox.Font.FontFamily, _richTextBox.Font.Size -
                    _FONT_MODIFIER, _richTextBox.Font.Style);

                _fontHeight = _bufferedGraphics.Graphics.MeasureString("123ABC", Font).Height;
            }

            private void SetControlWidth()
            {
                if (_richTextBox.Lines.Length.Equals(0) && _hideWhenNoLines)
                {
                    Width = 0;
                }
                else
                {
                    Width = WidthOfWidestLineNumber + _numPadding * 4;
                }

                Invalidate(false);
            }

            #endregion Functions

            #region Event Handlers

            private void _richTextBox_FontChanged(object sender, EventArgs e)
            {
                SetFontHeight();
                SetControlWidth();
            }

            private void _richTextBox_TextChanged(object sender, EventArgs e)
            {
                if (_richTextBox.WordWrap || !_lastLineCount.Equals(_richTextBox.Lines.Length))
                {
                    SetControlWidth();
                }

                _lastLineCount = _richTextBox.Lines.Length;
            }

            protected override void OnForeColorChanged(EventArgs e)
            {
                base.OnForeColorChanged(e);
                _fontBrush = new SolidBrush(ForeColor);
            }

            protected override void OnSizeChanged(EventArgs e)
            {
                base.OnSizeChanged(e);
                UpdateBackBuffer();
            }

            protected override void OnPaintBackground(PaintEventArgs pevent)
            {
                _bufferedGraphics.Graphics.Clear(BackColor);

                int firstIndex = _richTextBox.GetCharIndexFromPosition(Point.Empty);
                int firstLine = _richTextBox.GetLineFromCharIndex(firstIndex);
                Point bottomLeft = new(0, ClientRectangle.Height);
                int lastIndex = _richTextBox.GetCharIndexFromPosition(bottomLeft);
                int lastLine = _richTextBox.GetLineFromCharIndex(lastIndex);

                for (int i = firstLine; i <= lastLine + 1; i++)
                {
                    int charYPos = GetPositionOfRtbLine(i);
                    if (charYPos.Equals(-1)) continue;
                    float yPos = GetPositionOfRtbLine(i) + _DRAWING_OFFSET;

                    if (_style.Equals(LineNumberStyle.OffsetColors))
                    {
                        if (i % 2 == 0)
                        {
                            _bufferedGraphics.Graphics.FillRectangle(_offsetBrush, 0, yPos,
                                Width, _fontHeight * _FONT_MODIFIER * 10);
                        }
                    }
                    else if (_style.Equals(LineNumberStyle.Boxed))
                    {
                        PointF endPos = new(Width, yPos + _fontHeight - _DRAWING_OFFSET * 3);
                        PointF startPos = new(0, yPos + _fontHeight - _DRAWING_OFFSET * 3);
                        _bufferedGraphics.Graphics.DrawLine(_penBoxedLine, startPos, endPos);
                    }

                    PointF stringPos = new PointF(_numPadding, yPos);
                    string line = (i + 1).ToString(CultureInfo.InvariantCulture);
                    _bufferedGraphics.Graphics.DrawString(line, Font, _fontBrush, stringPos);
                }

                _bufferedGraphics.Render(pevent.Graphics);
            }

            private void _richTextBox_VScroll(object sender, EventArgs e)
            {
                if (_richTextBox.Lines.Length > 3000 && _speedBump)
                {
                    _speedBump = !_speedBump;
                    return;
                }

                Invalidate(false);
            }

            #endregion Event Handlers

            #region Properties

            private int NextLineDown
            {
                get
                {
                    int yPos = _richTextBox.ClientSize.Height + (int)(_fontHeight * ScrollSpeed + 0.5f);
                    Point topPos = new(0, yPos);
                    int index = _richTextBox.GetCharIndexFromPosition(topPos);
                    return _richTextBox.GetLineFromCharIndex(index);
                }
            }

            private int NextLineUp
            {
                get
                {
                    Point topPos = new(0, (int)(_fontHeight * (ScrollSpeed * -1) + -0.5f));
                    int index = _richTextBox.GetCharIndexFromPosition(topPos);
                    return _richTextBox.GetLineFromCharIndex(index);
                }
            }

            private int WidthOfWidestLineNumber
            {
                get
                {
                    if (_bufferedGraphics.Graphics != null)
                    {
                        string strNumber = _richTextBox.Lines.Length.ToString(CultureInfo.InvariantCulture);
                        SizeF size = _bufferedGraphics.Graphics.MeasureString(strNumber, _richTextBox.Font);
                        return (int)(size.Width + 0.5);
                    }

                    return 1;
                }
            }

            private bool DockToRight
            {
                get { return Dock == DockStyle.Right; }
                set { Dock = value ? DockStyle.Right : DockStyle.Left; }
            }

            [Category("Layout")]
            [Description("Gets or sets the spacing from the left and right of the numbers to the left and right of the control")]
            public int NumberPadding
            {
                get { return _numPadding; }
                set
                {
                    _numPadding = value;

                    if (_richTextBox != null)
                    {
                        SetControlWidth();
                    }
                }
            }

            [Category("Appearance")]
            public LineNumberStyle Style
            {
                get { return _style; }
                set
                {
                    _style = value;
                    Invalidate(false);
                }
            }

            [Category("Appearance")]
            public Color BoxedLineColor
            {
                get { return _penBoxedLine.Color; }
                set
                {
                    _penBoxedLine = new Pen(value);
                    Invalidate(false);
                }
            }

            [Category("Appearance")]
            public Color OffsetColor
            {
                get { return new Pen(_offsetBrush).Color; }
                set
                {
                    _offsetBrush = new SolidBrush(value);
                    Invalidate(false);
                }
            }

            [Category("Behavior")]
            public bool HideWhenNoLines
            {
                get { return _hideWhenNoLines; }
                set { _hideWhenNoLines = value; }
            }

            [Browsable(false)]
            //public override DockStyle Dock
            //  {
            //      get { return base.Dock; }
            //      set { base.Dock = value; }
            //  }

            [Category("Behavior")]
            public int ScrollSpeed
            {
                get { return _scrollingLineIncrement; }
                set { _scrollingLineIncrement = value; }
            }

            #endregion Properties
        }

        public override DockStyle Dock
        {
            get => base.Dock;
            set => base.Dock = DockStyle.Fill;
        }

        #region Event Handlers

        public event EventHandler TextChangedd
        {
            add { richTextBox.TextChanged += value; }
            remove { richTextBox.TextChanged -= value; }
        }

        #endregion Event Handlers
    }
}