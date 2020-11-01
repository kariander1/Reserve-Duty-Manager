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
    public partial class AddAssignation : Form
    {
        public Assignation currentAssignation { get; set; }
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        private List<ReserveDutyEntity> manpower { get; set; }
        private List<ShiftEntity> shifts { get; set; }
        public AddAssignation(List<ReserveDutyEntity> manPower,List<ShiftEntity> shifts)
        {
            InitializeComponent();
          
            this.manpower = manPower;
            this.shifts = shifts;
            PopulateComboBox();
        }
        public AddAssignation(List<ReserveDutyEntity> manPower, List<ShiftEntity> shifts, Assignation ass)
        {
            InitializeComponent();
            comboBox_Soldier.Enabled = false;
            textBox_PersonalNumber.Enabled = false;
           
            this.manpower = manPower;
            this.shifts = shifts;
           

            ReserveDutyEntity res = manPower.Where(pn => pn.ID == ass.reserverDutyEntity.ID).FirstOrDefault();
            PopulateComboBox(res);
            PopulateCheckboxlist(res,ass.shiftsIndexes);


            textBox_PersonalNumber.Text = ass.reserverDutyEntity.ID;
            dateTimePicker_startDate.Value = ass.startDate;
            dateTimePicker_Endate.Value = ass.endDate;
            currentAssignation = ass;
        }
        private void button_quit_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
        public void PopulateComboBox()
        {
            comboBox_Soldier.Items.Clear();
            foreach (ReserveDutyEntity item in this.manpower)
            {
                comboBox_Soldier.Items.Add(item.GetFullName());
            }
            if(comboBox_Soldier.Items.Count>0)
                comboBox_Soldier.SelectedIndex = 0;
        }
        public void PopulateComboBox(ReserveDutyEntity selected)
        {
            int selectIndex = 0;
            comboBox_Soldier.Items.Clear();
            foreach (ReserveDutyEntity item in this.manpower)
            {
                selectIndex = comboBox_Soldier.Items.Add(item.GetFullName());
                if (item.ID == selected.ID)
                    comboBox_Soldier.SelectedIndex = selectIndex;
            }      
        }
        public void PopulateCheckboxlist(ReserveDutyEntity man)
        {
            checkedListBox_Tasks.Items.Clear();
            foreach (string item in man.qualifiedShiftsIndexes)
            {
                ShiftEntity s = this.shifts.Where(sh => sh.ID == item).FirstOrDefault();
                if(s!=null)
                    checkedListBox_Tasks.Items.Add(s.shiftName);
                else
                    checkedListBox_Tasks.Items.Add("לא מזוהה");
            }
        }
        public void PopulateCheckboxlist(ReserveDutyEntity man,string[] shiftIndexes)
        {
            checkedListBox_Tasks.Items.Clear();
            foreach (string item in man.qualifiedShiftsIndexes)
            {
                ShiftEntity s = this.shifts.Where(sh => sh.ID == item).FirstOrDefault();
                int i;
              if(s==null)
                {
                    i = checkedListBox_Tasks.Items.Add("לא מזוהה");
                }
              else
                {
                    i = checkedListBox_Tasks.Items.Add(s.shiftName);
                }
       

                if (shiftIndexes.Contains(item))
                    checkedListBox_Tasks.SetItemChecked(i, true);
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

        private void comboBox_Soldier_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReserveDutyEntity man = manpower[comboBox_Soldier.SelectedIndex];
            textBox_PersonalNumber.Text = man.ID;
            dateTimePicker_startDate.Value = DateTime.Now.Date.AddDays(7);
            dateTimePicker_Endate.Value = DateTime.Now.Date.AddDays(7);
            PopulateCheckboxlist(man);
            button_add.Enabled = true;
            button_Validate.Enabled = true;

        }
        private Assignation BuildAssgination(Assignation ass)
        {
            ReserveDutyEntity man = manpower[comboBox_Soldier.SelectedIndex];
            string pn = textBox_PersonalNumber.Text;
            DateTime start = dateTimePicker_startDate.Value.Date;
            DateTime end = dateTimePicker_Endate.Value.Date;
            string shiftIndexes = "";

            for (int i = 0; i < checkedListBox_Tasks.CheckedItems.Count; i++)
            {
                int modifiedIndex = checkedListBox_Tasks.Items.IndexOf(checkedListBox_Tasks.CheckedItems[i]);
                if (i == 0)
                    shiftIndexes += (man.qualifiedShiftsIndexes[modifiedIndex]);
                else
                    shiftIndexes += (Program.metadata["MultifieldDeliminator"].ToString() + man.qualifiedShiftsIndexes[modifiedIndex]);


            }
        
            if(ass==null)
            {
              ass = new Assignation("זימון חדש", man, start, end, shiftIndexes, AssignationStatus.PendingApproval.ToString());
            }
            else
            {
                ass.startDate = start;
                ass.endDate = end;
                ass.shiftsIndexes = shiftIndexes.Split(new string[] { Program.metadata["MultifieldDeliminator"].ToString() },StringSplitOptions.RemoveEmptyEntries);
            }
            return ass;
        }
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
            listBox_Log.Visible = false;
            listBox_Log.Items.Clear();
            Assignation temp= BuildAssgination(this.currentAssignation);

            AssignationStatus last = temp.status;
            temp.status = AssignationStatus.Approved;
            List<string> warnings = Program.f.ValidateAssignation(temp);
            if(warnings.Count>0)
            {
                listBox_Log.Visible = true;
                foreach (string item in warnings)
                {
                    listBox_Log.Items.Add(item);
                }
            }
            temp.status = last; //Very cludgey
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            //if(this.currentAssignation == null)
            //    this.currentAssignation = BuildAssgination();
            //else
            //{
             this.currentAssignation =   BuildAssgination(this.currentAssignation);
            //}
            this.DialogResult = DialogResult.OK;
        }
    }
}
