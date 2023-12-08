using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace deskManagerApi.Models
{
    public class Room
    {
        #region Properties 

        [Key]
        public int Id { get; set; }        
        public string Name { get; set; }
        public string? mapViewBox { get; set; }
        public string? mapWidth { get; set; }
        public string? mapHeight { get; set; }
        public string mapXmlns { get; set; } = "http://www.w3.org/2000/svg";

        [ForeignKey("Floor")]
        public int? FloorId { get; set; }
        public Floor? Floor { get; set; }

        #endregion
    }
}
