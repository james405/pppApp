using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIMVC.APIClasses
{
    public class BusinessBase
    {
        public int BusinessId { get; set; }
        public string BusinessName { get; set; }
        public string BusinessType { get; set; }

        public BusinessBase() { }

        public BusinessBase(int businessId, string businessName, string businessType)
        {
            this.BusinessId = businessId;
            this.BusinessName = businessName;
            this.BusinessType = businessType;

        }
    }
}