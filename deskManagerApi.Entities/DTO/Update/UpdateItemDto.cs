using deskManagerApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deskManagerApi.Entities.DTO.Update
{
    public class UpdateItemDto
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
        /// Gets or sets the value of brand ID.
        /// </summary>
        [ForeignKey("Brand")]
        public int? BrandId { get; set; }

        /// <summary>
        /// Gets or sets the value of desk ID.
        /// </summary>
        [ForeignKey("Desk")]
        public int? DeskId { get; set; }

        #endregion
    }
}
