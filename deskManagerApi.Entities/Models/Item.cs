using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace deskManagerApi.Models
{
    [Table("items")]
    public class Item
    {
        #region Properties and indexers

        /// <summary>
        /// Gets or sets the ID value of item.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name value of item.
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the date value of item creation.
        /// </summary>
        public DateTime AddDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the serial number value of item.
        /// </summary>
        public string? SerialNumber { get; set; }

        /// <summary>
        /// Gets or sets the status value of item.
        /// </summary>
        public ItemStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the type value of item.
        /// </summary>
        public ItemType Type { get; set; }

        /// <summary>
        /// Gets or sets the value of owner ID.
        /// </summary>
        [ForeignKey("Owner")]
        public int? OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the value of owner object.
        /// </summary>
        public User? Owner { get; set; }

        /// <summary>
        /// Gets or sets the value of brand ID.
        /// </summary>
        [ForeignKey("Brand")]
        public int? BrandId { get; set; }

        /// <summary>
        /// Gets or sets the value of brand object.
        /// </summary>
        public Brand? Brand { get; set; }

        /// <summary>
        /// Gets or sets the value of desk ID.
        /// </summary>
        [ForeignKey("Desk")]
        public int? DeskId { get; set; }

        /// <summary>
        /// Gets or sets the value of desk object.
        /// </summary>
        public Desk? Desk { get; set; }

        #endregion
    }
}
