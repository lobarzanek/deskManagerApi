using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace deskManagerApi.Models
{
    public class Item
    {
        #region Properties

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime AddDate { get; set; }
        public string? SerialNumber { get; set; }
        public ItemStatus Status { get; set; }
        public ItemType Type { get; set; }

        [ForeignKey("Owner")]
        public int? OwnerId { get; set; }
        public User? Owner { get; set; }

        [ForeignKey("Brand")]
        public int? BrandId { get; set; }
        public Brand? Brand { get; set; }

        #endregion
    }
}
