using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deskManagerApi.Entities.DTO.Get
{
    public class GetUserBasicInfo
    {
        #region Properties and indexers

        /// <summary>
        /// Gets or sets the ID value of user.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Gets or sets the login value of user.
        /// </summary>
        public string? Name { get; set; }

        #endregion
    }
}
