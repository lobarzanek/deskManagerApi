using System.ComponentModel.DataAnnotations;

namespace deskManagerApi.Models
{
    public class Brand
    {
        #region Properties and indexers

        /// <summary>
        /// Gets or sets the ID value of desk.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name value of brand.
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the collection of items.
        /// </summary>
        public ICollection<Item> Items { get; set; }

        #endregion
    }
}
