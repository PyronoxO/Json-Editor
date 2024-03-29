﻿
namespace Json_Editor
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            newToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            printToolStripMenuItem = new ToolStripMenuItem();
            printPreviewToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            newTabToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            editToolStripMenuItem = new ToolStripMenuItem();
            undoToolStripMenuItem = new ToolStripMenuItem();
            redoToolStripMenuItem = new ToolStripMenuItem();
            cutToolStripMenuItem = new ToolStripMenuItem();
            copyToolStripMenuItem = new ToolStripMenuItem();
            pasteToolStripMenuItem = new ToolStripMenuItem();
            selectAllToolStripMenuItem = new ToolStripMenuItem();
            toolsToolStripMenuItem = new ToolStripMenuItem();
            customizeToolStripMenuItem = new ToolStripMenuItem();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            debugToolStripMenuItem = new ToolStripMenuItem();
            searchDocumentToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            toolStrip1 = new ToolStrip();
            newToolStripButton = new ToolStripButton();
            openToolStripButton = new ToolStripButton();
            saveToolStripButton = new ToolStripButton();
            printToolStripButton = new ToolStripButton();
            toolStripSeparator = new ToolStripSeparator();
            cutToolStripButton = new ToolStripButton();
            copyToolStripButton = new ToolStripButton();
            pasteToolStripButton = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            validate = new ToolStripButton();
            helpToolStripButton = new ToolStripButton();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            openFileDialog1 = new OpenFileDialog();
            autocompleteMenu1 = new AutocompleteMenuNS.AutocompleteMenu();
            saveFileDialog1 = new SaveFileDialog();
            enhanced_Tab_Control1 = new Enhanced_Tab_Control.Enhanced_Tab_Control();
            menuStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.FromArgb(    32,     32,     32);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, toolsToolStripMenuItem, searchDocumentToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1565, 28);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, openToolStripMenuItem, saveToolStripMenuItem, saveAsToolStripMenuItem, printToolStripMenuItem, printPreviewToolStripMenuItem, exitToolStripMenuItem, newTabToolStripMenuItem, toolStripSeparator2 });
            fileToolStripMenuItem.ForeColor = Color.White;
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(44, 24);
            fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.BackColor = Color.FromArgb(    32,     32,     32);
            newToolStripMenuItem.ForeColor = Color.White;
            newToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.ShortcutKeys =    Keys.Control  |  Keys.N ;
            newToolStripMenuItem.Size = new Size(167, 24);
            newToolStripMenuItem.Text = "&New";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.BackColor = Color.FromArgb(    32,     32,     32);
            openToolStripMenuItem.ForeColor = Color.White;
            openToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.ShortcutKeys =    Keys.Control  |  Keys.O ;
            openToolStripMenuItem.Size = new Size(167, 24);
            openToolStripMenuItem.Text = "&Open";
            openToolStripMenuItem.Click +=  openToolStripMenuItem_Click ;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.BackColor = Color.FromArgb(    32,     32,     32);
            saveToolStripMenuItem.ForeColor = Color.White;
            saveToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.ShortcutKeys =    Keys.Control  |  Keys.S ;
            saveToolStripMenuItem.Size = new Size(167, 24);
            saveToolStripMenuItem.Text = "&Save";
            saveToolStripMenuItem.Click +=  saveToolStripMenuItem_Click ;
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.BackColor = Color.FromArgb(    32,     32,     32);
            saveAsToolStripMenuItem.ForeColor = Color.White;
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new Size(167, 24);
            saveAsToolStripMenuItem.Text = "Save &As";
            saveAsToolStripMenuItem.Click +=  saveAsToolStripMenuItem_Click ;
            // 
            // printToolStripMenuItem
            // 
            printToolStripMenuItem.BackColor = Color.FromArgb(    32,     32,     32);
            printToolStripMenuItem.ForeColor = Color.White;
            printToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            printToolStripMenuItem.Name = "printToolStripMenuItem";
            printToolStripMenuItem.ShortcutKeys =    Keys.Control  |  Keys.P ;
            printToolStripMenuItem.Size = new Size(167, 24);
            printToolStripMenuItem.Text = "&Print";
            // 
            // printPreviewToolStripMenuItem
            // 
            printPreviewToolStripMenuItem.BackColor = Color.FromArgb(    32,     32,     32);
            printPreviewToolStripMenuItem.ForeColor = Color.White;
            printPreviewToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
            printPreviewToolStripMenuItem.Size = new Size(167, 24);
            printPreviewToolStripMenuItem.Text = "Print Pre&view";
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.BackColor = Color.FromArgb(    32,     32,     32);
            exitToolStripMenuItem.ForeColor = Color.White;
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(167, 24);
            exitToolStripMenuItem.Text = "E&xit";
            // 
            // newTabToolStripMenuItem
            // 
            newTabToolStripMenuItem.Name = "newTabToolStripMenuItem";
            newTabToolStripMenuItem.Size = new Size(167, 24);
            newTabToolStripMenuItem.Text = "New &Tab";
            newTabToolStripMenuItem.Click +=  newTabToolStripMenuItem_Click ;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(164, 6);
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.BackColor = Color.FromArgb(    32,     32,     32);
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { undoToolStripMenuItem, redoToolStripMenuItem, cutToolStripMenuItem, copyToolStripMenuItem, pasteToolStripMenuItem, selectAllToolStripMenuItem });
            editToolStripMenuItem.ForeColor = Color.White;
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(47, 24);
            editToolStripMenuItem.Text = "&Edit";
            // 
            // undoToolStripMenuItem
            // 
            undoToolStripMenuItem.BackColor = Color.FromArgb(    32,     32,     32);
            undoToolStripMenuItem.ForeColor = Color.White;
            undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            undoToolStripMenuItem.Size = new Size(140, 24);
            undoToolStripMenuItem.Text = "&Undo";
            // 
            // redoToolStripMenuItem
            // 
            redoToolStripMenuItem.BackColor = Color.FromArgb(    32,     32,     32);
            redoToolStripMenuItem.ForeColor = Color.White;
            redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            redoToolStripMenuItem.Size = new Size(140, 24);
            redoToolStripMenuItem.Text = "&Redo";
            // 
            // cutToolStripMenuItem
            // 
            cutToolStripMenuItem.BackColor = Color.FromArgb(    32,     32,     32);
            cutToolStripMenuItem.ForeColor = Color.White;
            cutToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            cutToolStripMenuItem.Size = new Size(140, 24);
            cutToolStripMenuItem.Text = "Cu&t";
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.BackColor = Color.FromArgb(    32,     32,     32);
            copyToolStripMenuItem.ForeColor = Color.White;
            copyToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.Size = new Size(140, 24);
            copyToolStripMenuItem.Text = "&Copy";
            // 
            // pasteToolStripMenuItem
            // 
            pasteToolStripMenuItem.BackColor = Color.FromArgb(    32,     32,     32);
            pasteToolStripMenuItem.ForeColor = Color.White;
            pasteToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            pasteToolStripMenuItem.Size = new Size(140, 24);
            pasteToolStripMenuItem.Text = "&Paste";
            // 
            // selectAllToolStripMenuItem
            // 
            selectAllToolStripMenuItem.BackColor = Color.FromArgb(    32,     32,     32);
            selectAllToolStripMenuItem.ForeColor = Color.White;
            selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            selectAllToolStripMenuItem.Size = new Size(140, 24);
            selectAllToolStripMenuItem.Text = "Select &All";
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { customizeToolStripMenuItem, optionsToolStripMenuItem, debugToolStripMenuItem });
            toolsToolStripMenuItem.ForeColor = Color.White;
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new Size(56, 24);
            toolsToolStripMenuItem.Text = "&Tools";
            // 
            // customizeToolStripMenuItem
            // 
            customizeToolStripMenuItem.BackColor = Color.FromArgb(    32,     32,     32);
            customizeToolStripMenuItem.ForeColor = Color.White;
            customizeToolStripMenuItem.Name = "customizeToolStripMenuItem";
            customizeToolStripMenuItem.Size = new Size(147, 24);
            customizeToolStripMenuItem.Text = "&Customize";
            customizeToolStripMenuItem.Click +=  customizeToolStripMenuItem_Click ;
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.BackColor = Color.FromArgb(    32,     32,     32);
            optionsToolStripMenuItem.ForeColor = Color.White;
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(147, 24);
            optionsToolStripMenuItem.Text = "&Options";
            optionsToolStripMenuItem.Click +=  optionsToolStripMenuItem_Click ;
            // 
            // debugToolStripMenuItem
            // 
            debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            debugToolStripMenuItem.Size = new Size(147, 24);
            debugToolStripMenuItem.Text = "&Compare";
            debugToolStripMenuItem.Click +=  debugToolStripMenuItem_Click ;
            // 
            // searchDocumentToolStripMenuItem
            // 
            searchDocumentToolStripMenuItem.ForeColor = Color.White;
            searchDocumentToolStripMenuItem.Name = "searchDocumentToolStripMenuItem";
            searchDocumentToolStripMenuItem.Size = new Size(138, 24);
            searchDocumentToolStripMenuItem.Text = "&Search Document";
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aboutToolStripMenuItem });
            helpToolStripMenuItem.ForeColor = Color.White;
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(53, 24);
            helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.BackColor = Color.FromArgb(    32,     32,     32);
            aboutToolStripMenuItem.ForeColor = Color.White;
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(128, 24);
            aboutToolStripMenuItem.Text = "&About...";
            // 
            // toolStrip1
            // 
            toolStrip1.BackColor = Color.FromArgb(    32,     32,     32);
            toolStrip1.GripMargin = new Padding(0);
            toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip1.Items.AddRange(new ToolStripItem[] { newToolStripButton, openToolStripButton, saveToolStripButton, printToolStripButton, toolStripSeparator, cutToolStripButton, copyToolStripButton, pasteToolStripButton, toolStripSeparator1, validate, helpToolStripButton });
            toolStrip1.Location = new Point(0, 28);
            toolStrip1.Margin = new Padding(0, 0, 0, 15);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.RenderMode = ToolStripRenderMode.System;
            toolStrip1.Size = new Size(1565, 25);
            toolStrip1.TabIndex = 2;
            toolStrip1.Text = "toolStrip1";
            // 
            // newToolStripButton
            // 
            newToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            newToolStripButton.Image = ( Image )resources.GetObject("newToolStripButton.Image");
            newToolStripButton.ImageTransparentColor = Color.Magenta;
            newToolStripButton.Name = "newToolStripButton";
            newToolStripButton.Size = new Size(23, 22);
            newToolStripButton.Text = "&New";
            newToolStripButton.Click +=  newToolStripButton_Click ;
            // 
            // openToolStripButton
            // 
            openToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            openToolStripButton.Image = ( Image )resources.GetObject("openToolStripButton.Image");
            openToolStripButton.ImageTransparentColor = Color.Magenta;
            openToolStripButton.Name = "openToolStripButton";
            openToolStripButton.Size = new Size(23, 22);
            openToolStripButton.Text = "&Open";
            // 
            // saveToolStripButton
            // 
            saveToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            saveToolStripButton.Image = ( Image )resources.GetObject("saveToolStripButton.Image");
            saveToolStripButton.ImageTransparentColor = Color.Magenta;
            saveToolStripButton.Name = "saveToolStripButton";
            saveToolStripButton.Size = new Size(23, 22);
            saveToolStripButton.Text = "&Save";
            // 
            // printToolStripButton
            // 
            printToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            printToolStripButton.Image = ( Image )resources.GetObject("printToolStripButton.Image");
            printToolStripButton.ImageTransparentColor = Color.Magenta;
            printToolStripButton.Name = "printToolStripButton";
            printToolStripButton.Size = new Size(23, 22);
            printToolStripButton.Text = "&Print";
            // 
            // toolStripSeparator
            // 
            toolStripSeparator.Name = "toolStripSeparator";
            toolStripSeparator.Size = new Size(6, 25);
            // 
            // cutToolStripButton
            // 
            cutToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            cutToolStripButton.Image = ( Image )resources.GetObject("cutToolStripButton.Image");
            cutToolStripButton.ImageTransparentColor = Color.Magenta;
            cutToolStripButton.Name = "cutToolStripButton";
            cutToolStripButton.Size = new Size(23, 22);
            cutToolStripButton.Text = "C&ut";
            // 
            // copyToolStripButton
            // 
            copyToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            copyToolStripButton.Image = ( Image )resources.GetObject("copyToolStripButton.Image");
            copyToolStripButton.ImageTransparentColor = Color.Magenta;
            copyToolStripButton.Name = "copyToolStripButton";
            copyToolStripButton.Size = new Size(23, 22);
            copyToolStripButton.Text = "&Copy";
            // 
            // pasteToolStripButton
            // 
            pasteToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            pasteToolStripButton.Image = ( Image )resources.GetObject("pasteToolStripButton.Image");
            pasteToolStripButton.ImageTransparentColor = Color.Magenta;
            pasteToolStripButton.Name = "pasteToolStripButton";
            pasteToolStripButton.Size = new Size(23, 22);
            pasteToolStripButton.Text = "&Paste";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // validate
            // 
            validate.DisplayStyle = ToolStripItemDisplayStyle.Image;
            validate.Image = ( Image )resources.GetObject("validate.Image");
            validate.ImageTransparentColor = Color.Magenta;
            validate.Name = "validate";
            validate.Size = new Size(23, 22);
            validate.Text = "toolStripButton1";
            validate.Click +=  validate_Click ;
            // 
            // helpToolStripButton
            // 
            helpToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            helpToolStripButton.Image = ( Image )resources.GetObject("helpToolStripButton.Image");
            helpToolStripButton.ImageTransparentColor = Color.Magenta;
            helpToolStripButton.Name = "helpToolStripButton";
            helpToolStripButton.Size = new Size(23, 22);
            helpToolStripButton.Text = "He&lp";
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.FromArgb(    32,     32,     32);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripStatusLabel2 });
            statusStrip1.Location = new Point(0, 884);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1565, 22);
            statusStrip1.SizingGrip = false;
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.ForeColor = SystemColors.Control;
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(0, 17);
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.ForeColor = SystemColors.Control;
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(0, 17);
            // 
            // autocompleteMenu1
            // 
            autocompleteMenu1.AllowsTabKey = true;
            autocompleteMenu1.AppearInterval = 1000;
            autocompleteMenu1.Colors = ( AutocompleteMenuNS.Colors )resources.GetObject("autocompleteMenu1.Colors");
            autocompleteMenu1.Font = new Font("Verdana", 9.75F, FontStyle.Italic, GraphicsUnit.Point,   0);
            autocompleteMenu1.ImageList = null;
            autocompleteMenu1.TargetControlWrapper = null;
            // 
            // enhanced_Tab_Control1
            // 
            enhanced_Tab_Control1.Dock = DockStyle.Fill;
            enhanced_Tab_Control1.DrawMode = TabDrawMode.OwnerDrawFixed;
            enhanced_Tab_Control1.Location = new Point(0, 53);
            enhanced_Tab_Control1.Name = "enhanced_Tab_Control1";
            enhanced_Tab_Control1.SelectedIndex = 0;
            enhanced_Tab_Control1.Size = new Size(1565, 831);
            enhanced_Tab_Control1.TabIndex = 4;
            enhanced_Tab_Control1.TabPageImage = ( Image )resources.GetObject("enhanced_Tab_Control1.TabPageImage");
            enhanced_Tab_Control1.Selected +=  enhanced_Tab_Control1_Selected ;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.FromArgb(    32,     32,     32);
            ClientSize = new Size(1565, 906);
            Controls.Add(enhanced_Tab_Control1);
            Controls.Add(statusStrip1);
            Controls.Add(toolStrip1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            RightToLeft = RightToLeft.No;
            StartPosition = FormStartPosition.Manual;
            Text = "IFC Mod Editor";
            FormClosing +=  Form1_FormClosing ;
            Load +=  Form1_Load ;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private void OpenToolStripButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void FileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

#endregion
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripMenuItem printToolStripMenuItem;
        private ToolStripMenuItem printPreviewToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem undoToolStripMenuItem;
        private ToolStripMenuItem redoToolStripMenuItem;
        private ToolStripMenuItem cutToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem pasteToolStripMenuItem;
        private ToolStripMenuItem selectAllToolStripMenuItem;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem customizeToolStripMenuItem;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem debugToolStripMenuItem;
        private ToolStrip toolStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private ToolStripButton newToolStripButton;
        private ToolStripButton openToolStripButton;
        private ToolStripButton saveToolStripButton;
        private ToolStripButton printToolStripButton;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripButton cutToolStripButton;
        private ToolStripButton copyToolStripButton;
        private ToolStripButton pasteToolStripButton;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton helpToolStripButton;
        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
        private ToolStripButton validate;
        private ToolStripMenuItem newTabToolStripMenuItem;
        private Enhanced_Tab_Control.Enhanced_Tab_Control enhanced_Tab_Control1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem searchDocumentToolStripMenuItem;
        public  AutocompleteMenuNS.AutocompleteMenu autocompleteMenu1;
        public static StatusStrip statusStrip1;
    }
}
