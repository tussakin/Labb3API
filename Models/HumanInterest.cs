using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb3API.Models
{
    public class HumanInterest
    {
        [Key]
        public int HumanInterestId { get; set; }
        [ForeignKey("Human")]
        public int FkHumanId { get; set; }
        public Human Human { get; set; }

        [ForeignKey("Interest")]
        public int? FkInterestId { get; set; }
        public Interest Interest { get; set; }

        [ForeignKey("Link")]
        public int? FkLinkId { get; set; }
        public Link Link { get; set; }
    }
}
