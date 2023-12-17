using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace deskManagerApi.Models
{
    [Table("floors")]
    public class Floor
    {
        #region Properties and indexers

        /// <summary>
        /// Gets or sets the ID value of floor.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name value of floor.
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value of building ID.
        /// </summary>
        [ForeignKey("Building")]
        public int? BuildingId { get; set; }

        /// <summary>
        /// Gets or sets the value of building object.
        /// </summary>
        public Building? Building { get; set; }

        /// <summary>
        /// Gets or sets the collection of rooms.
        /// </summary>
        public ICollection<Room> Rooms { get; set; } = new List<Room>();

        #endregion
    }
}
