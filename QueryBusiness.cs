using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using WebAPIMVC.Models;
using WebAPIMVC.Functionality;
using WebAPIMVC.Abstracts;
using System.CodeDom;

namespace WebAPIMVC.APIClasses
{

    public static class QueryBusinessData
    {

        #region Return Calls



        /// <summary>
        /// QueryBusinessID - returns a business by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static IEnumerable<WebAPIMVC.Models.Business> QueryBusinessID(int Id)
        {
            var dbc = DB.createContext();
            IEnumerable<WebAPIMVC.Models.Business> business = (from b in dbc.Businesses where b.BusinessId == Id select b).ToList();            
            return business;

        }

        /// <summary>
        /// BusinessOnlyAsList - returns any Iqueryable results as a list with only the business properties.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IList<BusinessBase> BusinessOnlyAsList(string name)
        {
            var dbc = DB.createContext();
            IList<BusinessBase> bsl = new List<BusinessBase>();
            var bs = QueryBusinessByPartialName(name, dbc);

            foreach (var b in bs)
            {
                BusinessBase tmpB = new BusinessBase(b.BusinessId, b.BusinessName, b.BusinessType);
                bsl.Add(tmpB);
            }

            dbc.Dispose();
            return bsl;
        }

        /// <summary>
        /// BusinessAsList - returns any Iquerable results as a list with associated sub data.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IList<WebAPIMVC.Models.Business> BusinessAsList(string name)
        {
            var dbc = DB.createContext();
            IList<WebAPIMVC.Models.Business> bs = QueryBusinessByPartialName(name, dbc).ToList();
            dbc.Dispose();
            return bs;
        }


        public static IList<BusinessBase> BusinessOnlyByDivisionAsList(int id)
        {
            //var dbc = DB.createContext();
            //IList<BusinessBase> bsl = new List<BusinessBase>();
            //var bs = QueryBusinessByPartialName(name, dbc);

            //foreach (var b in bs)
            //{
            //    BusinessBase tmpB = new BusinessBase(b.BusinessId, b.BusinessName, b.BusinessType);
            //    bsl.Add(tmpB);
            //}

            //dbc.Dispose();
            //return bsl;
            return null;
        }

        public static IList<T> QueryDataToEnumerate<T>(string type) where T: EnumType
        {
            var dbc = DB.createLocationContext();
            if (type == "")
            {
                return null;
            }
            else if (type == "Divisions")
            {
                IList<EnumType> cls = (from b in dbc.Divisions
                                       select new WebAPIMVC.APIClasses.EnumType()
                                       {
                                           Id = b.DivisionId,
                                           Name = b.DivisionName,
                                           ImageName = b.ImageName
                                       }).ToList();

                return (IList<T>)cls;
            }
            else if (type == "Regions")
            {
                IList<EnumType> cls = (from b in dbc.Regions
                                       select new WebAPIMVC.APIClasses.EnumType()
                                       {
                                           Id = b.RegionId,
                                           Name = b.RegionName,
                                           ImageName = b.ImageName
                                       }).ToList();
                return (IList<T>)cls;
            }
            else return null;
            
        }

        #endregion

        #region IQueryable Base Calls


        /// <summary>
        /// QueryBusinessByPartialName - returns an IQueryable data set. 
        /// Use this as a partial call to cut down the dataset before any additional manipulation, such as pulling in sub data
        /// </summary>
        /// <param name="name"></param>
        /// <returns>IQueryable<Business></returns>
        public static IQueryable<WebAPIMVC.Models.Business> QueryBusinessByPartialName(string name, PPPInfoNrmlEntities dbc)
        {

            //if the name is null then just return the top 25.
            if (name == null)
            {
                IQueryable<WebAPIMVC.Models.Business> qBusiness = dbc.Businesses.Select(i => i).Take(25);
                int cnt = qBusiness.Count();
                return qBusiness;
            }
            else
            {
                IQueryable<WebAPIMVC.Models.Business> qBusiness = dbc.Businesses.Where(i => i.BusinessName.Contains(name)).Select(i => i);
                int cnt = qBusiness.Count();
                return qBusiness;
            }

        }

        public static IQueryable<WebAPIMVC.Models.Business> QueryBusinessByState(string abbrev, PPPInfoNrmlEntities dbc)
        {
            IQueryable<WebAPIMVC.Models.Business> qBusiness = dbc.Businesses.Where(i => i.Address.State == abbrev).Select(i => i);
            int cnt = qBusiness.Count();
            return qBusiness;

        }

        public static IQueryable<WebAPIMVC.Models.Business> QueryBusinessByType(string type, PPPInfoNrmlEntities dbc)
        {

            IQueryable<WebAPIMVC.Models.Business> qBusiness = dbc.Businesses.Where(i => i.BusinessType == type).Select(i => i);
            int cnt = qBusiness.Count();
            return qBusiness;

        }

        //public static IList<WebAPIMVC.APIClasses.Business> QueryBusinessByRegionOrDivision(StateEnumeration area)
        //{
        //    IList<string> states = (from s in StateEnumeration.GetStates<StateEnumeration>(area) select s.Abbreviation).ToList();
        //    PPPInfoNrmlEntities dbc = DB.createContext();


        //    IList<Business> business = QueryBusinessBySet((List<string>)states, dbc).ToList<WebAPIMVC.APIClasses.Business>();

        //    return business;

           
        //}
       
        public static IList<Address> QueryStatesBySet(List<WebAPIMVC.APIClasses.State> states, PPPInfoNrmlEntities dbc)
        {

            List<int> sts = new List<int>();

            foreach(var s in states)
            {
                sts.Add(s.StateId);
            }

            //need to grab any addresses that are in the state list.
            IQueryable<Address> qAddress = from a in dbc.Addresses where sts.Contains(a.StateId) select new Address() { BusinessId = a.BusinessId, StateId = a.StateId, Abbreviation = a.State };

            return qAddress.ToList<Address>();
        }

        public static IList<Business> QueryBusinessBySet(List<string> abbrs, PPPInfoNrmlEntities dbc)
        {

            IList<Business> qBusiness = (from b in dbc.Businesses
                                         where abbrs.Any(abbr => b.Address.State.Equals(abbr)) orderby b.BusinessType
                                         select new Business()
                                         {
                                             BusinessId = b.BusinessId,
                                             BusinessName = b.BusinessName,
                                             BusinessType = b.BusinessType,
                                             Abbreviation = b.Address.State
                                         }).ToList();

            return qBusiness;
        }




        #endregion

    }
}



