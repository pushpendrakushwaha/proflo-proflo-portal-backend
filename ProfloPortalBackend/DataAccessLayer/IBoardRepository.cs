using ProfloPortalBackend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfloPortalBackend.DataAccessLayer
{
     public interface IBoardRepository
    {
        //List<Board> GetBoards();
        ////bool UpdateBoard(string boardId, Board board);
        //Board GetBoardByID(string boardId);
        //Task<Board> CreateBoard(Board board);
        ////bool RemoveBoard(string boardId);
        ////void CreateMembers(string boardId, Member member);
        ////bool UpdateMembers(string boardId, string memberId, Member member);
        ////bool RemoveMembers(string boardId, string memberId);
        ////void CreateInvite(string boardId, Invitee invite);
        ////bool UpdateInvite(string boardId, string inviteId, Invitee invite);
        ////ICollection<Invitee> GetBoardInvites(string boardId);
        ////ICollection<Member> GetBoardMembers(string boardId);
        ////bool RemoveInvite(string boardId, string inviteId);
        //ICollection<List> GetLists(string boardId);

        #region Board Methods
        Task<Board> CreateBoard(Board board);
        List<Board> GetBoards();
        Board GetBoardById(string boardId);
        bool UpdateBoard(string boardId, Board board);
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
