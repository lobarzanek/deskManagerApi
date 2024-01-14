using deskManagerApi.Entities.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace deskManagerApi.Models
{
    [Table("desks")]
    public class Desk
    {
        #region Properties and indexers

        /// <summary>
        /// Gets or sets the ID value of desk.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name value of desk.
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value of horizontal map location.
        /// </summary>
        public string? MapXLocation { get; set; }

        /// <summary>
        /// Gets or sets the value of vertical map location.
        /// </summary>
        public string? MapYLocation { get; set; }

        /// <summary>
        /// Gets or sets the value of desk width on map.
        /// </summary>
        public string? Width { get; set; }

        /// <summary>
        /// Gets or sets the value of desk height on map.
        /// </summary>
        public string? Height { get; set; }

        /// <summary>
        /// Gets or sets the value of room ID.
        /// </summary>
        [ForeignKey("Room")]
        public int? RoomId;

        /// <summary>
        /// Gets or sets the value of room object.
        /// </summary>
        public Room? Room { get; set; }

        /// <summary>
        /// Gets or sets the value of status object.
        /// </summary>
        public DeskStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the collection of DesksTeams.
        /// </summary>
        public ICollection<DesksTeams> DesksTeams { get; set; } = new List<DesksTeams>();

        /// <summary>
        /// Gets or sets the collection of Issues.
        /// </summary>
        public ICollection<Issue> Issues { get; set; } = new List<Issue>();

        /// <summary>
        /// Gets or sets the collection of Items.
        /// </summary>
        public ICollection<Item> Items { get; set; } = new List<Item>();

        /// <summary>
        /// Gets or sets the collection of Reservations.
        /// </summary>
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

        #endregion
    }
}
