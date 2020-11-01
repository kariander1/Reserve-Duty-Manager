using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DarkDemo
{
    public class ShiftEntity
    {
        public string ID { get; set; }
        public string shiftName { get; set; }
        public int ValidityInDays { get; set; }
        public string teamIndex { get; set; }
        public ShiftEntity(string id, string shiftName, int validity,string teamIndex)
        {
            this.ID = id;
            this.shiftName = shiftName;
            this.ValidityInDays = validity;
            this.teamIndex = teamIndex;
        }
        public ShiftEntity()
        {

        }
        public int GetNumberOfQualified(List<ReserveDutyEntity> manpower)
        {
            int count = 0;
            foreach (ReserveDutyEntity man in manpower)
            {
                if (man.qualifiedShiftsIndexes.Contains(this.ID))
                {
                    count++;
                }
            }
            return count;
        }
        public int GetNumberOfQualifiedRelevant(List<ReserveDutyEntity> manpower)
        {
            int count = 0;
            foreach (ReserveDutyEntity man in manpower)
            {
                if (man.qualifiedShiftsIndexes.Contains(this.ID))
                {
                    DateTime lastDate = man.GetLastDateOfShift(this);
                    if (teamIndex=="0" || this.IsQualified(lastDate))
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        public double GetQualPercentage(List<ReserveDutyEntity> manpower)
        {
            double qual = this.GetNumberOfQualified(manpower);
            double rel = this.GetNumberOfQualifiedRelevant(manpower);
            return (rel / qual) * 100;
        }
        public override string ToString()
        {
            return this.shiftName;
        }
        public bool IsQualified(DateTime lastShift)
        {
            return ((DateTime.Now - lastShift).Days <= ValidityInDays);
        }
        public static string ShiftsToindexes(List<ShiftEntity> shifts)
        {
            string s = "";
            string deliminator = Program.metadata["MultifieldDeliminator"].ToString() ;
            for (int i = 0; i < shifts.Count; i++)
            {
                if (i == 0)
                    s += shifts[i].ID;
                else
                    s += deliminator + shifts[i].ID;
            }
           
            return s;
        }
        public static string ShiftsToindexes(string[] shifts)
        {
            string s = "";
            string deliminator =(Program.metadata["MultifieldDeliminator"].ToString());
            for (int i = 0; i < shifts.Length; i++)
            {
                if (i == 0)
                    s += shifts[i];
                else
                    s += deliminator + shifts[i];
            }
            return s;
        }
    }
}
