using AutocompleteMenuNS;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ScintillaNET;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq.Expressions;
using System.Net.Http.Json;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using VPKSoft.ScintillaLexers.CreateSpecificLexer;
using Windows.Management.Deployment.Preview;
using Windows.UI.ViewManagement;
using WinRT;
using static Json_Editor.HelperClass;
using static ScintillaNET.Style;
using ScintillaNET_FindReplaceDialog;
using System.Security.Principal;
using System.Drawing.Text;
using System.Speech.Synthesis.TtsEngine;
using Enhanced_Tab_Control;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Json_Editor
{
    public partial class Form1 : Form
    {
        public static int maxLineNumberCharLength;

        public Form1()
        {
            InitializeComponent();

            // autocompleteMenu1.SetAutocompleteItems(new DynamicCollection(scintilla1));
            // autocompleteMenu1.TargetControlWrapper = new ScintillaWrapper(scintilla1);

            //HelperClass helper = new HelperClass();
            //helper.Initialize(scintilla1);
        }

        private string? filePath;

        private void Form1_Load(object sender, EventArgs e)

        {
            //UiStyling(scintilla1);
            //jsonStyling(scintilla1);
            //autocompleteMenu1.TargetControlWrapper = new ScintillaWrapper(scintilla1); //attach the syntax auto complete
            //this.Location = Properties.JEapp.Default.StartLocation;
            sc_editor DefTab = new sc_editor();
            enhanced_Tab_Control1.TabPages.Add(DefTab);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Open Text File";
            openFileDialog1.Filter = "Json|*.Json|All Files|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filep = openFileDialog1.FileName;

                if (enhanced_Tab_Control1.TabCount > 0 && string.IsNullOrEmpty(enhanced_Tab_Control1.TabPages[0].Text))

                {
                    try
                    {
                        if (enhanced_Tab_Control1.SelectedTab != null && enhanced_Tab_Control1.SelectedTab is sc_editor selectedTabPage)
                        {
                            selectedTabPage.FilePath = openFileDialog1.FileName;
                            selectedTabPage.Text = Path.GetFileName(openFileDialog1.FileName);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Error Processing File");
                    }
                }
                else if (enhanced_Tab_Control1.TabCount == 0)
                {
                    sc_editor Ntb = new sc_editor();

                    Ntb.FilePath = openFileDialog1.FileName;
                    Ntb.Text = Path.GetFileName(openFileDialog1.FileName);
                    enhanced_Tab_Control1.Controls.Add(Ntb);
                    enhanced_Tab_Control1.SelectedTab = Ntb;
                }
                else
                {
                    //CustomTabPage Ntb = new CustomTabPage(openFileDialog1.FileName);
                    //HelperClass helper = new HelperClass();
                    //helper.Initialize(Ntb.scintilla);
                    //sc_editor Ntb = new sc_editor();
                    sc_editor Ntb = new sc_editor();
                    Ntb.FilePath = openFileDialog1.FileName;
                    Ntb.Text = Path.GetFileName(openFileDialog1.FileName);
                    enhanced_Tab_Control1.Controls.Add(Ntb);
                    enhanced_Tab_Control1.SelectedTab = Ntb;
                }
            }
        }

        private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        //Json Validation

        private static string? validJson;

        private bool IsValidJson(string strInput)
        {
            if (string.IsNullOrWhiteSpace(strInput)) { return false; }
            strInput = strInput.Trim();
            if (( strInput.StartsWith("{") && strInput.EndsWith("}") ) || //For object
               ( strInput.StartsWith("[") && strInput.EndsWith("]") ) ||
               ( strInput.StartsWith("\"") && strInput.EndsWith("\"") )) //For array

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
                    TabPage selectedTabPage = enhanced_Tab_Control1.SelectedTab;
                    selectedTabPage.Controls.ToString();
                    if (selectedTabPage.Controls["scintilla1"] is Scintilla Ntbs)
                    {
                        // MessageBox.Show(errorMessage);
                        if (Ntbs.CurrentLine < lineNumber)
                        {
                            Ntbs.LineScroll(( lineNumber - Ntbs.CurrentLine - 1 ), 0);
                        }
                        else
                        {
                            Ntbs.LineScroll(-( Ntbs.CurrentLine - lineNumber + 1 ), 0);
                        }
                    }
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
            //if (enhanced_Tab_Control1.SelectedTab != null)
            //{
            //    if (( IsValidJson(scintilla1.Text) ) || ( filePath != null ))
            //    {
            //        using (StreamWriter file = File.CreateText(filePath!))
            //        {
            //            file.Write(scintilla1.Text);
            //        }
            //    }

            //}
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void validate_Click(object sender, EventArgs e)
        {
            statusStrip1.Items[1].Text = string.Empty;
            try
            {
                TabPage selectedTabPage = enhanced_Tab_Control1.SelectedTab;
                selectedTabPage.Controls.ToString();
                if (selectedTabPage.Controls["scintilla1"] is Scintilla Ntbs)
                {
                    if (IsValidJson(Ntbs.Text))
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        } //

        private void debugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //HelperClass.filep = scintilla1.Text;

            //var compform = new Form2();
            //compform.Show();
        }

        private void newTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e) // Settings Menu
        {
            var Projs = new Project_Settings();

            Projs.ShowDialog();

            if (Sdr == DialogResult.OK)

            {
                this.Refresh();
                MessageBox.Show("Settings Applied");

                foreach (sc_editor tab in enhanced_Tab_Control1.TabPages)
                {
                    tab.UiStyling();
                    tab.jsonStyling();
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.JEapp.Default.StartLocation = this.Location; // Save start location
            Properties.JEapp.Default.Save();
        }

        private void enhanced_Tab_Control1_Selected(object sender, TabControlEventArgs e)
        {
            //TabPage selectedTabPage = enhanced_Tab_Control1.SelectedTab;
            //string controlNames = "Controls in the selected tab:";
            //foreach (Control control in selectedTabPage.Controls)
            //{
            //    controlNames += "\n" + control.Name;
            //}
            //MessageBox.Show(controlNames);

            if (enhanced_Tab_Control1.SelectedTab != null)
            {
                TabPage selectedTabPage = enhanced_Tab_Control1.SelectedTab;
                selectedTabPage.Controls.ToString();

                if (selectedTabPage.Controls["scintilla1"] is Scintilla Ntbs)
                {
                    string textValue = Ntbs.Text;

                    Clipboard.SetText(textValue);
                }
                else
                {
                    // Handle the case where the control "scintilla1" is not found or is not of type Scintilla
                }
            }
            else
            {
                // Handle the case where no tab is selected
            }
        }
    }//end form1
}// end name space