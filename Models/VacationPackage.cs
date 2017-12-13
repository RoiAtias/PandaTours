using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PandaTours.Models
{
    /*This model defines VacationPackage and his details*/
    public class VacationPackage
    {
        /*VacationPackageID- the vacation-package's id in table*/
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VacationPackageID { get; set; }

        /*SupplierID-supplier key,use for connection between supplier and vacation-package*/
        [Display(Name = "Supplier ID")]
        public int SupplierID { get; set; }

        /*Destination- vacation-package's destination*/
        [Display(Name = "Destination")]
        [RegularExpression(@"^[a-zA-Z''-'\s]*$", ErrorMessage = "Required and must be alphabates only")]
        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string Destination { get; set; }

        /*Hotel-vacation-vacation-package's Hotel*/
        [Display(Name = "Hotel Name")]
        [RegularExpression(@"^[a-zA-Z''-'\s]*$", ErrorMessage = "Required and must be alphabates only")]
        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string Hotel { get; set; }

        /*DepartDate- vacation-package's departing date*/
        [Display(Name = "Departing")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DepartDate { get; set; }

        /*ReturnDate- vacation-package's return date*/
        [Display(Name = "Returning")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReturnDate { get; set; }

        /*NightNum- vacation-package's night number*/
        [Display(Name = "Night Number")]
        [Range(1, 7)]
        public int NightNum { get; set; }

        /*SinglePay- vacation-package's price fro one passanger*/
        [Display(Name = "Single Payment")]
        [RegularExpression(@"^(\$|)([1-9]\d{0,2}(\,\d{3})*|([1-9]\d*))(\.\d{0,})?$", ErrorMessage = "Required and Must be decimal number only")]
        [Required]
        public double SinglePay { get; set; }

        /*Image- vacation-package's image*/
        public String Image { get; set; }

        /*supplier- use for connection between vacation-package and supplier*/
        public virtual Supplier supplier { get; set; }

        /*Orders- use for connection between vacation-package and order*/
        public virtual ICollection<Order> Orders { get; set; }


    }
}