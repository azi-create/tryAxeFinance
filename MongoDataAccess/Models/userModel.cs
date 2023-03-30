

using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongoDataAccess.Models
{
    public class userModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string Id { get; set; }
        public string name { get; set; }
       
        public string lastName { get; set; }

        public string fullName => $"{name} {lastName}";


    }
}
