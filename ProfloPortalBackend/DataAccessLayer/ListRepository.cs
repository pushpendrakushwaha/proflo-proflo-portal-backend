using System;
using System.Linq;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;

using ProfloPortalBackend.Model;
using MongoDB.Bson;

namespace ProfloPortalBackend.DataAccessLayer
{
    public class ListRepository : IListRepository
    {
        private readonly DBContext context;
        public ListRepository(DBContext dBContext)
        {
            context = dBContext;
        }
        #region List Operations
        public async Task CreateList(List list)
        {
            await context.Lists.InsertOneAsync(list);
            var boardList = new BoardList()
            {
                ListId = list.ListId,
                ListTitle = list.ListTitle,
                ListPosition = list.ListPosition,
                CreationDate = list.CreationDate
            };
            var filter = Builders<Board>.Filter.Eq(c => c.BoardId, list.BoardId);
            var update = Builders<Board>.Update.Push(b => b.BoardLists, boardList);
            var updateBoard = context.Boards.FindOneAndUpdateAsync(filter, update);
            await updateBoard;
        }
        public List<List> GetList()
        {
            return context.Lists.Find(_ => true).ToList();
        }

        public List GetListById(string listId)
        {
            List list = context.Lists.Find(n => n.ListId == listId).First();
            return list;
        }
        public bool RemoveList(string listId)
        {
            var deleteResult = context.Lists.DeleteOne(c => c.ListId == listId);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public bool UpdateList(string listId, List list)
        {
            var filters = Builders<List>.Filter.Where(c => c.ListId == listId);
            var updatedResult = context.Lists.ReplaceOne(filters, list);
            return updatedResult.IsAcknowledged && updatedResult.ModifiedCount > 0;
        }



        #endregion

        //public void CreateListCard(string listId, Card card)
        //{
        //    card.CardId = ObjectId.GenerateNewId().ToString();
        //    var filter = Builders<ListCards>.Filter.Eq(c => c.ListId, listId);
        //    var update = Builders<ListCards>.Update.Push(c => c.ListCards, card);
        //    context.Lists.FindOneAndUpdate(filter, update);
        //}
        public ICollection<ListCards> GetListCard(string listId)
        {
            //teamBoard teamBoard = context.Teams.Find(n => n.teamID == teamID).First();
            List list = context.Lists.Find(n => n.ListId == listId).First();
            return list.ListCards;
        }

        public async Task<ICollection<Card>> LoadMore(int skip,int limit)
        {
           // var filter = Builders<List>.Filter.Eq(c => c.LId, listId);
            ICollection<Card> card = await context.Cards.Find(_ => true).Skip(skip).Limit(limit).ToListAsync();
            return card;
        }


        
    }
}

