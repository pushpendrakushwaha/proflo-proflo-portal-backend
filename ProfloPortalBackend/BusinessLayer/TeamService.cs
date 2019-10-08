using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProfloPortalBackend.DataAccessLayer;
using ProfloPortalBackend.Model;

namespace ProfloPortalBackend.BusinessLayer
{
    public class TeamService : ITeamService
    {
        public readonly ITeamRepository teamRepository;
        public readonly IMemberService memberService;
        public readonly IInviteService inviteService;
        public TeamService(ITeamRepository teamInterface, IMemberService memberService, IInviteService inviteService)
        {
            this.teamRepository = teamInterface;
            this.inviteService = inviteService;
            this.memberService = memberService;
        }
        public void createInvite(int teamID, Invite invite)
        {
            throw new NotImplementedException();
        }

        // public void createMembers(string teamId, Member member)
        // {
        //     teamRepository.CreateMembers(teamId, member);
        // }

        public async Task<Team> CreateTeamAsync(Team team, Member member)
        {
            var createdMember = await this.memberService.CreateMemberIfNotExistsAsync(member);
            var teamMember = new Member()
            {
                MemberId = createdMember.MemberId,
                MemberName = createdMember.MemberName,
                UserId = createdMember.UserId,
                EmailId = createdMember.EmailId
            };

            if (team.TeamMembers == null)
            {
                team.TeamMembers = new List<Member>() { };
            }
            team.TeamMembers.Add(teamMember);

            var createdTeam = await teamRepository.CreateTeamAsync(team);
            var teamInvitedTo = new Team
            {
                TeamId = createdTeam.TeamId,
                Name = createdTeam.Name,
                Description = createdTeam.Description
            };
            var inviter = new Member()
            {
                UserId = createdMember.UserId,
                EmailId = createdMember.EmailId,
                MemberName = createdMember.MemberName,
            };
            
            await inviteService.CreateInvitesAsync(teamInvitedTo, team.Invitees, inviter);
            if (createdMember.Teams == null)
            {
                createdMember.Teams = new List<Team>();
            }
            
            var memberTeam = new Team()
            {
                TeamId = createdTeam.TeamId,
                Name = createdTeam.Name,
                Description = createdTeam.Description,
            };

            createdMember.Teams.Add(memberTeam);
            await this.memberService.UpdateMemberAsync(createdMember);
            return createdTeam;
        }

        public async Task<List<Team>> GetTeamsAsync(Member member)
        {
            Console.WriteLine(member);
            var existingMember = await this.memberService.GetMemberByUserIdAsync(member.UserId);
            if (existingMember != null)
            {
                var teamIds = existingMember.Teams.Select(t => t.TeamId).ToList();
                return await teamRepository.GetTeamsByIds(teamIds);
            }
            else
            {
                return new List<Team>();
            }
        }

        public ICollection<TeamBoard> getTeamBoards(string teamID)
        {
            return teamRepository.getTeamBoards(teamID);
        }

        public Team GetTeamByID(string teamID)
        {
            var result = teamRepository.GetTeamByID(teamID);
            return result;
        }

        public ICollection<Member> GetTeamMembers(string teamID)
        {
            return teamRepository.GetTeamMembers(teamID);
        }

        public Member GetMemberByMemberId(string teamId, string memberId)
        {
            return teamRepository.GetMemberByMemberId(teamId, memberId);
        }

        public bool RemoveMembers(string teamID, string mID)
        {
            return teamRepository.RemoveMembers(teamID, mID);
        }

        public bool RemoveTeamById(string teamId)
        {
            return teamRepository.RemoveTeam(teamId);
        }

        public bool UpdateMembers(string teamId, string mid, Member member)
        {
            return teamRepository.UpdateMembers(teamId, mid, member);
        }

        public bool UpdateTeam(string teamId, Team team)
        {
            return teamRepository.UpdateTeam(teamId, team);
        }

        // public ICollection<Team> GetTeamsByUserID(string userId)
        // {
        //     var member = memberService.GetMemberByUserIdAsync(userId);
        // }
    }
}
