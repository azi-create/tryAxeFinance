using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;


namespace MongoDataAccess.Models
{
    public class changesModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string Id {  get; set; }

        public string timeOfOperation { get; set; }

        public string operation { get; set; }   

      


    }
}
