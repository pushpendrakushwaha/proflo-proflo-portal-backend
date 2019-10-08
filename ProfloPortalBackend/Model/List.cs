using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProfloPortalBackend.Model
{
    // List Entities
    public class List
    {
        [BsonId]
        [BsonElement("listId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ListId { get; set; }
        
        public string BoardId { get; set; }
        
        [BsonElement("listTitle")]
        public string ListTitle { get; set; }
        [BsonElement("listPosition")]
        public int ListPosition { get; set; }
        [BsonElement("creationDate")]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }

        [BsonElement("cards")]
        public List<ListCards> ListCards { get; set; }
    }
}
