using System.ComponentModel.DataAnnotations;

namespace Webdulich.Models
{
    public class Dichvu
    {
        [Key]
        public int DichvuId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}
