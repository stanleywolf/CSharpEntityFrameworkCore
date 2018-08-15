using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilders.Utilities
{
    public static class CommandHelper
    {       

        public static bool IsTeamExisting(string teamName)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Teams.SingleOrDefault(x => x.Name == teamName) == null ? false : true;
            }
            
        }

        public static bool IsUserExisting(string username)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Users.SingleOrDefault(x => x.Username == username) == null ? false : true;
            }
            
        }

        public static bool IsInviteExisting(string teamName, User user)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return user.RecievedInvitations.SingleOrDefault(x => x.Team.Name == teamName) == null ? false : true;
            }
        }

        public static bool IsUserCreatorOfTeam(string teamName, User user)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return user.CreatedUserTeams.SingleOrDefault(x => x.Team.Name == teamName) == null ? false : true;
            }           
        }

        public static bool IsUserCreatorOfEvent(string eventName, User user)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return user.CreatedEvent.SingleOrDefault(x => x.Name == eventName) == null ? false : true;
            }
            
        }
        //TODO
        public static bool IsMemberOfTeam(string teamName, string username)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Teams.Find(teamName).Members.Any(x => x.User.Username == username);
            }          
        }

        public static bool IsEventExisting(string eventName)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
            return context.Events.Any(x => x.Name == eventName);

            }
        }

    }
}
