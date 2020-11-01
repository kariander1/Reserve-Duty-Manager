using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DarkDemo
{
    public partial class DialogForm : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        public DialogForm(string title, string text,bool yesNo)
        {
            InitializeComponent();
            label_alarmList.Text = title;
            richTextBox_Text.Text = text;
            if (!yesNo)
                button_No.Visible = false;
     //       dataGridView_Logs.FirstDisplayedScrollingColumnIndex = 0;
        }



        private void panel_Top_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void DialogForm_Load(object sender, EventArgs e)
        {
            int duration = 100;//in milliseconds
            int steps = 10;
            Timer timer = new Timer();
            timer.Interval = duration / steps;

            int currentStep = 0;
            timer.Tick += (arg1, arg2) =>
            {
                Opacity = ((double)currentStep) / steps;
                currentStep++;

                if (currentStep >= steps)
                {
                    Opacity = 1;
                    timer.Stop();
                    timer.Dispose();
                }
            };

            timer.Start();
        }
        private DialogResult dr;
        private void DialogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            if (Opacity != 0)
            {
                dr = DialogResult;
                e.Cancel = true;
                int duration = 100;//in milliseconds
                int steps = 10;
                Timer timer = new Timer();
                timer.Interval = duration / steps;

                int currentStep = 0;
                timer.Tick += (arg1, arg2) =>
                {
                    Opacity = 1 - (((double)currentStep) / steps);
                    currentStep++;

                    if (currentStep >= steps)
                    {
                        Opacity = 0;
                        e.Cancel = false;
                        timer.Stop();
                        timer.Dispose();
                        Close();

                    }
                };

                timer.Start();
            }
            else
            {
                DialogResult = dr;
            }
        }
    }
}
