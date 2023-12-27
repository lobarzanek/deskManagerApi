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
    public class UpdateUserDto
    {
        #region Properties and indexers

        /// <summary>
        /// Gets or sets the ID value of user.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the firstname value of user.
        /// </summary>
        [Required(ErrorMessage = "Firstname is required")]
        [StringLength(20, ErrorMessage = "Firstname can't be longer than 20 characters")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the lastname value of user.
        /// </summary>
        [Required(ErrorMessage = "Lastname is required")]
        [StringLength(20, ErrorMessage = "Lastname can't be longer than 20 characters")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the login value of user.
        /// </summary>
        [Required(ErrorMessage = "Login is required")]
        [StringLength(20, ErrorMessage = "Login can't be longer than 20 characters")]
        public string Login { get; set; }

        /// <summary>
        /// Gets or sets the role value of user.
        /// </summary>
        public Role RoleId { get; set; }

        /// <summary>
        /// Gets or sets the value of team ID.
        /// </summary>
        [ForeignKey("Team")]
        public int? TeamId { get; set; }

        #endregion
    }
}
