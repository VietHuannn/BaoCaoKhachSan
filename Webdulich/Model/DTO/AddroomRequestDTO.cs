using System.ComponentModel.DataAnnotations;

namespace Webdulich.Model.DTO
{
    public class AddshopRequestDTO
    {
        [Required]
        [MinLength(2)]
        public int RoomId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string ImageURL { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
    }
}