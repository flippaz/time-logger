using System;

namespace TimeLogger.Repository.Entities
{
    public class Timesheet
    {
        public string Action { get; set; }

        public string Comments { get; set; }

        public int Id { get; set; }

        public DateTime LogDateTime { get; set; }

        public DateTime ModifiedDateTime { get; set; }
    }
}