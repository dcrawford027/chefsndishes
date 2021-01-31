using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChefsNDishes.Models
{
    public class Chef
    {
        [Key]
        public int ChefId {get;set;}

        [Required]
        [Display(Name = "First Name")]
        public string FirstName {get;set;}

        [Required]
        [Display(Name = "Last Name")]
        public string LastName {get;set;}

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date of Birth")]
        public DateTime DOB {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

    // Navigation Properties
        public List<Dish> CreatedDishes {get;set;}
    }
}