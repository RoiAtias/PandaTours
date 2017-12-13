using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PandaTours.Models
{
    /*This model defines OrderDistResult object for group by action*/
    public class OrderDistResult
    {
        /*CustomerID- the customer's id in table*/
        public int CustomerID { get; set; }

        /*PassportNum- passport number of customer*/
        public string PassportNum { get; set; }

        /*FirstName- customer's FirstName*/
        public string FirstName { get; set; }

        /*LastName- customer's LastName*/
        public string LastName { get; set; }

        /*Destination- order's destination*/
        public string Destination { get; set; }

        /*Total- order's total price*/
        public Double Total { get; set; }
    }
}