using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebAPIMVC.Models;

namespace WebAPIMVC.APIClasses
{
    public static class ApplicationSetup
    {

        /// <summary>
        /// Get Regions
        /// Returns regions with id, name and image file name.
        /// </summary>
        /// <returns></returns>
        public static IList<WebAPIMVC.Models.Region> GetRegions()
        {
            try
            {

                //note had to remove &quot from string and replace with single '
                DbContext mycntxt = new DbContext("metadata = res://*/Models.Locations.csdl|res://*/Models.Locations.ssdl|res://*/Models.Locations.msl;" +
                                                  "provider=System.Data.SqlClient;" +
                                                  "provider connection string=';Data Source=.;initial catalog=CensusDb;user id=p3AppUser;pwd=@yhT4f%9;" +
                                                  "MultipleActiveResultSets=True;App=EntityFramework';");
               
                        
                var regs = mycntxt.Database.SqlQuery<WebAPIMVC.Models.Region>
                    ("select RegionId, RegionName, ImageName from Regions order by RegionId").ToList();
            
                return regs;

            }
            catch(Exception ex)
            {
               
                return null;
            }

        }

        /// <summary>
        /// Get Disivions
        /// Returns divisions with id, name and image file name.
        /// </summary>
        /// <returns></returns>
        public static IList<WebAPIMVC.Models.Division> GetDivisions()
        {
            try
            {

                //note had to remove &quot from string and replace with single '
                DbContext mycntxt = new DbContext("metadata = res://*/Models.Locations.csdl|res://*/Models.Locations.ssdl|res://*/Models.Locations.msl;" +
                                                  "provider=System.Data.SqlClient;" +
                                                  "provider connection string=';Data Source=.;initial catalog=CensusDb;user id=p3AppUser;pwd=@yhT4f%9;" +
                                                  "MultipleActiveResultSets=True;App=EntityFramework';");
               
                var divs = mycntxt.Database.SqlQuery<WebAPIMVC.Models.Division>("select DivisionId, DivisionName, ImageName from Divisions order by DivisionId").ToList();

                return divs;

            }
            catch (Exception ex)
            {
               
                return null;
            }

        }

        /// <summary>
        /// Get States
        /// Returns States with id, abbreviation
        /// </summary>
        /// <returns></returns>
        public static IList<WebAPIMVC.Models.State> GetStates()
        {
            try
            {

                //note had to remove &quot from string and replace with single '
                DbContext mycntxt = new DbContext("metadata = res://*/Models.Locations.csdl|res://*/Models.Locations.ssdl|res://*/Models.Locations.msl;" +
                                                  "provider=System.Data.SqlClient;" +
                                                  "provider connection string=';Data Source=.;initial catalog=CensusDb;user id=p3AppUser;pwd=@yhT4f%9;" +
                                                  "MultipleActiveResultSets=True;App=EntityFramework';");

                var divs = mycntxt.Database.SqlQuery<WebAPIMVC.Models.State>("select StateId, Abbreviation from State order by Abbreviation").ToList();

                return divs;

            }
            catch (Exception ex)
            {

                return null;
            }

        }


    }
}