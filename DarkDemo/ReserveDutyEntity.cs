using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DarkDemo
{
    public class ReserveDutyEntity
    {
        public string ID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

        public DateTime resignmentDate { get; set; }
        private string rankLiteral { get; set; }
        public bool inManPower { get; set; }
        public  Rank rank { get; set; }
        public List<Assignation> Assignations { get; set; }
        private QualificationStatus qualificationStatus;
        public string[] qualifiedShiftsIndexes { get; set; }
        public ReserveDutyEntity(string id,string firstName,string lastName,DateTime resignment,string rank,string shiftsString,bool inManPower)
        {
            this.ID = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.resignmentDate = resignment;
            SetRankLiteral(rank);
            this.qualifiedShiftsIndexes = MultiValuedParser(shiftsString);
            this.Assignations = new List<Assignation>();
            this.inManPower = inManPower;
           // this.qualificationStatus = CalculateQulificationStatus();
        }
        public void SetRankLiteral(string rank)
        {
            this.rankLiteral = rank;
            this.rank = TranslateRankLiteral(rank);
        }
        public ReserveDutyEntity()
        {
            this.Assignations = new List<Assignation>();
        }
        public string GetFullName()
        {
            return this.firstName + " " + this.lastName;
        }
        public DateTime GetLastDateOfShift(ShiftEntity shift)
        {
            DateTime max = new DateTime();
            foreach (Assignation assign in this.Assignations)
            {
                if(assign.IsPartOfShift(shift.ID)&& assign.status == AssignationStatus.Executed)
                {
                    if (assign.endDate > max)
                        max = assign.endDate;
                }
            }
            return max;
        }
        public bool IsShiftBeingExectued(ShiftEntity shift)
        {
            
            foreach (Assignation assign in this.Assignations)
            {
                if (assign.IsPartOfShift(shift.ID) && assign.status == AssignationStatus.Executing)
                {
                    if (DateTime.Now.Date >= assign.startDate && DateTime.Now.Date <= assign.endDate)
                        return true;
                }
            }
            return false;
        }
        public DateTime GetNextDateOfShift()
        {
            foreach (Assignation assign in this.Assignations)
            {
                if ((assign.status == AssignationStatus.Approved || assign.status == AssignationStatus.Executing) && assign.startDate >= DateTime.Now.Date)
                    return assign.startDate;
            }
            return new DateTime();
        }
        public DateTime GetNextDateOfShift(ShiftEntity shift)
        {
            DateTime min = new DateTime();
            foreach (Assignation assign in this.Assignations)
            {
                if (assign.IsPartOfShift(shift.ID) && (assign.status == AssignationStatus.Approved || assign.status == AssignationStatus.Executing))
                {
                    if (assign.startDate < DateTime.Now.Date)
                        continue;
                    if(min == new DateTime())
                        min= assign.endDate;
                    else if (assign.startDate < min)
                        min = assign.endDate;
                }
            }
            return min;
        }
        public void RemoveIndex(string index)
        {
            List<string> newIndexes = new List<string>();
            for (int i = 0; i < qualifiedShiftsIndexes.Length; i++)
            {
                if (qualifiedShiftsIndexes[i] != index)
                    newIndexes.Add(qualifiedShiftsIndexes[i]);
            }
            this.qualifiedShiftsIndexes = newIndexes.ToArray();
        }
        public static string[] MultiValuedParser(string shiftsString)
        {
            return shiftsString.Split(new string[] { Program.metadata["MultifieldDeliminator"].ToString() },StringSplitOptions.RemoveEmptyEntries);
        }
        public string[] GetTeamAffiliationIndexes(List<ShiftEntity> shifts)
        {
            List<string> teamIndexes = new List<string>() { "0"};
            foreach (string index in qualifiedShiftsIndexes)
            {
                try
                {
                    ShiftEntity s= shifts.Where(id => id.ID == index).FirstOrDefault();
                    if (!teamIndexes.Contains(s.teamIndex))
                    {
                        teamIndexes.Add(s.teamIndex);
                    }
                }
                catch (Exception)
                {

                    continue;
                }
                
            }
            return teamIndexes.ToArray();
        }
        public static string TranslateToRank(Rank rank)
        {
            string s = "רבט";
            switch (rank)
            {
                case Rank.Corporal:
                    s = "רבט";
                    break;
                case Rank.Sergeant:
                    s = "סמל";
                    break;
                case Rank.StaffSergeant:
                    s = "סמר";
                    break;
                case Rank.SergeantFirstClass:
                    s = "רסל";
                    break;
                case Rank.MasterSergeant:
                    s = "רסר";
                    break;
                case Rank.SergeantMajor:
                    s = "רסמ";
                    break;
                case Rank.CommandSergeantMajor:
                    s = "רסב";
                    break;
                case Rank.WarrantOfficer:
                    s = "רנמ";
                    break;
                case Rank.ChiefWarrantOfficer:
                    s = "רנג";
                    break;
                case Rank.SecondLieutenant:
                    s = "סגמ";
                    break;
                case Rank.FirstLieutenant:
                    s = "סגן";
                    break;
                case Rank.Captain:
                    s = "סרן";
                    break;
                case Rank.Major:
                    s = "רסן";
                    break;
                case Rank.LieutenantColonel:
                    s = "סאל";
                    break;
                case Rank.Colonel:
                    s = "אלמ";
                    break;
                case Rank.BrigadierGeneral:
                    s = "תאל";
                    break;
                case Rank.MajorGeneral:
                    s = "אלוף";
                    break;
                case Rank.LieutenantGeneral:
                    s = "ראל";
                    break;
                case Rank.BrevetFirstLieutenant:
                    s = "קמא";
                    break;
                case Rank.BrevetCaptain:
                    s = "קאב";
                    break;
                default:
                    break;
            }
            return s;
        }
            
        public static Rank TranslateRankLiteral(string rank)
        {

            Rank r= Rank.Corporal;
     
            switch (rank)
            {
                case "רבט": r = Rank.Corporal; break;
                case "סמל": r = Rank.Sergeant; break;
                case "סמר": r = Rank.StaffSergeant; break;
                case "רסל": r = Rank.SergeantFirstClass; break;
                case "רסר": r = Rank.MasterSergeant; break;
                case "רסמ": r = Rank.SergeantMajor; break;
                case "רסב": r = Rank.CommandSergeantMajor; break;
                case "רנמ": r = Rank.WarrantOfficer; break;
                case "רנג": r = Rank.ChiefWarrantOfficer; break;
                case "סגמ": r = Rank.SecondLieutenant; break;
                case "סגן": r = Rank.FirstLieutenant; break;
                case "סרן": r = Rank.Captain; break;
                case "רסן": r = Rank.Major; break;
                case "סאל": r = Rank.LieutenantColonel; break;
                case "אלמ": r = Rank.Colonel; break;
                case "תאל": r = Rank.BrigadierGeneral; break;
                case "אלוף": r = Rank.MajorGeneral; break;
                case "ראל": r = Rank.LieutenantGeneral; break;
                case "קמא": r = Rank.BrevetFirstLieutenant; break;
                case "קאב": r = Rank.BrevetCaptain; break;
            }
            return r;
        }
        public DateTime GetLastReserveDutyDate()
        {

          
            List<Assignation> lst = this.Assignations.Where(st => st.status== AssignationStatus.Executed).OrderByDescending(o => o.endDate).ToList();
            if (lst.Count > 0)
            {
                return lst[0].endDate; 
            }
            else
            {
                Program.f.addToWarning(this.firstName+" "+this.lastName+" - לא נמצא יום מילואים אחרון. ביקור אחרון לפי תאריך שחרור");
                return this.resignmentDate;
            }

        }
        public List<ShiftEntity> GetQualifiedShifts(List<ShiftEntity> shifts)
        {
            List<ShiftEntity> s = new List<ShiftEntity>();
            foreach (string shiftIndex in this.qualifiedShiftsIndexes)
            {
                foreach (ShiftEntity item in shifts)
                {
                    if (item.ID == shiftIndex)
                    {
                        s.Add(item);
                        break;
                    }
                }
              //  s.Add(shifts[int.Parse(shiftIndex)]);
            }
            return s;
        }
        public QualificationStatus GetQualificationStatus()
        {
            this.qualificationStatus = CalculateQulificationStatus();
            return this.qualificationStatus;
        }
        public void SetQualificationStatus(QualificationStatus st)
        {
            this.qualificationStatus = st;
        }
        public static string GetQualificationStatusLiteral(QualificationStatus s)
        {
            switch (s)
            {
                case QualificationStatus.AlmostNotQualified:
                case QualificationStatus.HalfQualified:
                case QualificationStatus.Qualified: return "כשיר";

                case QualificationStatus.NotQualified: return "לא כשיר";
                case QualificationStatus.ForReduction: return "מיועד/ת לגריעה";
                case QualificationStatus.Reduced: return "לא בשבצ\"ק";
                case QualificationStatus.Unknown: return "לא ידוע";
            }
            return "לא ידוע";
        }
        public string GetQualificationStatusLiteral()
        {
            switch(this.qualificationStatus)
            {
                case QualificationStatus.AlmostNotQualified:
                case QualificationStatus.HalfQualified:
                case QualificationStatus.Qualified: return "כשיר";

                case QualificationStatus.NotQualified: return "לא כשיר";
                case QualificationStatus.ForReduction: return "מיועד/ת לגריעה";
                case QualificationStatus.Reduced: return "לא בשבצ\"ק";
                case QualificationStatus.Unknown: return "לא ידוע";
            }
            return "לא ידוע";
        }
        public QualificationStatus CalculateQulificationStatus()
        {
            if (!this.inManPower)
                return QualificationStatus.Reduced;

            DateTime lastDate = this.GetLastReserveDutyDate();

            TimeSpan inactivity = DateTime.Now - lastDate;


            QualificationStatus temp = QualificationStatus.Unknown;
            if (inactivity.Days < (int)Program.metadata["DaysForHalfQualified"])
            {
                temp = (QualificationStatus.Qualified);
            }
            if (inactivity.Days >= (int)Program.metadata["DaysForHalfQualified"])
            {

                temp =(QualificationStatus.HalfQualified);
            }
            if (inactivity.Days >= (int)Program.metadata["DaysForAlmostNotQualified"])
            {

                temp =(QualificationStatus.AlmostNotQualified);
            }
            if (inactivity.Days >= (int)Program.metadata["DaysForNotQualified"])
            {

                temp =(QualificationStatus.NotQualified);
            }
            if (inactivity.Days >= (int)Program.metadata["DaysForReduction"] && GetNextDateOfShift()== new DateTime())
            {
                Program.f.addToWarning(this.firstName + " " + this.lastName + " מיועד/ת לרשימת גריעה!");
                temp =(QualificationStatus.ForReduction);
            }
            return temp;
        }
        public void SetQualificationSatus(QualificationStatus s)
        {
            this.qualificationStatus = s;
        }
        public override string ToString()
        {
            return GetFullName(); 
        }
        public string GetRankLiteral()
        {
            return this.rankLiteral;
        }
    }
    public enum Rank
    {
        Corporal,
        Sergeant,
        StaffSergeant,
        SergeantFirstClass,
        MasterSergeant,
        SergeantMajor,
        CommandSergeantMajor,
        WarrantOfficer,
        ChiefWarrantOfficer,
        SecondLieutenant,
        FirstLieutenant,
        Captain,
        Major,
        LieutenantColonel,
        Colonel,
        BrigadierGeneral,
        MajorGeneral,
        LieutenantGeneral,
        BrevetFirstLieutenant,
        BrevetCaptain,

    }
    public enum QualificationStatus
    {
        Qualified,
        HalfQualified,
        AlmostNotQualified,
        NotQualified,
        ForReduction,
        Reduced,
        Unknown,
    }
}
