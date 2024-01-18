using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deskManagerApi.Entities.DTO.Get
{
    public class GetDeskBasicInfo
    {
        #region Properties and indexers

        /// <summary>
        /// Gets or sets the ID value of desk.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name value of desk.
        /// </summary>
        public string Name { get; set; }

        #endregion
    }
}
