using ProfloPortalBackend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfloPortalBackend.BusinessLayer
{
     public interface ITeamService
    {
        Task<List<Team>> GetTeamsAsync(Member member);
        // Task<List<Team>> GetTeamsByIds(List<string> TeamIds);
        // ICollection<Team> GetTeamsByUserID(string userId);
        bool UpdateTeam(string teamId, Team team);
        Team GetTeamByID(string teamID);
        Task<Team> CreateTeamAsync(Team team, Member member);
        
        bool RemoveTeamById(string teamId);
        // void createMembers(string teamId, Member member);
        bool UpdateMembers(string teamId, string mid, Member member);
        bool RemoveMembers(string teamID, string mID);
        
        //void createInvite(int teamID, Invite invite);
        //bool UpdateInvite(int teamId, int inviteID, Invite invite);
        //ICollection<Invite> getTeamInvites(int teamID);
        ICollection<Member> GetTeamMembers(string teamID);
        //bool RemoveInvite(int teamId, int inviteID);


        ICollection<TeamBoard> getTeamBoards(string teamID);

        // Member GetMemberByMemberId(string teamId, string memberId);

    }
}
