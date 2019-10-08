using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProfloPortalBackend.Model;
using MongoDB.Driver;
using MongoDB.Bson;

namespace ProfloPortalBackend.DataAccessLayer
{
    public class CardRepository : ICardRepository
    {
        private readonly DBContext context;
        public CardRepository(DBContext dBContext)
        {
            context = dBContext;
        }

        #region Card Operation
        public async Task<Card> CreateCard(Card card)
        {
            await context.Cards.InsertOneAsync(card);
            var listCard = new ListCards()
            {
                CardId = card.CardId,
                description = card.Description,
                CardTitle = card.CardTitle,
                CreationDate = card.CreationDate,
                DueDate = card.DueDate
            };
            var filter = Builders<List>.Filter.Eq(c => c.ListId, card.ListId);
            var update = Builders<List>.Update.Push(c => c.ListCards, listCard);
            var updateBoard = context.Lists.FindOneAndUpdateAsync(filter, update);
            await updateBoard;
            return card;
        }
        public List<Card> GetCards()
        {
            return context.Cards.Find(_ => true).ToList();
        }

        public async Task<Dictionary<string, List<Card>>> GetCardsByListIds(List<string> listIds)
        {
            var cardsByList = new Dictionary<string, List<Card>>();
            var findOptions = new FindOptions<Card>
            {
                Limit = 20
            };
            var cardsByListIds = listIds.Select(async (listId) =>
            {
                var filterByCardId = Builders<Card>.Filter.Eq(c => c.ListId, listId);
                var cards = await context.Cards.FindAsync(filterByCardId, findOptions);

                return cards.ToList();
            });
            var cardsSet = await Task.WhenAll(cardsByListIds);
            Enumerable.Range(0, listIds.Count).Aggregate(cardsByList, (acc, index) =>
            {
                cardsByList.Add(listIds[index], cardsSet[index]);
                return cardsByList;
            });
            return cardsByList;
        }

        public async Task<Card> GetCardByIDAsync(string cardId)
        {
            return await context.Cards.Find(n => n.CardId == cardId).FirstOrDefaultAsync();
        }

        public bool RemoveCard(string cardId)
        {
            var deletedResult = context.Cards.DeleteOne(n => n.CardId == cardId);
            return deletedResult.IsAcknowledged && deletedResult.DeletedCount > 0;
        }
        public async Task<bool> UpdateCard(string cardId, Card card)
        {
            Console.WriteLine($"Card Id is {cardId}");
            var filter = Builders<Card>.Filter.Eq(c => c.CardId, cardId);
            var updatedResult = await context.Cards.ReplaceOneAsync(filter, card);
            Console.WriteLine(updatedResult.IsAcknowledged);
            Console.WriteLine(updatedResult.MatchedCount);
            Console.WriteLine(updatedResult.ModifiedCount);
            return updatedResult.IsAcknowledged && updatedResult.ModifiedCount > 0;
        }
        #endregion


        #region Card Members

        public void CreateCardMembers(string cardId, Member member)
        {
            member.MemberId = ObjectId.GenerateNewId().ToString();
            var filter = Builders<Card>.Filter.Eq(c => c.CardId, cardId);
            var update = Builders<Card>.Update.Push(c => c.Assignees, member);
            context.Cards.FindOneAndUpdate(filter, update);
        }
        public ICollection<Member> GetCardMembers(string cardID)
        {
            Card card = context.Cards.Find(n => n.CardId == cardID).First();
            return card.Assignees;
        }
        public Member GetMemberByMemberId(string cardId, string memberId)
        {
            Card card = context.Cards.Find(c => c.CardId == cardId).First();
            Member member = card.Assignees.Find(m => m.MemberId == memberId);
            return member;
        }
        public bool RemoveMembers(string cardId, string Mid)
        {
            Card card = context.Cards.Find(n => n.CardId == cardId).First();
            Member member = card.Assignees.Find(n => n.MemberId == Mid);
            var filter = Builders<Card>.Filter.Eq(n => n.CardId, cardId);
            var update = Builders<Card>.Update.Pull(e => e.Assignees, member);
            var updatedResult = context.Cards.UpdateOneAsync(filter, update).Result;
            return updatedResult.IsAcknowledged && updatedResult.ModifiedCount > 0;
        }
        public bool UpdateCardMembers(string cardId, string memberID, Member member)
        {
            //var filter = Builders<Card>.Filter.Eq(n => n.CardId, cardId);
            //var update = Builders<Card>.Update.Set(e => e.Assignees.Find(n => n.MemberId == memberID), member);
            //var updatedResult = context.Cards.UpdateOneAsync(filter, update).Result;
            var filter = Builders<Card>.Filter.Where(n => n.CardId == cardId && n.Assignees.Any(t => t.MemberId == memberID));
            var updateMemberOfCard = Builders<Card>.Update.Set(x => x.Assignees[-1], member);
            var updatedResult = context.Cards.UpdateOne(filter, updateMemberOfCard);
            return updatedResult.IsAcknowledged && updatedResult.ModifiedCount > 0;
        }
        #endregion

        #region Card Attachments

        public void CreateAttachment(string cardID, Attachment attachment)
        {
            attachment.AttachmentId = ObjectId.GenerateNewId().ToString();
            var filter = Builders<Card>.Filter.Eq(c => c.CardId, cardID);
            var update = Builders<Card>.Update.Push(c => c.Attachments, attachment);
            context.Cards.FindOneAndUpdate(filter, update);
        }
        public ICollection<Attachment> GetAttachment(string cardID)
        {
            Card card = context.Cards.Find(n => n.CardId == cardID).First();
            return card.Attachments;
        }

        public Attachment GetAttachmentByID(string cardId, string attachmentID)
        {
            Card card = context.Cards.Find(n => n.CardId == cardId).First();
            Attachment attachement = card.Attachments.Find(n => n.AttachmentId == attachmentID);
            return attachement;
        }
        public bool UpdateAttachment(string cardId, string attachmentId, Attachment attachement)
        {
            //Card card = context.Cards.Find(n => n.CId == cardID).First();
            //Attachement attachement1 = card.Attachements.First(n => n.AttachementId == attachement.AttachementId);
            var filter = Builders<Card>.Filter.Where(n => n.CardId == cardId && n.Attachments.Any(t => t.AttachmentId == attachmentId));
            var updateAttachmentOfCard = Builders<Card>.Update.Set(x => x.Attachments[-1], attachement);
            var updatedResult = context.Cards.UpdateOne(filter, updateAttachmentOfCard);
            return updatedResult.IsAcknowledged && updatedResult.ModifiedCount > 0;
        }
        public bool RemoveAttachment(string cardId, string attachmentID)
        {
            Card card = context.Cards.Find(n => n.CardId == cardId).First();
            Attachment attachement = card.Attachments.Find(n => n.AttachmentId == attachmentID);
            var filter = Builders<Card>.Filter.Eq(n => n.CardId, cardId);
            var update = Builders<Card>.Update.Pull(e => e.Attachments, attachement);
            var updatedResult = context.Cards.UpdateOneAsync(filter, update).Result;
            return updatedResult.IsAcknowledged && updatedResult.ModifiedCount > 0;
        }
        #endregion

        #region Card Invitees
        public void CreateInvite(string cardID, Invite invite)
        {
            invite.InviteId = ObjectId.GenerateNewId().ToString();
            var filter = Builders<Card>.Filter.Eq(c => c.CardId, cardID);
            var update = Builders<Card>.Update.Push(c => c.CardInvites, invite);
            context.Cards.FindOneAndUpdate(filter, update);
        }
        public ICollection<Invite> GetCardInvites(string cardID)
        {
            Card card = context.Cards.Find(n => n.CardId == cardID).First();
            return card.CardInvites;
        }
        public bool UpdateInvite(string cardId, string inviteID, Invite invite)
        {
            var filter = Builders<Card>.Filter.Where(n => n.CardId == cardId && n.CardInvites.Any(t => t.InviteId == inviteID));
            var updateInviteeOfCard = Builders<Card>.Update.Set(x => x.CardInvites[-1], invite);
            var updatedResult = context.Cards.UpdateOne(filter, updateInviteeOfCard);
            return updatedResult.IsAcknowledged && updatedResult.ModifiedCount > 0;
        }
        public bool RemoveInvite(string cardId, string inviteID)
        {
            Card card = context.Cards.Find(n => n.CardId == cardId).First();
            Invite invitee = card.CardInvites.Find(n => n.InviteId == inviteID);
            var filter = Builders<Card>.Filter.Eq(n => n.CardId, cardId);
            var update = Builders<Card>.Update.Pull(e => e.CardInvites, invitee);
            var updatedResult = context.Cards.UpdateOneAsync(filter, update).Result;
            return updatedResult.IsAcknowledged && updatedResult.ModifiedCount > 0;
        }
        #endregion

        #region Card Labels
        public void CreateLabel(string cardID, Label label)
        {
            label.LabelId = ObjectId.GenerateNewId().ToString();
            var filter = Builders<Card>.Filter.Eq(c => c.CardId, cardID);
            var update = Builders<Card>.Update.Push(c => c.Labels, label);
            context.Cards.FindOneAndUpdate(filter, update);
        }

        public Label GetLabelByID(string cardId, string labelID)
        {
            Card card = context.Cards.Find(n => n.CardId == cardId).First();
            Label label = card.Labels.First(n => n.LabelId == labelID);
            return label;
        }
        public ICollection<Label> GetCardLabels(string cardID)
        {
            Card card = context.Cards.Find(n => n.CardId == cardID).First();
            return card.Labels;
        }
        public bool UpdateLabel(string cardID, string labelID, Label label)
        {
            var filter = Builders<Card>.Filter.Where(n => n.CardId == cardID && n.Labels.Any(t => t.LabelId == labelID));
            var updateLabelOfCard = Builders<Card>.Update.Set(x => x.Labels[-1], label);
            var updatedResult = context.Cards.UpdateOne(filter, updateLabelOfCard);
            return updatedResult.IsAcknowledged && updatedResult.ModifiedCount > 0;
        }
        public bool RemoveLabel(string cardId, string LabelID)
        {
            Card card = context.Cards.Find(n => n.CardId == cardId).First();
            Label label = card.Labels.Find(n => n.LabelId == LabelID);
            var filter = Builders<Card>.Filter.Eq(n => n.CardId, cardId);
            var update = Builders<Card>.Update.Pull(e => e.Labels, label);
            var updatedResult = context.Cards.UpdateOneAsync(filter, update).Result;
            return updatedResult.IsAcknowledged && updatedResult.ModifiedCount > 0;
        }
        #endregion

        #region Card Comments

        public void CreateComments(string cardId, Comment comments)
        {
            comments.CommentID = ObjectId.GenerateNewId().ToString();
            var filter = Builders<Card>.Filter.Eq(c => c.CardId, cardId);
            var update = Builders<Card>.Update.Push(c => c.Comments, comments);
            context.Cards.UpdateOne(filter, update);
        }

        public Comment GetCommentByID(string cardId, string commentID)
        {
            Card card = context.Cards.Find(n => n.CardId == cardId).First();
            Comment comments = card.Comments.Find(n => n.CommentID == commentID);
            return comments;
        }

        public ICollection<Comment> GetComments(string cardID)
        {
            Card card = context.Cards.Find(n => n.CardId == cardID).First();
            return card.Comments;
        }
        public bool UpdateComments(string cardID, string commentID, Comment comments)
        {
            var filter = Builders<Card>.Filter.Where(n => n.CardId == cardID && n.Comments.Any(t => t.CommentID == commentID));
            var updateCommentOfCard = Builders<Card>.Update.Set(x => x.Comments[-1], comments);
            var updatedResult = context.Cards.UpdateOne(filter, updateCommentOfCard);
            return updatedResult.IsAcknowledged && updatedResult.ModifiedCount > 0;
        }

        public bool RemoveComments(string cardId, string commentID)
        {
            Card card = context.Cards.Find(n => n.CardId == cardId).First();
            Comment comments = card.Comments.Find(n => n.CommentID == commentID);
            var filter = Builders<Card>.Filter.Eq(n => n.CardId, cardId);
            var update = Builders<Card>.Update.Pull(e => e.Comments, comments);
            var updatedResult = context.Cards.UpdateOneAsync(filter, update).Result;
            return updatedResult.IsAcknowledged && updatedResult.ModifiedCount > 0;
        }

        #endregion




    }
}
