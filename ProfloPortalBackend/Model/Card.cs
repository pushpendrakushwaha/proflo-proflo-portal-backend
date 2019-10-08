using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProfloPortalBackend.Model
{
    // Card Entities
    public class Card
    {
        [BsonId]
        [BsonElement("cardId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CardId { get; set; }
        
        [BsonElement("boardId")]
        public string BoardId { get; set; }

        [BsonElement("listId")]
        public string ListId { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("cardtitle")]
        public string CardTitle { get; set; }


        [BsonElement("creationDate")]
        [BsonRepresentation(BsonType.DateTime)]
        public  DateTime CreationDate { get; set; }

        [BsonElement("dueDate")]
        [BsonRepresentation(BsonType.String)]
        public  DateTime DueDate { get; set; }
        [BsonElement("assignees")]
        [BsonIgnoreIfDefault]
        public List<Member> Assignees { get; set; }
        [BsonElement("labels")]
        public List<Label> Labels { get; set; }
        [BsonElement("attachments")]
        public List<Attachment> Attachments { get; set; }
        [BsonElement("comments")]
        public List<Comment> Comments { get; set; }
        [BsonElement("cardInvites")]
        public List<Invite> CardInvites { get; set; }
        
    }
}
