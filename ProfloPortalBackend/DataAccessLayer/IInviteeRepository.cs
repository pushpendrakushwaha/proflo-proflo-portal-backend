using ProfloPortalBackend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfloPortalBackend.DataAccessLayer
{
     public  interface IInviteRepository
    {
        Task<List<Invite>> CreateInvitesAsync(Team team, List<Member> invitees, Member inviter);
        Task<List<Invite>> GetInvitesAsync(string emailId);
        Task<List<Team>> GetInvitedTeamAsync(string emailId);
    }
}
