using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using WebAPIMVC.Models;

namespace WebAPIMVC.Functionality
{
    public class DB
    {

        //get the context
        public static PPPInfoNrmlEntities createContext()
        {
            
            
            PPPInfoNrmlEntities reqCntext = new PPPInfoNrmlEntities();
            return reqCntext;

        }

        public static CensusDbEntities createLocationContext()
        {


            CensusDbEntities reqCntext = new CensusDbEntities();
            return reqCntext;

        }

    }
}