using deskManagerApi.Entities.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace deskManagerApi.Models
{
    [Table("users")]
    public class User
    {
        #region Properties and indexers

        /// <summary>
        /// Gets or sets the ID value of user.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the firstname value of user.
        /// </summary>
        [Required(ErrorMessage = "Firstname is required")]
        [StringLength(20, ErrorMessage = "Firstname can't be longer than 20 characters")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the lastname value of user.
        /// </summary>
        [Required(ErrorMessage = "Lastname is required")]
        [StringLength(20, ErrorMessage = "Lastname can't be longer than 20 characters")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the login value of user.
        /// </summary>
        [Required(ErrorMessage = "Login is required")]
        [StringLength(20, ErrorMessage = "Login can't be longer than 20 characters")]
        public string Login { get; set; }

        /// <summary>
        /// Gets or sets the password value of user.
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, ErrorMessage = "Password can't be longer than 20 characters")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the role value of user.
        /// </summary>
        public Role RoleId { get; set; }

        /// <summary>
        /// Gets or sets the value of team ID.
        /// </summary>
        [ForeignKey("Team")]
        public int? TeamId { get; set; }

        /// <summary>
        /// Gets or sets the value of team object.
        /// </summary>
        public Team? Team { get; set; }

        /// <summary>
        /// Gets or sets the collection of Items.
        /// </summary>
        public ICollection<Item> Items { get; set; } = new List<Item>();

        /// <summary>
        /// Gets or sets the collection of Issues.
        /// </summary>
        public ICollection<Issue> Issues { get; set; } = new List<Issue>();

        /// <summary>
        /// Gets or sets the collection of Issue Histories.
        /// </summary>
        public ICollection<IssueHistory> IssueHistories { get; set; } = new List<IssueHistory>();

        /// <summary>
        /// Gets or sets the collection of Reservations.
        /// </summary>
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

        #endregion
    }
}
