using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PandaTours.Models
{
    /*This model defines Supplier and his details*/
    public class Supplier
    {
        /*SupplierID- the supplier's id in table*/
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SupplierID { get; set; }

        /*SupplierName- supplier's name*/
        [Display(Name = "Supplier Name")]
        [RegularExpression(@"^[a-zA-Z0-9''-'\s]*$", ErrorMessage = "Required and must be alphabates and numbers only")]
        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string SupplierName { get; set; }

        /*Address- supplier's sddress*/
        [Display(Name = "Supplier Address")]
        [RegularExpression(@"^[a-zA-Z0-9''-'\s]*$", ErrorMessage = "Required and must be alphabates and numbers only")]
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Address { get; set; }

        /*Destination- the destination which is provided by the supplier*/
        [Display(Name = "Destination")]
        [RegularExpression(@"^[a-zA-Z''-'\s]*$", ErrorMessage = "Required and must be alphabates and numbers only")]
        [Required]
        [StringLength(15, MinimumLength = 2)]
        public string Destination { get; set; }

        /*Email- supplier's קmail*/
        [Display(Name = "Supplier Email")]
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessage = "Required and must be a valid email")]
        [Required]
        [StringLength(40)]
        public string Email { get; set; }

        /*TelephoneSupplier- supplier's phone*/
        [Display(Name = "Supplier Phone")]
        [RegularExpression(@"^((\\+[1-9]{1,4}[ \\-]*)|(\\([0-9]{2,3}\\)[ \\-]*)|([0-9]{2,4})[ \\-]*)*?[0-9]{3,4}?[ \\-]*[0-9]{3,4}?$", ErrorMessage = "Required and must be numeric only")]
        [Required]
        [StringLength(15, MinimumLength = 9)]
        public string TelephoneSupplier { get; set; }

        /*VacationPackage- use for connection between vacation package and supplier*/
        public virtual ICollection<VacationPackage> VacationPackage { get; set; }

    }
}