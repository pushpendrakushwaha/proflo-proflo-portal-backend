using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using ProfloPortalBackend.Model;

namespace ProfloPortalBackend.RealTime
{
    public class ProfloRTService : IProfloRTService
    {
        private HubConnection hubConnection;
        public ProfloRTService()
        {
            hubConnection = new HubConnectionBuilder()
                //.WithUrl("http://localhost/proflo")
                .WithUrl("http://core-api.proflo.cgi-wave7.stackroute.io/proflo")
                .Build();
        }

        private async Task InitializeConnection()
        {
            if (hubConnection.State == HubConnectionState.Disconnected)
            {
                Console.WriteLine("Initializing Connection");
                await hubConnection.StartAsync();
            }
        }

        public async Task EmitAddBoardEvent(Board board)
        {
            await InitializeConnection();
            await hubConnection.InvokeAsync("EmitAddBoardEvent", board);
        }

        public async Task EmitAddListEvent(List list)
        {
            try
            {
                Console.WriteLine("Creating List");
                await InitializeConnection();
                Console.WriteLine(list.ListId);
                await hubConnection.InvokeAsync("EmitAddListEvent", list);
                Console.WriteLine("Done with Operation");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task EmitAddCardEvent(Card card)
        {
            await InitializeConnection();
            await hubConnection.InvokeAsync("EmitAddCardEvent", card);
        }

        public async Task EmitMoveCardEvent(MoveCardRequest moveCardRequest)
        {
            await InitializeConnection();
            await hubConnection.InvokeAsync("EmitMoveCardEvent", moveCardRequest);
        }

        public async Task EmitMoveListEvent(MoveListRequest moveListRequest)
        {
            await InitializeConnection();
            Console.WriteLine("Move List Event");
            await hubConnection.InvokeAsync("EmitMoveListEvent", moveListRequest);
        }
    }
}