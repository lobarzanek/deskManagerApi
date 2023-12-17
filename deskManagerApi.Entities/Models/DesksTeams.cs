using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace deskManagerApi.Models
{
    [Table("deskTeams")]
    public class DesksTeams
    {
        #region Properties and indexers

        /// <summary>
        /// Gets or sets the ID value of desk.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the value of desk ID.
        /// </summary>
        [ForeignKey("Desk")]
        public int? DeskId { get; set; }

        /// <summary>
        /// Gets or sets the value of desk object.
        /// </summary>
        public Desk? Desk { get; set; }

        /// <summary>
        /// Gets or sets the value of team ID.
        /// </summary>
        [ForeignKey("Team")]
        public int? TeamId { get; set; }

        /// <summary>
        /// Gets or sets the value of team object.
        /// </summary>
        public Team? Team { get; set; }

        #endregion
    }
}
