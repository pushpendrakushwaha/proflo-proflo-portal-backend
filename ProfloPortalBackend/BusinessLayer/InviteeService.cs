using ProfloPortalBackend.DataAccessLayer;
using ProfloPortalBackend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfloPortalBackend.BusinessLayer
{
    public class InviteService: IInviteService
    {
        public readonly IInviteRepository inviteRepository;
        public InviteService(IInviteRepository InvitesRepository)
        {
            this.inviteRepository = InvitesRepository;
        }

        public async Task<List<Invite>> CreateInvitesAsync(Team team, List<Member> invitees, Member inviter)
        {
            return await inviteRepository.CreateInvitesAsync(team, invitees, inviter);
        }

        public async Task<List<Invite>> GetInvitesAsync(string emailId)
        {
            return await inviteRepository.GetInvitesAsync(emailId);
        }

        public async Task<List<Team>> GetInvitedTeamAsync(string emailId)
        {
            return await inviteRepository.GetInvitedTeamAsync(emailId);
        }
    }
}
