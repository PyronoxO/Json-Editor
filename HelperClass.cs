using AutocompleteMenuNS;
using DiffPlex.Model;
using Json_Editor.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ScintillaNET;
using ScintillaNET_FindReplaceDialog;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using VPKSoft.ScintillaLexers.CreateSpecificLexer;
using Windows.Services.Maps.LocalSearch;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Json_Editor
{
    public class HelperClass
    {
        public static string? filep { get; set; } //to pass the file path
        public static DialogResult Sdr { get; set; }
       
        public Scintilla scintilla = new Scintilla();
        //FindReplace MyFindReplace;
        public static AutocompleteMenu autocompleteMenu1 = new AutocompleteMenu();
        public  void Initialize(Scintilla scintilla)
        {
           // System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
           // MyFindReplace = new FindReplace(scintilla);
            autocompleteMenu1.SetAutocompleteItems(new DynamicCollection(scintilla));
            autocompleteMenu1.TargetControlWrapper = new ScintillaWrapper(scintilla);
            autocompleteMenu1.AllowsTabKey = true;
            autocompleteMenu1.AppearInterval = 1000;
            //autocompleteMenu1.Colors = ( AutocompleteMenuNS.Colors )resources.GetObject("autocompleteMenu1.Colors");
            scintilla.Enter += genericScintilla_Enter;
            autocompleteMenu1.Font = new Font("Verdana", 9.75F, FontStyle.Italic, GraphicsUnit.Point, 0);
        }

        public static void UpdateLineNumberMarginWidth(Scintilla scintilla, ref int maxLineNumberCharLength) // Display Line Numbers

        {
            var currentLineNumberCharLength = scintilla.Lines.Count.ToString().Length;
            if (currentLineNumberCharLength == maxLineNumberCharLength)
                return;
            const int padding = 2;
            scintilla.Margins[0].Width = scintilla.TextWidth(Style.LineNumber, new string('5', currentLineNumberCharLength + 1)) + padding;
            maxLineNumberCharLength = currentLineNumberCharLength;
        }

        public static void UpdateLineNumbers(Scintilla scintilla, int startingAtLine)
        {
            // Starting at the specified line index, update each
            // subsequent line margin text with a hex line number.
            for (int i = startingAtLine; i < scintilla.Lines.Count; i++)
            {
                scintilla.Lines[i].MarginStyle = Style.LineNumber;
                scintilla.Lines[i].MarginText = i.ToString();
            }
        }

        public static void InsertMatchedChars(Scintilla scintilla, CharAddedEventArgs e) //AutoComplete Brackets
        {
            var caretPos = scintilla.CurrentPosition;
            var docStart = caretPos == 1;
            var docEnd = caretPos == scintilla.Text.Length;

            var charPrev = docStart ? scintilla.GetCharAt(caretPos) : scintilla.GetCharAt(caretPos - 2);
            var charNext = scintilla.GetCharAt(caretPos);

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
                    scintilla.InsertText(caretPos, ")");
                    break;

                case '{':
                    if (charNextIsCharOrString) return;
                    scintilla.InsertText(caretPos, "}");
                    break;

                case '[':
                    if (charNextIsCharOrString) return;
                    scintilla.InsertText(caretPos, "]");
                    break;

                case '"':
                    // 0x22 = "
                    if (charPrev == 0x22 && charNext == 0x22)
                    {
                        scintilla.DeleteRange(caretPos, 1);
                        scintilla.GotoPosition(caretPos);
                        return;
                    }

                    if (isCharOrString)
                        scintilla.InsertText(caretPos, "\"");
                    break;

                case '\'':
                    // 0x27 = '
                    if (charPrev == 0x27 && charNext == 0x27)
                    {
                        scintilla.DeleteRange(caretPos, 1);
                        scintilla.GotoPosition(caretPos);
                        return;
                    }

                    if (isCharOrString)
                        scintilla.InsertText(caretPos, "'");

                    break;

                case '<':
                    //  0x3C = <
                    if (charNextIsCharOrString) return;
                    {
                        scintilla.InsertText(caretPos, ">");
                        break;
                    }
            }
        }

        #region populate autocomplete menu from content

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

        //public static void shortcuts(Scintilla scintilla, object sender,)
        //{
        //    Keys ScKeys = Keys.Control | Keys.C;
        //    HandleShortcuts(scintilla, ref KMessage, ScKeys);
        //}

        #endregion populate autocomplete menu from content

        #region Shortcuts





        //public void HandleShortcuts(object sender, KeyEventArgs e)
        //{
        //    Scintilla scintilla = ( Scintilla )sender;
        //   // FindReplace MyFindReplace = new ScintillaNET_FindReplaceDialog.FindReplace;

        //    if (e.Control && e.KeyCode == Keys.C)
        //    {
        //        if (!scintilla.SelectedText.Equals(""))
        //            scintilla.Copy();
        //        e.SuppressKeyPress = true;
        //    }
        //    else if (e.Control && e.KeyCode == Keys.V)
        //    {
        //        scintilla.Paste();
        //        e.SuppressKeyPress = true;
        //    }
        //    else if (e.Control && e.KeyCode == Keys.X)
        //    {
        //        if (!scintilla.SelectedText.Equals(""))
        //            scintilla.Cut();
        //        e.SuppressKeyPress = true;
        //    }
        //    else if (e.Control && e.KeyCode == Keys.Z)
        //    {
        //        scintilla.Undo();
        //        e.SuppressKeyPress = true;
        //    }
        //    else if (e.Control && e.KeyCode == Keys.Y)
        //    {
        //        scintilla.Redo();
        //        e.SuppressKeyPress = true;
        //    }
        //    else if (e.Control && e.KeyCode == Keys.Oemplus)
        //    {
        //        scintilla.ZoomIn();
        //        e.SuppressKeyPress = true;
        //    }
        //    else if (e.Control && e.KeyCode == Keys.OemMinus)
        //    {
        //        scintilla.ZoomOut();
        //        e.SuppressKeyPress = true;
        //    }
        //    else if (e.Control && e.KeyCode == Keys.F)
        //    {
        //       findReplace1.ShowFind();
        //        e.SuppressKeyPress = true;
        //    }
        //    else if (e.Shift && e.KeyCode == Keys.F3)
        //    {
        //         findReplace1.Window.FindPrevious();
        //        e.SuppressKeyPress = true;
        //    }
        //    else if (e.KeyCode == Keys.F3)
        //    {
        //        Form1.findReplace1.Window.FindNext();
        //        e.SuppressKeyPress = true;
        //    }
        //    else if (e.Control && e.KeyCode == Keys.H)
        //    {
        //        Form1.findReplace1.ShowReplace();
        //        e.SuppressKeyPress = true;
        //    }
        //    else if (e.Shift && e.KeyCode == Keys.Tab)
        //    {
        //        autocompleteMenu1.Show(scintilla, true);
        //         e.SuppressKeyPress = true;
        //    }

        //    else if (e.Control && e.KeyCode == Keys.G)

        //    {
        //        e.Handled = true;
        //        //return;
        //         GoTo MyGoTo = new GoTo((scintilla ));
        //        MyGoTo.ShowGoToDialog();
        //        e.SuppressKeyPress = true;

        //    }
        //    else if (e.Control && e.KeyCode == Keys.I)
        //    {
        //        Form1.findReplace1.ShowIncrementalSearch();
        //        e.SuppressKeyPress = true;
        //    }



        //    // Add other cases as needed
        //}
        public static void HandleFindReplaceKeyPressed(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //HelperClass helper = new HelperClass();
            //helper.HandleShortcuts(sender, e);

        }
        private void genericScintilla_Enter(object sender, EventArgs e)
        {
            //Form1.findReplace1.Scintilla = ( Scintilla )sender;
        }

        #endregion Shortcuts

        #region scintilla styling

        public static void UiStyling(Scintilla scintilla)
        {
            //scintilla.StyleResetDefault();

            string caretStyleValue = Properties.Settings1.Default.CaretStyle; // Get the value from the settings

            switch (caretStyleValue)
            {
                case "Line":
                    scintilla.CaretStyle = ScintillaNET.CaretStyle.Line;
                    break;

                case "Block":
                    scintilla.CaretStyle = ScintillaNET.CaretStyle.Block;
                    break;

                case "Invisible":
                    scintilla.CaretStyle = ScintillaNET.CaretStyle.Invisible;
                    break;

                default:
                    scintilla.CaretStyle = ScintillaNET.CaretStyle.Line; // Handle the case where the value is none of the expected options
                    break;
            }

            scintilla.Margins[0].BackColor = Properties.Settings1.Default.MarginBackcolor;
            scintilla.Styles[Style.Default].BackColor = Properties.Settings1.Default.uEditorBC;

            scintilla.Styles[Style.Default].Bold = Properties.Settings1.Default.uFontStyleb;
            scintilla.Styles[Style.Default].Italic = Properties.Settings1.Default.uFontStylei;
            scintilla.Styles[Style.Default].Font = Properties.Settings1.Default.uFont;

            scintilla.Styles[Style.Default].Size = Properties.Settings1.Default.uFontSize;
            scintilla.Styles[Style.LineNumber].BackColor = Properties.Settings1.Default.uLineNumBc; //Color.FromArgb(12, 12, 12);
            scintilla.Styles[Style.LineNumber].ForeColor = Properties.Settings1.Default.uLineNumFc; //Color.Red;
        }

        #endregion scintilla styling

        #region Json File Processing

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

        private static void AddNodes(TreeNode parentNode, JToken token, string? nodeName = null, int maxLevels = 2)
        {
            if (maxLevels <= 0)
            {
                return;
            }

            if (token is JObject)
            {
                var nameToken = token["Name"];
                var node = new TreeNode(nameToken != null ? nameToken.ToString() : ( nodeName ?? Path.GetFileName(filep) ));
                parentNode.Nodes.Add(node);

                foreach (var property in ( token as JObject )?.Properties() ?? Enumerable.Empty<JProperty>())
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

        public static void ProcessJsonFile(Scintilla scintilla, TreeView treeView, string filePath)
        {
            try
            {
                if (!string.IsNullOrEmpty(filePath) && Path.GetExtension(filePath).Equals(".json", StringComparison.OrdinalIgnoreCase))
                {
                    UpdateLineNumbers(scintilla, 1);

                    string jsonContent;

                    using (var streamReader = new StreamReader(filePath))
                    {
                        jsonContent = streamReader.ReadToEnd();
                    }

                    if (!string.IsNullOrEmpty(jsonContent))
                    {
                        using (var jsonReader = new JsonTextReader(new StringReader(jsonContent)))
                        {
                            var jsonString = JsonConvert.DeserializeObject<dynamic>(jsonContent);

                            TreeNode rootNode = new TreeNode("Root Node");
                            treeView.Nodes.Add(rootNode);

                            AddNodes(rootNode, jsonString);

                            if (treeView.Nodes.Count > 0)
                            {
                                ExpandAllNodes(treeView.Nodes);
                            }

                            scintilla.Text = jsonString?.ToString().Trim();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion Json File Processing

        #region treeview operation

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

        #endregion treeview operation

        #region Json Lexer Styling

        public static void jsonStyling(Scintilla scintilla)

        {
            //scintilla1.StyleClearAll();

            // Configure thejson style

            scintilla.LexerName = "json";
            //CreateLexerCommon.AddFolding(scintilla1);

            scintilla.SetKeywords(0, "false true");
            scintilla.SetProperty("lexer.json.allow.comments", "1");
            scintilla.SetProperty("lexer.json.escape.sequence", "1");
            scintilla.Styles[Style.Json.Default].ForeColor = Color.Turquoise;
            scintilla.Styles[Style.Json.Default].BackColor = Color.FromArgb(63, 63, 63);
            scintilla.Styles[Style.Json.LineComment].ForeColor = Color.FromArgb(0, 128, 0); // Green
            scintilla.Styles[Style.Json.LineComment].BackColor = Color.FromArgb(63, 63, 63);
            scintilla.Styles[Style.Json.Number].ForeColor = Color.OrangeRed;
            scintilla.Styles[Style.Json.Number].BackColor = Color.FromArgb(63, 63, 63);

            scintilla.Styles[Style.Json.String].ForeColor = Color.LightSkyBlue;
            scintilla.Styles[Style.Json.String].BackColor = Color.FromArgb(63, 63, 63);
            scintilla.Styles[Style.Json.EscapeSequence].ForeColor = Color.LightBlue;
            scintilla.Styles[Style.Json.EscapeSequence].BackColor = Color.FromArgb(63, 63, 63);
            //Brackets colors
            scintilla.Styles[Style.Json.Operator].ForeColor = Color.Yellow;
            scintilla.Styles[Style.Json.Operator].BackColor = Color.FromArgb(63, 63, 63);
            scintilla.Styles[Style.Json.Operator].Bold = true;
            //property name
            scintilla.Styles[Style.Json.PropertyName].BackColor = Color.FromArgb(63, 63, 63);
            scintilla.Styles[Style.Json.PropertyName].ForeColor = Color.AliceBlue;//LightBlue;
            scintilla.Styles[Style.Json.PropertyName].Italic = true;

            scintilla.Styles[Style.Json.Keyword].BackColor = Color.FromArgb(63, 63, 63);
            scintilla.Styles[Style.Json.Keyword].ForeColor = Color.Green;

            scintilla.Styles[Style.Json.Error].BackColor = Color.FromArgb(63, 63, 63);
            scintilla.Styles[Style.Json.Error].ForeColor = Color.Red;

            scintilla.Styles[Style.BraceLight].BackColor = Color.FromArgb(63, 63, 63);
            scintilla.Styles[Style.BraceLight].ForeColor = Color.Orange;
            scintilla.Styles[Style.BraceBad].ForeColor = Color.Red;
            scintilla.IndentationGuides = IndentView.LookForward;
            CreateLexerCommon.AddFolding(scintilla);

            // Set the keywords
        }

        #endregion Json Lexer Styling
    }
}