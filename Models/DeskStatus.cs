using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace deskManagerApi.Models
{
    public class DeskStatus
    {
        #region Properties 

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        #endregion
    }
}
