using deskManagerApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deskManagerApi.Entities.DTO.Update
{
    public class UpdateIssueDto
    {
        #region Properties and indexers

        /// <summary>
        /// Gets or sets the ID value of issue.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the description value of issue.
        /// </summary>
        [Required(ErrorMessage = "Description is required")]
        [StringLength(1024, ErrorMessage = "Description can't be longer than 1024 characters")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the status value of issue.
        /// </summary>
        public IssueStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the value of desk ID.
        /// </summary>
        [ForeignKey("Desk")]
        public int? DeskId { get; set; }

        /// <summary>
        /// Gets or sets the value of user ID who reports the issue.
        /// </summary>
        [ForeignKey("Reporter")]
        public int? ReporterId { get; set; }

        #endregion
    }
}
