using AutocompleteMenuNS;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ScintillaNET;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using VPKSoft.ScintillaLexers.CreateSpecificLexer;
using WinRT;
using static Json_Editor.HelperClass;
using static ScintillaNET.Style;

namespace Json_Editor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            autocompleteMenu1.SetAutocompleteItems(new DynamicCollection(scintilla1));
            autocompleteMenu1.TargetControlWrapper = new ScintillaWrapper(scintilla1);
        }

        private string? filePath;

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Open Text File";
            openFileDialog1.Filter = "Json|*.Json|All Files|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;
                treeView1.Nodes.Clear();
                scintilla1.Text.Clone();
                try
                {
                    if (!string.IsNullOrEmpty(openFileDialog1.FileName) && Path.GetExtension(openFileDialog1.FileName).Equals(".json", StringComparison.OrdinalIgnoreCase))

                    {
                        jsonStyling();
                        UpdateLineNumbers(1);

                        string jsonContent;

                        using (var streamReader = new StreamReader(openFileDialog1.FileName))
                        {
                            jsonContent = streamReader.ReadToEnd();
                        }

                        if (!string.IsNullOrEmpty(jsonContent))
                        {
                            using (var jsonReader = new JsonTextReader(new StringReader(jsonContent)))
                            {
                                var jsonString = JsonConvert.DeserializeObject<dynamic>(jsonContent);

                                //var serializer = new JsonSerializer();
                                //var jsonObject = serializer.Deserialize<dynamic>(jsonReader);

                                // Clear existing nodes in treeView1

                                TreeNode rootNode = new TreeNode("Root Node");
                                treeView1.Nodes.Add(rootNode);

                                AddNodes(rootNode, jsonString);

                                if (treeView1.Nodes.Count > 0)
                                {
                                    ExpandAllNodes(treeView1.Nodes);
                                }

                                scintilla1.Text = jsonString.ToString().Trim();
                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(openFileDialog1.FileName) && Path.GetExtension(openFileDialog1.FileName).Equals(".txt", StringComparison.OrdinalIgnoreCase))
                        {
                            string jsonContent = string.Empty;

                            using (var streamReader = new StreamReader(openFileDialog1.FileName))
                            {
                                jsonContent = streamReader.ReadToEnd();
                                if (!string.IsNullOrEmpty(jsonContent))
                                {
                                    jsonStyling();
                                    UpdateLineNumbers(1);

                                    scintilla1.Text = jsonContent.ToString().Trim();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                // Load the selected file into the Scintilla control
            }
        }


        void ExpandAllNodes(TreeNodeCollection nodes)
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

        //private void AddNodes(TreeNode parentNode, JToken token, string nodeName = null)
        //{
        //    if (token is JObject)
        //    {
        //        var nameProperty = token.SelectToken("Name");
        //        var currentName = nameProperty != null ? nameProperty.ToString() : nodeName;
        //        var node = new TreeNode(currentName ?? "Unnamed");
        //        parentNode.Nodes.Add(node);

        //        foreach (var property in token.Children<JProperty>())
        //        {
        //            AddNodes(node, property.Value, property.Name);
        //        }
        //    }
        //    else if (token is JArray)
        //    {
        //        for (int i = 0; i < token.Count(); i++)
        //        {
        //            AddNodes(parentNode, token[i], $"Item {i + 1}");
        //        }
        //    }
        //    else
        //    {
        //        var node = new TreeNode(token.ToString());
        //        parentNode.Nodes.Add(node);
        //    }
        //}

        //private void AddNodes(TreeNode parentNode, JToken token, string nodeName = null)
        //{
        //    if (token is JObject)
        //    {
        //        var nameToken = token["Name"];
        //        var node = new TreeNode(nameToken != null ? nameToken.ToString() : (nodeName ?? "Unnamed"));
        //        parentNode.Nodes.Add(node);

        //        foreach (var property in token.Children<JProperty>())
        //        {
        //            if (property.Name != "Name") // Skip adding child nodes for the "Name" property
        //            {
        //                AddNodes(node, property.Value, property.Name); // Pass the property name as the node name
        //            }
        //        }
        //    }
        //    else if (token is JArray)
        //    {
        //        for (int i = 0; i < token.Count(); i++)
        //        {
        //            AddNodes(parentNode, token[i], nodeName); // Pass the current node name
        //        }
        //    }
        //    else
        //    {
        //        var node = new TreeNode(nodeName + ": " + token);
        //        parentNode.Nodes.Add(node);
        //    }
        //}



        private void AddNodes(TreeNode parentNode, JToken token, string nodeName = null, int maxLevels = 2)
        {
            if (maxLevels <= 0)
            {
                return;
            }

            if (token is JObject)
            {
                var nameToken = token["Name"];
                var node = new TreeNode(nameToken != null ? nameToken.ToString() : (nodeName ?? "Unnamed"));
                parentNode.Nodes.Add(node);

                foreach (var property in (token as JObject).Properties())
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
                    AddNodes(parentNode, token[i], nodeName, maxLevels - 1); // Pass the current node name and decrement the level
                }
            }
            else
            {
                var node = new TreeNode(nodeName + ": " + token);
                parentNode.Nodes.Add(node);
            }
        }











        private int maxLineNumberCharLength;

        private void Scintilla1_TextChanged_1(object sender, EventArgs e)
        {
            // Did the number of characters in the line number display change?

            var maxLineNumberCharLength = scintilla1.Lines.Count.ToString().Length;
            if (maxLineNumberCharLength == this.maxLineNumberCharLength)
                return;

            // Calculate the width required to display the last line number
            // and include some padding for good measure.
            const int padding = 2;
            scintilla1.Margins[0].Width = scintilla1.TextWidth(Style.LineNumber, new string('5', maxLineNumberCharLength + 1)) + padding;
            this.maxLineNumberCharLength = maxLineNumberCharLength;
        }

        private void UpdateLineNumbers(int startingAtLine)
        {
            // Starting at the specified line index, update each
            // subsequent line margin text with a hex line number.
            for (int i = startingAtLine; i < scintilla1.Lines.Count; i++)
            {
                scintilla1.Lines[i].MarginStyle = Style.LineNumber;
                scintilla1.Lines[i].MarginText = i.ToString();
            }
        }

        private void Scintilla1_Insert(object sender, ModificationEventArgs e)
        {
            // Only update line numbers if the number of lines changed
            if (e.LinesAdded != 0)
                UpdateLineNumbers(scintilla1.LineFromPosition(e.Position));
        }

        private void Scintilla1_Delete(object sender, ModificationEventArgs e)
        {
            // Only update line numbers if the number of lines changed
            if (e.LinesAdded != 0)
                UpdateLineNumbers(scintilla1.LineFromPosition(e.Position));
        }

        //Json Styling//
        private void jsonStyling()

        {
            //scintilla1.StyleClearAll();

            // Configure thejson style

            scintilla1.LexerName = "json";
            //CreateLexerCommon.AddFolding(scintilla1);

            scintilla1.SetKeywords(0, "false true");

            scintilla1.SetProperty("lexer.json.allow.comments", "1");
            scintilla1.SetProperty("lexer.json.escape.sequence", "1");

            scintilla1.Styles[Style.Json.Default].ForeColor = Color.LightBlue;
            scintilla1.Styles[Style.Json.Default].BackColor = Color.FromArgb(63, 63, 63);

            //scintilla1.Styles[Style.Json.BlockComment].ForeColor = Color.FromArgb(0, 128, 0); // Green
            //scintilla1.Styles[Style.Json.LineComment].ForeColor = Color.FromArgb(0, 128, 0); // Green
            //scintilla1.Styles[Style.Json.PropertyName].ForeColor = Color.DarkBlue; // Gray
            scintilla1.Styles[Style.Json.Number].ForeColor = Color.DarkGoldenrod;
            scintilla1.Styles[Style.Json.Number].BackColor = Color.FromArgb(63, 63, 63);

            scintilla1.Styles[Style.Json.String].ForeColor = Color.LightSkyBlue;
            scintilla1.Styles[Style.Json.String].BackColor = Color.FromArgb(63, 63, 63);
            //scintilla1.Styles[Style.Json.Operator].ForeColor = Color.LightBlue;
            //scintilla1.Styles[Style.Json.StringEol].BackColor = Color.Pink;

            //Brackets colors
            scintilla1.Styles[Style.Json.Operator].ForeColor = Color.Yellow;
            scintilla1.Styles[Style.Json.Operator].BackColor = Color.FromArgb(63, 63, 63);
            scintilla1.Styles[Style.Json.Operator].Bold = true;

            //scintilla1.Styles[Style.Json.EscapeSequence].ForeColor = Color.Maroon;

            //property name
            scintilla1.Styles[Style.Json.PropertyName].BackColor = Color.FromArgb(63, 63, 63);
            scintilla1.Styles[Style.Json.PropertyName].ForeColor = Color.AliceBlue;//LightBlue;
            scintilla1.Styles[Style.Json.PropertyName].Italic = true;

            scintilla1.Styles[Style.Json.Keyword].BackColor = Color.FromArgb(63, 63, 63);
            scintilla1.Styles[Style.Json.Keyword].ForeColor = Color.LightGreen;
            scintilla1.Styles[Style.Json.Keyword].BackColor = Color.FromArgb(63, 63, 63);
            scintilla1.Styles[Style.Json.Keyword].ForeColor = Color.Green;

            scintilla1.Styles[Style.Json.Error].BackColor = Color.FromArgb(63, 63, 63);
            scintilla1.Styles[Style.Json.Error].ForeColor = Color.Red;

            scintilla1.Styles[Style.BraceLight].BackColor = Color.FromArgb(63, 63, 63);
            scintilla1.Styles[Style.BraceLight].ForeColor = Color.Orange;
            scintilla1.Styles[Style.BraceBad].ForeColor = Color.Red;
            scintilla1.IndentationGuides = IndentView.Real;
            CreateLexerCommon.AddFolding(scintilla1);

            // Set the keywords
        }

        private void UiStyling()
        {
            //scintilla1.StyleResetDefault();
            scintilla1.CaretStyle = CaretStyle.Line;
            scintilla1.Margins[0].BackColor = Color.FromArgb(12, 12, 12);
            scintilla1.Styles[Style.Default].BackColor = Color.FromArgb(63, 63, 63);
            scintilla1.Styles[Style.Default].Bold = true;
            scintilla1.Styles[Style.Default].Font = "Helvetica";
            scintilla1.Styles[Style.Default].Size = 12;
            scintilla1.Styles[Style.LineNumber].BackColor = Color.FromArgb(12, 12, 12);
            scintilla1.Styles[Style.LineNumber].ForeColor = Color.Red;
        }

        private static bool IsBrace(int c)
        {
            switch (c)
            {
                case '(':
                case ')':
                case '[':
                case ']':
                case '{':
                case '}':
                case '<':
                case '>':
                    return true;
            }

            return false;
        }

        //Syntax  Highlighting//
        private int lastCaretPos = 0;

        private void Scintilla1_UpdateUI(object sender, UpdateUIEventArgs e)
        {
            // Has the caret changed position?
            var caretPos = scintilla1.CurrentPosition;
            int lineNumber = scintilla1.CurrentLine + 1;
            statusStrip1.Items[0].ForeColor = Color.LightBlue;
            statusStrip1.Items[0].Text = "Current Position: " + scintilla1.CurrentPosition.ToString() + " " + "Current Line: " + lineNumber;

            if (lastCaretPos != caretPos)
            {
                lastCaretPos = caretPos;
                var bracePos1 = -1;
                var bracePos2 = -1;

                // Is there a brace to the left or right?
                if (caretPos > 0 && IsBrace(scintilla1.GetCharAt(caretPos - 1)))
                    bracePos1 = (caretPos - 1);
                else if (IsBrace(scintilla1.GetCharAt(caretPos)))
                    bracePos1 = caretPos;

                if (bracePos1 >= 0)
                {
                    // Find the matching brace
                    bracePos2 = scintilla1.BraceMatch(bracePos1);
                    if (bracePos2 == Scintilla.InvalidPosition)
                    {
                        scintilla1.BraceBadLight(bracePos1);
                        scintilla1.HighlightGuide = 0;
                    }
                    else
                    {
                        scintilla1.BraceHighlight(bracePos1, bracePos2);
                        scintilla1.HighlightGuide = scintilla1.GetColumn(bracePos1);
                    }
                }
                else
                {
                    // Turn off brace matching
                    scintilla1.BraceHighlight(Scintilla.InvalidPosition, Scintilla.InvalidPosition);
                    scintilla1.HighlightGuide = 0;
                }
            }
        }

        //Autocomplete// Brackets
        private void Scintilla1_CharAdded(object sender, CharAddedEventArgs e)
        {
            //// Find the word start
            //var currentPos = scintilla1.CurrentPosition;
            //var wordStartPos = scintilla1.WordStartPosition(currentPos, true);

            ////// Display the autocompletion list
            //var lenEntered = currentPos - wordStartPos;
            //if (lenEntered >2)
            //{
            //    if (!scintilla1.AutoCActive)

            //        scintilla1.AutoCShow(lenEntered, "Name RowName Meta DataTableName Amount");
            //        scintilla1.AutoCShow(lenEntered, "Item");
            //}

            //switch (e.Char)
            //{
            //    case '(':
            //        scintilla1.InsertText(scintilla1.CurrentPosition, ")");
            //        break;

            //    case '{':
            //        scintilla1.InsertText(scintilla1.CurrentPosition, "}");
            //        break;

            //    case '[':
            //        scintilla1.InsertText(scintilla1.CurrentPosition, "]");
            //        break;

            //    case '"':
            //        scintilla1.InsertText(scintilla1.CurrentPosition, "\"");
            //        break;

            //    case '\'':
            //        scintilla1.InsertText(scintilla1.CurrentPosition, "'");
            //        break;
            //}

            InsertMatchedChars(e);
        }

        private void InsertMatchedChars(CharAddedEventArgs e)
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

            var isEnclosed = (charPrev == '(' && charNext == ')') ||
                                  (charPrev == '{' && charNext == '}') ||
                                  (charPrev == '[' && charNext == ']');

            var isSpaceEnclosed = (charPrev == '(' && isCharNextBlank) || (isCharPrevBlank && charNext == ')') ||
                                  (charPrev == '{' && isCharNextBlank) || (isCharPrevBlank && charNext == '}') ||
                                  (charPrev == '[' && isCharNextBlank) || (isCharPrevBlank && charNext == ']');

            var isCharOrString = (isCharPrevBlank && isCharNextBlank) || isEnclosed || isSpaceEnclosed;

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
                    //  0x3c = '
                    if (charPrev == 0x3c && charNext == 0x3e)
                    {
                        scintilla1.DeleteRange(caretPos, 1);
                        scintilla1.GotoPosition(caretPos);
                        return;
                    }
                    if (isCharOrString)
                        scintilla1.InsertText(caretPos, ">");

                    break;
            }
        }

        private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            autocompleteMenu1.Show(scintilla1, true);
        }

        private void scintilla1_KeyDown(object sender, KeyEventArgs e)
        {
            //forcibly shows menu
            if (e.Control && e.KeyCode == Keys.Tab)

                autocompleteMenu1.Show(scintilla1, true);
        }

        #region Auto complete menu

        internal class DynamicCollection : IEnumerable<AutocompleteItem>
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

        #endregion Auto complete menu

        private void Form1_Load(object sender, EventArgs e)

        {
            UiStyling();
            jsonStyling();

            autocompleteMenu1.TargetControlWrapper = new ScintillaWrapper(scintilla1); //attach the syntax auto complete
        }

        //Json Validation

        private static string? validJson;

        private static bool IsValidJson(string strInput)
        {
            if (string.IsNullOrWhiteSpace(strInput)) { return false; }
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
               (strInput.StartsWith("[") && strInput.EndsWith("]")) ||
               (strInput.StartsWith("\"") && strInput.EndsWith("\""))) //For array

            {
                try
                {
                    var obj = JContainer.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    int lineNumber = jex.LineNumber - 1;
                    string errorMessage = "There is an error in line " + lineNumber;
                    MessageBox.Show(errorMessage);
                    validJson = errorMessage;
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    MessageBox.Show(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((IsValidJson(scintilla1.Text)) || (filePath != null))
            {
                using (StreamWriter file = File.CreateText(filePath))
                {
                    file.Write(scintilla1.Text);
                }
            }
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValidJson(scintilla1.Text))

                {
                    MessageBox.Show("file saved");
                    var saveDialogResult = saveFileDialog1.ShowDialog();
                    if (saveDialogResult == DialogResult.OK)
                    {
                        using (StreamWriter file = File.CreateText(saveFileDialog1.FileName))
                        {
                            file.Write(scintilla1.Text);

                            //if i want it serialized

                            // JsonSerializer serializer = new JsonSerializer();
                            // serializer.Serialize(file, scintilla1.Text);
                        }
                    }
                }
            }
            catch (Exception ex)

            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void validate_Click(object sender, EventArgs e)
        {
            statusStrip1.Items[1].Text = string.Empty;
            try
            {
                if (IsValidJson(scintilla1.Text))
                {
                    statusStrip1.Items[0].ForeColor = Color.Green;
                    statusStrip1.Items[0].Text = "Valid Json";
                }
                else
                {
                    statusStrip1.Items[1].ForeColor = Color.Red;
                    statusStrip1.Items[1].Text = validJson;
                    statusStrip1.Items[0].Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void debugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelperClass.filep = scintilla1.Text;

            var compform = new Form2();
            compform.Show();
        }



        

        // Use the AfterSelect event to handle node selection
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selectedNode = e.Node;
            ExtractCodeFromScintilla(selectedNode);
        }

        // Extract the corresponding code from the Scintilla control based on the selected node
        private void ExtractCodeFromScintilla(TreeNode selectedNode)
        {
            string selectedNodeText = selectedNode.Text;
            string scintillaText = scintilla1.Text;

            // Find the selected node's text in the Scintilla control
            int startIndex = scintillaText.IndexOf(selectedNodeText);
            if (startIndex >= 0)
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

        // Display the extracted code and copy it to the clipboard
        private void DisplayAndCopyCode(string selectedNodeText, string selectedCode)
        {
            // Copy the extracted code to the clipboard
            Clipboard.SetText(selectedCode);

            // Display the extracted code
            MessageBox.Show($"Selected code for {selectedNodeText}:\n{selectedCode}", "Extracted Code");
        }
    }//end form1
}// end name space