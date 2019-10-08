using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProfloPortalBackend.Model
{
    // Member Entities
    public class Member
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("memberId")]
        public string MemberId { get; set; }
        
        [BsonElement("memberName")]
        public string MemberName { get; set; }
        
        [BsonElement("status")]
        public string Status { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime CreatedOn { get; set; }

        [BsonIgnoreIfDefault]
        [BsonElement("userId")]
        public string UserId { get; set; }
        
        [JsonProperty("emailId")]
        [BsonElement("emailId")]
        public string EmailId { get; set; }

        [BsonIgnoreIfDefault]
        [BsonElement("teams")]
        public List<Team> Teams { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        
    }
}
