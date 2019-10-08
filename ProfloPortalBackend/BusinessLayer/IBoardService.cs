using ProfloPortalBackend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfloPortalBackend.BusinessLayer
{
    public interface IBoardService
    {
        //List<Board> GetBoards();
        ////bool UpdateBoard(int boardId, Board board);
        //Board GetBoardByID(string boardId);
        //Task<Board> CreateBoard(Board board);
        ////bool RemoveBoard(int boardId);
        ////void createMembers(int boardId, Member member);
        ////bool UpdateMembers(int boardId, int Mid, Member member);
        ////bool RemoveMembers(int boardId, int mID);
        ////void createInvite(int boardId, Invite invite);
        ////bool UpdateInvite(int boardId, int inviteID, Invite invite);
        ////ICollection<Invite> getBoardInvites(int boardId);
        ////ICollection<Member> getBoardMembers(int boardId);
        ////bool RemoveInvite(int boardId, int inviteID);
        //ICollection<List> GetLists(string boardId);


        #region Board Methods
        Task<Board> CreateBoard(Board board);
        List<Board> GetBoards();
        Task<Board> GetBoardById(string boardId);
        bool UpdateBoard(string boardId, Board board);
        Task MoveList(MoveListRequest moveListRequest);
        bool RemoveBoard(string boardId);
        #endregion

        #region BoardMembers Methods
        void CreateMembers(string boardId, Member member);
        ICollection<Member> GetBoardMembers(string boardId);
        Member GetMemberByMemberId(string boardId, string memberId);
        bool UpdateMembers(string boardId, string memberId, Member member);
        bool RemoveMembers(string boardId, string memberId);
        #endregion

        #region BoardInvite 
        void CreateInvite(string boardId, Invite invite);
        ICollection<Invite> GetBoardInvites(string boardId);
        bool UpdateInvite(string boardId, string inviteId, Invite invite);
        bool RemoveInvite(string boardId, string inviteId);
        #endregion
        ICollection<BoardList> GetBoardLists(string boardId);



        //ICollection<List> GetLists(string boardId);
    }
}
