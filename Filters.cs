using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIMVC.APIClasses
{
    public enum QOperator
    {
       Equals,
       EqualsOrGreater,
       EqualsOrLess,
       LessThan,
       GreaterThan,
       Between

    }
    public class Filters
    {
        public string BusinessType { get; set; }
        public string BusinessName { get; set; }
        public string LenderName { get; set; }
        public string State { get; set; }
        public string City { get; set; }
     
        public DateTime  DateApprovedFrom { get; set; }
        public DateTime DataApprovedTo { get; set; }

        public int JobsRetainedMin { get; set; }
        public int JobsRetainedMax { get; set; }
        public QOperator  JobOperator { get; set; }


        public Filters() { }
        public Filters(string BusinessType)
        {
            this.BusinessType = BusinessType;
        }

        public Filters(string LenderName, string BusinessType)
        {
            this.BusinessType = BusinessType;
            this.LenderName = LenderName;
        }

        public Filters(string LenderName, string BusinessType, string State)
        {
            this.BusinessType = BusinessType;
            this.LenderName = LenderName;
            this.State = State;
        }

        public Filters(int JobsRetainedMin, int JobsRetainedMax, QOperator JobOperator)
        {
            this.JobOperator = JobOperator;
            this.JobsRetainedMin = JobsRetainedMin;
            this.JobsRetainedMax = JobsRetainedMax;
        }

        public Filters(int JobsRetainedMin, int JobsRetainedMax, QOperator JobOperator, string State)
        {
            this.JobOperator = JobOperator;
            this.JobsRetainedMin = JobsRetainedMin;
            this.JobsRetainedMax = JobsRetainedMax;
            this.State = State;
        }


    }
}