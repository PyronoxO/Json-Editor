using ScintillaDiff;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ScintillaDiff.ScintillaDiffStyles;
using static Json_Editor.HelperClass;

namespace Json_Editor
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            comp.DiffStyle = DiffStyle.DiffSideBySide;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var opl = new OpenFileDialog();
                if (opl.ShowDialog() == DialogResult.OK)
                {
                    comp.TextLeft = File.ReadAllText(opl.FileName);

                    if (opl.FileName != "")

                    {
                        var opr = new OpenFileDialog();
                        if (opr.ShowDialog() == DialogResult.OK)
                        {
                            comp.TextRight = File.ReadAllText(opr.FileName);
                        }
                        else
                        {
                            if (opr.ShowDialog() == DialogResult.Cancel)

                            { MessageBox.Show("You Must load a file to copare"); }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var opl = new OpenFileDialog();
                if (opl.ShowDialog() == DialogResult.OK)
                {
                    comp.TextLeft = File.ReadAllText(opl.FileName);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var opr = new OpenFileDialog();
                if (opr.ShowDialog() == DialogResult.OK)
                {
                    comp.TextRight = File.ReadAllText(opr.FileName);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HelperClass.filep))
            {
                comp.TextLeft = HelperClass.filep;


            }
        }
    }
}