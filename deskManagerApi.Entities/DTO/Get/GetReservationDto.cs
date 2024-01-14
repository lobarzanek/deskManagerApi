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
    public class GetReservationDto
    {
        #region Properties and indexers

        /// <summary>
        /// Gets or sets the ID value of Reservation.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the date value of reservation.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the value of User Name.
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// Gets or sets the value of Desk Name.
        /// </summary>
        public string? DeskName { get; set; }

        /// <summary>
        /// Gets or sets the value of Room Name.
        /// </summary>
        public string? RoomName { get; set; }

        /// <summary>
        /// Gets or sets the value of Room Name.
        /// </summary>
        public string? FloorName { get; set; }

        #endregion
    }
}
