using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PandaTours.Models
{
    /*This model defines order and his details*/
    public class Order
    {
        /*OrderID- the order's id in table*/
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }

        /*CustomerID- customer key,use for connection between customer and order*/
        public int CustomerID { get; set; }

        /*VacationPackageID-vacation package key, use for connection between vacation package and order*/
        public int VacationPackageID { get; set; }

        /*PassengersNum- number of passanger in order*/
        [Display(Name = "Passengers Number")]
        [Required]
        [Range(1, 10)]
        public int PassengersNum { get; set; }

        /*Total- total price of order*/
        [Display(Name = "Total Payment")]
        public double Total { get; set; }

        /*OrderDate- order's date*/
        [Display(Name = "Order Date")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        /*VacationPackage- use for connection between vacation package and order*/
        public virtual VacationPackage VacationPackage { get; set; }

        /*Customer- use for connection between customer and order*/
        public virtual Customer Customer { get; set; }

    }
}