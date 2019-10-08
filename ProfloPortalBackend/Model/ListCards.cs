using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProfloPortalBackend.Model
{
    // ListCard Entities
    public class ListCards
    {
        [BsonId]
        [BsonElement("cardId")]
        public string CardId { get; set; }

        [BsonElement("description")]
        public string description { get; set; }
        
        [BsonElement("cardTitle")]
        public string CardTitle { get; set; }
        
        [BsonElement("createdDate")]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }
        
        [BsonElement("dueDate")]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }
    }
}
