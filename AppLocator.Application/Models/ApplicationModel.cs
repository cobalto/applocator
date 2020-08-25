using System.ComponentModel.DataAnnotations;

namespace AppLocator.Application.Models
{
    public sealed class ApplicationModel
    {
        [Required]
        public int Application { get; set; }

        [Required]
        [MinLength(5)]
        public string Url { get; set; }

        [Required]
        [MinLength(3)]
        public string PathLocal { get; set; }

        [Required]
        public bool DebuggingMode { get; set; }
    }
}
