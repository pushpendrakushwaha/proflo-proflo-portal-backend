using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProfloPortalBackend.DataAccessLayer;
using ProfloPortalBackend.Model;
using ProfloPortalBackend.RealTime;

namespace ProfloPortalBackend.BusinessLayer
{
    public class BoardService : IBoardService
    {
        public readonly IBoardRepository boardRepository;
        public readonly IListRepository listRepository;
        public readonly ICardRepository cardRepository;
        public readonly IProfloRTService profloRTService;
        public BoardService(IBoardRepository boardRepository, IProfloRTService profloRTService, IListRepository listRepository, ICardRepository cardRepository)
        {
            this.boardRepository = boardRepository;
            this.listRepository = listRepository;
            this.cardRepository = cardRepository;
            this.profloRTService = profloRTService;
        }

        #region Board Operations
        public async Task<Board> CreateBoard(Board board)
        {
            if (board.BoardMembers == null)
            {
                board.BoardMembers = new List<Member>();
            }
            if (board.BoardInvites == null)
            {
                board.BoardInvites = new List<Invite>();
            }
            if (board.BoardLists == null)
            {
                board.BoardLists = new List<BoardList>();
            }

            return await boardRepository.CreateBoard(board);
        }
        public List<Board> GetBoards()
        {
            return boardRepository.GetBoards();
        }
        public async Task<Board> GetBoardById(string boardId)
        {
            var board = boardRepository.GetBoardById(boardId);
            var listIds = board.BoardLists.Select(l => l.ListId).ToArray();
            var cardsByLists = await cardRepository.GetCardsByListIds(listIds.ToList());
            board.BoardLists = board.BoardLists.Select(list =>
            {
                list.Cards = cardsByLists[list.ListId];
                return list;
            }).ToList();
            return board;
        }

        public bool UpdateBoard(string boardId, Board board)
        {
            Board board1 = new Board();
            if (board.BoardMembers == null)
            {
                board.BoardMembers = board1.BoardMembers;
            }
            if (board.BoardInvites == null)
            {
                board.BoardInvites = board1.BoardInvites;
            }
            if (board.BoardLists == null)
            {
                board.BoardLists = board1.BoardLists;
            }
            return boardRepository.UpdateBoard(boardId, board);
        }

        public async Task MoveList(MoveListRequest moveListRequest)
        {
            Console.WriteLine("Moving List Request");
            var board = boardRepository.GetBoardById(moveListRequest.BoardId);
            var list = board.BoardLists[moveListRequest.FromListPosition];
            board.BoardLists.RemoveAt(moveListRequest.FromListPosition);
            board.BoardLists.Insert(moveListRequest.ToListPosition, list);
            var updateStatus = this.UpdateBoard(moveListRequest.BoardId, board);
            Console.WriteLine(updateStatus);
            await profloRTService.EmitMoveListEvent(moveListRequest);
        }

        public bool RemoveBoard(string boardId)
        {
            return boardRepository.RemoveBoard(boardId);
        }

        #endregion


        #region BoardMembers
        public void CreateMembers(string boardId, Member member)
        {
            boardRepository.CreateMembers(boardId, member);
        }
        public ICollection<Member> GetBoardMembers(string boardId)
        {
            return boardRepository.GetBoardMembers(boardId);
        }
        public Member GetMemberByMemberId(string boardId, string memberId)
        {
            return boardRepository.GetMemberByMemberId(boardId, memberId);
        }
        public bool UpdateMembers(string boardId, string Mid, Member member)
        {
            return boardRepository.UpdateMembers(boardId, Mid, member);
        }
        public bool RemoveMembers(string boardId, string MId)
        {
            return boardRepository.RemoveMembers(boardId, MId);
        }
        #endregion


        #region BoardInvitee
        public void CreateInvite(string boardId, Invite invite)
        {
            boardRepository.CreateInvite(boardId, invite);
        }
        public ICollection<Invite> GetBoardInvites(string boardId)
        {
            return boardRepository.GetBoardInvites(boardId);
        }
        public bool UpdateInvite(string boardId, string inviteId, Invite invite)
        {
            return boardRepository.UpdateInvite(boardId, inviteId, invite);
        }
        public bool RemoveInvite(string boardId, string inviteId)
        {
            return boardRepository.RemoveInvite(boardId, inviteId);
        }
        #endregion
        public ICollection<BoardList> GetBoardLists(string boardId)
        {
            return boardRepository.GetBoardLists(boardId);
        }

    }
}
