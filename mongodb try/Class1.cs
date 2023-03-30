using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace mongodb_try
{

   
    public class PersonMode
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string Id { get; set; }
        public string name { get; set; }

        public string lastName { get; set; }

    }
}
