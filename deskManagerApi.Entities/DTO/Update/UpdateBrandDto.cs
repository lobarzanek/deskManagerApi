using deskManagerApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deskManagerApi.Entities.DTO.Update
{
    public class UpdateBrandDto
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

        #endregion
    }
}
