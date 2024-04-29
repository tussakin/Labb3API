using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb3API.Models
{
    public class Interest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InterestId { get; set; }
        public string InterestTitle { get; set; }
        public string InterestDescription { get; set; }

    }
}
