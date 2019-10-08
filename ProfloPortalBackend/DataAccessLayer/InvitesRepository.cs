using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using ProfloPortalBackend.BusinessLayer;
using ProfloPortalBackend.Model;

namespace ProfloPortalBackend.DataAccessLayer
{
    public class InvitesRepository: IInviteRepository
    {
        private readonly DBContext context;
       
        public InvitesRepository(DBContext dBContext)
        {
            context = dBContext;
            
        }

        public async Task<List<Invite>> CreateInvitesAsync(Team team, List<Member> invitees, Member inviter)
        {
            var invites = invitees.Select(member => new Invite() { Team = team, Invitee = member, Inviter = inviter });
            await context.Invites.InsertManyAsync(invites);
            return invites.ToList();
        }

        public async Task<List<Invite>> GetInvitesAsync(string emailId)
        {
            var filterDefinition = Builders<Invite>.Filter.Eq(x => x.Invitee.EmailId, emailId);
            var invitees =  await context.Invites.FindAsync(filterDefinition);
            return invitees.ToList();
        }

        public async Task<List<Team>> GetInvitedTeamAsync(string emailId)
        {
            var filterdef = Builders<Invite>.Filter.Eq(x => x.Invitee.EmailId, emailId);
            var Invitess =  await context.Invites.FindAsync(filterdef);

            //var filter = Builders<Team>.Filter.Eq(x => x.Invitees.ToString(), Invitess);
            //var team = await context.Teams.FindAsync(filter);
            var result = Invitess.SingleOrDefault();
            string teamId = result.Team.TeamId;

            var filterByTeamIds = Builders<Team>.Filter.Eq(x => x.TeamId, teamId );
            var teams = await context.Teams.FindAsync(filterByTeamIds);
            //var MyTeams = teams.SingleOrDefault();

            return teams.ToList();


            //return result.Team;


            //var temp = team.FirstOrDefault();
            //return temp;

        }
    }
}
