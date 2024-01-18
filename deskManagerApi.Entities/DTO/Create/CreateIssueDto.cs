using deskManagerApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deskManagerApi.Entities.DTO.Create
{
    public class CreateIssueDto
    {
        #region Properties and indexers

        /// <summary>
        /// Gets or sets the description value of issue.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the status value of issue.
        /// </summary>
        public IssueStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the value of desk ID.
        /// </summary>
        public int DeskId { get; set; }

        /// <summary>
        /// Gets or sets the value of user ID who reports the issue.
        /// </summary>
        public int ReporterId { get; set; }

        #endregion
    }
}
