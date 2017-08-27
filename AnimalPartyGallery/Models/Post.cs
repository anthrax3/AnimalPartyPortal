using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AnimalPartyGallery.Models
{
    public class Post
    {
       
        public int ID { get; set; }

        public string Producer { get; set; }

        [Required(ErrorMessage="Event Name required")]
        [Display(Name = "Event Name")]
        public string Title { get; set; }

        [Display(Name = "VideoURL")]
        public string Video { get; set; }

        [Display(Name = "PhotoURL")]
        public string Photo { get; set; }

        [Required(ErrorMessage = "Content required")]
        [Display(Name = "Details")]
        public string Content { get; set; }


        [Required(ErrorMessage = "Your must provide a PhoneNumber")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }

        [Display(Name = "GoogleMap")]
        public string GoogleMap { get; set; }


        [Required(ErrorMessage = "Event Date required")]
        [Display(Name = "Event Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date{get; set;}

        private ICollection<IntegerData> _comments;
        public virtual ICollection<IntegerData> Comments
        {
            get
            {
                return this._comments ?? (this._comments = new HashSet<IntegerData>());
            }
        }


    }
}