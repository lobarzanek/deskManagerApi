﻿using deskManagerApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deskManagerApi.Entities.DTO.Update
{
    public class UpdateRoomDto
    {
        #region Properties and indexers

        /// <summary>
        /// Gets or sets the ID value of room.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name value of room.
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value of map view box.
        /// </summary>
        public string? mapViewBox { get; set; }

        /// <summary>
        /// Gets or sets the value of map width.
        /// </summary>
        public string? mapWidth { get; set; }

        /// <summary>
        /// Gets or sets the value of map height.
        /// </summary>
        public string? mapHeight { get; set; }

        /// <summary>
        /// Gets or sets the value of xmlns.
        /// </summary>
        public string mapXmlns { get; set; } = "http://www.w3.org/2000/svg";

        /// <summary>
        /// Gets or sets the value of floor ID.
        /// </summary>
        [ForeignKey("Floor")]
        public int? FloorId { get; set; }

        #endregion
    }
}
