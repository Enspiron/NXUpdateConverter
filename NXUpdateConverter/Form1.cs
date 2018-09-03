using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NXUpdateConverter
{
    public partial class Form1 : Form
    {
        private string make(uint major, uint minor, uint micro, uint bugfix)
        {
            var m = (major - 0xFC000000) << 26;
            var n = (minor - 0x03F00000) << 20;
            var c = (micro - 0x000F0000) << 16;
            return $"{m + n + c + bugfix}";
        }

        private string parse(uint version)
        {
            var major = (version & 0xFC000000) >> 26;
            var minor = (version & 0x03F00000) >> 20;
            var micro = (version & 0x000F0000) >> 16;
            var bugfix = version & 0x0000FFFF;
            return $"{major}.{minor}.{micro}.{bugfix}";
        }

        private static bool convert = true;

        //static bool convert = true;
        public Form1()
        {
            InitializeComponent();
            if(convert)
            {
                Convert.Text = "Convert ↓";
            }
        }

        private void Convert_Click(object sender, EventArgs e)
        {
            if (convert)
            {
                convert = false;
                Convert.Text = "Convert ↑";
            }
            else
            {
                convert = true;
                Convert.Text = "Convert ↓";
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("GUI made by Enspiron! With help for code by SimonMKWii!");
        }

        private void Go_Click(object sender, EventArgs e)
        {
            //Thansk to SimonMKWii for the Code!!!
            if (cnmts.Text != "" || Version.Text != "")
            {
                if (convert)
                {
                    cnmts.Text = make(System.Convert.ToUInt32(Version.Text.Substring(0, 1)),
                            System.Convert.ToUInt32(Version.Text.Substring(2, 1)),
                            System.Convert.ToUInt32(Version.Text.Substring(4, 1)),
                            System.Convert.ToUInt32(Version.Text.Substring(6)));
                }
                else
                {
                    Version.Text = parse(System.Convert.ToUInt32(cnmts.Text));
                }
            }
        }

        private void copyVersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(Version.Text != "")
            Clipboard.SetText(Version.Text);
        }

        private void copyConvertedVersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(cnmts.Text != "")
            Clipboard.SetText(cnmts.Text);
        }

        private void cnmts_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(cnmts, "This is the CNMTS");
            toolTip1.SetToolTip(Version, "This is the Version");
            toolTip1.SetToolTip(Convert, "Click to switch between conversions");
            toolTip1.SetToolTip(Go, "Click to convert!");
        }

     

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Info info = new Info();
            info.Show();
        }
    }
}
