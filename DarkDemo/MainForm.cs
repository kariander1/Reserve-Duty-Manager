using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DarkDemo.Properties;
using System.Threading;
using System.Drawing.Drawing2D;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms.DataVisualization.Charting;
using System.Globalization;
namespace DarkDemo
{
    public partial class MainForm : Form
    {
        
        private int _tabIndex;
        private bool _isLoading = false;
        const int MAX_WINDOW_WIDTH = 166;
        const int MIN_WINDOW_WIDTH = 109;
        const int SECONDARY_PANEL_MIN_HEIGHT = 221;
        const int SECONDARY_PANEL_MAX_HEIGHT = 320;
      
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public static List<Image> rankImages;
        private List<Assignation> assignationsEntities;
       // private List<Assignation> allAssignation;
        private List<ReserveDutyEntity> manPowerEntities;
        private List<ShiftEntity> shiftsEntities;
        private List<ShiftEntity> filteredShiftEntities;
        private Hashtable teams;
        public static AlarmForm alarmWindow;

        private static ProgramUser currentUser;

        public static Assignation selectedAssingation = null;
        public static ReserveDutyEntity selectedManpower = null;
        public static ShiftEntity selectedShift = null;

        System.Windows.Forms.Timer promptCheck;

        Color DEFAULT_BACKCOLOR = Color.FromArgb(255, 41, 44, 51);
        Color DEFAULT_FORECOLOR = Color.FromArgb(255, 62, 120, 138);
        Color QUAL_COLOR = Color.FromArgb(255, 49, 98, 34);
        Color HALF_QUAL_COLOR = Color.FromArgb(255, 98, 94, 34);
        Color NOT_QUAL_COLOR = Color.FromArgb(255, 132, 0, 0);
        Color ALMOST_NOT_QUAL_COLOR = Color.FromArgb(255, 176, 92, 30);
        Color FOR_REDUCTION_COLOR = Color.FromArgb(255, 0, 0, 0);
        Color BAR_IN_PROGRESS_COLOR = Color.FromArgb(255, 106, 236, 197);
        Color SHIFT_IN_PROGRESS_COLOR = Color.FromArgb(255, 136, 151, 240);
        Color SHIFT_DONE_COLOR = Color.FromArgb(255, 60, 84, 255);
        Color INFO = Color.FromArgb(255, 255, 255, 225);
        Color BUTTON_HIGHLIGHT_COLOR = Color.FromArgb(255,68,71,83);

      
        public MainForm(ProgramUser p)
        {
            InitializeComponent();
           
         
            alarmWindow = new AlarmForm();
            tabIndex = 0;
            currentUser = p; //TODO filter by user
            promptCheck = new System.Windows.Forms.Timer();
            promptCheck.Interval = 500;
            promptCheck.Tick += promterChecker;
            

            BuildControls();
           
       
            LoadDataBase();
            LoadImages();
            // listView_ManPower.Dock = DockStyle.Fill;

        }
        private void ClearStatistics()
        {
            chart_statistics.Series[0].Points.Clear();
        }
        private void LoadStatistics(DateTime start,DateTime end)
        {
    
          
            Axis XA = chart_statistics.ChartAreas[0].AxisX;
            Axis YA = chart_statistics.ChartAreas[0].AxisY;
            Series S1 = chart_statistics.Series[0];


           // int[] daysCount = new int[12];
            SortedDictionary<DateTime, int> dict = new SortedDictionary<DateTime, int>();
            SortedDictionary<string, int> byShifts = new SortedDictionary<string, int>();
            SortedDictionary<string, int> byTeams = new SortedDictionary<string, int>();
            foreach (Assignation item in assignationsEntities)
            {
                if (!(item.status == AssignationStatus.Executed || item.status == AssignationStatus.Executing || item.status == AssignationStatus.Approved))
                    continue;
                DateTime assStart = item.startDate;
                DateTime assEnd = item.endDate;
                DateTime temp = assStart;
                while(temp<=assEnd)
                {
                    if (temp >= start && temp <= end) //add data point
                    {
                        DateTime modifiedDate = temp.Date.AddDays(-temp.Day); 
                        //  daysCount[temp.Month - 1]++;
                        if (!dict.ContainsKey(modifiedDate))
                            dict.Add(modifiedDate, 1);
                        else
                            dict[modifiedDate]++;


                        foreach (string index in item.shiftsIndexes)
                        {
                            ShiftEntity s = ResolveShiftFromIndex(index);
                            if(!(byShifts.ContainsKey(s.shiftName)))
                            {
                                byShifts.Add(s.shiftName,1);
                            }
                            else
                            {
                                byShifts[s.shiftName]++;
                            }
                            string team = teams[int.Parse(s.teamIndex)].ToString();
                            if (!(byTeams.ContainsKey(team)))
                            {
                                byTeams.Add(team, 1);
                            }
                            else
                            {
                                byTeams[team]++;
                            }
                        }
                    }
                    temp = temp.AddDays(1);
                }
            }
            foreach (DateTime key in dict.Keys)
            {
            
                    string monthName = (key).ToString("MMM yyyy", CultureInfo.InvariantCulture);
                    if (chart_statistics.InvokeRequired)
                    {
                        chart_statistics.Invoke(new Action(() => S1.Points.AddXY(monthName, dict[key])));
                    }
                   else
                    {
                        S1.Points.AddXY(monthName, dict[key]);
                    }
                
            }
            foreach (string shift in byShifts.Keys)
            {
                if(chart_ByShift.InvokeRequired)
                {
                    
                    chart_ByShift.Invoke(new Action(() => chart_ByShift.Series[0].Points.AddXY(shift, byShifts[shift])));
                }
              else
                {
                    chart_ByShift.Series[0].Points.AddXY(shift, byShifts[shift]);
                }
            }
            foreach (string team in byTeams.Keys)
            {
                if (chart_ByTeams.InvokeRequired)
                {

                    chart_ByTeams.Invoke(new Action(() => chart_ByTeams.Series[0].Points.AddXY(team, byTeams[team])));
                }
                else
                {
                    chart_ByTeams.Series[0].Points.AddXY(team, byTeams[team]);
                }
            }
            //List<DateTime> dates = new List<DateTime>();
            //DateTime dt = DateTime.Now; 
           // for (int i = 1; i < 12; i++)
            //    dates.Add(new DateTime(dt.Year, i, 1));
            //foreach (DateTime d in dates)
            //    S1.Points.AddXY(d, rnd.Next(99) + 33);
            //foreach (var item in collection)
            //{

            //}
        }
        private void promterChecker(object sender, EventArgs e)
        {
            
            if (!isLoading)
            {
                Prompter();
                
            }
          
        }
        public void LoadImages()
        {
            rankImages = new List<Image>();
            foreach (Image im in imageList_Big.Images)
            {
                rankImages.Add(im);
            }
        }
        public void LoadDataBase()
        {
          
            isLoading = true;
            promptCheck.Enabled = true;
            shiftsEntities = new List<ShiftEntity>();
            manPowerEntities = new List<ReserveDutyEntity>();
            //allAssignation = new List<Assignation>();
            assignationsEntities = new List<Assignation>();
            loadingCircle_Loading.Active = true;
            Thread t = new Thread(DBloader);
            t.Start(); //TODO filter man power according to team
          
        }
        public void BuildControls()
        {
            dataGridView_manPower.Dock = DockStyle.Fill;
            dataGridView_Assignations.Dock = DockStyle.Fill;
            dataGridView_Shifts.Dock = DockStyle.Fill;

            dataGridView_assignationDetails.Rows.Add(new object[] { "מספר זימון" });
            dataGridView_assignationDetails.Rows.Add(new object[] { "שם חייל" });
            dataGridView_assignationDetails.Rows.Add(new object[] { "סטטוס" });

            dataGridView_ShiftStat.Rows.Add(new object[] { "מספר מוסמכים" });
            dataGridView_ShiftStat.Rows.Add(new object[] { "מספר כשירים" });
            dataGridView_ShiftStat.Rows.Add(new object[] { "אחוז כשירות" });
        }
        public void Prompter()
        {
            promptCheck.Enabled = false;
            ManPowerPrompter(manPowerEntities);
            AssignationsPrompter(assignationsEntities);
            AssignationsValidator(assignationsEntities);
            tabIndex = 0;
          
        }
        public void DBloader() //TODO add loading log
        {
            try
            {


                Application.DoEvents();
                //LoadMetaData();
                LoadTeams();
                LoadShiftsEntities();
                LoadManPower();
                LoadAssignations();
                LoadStatistics(DateTime.Now.AddYears(-1), DateTime.Now);
                //   LoadAssignations();
                isLoading = false;

            }
            catch (Exception e)
            {

                addToWarning("שגיאה בקריאת מסד נתונים: " + e.Message);
            }
            loadingCircle_Loading.Active = false;
        }

        public List<string> ValidateAssignation(Assignation assign)
        {
            List<string> warnings = new List<string>();

            if (assign.status == AssignationStatus.Canceled)
                return warnings;

            int remaningDaysForAlerting = int.Parse(Program.metadata["DaysForAlertingNotApproved"].ToString());
            int remainingDays = (assign.startDate - DateTime.Now.Date).Days;
            ReserveDutyEntity currentEntity = assign.reserverDutyEntity;
            if (assign.endDate < assign.startDate)
            {
                warnings.Add(" זימון מספר " + assign.ID + " עבור " + currentEntity.GetFullName() + "- תאריך סיום מוקדם מתאריך התחלה");

            }
            if (remainingDays <= remaningDaysForAlerting && assign.status == AssignationStatus.PendingApproval)
            {
                warnings.Add(" זימון מספר " + assign.ID + " עבור " + currentEntity.GetFullName() + "- טרם אושר");

            }

            if (remainingDays <= 0 && assign.status != AssignationStatus.Executing && assign.status != AssignationStatus.Executed)
            {
                warnings.Add(" זימון מספר " + assign.ID + " עבור " + currentEntity.GetFullName() + "- עבר את תאריך ההתחלה");

            }
            if (DateTime.Now.Date > assign.endDate && assign.status != AssignationStatus.Executed)
            {
                warnings.Add(" זימון מספר " + assign.ID + " עבור " + currentEntity.GetFullName() + "- עבר את תאריך הסוף");

            }
            foreach (string index in assign.shiftsIndexes)
            {
                if (!currentEntity.qualifiedShiftsIndexes.Contains(index))
                {
                    
                    warnings.Add(" זימון מספר " + assign.ID + " עבור " + currentEntity.GetFullName() + "- אינו מוסמך לבצע את משמרת עם מזהה " + (index));

                }
            }
            List<Assignation> futureAssignations = new List<Assignation>(currentEntity.Assignations);
            //   futureAssignations.Add(assign);
            List<Assignation> overlapping = assign.CheckForOverlappingAssignation(futureAssignations);

            foreach (Assignation item in overlapping)
            {
                warnings.Add(" זימון מספר " + assign.ID + " עבור " + currentEntity.GetFullName() + "- חופף עם זימון מספר " + item.ID);

            }
            return warnings;
        }
        public void AssignationsValidator(List<Assignation> assigns)
        {

            foreach (Assignation assign in assigns)
            {


                assign.comments = new List<string>();
                List<string> warnings = ValidateAssignation(assign);

                foreach (string warning in warnings)
                {
                    addToWarning(warning);
                    assign.AddComment(warning);
                }
                if (warnings.Count > 0)
                    assign.IsFaulty = true;
                else
                    assign.IsFaulty = false;

            }
        }
        public void UpdateShiftEntity(ShiftEntity shift)
        {
            AccessDB db = new AccessDB(Settings.Default.DBpath);
            if (!db.Connect(Program.DATABASE_PASSWORD))
            {
                addToWarning("Couldn't connect to database!");
                DialogForm d = new DialogForm("כשל התחברות", "לא הצלחתי להתחבר למסד נתונים", false);
                d.ShowDialog();
                return;
            }
            Hashtable shiftData = new Hashtable();
          
            shiftData["ShiftName"] = shift.shiftName;
            shiftData["QualificationDuration"] = shift.ValidityInDays;
            shiftData["TeamAttribution"] = int.Parse(shift.teamIndex);
            string exitString = null;
            string dialogString = "לא הוזן בהצלחה";

            if (shift.ID==null)
            {
                exitString = db.InsertIntoTable("Shifts", shiftData);
                if (exitString != null)
                {
                    dialogString = "משמרת "+shift.shiftName+" בעל מזהה " +exitString+ " הוזן בהצלחה!";
                    shift.ID = (exitString);
                    shiftsEntities.Add(shift);
                    filteredShiftEntities.Add(shift);
                }
            }
            else
            {
                if (db.UpdateTable("Shifts", shiftData, "ID", shift.ID))
                {
                    dialogString = shift.shiftName +" עודכן בהצלחה!";
                }
            }
            db.Disconnect();
            DialogForm dd = new DialogForm("הכנסת משמרת", dialogString, false);
            dd.ShowDialog();
        }
        public void UpdateManPower(ReserveDutyEntity man)
        {
            AccessDB db = new AccessDB(Settings.Default.DBpath);
            if (!db.Connect(Program.DATABASE_PASSWORD))
            {
                addToWarning("Couldn't connect to database!");
                DialogForm d = new DialogForm("כשל התחברות", "לא הצלחתי להתחבר למסד נתונים", false);
                d.ShowDialog();
                return;
            }
            Hashtable manData = new Hashtable();
            manData["ID"] = man.ID;
            manData["FirstName"] = man.firstName;
            manData["LastName"] = man.lastName;
            manData["Rank"] = man.GetRankLiteral();
            manData["DateOfResignment"] = man.resignmentDate.ToShortDateString();
            manData["InManPower"] = man.inManPower;
            manData["Qualifications"] = ShiftEntity.ShiftsToindexes(man.qualifiedShiftsIndexes);

            ReserveDutyEntity temp = this.manPowerEntities.Where(manp => manp.ID == man.ID).FirstOrDefault();
            string exitString=null;
            string dialogString = "לא הוזן בהצלחה";
            if (temp != null)
            {
                if (db.UpdateTable("ManPower", manData, "ID", man.ID))
                {
                    dialogString = "עודכן בהצלחה!";
                }
            }
            else
            {
                exitString = db.InsertIntoTable("ManPower", manData); 
                this.manPowerEntities.Add(man);
            }
            db.Disconnect();
            if (exitString != null)
            {
                dialogString = "הוזן בהצלחה!";
              
            }
            DialogForm dd = new DialogForm("הכנסת כח אדם", man.GetFullName()+ " "+dialogString, false);
            dd.ShowDialog();

        }
        public void ManPowerPrompter(List<ReserveDutyEntity> manPower)
        {
            List<ReserveDutyEntity> tempManpower = new List<ReserveDutyEntity>(manPower);
            int daysForReduction = int.Parse(Program.metadata["DaysForReduction"].ToString());
         
            foreach (ReserveDutyEntity man in tempManpower)
            {
                QualificationStatus stat = man.GetQualificationStatus();
                if (stat == QualificationStatus.ForReduction)
                {
                    PopulateSecondaryPanel(man);
                    tabIndex = 1;
                    Application.DoEvents();                    
                    string s = man.GetFullName() + " אינו כשיר כבר יותר מ"+ daysForReduction.ToString()+ " ימים\nהאם ברצונך להעביר אותו\\ה לרשימת גריעה?";
                    DialogForm d = new DialogForm(man.ID,s, true);
                    if(d.ShowDialog() == DialogResult.Yes)
                    {
                        man.SetQualificationStatus(QualificationStatus.Reduced);
                        man.inManPower = false;
                        UpdateManPower(man);
                    }
                }
                else if(!man.inManPower && (DateTime.Now.Date - man.GetLastReserveDutyDate()).Days < daysForReduction)
                {
                    PopulateSecondaryPanel(man);
                    tabIndex = 1;
                    Application.DoEvents();
                    string s = man.GetFullName() + " ביקר ב-"+ man.GetLastReserveDutyDate().ToShortDateString()+"\nהאם ברצונך להשיב אותו לרשימת השבצ\"ק?";
                    DialogForm d = new DialogForm(man.ID, s, true);
                    if (d.ShowDialog() == DialogResult.Yes)
                    {
                        // man.qualificationStatus = QualificationStatus.;
                        man.inManPower = true;
                        man.CalculateQulificationStatus();
                      
                        UpdateManPower(man);
                    }
                }
                List<string> tempIndexes = new List<string>(man.qualifiedShiftsIndexes);
                foreach (string shiftIndex in tempIndexes)
                {
                    if (ResolveShiftFromIndex(shiftIndex) == null)
                    {
                        PopulateSecondaryPanel(man);
                        tabIndex = 1;
                        Application.DoEvents();
                        string s = man.GetFullName()+ " מוסמך למשמרת לא מזוהה. מזהה " + shiftIndex + ".";
                        s += "האם ברצונך להסיר משמרת זו מההסמכות של " + man+"?";
                        DialogForm d = new DialogForm(man.ID, s, true);
                        if (d.ShowDialog() == DialogResult.Yes)
                        {
                            // man.qualificationStatus = QualificationStatus.;
                            man.RemoveIndex(shiftIndex);

                            UpdateManPower(man);
                        }
                    }
                }
            }
        }        
        public void ValidateAssignationShifts(Assignation assign)
        {
            foreach (string index in assign.shiftsIndexes)
            {
                ShiftEntity shift =  this.shiftsEntities.Where(sh => sh.ID == index).FirstOrDefault();
                if(shift==null)
                {
                    string s = "זימון מספר " + assign.ID + " לא מכיל את המשמרת בעל מזהה "+index;
                    s += "האם ברצונך לבטל את זימון " + assign.ID + " של " + assign.reserverDutyEntity.GetFullName() + "?\n";
                    s += "\nתאריך התחלה : " + assign.startDate.ToShortDateString();
                    s += "\nתאריך סיום : " + assign.endDate.ToShortDateString();//TODO finished here
                }
            }
        }
        public void AssignationsPrompter(List<Assignation> assigns)
        {
            List<Assignation> tempAssigns = new List<Assignation>(assigns);
            foreach (Assignation item in tempAssigns)
            {
                ReserveDutyEntity r =item.reserverDutyEntity;
                if (item.status == AssignationStatus.Approved || item.status == AssignationStatus.Executing)
                {
                    if (DateTime.Now.Date > item.endDate)
                    {
                        PopulateSecondaryPanel(item);
                        tabIndex = 3;
                        string s = "זימון מספר " + item.ID + " עבר את זמן הסיום.\n";
                        s += "האם ברצונך לסיים את זימון מספר " + item.ID + " של " + r.GetFullName() + "?\n";
                        s += "\nתאריך התחלה : " + item.startDate.ToShortDateString();
                        s += "\nתאריך סיום : " + item.endDate.ToShortDateString();

                        DialogForm d = new DialogForm("זימון מספר " + item.ID, s, true);
                        if (d.ShowDialog() == DialogResult.Yes)
                        {
                            item.status = AssignationStatus.Executed;
                            UpdateAssignation(item); 
                        }

                    }
                    else if(DateTime.Now.Date >= item.startDate && item.status == AssignationStatus.Approved)
                    {
                        PopulateSecondaryPanel(item);
                        tabIndex = 3;
                        string s = "זימון מספר " + item.ID + " עבר את זמן ההתחלה.\n";
                        s += "האם ברצונך להתחיל את זימון מספר " + item.ID + " של " + r.GetFullName() + "?\n";
                        s += "\nתאריך התחלה : " + item.startDate.ToShortDateString();
                        s += "\nתאריך סיום : " + item.endDate.ToShortDateString();
                        DialogForm d = new DialogForm("זימון מספר " + item.ID, s, true);
                        if (d.ShowDialog() == DialogResult.Yes)
                        {
                            item.status = AssignationStatus.Executing;
                            UpdateAssignation(item);
                        }
                    }

                }
                else if (DateTime.Now.Date > item.endDate && item.status == AssignationStatus.PendingApproval)
                {
                    PopulateSecondaryPanel(item);
                    tabIndex = 3;
                    string s = "זימון מספר " + item.ID + " לא אושר, ועבר זמן סיומו.\n";
                    s += "האם ברצונך למחוק את זימון מספר " + item.ID + " של " + r.GetFullName() + "?\n";
                    s += "\nתאריך התחלה : " + item.startDate.ToShortDateString();
                    s += "\nתאריך סיום : " + item.endDate.ToShortDateString();
                    DialogForm d = new DialogForm("זימון מספר " + item.ID, s, true);
                    if (d.ShowDialog() == DialogResult.Yes)
                    {
                        DeleteAssignation(item);
                    }
                }
                else
                {

                    int daysRemaining = (item.startDate - DateTime.Now.Date).Days;
                    int remaningDaysForAlerting = int.Parse(Program.metadata["DaysForAlertingNotApproved"].ToString());
                    if (daysRemaining <= remaningDaysForAlerting && item.status == AssignationStatus.PendingApproval)
                    {
                        PopulateSecondaryPanel(item);
                        tabIndex = 3;
                        string s = "זימון מספר " + item.ID + " טרם אושר. נותרו " + daysRemaining + " ימים להתחלתו.\n";
                        s += "האם ברצונך לאשר את זימון מספר " + item.ID + " של " + r.GetFullName() + "?\n";
                        s += "\nתאריך התחלה : " + item.startDate.ToShortDateString();
                        s += "\nתאריך סיום : " + item.endDate.ToShortDateString();
                        DialogForm d = new DialogForm("זימון מספר " + item.ID, s, true);
                        if (d.ShowDialog() == DialogResult.Yes)
                        {
                            item.status = AssignationStatus.Approved;
                            UpdateAssignation(item);
                        }
                    }
                }
                List<string> tempIndexes = new List<string>(item.shiftsIndexes);
                foreach (string shiftIndex in tempIndexes)
                {
                    if (ResolveShiftFromIndex(shiftIndex) == null)
                    {
                        PopulateSecondaryPanel(item);
                        tabIndex = 3;
                        string s = "זימון מספר " + item.ID + " עם משמרת לא מזוהה. מזהה " + shiftIndex+".";
                        s += "האם ברצונך להסיר משמרת זו מהזימון של " + r.GetFullName() + "?\n";
                        s += "\nתאריך התחלה : " + item.startDate.ToShortDateString();
                        s += "\nתאריך סיום : " + item.endDate.ToShortDateString();
                        DialogForm d = new DialogForm("זימון מספר " + item.ID, s, true);
                        if (d.ShowDialog() == DialogResult.Yes)
                        {
                            item.RemoveIndex(shiftIndex);
                            UpdateAssignation(item);
                        }
                    }
                }
            }
        }
        private Assignation ResolveAssignationFromID(string id)
        {
            return assignationsEntities.Where(man => man.ID == id).FirstOrDefault();
        }
        private ShiftEntity ResolveShiftFromIndex(string shiftIndex)
        {

            return shiftsEntities.Where(man => man.ID == shiftIndex).FirstOrDefault();
        }
        private ReserveDutyEntity ResolveManPowerFromPersonalNumber(string personalNumber)
        {
            return manPowerEntities.Where(man => man.ID == personalNumber).FirstOrDefault();
        }

        public void addToWarning(string text)
        {
            if (alarmWindow.Visible == false)
            {
                button_showAlarms.BackgroundImage = Resources.warning;
            }
            alarmWindow.addToLog(text);
        }
        private void button_quit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button_Dashboard_Click(object sender, EventArgs e)
        {
            tabIndex = 0;
        }
        public void minimizeMenu()
        {
            if (panel_options.InvokeRequired)
            {
                panel_options.Invoke(new Action(() => panel_options.Size = new Size(MIN_WINDOW_WIDTH, panel_options.Size.Height)));
            }
            else
                panel_options.Size = new Size(MIN_WINDOW_WIDTH, panel_options.Size.Height);
        }
        public void maximizeMenu()
        {
            if (panel_options.InvokeRequired)
            {
                panel_options.Invoke(new Action(() => panel_options.Size = new Size(MAX_WINDOW_WIDTH, panel_options.Size.Height)));
            }
            else
                panel_options.Size = new Size(MAX_WINDOW_WIDTH, panel_options.Size.Height);
        }
        private void button_ManPower_Click(object sender, EventArgs e)
        {
            tabIndex = 1;
        }
        private void button_Assignations_Click(object sender, EventArgs e)
        {
            tabIndex = 3;


        }
        public bool isLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                //if(!_isLoading)
                //{
                //    Prompter();
                //}
            }
        }
        public int tabIndex
        {
            get { return _tabIndex; }
            set
            {
                _tabIndex = value;
                dataGridView_manPower.Visible = false;
                dataGridView_Assignations.Visible = false;
                dataGridView_Shifts.Visible = false;
                label_Filter.Visible = false;
                panel_ManPower.Visible = false;
                panel_Assignations.Visible = false;
                panel_CurrentShift.Visible = false;
                panel_Statistics.Visible = false;
                textBox_Filter.Visible = false;
                checkBox_FilterReduced.Visible = false;
                checkBox_FilterDone.Visible = false;
                checkBox_FilterCanceled.Visible = false;


                chart_statistics.Visible = false;
                button_Plot.Visible = false;
                label_StartDate.Visible = false;
                label_EndDate.Visible = false;
                dateTimePicker_startDate.Visible = false;
                dateTimePicker_EndDate.Visible = false;


                panel_SecondaryPanel.Height = SECONDARY_PANEL_MIN_HEIGHT;
                if (_tabIndex == 0)
                {
                    maximizeMenu();

                    chart_statistics.Visible = true;
                    panel_Statistics.Visible = true;
                    button_Plot.Visible = true;
                    label_StartDate.Visible = true;
                    label_EndDate.Visible = true;
                    dateTimePicker_startDate.Visible = true;
                    dateTimePicker_EndDate.Visible = true;

                    panel_Statistics.Dock = DockStyle.Fill;
                }
                else if (_tabIndex == 1)
                {
                    minimizeMenu();
               
                    dataGridView_manPower.Visible = true;
                    label_Filter.Visible = true;
                    textBox_Filter.Visible = true;
                    panel_ManPower.Visible = true;
                    checkBox_FilterReduced.Visible = true;
                    panel_ManPower.Dock = DockStyle.Fill;
                    LoadManPower(manPowerEntities);
                    ClearManPowerSecondaryPanel();
                }
                else if (_tabIndex == 2)
                {
                    minimizeMenu();

                    dataGridView_Shifts.Visible = true;
                    label_Filter.Visible = true;
                    textBox_Filter.Visible = true;
                    panel_CurrentShift.Visible = true;
                    panel_CurrentShift.Dock = DockStyle.Fill;
                    LoadShifts(this.filteredShiftEntities);
                    ClearShiftSecondaryPanel();
                }
                else if (_tabIndex == 3)
                {
                    minimizeMenu();
                    dataGridView_Assignations.Visible = true;
                    panel_Assignations.Visible = true;
                    panel_Assignations.Dock = DockStyle.Fill;
                    label_Filter.Visible = true;
                    textBox_Filter.Visible = true;
                    checkBox_FilterDone.Visible = true;
                    checkBox_FilterCanceled.Visible = true;
                    LoadAssignations(assignationsEntities);
                    ClearAssignationSecondaryPanel();
                }
            }
        }

        #region DatabaseOperations
        private string ExportAssignation(Assignation assign)
        {
            newProgressBar_Prog.Value = 0;
            Application.DoEvents();
            AccessDB db = new AccessDB(Settings.Default.DBpath);
            if (!db.Connect(Program.DATABASE_PASSWORD))
            {
                addToWarning("Couldn't connect to database!");
                DialogForm d = new DialogForm("כשל התחברות", "לא הצלחתי להתחבר למסד נתונים", false);
                d.ShowDialog();
                return null;
            }
            ExcelForm ex = new ExcelForm(Settings.Default.AssignationsFormTemplatePath);
            newProgressBar_Prog.Value = 10;
            Application.DoEvents();
            ex.OpenWorkBook();
            ex.SetWorksheet(Program.metadata["ExcelWorkSheetName"].ToString());
            List<Hashtable> data = db.GetTable("ExportData");

            int count = 1;
            foreach (Hashtable variable in data)
            {
                bool apply = Convert.ToBoolean(variable["Applied"].ToString());
                if (!apply)
                    continue;
                string val = TranslateAssignationExportData(variable["VariableName"].ToString(), assign);
                int rowIndex = int.Parse(variable["RowIndex"].ToString());
                int columnIndex = int.Parse(variable["ColumnIndex"].ToString());

                ex.AppendCell(rowIndex, columnIndex, val);
                int modVal = (int)((10 * count / data.Count)*0.8 + 10);
                newProgressBar_Prog.Value = modVal;
                Application.DoEvents();
                count++;

            }

            string pathDest = Settings.Default.OutputFolderPath;
            string fileName = FormatExportName(assign, pathDest);
            ex.CopyWorkBook(pathDest, fileName);
            ex.Close();
            newProgressBar_Prog.Value = 100;
            Application.DoEvents();
            return pathDest + "\\" + fileName + Program.metadata["ExcelExtension"].ToString();
        }
       
        public void LoadShiftsEntities()
        {
            filteredShiftEntities = new List<ShiftEntity>();
            shiftsEntities = new List<ShiftEntity>();
            AccessDB db = new AccessDB(Settings.Default.DBpath);
            if (!db.Connect(Program.DATABASE_PASSWORD))
            {
                DialogForm d = new DialogForm("כשל התחברות","לא הצלחתי להתחבר למסד נתונים",false);
                d.ShowDialog();
              
                return;
            }
            List<Hashtable> shifts = db.GetTable("Shifts");
            foreach (Hashtable shift in shifts)
            {
                string id = shift["ID"].ToString();
                string name = shift["ShiftName"].ToString();
                int validity = int.Parse(shift["QualificationDuration"].ToString());
                string teamIndex = "0";
                try
                {
                    teamIndex = (shift["TeamAttribution"]).ToString();
                    if (teamIndex == "")
                        teamIndex = "0";
                }
                catch (Exception)
                {


                }
                ShiftEntity temp = new ShiftEntity(id, name, validity, teamIndex);
                    shiftsEntities.Add(temp);
                if (currentUser.teamsIndexes.Contains(teamIndex) || teamIndex == "0" || currentUser.teamsIndexes.Contains("0"))
                    filteredShiftEntities.Add(temp);
            }
        }
        public void LoadTeams()
        {
            teams = new Hashtable();
            AccessDB db = new AccessDB(Settings.Default.DBpath);
            if (!db.Connect(Program.DATABASE_PASSWORD))
            {
                DialogForm d = new DialogForm("כשל התחברות", "לא הצלחתי להתחבר למסד נתונים", false);
                d.ShowDialog();
                return;
            }
            List<Hashtable> teamsDB = db.GetTable("TeamEntities");
            foreach (Hashtable team in teamsDB)
            {
                teams[team["ID"]] = team["EntityName"];
            }
            teams[0] = "כולם";
            db.Disconnect();
        }
        public void LoadManPower()
        {
            manPowerEntities = new List<ReserveDutyEntity>();
            AccessDB db = new AccessDB(Settings.Default.DBpath);
            if (!db.Connect(Program.DATABASE_PASSWORD))
            {
                DialogForm d = new DialogForm("כשל התחברות", "לא הצלחתי להתחבר למסד נתונים", false);
                d.ShowDialog();
                return;
            }
            List<Hashtable> manPower = db.GetTable("ManPower");
            foreach (Hashtable man in manPower)
            {

                string firstName = man["FirstName"].ToString();
                string lastName = man["LastName"].ToString();
                string personalNumber = man["ID"].ToString();
                string rank = man["Rank"].ToString();
                DateTime resign = Convert.ToDateTime(man["DateOfResignment"].ToString());
                string shiftString = man["Qualifications"].ToString();
                bool inManPower = Convert.ToBoolean(man["InManPower"]);
                //  List<Assignation> manAssignations = new List<Assignation>();
                //  manAssignations = this.assignationsEntities.Where(pn => pn.PersonalNumber == personalNumber).ToList(); //TODO add later assignations
                ReserveDutyEntity temp = new ReserveDutyEntity(personalNumber, firstName, lastName, resign, rank, shiftString, inManPower);


                string[] teamIndexes = temp.GetTeamAffiliationIndexes(shiftsEntities);
                //   foreach (string tempindex in currentUser.teamsIndexes)
                //   {
                //      if (tempindex=="0" || teamIndexes.Contains(tempindex))
                //     {

                //   }
                //}
                foreach (string tempindex in currentUser.teamsIndexes)
                {
                    if (teamIndexes.Contains(tempindex))
                    {

                        manPowerEntities.Add(temp);
                        break;
                    }
                }
            }
            db.Disconnect();
            //    LoadManPower(manPowerEntities);
        }
        public void LoadAssignations()
        {

            assignationsEntities = new List<Assignation>();

            AccessDB db = new AccessDB(Settings.Default.DBpath);
            if (!db.Connect(Program.DATABASE_PASSWORD))
            {
                addToWarning("Couldn't connect to database!");
                DialogForm d = new DialogForm("כשל התחברות", "לא הצלחתי להתחבר למסד נתונים", false);
                d.ShowDialog();
                return;
            }

            List<Hashtable> assigns = new List<Hashtable>();
            assigns = db.GetTable("Assignations");
            foreach (Hashtable assign in assigns)
            {
          
                string ID = assign["ID"].ToString();
                string assignPersonalNumber = assign["PersonalNumber"].ToString();
                ReserveDutyEntity res = ResolveManPowerFromPersonalNumber(assignPersonalNumber);
                if (res == null) // If it wasn't added to the manpower
                    continue;
                DateTime start = Convert.ToDateTime(assign["StartDate"].ToString());
                DateTime end = Convert.ToDateTime(assign["EndDate"].ToString());
                string shiftsString = assign["ShiftsAssigned"].ToString();
                string statusString = assign["Status"].ToString();
             
                //allAssignation.Add(new Assignation(ID, assignPersonalNumber, start, end, shiftsString, statusString));
                //   ReserveDutyEntity man = ResolveManPowerFromPersonalNumber(ID);
                //   if(man!=null) //If the relevant reserve duty is in the current teams in charge
                Assignation temp = new Assignation(ID, res, start, end, shiftsString, statusString);
                    assignationsEntities.Add(temp);
                res.Assignations.Add(temp);
            }
            db.Disconnect();

        }
        //public void LoadMetaData()
        //{
        //    metadata = new Hashtable();
        //    AccessDB db = new AccessDB(Settings.Default.DBpath);
        //    if (!db.Connect(Program.DATABASE_PASSWORD))
        //    {
        //        DialogForm d = new DialogForm("כשל התחברות", "לא הצלחתי להתחבר למסד נתונים", false);
        //        d.ShowDialog();
        //        addToWarning("Couldn't connect to database!");
        //        return;
        //    }
        //    metadata = db.GetTable("Metadata")[0];
        //    db.Disconnect();
        //}
        public void AddAssignation(Assignation assign)
        {

            AccessDB db = new AccessDB(Settings.Default.DBpath);
            if (!db.Connect(Program.DATABASE_PASSWORD))
            {
                addToWarning("Couldn't connect to database!");
                DialogForm de = new DialogForm("כשל התחברות", "לא הצלחתי להתחבר למסד נתונים", false);
                de.ShowDialog();
                return;
            }

            Hashtable assignData = new Hashtable();
            assignData["PersonalNumber"] = assign.reserverDutyEntity.ID;
            assignData["StartDate"] = assign.startDate.ToShortDateString();
            assignData["EndDate"] = assign.endDate.ToShortDateString();
            assignData["ShiftsAssigned"] = ShiftEntity.ShiftsToindexes(assign.shiftsIndexes);
            assign.status = AssignationStatus.PendingApproval;
            assignData["Status"] = assign.status.ToString();
            string id = db.InsertIntoTable("Assignations", assignData);
            db.Disconnect();
            // db.UpdateTable("Assignations","ShiftsAssigned", ShiftEntity.ShiftsToindexes(assign.shiftsIndexes),"id",int.Parse(id));
            string text = "שגיאה בהכנסת זימון, פנה אל הלוגים.";
            if (id != null)
            {
                text = "זימון מספר " + id + " נכנס בהצלחה";
                assign.ID = id;
                this.assignationsEntities.Add(assign);
                //     List<Assignation> manAssignations = new List<Assignation>();
                //     manAssignations = this.assignationsEntities.Where(pn => pn.PersonalNumber == assign.PersonalNumber).ToList();
                //       ReserveDutyEntity man = ResolveManPowerFromPersonalNumber(assign.PersonalNumber);
                //     man.Assignations = manAssignations;
                assign.reserverDutyEntity.Assignations.Add(assign);
            }
            DialogForm d = new DialogForm("הכנסת זימון", text, false);
            d.ShowDialog();

        }
        public bool DeleteAssignation(Assignation assign)
        {
            AccessDB db = new AccessDB(Settings.Default.DBpath);
            if (!db.Connect(Program.DATABASE_PASSWORD))
            {
                addToWarning("Couldn't connect to database!");
                DialogForm d = new DialogForm("כשל התחברות", "לא הצלחתי להתחבר למסד נתונים", false);
                d.ShowDialog();
                return false;
            }
            int id = int.Parse(assign.ID);
            bool success = db.DeleteRecord("Assignations", "ID", id);
            db.Disconnect();

            this.assignationsEntities.Remove(assign);
            assign.reserverDutyEntity.Assignations.Remove(assign);
            //List<Assignation> manAssignations = new List<Assignation>();
            //manAssignations = this.assignationsEntities.Where(pn => pn.PersonalNumber == assign.PersonalNumber).ToList();
            //ReserveDutyEntity man = ResolveManPowerFromPersonalNumber(assign.PersonalNumber);
            //man.Assignations = manAssignations;

            return success;
        }
        public bool DeleteManPower(ReserveDutyEntity man)
        {
            AccessDB db = new AccessDB(Settings.Default.DBpath);
            if (!db.Connect(Program.DATABASE_PASSWORD))
            {
                addToWarning("Couldn't connect to database!");
                DialogForm d = new DialogForm("כשל התחברות", "לא הצלחתי להתחבר למסד נתונים", false);
                d.ShowDialog();
                return false;
            }
        
            bool success = db.DeleteRecord("ManPower", "ID", man.ID);
            success = success && db.DeleteRecord("Assignations", "PersonalNumber", man.ID);
            db.Disconnect();


            this.assignationsEntities.RemoveAll(assign => assign.reserverDutyEntity.ID == man.ID);
            this.manPowerEntities.Remove(man);
            
         //TODO remove also assignations?

            return success;
        }
        public bool DeleteShift(ShiftEntity shift)
        {
            AccessDB db = new AccessDB(Settings.Default.DBpath);
            if (!db.Connect(Program.DATABASE_PASSWORD))
            {
                addToWarning("Couldn't connect to database!");
                DialogForm d = new DialogForm("כשל התחברות", "לא הצלחתי להתחבר למסד נתונים", false);
                d.ShowDialog();
                return false;
            }

            bool success = db.DeleteRecord("Shifts", "ID",int.Parse(shift.ID));

           
            db.Disconnect();


            //  this.assignationsEntities.RemoveAll(assign => assign.reserverDutyEntity.ID == man.ID);
            //   this.manPowerEntities.Remove(man);
            if (success)
            {
                this.shiftsEntities.Remove(shift);
                this.filteredShiftEntities.Remove(shift);
            }
            //TODO remove also assignations?

            return success;
        }
        public void UpdateAssignation(Assignation assign)
        {
            AccessDB db = new AccessDB(Settings.Default.DBpath);
            if (!db.Connect(Program.DATABASE_PASSWORD))
            {
                addToWarning("Couldn't connect to database!");
                DialogForm d = new DialogForm("כשל התחברות", "לא הצלחתי להתחבר למסד נתונים", false);
                d.ShowDialog();
                return;
            }
            Hashtable assignData = new Hashtable();

            assignData["PersonalNumber"] = assign.reserverDutyEntity.ID;
            assignData["StartDate"] = assign.startDate.ToShortDateString();
            assignData["EndDate"] = assign.endDate.ToShortDateString();
            assignData["ShiftsAssigned"] = ShiftEntity.ShiftsToindexes(assign.shiftsIndexes);
            assignData["Status"] = assign.status.ToString();
            db.UpdateTable("Assignations", assignData, "ID", assign.ID);
            db.Disconnect();

         //   List<Assignation> manAssignations = new List<Assignation>();
          //  manAssignations = this.assignationsEntities.Where(pn => pn.PersonalNumber == assign.PersonalNumber).ToList();
          //  ReserveDutyEntity man = ResolveManPowerFromPersonalNumber(assign.PersonalNumber);
          //  man.Assignations = manAssignations;
        }
        private void LoadShifts(List<ShiftEntity> shifts)
        {
            selectedShift = null;
            dataGridView_Shifts.Rows.Clear();
            foreach (ShiftEntity temp in shifts)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.Cells.Add(new DataGridViewTextBoxCell { Value = temp.ID });
                row.Cells.Add(new DataGridViewTextBoxCell { Value = temp.shiftName });
                if(temp.ValidityInDays==0)
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = "תמיד" });
                else
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = temp.ValidityInDays + " ימים" });
                string teamName = "כולם";
                if(temp.teamIndex !="0")
                    teamName = teams[int.Parse(temp.teamIndex)].ToString();

                row.DefaultCellStyle.ForeColor = SystemColors.Info;
                double percentOfQual = temp.GetQualPercentage(manPowerEntities);
                if (percentOfQual.Equals(double.NaN))
                    percentOfQual = 0;
                row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Format("{0:0.00}%", percentOfQual) });
                double threshold = double.Parse(Program.metadata["QualPercentageForShift"].ToString());
             
                if (percentOfQual < threshold)
                {
                    addToWarning(temp.shiftName + " מתחת ל" + Program.metadata["QualPercentageForShift"].ToString() + " אחוז כשירות!");
                    row.DefaultCellStyle.BackColor = NOT_QUAL_COLOR;
                }
                else if (percentOfQual < (100-threshold)/4+threshold )
                {
                    row.DefaultCellStyle.BackColor = ALMOST_NOT_QUAL_COLOR;
                }
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = teamName });
                if (dataGridView_Shifts.InvokeRequired)
                {
                    dataGridView_Shifts.Invoke(new Action(() => dataGridView_Shifts.Rows.Add(row)));
                }
                else
                {
                    dataGridView_Shifts.Rows.Add(row);
                }
            }


            CallDefaultFilter();
            loadingCircle_Loading.Active = false;
        }
        #endregion
        private void LoadManPower(List<ReserveDutyEntity> list)
        {
            selectedManpower = null;
            dataGridView_manPower.Rows.Clear();
            foreach (ReserveDutyEntity temp in list)
            {

                //    ListViewItem row = new ListViewItem(temp.firstName, (int)(temp.rank));
                DataGridViewRow row = new DataGridViewRow();
                DataGridViewImageCell image = new DataGridViewImageCell();
                image.Value = imageList_Ranks.Images[(int)temp.rank];
                row.Cells.Add(image);

                row.Cells.Add(new DataGridViewTextBoxCell { Value = temp.firstName });
                row.Cells.Add(new DataGridViewTextBoxCell { Value = temp.lastName });
                row.Cells.Add(new DataGridViewTextBoxCell { Value = temp.ID });


                //    TimeSpan inactivity = DateTime.Now - lastDate;

                // row.DefaultCellStyle.ForeColor = Color.Black;
                QualificationStatus stat = temp.GetQualificationStatus();
                switch (stat)
                {
                    case QualificationStatus.Qualified:
                        row.DefaultCellStyle.BackColor = QUAL_COLOR;

                        break;
                    case QualificationStatus.HalfQualified:
                        row.DefaultCellStyle.BackColor = HALF_QUAL_COLOR;
                        break;
                    case QualificationStatus.AlmostNotQualified:
                        row.DefaultCellStyle.BackColor = ALMOST_NOT_QUAL_COLOR;
                        break;
                    case QualificationStatus.NotQualified:
                        row.DefaultCellStyle.BackColor = NOT_QUAL_COLOR;
                        break;
                    case QualificationStatus.ForReduction:
                        row.DefaultCellStyle.BackColor = FOR_REDUCTION_COLOR;
                        row.DefaultCellStyle.ForeColor = Color.White;
                        break;
                    case QualificationStatus.Reduced:
                        row.DefaultCellStyle.BackColor = DEFAULT_BACKCOLOR;
                        row.DefaultCellStyle.ForeColor = DEFAULT_FORECOLOR;
                        break;
                    case QualificationStatus.Unknown:
                        break;
                    default:
                        break;
                }



                DateTime lastDate = temp.GetLastReserveDutyDate();
                row.Cells.Add(new DataGridViewTextBoxCell { Value = lastDate.ToShortDateString() });
                row.Cells.Add(new DataGridViewTextBoxCell { Value = temp.GetQualificationStatusLiteral() });
                if (dataGridView_manPower.InvokeRequired)
                {
                    dataGridView_manPower.Invoke(new Action(() => dataGridView_manPower.Rows.Add(row)));
                }
                else
                {
                    dataGridView_manPower.Rows.Add(row);
                }
            }
            if(list.Count>0)
            {
                PopulateSecondaryPanel(list[0]);
            }
            //  loadingCircle_ManPower.Visible = false;
            CallDefaultFilter();
            loadingCircle_Loading.Active = false;
        }
        private void LoadAssignations(List<Assignation> list)
        {
            selectedAssingation = null;
            dataGridView_Assignations.Rows.Clear();
            foreach (Assignation assign in list)
            {
                DataGridViewRow row = new DataGridViewRow();
                //     ReserveDutyEntity man = ResolveManPowerFromPersonalNumber(assign.reserverDutyEntity.ID);
                ReserveDutyEntity man = assign.reserverDutyEntity;
                row.Cells.Add(new DataGridViewTextBoxCell { Value = assign.ID });
                row.Cells.Add(new DataGridViewTextBoxCell { Value = man.GetFullName() });
                row.Cells.Add(new DataGridViewTextBoxCell { Value = man.ID });
                row.Cells.Add(new DataGridViewTextBoxCell { Value = assign.startDate.ToShortDateString() });
                row.Cells.Add(new DataGridViewTextBoxCell { Value = assign.endDate.ToShortDateString() });
                row.Cells.Add(new DataGridViewTextBoxCell { Value = assign.GetStatusLiteral() });


                row.DefaultCellStyle.ForeColor = Color.White;
                switch (assign.status)
                {
                    case AssignationStatus.PendingApproval:
                        row.DefaultCellStyle.BackColor = HALF_QUAL_COLOR;

                        break;
                    case AssignationStatus.Approved:
                        row.DefaultCellStyle.BackColor = QUAL_COLOR;
                        break;
                    case AssignationStatus.Executing:
                        row.DefaultCellStyle.BackColor = SHIFT_IN_PROGRESS_COLOR;
                        break;
                    case AssignationStatus.Executed:
                        row.DefaultCellStyle.BackColor = SHIFT_DONE_COLOR;
                        break;
                    case AssignationStatus.Canceled:
                        row.DefaultCellStyle.BackColor = DEFAULT_BACKCOLOR;
                        // row.DefaultCellStyle.ForeColor = DEFAULT_FORECOLOR;
                        break;
                    case AssignationStatus.Unknown:
                        break;
                    default:
                        break;
                }
                if (assign.IsFaulty)
                    row.DefaultCellStyle.BackColor = NOT_QUAL_COLOR;

                dataGridView_Assignations.Rows.Add(row);
                CallDefaultFilter();
            }

        }
        public void CallDefaultFilter()
        {
            FilterDatagridView(this.GetVisibleDataGridView(), textBox_Filter.Text);
        }
        public void FilterDatagridView(DataGridView dgv, string filter)
        {

            foreach (DataGridViewRow row in dgv.Rows)
            {
                bool display = false;
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    if (checkBox_FilterReduced.Checked)
                    {
                        if (row.Cells[i].Value.ToString() == ReserveDutyEntity.GetQualificationStatusLiteral(QualificationStatus.Reduced))
                        {
                            display = false;
                            break;
                        }
                    }
                    if(checkBox_FilterDone.Checked)
                    {
                        if (row.Cells[i].Value.ToString() == Assignation.GetStatusLiteral(AssignationStatus.Executed))
                        {
                            display = false;
                            break;
                        }
                    }
                    if (checkBox_FilterCanceled.Checked)
                    {
                        if (row.Cells[i].Value.ToString() == Assignation.GetStatusLiteral(AssignationStatus.Canceled))
                        {
                            display = false;
                            break;
                        }
                    }
                    if (row.Cells[i].Value.ToString().Contains(filter))
                    {
                        display = true;
                       
                    }
                }
                if (!display)
                {
                    
                    row.Visible = false;
                }
                else
                {
                    row.Visible = true;
                }
            }
        }
        //public void FilterManPower(string filter)
        //{

        //    List<ReserveDutyEntity> filteredList = new List<ReserveDutyEntity>(manPowerEntities);
        //    foreach (DataGridViewRow row in dataGridView_manPower.Rows)
        //    {
        //        bool display = false;
        //        for (int i = 0; i < row.Cells.Count; i++)
        //        {

        //            if (row.Cells[i].Value.ToString().Contains(filter))
        //            {
        //                display = true;
        //                break;
        //            }
        //        }

        //        if (!display)
        //        {
        //            string itemID = row.Cells[3].Value.ToString();
        //            filteredList.RemoveAll(u => u.ID == itemID);
        //        }
        //    }
        //    dataGridView_manPower.Rows.Clear();
        //    LoadManPower(filteredList);
        //}


        private void listView_ManPower_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void listView_ManPower_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
        }


        private void textBox_Filter_TextChanged(object sender, EventArgs e)
        {
            timer_FilterTimer.Enabled = true;
            timer_FilterTimer.Stop();
            timer_FilterTimer.Start();

        }

        private void timer_FilterTimer_Tick(object sender, EventArgs e)
        {
            timer_FilterTimer.Stop();
            //  
            CallDefaultFilter();
            timer_FilterTimer.Enabled = false;
        }

        private void dataGridView_manPower_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex != -1)
            {
                // Color currentColor = dataGridView_manPower.Rows[e.RowIndex].DefaultCellStyle.BackColor;
                // Color newColor = Color.FromArgb(20, currentColor);
                // dataGridView_manPower.Rows[e.RowIndex].DefaultCellStyle.BackColor = newColor;//TODO fix colors
                ((DataGridView)(sender)).Rows[e.RowIndex].Selected = true;

            }
        }

        private void panel_MainPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void dataGridView_manPower_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            panel_SecondaryPanel.Height = SECONDARY_PANEL_MIN_HEIGHT;
            if (e.ColumnIndex >= 0)
            {
                string id;
                try
                {

                    id = dataGridView_manPower.Rows[e.RowIndex].Cells[3].Value.ToString(); //SOmetime it's changed to -1 if the user clicks quickly

                }
                catch (Exception)
                {

                    return;
                }
                foreach (ReserveDutyEntity man in manPowerEntities)
                {
                    if (man.ID.Equals(id))
                    {
                        selectedManpower = man;
                        PopulateSecondaryPanel(man);
                    }
                }
            }
        }
        private DataGridView GetVisibleDataGridView()
        {
            foreach (Control con in panel_MainPanel.Controls)
            {
                if (con is DataGridView)
                {
                    if (((DataGridView)con).Visible == true)
                        return ((DataGridView)con);
                }
            }
            return null;
        }
        private void ClearShiftSecondaryPanel()
        {
            dataGridView_QualifiedManPower.Rows.Clear();
            label_ShiftName.Visible = false;
            label_DeleteShift.Visible = false;
            button_DeleteShift.Visible = false;

            label_EditShift.Visible = false;
            button_EditShift.Visible = false;
        }
        private void ClearAssignationSecondaryPanel()
        {
            label_Approve.Visible = false;
            button_Approve.Visible = false;

            label_Export.Visible = false;
            button_Export.Visible = false;

            label_Cancel.Visible = false;
            button_Cancel.Visible = false;

            label_delete.Visible = false;
            button_Delete.Visible = false;

            label_EditASsign.Visible = false;
            button_EditAssign.Visible = false;

            label_QualStatusLiteral.Visible = false;
            dataGridView_assignationDetails.Visible = false;
            listBox_Taks.Items.Clear();
            listBox_Comments.Items.Clear();
        }
        private void PopulateSecondaryPanel(Assignation assign)
        {
            ClearAssignationSecondaryPanel();

            label_delete.Visible = true;
            button_Delete.Visible = true;

            label_EditASsign.Visible = true;
            button_EditAssign.Visible = true;

            label_Export.Visible = true;
            button_Export.Visible = true;

            label_QualStatusLiteral.Visible = true;
            dataGridView_assignationDetails.Visible = true;

            if (assign == null)
                return;
            //      ReserveDutyEntity man = ResolveManPowerFromPersonalNumber(assign.reserverDutyEntity.ID);
            ReserveDutyEntity man = assign.reserverDutyEntity;
            foreach (string shiftIndex in assign.shiftsIndexes)
            {
                ShiftEntity temp = ResolveShiftFromIndex(shiftIndex);
                if (temp != null)
                    listBox_Taks.Items.Add(temp.shiftName);
                else
                    addToWarning("משמרת בעל מזהה "+shiftIndex+" לא קיימת");
            }
            foreach (string comment in assign.comments)
            {
                listBox_Comments.Items.Add(comment);
            }


            dataGridView_assignationDetails.Rows[0].Cells[1].Value = int.Parse(assign.ID);
            dataGridView_assignationDetails.Rows[1].Cells[1].Value = man.GetFullName();
            dataGridView_assignationDetails.Rows[1].Cells[1].Style.ForeColor = SystemColors.Info;

            dataGridView_assignationDetails.Rows[2].Cells[1].Value = assign.GetStatusLiteral();

            switch (assign.status)
            {
                case AssignationStatus.PendingApproval:
                    label_Approve.Visible = true;
                    button_Approve.Visible = true;

                    label_Cancel.Visible = true;
                    button_Cancel.Visible = true;
                    break;
                case AssignationStatus.Approved:
                    label_Cancel.Visible = true;
                    button_Cancel.Visible = true;
                    break;
                case AssignationStatus.Executing:
                    label_Cancel.Visible = true;
                    button_Cancel.Visible = true;
                    break;
                case AssignationStatus.Executed:
                    break;
                case AssignationStatus.Canceled:
                    label_Approve.Visible = true;
                    button_Approve.Visible = true;
                    break;
                case AssignationStatus.Unknown:
                    break;
                default:
                    break;
            }
        }


        private void ClearManPowerSecondaryPanel()
        {
            label_DeleteManpower.Visible = false;
            button_deleteManPower.Visible = false;
            dataGridView_Shift.Rows.Clear();
        }
        private void PopulateSecondaryPanel(ShiftEntity shift)
        {
            dataGridView_QualifiedManPower.Rows.Clear();
            label_ShiftName.Text = shift.shiftName;
            label_DeleteShift.Visible = true;
            button_DeleteShift.Visible = true;

            label_EditShift.Visible = true;
            button_EditShift.Visible = true;

            //double numOfQual = 0;
           // double numOfQualAndRel = 0;
            foreach (ReserveDutyEntity man in manPowerEntities)
            {
                if (man.qualifiedShiftsIndexes.Contains(shift.ID))
                {
                  //  numOfQual++;
                    DateTime lastDate = man.GetLastDateOfShift(shift);
                    DateTime nextDate = man.GetNextDateOfShift(shift);
                    DateTime endOfQual = lastDate.AddDays(shift.ValidityInDays);
                    int daysRemaining = (endOfQual - DateTime.Now).Days;
                    if (shift.IsQualified(lastDate))
                    {
                      //  numOfQualAndRel++;
                    }
                    DataGridViewRow row = new DataGridViewRow();
                    row.Cells.Add(new DataGridViewTextBoxCell() { Value = man.GetFullName() });
                    if (lastDate == new DateTime())
                    {
                        row.Cells.Add(new DataGridViewTextBoxCell() { Value = "אין" });
                    }
                    else
                    {
                        row.Cells.Add(new DataGridViewTextBoxCell() { Value = lastDate.ToShortDateString() });
                    }
             
                    if (shift.ValidityInDays == 0)
                    {
                        row.Cells.Add(new DataGridViewProgressCell() { Value = 100, formattedString = "כשיר" });
                    }
                    else if (man.IsShiftBeingExectued(shift))
                    {
                        row.Cells.Add(new DataGridViewProgressCell() { Value = 100, formattedString = "בביצוע", BarColor = BAR_IN_PROGRESS_COLOR });
                    }
                    else if (daysRemaining < 0)
                    {
                        row.Cells.Add(new DataGridViewProgressCell() { Value = 0, formattedString = "לא כשיר" });
                      //  addToWarning(man.firstName + " " + man.lastName + " לא כשיר עבור \"" + shift.shiftName + "\""); ;
                    }
                    else
                    {
                        row.Cells.Add(new DataGridViewProgressCell() { Value = ((100 * daysRemaining) / shift.ValidityInDays), formattedString = "ימים " + daysRemaining });
                    }
                    if (lastDate == new DateTime() ||shift.ValidityInDays == 0)
                    {
                        row.Cells.Add(new DataGridViewTextBoxCell() { Value = "" });
                    }
                    else
                        row.Cells.Add(new DataGridViewTextBoxCell() { Value = endOfQual.ToShortDateString() });


                    if (nextDate == (new DateTime()))
                    {
                        row.Cells.Add(new DataGridViewTextBoxCell() { Value = "אין" });
                        
                    }
                    else
                    {

                        row.Cells[row.Cells.Add(new DataGridViewTextBoxCell() { Value = nextDate.ToShortDateString() })].Style.ForeColor = Color.FromName("Info");// TODO Implement transition to assignation secondary panel
                    }

                    dataGridView_QualifiedManPower.Rows.Add(row);
                }
             
            }
            dataGridView_ShiftStat.Rows[0].Cells[1].Value = shift.GetNumberOfQualified(manPowerEntities);
            dataGridView_ShiftStat.Rows[1].Cells[1].Value = shift.GetNumberOfQualifiedRelevant(manPowerEntities); ;
            double percentOfQual = shift.GetQualPercentage(manPowerEntities);
            if (percentOfQual.Equals(double.NaN))
                percentOfQual = 0;
            dataGridView_ShiftStat.Rows[2].Cells[1].Value = String.Format("{0:0.00}%", percentOfQual );
            if(percentOfQual<double.Parse(Program.metadata["QualPercentageForShift"].ToString()))
            {
                addToWarning(shift.shiftName + " מתחת ל" + Program.metadata["QualPercentageForShift"].ToString() + " אחוז כשירות!"); ;
            }
        }
        private void PopulateSecondaryPanel(ReserveDutyEntity man)
        {

            ClearManPowerSecondaryPanel();
            label_DeleteManpower.Visible = true;
            button_deleteManPower.Visible = true;

        
            label_Name.Text = man.firstName + " " + man.lastName;
            label_PN.Text = man.ID;
            label_rankLiteral.Text = man.GetRankLiteral();
            Bitmap im = new Bitmap(rankImages[(int)man.rank]);
         //   Image temp = rankImages[(int)man.rank];
            im.RotateFlip(RotateFlipType.Rotate90FlipNone);
            //     Bitmap newImage = ResizeImage(temp,temp.Width*3,temp.Height*3);
            label_QualStatusLiteral.Text = man.GetQualificationStatusLiteral();
            ovalPictureBox_rank.Image = im;


            List<ShiftEntity> currentShifts = man.GetQualifiedShifts(shiftsEntities);
            foreach (ShiftEntity shift in currentShifts)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = shift.shiftName }); // שם משמרת

                DateTime lastShift = man.GetLastDateOfShift(shift);
                DateTime endOfQual = lastShift.AddDays(shift.ValidityInDays);
                int daysRemaining = (endOfQual - DateTime.Now).Days;
                if (lastShift == new DateTime())
                {
                    row.Cells.Add(new DataGridViewTextBoxCell() { Value = "אין" });
                }
                else
                {
                    row.Cells.Add(new DataGridViewTextBoxCell() { Value = lastShift.ToShortDateString() });
                }
                DataGridViewProgressCell c = new DataGridViewProgressCell();
                if (shift.ValidityInDays == 0)
                {
                    row.Cells.Add(new DataGridViewProgressCell() { Value = 100, formattedString = "כשיר" });
                }
                else if (man.IsShiftBeingExectued(shift))
                {
                    row.Cells.Add(new DataGridViewProgressCell() { Value = 100, formattedString = "בביצוע", BarColor = BAR_IN_PROGRESS_COLOR });
                }
                else if (daysRemaining < 0)
                {
                    row.Cells.Add(new DataGridViewProgressCell() { Value = 0, formattedString = "לא כשיר" });
                    addToWarning(man.GetFullName() + " לא כשיר עבור \"" + shift.shiftName + "\""); ;
                }
                else
                {
                    row.Cells.Add(new DataGridViewProgressCell() { Value = ((100 * daysRemaining) / shift.ValidityInDays), formattedString = "ימים " + daysRemaining });
                }
                if (lastShift == new DateTime() || shift.ValidityInDays==0)
                {
                    row.Cells.Add(new DataGridViewTextBoxCell() { Value = "" });
                }
                else
                    row.Cells.Add(new DataGridViewTextBoxCell() { Value = endOfQual.ToShortDateString() });
                DateTime next = man.GetNextDateOfShift(shift);
                if (next == (new DateTime()))
                {
                    row.Cells.Add(new DataGridViewTextBoxCell() { Value = "אין" });
                    if (daysRemaining >= 0 && daysRemaining < int.Parse(Program.metadata["DayForAlertingNotQualified"].ToString()))
                    {
                        addToWarning(man.GetFullName() + " בעוד " + daysRemaining + " יוצא/ת מכשירות!");
                    }
                }
                else
                {

                    row.Cells[row.Cells.Add(new DataGridViewTextBoxCell() { Value = next.ToShortDateString() })].Style.ForeColor = Color.FromName("Info");// TODO Implement transition to assignation secondary panel
                }
                dataGridView_Shift.Rows.Add(row);
            }
        }

        private void panel_SecondaryPanel_MouseClick(object sender, MouseEventArgs e)
        {
            //  panel_SecondaryPanel.Height = SECONDARY_PANEL_MAX_HEIGHT;
        }

        private void dataGridView_Shift_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            panel_SecondaryPanel.Height = SECONDARY_PANEL_MAX_HEIGHT;
        }

        private void dataGridView_Shift_Click(object sender, EventArgs e)
        {
            panel_SecondaryPanel.Height = SECONDARY_PANEL_MAX_HEIGHT;
        }

        private void button_showAlarms_Click(object sender, EventArgs e)
        {
            button_showAlarms.BackgroundImage = Resources.sad;
            alarmWindow.Show();
            alarmWindow.Focus();
        }

        private void button_Reload_Click(object sender, EventArgs e)
        {
            ClearStatistics();
            LoadDataBase();
        }

        
        private void dataGridView_Assignations_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0)
            {
                string assignId;
                try
                {

                    assignId = dataGridView_Assignations.Rows[e.RowIndex].Cells[0].Value.ToString(); //SOmetime it's changed to -1 if the user clicks quickly

                }
                catch (Exception)
                {

                    return;
                }
                selectedAssingation = ResolveAssignationFromID(assignId);
                PopulateSecondaryPanel(selectedAssingation);

            }
        }

        private void CreateAssignationForm(Assignation ass)
        {

        }
        private void button_AddAssignation_Click(object sender, EventArgs e)
        {
            AddAssignation ass = new AddAssignation(this.manPowerEntities, this.shiftsEntities);
            if (ass.ShowDialog() == DialogResult.OK)
            {
                CreateAssignationForm(ass.currentAssignation);
                AddAssignation(ass.currentAssignation);
                AssignationsValidator(this.assignationsEntities);
                LoadAssignations(assignationsEntities);
            }
        }

        private void button_Approve_Click(object sender, EventArgs e)
        {
            if (selectedAssingation != null)
            {
                selectedAssingation.status = AssignationStatus.Approved;
                UpdateAssignation(selectedAssingation);
                AssignationsValidator(this.assignationsEntities);
                LoadAssignations(assignationsEntities);
            }
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            if (selectedAssingation != null)
            {
                selectedAssingation.status = AssignationStatus.Canceled;
                UpdateAssignation(selectedAssingation);
                AssignationsValidator(this.assignationsEntities);
                LoadAssignations(assignationsEntities);
            }
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            if (selectedAssingation != null)
            {
                if (DeleteAssignation(selectedAssingation))
                {
                    AssignationsValidator(this.assignationsEntities);
                    LoadAssignations(assignationsEntities);
                }
            }
        }

        private void dataGridView_assignationDetails_SelectionChanged(object sender, EventArgs e)
        {
            // if (dataGridView_assignationDetails.Columns[dataGridView_assignationDetails.CurrentCell.ColumnIndex].Name == mySpecifiedColumn.Name)
            dataGridView_assignationDetails.CurrentCell.Selected = false;
        }

        private void dataGridView_assignationDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == 1 && e.ColumnIndex == 1)
            {

                //   Assignation a = ResolveAssignationFromID(dataGridView_assignationDetails[1,1].Value.ToString());
                //    ReserveDutyEntity man = ResolveManPowerFromPersonalNumber(selectedAssingation.reserverDutyEntity.ID);
                ReserveDutyEntity man = selectedAssingation.reserverDutyEntity;
                PopulateSecondaryPanel(man);
                tabIndex = 1;
            }
        }

        private void dataGridView_assignationDetails_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == 1 && e.ColumnIndex == 1)
            {
                dataGridView_assignationDetails[1, 1].Style.BackColor =BUTTON_HIGHLIGHT_COLOR;
            }
        }

        private void dataGridView_assignationDetails_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == 1 && e.ColumnIndex == 1)
            {
                dataGridView_assignationDetails[1, 1].Style.BackColor = DEFAULT_BACKCOLOR;
            }
        }

        private void dataGridView_Shift_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView_Shift.CurrentCell.Selected = false;
        }

        private void button_EditAssign_Click(object sender, EventArgs e)
        {
            AddAssignation ass = new AddAssignation(this.manPowerEntities,this.shiftsEntities, selectedAssingation);
            if (ass.ShowDialog() == DialogResult.OK)
            {
                CreateAssignationForm(ass.currentAssignation);
                UpdateAssignation(ass.currentAssignation);
                AssignationsValidator(this.assignationsEntities);
                LoadAssignations(assignationsEntities);
            }
        }

        private void button_Export_Click(object sender, EventArgs e)
        {
            newProgressBar_Prog.Visible = true;
            string destPath = ExportAssignation(selectedAssingation);
         
            if (destPath != null)
            {
                DialogForm d = new DialogForm("הצלחה","זימון מספר "+selectedAssingation.ID+" יוצא בהצלחה\n האם ברצונך לצפות בו?",true);
                if (d.ShowDialog() == DialogResult.Yes)
                {
                    Process.Start(destPath);
                }
            }
            else
            {
                DialogForm d = new DialogForm("כישלון", "כישלון בייצוא זימון", false);
                d.ShowDialog();
            }
            newProgressBar_Prog.Visible = false;
        }
  
        private string FormatExportName(Assignation assign, string pathDest)
        {
            ReserveDutyEntity man = assign.reserverDutyEntity;
            string name = assign.ID+"_זימון_" + man.firstName + "_" + man.lastName + "_" + man.ID + "_" + assign.startDate.ToShortDateString().Replace('/', '-') + "_" + assign.endDate.ToShortDateString().Replace('/', '-');
            int index=0;
            string extension = Program.metadata["ExcelExtension"].ToString();
            string modPath = pathDest + "\\" + name+ extension;
            bool exists = false;
            while (File.Exists(modPath))
            {
                exists = true;
                index++;
                modPath = pathDest + "\\" + name+"_"+index+ extension;
            }
            if (exists)
                name += "_" + index;

            return name;
        }
        private string TranslateAssignationExportData(string variableName, Assignation assign)
        {
            ReserveDutyEntity man = assign.reserverDutyEntity;
            switch (variableName)
            {
                case "ReleaseDate": return man.resignmentDate.ToShortDateString();
                case "UnitName": return Program.metadata["AssignationDestination"].ToString(); //TODO get list
                case "Department":return currentUser.department;
                case "AssignPurpose": return "כשירות"; //TODO get list
                case "ReserveFirstName": return man.firstName;
                case "ReserveLastName": return man.lastName;
                case "ReservePersonalNumber": return man.ID;
                case "NumberOfDays": return assign.reserveDays.ToString();
                case "Rank": return man.GetRankLiteral();
                case "Destination": return Program.metadata["AssignationUnit"].ToString(); //TODO get list
                case "CurrentDate": return DateTime.Now.ToShortDateString();
                case "AssignationNumber": return assign.ID;
                case "SummonerPersonalNumber": return currentUser.personalNumber;
                case "SummonerFirstName": return currentUser.firstName;
                case "SummonerLastName": return currentUser.lastName;
                case "SummonerRank": return currentUser.rankLiteral;
                case "SummonerPosition": return currentUser.position;
                case "AssignationStatus": return assign.GetStatusLiteral();
                case "StartDate": return assign.startDate.ToShortDateString();
                case "EndDate": return assign.endDate.ToShortDateString();
                default: return "לא ידוע";
            }
        }

        private void checkBox_FilterReduced_CheckedChanged(object sender, EventArgs e)
        {
            CallDefaultFilter();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
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
       
        }
        private void button_AddShift_Click(object sender, EventArgs e)
        {
            Hashtable temp = new Hashtable(teams);
            if (!currentUser.teamsIndexes.Contains("0"))
            {
                foreach (object teamIndex in teams.Keys)
                {
                    if (!currentUser.teamsIndexes.Contains(teamIndex.ToString()))
                    {
                        temp.Remove(teamIndex);
                    }
                }
            }
            AddShift ad = new AddShift(temp);
            if (ad.ShowDialog() == DialogResult.OK)
            {
                UpdateShiftEntity(ad.currentShift);
                LoadShifts(this.shiftsEntities);
            }
        }
        private void button_DeleteShift_Click(object sender, EventArgs e)
        {
            if (selectedShift == null)
                return;
            if (new DialogForm("מחיקת משמרת", "האם אתה בטוח שברצונך למחוק את " + selectedShift.shiftName + "?", true).ShowDialog() == DialogResult.Yes)
            {
                string text = selectedShift.shiftName + " לא נמחק בהצלחה";
                if (DeleteShift(selectedShift))
                {
                    text = selectedShift.shiftName + " נמחק בהצלחה";
                }
                new DialogForm("מחיקת משמרת", text, false).ShowDialog();
                LoadShifts(this.shiftsEntities);
            }
        }
        private void button_EditShift_Click(object sender, EventArgs e)
        {
            Hashtable temp = new Hashtable(teams);
            if (!currentUser.teamsIndexes.Contains("0"))
            {
                foreach (object teamIndex in teams.Keys)
                {
                    if (!currentUser.teamsIndexes.Contains(teamIndex.ToString()))
                    {
                        temp.Remove(teamIndex);
                    }
                }
            }
            AddShift ad = new AddShift(temp,selectedShift);
            if (ad.ShowDialog() == DialogResult.OK)
            {
                UpdateShiftEntity(ad.currentShift);
                LoadShifts(this.shiftsEntities);
            }
        }
        private void button_AddManPower_Click(object sender, EventArgs e)
        {
            AddManpower ad = new AddManpower(filteredShiftEntities);
            if(ad.ShowDialog() == DialogResult.OK)
            {
                UpdateManPower(ad.manpower);
                LoadManPower(this.manPowerEntities);
               // LoadManPower();
            }
        }

        private void button_deleteManPower_Click(object sender, EventArgs e)
        {
            if (selectedManpower == null)
                return;
            if (new DialogForm("מחיקת כח אדם","האם אתה בטוח שברצונך למחוק את "+selectedManpower.GetFullName()+"?\nפעולה זו תביא למחיקת כל הזימונים של אותו מילואימניק",true).ShowDialog() == DialogResult.Yes)
            {
                string text = selectedManpower.GetFullName() + " לא נמחק בהצלחה";
                if(DeleteManPower(selectedManpower))
                {
                    text = selectedManpower.GetFullName() + " נמחק בהצלחה";
                }
                new DialogForm("מחיקת כח אדם",text,false).ShowDialog();
                LoadManPower(this.manPowerEntities);
            }
        }

        private void button_EditManPower_Click(object sender, EventArgs e)
        {
            if (selectedManpower == null)
                return;
            AddManpower ad = new AddManpower(selectedManpower, filteredShiftEntities);
            if (ad.ShowDialog() == DialogResult.OK)
            {
                UpdateManPower(ad.manpower);
                LoadManPower(this.manPowerEntities);
                // LoadManPower();
            }
        }

        private void button_ShiftsPanel_Click(object sender, EventArgs e)
        {
            tabIndex = 2;
        }

        private void dataGridView_Shifts_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                // Color currentColor = dataGridView_manPower.Rows[e.RowIndex].DefaultCellStyle.BackColor;
                // Color newColor = Color.FromArgb(20, currentColor);
                // dataGridView_manPower.Rows[e.RowIndex].DefaultCellStyle.BackColor = newColor;//TODO fix colors
                ((DataGridView)(sender)).Rows[e.RowIndex].Selected = true;

            }
        }

        private void dataGridView_Shifts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0)
            {
                string shiftId;
                try
                {

                    shiftId = dataGridView_Shifts.Rows[e.RowIndex].Cells[0].Value.ToString(); //SOmetime it's changed to -1 if the user clicks quickly

                }
                catch (Exception)
                {

                    return;
                }
                selectedShift = ResolveShiftFromIndex(shiftId);
                if(selectedShift!=null)
                    PopulateSecondaryPanel(selectedShift);

            }
        }

        private void dataGridView_ShiftStat_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView_ShiftStat.ClearSelection();
        }

        private void button_Plot_Click(object sender, EventArgs e)
        {
            ClearStatistics();
            LoadStatistics(dateTimePicker_startDate.Value, dateTimePicker_EndDate.Value);
        }
        Point? prevPosition = null;
        ToolTip tooltip = new ToolTip();

        private void chart_statistics_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.Location;
            if (prevPosition.HasValue && pos == prevPosition.Value)
                return;
            tooltip.RemoveAll();
            prevPosition = pos;
            var results = chart_statistics.HitTest(pos.X, pos.Y, false,
                                            ChartElementType.DataPoint);
            foreach (var result in results)
            {
                if (result.ChartElementType == ChartElementType.DataPoint)
                {
                    var prop = result.Object as DataPoint;
                    if (prop != null)
                    {
                        var pointXPixel = result.ChartArea.AxisX.ValueToPixelPosition(prop.XValue);
                        var pointYPixel = result.ChartArea.AxisY.ValueToPixelPosition(prop.YValues[0]);

                        // check if the cursor is really close to the point (10 pixels around the point)
                        if (Math.Abs(pos.X - pointXPixel) < 10 &&
                            Math.Abs(pos.Y - pointYPixel) < 10)
                        {
                            tooltip.Show("X=" + prop.XValue + "\nDays=" + prop.YValues[0], this.chart_statistics,
                                            pos.X, pos.Y - 15);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        //public static Bitmap ResizeImage(Image image, int width, int height)
        //{
        //    var destRect = new Rectangle(0, 0, width, height);
        //    var destImage = new Bitmap(width, height);

        //    destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

        //    using (var graphics = Graphics.FromImage(destImage))
        //    {
        //        graphics.CompositingMode = CompositingMode.SourceCopy;
        //        graphics.CompositingQuality = CompositingQuality.HighQuality;
        //        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //        graphics.SmoothingMode = SmoothingMode.HighQuality;
        //        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

        //        //using (var wrapMode = new ImageAttributes())
        //        //{
        //        //    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
        //        //    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
        //        //}
        //    }

        //    return destImage;
        //}
    }
    class OvalPictureBox : PictureBox
        {
            public OvalPictureBox()
            {
                this.BackColor = Color.DarkGray;
            }
            protected override void OnResize(EventArgs e)
            {
                base.OnResize(e);
                using (var gp = new GraphicsPath())
                {
                    gp.AddEllipse(new Rectangle(0, 0, this.Width - 1, this.Height - 1));
                    this.Region = new Region(gp);
                }
            }
        }
        public class NewProgressBar : ProgressBar
        {
            public NewProgressBar()
            {
                this.SetStyle(ControlStyles.UserPaint, true);
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                Rectangle rec = e.ClipRectangle;

                rec.Width = (int)(rec.Width * ((double)Value / Maximum)) - 4;
                if (ProgressBarRenderer.IsSupported)
                    ProgressBarRenderer.DrawHorizontalBar(e.Graphics, e.ClipRectangle);
                rec.Height = rec.Height - 4;
                e.Graphics.FillRectangle(Brushes.Red, 2, 2, rec.Width, rec.Height);
            }
        }
        public class DataGridViewProgressColumn : DataGridViewImageColumn
        {
            public DataGridViewProgressColumn()
            {
                CellTemplate = new DataGridViewProgressCell();
            }
        }
        class DataGridViewProgressCell : DataGridViewImageCell
        {
            // Used to make custom cell consistent with a DataGridViewImageCell
            public Color BarColor = Color.FromArgb(203, 235, 108);
            public string formattedString { get; set; }
            static Image emptyImage;
            static DataGridViewProgressCell()
            {
                emptyImage = new Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            }
            public DataGridViewProgressCell()
            {
                this.ValueType = typeof(int);
            }
            // Method required to make the Progress Cell consistent with the default Image Cell. 
            // The default Image Cell assumes an Image as a value, although the value of the Progress Cell is an int.
            protected override object GetFormattedValue(object value,
                                int rowIndex, ref DataGridViewCellStyle cellStyle,
                                TypeConverter valueTypeConverter,
                                TypeConverter formattedValueTypeConverter,
                                DataGridViewDataErrorContexts context)
            {
                return emptyImage;
            }
            protected override void Paint(System.Drawing.Graphics g, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
            {
                try
                {
                    int progressVal = (int)value;
                    float percentage = ((float)progressVal / 100.0f); // Need to convert to float before division; otherwise C# returns int which is 0 for anything but 100%.
                    Brush backColorBrush = new SolidBrush(cellStyle.BackColor);
                    Brush foreColorBrush = new SolidBrush(cellStyle.ForeColor);
                    // Draws the cell grid
                    base.Paint(g, clipBounds, cellBounds,
                     rowIndex, cellState, value, formattedValue, errorText,
                     cellStyle, advancedBorderStyle, (paintParts & ~DataGridViewPaintParts.ContentForeground));
                    string valueToDisplay;
                    if (this.formattedString == null)
                        valueToDisplay = progressVal.ToString() + "%";
                    else
                        valueToDisplay = this.formattedString;
                    if (percentage > 0.0)
                    {
                        // Draw the progress bar and the text
                        g.FillRectangle(new SolidBrush(BarColor), cellBounds.X + 2, cellBounds.Y + 2, Convert.ToInt32((percentage * cellBounds.Width - 4)), cellBounds.Height - 4);
                        g.DrawString(valueToDisplay, cellStyle.Font, foreColorBrush, cellBounds.X + (cellBounds.Width / 2) - 5, cellBounds.Y + 2);

                    }
                    else
                    {
                        // draw the text
                        if (this.DataGridView.CurrentRow.Index == rowIndex)
                            g.DrawString(valueToDisplay, cellStyle.Font, new SolidBrush(cellStyle.SelectionForeColor), cellBounds.X + 6, cellBounds.Y + 2);
                        else
                            g.DrawString(valueToDisplay, cellStyle.Font, foreColorBrush, cellBounds.X + 6, cellBounds.Y + 2);
                    }
                }
                catch (Exception e) { }

            }

        }

    }


