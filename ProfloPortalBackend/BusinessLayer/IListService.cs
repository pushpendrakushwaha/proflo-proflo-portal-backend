using ProfloPortalBackend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfloPortalBackend.BusinessLayer
{
    public interface IListService
    {
        Task CreateList(List list);
        List<List> GetList();
        bool UpdateList(string listId, List list);
        List GetListById(string listId);
        bool RemoveList(string listId);
      //  ICollection<Card> GetListCard(string listId);
        //void CreateListCard(string listId, Card card);
        ICollection<ListCards> GetListCards(string listId);

        Task<ICollection<Card>> LoadMore(int skip, int limit);
    }
}
