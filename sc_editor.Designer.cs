namespace Json_Editor
{
    partial class sc_editor
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && ( components != null ))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(sc_editor));
            MyFindReplace = new ScintillaNET_FindReplaceDialog.FindReplace();
            scintilla1 = new ScintillaNET.Scintilla();
            autocompleteMenu1 = new AutocompleteMenuNS.AutocompleteMenu();
            treeView1 = new TreeView();
            SuspendLayout();
            // 
            // MyFindReplace
            // 
            MyFindReplace._lastReplaceHighlight = false;
            MyFindReplace._lastReplaceLastLine = 0;
            MyFindReplace._lastReplaceMark = false;
            MyFindReplace.Scintilla = scintilla1;
            MyFindReplace.KeyPressed +=  MyFindReplace_KeyPressed ;
            // 
            // scintilla1
            // 
            scintilla1.Anchor =      AnchorStyles.Top  |  AnchorStyles.Bottom   |  AnchorStyles.Left   |  AnchorStyles.Right ;
            scintilla1.AutoCMaxHeight = 9;
            scintilla1.BiDirectionality = ScintillaNET.BiDirectionalDisplayType.Disabled;
            scintilla1.CaretLineVisible = true;
            scintilla1.LexerName = null;
            scintilla1.Location = new Point(418, 3);
            scintilla1.Name = "scintilla1";
            scintilla1.ScrollWidth = 49;
            scintilla1.Size = new Size(917, 887);
            scintilla1.TabIndents = true;
            scintilla1.TabIndex = 0;
            scintilla1.UseRightToLeftReadingLayout = false;
            scintilla1.WrapMode = ScintillaNET.WrapMode.None;
            scintilla1.CharAdded +=  Scintilla_CharAdded ;
            scintilla1.Delete +=  Scintilla_Delete ;
            scintilla1.Insert +=  Scintilla_Insert ;
            scintilla1.UpdateUI +=  Scintilla_UpdateUI ;
            scintilla1.TextChanged +=  Scintilla_TextChanged ;
            scintilla1.KeyDown +=  scintilla_KeyDown ;
            scintilla1.KeyPress +=  Scintilla_KeyPress ;
            scintilla1.CaretLineVisibleAlways = true;   
            // 
            // autocompleteMenu1
            // 
            autocompleteMenu1.Colors = ( AutocompleteMenuNS.Colors )resources.GetObject("autocompleteMenu1.Colors");
            autocompleteMenu1.Font = new Font("Microsoft Sans Serif", 9F);
            autocompleteMenu1.ImageList = null;
            autocompleteMenu1.TargetControlWrapper = null;
            // 
            // treeView1
            // 
            treeView1.Anchor =      AnchorStyles.Top  |  AnchorStyles.Bottom   |  AnchorStyles.Left   |  AnchorStyles.Right ;
            treeView1.BackColor = Color.FromArgb(    63,     63,     63);
            treeView1.Font = new Font("Helvetica", 12F);
            treeView1.ForeColor = Color.White;
            treeView1.Location = new Point(-22, 0);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(434, 890);
            treeView1.TabIndex = 1;
            
            treeView1.NodeMouseDoubleClick +=treeView1_NodeMouseDoubleClick;
            // 
            // sc_editor
            // 
            BackColor = Color.FromArgb(    32,     32,     32);
            Controls.Add(scintilla1);
            Controls.Add(treeView1);
            Name = "sc_editor";
            Size = new Size(1335, 893);
            ResumeLayout(false);
        }

        #endregion

        private ScintillaNET_FindReplaceDialog.FindReplace MyFindReplace;
        public ScintillaNET.Scintilla scintilla1;
        private AutocompleteMenuNS.AutocompleteMenu autocompleteMenu1;
        public TreeView treeView1;
    }





}
