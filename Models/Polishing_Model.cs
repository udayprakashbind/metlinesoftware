using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreMgmt.Models
{
    public class Polishing_Model
    {
        public int Id { get; set; }
        public string JobType { get; set; }
        public int jobId { get; set; }
        public int rowId { get; set; }
        public string JobName { get; set; }
        public string Code { get; set; }
        public string remark { get; set; }
        public string JobWeight { get; set; }
        public string JobIssueWeight { get; set; }
        public string PolishWeight { get; set; }
        public string remainingJobWeight { get; set; }
        public string PolishScrap { get; set; }
        public string ItemName { get; set; }
        public string PolishPices { get; set; }
        public string JobPices { get; set; }
        public string Date { get; set; }
    }
    public class jobweightUpdate
    {
        public int Id { get; set; }
        public string JobType { get; set; }

        public string JobUpdateWeight { get; set; }
        public string JobUpdatePices { get; set; }

    }
}