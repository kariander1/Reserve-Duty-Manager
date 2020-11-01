using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DarkDemo
{
    public class ProgramUser
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string fullName { get; set; }
        public string personalNumber { get; set; }
        public string position { get; set; }
        public Rank rank { get; set; }
        public string rankLiteral { get; set; }
        public string department { get; set; }
        public string[] teamsIndexes { get; set; }
        public ProgramUser(string firstName,string lastName,string pn,string position,string rank,string department,string teamsIndexes)
        {
            this.department = department;
            this.firstName = firstName;
            this.lastName = lastName;
            this.fullName = firstName + " " + lastName;
            this.personalNumber = pn;
            this.position = position;
            this.teamsIndexes = ReserveDutyEntity.MultiValuedParser(teamsIndexes);
            this.rank = ReserveDutyEntity.TranslateRankLiteral(rank);
            this.rankLiteral = rank;
        }
        public override string ToString()
        {
            return fullName;
        }
    }
}
