using ProfloPortalBackend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfloPortalBackend.DataAccessLayer
{
    public interface ICardRepository
    {
        #region Card Operation
        Task<Dictionary<string, List<Card>>> GetCardsByListIds(List<string> listIds);
        Task<Card> CreateCard(Card card);
        List<Card> GetCards();
        Task<Card> GetCardByIDAsync(string cardId);
        Task<bool> UpdateCard(string cardId, Card card);
        bool RemoveCard(string cardId);
        #endregion

        #region Card Members
        bool RemoveMembers(string cardId, string Mid);
        void CreateCardMembers(string cardId, Member member);
        bool UpdateCardMembers(string cardId, string memberID, Member member);
        ICollection<Member> GetCardMembers(string cardID);
        Member GetMemberByMemberId(string cardId, string memberId);
        #endregion

        #region Card Attachments
        void CreateAttachment(string cardID, Attachment attachement);
        bool UpdateAttachment(string cardID, string attachmentID, Attachment attachement);
        bool RemoveAttachment(string cardId, string attachmentID);
        Attachment GetAttachmentByID(string cardId, string attachmentID);
        ICollection<Attachment> GetAttachment(string cardID);
        #endregion

        #region Card Invitees

        void CreateInvite(string cardID, Invite invite);
        bool RemoveInvite(string cardId, string inviteID);
       // bool UpdateInvite(string cardId, string inviteID, Invite invite);
        ICollection<Invite> GetCardInvites(string cardID);
        #endregion

        #region Card Labels
        void CreateLabel(string cardID, Label label);
        ICollection<Label> GetCardLabels(string cardID);
        Label GetLabelByID(string cardId, string LabelID);
        bool UpdateLabel(string cardID, string labelID, Label label);
        bool RemoveLabel(string cardId, string LabelID);
        #endregion

        #region Card Comments
        void CreateComments(string cardId, Comment comments);
        ICollection<Comment> GetComments(string cardId);
        Comment GetCommentByID(string cardId, string commentID);

        bool UpdateComments(string cardID, string commentID, Comment comments);
        bool RemoveComments(string cardId, string commentID);

        #endregion
        //ICollection<teamBoard> GetTeamBoards(string teamID);

    }
}
