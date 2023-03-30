
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDataAccess.Models
{
    public class choreHistoryModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]

        public string Id { get; set; }

        public string ChoreId { get; set; }

        public string ChoreText { get; set;}

        public DateTime DateCompleted { get; set; }

        public userModel WhoCompleted { get; set; }

        public choreHistoryModel()
        {

        }
        public choreHistoryModel(choreModel chore)
        {
            ChoreId = chore.Id;
            DateCompleted = chore.LastCompleted ?? DateTime.Now;
            WhoCompleted = chore.AssignedTo;
            ChoreText = chore.ChoreText;
        }
    }
}
