using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb3API.Models
{
    public class Human
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HumanId { get; set; }
        public string HumanName { get; set; }
        public string HumanEmail { get; set; }
        public string HumanNickname { get; set; }
        public string HumanRandomQuote { get; set; }


    }
}
