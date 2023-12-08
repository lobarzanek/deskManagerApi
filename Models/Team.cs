using System.ComponentModel.DataAnnotations;

namespace deskManagerApi.Models
{
    public class Team
    {
        #region Properties

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        #endregion
    }
}
