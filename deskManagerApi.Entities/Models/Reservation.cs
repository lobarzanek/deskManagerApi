using deskManagerApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deskManagerApi.Entities.Models
{
    [Table("reservations")]
    public class Reservation
    {
        #region Properties and indexers

        /// <summary>
        /// Gets or sets the ID value of Reservation.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the date value of reservation.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the value of User ID.
        /// </summary>
        [ForeignKey("User")]
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or sets the value of User object.
        /// </summary>
        public User? User { get; set; }

        /// <summary>
        /// Gets or sets the value of desk ID.
        /// </summary>
        [ForeignKey("Desk")]
        public int? DeskId { get; set; }

        /// <summary>
        /// Gets or sets the value of desk object.
        /// </summary>
        public Desk? Desk { get; set; }

        #endregion
    }
}
