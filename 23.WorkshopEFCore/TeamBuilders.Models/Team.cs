using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TeamBuilder.Models
{
    public class Team
    {
        public Team()
        {
            this.Members=new List<UserTeam>();
            this.Events = new List<TeamEvent>();
        }
        [Key]
        [MinLength(0)]
        public int Id { get; set; }

        [MaxLength(25)]
        [Required]
        public string Name { get; set; }

        [MaxLength(32)]
        public string Description { get; set; }

        [StringLength(3,MinimumLength = 3)]
        public string Acronym { get; set; }

        [ForeignKey("Creator")]
        [MinLength(0)]
        public int CreatorId { get; set; }
        public virtual User Creator { get; set; }

        public virtual ICollection<UserTeam> Members { get; set; }

        public virtual ICollection<TeamEvent> Events { get; set; }
    }
}
