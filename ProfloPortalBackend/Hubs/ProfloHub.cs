using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using ProfloPortalBackend.Model;
using System.Collections.Generic;

namespace ProfloPortalBackend.Hubs
{
    public class ProfloHub: Hub
    {
        public static Dictionary<string, List<string>> boardConnectionMapping;

        public ProfloHub()
        {
            if (boardConnectionMapping == null)
            {
                boardConnectionMapping = new Dictionary<string, List<string>>();
            }
        }
        
        public void Initialize(string boardId)
        {
            Console.WriteLine($"Board Id is {boardId}");
            if (!boardConnectionMapping.ContainsKey(boardId))
            {
                Console.WriteLine("Creating board Cnnection Mapping");
                boardConnectionMapping.Add(boardId, new List<string>());
            }
            boardConnectionMapping[boardId].Add(Context.ConnectionId);
        }

        public async Task EmitAddBoardEvent(Board board)
        {
            await Clients.All.SendAsync("BoardAdded", board);
        }

        public async Task EmitAddListEvent(List list)
        {
            Console.WriteLine("Add List Event");
            await Clients.Clients(boardConnectionMapping[list.BoardId]).SendAsync("ListAdded", list);
        }
        
        public async Task EmitAddCardEvent(Card card)
        {
            Console.WriteLine("Add List Event");
            await Clients.Clients(boardConnectionMapping[card.BoardId]).SendAsync("CardAdded", card);
        }

        public async Task EmitMoveCardEvent(MoveCardRequest moveCardRequest)
        {
            Console.WriteLine("Move Card Request");
            await Clients.Clients(boardConnectionMapping[moveCardRequest.BoardId]).SendAsync("CardMoved", moveCardRequest);
        }

        public async Task EmitMoveListEvent(MoveListRequest moveListRequest)
        {
            Console.WriteLine("Move List Request");
            await Clients.Clients(boardConnectionMapping[moveListRequest.BoardId]).SendAsync("ListMoved", moveListRequest);
        }
    }
}