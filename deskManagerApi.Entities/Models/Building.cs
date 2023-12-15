using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace deskManagerApi.Models
{
    [Table("buildings")]
    public class Building
    {
        #region Properties and indexers

        /// <summary>
        /// Gets or sets the ID value of building.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name value of building.
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the collection of floors.
        /// </summary>
        public ICollection<Floor> Floors { get; set; } = new List<Floor>();

        #endregion
    }
}
