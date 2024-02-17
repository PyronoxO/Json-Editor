using ScintillaNET;
using ScintillaNET_FindReplaceDialog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Json_Editor
{
    public partial class Form3 : Form
    {
        
        public Form3()
        {
            InitializeComponent();


            //MyFindReplace = new FindReplace(scintilla1);
            // MyFindReplace.KeyPressed += MyFindReplace_KeyPressed;
        }



        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }


        //private GoTo gg(Scintilla scintilla)

        //{
        //    GoTo MyGoTo = new GoTo(scintilla1);
        //    MyGoTo.ShowGoToDialog();
        //    return MyGoTo;
        //}



        private void Form3_KeyDown(object sender, KeyEventArgs e)
        {



        }

        private bool isEventHandled = false;

        private void Form3_Load(object sender, EventArgs e)
        {
            //sc_editor1.FilePath = @"C:\Users\ponto.DESKTOP-SNTQMLB\Desktop\IcarusModEditorVer3_8\Mods\Ammo_Pack\data\MetaWorkshop\New folder\D_WorkshopItems.json";
            //sc_editor1.ApplyUiStyling = true;
           // sc_editor1.Refresh();   
        }

        //private void scintilla1_KeyDown(object sender, KeyEventArgs e)
        //{



        //    FNR.HandleKeyDown(sender, e);


        //}

        //private void ctlScintilla_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar < 32)
        //    {
        //        // Prevent control characters from getting inserted into the text buffer
        //        e.Handled = true;
        //        return;
        //    }
        //}

        //private void ctlScintilla_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar < 32)
        //    {
        //        // Prevent control characters from getting inserted into the text buffer
        //        e.Handled = true;
        //        return;
        //    }
        //}

        //private void MyFindReplace_KeyPressed(object sender, System.Windows.Forms.KeyEventArgs e)
        //{
        //    scintilla1_KeyDown(sender, e);
        //}

    }

}
