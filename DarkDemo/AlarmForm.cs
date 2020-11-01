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
    public partial class AlarmForm : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        public AlarmForm()
        {
            InitializeComponent();
     //       dataGridView_Logs.FirstDisplayedScrollingColumnIndex = 0;
        }

        private void button_quit_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
        public void addToLog(string text)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.Cells.Add(new DataGridViewTextBoxCell() { Value = DateTime.Now });
            row.Cells.Add(new DataGridViewTextBoxCell() { Value =text });
      
            if (dataGridView_Logs.InvokeRequired)
            {
                dataGridView_Logs.Invoke(new Action(() => dataGridView_Logs.Rows.Add(row)));
            }
            else
            {
                dataGridView_Logs.Rows.Add(row);
            }
        }

        private void panel_Top_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
