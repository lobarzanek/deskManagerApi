using deskManagerApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deskManagerApi.Entities.DTO.Get
{
    public class GetItemDto
    {
        #region Properties and indexers

        /// <summary>
        /// Gets or sets the ID value of item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name value of item.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the date value of item creation.
        /// </summary>
        public DateTime AddDate { get; set; }

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
        public int? OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the name value of Owner.
        /// </summary>
        public string? OwnerName { get; set; }

        /// <summary>
        /// Gets or sets the value of brand ID.
        /// </summary>
        public int? BrandId { get; set; }

        /// <summary>
        /// Gets or sets the name value of Brand.
        /// </summary>
        public string? BrandName { get; set; }

        /// <summary>
        /// Gets or sets the value of desk ID.
        /// </summary>
        public int? DeskId { get; set; }

        /// <summary>
        /// Gets or sets the name value of Desk.
        /// </summary>
        public string? DeskName { get; set; }

        #endregion
    }
}
