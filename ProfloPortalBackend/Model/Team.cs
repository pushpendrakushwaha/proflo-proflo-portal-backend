using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProfloPortalBackend.Model
{

    // Team Entities
    public class Team
    {
        [BsonId]
        [BsonElement("teamId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string TeamId;
        
        [BsonElement("name")]
        public string Name { get; set; }
        
        [BsonElement("description")]
        public string Description { get; set; }
        
        [BsonElement("boards")]
        [BsonIgnoreIfDefault]
        public List<TeamBoard> TeamBoards { get; set; }

        [BsonElement("members")]
        [BsonIgnoreIfDefault]
        public List<Member> TeamMembers { get; set; }

        [BsonElement("invitees")]
        [BsonIgnoreIfDefault]
        public List<Member> Invitees { get; set; }
    }
}
