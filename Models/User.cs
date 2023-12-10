using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace deskManagerApi.Models
{
    public class User
    {
        #region Properties

        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public Role RoleId { get; set; }

        [ForeignKey("Team")]
        public int? TeamId { get; set; }
        public Team? Team { get; set; }
        public ICollection<Item> Items { get; set; }
        public ICollection<Issue> Issues { get; set; }
        public ICollection<IssueHistory> IssueHistories { get; set; }

        #endregion
    }
}
