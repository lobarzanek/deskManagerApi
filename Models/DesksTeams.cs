using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace deskManagerApi.Models
{
    public class DesksTeams
    {
        #region Properties

        [Key]
        public int Id { get; set; }

        [ForeignKey("Desk")]
        public int? DeskId { get; set; }
        public Desk? Desk { get; set; }

        [ForeignKey("Team")]
        public int? TeamId { get; set; }
        public Team? Team { get; set; }

        #endregion
    }
}
