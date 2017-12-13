using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PandaTours.Models
{
    /*This model defines Location and his details*/
    public class Location
    {
        public int LocationId { get; set; }

        /*Name- place name*/
        [Display(Name = "Name Places")]
        [RegularExpression(@"^[a-zA-Z''-'\s]*$", ErrorMessage = "Required and must be alphabates only")]
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Name { get; set; }

        /*Description- Description of a place , not must*/
        [Display(Name = "Description")]
        [RegularExpression(@"^[a-zA-Z''-'\s]*$", ErrorMessage = "Must be alphabates only")]
        [StringLength(15)]
        public string Description { get; set; }

        /*Latitude- place's Latitude*/
        [Display(Name = "Latitude")]
        [RegularExpression(@"^-?([1]?[1-7][1-9]|[1]?[1-8][0]|[1-9]?[0-9])\.{1}\d{1,15}$", ErrorMessage = "Required and Must be decimal number only")]
        [Required]
        public double Latitude { get; set; }

        /*Longitude- place's Longitude*/
        [Display(Name = "Longitude")]
        [RegularExpression(@"^-?([1]?[1-7][1-9]|[1]?[1-8][0]|[1-9]?[0-9])\.{1}\d{1,15}$", ErrorMessage = "Required and Must be decimal number only")]
        [Required]
        public double Longitude { get; set; }

        /*ZIndex- place's ZIndex*/
        [Display(Name = "ZIndex")]
        [Required]
        [Range(1, 10)]
        public int ZIndex { get; set; }
    }
}