using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace deskManagerApi.Models
{
    [Table("issues")]
    public class Issue
    {
        #region Properties and indexers

        /// <summary>
        /// Gets or sets the ID value of issue.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the description value of issue.
        /// </summary>
        [Required(ErrorMessage = "Description is required")]
        [StringLength(1024, ErrorMessage = "Description can't be longer than 1024 characters")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the status value of issue.
        /// </summary>
        public IssueStatus Status { get; set; }

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
        /// Gets or sets the value of user ID who reports the issue.
        /// </summary>
        [ForeignKey("Reporter")]
        public int? ReporterId { get; set; }

        /// <summary>
        /// Gets or sets the value of user object who reports the issue.
        /// </summary>
        public User? Reporter { get; set; }

        /// <summary>
        /// Gets or sets the collection of issue history.
        /// </summary>
        public ICollection<IssueHistory> History { get; set; } = new List<IssueHistory>();

        #endregion
    }
}
