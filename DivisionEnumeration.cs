using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using WebAPIMVC.APIClasses;
using WebAPIMVC.Functionality;
using System.Data.Entity.Core.Common.CommandTrees;
using WebAPIMVC.Models;
using System.Web.UI;

namespace WebAPIMVC.Abstracts
{
    public  class DivisionEnumeration
    {
        public int Id { get; }
        public string Name { get; }
        public string ImageName { get; }
        protected DivisionEnumeration(int id, string name, string imageName)
        {
            Id = id;
            Name = name;
            ImageName = imageName;
        }

       

        #region Base Functionality

        /// <summary>
        /// GetAll
        /// Returns all the items in the enumeration
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<Division> GetAll<T>() where T : Division
        {
            var fields = typeof(T).GetFields(BindingFlags.Public |
                                             BindingFlags.Static |
                                             BindingFlags.DeclaredOnly);
            var tmps = fields.Select(f => f.GetValue(null)).Cast<T>();
            return null;
            //return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        /// <summary>
        /// GetOridinal - returns the ordinal value of the selected enumeration.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int GetOrdinal<T>(string name) where T : Division
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
        public static string GetImageName<T>(Division name) where T : Division
        {
            var fields = typeof(T).GetFields(BindingFlags.Public |
                                         BindingFlags.Static |
                                         BindingFlags.DeclaredOnly);


            return fields.Where(f => f.Name.ToString() == name.ToString()).Select(f => f.GetValue(null)).Cast<T>().FirstOrDefault().ImageName;

        }

        #endregion


        #region Functionality

        /// <summary>
        /// GetStates
        /// Returns the list of states in a division 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<WebAPIMVC.APIClasses.State> GetStates<T>(int id) where T : Division
        {

            var dbc = DB.createLocationContext();
            List<APIClasses.State> tmp = new List<APIClasses.State>();

            tmp.AddRange(dbc.States.Where(d => d.DivisionIdFK == id).Select(d => new APIClasses.State
            {
                StateId = d.StateId,
                Name = d.Name,
                Abbreviation = d.Abbreviation
            }));


            List<WebAPIMVC.APIClasses.State> states = new List<WebAPIMVC.APIClasses.State>();

            foreach (WebAPIMVC.APIClasses.State s in tmp)
            {
                WebAPIMVC.APIClasses.State outS = new WebAPIMVC.APIClasses.State();
                outS.Abbreviation = s.Abbreviation;
                outS.Name = s.Name;
                outS.StateId = s.StateId;
                states.Add(outS);
            }

            dbc.Dispose();
            return states;
        }

        
        #endregion


    }
}