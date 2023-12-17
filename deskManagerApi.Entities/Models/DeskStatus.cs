using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace deskManagerApi.Models
{
    [Table("deskStatuses")]
    public class DeskStatus
    {
        #region Properties and indexers

        /// <summary>
        /// Gets or sets the ID value of Desk Status.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name value of Desk Status.
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the collection of Desks.
        /// </summary>
        public ICollection<Desk> Desks { get;set; } = new List<Desk>();

        #endregion
    }
}
