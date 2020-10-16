using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using WebAPIMVC.APIClasses;
using WebAPIMVC.Functionality;
using System.Data.Entity.Core.Common.CommandTrees;
using WebAPIMVC.Models;
using WebAPIMVC.Abstracts;

namespace WebAPIMVC.AbstractStaticEnumeration
{
    public abstract class Enumeration
    {

        protected Enumeration(int id, string abbreviation)
        {
            Id = id;
            Abbreviation = abbreviation;
        }

        public int Id { get; }
        
        public string Abbreviation { get; }
    }
}