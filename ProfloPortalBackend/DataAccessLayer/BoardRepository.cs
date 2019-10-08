using ProfloPortalBackend.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace ProfloPortalBackend.DataAccessLayer
{

    public class BoardRepository : IBoardRepository
    {
        private readonly DBContext context;
        public BoardRepository(DBContext dBContext)
        {
            context = dBContext;
        }

        #region Board Operations
        public async Task<Board> CreateBoard(Board board)
        {
            //board.BoardLists = new List<List>();
            await context.Boards.InsertOneAsync(board);

            var teamBoard = new TeamBoard()
            {
                // BoardId = "",
                // BoardName = "",
                // Description = "",
                BoardId = board.BoardId,
                BoardName = board.BoardName,
                Description = board.Description,
            };
            var filter = Builders<Team>.Filter.Eq(c => c.TeamId, board.TeamId);
            var update = Builders<Team>.Update.Push(c => c.TeamBoards, teamBoard);
            var updateTeam = context.Teams.FindOneAndUpdateAsync(filter, update);
            await updateTeam;
            return board;
        }
        public Board GetBoardById(string boardId)
        {
            Board board = context.Boards.Find(n => n.BoardId == boardId).First();
            return board;
        }
        public List<Board> GetBoards()
        {
            return context.Boards.Find(_ => true).ToList();
        }
        
        public bool UpdateBoard(string boardId, Board board)
        {
            var filter = Builders<Board>.Filter.Eq(c => c.BoardId, boardId);
            var updatedResult = context.Boards.ReplaceOne(filter, board);
            Console.WriteLine(updatedResult.IsAcknowledged);
            Console.WriteLine(updatedResult.IsModifiedCountAvailable);
            Console.WriteLine(updatedResult.MatchedCount);
            Console.WriteLine(updatedResult.ModifiedCount);
            return updatedResult.IsAcknowledged && updatedResult.ModifiedCount > 0;
        }

        public bool RemoveBoard(string boardId)
        {
            var deletedResult = context.Boards.DeleteOne(c => c.BoardId == boardId);
            return deletedResult.IsAcknowledged && deletedResult.DeletedCount > 0;
        }
        #endregion

        #region Board Members
        public void CreateMembers(string boardId, Member member)
        {
            member.MemberId = ObjectId.GenerateNewId().ToString();
            var filter = Builders<Board>.Filter.Eq(c => c.BoardId, boardId);
            var update = Builders<Board>.Update.Push(c => c.BoardMembers, member);
            context.Boards.FindOneAndUpdate(filter, update);
        }
        public ICollection<Member> GetBoardMembers(string boardId)
        {
            Board board = context.Boards.Find(n => n.BoardId == boardId).First();
            return board.BoardMembers;
        }
        public Member GetMemberByMemberId(string boardId, string memberId)
        {
            Board board = context.Boards.Find(c => c.BoardId == boardId).First();
            Member member = board.BoardMembers.Find(m => m.MemberId == memberId);
            return member;
        }
        public bool UpdateMembers(string boardId, string Mid, Member member)
        {
            var filter = Builders<Board>.Filter.Eq(n => n.BoardId, boardId);
            var update = Builders<Board>.Update.Set(e => e.BoardMembers.Find(n => n.MemberId == Mid), member);
            var updatedResult = context.Boards.UpdateOneAsync(filter, update).Result;
            return updatedResult.IsAcknowledged && updatedResult.ModifiedCount > 0;
        }
        public bool RemoveMembers(string boardId, string mID)
        {
            Board GetBoard = context.Boards.Find(n => n.BoardId == boardId).First();
            Member _member = GetBoard.BoardMembers.First(n => n.MemberId == mID);
            var filter = Builders<Board>.Filter.Eq(n => n.BoardId, boardId);
            var delete = Builders<Board>.Update.Pull(n => n.BoardMembers, _member);
            var updatedResult = context.Boards.UpdateOneAsync(filter, delete).Result;
            return updatedResult.IsAcknowledged && updatedResult.ModifiedCount > 0;
        }
        #endregion


        #region BoardInvitee
        public void CreateInvite(string boardId, Invite invite)
        {
            invite.InviteId = ObjectId.GenerateNewId().ToString();
            var filter = Builders<Board>.Filter.Eq(c => c.BoardId, boardId);
            var update = Builders<Board>.Update.Push(c => c.BoardInvites, invite);
            context.Boards.FindOneAndUpdate(filter, update);
        }
        public ICollection<Invite> GetBoardInvites(string boardId)
        {
            Board board = context.Boards.Find(n => n.BoardId == boardId).First();
            return board.BoardInvites;
        }
        public bool UpdateInvite(string boardId, string inviteID, Invite invte)
        {
            Board GetBoard = context.Boards.Find(n => n.BoardId == boardId).First();
            Invite _invite = GetBoard.BoardInvites.Find(n => n.InviteId == inviteID);
            var filter = Builders<Board>.Filter.Eq(n => n.BoardId, boardId);
            var update = Builders<Board>.Update.Set(n => n.BoardInvites.Find(b => b.InviteId == inviteID), _invite);
            var updatedResult = context.Boards.UpdateOneAsync(filter, update).Result;
            return updatedResult.IsAcknowledged && updatedResult.ModifiedCount > 0;
        }

        public bool RemoveInvite(string boardId, string inviteID)
        {
            Board GetBoard = context.Boards.Find(n => n.BoardId == boardId).First();
            Invite _invite = GetBoard.BoardInvites.Find(n => n.InviteId == inviteID);
            var filter = Builders<Board>.Filter.Eq(n => n.BoardId, boardId);
            var delete = Builders<Board>.Update.Pull(n => n.BoardInvites, _invite);
            var updatedResult = context.Boards.UpdateOneAsync(filter, delete).Result;
            return updatedResult.IsAcknowledged && updatedResult.ModifiedCount > 0;
        }
        #endregion
        public ICollection<BoardList> GetBoardLists(string boardId)
        {
            //teamBoard teamBoard = context.Teams.Find(n => n.teamID == teamID).First();
            Board board = context.Boards.Find(n => n.BoardId == boardId).First();
            return board.BoardLists;
        }

        //public ICollection<List> GetLists(string boardId)
        //{
        //    Board board = context.Boards.Find(n => n.BoardId == boardId).First();
        //    return board.BoardLists;
        //}


    }
}

