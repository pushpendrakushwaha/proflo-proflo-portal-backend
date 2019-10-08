using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace ProfloPortalBackend.Model
{
    // Invite Entities
    public class Invite
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string InviteId { get; set; }
        
        [BsonElement("inviter")]
        public Member Inviter { get; set; }
        
        [BsonElement("invitee")]
        public Member Invitee { get; set; }
        
        [BsonElement("team")]
        public Team Team { get; set; }
    }
}