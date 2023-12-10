using System.ComponentModel.DataAnnotations;

namespace deskManagerApi.Models
{
    public class Brand
    {
        #region Properties

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Item> Items { get; set; }

        #endregion
    }
}
