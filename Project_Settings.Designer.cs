﻿




namespace Json_Editor
    {
    partial class Project_Settings
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            groupBox1 = new GroupBox();
            Lnbc = new Label();
            Lnc = new Label();
            Ebc = new Label();
            lbCm = new Label();
            CaretCb = new ComboBox();
            label11 = new Label();
            label7 = new Label();
            label3 = new Label();
            label2 = new Label();
            SavButton = new Button();
            label8 = new Label();
            label4 = new Label();
            colorDialog1 = new ColorDialog();
            fontDialog1 = new FontDialog();
            Fonts = new GroupBox();
            label5 = new Label();
            button1 = new Button();
            groupBox2 = new GroupBox();
            UU_Folder = new Button();
            Unpl_Folder = new Button();
            Set_mod_Folder = new Button();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            label10 = new Label();
            label9 = new Label();
            groupBox1.SuspendLayout();
            Fonts.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F);
            label1.ForeColor = Color.White;
            label1.Location = new Point(6, 34);
            label1.Name = "label1";
            label1.Size = new Size(108, 25);
            label1.TabIndex = 0;
            label1.Text = "Carret Style";
            // 
            // groupBox1
            // 
            groupBox1.Anchor =      AnchorStyles.Top  |  AnchorStyles.Bottom   |  AnchorStyles.Left   |  AnchorStyles.Right ;
            groupBox1.Controls.Add(Lnbc);
            groupBox1.Controls.Add(Lnc);
            groupBox1.Controls.Add(Ebc);
            groupBox1.Controls.Add(lbCm);
            groupBox1.Controls.Add(CaretCb);
            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.ForeColor = Color.White;
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(756, 242);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Editor Styles Options";
            // 
            // Lnbc
            // 
            Lnbc.BackColor = SystemColors.ActiveCaption;
            Lnbc.BorderStyle = BorderStyle.Fixed3D;
            Lnbc.Location = new Point(241, 193);
            Lnbc.Name = "Lnbc";
            Lnbc.Size = new Size(20, 20);
            Lnbc.TabIndex = 3;
            Lnbc.Click +=  Lnbc_Click ;
            // 
            // Lnc
            // 
            Lnc.BackColor = SystemColors.ActiveCaption;
            Lnc.BorderStyle = BorderStyle.Fixed3D;
            Lnc.Location = new Point(241, 157);
            Lnc.Name = "Lnc";
            Lnc.Size = new Size(20, 20);
            Lnc.TabIndex = 3;
            Lnc.Click +=  Lnc_Click ;
            // 
            // Ebc
            // 
            Ebc.BackColor = SystemColors.ActiveCaption;
            Ebc.BorderStyle = BorderStyle.Fixed3D;
            Ebc.Location = new Point(241, 121);
            Ebc.Name = "Ebc";
            Ebc.Size = new Size(20, 20);
            Ebc.TabIndex = 3;
            Ebc.Click +=  Ebc_Click ;
            // 
            // lbCm
            // 
            lbCm.BackColor = SystemColors.ActiveCaption;
            lbCm.BorderStyle = BorderStyle.Fixed3D;
            lbCm.Location = new Point(241, 87);
            lbCm.Name = "lbCm";
            lbCm.Size = new Size(20, 20);
            lbCm.TabIndex = 3;
            lbCm.Click +=  lbCm_Click ;
            // 
            // CaretCb
            // 
            CaretCb.FormattingEnabled = true;
            CaretCb.Items.AddRange(new object[] { "Line", "Block", "Invisible" });
            CaretCb.Location = new Point(163, 31);
            CaretCb.Name = "CaretCb";
            CaretCb.Size = new Size(98, 28);
            CaretCb.TabIndex = 1;
            CaretCb.SelectedValueChanged +=  CaretCb_SelectedValueChanged ;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 14.25F);
            label11.ForeColor = Color.White;
            label11.Location = new Point(6, 188);
            label11.Name = "label11";
            label11.Size = new Size(224, 25);
            label11.TabIndex = 0;
            label11.Text = "Line Numbers Back Color";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 14.25F);
            label7.ForeColor = Color.White;
            label7.Location = new Point(6, 152);
            label7.Name = "label7";
            label7.Size = new Size(180, 25);
            label7.TabIndex = 0;
            label7.Text = "Line Numbers Color";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14.25F);
            label3.ForeColor = Color.White;
            label3.Location = new Point(6, 116);
            label3.Name = "label3";
            label3.Size = new Size(152, 25);
            label3.TabIndex = 0;
            label3.Text = "Editor BackColor";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F);
            label2.ForeColor = Color.White;
            label2.Location = new Point(6, 82);
            label2.Name = "label2";
            label2.Size = new Size(173, 25);
            label2.TabIndex = 0;
            label2.Text = "Margins Backcolor ";
            // 
            // SavButton
            // 
            SavButton.Anchor =    AnchorStyles.Bottom  |  AnchorStyles.Right ;
            SavButton.BackColor = Color.FromArgb(    32,     32,     32);
            SavButton.ForeColor = Color.White;
            SavButton.Location = new Point(654, 881);
            SavButton.Name = "SavButton";
            SavButton.Size = new Size(114, 29);
            SavButton.TabIndex = 2;
            SavButton.Text = "Apply Settings";
            SavButton.UseVisualStyleBackColor = false;
            SavButton.Click +=  SavButton_Click ;
            // 
            // label8
            // 
            label8.Anchor =      AnchorStyles.Top  |  AnchorStyles.Bottom   |  AnchorStyles.Left   |  AnchorStyles.Right ;
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 14.25F);
            label8.ForeColor = Color.White;
            label8.Location = new Point(6, 41);
            label8.Name = "label8";
            label8.Size = new Size(109, 25);
            label8.TabIndex = 0;
            label8.Text = "Mod Folder";
            // 
            // label4
            // 
            label4.Anchor =      AnchorStyles.Top  |  AnchorStyles.Bottom   |  AnchorStyles.Left   |  AnchorStyles.Right ;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14.25F);
            label4.ForeColor = Color.White;
            label4.Location = new Point(6, 41);
            label4.Name = "label4";
            label4.Size = new Size(113, 25);
            label4.TabIndex = 0;
            label4.Text = "Editor Font :";
            // 
            // colorDialog1
            // 
            colorDialog1.FullOpen = true;
            colorDialog1.SolidColorOnly = true;
            // 
            // fontDialog1
            // 
            fontDialog1.ShowEffects = false;
            // 
            // Fonts
            // 
            Fonts.Anchor =      AnchorStyles.Top  |  AnchorStyles.Bottom   |  AnchorStyles.Left   |  AnchorStyles.Right ;
            Fonts.Controls.Add(label5);
            Fonts.Controls.Add(button1);
            Fonts.Controls.Add(label4);
            Fonts.ForeColor = Color.White;
            Fonts.Location = new Point(12, 272);
            Fonts.Name = "Fonts";
            Fonts.Size = new Size(756, 102);
            Fonts.TabIndex = 8;
            Fonts.TabStop = false;
            Fonts.Text = "Fonts";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(163, 47);
            label5.Name = "label5";
            label5.Size = new Size(34, 20);
            label5.TabIndex = 9;
            label5.Text = "Lfnt";
            // 
            // button1
            // 
            button1.ForeColor = Color.Black;
            button1.Location = new Point(715, 43);
            button1.Name = "button1";
            button1.Size = new Size(28, 24);
            button1.TabIndex = 8;
            button1.Text = "...";
            button1.UseVisualStyleBackColor = true;
            button1.Click +=  button1_Click_1 ;
            // 
            // groupBox2
            // 
            groupBox2.Anchor =      AnchorStyles.Top  |  AnchorStyles.Bottom   |  AnchorStyles.Left   |  AnchorStyles.Right ;
            groupBox2.Controls.Add(UU_Folder);
            groupBox2.Controls.Add(Unpl_Folder);
            groupBox2.Controls.Add(Set_mod_Folder);
            groupBox2.Controls.Add(textBox3);
            groupBox2.Controls.Add(textBox2);
            groupBox2.Controls.Add(textBox1);
            groupBox2.Controls.Add(label10);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(label8);
            groupBox2.ForeColor = Color.White;
            groupBox2.Location = new Point(12, 482);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(756, 189);
            groupBox2.TabIndex = 9;
            groupBox2.TabStop = false;
            groupBox2.Text = "Application Paths";
            // 
            // UU_Folder
            // 
            UU_Folder.Anchor =      AnchorStyles.Top  |  AnchorStyles.Bottom   |  AnchorStyles.Left   |  AnchorStyles.Right ;
            UU_Folder.BackColor = Color.FromArgb(    32,     32,     32);
            UU_Folder.ForeColor = Color.White;
            UU_Folder.Location = new Point(715, 139);
            UU_Folder.Name = "UU_Folder";
            UU_Folder.Size = new Size(30, 27);
            UU_Folder.TabIndex = 2;
            UU_Folder.Text = "...";
            UU_Folder.UseVisualStyleBackColor = false;
            // 
            // Unpl_Folder
            // 
            Unpl_Folder.Anchor =      AnchorStyles.Top  |  AnchorStyles.Bottom   |  AnchorStyles.Left   |  AnchorStyles.Right ;
            Unpl_Folder.BackColor = Color.FromArgb(    32,     32,     32);
            Unpl_Folder.ForeColor = Color.White;
            Unpl_Folder.Location = new Point(715, 91);
            Unpl_Folder.Name = "Unpl_Folder";
            Unpl_Folder.Size = new Size(30, 27);
            Unpl_Folder.TabIndex = 2;
            Unpl_Folder.Text = "...";
            Unpl_Folder.UseVisualStyleBackColor = false;
            // 
            // Set_mod_Folder
            // 
            Set_mod_Folder.Anchor =      AnchorStyles.Top  |  AnchorStyles.Bottom   |  AnchorStyles.Left   |  AnchorStyles.Right ;
            Set_mod_Folder.BackColor = Color.FromArgb(    32,     32,     32);
            Set_mod_Folder.ForeColor = Color.White;
            Set_mod_Folder.Location = new Point(715, 42);
            Set_mod_Folder.Name = "Set_mod_Folder";
            Set_mod_Folder.Size = new Size(30, 27);
            Set_mod_Folder.TabIndex = 2;
            Set_mod_Folder.Text = "...";
            Set_mod_Folder.UseVisualStyleBackColor = false;
            // 
            // textBox3
            // 
            textBox3.Anchor =      AnchorStyles.Top  |  AnchorStyles.Bottom   |  AnchorStyles.Left   |  AnchorStyles.Right ;
            textBox3.Location = new Point(206, 139);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(496, 27);
            textBox3.TabIndex = 1;
            // 
            // textBox2
            // 
            textBox2.Anchor =      AnchorStyles.Top  |  AnchorStyles.Bottom   |  AnchorStyles.Left   |  AnchorStyles.Right ;
            textBox2.Location = new Point(206, 91);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(496, 27);
            textBox2.TabIndex = 1;
            // 
            // textBox1
            // 
            textBox1.Anchor =      AnchorStyles.Top  |  AnchorStyles.Bottom   |  AnchorStyles.Left   |  AnchorStyles.Right ;
            textBox1.Location = new Point(206, 39);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(496, 27);
            textBox1.TabIndex = 1;
            // 
            // label10
            // 
            label10.Anchor =      AnchorStyles.Top  |  AnchorStyles.Bottom   |  AnchorStyles.Left   |  AnchorStyles.Right ;
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 14.25F);
            label10.ForeColor = Color.White;
            label10.Location = new Point(6, 141);
            label10.Name = "label10";
            label10.Size = new Size(153, 25);
            label10.TabIndex = 0;
            label10.Text = "Unreal Unpacker";
            // 
            // label9
            // 
            label9.Anchor =      AnchorStyles.Top  |  AnchorStyles.Bottom   |  AnchorStyles.Left   |  AnchorStyles.Right ;
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 14.25F);
            label9.ForeColor = Color.White;
            label9.Location = new Point(6, 90);
            label9.Name = "label9";
            label9.Size = new Size(194, 25);
            label9.TabIndex = 0;
            label9.Text = "Unpacked Game Data";
            // 
            // Project_Settings
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(    32,     32,     32);
            ClientSize = new Size(780, 922);
            Controls.Add(groupBox2);
            Controls.Add(Fonts);
            Controls.Add(groupBox1);
            Controls.Add(SavButton);
            Name = "Project_Settings";
            Text = "settings";
            FormClosing +=  Project_Settings_FormClosing ;
            Load +=  Project_Settings_Load ;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            Fonts.ResumeLayout(false);
            Fonts.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private GroupBox groupBox1;
        private Label label8;
        private Label label7;
        private Label label4;
        private Label label3;
        private Label label2;
        private ComboBox CaretCb;
        private ColorDialog colorDialog1;
        private Button SavButton;
        private Label lbCm;
        private Label Ebc;
        private Label Lnbc;
        private Label Lnc;
        private FontDialog fontDialog1;
        private GroupBox Fonts;
        private GroupBox groupBox2;
        private TextBox textBox3;
        private TextBox textBox2;
        private TextBox textBox1;
        private Label label10;
        private Label label9;
        private Button UU_Folder;
        private Button Unpl_Folder;
        private Button Set_mod_Folder;
        private Label label11;
        private Button button1;
        private Label label5;
        private Label Lfnt;

    }
    }