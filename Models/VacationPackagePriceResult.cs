using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PandaTours.Models
{
    /*This model defines VacationPackagePriceResult object for join action*/
    public class VacationPackagePriceResult
    {
        /*Destination- vacation-package's destination*/
        public string Destination { get; set; }

        /*VacationPackageID- the vacation-package's id in table*/
        public int VacationPackageID { get; set; }

        /*Total- total price of order*/
        public Double Total { get; set; }

        /*CustomerID- the customer's id in table*/
        public int CustomerID { get; set; }

        /*PassportNum- passport number of customer*/
        public string PassportNum { get; set; }

        /*FirstName- customer's FirstName*/
        public string FirstName { get; set; }

        /*LastName- customer's LastName*/
        public string LastName { get; set; }

        /*Hotel-vacation-vacation-package's Hotel*/
        public string Hotel { get; set; }

        /*NightNum- vacation-package's night number*/
        public int NightNum { get; set; }

        /*PassengersNum- number of passanger in order*/
        public int PassengersNum { get; set; }
    }
}