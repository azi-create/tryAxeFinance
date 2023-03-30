
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDataAccess.Models
{
    public class choreModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]


        public string Id { get; set; }

        public string ChoreText { get; set; }

        public int FrequencuInDays { get; set; }

        public userModel? AssignedTo { get; set; }
         
       public DateTime? LastCompleted { get; set; }




 

    }
}
