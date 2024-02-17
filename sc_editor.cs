using AutocompleteMenuNS;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ScintillaNET;
using ScintillaNET_FindReplaceDialog;
using System.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using VPKSoft.ScintillaLexers.CreateSpecificLexer;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Json_Editor
{
    public partial class sc_editor : TabPage
    {
        public sc_editor()
        {
            InitializeComponent();
            //sciontilla propperties
            scintilla1.AllowDrop = true;
            scintilla1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            scintilla1.AutoCMaxHeight = 9;
            scintilla1.BiDirectionality = ScintillaNET.BiDirectionalDisplayType.Disabled;
            scintilla1.BorderStyle = ScintillaNET.BorderStyle.None;
            scintilla1.CaretForeColor = Color.White;
            scintilla1.CaretLineBackColor = Color.Black;
            scintilla1.CaretLineBackColorAlpha = 70;
            scintilla1.CaretLineLayer = ScintillaNET.Layer.OverText;
            scintilla1.CaretLineVisible = true;
            scintilla1.CaretStyle = ScintillaNET.CaretStyle.Block;
            scintilla1.ExtraAscent = 5;
            scintilla1.Font = new Font("Verdana", 11.25F);
            scintilla1.IndentationGuides = ScintillaNET.IndentView.LookForward;
            scintilla1.LexerName = null;
            scintilla1.ScrollWidth = 49;
            scintilla1.TabIndents = true;
            scintilla1.TabIndex = 8;
            scintilla1.UseRightToLeftReadingLayout = false;
            scintilla1.UseTabs = true;

            //scintilla1.Enter += genericScintilla_Enter;

            //Autocomplete Menu

            //System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            autocompleteMenu1.SetAutocompleteItems(new DynamicCollection(scintilla1));
            autocompleteMenu1.TargetControlWrapper = new ScintillaWrapper(scintilla1);
            autocompleteMenu1.AllowsTabKey = true;
            autocompleteMenu1.AppearInterval = 1000;

            autocompleteMenu1.Font = new Font("Verdana", 9.75F, FontStyle.Italic, GraphicsUnit.Point, 0);

            jsonStyling();
            UiStyling();
        }

        private string? _filePath;

        public string FilePath
        {
            get { return _filePath!; }
            set
            {
                _filePath = value;

                // Check if in design mode before processing the file
                if (!DesignMode)
                {
                    // Only process the file if the path is not empty
                    if (!string.IsNullOrEmpty(_filePath))
                    {
                        ProcessJsonFile(_filePath);
                    }
                }
            }
        }

        public class DynamicCollection : IEnumerable<AutocompleteItem>
        {
            private readonly Scintilla _scintilla;

            public DynamicCollection(Scintilla scintilla)
            {
                _scintilla = scintilla;
            }

            public IEnumerator<AutocompleteItem> GetEnumerator()
            {
                return BuildList().GetEnumerator();
            }

            private IEnumerable<AutocompleteItem> BuildList()
            {
                // Find all words in the Scintilla control
                var words = new HashSet<string>();
                var text = _scintilla.Text;

                foreach (Match m in Regex.Matches(text, @"\b\w+\b"))
                {
                    words.Add(m.Value);
                }

                // Return autocomplete items
                foreach (var word in words)
                    yield return new AutocompleteItem(word);
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private int maxLineNumberCharLength;

        public void UpdateLineNumberMarginWidth(ref int maxLineNumberCharLength) // Display Line Numbers

        {
            var currentLineNumberCharLength = scintilla1.Lines.Count.ToString().Length;
            if (currentLineNumberCharLength == maxLineNumberCharLength)
                return;
            const int padding = 2;
            scintilla1.Margins[0].Width = scintilla1.TextWidth(Style.LineNumber, new string('5', currentLineNumberCharLength + 1)) + padding;
            maxLineNumberCharLength = currentLineNumberCharLength;
        }

        public void UpdateLineNumbers(int startingAtLine)
        {
            // Starting at the specified line index, update each
            // subsequent line margin text with a hex line number.
            for (int i = startingAtLine; i < scintilla1.Lines.Count; i++)
            {
                scintilla1.Lines[i].MarginStyle = Style.LineNumber;
                scintilla1.Lines[i].MarginText = i.ToString();
            }
        }

        private void InsertMatchedChars(CharAddedEventArgs e) //AutoComplete Brackets
        {
            var caretPos = scintilla1.CurrentPosition;
            var docStart = caretPos == 1;
            var docEnd = caretPos == scintilla1.Text.Length;

            var charPrev = docStart ? scintilla1.GetCharAt(caretPos) : scintilla1.GetCharAt(caretPos - 2);
            var charNext = scintilla1.GetCharAt(caretPos);

            var isCharPrevBlank = charPrev == ' ' || charPrev == '\t' ||
                                  charPrev == '\n' || charPrev == '\r';

            var isCharNextBlank = charNext == ' ' || charNext == '\t' ||
                                  charNext == '\n' || charNext == '\r' ||
                                  docEnd;

            var isEnclosed = ( charPrev == '(' && charNext == ')' ) ||
                                  ( charPrev == '{' && charNext == '}' ) ||
                                  ( charPrev == '[' && charNext == ']' ) ||
                                  ( charPrev == '<' && charNext == '>' );

            var isSpaceEnclosed = ( charPrev == '(' && isCharNextBlank ) || ( isCharPrevBlank && charNext == ')' ) ||
                                  ( charPrev == '{' && isCharNextBlank ) || ( isCharPrevBlank && charNext == '}' ) ||
                                  ( charPrev == '[' && isCharNextBlank ) || ( isCharPrevBlank && charNext == ']' ) ||
                                  ( charPrev == '<' && isCharNextBlank ) || ( isCharPrevBlank && charNext == '>' );

            var isCharOrString = ( isCharPrevBlank && isCharNextBlank ) || isEnclosed || isSpaceEnclosed;

            var charNextIsCharOrString = charNext == '"' || charNext == '\'';

            switch (e.Char)
            {
                case '(':
                    if (charNextIsCharOrString) return;
                    scintilla1.InsertText(caretPos, ")");
                    break;

                case '{':
                    if (charNextIsCharOrString) return;
                    scintilla1.InsertText(caretPos, "}");
                    break;

                case '[':
                    if (charNextIsCharOrString) return;
                    scintilla1.InsertText(caretPos, "]");
                    break;

                case '"':
                    // 0x22 = "
                    if (charPrev == 0x22 && charNext == 0x22)
                    {
                        scintilla1.DeleteRange(caretPos, 1);
                        scintilla1.GotoPosition(caretPos);
                        return;
                    }

                    if (isCharOrString)
                        scintilla1.InsertText(caretPos, "\"");
                    break;

                case '\'':
                    // 0x27 = '
                    if (charPrev == 0x27 && charNext == 0x27)
                    {
                        scintilla1.DeleteRange(caretPos, 1);
                        scintilla1.GotoPosition(caretPos);
                        return;
                    }

                    if (isCharOrString)
                        scintilla1.InsertText(caretPos, "'");

                    break;

                case '<':
                    //  0x3C = <
                    if (charNextIsCharOrString) return;
                    {
                        scintilla1.InsertText(caretPos, ">");
                        break;
                    }
            }
        }

        private void Scintilla_TextChanged(object sender, EventArgs e) // Display Line Numbers
        {
            UpdateLineNumberMarginWidth(ref maxLineNumberCharLength);
            UpdateLineNumbers(1);
            //UiStyling();
        }

        private int lastCaretPos = 0;

        //private static bool IsBrace(int c)
        //{
        //    switch (c)
        //    {
        //        case '(':
        //        case ')':
        //        case '[':
        //        case ']':
        //        case '{':
        //        case '}':
        //        case '<':
        //        case '>':
        //            return true;
        //    }

        //    return false;
        //}

        private void Scintilla_UpdateUI(object sender, UpdateUIEventArgs e)
        {
            // Has the caret changed position?
            var caretPos = scintilla1.CurrentPosition;
            int lineNumber = scintilla1.CurrentLine + 1;
            Form1.statusStrip1.Items[0].ForeColor = Color.LightBlue;
            Form1.statusStrip1.Items[0].Text = "Current Position: " + scintilla1.CurrentPosition.ToString() + " " + "Current Line: " + lineNumber;

            //if (lastCaretPos != caretPos)
            //{
            //    lastCaretPos = caretPos;
            //    var bracePos1 = -1;
            //    var bracePos2 = -1;

            //    Is there a brace to the left or right ?
            //    if (caretPos > 0 && IsBrace(scintilla1.GetCharAt(caretPos - 1)))
            //        bracePos1 = ( caretPos - 1 );
            //    else if (IsBrace(scintilla1.GetCharAt(caretPos)))
            //        bracePos1 = caretPos;

            //    if (bracePos1 >= 0)
            //    {
            //        // Find the matching brace
            //        bracePos2 = scintilla1.BraceMatch(bracePos1);
            //        if (bracePos2 == Scintilla.InvalidPosition)
            //        {
            //            scintilla1.BraceBadLight(bracePos1);
            //            scintilla1.HighlightGuide = 0;
            //        }
            //        else
            //        {
            //            scintilla1.BraceHighlight(bracePos1, bracePos2);
            //            scintilla1.HighlightGuide = scintilla1.GetColumn(bracePos1);
            //        }
            //    }
            //    else
            //    {
            //        // Turn off brace matching
            //        scintilla1.BraceHighlight(Scintilla.InvalidPosition, Scintilla.InvalidPosition);
            //        scintilla1.HighlightGuide = 0;
            //    }
            //}
        }

        private void Scintilla_CharAdded(object sender, CharAddedEventArgs e)
        {
            InsertMatchedChars(e);
        }

        private void scintilla_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                if (!scintilla1.SelectedText.Equals(""))
                    scintilla1.Copy();
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                scintilla1.Paste();
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.X)
            {
                if (!scintilla1.SelectedText.Equals(""))
                    scintilla1.Cut();
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.Z)
            {
                scintilla1.Undo();
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.Y)
            {
                scintilla1.Redo();
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.Oemplus)
            {
                scintilla1.ZoomIn();
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.OemMinus)
            {
                scintilla1.ZoomOut();
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                MyFindReplace.ShowFind();
                e.SuppressKeyPress = true;
            }
            else if (e.Shift && e.KeyCode == Keys.F3)
            {
                MyFindReplace.Window.FindPrevious();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.F3)
            {
                MyFindReplace.Window.FindNext();
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.H)
            {
                MyFindReplace.ShowReplace();
                e.SuppressKeyPress = true;
            }
            else if (e.Shift && e.KeyCode == Keys.Tab)
            {
                autocompleteMenu1.Show(scintilla1, true);
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.G)

            {
                e.Handled = true;
                //return;
                GoTo MyGoTo = new GoTo(( scintilla1 ));
                MyGoTo.ShowGoToDialog();
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.I)
            {
                MyFindReplace.ShowIncrementalSearch();
                e.SuppressKeyPress = true;
            }
        }

        private void Scintilla_Insert(object sender, ModificationEventArgs e)
        {
            // Only update line numbers if the number of lines changed
            if (e.LinesAdded != 0)
                UpdateLineNumbers(scintilla1.LineFromPosition(e.Position));
        }

        private void Scintilla_Delete(object sender, ModificationEventArgs e)
        {
            // Only update line numbers if the number of lines changed
            if (e.LinesAdded != 0)
                UpdateLineNumbers(scintilla1.LineFromPosition(e.Position));
        }

        private void Scintilla_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < 32)
            {
                // Prevent control characters from getting inserted into the text buffer
                e.Handled = true;
                return;
            }
        }

        private void MyFindReplace_KeyPressed(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            scintilla_KeyDown(sender, e);
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode selectedNode = e.Node!;
            ExtractCodeFromScintilla(scintilla1, selectedNode);
        }

        private void AddNodes(TreeNode parentNode, JToken token, string? nodeName = null, int maxLevels = 2)
        {
            if (maxLevels <= 0)
            {
                return;
            }

            if (token is JObject)
            {
                var nameToken = token["Name"];
                var node = new TreeNode(nameToken != null ? nameToken.ToString() : ( nodeName ?? Path.GetFileName(FilePath) ));
                parentNode.Nodes.Add(node);

                foreach (var property in ( token as JObject ).Properties())
                {
                    if (property.Name != "Name" && property.Name != "Rows") // Stop adding child nodes for "Name" and "Rows"
                    {
                        AddNodes(node, property.Value, property.Name, maxLevels - 1); // Pass the property name as the node name and decrement the level
                    }
                }

                var rowsProperty = token["Rows"];
                if (rowsProperty != null && rowsProperty is JArray) // Handle the "Rows" array separately
                {
                    foreach (var row in rowsProperty)
                    {
                        var name = row["Name"]?.ToString();
                        if (!string.IsNullOrEmpty(name))
                        {
                            var rowNode = new TreeNode(name);
                            node.Nodes.Add(rowNode); // Add a node for each "Name" in the "Rows" array
                        }
                    }
                }
            }
            else if (token is JArray)
            {
                for (int i = 0; i < token.Count(); i++)
                {
                    AddNodes(parentNode, token[i]!, nodeName, maxLevels - 1); // Pass the current node name and decrement the level
                }
            }
            else
            {
                var node = new TreeNode(nodeName + ": " + token);
                parentNode.Nodes.Add(node);
            }
        }

        private void ProcessJsonFile(string _filepath)
        {
            try
            {
                if (!string.IsNullOrEmpty(_filepath) && Path.GetExtension(_filepath).Equals(".json", StringComparison.OrdinalIgnoreCase))
                {
                    UpdateLineNumbers(1);

                    string jsonContent;

                    using (var streamReader = new StreamReader(_filepath))
                    {
                        jsonContent = streamReader.ReadToEnd();
                    }

                    if (!string.IsNullOrEmpty(jsonContent))
                    {
                        using (var jsonReader = new JsonTextReader(new StringReader(jsonContent)))
                        {
                            var jsonString = JsonConvert.DeserializeObject<dynamic>(jsonContent);

                            TreeNode rootNode = new TreeNode("Root Node");
                            treeView1.Nodes.Add(rootNode);

                            AddNodes(rootNode, jsonString);

                            if (treeView1.Nodes.Count > 0)
                            {
                                ExpandAllNodes(treeView1.Nodes);
                            }

                            scintilla1.Text = jsonString?.ToString().Trim();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void DisplayAndCopyCode(string selectedNodeText, string selectedCode)
        {
            // Copy the extracted code to the clipboard
            Clipboard.SetText(selectedCode);

            // Display the extracted code
            MessageBox.Show($"Selected code for {selectedNodeText}:\n{selectedCode}", "Extracted Code");
        }

        public static void ExtractCodeFromScintilla(Scintilla scintilla, TreeNode selectedNode)
        {
            string selectedNodeText = selectedNode.Text;
            string scintillaText = scintilla.Text;

            // Find the selected node's text in the Scintilla control
            int startIndex = scintillaText.IndexOf(selectedNodeText);

            if (selectedNodeText == "Defaults")
            {
                startIndex = scintillaText.IndexOf("Defaults");
                if (startIndex != -1) // Check if "Defaults" is found
                {
                    MessageBox.Show("Defaults Property is not possible to copy, manual selection is required");
                }
            }
            else if (startIndex >= 0)
            {
                // Find the opening brace '{' before the selected node's text
                int braceIndex = scintillaText.LastIndexOf('{', startIndex);
                if (braceIndex >= 0)
                {
                    int braceCount = 1;
                    int endIndex = braceIndex + 1;

                    // Find the matching closing brace '}' for the opening brace

                    while (braceCount > 0 && endIndex < scintillaText.Length)
                    {
                        if (scintillaText[endIndex] == '{')
                        {
                            braceCount++;
                        }
                        else if (scintillaText[endIndex] == '}')
                        {
                            braceCount--;
                        }
                        endIndex++;
                    }

                    if (braceCount == 0)
                    {
                        // Find the next comma ',' after the closing brace
                        int commaIndex = scintillaText.IndexOf(',', endIndex);
                        if (commaIndex >= 0)
                        {
                            // Extract the content including the curly braces and the comma
                            string selectedCode = scintillaText.Substring(braceIndex, commaIndex - braceIndex + 1);
                            DisplayAndCopyCode(selectedNodeText, selectedCode);
                        }
                        else
                        {
                            // Extract the content including the curly braces until the end of the document
                            string selectedCode = scintillaText.Substring(braceIndex);
                            DisplayAndCopyCode(selectedNodeText, selectedCode);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No matching closing brace found for the selected node.", "Error");
                    }
                }
                else
                {
                    MessageBox.Show("No opening brace found before the selected node.", "Error");
                }
            }
            else
            {
                MessageBox.Show("Selected node not found in the Scintilla control.", "Error");
            }
        }

        private void jsonStyling()

        {
            //scintilla1.StyleClearAll();

            // Configure thejson style

            scintilla1.LexerName = "json";

            scintilla1.SetKeywords(0, "false true");
            scintilla1.SetProperty("lexer.json.allow.comments", "1");
            scintilla1.SetProperty("lexer.json.escape.sequence", "1");
            scintilla1.Styles[Style.Json.Default].ForeColor = Color.Turquoise;
            scintilla1.Styles[Style.Json.Default].BackColor = Color.FromArgb(63, 63, 63);
            scintilla1.Styles[Style.Json.LineComment].ForeColor = Color.FromArgb(0, 128, 0); // Green
            scintilla1.Styles[Style.Json.LineComment].BackColor = Color.FromArgb(63, 63, 63);
            scintilla1.Styles[Style.Json.Number].ForeColor = Color.OrangeRed;
            scintilla1.Styles[Style.Json.Number].BackColor = Color.FromArgb(63, 63, 63);

            scintilla1.Styles[Style.Json.String].ForeColor = Color.LightSkyBlue;
            scintilla1.Styles[Style.Json.String].BackColor = Color.FromArgb(63, 63, 63);
            scintilla1.Styles[Style.Json.EscapeSequence].ForeColor = Color.LightBlue;
            scintilla1.Styles[Style.Json.EscapeSequence].BackColor = Color.FromArgb(63, 63, 63);
            //Brackets colors
            scintilla1.Styles[Style.Json.Operator].ForeColor = Color.Yellow;
            scintilla1.Styles[Style.Json.Operator].BackColor = Color.FromArgb(63, 63, 63);
            scintilla1.Styles[Style.Json.Operator].Bold = true;
            //property name
            scintilla1.Styles[Style.Json.PropertyName].BackColor = Color.FromArgb(63, 63, 63);
            scintilla1.Styles[Style.Json.PropertyName].ForeColor = Color.AliceBlue;//LightBlue;
            scintilla1.Styles[Style.Json.PropertyName].Italic = true;

            scintilla1.Styles[Style.Json.Keyword].BackColor = Color.FromArgb(63, 63, 63);
            scintilla1.Styles[Style.Json.Keyword].ForeColor = Color.Green;

            scintilla1.Styles[Style.Json.Error].BackColor = Color.FromArgb(63, 63, 63);
            scintilla1.Styles[Style.Json.Error].ForeColor = Color.Red;

            scintilla1.Styles[Style.BraceLight].BackColor = Color.FromArgb(63, 63, 63);
            scintilla1.Styles[Style.BraceLight].ForeColor = Color.Orange;
            scintilla1.Styles[Style.BraceBad].ForeColor = Color.Red;
            scintilla1.IndentationGuides = IndentView.LookForward;
            CreateLexerCommon.AddFolding(scintilla1);

            // Set the keywords
        }

        private bool applyUiStyling = true;

        public bool ApplyUiStyling
        {
            get { return applyUiStyling; }
            set
            {
                if (applyUiStyling != value)
                {
                    applyUiStyling = value;

                    // If ApplyUiStyling is set to true, execute UiStyling
                    if (applyUiStyling)
                    {
                        UiStyling();
                    }
                }
            }
        }

        public void UiStyling()
        {
            scintilla1.StyleResetDefault();
            scintilla1.CaretStyle = ScintillaNET.CaretStyle.Line;

            string caretStyleValue = Properties.Settings1.Default.CaretStyle; // Get the value from the settings

            switch (caretStyleValue)
            {
                case "Line":
                    scintilla1.CaretStyle = ScintillaNET.CaretStyle.Line;
                    break;

                case "Block":
                    scintilla1.CaretStyle = ScintillaNET.CaretStyle.Block;
                    break;

                case "Invisible":
                    scintilla1.CaretStyle = ScintillaNET.CaretStyle.Invisible;
                    break;

                default:
                    scintilla1.CaretStyle = ScintillaNET.CaretStyle.Line; // Handle the case where the value is none of the expected options
                    break;
            }

            scintilla1.Margins[0].BackColor = Properties.Settings1.Default.MarginBackcolor;
            scintilla1.Styles[Style.Default].BackColor = Properties.Settings1.Default.uEditorBC;

            //scintilla1.Styles[Style.Default].Bold = Properties.Settings1.Default.uFontStyleb;
            //scintilla1.Styles[Style.Default].Italic = Properties.Settings1.Default.uFontStylei;
            //scintilla1.Styles[Style.Default].Font = Properties.Settings1.Default.uFont;

            scintilla1.Font = Properties.Settings1.Default.DeFont;
            


            scintilla1.Styles[Style.Default].Size = Properties.Settings1.Default.uFontSize;
            scintilla1.Styles[Style.LineNumber].BackColor = Properties.Settings1.Default.uLineNumBc; //Color.FromArgb(12, 12, 12);
            scintilla1.Styles[Style.LineNumber].ForeColor = Properties.Settings1.Default.uLineNumFc; //Color.Red;
        }

        private static void ExpandAllNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                node.Expand();
                if (node.Nodes.Count > 0)
                {
                    ExpandAllNodes(node.Nodes);
                }
            }
        }
    }
}