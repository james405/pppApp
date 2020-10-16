using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using WebAPIMVC.APIClasses;
using WebAPIMVC.Functionality;
using System.Data.Entity.Core.Common.CommandTrees;
using WebAPIMVC.Models;

namespace WebAPIMVC.Abstracts
{
    public abstract class Enumeration
    {

        protected Enumeration(int id, string name, string imageName)
        {
            Id = id;
            Name = name;
            ImageName = imageName;
        }

        public int Id { get; }
        public string Name { get; }
        public string ImageName { get; }


    }

    
}
