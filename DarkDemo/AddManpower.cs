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
    public partial class AddManpower : Form
    {
   
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        public ReserveDutyEntity manpower { get; set; }
        private List<ShiftEntity> shifts { get; set; }
        public AddManpower(ReserveDutyEntity manPower,List<ShiftEntity> shifts)
        {
            InitializeComponent();
            textBox_PersonalNumber.Enabled = false;
            this.manpower = manPower;
            this.shifts = shifts;
            PopulateFields();
         
        }
        public void PopulateFields()
        {
            if (this.manpower != null)
            {
                textBox_FirstName.Text = this.manpower.firstName;
                textBox_LastName.Text = this.manpower.lastName;
                textBox_PersonalNumber.Text = this.manpower.ID;
                dateTimePicker_ResignDate.Value = this.manpower.resignmentDate;
                checkBox_InManPower.Checked = this.manpower.inManPower;
            }
            PopulateComboBox();
            PopulateCheckboxlist(this.shifts);
        }
        public AddManpower(List<ShiftEntity> shifts)
        {
            InitializeComponent();          
            this.shifts = shifts;
            PopulateFields();
        }
        private void button_quit_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
        public void PopulateComboBox()
        {
            var values = Enum.GetValues(typeof(Rank)).Cast<Rank>();
            foreach (var item in values)
            {
                Rank r = (Rank)item;
                comboBox_rank.Items.Add(ReserveDutyEntity.TranslateToRank(r));
            }
            if(this.manpower==null)
                comboBox_rank.SelectedIndex = 0;
            else
            {
                int i = 0;
                foreach (var item in values)
                {
                    Rank r = (Rank)item;
                   if(r.ToString() == this.manpower.rank.ToString())
                    {
                        comboBox_rank.SelectedIndex = i;
                        break;
                    }
                    i++;
                }
            }
        }      
        public void PopulateCheckboxlist(List<ShiftEntity> qshifts)
        {
            checkedListBox_Tasks.Items.Clear();
            foreach (ShiftEntity item in qshifts)
            {
              
                int i= checkedListBox_Tasks.Items.Add(item.shiftName);
                if(this.manpower!=null)
                {
                    if(this.manpower.qualifiedShiftsIndexes.Contains(item.ID))
                    {
                        checkedListBox_Tasks.SetItemChecked(i, true);
                    }
                }
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
        private ReserveDutyEntity BuildManPower()
        {
            string pn = textBox_PersonalNumber.Text;
            string fn = textBox_FirstName.Text;
            string ln = textBox_LastName.Text;
            DateTime rd = dateTimePicker_ResignDate.Value;
            string rankLiteral = comboBox_rank.Text;
            bool inManPower = checkBox_InManPower.Checked;
            List<string> shiftIndexes = new List<string>();
            for (int i = 0; i < checkedListBox_Tasks.CheckedItems.Count; i++)
            {
                int modifiedIndex = checkedListBox_Tasks.Items.IndexOf(checkedListBox_Tasks.CheckedItems[i]);
                shiftIndexes.Add(shifts[modifiedIndex].ID);
            }

                ReserveDutyEntity temp;
            if (this.manpower != null)
            {
                temp = this.manpower;
            }
            else
            {
                temp = new ReserveDutyEntity();
                
            }
            temp.ID = pn;
            temp.firstName = fn;
            temp.lastName = ln;
            temp.resignmentDate = rd;
            temp.SetRankLiteral(rankLiteral);
            temp.inManPower = inManPower;
            temp.qualifiedShiftsIndexes = shiftIndexes.ToArray();
            return temp;
        }
        //private Assignation BuildAssgination(Assignation ass)
        //{
        //    ReserveDutyEntity man = manpower[comboBox_Soldier.SelectedIndex];
        //    string pn = textBox_PersonalNumber.Text;
        //    DateTime start = dateTimePicker_ResignDate.Value.Date;
        //    DateTime end = dateTimePicker_Endate.Value.Date;
        //    string shiftIndexes = "";

        //    for (int i = 0; i < checkedListBox_Tasks.CheckedItems.Count; i++)
        //    {
        //        int modifiedIndex = checkedListBox_Tasks.Items.IndexOf(checkedListBox_Tasks.CheckedItems[i]);
        //        if (i == 0)
        //            shiftIndexes += (man.qualifiedShiftsIndexes[modifiedIndex]);
        //        else
        //            shiftIndexes += (Program.metadata["MultifieldDeliminator"].ToString() + man.qualifiedShiftsIndexes[modifiedIndex]);


        //    }
        
        //    if(ass==null)
        //    {
        //      ass = new Assignation("זימון חדש", man, start, end, shiftIndexes, AssignationStatus.PendingApproval.ToString());
        //    }
        //    else
        //    {
        //        ass.startDate = start;
        //        ass.endDate = end;
        //        ass.shiftsIndexes = shiftIndexes.Split(new string[] { Program.metadata["MultifieldDeliminator"].ToString() },StringSplitOptions.RemoveEmptyEntries);
        //    }
        //    return ass;
        //}
        //private void BuildAssgination()
        //{
        //    ReserveDutyEntity man = manpower[comboBox_Soldier.SelectedIndex];
        //    string pn = textBox_PersonalNumber.Text;
        //    DateTime start = dateTimePicker_startDate.Value.Date;
        //    DateTime end = dateTimePicker_Endate.Value.Date;
        //    string shiftIndexes="";
 
        //    for (int i = 0; i < checkedListBox_Tasks.CheckedItems.Count; i++)
        //    {
        //        int modifiedIndex = checkedListBox_Tasks.Items.IndexOf(checkedListBox_Tasks.CheckedItems[i]);
        //            if (i==0)
        //                shiftIndexes+=(man.qualifiedShiftsIndexes[modifiedIndex]);
        //            else
        //                shiftIndexes += (Program.metadata["MultifieldDeliminator"].ToString()+man.qualifiedShiftsIndexes[modifiedIndex]);
       
                
        //    }
        //    AssignationStatus status = AssignationStatus.Approved;
        //    return (new Assignation("זימון חדש", pn, start, end, shiftIndexes, status.ToString()));
       

        //}
        private void button_Validate_Click(object sender, EventArgs e)
        {
        //    listBox_Log.Visible = false;
        //    listBox_Log.Items.Clear();
        //    Assignation temp= BuildAssgination(this.currentAssignation);

        //    AssignationStatus last = temp.status;
        //    temp.status = AssignationStatus.Approved;
        //    List<string> warnings = Program.f.ValidateAssignation(temp);
        //    if(warnings.Count>0)
        //    {
        //        listBox_Log.Visible = true;
        //        foreach (string item in warnings)
        //        {
        //            listBox_Log.Items.Add(item);
        //        }
        //    }
        //    temp.status = last; //Very cludgeye
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            //if(this.currentAssignation == null)
            //    this.currentAssignation = BuildAssgination();
            //else
            //{
            //    this.currentAssignation =   BuildAssgination(this.currentAssignation);
            //}
            this.manpower = BuildManPower();
            this.DialogResult = DialogResult.OK;
        }

        private void comboBox_rank_SelectedIndexChanged(object sender, EventArgs e)
        {
            Image temp = MainForm.rankImages[comboBox_rank.SelectedIndex];
            pictureBox_Rank.BackgroundImage = temp;//TODO ended here
        }
    }
}
