using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProfloPortalBackend.Model;
using MongoDB.Driver;
using MongoDB.Bson;

namespace ProfloPortalBackend.DataAccessLayer
{
    public class TeamImplements : ITeamRepository
    {
        private readonly DBContext context;
        public TeamImplements(DBContext dBContext)
        {
            context = dBContext;
        }

        //public void createInvite(string teamID,Invitee invite)
        //{
        //    var filter = Builders<Team>.Filter.Eq(c => c.TeamId, teamID);
        //    var update = Builders<Team>.Update.Push(c => c., invite);
        //    context.Teams.FindOneAndUpdate(filter, update);
        //}

        // public void CreateMembers(string teamID, Member member)
        // {
        //     member.MemberId = ObjectId.GenerateNewId().ToString();
        //     var filter = Builders<Team>.Filter.Eq(c => c.TeamId, teamID);
        //     var update = Builders<Team>.Update.Push(c => c.TeamMembers, member);
        //     context.Teams.FindOneAndUpdate(filter, update);
        // }

        #region TeamOperations

            // Team Creation
        public async Task<Team> CreateTeamAsync(Team team)
        {
            team.TeamBoards = new List<TeamBoard>();
            Console.WriteLine(team.Name);
            Console.WriteLine(team.TeamId);
            await context.Teams.InsertOneAsync(team);
            return team;
        }

        // Get TeamsByIDs

        public async Task<List<Team>> GetTeamsByIds(List<string> teamIds)
        {
            var filterByTeamIds = Builders<Team>.Filter.In(x => x.TeamId, teamIds);
            var teams = await context.Teams.FindAsync(filterByTeamIds);
            return teams.ToList();
        }

        #endregion

        // Get MemberbyMemberId
        public Member GetMemberByMemberId(string teamId, string memberId)
        {
            Team team = context.Teams.Find(c => c.TeamId== teamId).First();
            Member member = team.TeamMembers.Find(m => m.MemberId == memberId);
            return member;
        }

        //public void CreateTeamBoard(string teamID, Board board)
        //{
        //    TeamBoard teamBoard = new TeamBoard();
        //    teamBoard.BoardId = board.BId;
        //    teamBoard.BoardName = board.BoardName;
        //    teamBoard.Description = board.Description;
        //    var filter = Builders<Team>.Filter.Eq(c => c.TeamId, teamID);
        //    var update = Builders<Team>.Update.Push(c => c.TeamBoards, teamBoard);
        //    context.Teams.FindOneAndUpdate(filter, update);
        //}


        // Get TeamBoards
        public ICollection<TeamBoard> getTeamBoards(string teamID)
        {
            //teamBoard teamBoard = context.Teams.Find(n => n.teamID == teamID).First();
            Team team = context.Teams.Find(n => n.TeamId == teamID).First();
            return team.TeamBoards;
        }


        // Get TeamByID
        public Team GetTeamByID(string teamID)
        {
            Team team = context.Teams.Find(n => n.TeamId == teamID).First();
            return team;
        }



        //public ICollection<Invitee> getTeamInvites(int teamID)
        //{
        //    Team team = context.Teams.Find(n => n.teamId == teamID).First();
        //    return team.teamInvites;
        //}

        
        // Get TeamMembers
        public ICollection<Member> GetTeamMembers(string teamID)
        {
            Team team = context.Teams.Find(n => n.TeamId == teamID).First();
            return team.TeamMembers;
        }

        // Get Teams
        public List<Team> GetTeams()
        {
            return context.Teams.Find(_ => true).ToList();
        }

        //public bool RemoveInvite(int teamId,int inviteID)
        //{
        //    Team GetTeam = context.Teams.Find(n => n.TeamID == teamId).First();
        //    invitee _invite = GetTeam.teamInvites.Find(n => n.inviteID == inviteID);
        //    var filter = Builders<Team>.Filter.Eq(n => n.teamID, teamId);
        //    var delete = Builders<Team>.Update.Pull(n => n.teamInvites, _invite);
        //    var updatedResult = context.Teams.UpdateOneAsync(filter, delete).Result;
        //    return updatedResult.IsAcknowledged && updatedResult.ModifiedCount > 0;
        //}

        
        public bool RemoveMembers(string teamID, string mID)
        {
            Team GetTeam = context.Teams.Find(n => n.TeamId == teamID).First();
            Member _member = GetTeam.TeamMembers.First(n => n.MemberId == mID);
            var filter = Builders<Team>.Filter.Eq(n => n.TeamId, teamID);
            var delete = Builders<Team>.Update.Pull(n => n.TeamMembers, _member);
            var updatedResult = context.Teams.UpdateOneAsync(filter, delete).Result;
            return updatedResult.IsAcknowledged && updatedResult.ModifiedCount > 0;
        }

        public bool RemoveTeam(string teamID)
        {
            var deletedResult = context.Teams.DeleteOne(c => c.TeamId == teamID);
            return deletedResult.IsAcknowledged && deletedResult.DeletedCount > 0;
        }

        //public bool UpdateInvite(int teamId,int inviteID, invitee invite)
        //{
        //    Team GetTeam = context.Teams.Find(n => n.teamID == teamId).First();
        //    invitee _invite = GetTeam.teamInvites.Find(n => n.inviteID == inviteID);
        //    var filter = Builders<Team>.Filter.Eq(n => n.teamID, teamId);
        //    var update = Builders<Team>.Update.Set(n => n.teamInvites.Find(b=>b.inviteID==inviteID),_invite);
        //    var updatedResult = context.Teams.UpdateOneAsync(filter, update).Result;
        //    return updatedResult.IsAcknowledged && updatedResult.ModifiedCount > 0;
        //}
        
        public bool UpdateMembers(string teamId, string mid, Member member)
        {
            //Team GetTeam = context.Teams.Find(n => n.teamID == teamId).First();
            //Member _member = GetTeam.teamMembers.First(n => n.Mid ==mid );
            //var filter = Builders<Team>.Filter.Eq(n => n.teamID, teamId);
            //var delete = Builders<Team>.Update.Pull(n => n.teamMembers, _member);
            //context.Teams.FindOneAndUpdate(filter, delete);
            // var profile = context.Teams.Find(n => n.TeamId == teamId).FirstOrDefault();
            // var member1 = profile.TeamMembers.Find(n => n.MemberId == mid);
            // var update = Builders<Team>.Update.Push(c => c.TeamMembers, member1);
            var filter = Builders<Team>.Filter.Where(n => n.TeamId == teamId && n.TeamMembers.Any(t => t.MemberId == mid));
            // var team = context.Teams.Find(filter).FirstOrDefault();
            // var memberToBeRemoved = team.TeamMembers.Where(m => m.MemberId == mid).FirstOrDefault();
            // team.TeamMembers.Remove(memberToBeRemoved);
            // team.TeamMembers.Add(member);
            // var updateMembersOfTeam = Builders<Team>.Update.Set(x => x.TeamMembers, team.TeamMembers);
            // var updatedResult = context.Teams.UpdateOne(filter, updateMembersOfTeam);

            var updateMemberOfTeam = Builders<Team>.Update.Set(x => x.TeamMembers[-1], member);
            var updatedResult = context.Teams.UpdateOne(filter, updateMemberOfTeam);

            return updatedResult.IsAcknowledged && updatedResult.ModifiedCount > 0;
        }



        public bool UpdateTeam(string teamId, Team team)
        {
            var filter = Builders<Team>.Filter.Where(c => c.TeamId == teamId);
            var updatedResult = context.Teams.ReplaceOne(filter, team);
            return updatedResult.IsAcknowledged && updatedResult.ModifiedCount > 0;
        }


    }
}
