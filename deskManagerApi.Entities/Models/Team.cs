using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace deskManagerApi.Models
{
    [Table("teams")]
    public class Team
    {
        #region Properties and indexers

        /// <summary>
        /// Gets or sets the ID value of team.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name value of team.
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the collection of DesksTeams.
        /// </summary>
        public ICollection<DesksTeams> DesksTeams { get; set; } = new List<DesksTeams>();

        /// <summary>
        /// Gets or sets the collection of users.
        /// </summary>
        public ICollection<User> Users { get; set; } = new List<User>();

        #endregion
    }
}
