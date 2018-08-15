using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TeamBuilder.Models
{
    public class Event
    {

        public Event()
        {
            this.PerticipatingEventTeams = new List<TeamEvent>();
        }
        [MinLength(0)]
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndTime { get; set; }

        [ForeignKey("Creator")]
        [MinLength(0)]
        public int CreatorId { get; set; }
        public User Creator { get; set; }

        public virtual ICollection<TeamEvent> PerticipatingEventTeams { get; set; }
    }
}
