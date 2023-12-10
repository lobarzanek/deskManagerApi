using deskManagerApi.Models;
using System.ComponentModel.DataAnnotations;

namespace deskManagerApi.DTO.Get
{
    public class GetBrandDto
    {
        #region Properties

        public int Id { get; set; }
        public string Name { get; set; }

        #endregion
    }
}
