using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace deskManagerApi.Models
{
    public class IssueHistory
    {
        #region Properties

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ChangeDate { get; set; } = DateTime.Now;
        public IssueStatus StatusFrom { get; set; }
        public IssueStatus StatusTo { get; set; }

        [ForeignKey("User")]
        public int? ChangedBy { get; set; }
        public User? User { get; set; }

        [ForeignKey("Issue")]
        public int? IssueId { get; set; }
        public Issue? Issue { get; set; }

        #endregion
    }
}
