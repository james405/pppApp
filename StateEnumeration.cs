using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using WebAPIMVC.Functionality;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Web.UI;
using WebAPIMVC.APIClasses;
using WebAPIMVC.Models;

namespace WebAPIMVC.Abstracts
{
    //note either have to add states images or remove the image 

    public class StateEnumeration 
    {
        public int Id { get; }
        public string Abbreviation { get; }
        

        protected StateEnumeration(int id, string abbreviation)
        {
            Id = id;
            Abbreviation = abbreviation;

        }

        #region Base Functionality

        /// <summary>
        /// GetAll
        /// Returns all the items in the enumeration
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<State> GetAll<T>() where T : State
        {
            var fields = typeof(T).GetFields(BindingFlags.Public |
                                             BindingFlags.Static |
                                             BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        /// <summary>
        /// GetOridinal - returns the ordinal value of the selected enumeration.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int GetOrdinal<T>(State name) where T : State
        {

            var fields = typeof(T).GetFields(BindingFlags.Public |
                                           BindingFlags.Static |
                                           BindingFlags.DeclaredOnly);
            return fields.Where(f => f.Name.ToString() == name.ToString()).Select(f => f.GetValue(null)).Cast<T>().FirstOrDefault().Id;

        }

        /// <summary>
        /// GetImageName - returns the image name for the selected enumeration.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetImageName<T>(State name) where T : State
        {
            var fields = typeof(T).GetFields(BindingFlags.Public |
                                         BindingFlags.Static |
                                         BindingFlags.DeclaredOnly);


            return fields.Where(f => f.Name.ToString() == name.ToString()).Select(f => f.GetValue(null)).Cast<T>().FirstOrDefault().Abbreviation;

        }




        #endregion

        #region Functionality

        /// <summary>
        /// GetStates
        /// Returns the list of states in a division or region
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<WebAPIMVC.APIClasses.State> GetStates<T>(int id) where T : State
        {

            var dbc = DB.createLocationContext();
            List<Models.State> tmp = new List<Models.State>();

            if (typeof(T).Name.ToString() == "Region")
            {
                tmp.AddRange(dbc.States.Where(d => d.RegionIdFK == id).Select(d => d).ToList());
            }
            else
            {
                tmp.AddRange(dbc.States.Where(d => d.DivisionIdFK == id).Select(d => d).ToList());
            }

            List<WebAPIMVC.APIClasses.State> states = new List<WebAPIMVC.APIClasses.State>();

            foreach (WebAPIMVC.Models.State s in tmp)
            {
                WebAPIMVC.APIClasses.State outS = new WebAPIMVC.APIClasses.State();
                outS.Abbreviation = s.Abbreviation;
                outS.StateId = s.StateId;
                states.Add(outS);
            }

            dbc.Dispose();
            return states;
        }

        public static List<string> GetStatesAbbreviation<T>(int id) where T : State
        {

            CensusDbEntities dbc = DB.createLocationContext();
            List<WebAPIMVC.Models.State> tmp = new List<WebAPIMVC.Models.State>();

            if (typeof(T).Name.ToString() == "Region")
            {
                tmp.AddRange(dbc.States.Where(d => d.RegionIdFK == id).Select(d => d).ToList());
            }
            else
            {
                tmp.AddRange(dbc.States.Where(d => d.DivisionIdFK == id).Select(d => d).ToList());
            }

            List<string> states = new List<string>();

            foreach (WebAPIMVC.Models.State s in tmp)
            {
              
                states.Add(s.Abbreviation);
            }

            dbc.Dispose();
            return states;
        }

        #endregion

    }

}