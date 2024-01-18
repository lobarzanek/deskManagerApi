using deskManagerApi.Entities.Models;
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
    public class GetDeskMapViewDto
    {
        #region Properties and indexers

        /// <summary>
        /// Gets or sets the ID value of desk.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name value of desk.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value if any mouse is attached to the desk.
        /// </summary>
        public bool Mouse { get; set; }

        /// <summary>
        /// Gets or sets the value if any keyboard is attached to the desk.
        /// </summary>
        public bool Keyboard { get; set; }

        /// <summary>
        /// Gets or sets the value if any dockStation is attached to the desk.
        /// </summary>
        public bool DockStation { get; set; }

        /// <summary>
        /// Gets or sets the value of how many monitors are attached to the desk.
        /// </summary>
        public int MonitorNumber { get; set; }

        /// <summary>
        /// Gets or sets the value of horizontal map location.
        /// </summary>
        public string? MapXLocation { get; set; }

        /// <summary>
        /// Gets or sets the value of vertical map location.
        /// </summary>
        public string? MapYLocation { get; set; }

        /// <summary>
        /// Gets or sets the value of desk width on map.
        /// </summary>
        public string? Width { get; set; }

        /// <summary>
        /// Gets or sets the value of desk height on map.
        /// </summary>
        public string? Height { get; set; }

        /// <summary>
        /// Gets or sets the value of desk status.
        /// </summary>
        public string Status { get; set; }

        #endregion
    }
}
