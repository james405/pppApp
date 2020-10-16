using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIMVC.Functionality;
using WebAPIMVC.Models;
using WebAPIMVC.APIClasses;
using WebAPIMVC.Abstracts;
using System.Web.UI.WebControls;

namespace WebAPIMVC.Controllers
{
    public class VController : ApiController
    {
     
        public IList<WebAPIMVC.Models.Business> GetBusinessesAsList(string name)
        {
            IList<WebAPIMVC.Models.Business> blist = QueryBusinessData.BusinessAsList(name);
            return blist;
        }

        public IList<BusinessBase> GetBusinessesOnlyAsList(string name)
        {
            IList<BusinessBase> blist = QueryBusinessData.BusinessOnlyAsList(name);
            return blist;
        }

        public IList<BusinessBase> GetBusinessesByDivisionId(int id)
        {
            IList<BusinessBase> blist = QueryBusinessData.BusinessOnlyByDivisionAsList(id);
            return blist;
         }

        public IList<WebAPIMVC.Models.Business> GetBusinessById(int id)
        {
            return (IList<WebAPIMVC.Models.Business>)QueryBusinessData.QueryBusinessID(id);
        }

        #region Miscellaneous Functionality

        public IList<T> GetDataToEnumerate<T>(string type) where T : EnumType
        {
            return (IList<T>)QueryBusinessData.QueryDataToEnumerate<EnumType>(type);
            
        }

        public List<APIClasses.State> GetStates(string type, string name)
        {
            List<APIClasses.State> tmp = new List<APIClasses.State>();
            if (type.ToString() == "Regions")
            {
                var id = RegionEnumeration.GetOrdinal<Region>(name);
                tmp.AddRange(RegionEnumeration.GetStates<Region>(id).ToList());
            }
            else if (type == "Divisions")
            {
                var id = DivisionEnumeration.GetOrdinal<Division>(name);
                tmp.AddRange(DivisionEnumeration.GetStates<Division>(id).ToList());
            }
            else 
                return null;

            return tmp;
        }


        #endregion

    }
}
