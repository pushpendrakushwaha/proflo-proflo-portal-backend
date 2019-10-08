using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProfloPortalBackend.Model
{
    // BoardList Entities
    public class BoardList
    {
        [BsonElement("listId")]
        public string ListId { get; set; }
        [BsonElement("listTitle")]
        public string ListTitle { get; set; }
        [BsonElement("listPosition")]
        public int ListPosition { get; set; }
        
        [BsonElement("creationDate")]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }
        public List<Card> Cards {get; set;}
    }
}
