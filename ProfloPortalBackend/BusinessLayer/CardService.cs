using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using ProfloPortalBackend.DataAccessLayer;
using ProfloPortalBackend.Model;
using ProfloPortalBackend.RealTime;

namespace ProfloPortalBackend.BusinessLayer
{
    public class CardService : ICardService
    {
        public readonly ICardRepository cardRepository;
        public readonly IProfloRTService profloRTService;
        public CardService(ICardRepository cardRepository, IProfloRTService profloRTService)
        {
            this.profloRTService = profloRTService;
            this.cardRepository = cardRepository;
        }

        public async Task MoveCard(MoveCardRequest moveCardRequest)
        {
            Console.WriteLine(moveCardRequest.ToListId);
            Console.WriteLine(moveCardRequest.FromListId);
            var card = await cardRepository.GetCardByIDAsync(moveCardRequest.CardId);
            card.ListId = moveCardRequest.ToListId;
            var update = await cardRepository.UpdateCard(moveCardRequest.CardId, card);
            Console.WriteLine("Update Status");
            Console.WriteLine(update);
            await profloRTService.EmitMoveCardEvent(moveCardRequest);
        }


        #region Card Operation
        public async Task<Card> CreateCard(Card card)
        {
            var card1 = new Card()
            {
                CreationDate = DateTime.Now
            };

            if (card.Comments == null)
            {
                card.Comments = new List<Comment>();
                
                
            }
            if(card.CardInvites == null)
            {
                card.CardInvites = new List<Invite>();
            }
            var createdCard = await cardRepository.CreateCard(card);
            await profloRTService.EmitAddCardEvent(createdCard);
            return createdCard;
        }
        public async Task<Card> GetCardByIDAsync(string cardId)
        {
            var result = await cardRepository.GetCardByIDAsync(cardId);
            return result;
        }

        public List<Card> GetCards()
        {
            return cardRepository.GetCards();
        }
        public async Task<bool> UpdateCard(string cardId, Card card)
        {
            return await cardRepository.UpdateCard(cardId, card);
        }
        public bool RemoveCard(string cardId)
        {
            return cardRepository.RemoveCard(cardId);
        }
        #endregion

        #region Card Members
        public void CreateCardMembers(string cardId, Member member)
        {

            cardRepository.CreateCardMembers(cardId, member);
        }
        public ICollection<Member> GetCardMembers(string cardID)
        {
            return cardRepository.GetCardMembers(cardID);
        }
        public Member GetMemberByMemberId(string cardId, string memberId)
        {
            return cardRepository.GetMemberByMemberId(cardId, memberId);
        }

        public bool UpdateCardMembers(string cardId, string memberID, Member member)
        {
            return cardRepository.UpdateCardMembers(cardId, memberID, member);
        }    
        public bool RemoveMembers(string cardId, string Mid)
        {
            return cardRepository.RemoveMembers(cardId, Mid);
        }
        #endregion

        #region Card Invitee

        public void CreateInvite(string cardID, Invite invite)
        {
            cardRepository.CreateInvite(cardID, invite);
        }
        public ICollection<Invite> GetCardInvites(string cardID)
        {
            return cardRepository.GetCardInvites(cardID);
        }
        //public bool UpdateInvite(string cardId, string inviteID, Invite invite)
        //{
        //    return cardRepository.UpdateInvite(cardId, inviteID, invite);
        //}
        public bool RemoveInvite(string cardId, string inviteID)
        {
            return cardRepository.RemoveInvite(cardId, inviteID);
        }
        #endregion

        #region Card Label
        public void CreateLabel(string cardID, Label label)
        {
            cardRepository.CreateLabel(cardID, label);
        }
        public ICollection<Label> GetCardLabels(string cardID)
        {
            return cardRepository.GetCardLabels(cardID);
        }

        public bool UpdateLabel(string cardID, string labelID, Label label)
        {
            return cardRepository.UpdateLabel(cardID, labelID, label);
        }

        public bool RemoveLabel(string cardId, string labelID)
        {
            return cardRepository.RemoveLabel(cardId, labelID);
        }

        public Label GetLabelByID(string cardId, string labelID)
        {
            return cardRepository.GetLabelByID(cardId, labelID);
        }
        #endregion

        #region Card Comments

        public void CreateComments(string cardId, Comment comments)
        {
            comments.CommentID = ObjectId.GenerateNewId().ToString();
            cardRepository.CreateComments(cardId, comments);
        }
        public Comment GetCommentByID(string cardId, string commentID)
        {
            return cardRepository.GetCommentByID(cardId, commentID);
        }

        public ICollection<Comment> GetComments(string cardID)
        {
            return cardRepository.GetComments(cardID);
        }

        public bool UpdateComments(string cardID, string commentID, Comment comments)
        {
            return cardRepository.UpdateComments(cardID, commentID, comments);
        }

        public bool RemoveComments(string cardId, string commentID)
        {
            return cardRepository.RemoveComments(cardId, commentID);
        }
        #endregion

        #region Card Attachment

        public void CreateAttachment(string cardID, Attachment attachement)
        {
            cardRepository.CreateAttachment(cardID, attachement);
        }

        public Attachment GetAttachmentByID(string cardId, string attachmentID)
        {
            return cardRepository.GetAttachmentByID(cardId, attachmentID);
        }

        public ICollection<Attachment> GetAttachment(string cardID)
        {
            return cardRepository.GetAttachment(cardID);
        }

        public bool UpdateAttachment(string cardID, string attachmentID, Attachment attachement)
        {
            return cardRepository.UpdateAttachment(cardID, attachmentID, attachement);
        }

        public bool RemoveAttachment(string cardId, string attachmentID)
        {
            return cardRepository.RemoveAttachment(cardId, attachmentID);
        }
        #endregion
    }
}
