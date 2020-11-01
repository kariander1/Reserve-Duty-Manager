using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DarkDemo
{
    public class Assignation
    {
        public string ID { get; set; }
      //  public string PersonalNumber { get; set; }
        public ReserveDutyEntity reserverDutyEntity { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string[] shiftsIndexes { get; set; }
        public AssignationStatus status { get; set; }
        public bool IsFaulty { get; set; }
        public List<string> comments { get; set; }
        public int reserveDays { get; set; }
        public Assignation(string id, ReserveDutyEntity reserverDutyEntity, DateTime start,DateTime end,string shiftsString,string assignStatusString)
        {
            this.ID = id;
            this.reserverDutyEntity = reserverDutyEntity;
            this.startDate = start;
            this.endDate = end;
            this.reserveDays =(endDate.Date - startDate.Date).Days +1;
            this.shiftsIndexes = ReserveDutyEntity.MultiValuedParser(shiftsString);
            this.status = TranslateAssignationStatus(assignStatusString);
            this.IsFaulty = false;
            this.comments = new List<string>();
        }
        public Assignation()
        {
        }
        public void RemoveIndex(string index)
        {
            List<string> newIndexes = new List<string>();
            for (int i = 0; i < shiftsIndexes.Length; i++)
            {
                if (shiftsIndexes[i] != index)
                    newIndexes.Add(shiftsIndexes[i]);
            }
            this.shiftsIndexes = newIndexes.ToArray();
        }
        public void AddComment(string comment)
        {
            this.comments.Add(comment);
        }
        public bool IsPartOfShift(string shiftID)
        {
            foreach (string item in shiftsIndexes)
            {
                if (shiftID == item)
                    return true;
            }
            return false;
        }
        public List<Assignation> CheckForOverlappingAssignation(List<Assignation> assigns)
        {
            List<Assignation> tempList = new List<Assignation>();
            foreach (Assignation assign in assigns)
            {
                if (assign.status == AssignationStatus.Canceled || assign.ID==this.ID)
                    continue;
                if (this.endDate >= assign.startDate && this.startDate< assign.startDate)
                    tempList.Add(assign);
                else if (this.startDate <= assign.endDate && this.endDate > assign.endDate)
                    tempList.Add(assign);
                else if (this.startDate <= assign.endDate && this.startDate >= assign.startDate) // Inside other assignations
                    tempList.Add(assign);
            }
            return tempList;
        }
        public static string GetStatusLiteral(AssignationStatus status)
        {
            switch (status)
            {
                case AssignationStatus.PendingApproval:
                    return "טרם מאושר";
                case AssignationStatus.Approved:
                    return "מאושר";
                case AssignationStatus.Executing:
                    return "בביצוע";
                case AssignationStatus.Executed:
                    return "בוצע";
                case AssignationStatus.Canceled:
                    return "בוטל";
                case AssignationStatus.Unknown:
                    return "לא ידוע";
                default:
                    return "לא ידוע";
            }
        }
        public string GetStatusLiteral()
        {
            switch (this.status)
            {
                case AssignationStatus.PendingApproval:
                    return "טרם מאושר";
                case AssignationStatus.Approved:
                    return "מאושר";
                case AssignationStatus.Executing:
                    return "בביצוע";
                case AssignationStatus.Executed:
                    return "בוצע";
                case AssignationStatus.Canceled:
                    return "בוטל";
                case AssignationStatus.Unknown:
                    return "לא ידוע";
                default:
                    return "לא ידוע";
            }
        }
        public static AssignationStatus TranslateAssignationStatus(string s)
        {
            switch (s)
            {
                case "PendingApproval": return AssignationStatus.PendingApproval;
                case "Approved": return AssignationStatus.Approved;
                case "Executing": return AssignationStatus.Executing;
                case "Executed": return AssignationStatus.Executed;
                case "Canceled": return AssignationStatus.Canceled;
            }
            return AssignationStatus.Unknown;
        }
    }
    public enum AssignationStatus
    {
        PendingApproval,
        Approved,
        Executing,
        Executed,
        Canceled,
        Unknown,
    }
}
