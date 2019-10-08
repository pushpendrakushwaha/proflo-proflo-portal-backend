using ProfloPortalBackend.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfloPortalBackend.DataAccessLayer
{
    public interface ITeamRepository
    {
        List<Team> GetTeams();
        bool UpdateTeam(string teamId, Team team);
        Team GetTeamByID(string teamId);
        Task<Team> CreateTeamAsync(Team team);
        Task<List<Team>> GetTeamsByIds(List<string> teamIds);
        bool RemoveTeam(string teamId);
        // void CreateMembers(string teamId, Member member);
        bool UpdateMembers(string teamId, string mid, Member member);
        bool RemoveMembers(string teamId, string mID);
        // void CreateInvite(string teamId, Invitee invite);
        //  bool UpdateInvite(string teamId, int inviteID, Invitee invite);
        // ICollection<Invitee> GetTeamInvites(string teamId);
        ICollection<Member> GetTeamMembers(string teamId);
        // bool RemoveInvite(string teamId, int inviteID);

        ICollection<TeamBoard> getTeamBoards(string teamID);
        Member GetMemberByMemberId(string teamId, string memberId);

    }
}
