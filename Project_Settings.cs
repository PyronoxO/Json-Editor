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
            Lfnt.Text = ( Properties.Settings1.Default.DeFont.OriginalFontName + " " + "Click To Change" );
        }

        #region Control Handlers

       private void CaretCb_SelectedValueChanged(object sender, EventArgs e)
        {
            if (CaretCb.SelectedText == "Block")
            {
                Properties.Settings1.Default.CaretStyle = "Block";
            }
            else if (CaretCb.SelectedText == "Line")
            {
                Properties.Settings1.Default.CaretStyle = "Line";
            }
            else
            {
                Properties.Settings1.Default.CaretStyle = "Invisible";

            }

        }




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

        #endregion Control Handlers

        private void button1_Click_1(object sender, EventArgs e)
        {
            fontDialog1.Font = Properties.Settings1.Default.DeFont;
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings1.Default.DeFont = fontDialog1.Font;
            }
        }

        private void Project_Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings1.Default.Save();
        }
    }
}