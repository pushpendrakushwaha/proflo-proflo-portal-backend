using ProfloPortalBackend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfloPortalBackend.BusinessLayer
{
     public interface ICardService
    {
        #region Card Operation
        Task<Card> CreateCard(Card card);
        List<Card> GetCards();
        Task<Card> GetCardByIDAsync(string cardId);
        Task<bool> UpdateCard(string cardId, Card card);
        bool RemoveCard(string cardId);
        #endregion

        #region Card Members
        void CreateCardMembers(string cardId, Member member);
        ICollection<Member> GetCardMembers(string cardID);
        Member GetMemberByMemberId(string cardId, string memberId);

        bool UpdateCardMembers(string cardId, string memberID, Member member);
        bool RemoveMembers(string cardId, string Mid);
        #endregion

        #region Create Invitee
        void CreateInvite(string cardID, Invite invite);
        ICollection<Invite> GetCardInvites(string cardID);
        bool RemoveInvite(string cardId, string inviteID);
       // bool UpdateInvite(string cardId, string inviteID, Invite invite);
        #endregion

        #region Card Label
        void CreateLabel(string cardID, Label label);
        bool UpdateLabel(string cardID, string labelID, Label label);
        bool RemoveLabel(string cardId, string LabelID);
        Label GetLabelByID(string cardId, string LabelID);
        ICollection<Label> GetCardLabels(string cardID);
        #endregion

        #region Card Comments
        void CreateComments(string cardId, Comment comments);
        bool UpdateComments(string cardID, string commentID, Comment comments);
        bool RemoveComments(string cardId, string commentID);
        Comment GetCommentByID(string cardId, string commentID);
        ICollection<Comment> GetComments(string cardID);

        #endregion
        #region Card Attachments
        void CreateAttachment(string cardID, Attachment attachement);
        Attachment GetAttachmentByID(string cardId, string attachmentID);
        ICollection<Attachment> GetAttachment(string cardID);
        bool UpdateAttachment(string cardID, string attachmentID, Attachment attachement);
        bool RemoveAttachment(string cardId, string attachmentID);

        Task MoveCard(MoveCardRequest moveCardRequest);

        #endregion
        ////ICollection<teamBoard> getTeamBoards(int teamID);

    }
}
