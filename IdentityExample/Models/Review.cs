using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IdentityExample.Models
{
    public enum Score
    {
        Like, Dislike
    }

    public class Review
    {
        [Key]
        public int ReviewID { get; set; }

        [StringLength(500, ErrorMessage = "Content cannot be longer than 500 characters.")]
        [DataType(DataType.MultilineText)]
        [Required]
        public string Content { get; set; }

        [Display(Name = "Review Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd hh:mm tt}")]
        public DateTime ReviewDate { get; set; }

        public Score? Score { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}