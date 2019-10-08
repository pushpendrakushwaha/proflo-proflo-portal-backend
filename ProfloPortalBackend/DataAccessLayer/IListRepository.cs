using ProfloPortalBackend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfloPortalBackend.DataAccessLayer
{
    public interface IListRepository
    {
        //    List<List> GetList();
        //    //bool UpdateList(string listId, List list);
        //    List GetListByID(string listId);
        //    Task CreateList(string boardID,List list);
        //    Task<ICollection<Card>> loadMore(int skip, int limit);
        //   // bool RemoveList(string listId);
        //    ICollection<Card> getListCard(string listId);
        //   // void CreateListCard(int listId, Card card);

        Task CreateList(List list);
        List<List> GetList();
        bool UpdateList(string listId, List list);
        List GetListById(string listId);
        bool RemoveList(string listId);
        ICollection<ListCards> GetListCard(string listId);
        //void CreateListCard(string listId, Card card);
        Task<ICollection<Card>> LoadMore(int skip, int limit);
    }
}
