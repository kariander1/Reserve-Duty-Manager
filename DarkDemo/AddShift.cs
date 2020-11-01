using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DarkDemo
{
    public partial class AddShift : Form
    {
    
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        public ShiftEntity currentShift { get; set; }
        private Hashtable teams { get; set; }
        public AddShift(Hashtable teams, ShiftEntity shift)
        {
            InitializeComponent();
            this.teams = teams;
            this.currentShift = shift;
            PopulateFields();

        }
        public AddShift(Hashtable teams)
        {
            InitializeComponent();
            this.teams = teams;
            PopulateFields();
        }
        public void PopulateFields()
        {
            if (this.currentShift != null)
            {
                textBox_ShiftName.Text = this.currentShift.shiftName;
                numericUpDown_validity.Value = this.currentShift.ValidityInDays;
                if(this.currentShift.ValidityInDays==0)
                {
                    checkBox_AlwaysValid.Checked = true;

                }
                comboBox_team.Text = teams[int.Parse(this.currentShift.teamIndex)].ToString();
            }
            PopulateComboBox();
          
        }

        private void button_quit_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
        public void PopulateComboBox()
        {
            foreach (string team in teams.Values)
            {
                comboBox_team.Items.Add(team);
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
        private ShiftEntity BuildShift()
        {
            string shiftname = textBox_ShiftName.Text;
            int validityDays =(int)numericUpDown_validity.Value;
            if (checkBox_AlwaysValid.Checked)
                validityDays = 0;

            var key = teams.Keys.OfType<object>().FirstOrDefault(s => teams[s].ToString() == comboBox_team.Text);
            string teamindex = key.ToString();
          

                ShiftEntity temp;
            if (this.currentShift != null)
            {
                temp = currentShift;
            }
            else
            {
                temp = new ShiftEntity();
                
            }
          //  temp.ID = pn;
            temp.shiftName = shiftname;
            temp.ValidityInDays = validityDays;
            temp.teamIndex= teamindex;
         
            return temp;
        }
        


        private void button_add_Click(object sender, EventArgs e)
        {
         
            this.currentShift = BuildShift();
            this.DialogResult = DialogResult.OK;
        }
        private void checkBox_AlwaysValid_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown_validity.Enabled = !((checkBox_AlwaysValid).Checked);
        }
    }
}
