using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TeamBuilder.Models.Enums;

namespace TeamBuilder.Models
{
    public class User
    {
        public User()
        {
            this.MemberOf=new List<UserTeam>();
            this.CreatedEvent =new List<Event>();
            this.CreatedUserTeams =new List<UserTeam>();
            this.RecievedInvitations = new List<Invitation>();
        }

        [Key]
        [MinLength(0)]
        public int Id { get; set; }

        [Required]
        [StringLength(25,MinimumLength = 3)]
        public string Username { get; set; }

        [MaxLength(25)]
        public string FirstName { get; set; }

        [MaxLength(25)]
        public string LastName { get; set; }

        [StringLength(30,MinimumLength = 6)]
        //TODO uperrletter
        [Required]
        public string Password { get; set; }

        [MinLength(0)]
        public int Age { get; set; }

        public bool IsDeleted { get; set; }

        public GenderType GenderType { get; set; }

        public virtual ICollection<Event> CreatedEvent { get; set; }

        public virtual ICollection<UserTeam> MemberOf { get; set; }

        public virtual ICollection<UserTeam> CreatedUserTeams { get; set; }

        public virtual ICollection<Invitation> RecievedInvitations { get; set; }
    }
}
