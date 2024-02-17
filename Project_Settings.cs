using ScintillaNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Json_Editor.HelperClass;

namespace Json_Editor
{
    public partial class Project_Settings : Form
    {
        public Project_Settings()
        {
            InitializeComponent();
        }

        private void Project_Settings_Load(object sender, EventArgs e)

        {
            CaretCb.SelectedItem = Properties.Settings1.Default.CaretStyle;
            lbCm.BackColor = Properties.Settings1.Default.MarginBackcolor;
            Ebc.BackColor = Properties.Settings1.Default.EditorBC;
            Lnc.BackColor = Properties.Settings1.Default.uLineNumFc;
            Lnbc.BackColor = Properties.Settings1.Default.uLineNumBc;

            FontCb.DisplayMember = "Name";
            FontCb.ValueMember = "Name"; // Set the ValueMember to the font name
            FontCb.DataSource = new InstalledFontCollection().Families;
            FsCb.SelectedItem = Properties.Settings1.Default.uFontSize.ToString();

            if (!Properties.Settings1.Default.uFontStyleb && !Properties.Settings1.Default.uFontStylei)
            {
                radioButton1.Checked = true;
            }
            else if (Properties.Settings1.Default.uFontStyleb && !Properties.Settings1.Default.uFontStylei)
            {
                radioButton2.Checked = true;
            }
            else if (!Properties.Settings1.Default.uFontStyleb && Properties.Settings1.Default.uFontStylei)
            {
                radioButton3.Checked = true;
            }
        }

        #region Control Handlers

        private void SavButton_Click(object sender, EventArgs e)
        {
            Properties.Settings1.Default.CaretStyle = CaretCb.SelectedItem?.ToString();

            Properties.Settings1.Default.Save();

            Sdr = DialogResult.OK;
        }

        private void lbCm_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = lbCm.BackColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                lbCm.BackColor = colorDialog1.Color;
                Properties.Settings1.Default.MarginBackcolor = colorDialog1.Color;
            }
        }

        private void Ebc_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = Ebc.BackColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)

            {
                Ebc.BackColor = colorDialog1.Color;
                Properties.Settings1.Default.uEditorBC = colorDialog1.Color;
                // change in helep class to take effect
            }
        }

        private void Lnc_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)

            {
                Lnc.BackColor = colorDialog1.Color;
                Properties.Settings1.Default.uLineNumFc = colorDialog1.Color;
                // change in helep class to take effect
            }
        }

        private void Lnbc_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)

            {
                Lnbc.BackColor = colorDialog1.Color;
                Properties.Settings1.Default.uLineNumBc = colorDialog1.Color;
                // change in helep class to take effect
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Properties.Settings1.Default.uFontStyleb)
            {
                fontDialog1.Font = new Font(Properties.Settings1.Default.uFont.ToString(), Properties.Settings1.Default.uFontSize, System.Drawing.FontStyle.Bold);
            }
            else
            {
                fontDialog1.Font = new Font(Properties.Settings1.Default.uFont.ToString(), Properties.Settings1.Default.uFontSize, System.Drawing.FontStyle.Regular);
            }

            if (fontDialog1.ShowDialog() == DialogResult.OK)

            {
                Properties.Settings1.Default.uFont = fontDialog1.Font.Name;
                if (fontDialog1.Font.Style == FontStyle.Bold)
                {
                    Properties.Settings1.Default.uFontStyleb = true;
                }
                else
                {
                    Properties.Settings1.Default.uFontStyleb = false;
                }
            }

            Properties.Settings1.Default.uFontSize = ( int )fontDialog1.Font.Size;
        }

        private void FontCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (FontCb.SelectedItem != null)
            {
                var selectedFont = ( FontFamily )FontCb.SelectedItem;
                var fontName = selectedFont.Name; // This will give you just the font name

                Properties.Settings1.Default.uFont = fontName;
            }
        }

        private void FsCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int tt = Int32.Parse(FsCb.SelectedItem?.ToString() ?? "0");

            Properties.Settings1.Default.uFontSize = Int32.Parse(FsCb.SelectedItem?.ToString() ?? "0");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                radioButton2.Checked = false;
                radioButton3.Checked = false;

                Properties.Settings1.Default.uFontStyleb = false;
                Properties.Settings1.Default.uFontStylei = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                radioButton1.Checked = false;
                radioButton3.Checked = false;
                Properties.Settings1.Default.uFontStyleb = true;
                Properties.Settings1.Default.uFontStylei = false;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                Properties.Settings1.Default.uFontStyleb = false;
                Properties.Settings1.Default.uFontStylei = true;
            }
        }

        #endregion Control Handlers

        private void button1_Click_1(object sender, EventArgs e)
        {
            fontDialog1.Font = Properties.Settings1.Default.DeFont;
            if (fontDialog1.ShowDialog() == DialogResult.OK && fontDialog1.Font.Underline)
            {

                Font newFont = new Font(fontDialog1.Font, fontDialog1.Font.Style | FontStyle.Underline);

                // Assign the new font to the DeFont property
                Properties.Settings1.Default.DeFont = newFont;
                 
            }
            else if (fontDialog1.ShowDialog() == DialogResult.OK && fontDialog1.Font.Underline && fontDialog1.Font.Strikeout)
            {
                Font newFont = new Font(fontDialog1.Font, fontDialog1.Font.Style | FontStyle.Underline | FontStyle.Strikeout);

                // Assign the new font to the DeFont property
                Properties.Settings1.Default.DeFont = newFont;

            }
            else if (fontDialog1.ShowDialog() == DialogResult.OK  && fontDialog1.Font.Strikeout)
            {
                Font newFont = new Font(fontDialog1.Font, fontDialog1.Font.Style | FontStyle.Strikeout);

                // Assign the new font to the DeFont property
                Properties.Settings1.Default.DeFont = newFont;

            }
            else if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings1.Default.DeFont = fontDialog1.Font; 

            }


        }

        
    }
}