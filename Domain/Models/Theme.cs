using System;
using System.ComponentModel.DataAnnotations;

namespace ThemeApi.Domain.Models
{
    public class Theme
    {
        public Guid Id { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } // When we created the theme
        [Required]
        public string Name { get; set; } // Theme name
        [Required]
        [DataType(DataType.Time)]
        public string DateForShown { get; set; } // When to use theme
        [DataType(DataType.ImageUrl)]
        public string IconImage { get; set; } // Theme icon URL string
        public string MarqueeText { get; set; } // Animated float text
        public string PrimaryColor { get; set; } // Could be background color
        public string SecondaryColor { get; set; } // Could be text color
    }
}
