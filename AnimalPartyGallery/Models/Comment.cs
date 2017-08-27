using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AnimalPartyGallery.Models
{
    public class Comment
    {
        
        public int ID { get; set; }
        public int RefferedPost { get; set; }
        public string Author { get; set; }
        [Required(ErrorMessage = "Title required")]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Content required")]
        [Display(Name = "Content")]
        public string Content { get; set; }
        [Required(ErrorMessage = "Photo required")]
        [Display(Name = "Photo")]
        public string Photo { get; set; }
        [Display(Name = "Hichhiker")]
        public bool Hitchhiker { get; set; }

        
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }

    }
}