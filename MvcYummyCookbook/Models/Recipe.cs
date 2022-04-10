using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcYummyCookbook.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        [RegularExpression(@"^[a-zA-Z]+[a-zA-Z\s]*$")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Time [min]")]
        public int Time { get; set; }
        [RegularExpression(@"(easy|medium|hard)$")]
        public string Difficulty { get; set; }
        [Display(Name = "Number of likes")]
        public int NumberOfLikes { get; set; }
        [Required]
        public string Ingredients { get; set; }
        [StringLength(500, MinimumLength = 10)]
        [Required]
        public string Process { get; set; }
        public string Tips { get; set; }
        public string Tricks { get; set; }

    }
}
