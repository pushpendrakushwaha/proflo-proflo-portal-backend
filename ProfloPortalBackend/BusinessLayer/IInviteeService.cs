using ProfloPortalBackend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfloPortalBackend.BusinessLayer
{
    public interface IInviteService
    {
        Task<List<Invite>> CreateInvitesAsync(Team team, List<Member> invites, Member inviter);
        Task<List<Invite>> GetInvitesAsync(string emailId);
        Task<List<Team>> GetInvitedTeamAsync(string emailId);
    }
}
