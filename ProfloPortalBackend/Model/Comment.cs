using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProfloPortalBackend.Model
{
    // Comments Entities
    public class Comment
    {
        [BsonId]
        [BsonElement("commentId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CommentID { get; set; }


        [BsonElement("authoredBy")]

        public Member authoredBy { get; set; }

        [BsonElement("commentText")]
        public string CommentText { get; set; }

        // Also need date and time of when the comment was created

    }
}
