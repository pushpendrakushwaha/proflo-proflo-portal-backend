using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using ProfloPortalBackend.DataAccessLayer;
using ProfloPortalBackend.Model;
using ProfloPortalBackend.RealTime;

namespace ProfloPortalBackend.BusinessLayer
{
    public class ListService : IListService
    {
        public readonly IListRepository listRepository;
        public readonly IProfloRTService profloRTService;
        public ListService(IListRepository listRepository, IProfloRTService profloRTService)
        {
            this.listRepository = listRepository;
            this.profloRTService = profloRTService;
        }

        #region List Operations
        public async Task CreateList(List list)
        {
            if (list.ListCards == null)
            {
                list.ListCards = new List<ListCards>();
            }
            await listRepository.CreateList(list);
            Console.WriteLine("Creating List");
            await profloRTService.EmitAddListEvent(list);
        }
        public List<List> GetList()
        {
            return listRepository.GetList();
        }
        public List GetListById(string listId)
        {
            var result = listRepository.GetListById(listId);
            return result;
        }
        public bool UpdateList(string listId, List list)
        {
            return listRepository.UpdateList(listId, list);
        }
        public bool RemoveList(string listId)
        {
            return listRepository.RemoveList(listId);
        }
        #endregion


        //public void CreateListCard(string listId, Card card)
        //{
        //    listRepository.GetListCard(listId);
        //}
        public ICollection<ListCards> GetListCards(string listId)
        {
            return listRepository.GetListCard(listId);
        }

        public Task<ICollection<Card>> LoadMore(int skip, int limit)
        {
            throw new NotImplementedException();
        }
    }
}
