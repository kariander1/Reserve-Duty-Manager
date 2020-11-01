using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DarkDemo.Properties;
using System.Collections;
using System.Threading;
namespace DarkDemo
{
    public partial class LoginForm : Form
    {
        public ProgramUser currentUser { get; set; }
        List<ProgramUser> programUsers;
        List<string> fullnames;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        public LoginForm()
        {
            InitializeComponent();
            Application.DoEvents();
          
     //       dataGridView_Logs.FirstDisplayedScrollingColumnIndex = 0;
        }

        private void button_quit_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void LoadUsers()
        {
            programUsers = new List<ProgramUser>();
            AccessDB db = new AccessDB(Settings.Default.DBpath);
            if (!db.Connect(Program.DATABASE_PASSWORD))
            {
                DialogForm d = new DialogForm("כשל התחברות", "לא הצלחתי להתחבר למסד נתונים", false);
                pictureBox_image.BackgroundImage = Resources.sadFace;
                d.ShowDialog();
                return;
            }

            Program.metadata = new Hashtable();
            List<Hashtable> tempList = db.GetTable("Metadata");
            if (tempList == null)
            {
                DialogForm d = new DialogForm("כשל התחברות", "לא הצלחתי לקרוא את Metadata", false);
                pictureBox_image.BackgroundImage = Resources.sadFace;
                d.ShowDialog();
                return;
            }
            Program.metadata = tempList[0];
         
            List<string> autoComp = new List<string>();
            List<Hashtable> users = db.GetTable("ProgramUsers");
            fullnames = new List<string>();
            db.Disconnect();
            foreach (Hashtable user in users)
            {
                string id = user["ID"].ToString();
                string fname = user["FirstName"].ToString();
                string lname = user["LastName"].ToString();
                string position = user["Position"].ToString();
                string rank = user["Rank"].ToString();
                string comp = user["Compartment"].ToString();
                string shiftIndex = "0";
                try
                {
                    shiftIndex = user["TeamsInCharge"].ToString();
                }
                catch (Exception)
                {


                    db.Disconnect();


                }
                ProgramUser temp = new ProgramUser(fname, lname, id, position, rank, comp, shiftIndex);
                autoComp.Add(temp.fullName);
                fullnames.Add(temp.fullName);
                programUsers.Add(temp);
            }
            var auto = new AutoCompleteStringCollection();
            auto.AddRange(autoComp.ToArray());


            if (textBox_user.InvokeRequired)
            {
                textBox_user.Invoke(new Action(() => textBox_user.AutoCompleteCustomSource = auto));
            }
            else
            {
                textBox_user.AutoCompleteCustomSource = auto;
            }



            loadingCircle_Loading.Active = false;
            if (textBox_user.InvokeRequired)
            {
                textBox_user.Invoke(new Action(() => loadingCircle_Loading.Visible = false));
            }
            if (button_Yes.InvokeRequired)
            {
                button_Yes.Invoke(new Action(() => button_Yes.Visible = true));
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
        public bool LogInTrial()
        {
            pictureBox_image.BackgroundImage = Resources.sadFace;
            AccessDB db = new AccessDB(Settings.Default.DBpath);
            if (!db.Connect(Program.DATABASE_PASSWORD))
            {
                DialogForm d = new DialogForm("כשל התחברות", "לא הצלחתי להתחבר למסד נתונים", false);
                
                d.ShowDialog();
                return false;
            }
            int i = fullnames.IndexOf(textBox_user.Text);
            if(i==-1)
            {
                DialogForm d = new DialogForm("כשל התחברות", "שם משתמש לא קיים", false);
                d.ShowDialog();
                return false;
            }
            string ID = programUsers[i].personalNumber;
            string pass = textBox_pass.Text;
            
            List<Hashtable> tb = db.GetTableWithCriteria("ProgramUsers", new List<string>{ "ID" }, new List<string>{ ID });
            if (tb.Count == 0)
            {
                DialogForm d = new DialogForm("כשל התחברות", "שם משתמש לא קיים במסד נתונים", false);
                d.ShowDialog();
                return false;
            }
                string destp = tb[0]["Password"].ToString();
            if(pass ==destp)
            {
                pictureBox_image.BackgroundImage = Resources.happyface;
                currentUser = programUsers[i];
                return true;
            }
            else
            {
                DialogForm d = new DialogForm("כשל התחברות", "סיסמא לא נכונה", false);
                d.ShowDialog();
            }
         
            return false;

        }
        private void button_Yes_Click(object sender, EventArgs e)
        {
            if(!LogInTrial())
            {
                pictureBox_image.BackgroundImage = Resources.sadFace;
            }
            else
            {
                pictureBox_image.BackgroundImage = Resources.happyface;
                DialogResult = DialogResult.OK;
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            loadingCircle_Loading.Active = true;
            int duration = 1000;//in milliseconds
            int steps = 100;
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
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
            Thread loader = new Thread(LoadUsers);
            loader.Start();
           // LoadUsers();//TODO Make on different thread
        }
        private DialogResult dr;
        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
          
            if (Opacity != 0)
            {
                dr = DialogResult;
                e.Cancel = true;
                int duration = 100;//in milliseconds
                int steps = 10;
                System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
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
