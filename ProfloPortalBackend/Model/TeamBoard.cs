using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfloPortalBackend.Model
{
    // TeamBoard Entities
    public class TeamBoard
    {
        [BsonId]
        [BsonElement("boardId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string BoardId { get; set; }
        [BsonElement("boardName")]
        public string BoardName { get; set ; }
        [BsonElement("description")]
        public string Description { get; set ; }

        
    }
}
