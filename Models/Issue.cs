using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace deskManagerApi.Models
{
    public class Issue
    {
        #region Properties

        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public IssueStatus Status { get; set; }

        [ForeignKey("Desk")]
        public int? DeskId { get; set; }
        public Desk? Desk { get; set; }

        [ForeignKey("Reporter")]
        public int? ReporterId { get; set; }
        public User? Reporter { get; set; }

        #endregion
    }
}
