using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


/*This model defines customer and his details*/
namespace PandaTours.Models
{
    public class Customer
    {
        /*CustomerID- the customer's id in table*/
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CustomerID { get; set; }

        /*PassportNum- passport number of customer*/
        [Display(Name = "Passport Number")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Required and must be of 8 digits at least")]
        [Required]
        [StringLength(9, MinimumLength = 8)]
        public string PassportNum { get; set; }

        /*FirstName- customer's FirstName*/
        [Display(Name = "First Name")]
        [RegularExpression(@"^[a-zA-Z''-'\s]*$", ErrorMessage = "Required and must be alphabates only")]
        [Required]
        [StringLength(10, MinimumLength = 2)]
        public string FirstName { get; set; }

        /*LastName- customer's LastName*/
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[a-zA-Z''-'\s]*$", ErrorMessage = "Required and must be alphabates only")]
        [Required]
        [StringLength(10, MinimumLength = 2)]
        public string LastName { get; set; }

        /*BirthDate- customer's BirthDate*/
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        /*Address- customer's Address*/
        [Display(Name = "Address")]
        [RegularExpression(@"^[a-zA-Z0-9''-'\s]*$", ErrorMessage = "Must be alphabates and numbers only")]
        [StringLength(30, MinimumLength = 2)]
        public string Address { get; set; }

        /*PhoneNum- customer's PhoneNum*/
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^((\\+[1-9]{1,4}[ \\-]*)|(\\([0-9]{2,3}\\)[ \\-]*)|([0-9]{2,4})[ \\-]*)*?[0-9]{3,4}?[ \\-]*[0-9]{3,4}?$", ErrorMessage = "Required and must be numeric only")]
        [Required]
        [StringLength(15, MinimumLength = 9)]
        public string PhoneNum { get; set; }

        /*Email- customer's Email*/
        [Display(Name = "E-Mail")]
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessage = "Required and must be a valid email")]
        [Required]
        [StringLength(40)]
        public string Email { get; set; }

        /*JoinDate- customer's JoinDate*/
        [Display(Name = "Join Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime JoinDate { get; set; }


        /*Orders- use for connection between customer and order*/
        public virtual ICollection<Order> Orders { get; set; }
    }
}