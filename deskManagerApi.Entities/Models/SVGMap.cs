using deskManagerApi.Entities.DTO.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deskManagerApi.Entities.Models
{
    public class SVGMap
    {
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
        /// Gets or sets the value of map height.
        /// </summary>
        public ICollection<GetDeskMapViewDto> Desks { get; set; } = new List<GetDeskMapViewDto>();
    }
}
