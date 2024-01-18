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
    public class CreateReservationDto
    {
        #region Properties and indexers

        /// <summary>
        /// Gets or sets the date value of reservation.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the value of User ID.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the value of desk ID.
        /// </summary>
        public int DeskId { get; set; }

        #endregion
    }
}
