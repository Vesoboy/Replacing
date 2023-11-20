using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Replacing.Models
{
    public class ReplaceWord
    {
        [Key]
        [Required(ErrorMessage = "OldSymbol is required.")]
        public string OldSymbol { get; set; }

        [Required(ErrorMessage = "NewSymbol is required.")]
        public string NewSymbol { get; set; }
    }
}
