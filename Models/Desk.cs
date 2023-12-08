using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace deskManagerApi.Models
{
    public class Desk
    {
        #region Properties 

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? MapXLocation { get; set; } 
        public string? MapYLocation { get; set; }
        public string? MapWidth { get; set; }
        public string? MapHeight { get; set; }

        [ForeignKey("Room")]
        public int? RoomId;
        public Room? Room { get; set; }

        [ForeignKey("Status")]
        public int? StatusId;
        public DeskStatus? Status { get; set; }

        #endregion
    }
}
