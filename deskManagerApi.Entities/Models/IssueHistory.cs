using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace deskManagerApi.Models
{
    [Table("issueHistories")]
    public class IssueHistory
    {

        #region Properties and indexers

        /// <summary>
        /// Gets or sets the ID value of desk.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the date value of change.
        /// </summary>
        public DateTime ChangeDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the value of previous issue status.
        /// </summary>
        public IssueStatus StatusFrom { get; set; }

        /// <summary>
        /// Gets or sets the value of current issue status.
        /// </summary>
        public IssueStatus StatusTo { get; set; }

        /// <summary>
        /// Gets or sets the value of user ID who change the status.
        /// </summary>
        [ForeignKey("User")]
        public int? ChangedBy { get; set; }

        /// <summary>
        /// Gets or sets the value of user object who change the status.
        /// </summary>
        public User? User { get; set; }

        /// <summary>
        /// Gets or sets the value of issue ID.
        /// </summary>
        [ForeignKey("Issue")]
        public int? IssueId { get; set; }

        /// <summary>
        /// Gets or sets the value of issue object.
        /// </summary>
        public Issue? Issue { get; set; }

        #endregion
    }
}
