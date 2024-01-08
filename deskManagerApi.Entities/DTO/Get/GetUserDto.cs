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
    public class GetUserDto
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
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the lastname value of user.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the login value of user.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Gets or sets the role value of user.
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// Gets or sets the value of team ID.
        /// </summary>
        public int? TeamId { get; set; }

        /// <summary>
        /// Gets or sets the value of team name.
        /// </summary>
        public string? TeamName { get; set; }

        #endregion
    }
}
