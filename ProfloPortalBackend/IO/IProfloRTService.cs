using System.Threading.Tasks;
using ProfloPortalBackend.Model;

namespace ProfloPortalBackend.RealTime
{
    public interface IProfloRTService
    {
        Task EmitAddBoardEvent(Board board);
        Task EmitAddListEvent(List list);
        Task EmitAddCardEvent(Card card);
        Task EmitMoveCardEvent(MoveCardRequest moveCardRequest);
        Task EmitMoveListEvent(MoveListRequest moveListRequest);
    }
}