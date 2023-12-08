using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace deskManagerApi.Models
{
    public class Floor
    {
        #region Properties 

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("Building")]
        public int? BuildingId { get; set; }
        public Building? Building { get; set; }

        #endregion
    }
}
