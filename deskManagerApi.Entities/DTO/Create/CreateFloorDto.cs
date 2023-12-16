using deskManagerApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deskManagerApi.Entities.DTO.Create
{
    public class CreateFloorDto
    {
        #region Properties and indexers

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

        #endregion
    }
}
