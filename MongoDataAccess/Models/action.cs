
using MongoDataAccess.Models.ActionTypes;  

namespace Domain.Entities
{
    public class Action
    {
     

        public string ActionType { get; set; }

        public string Source { get; set; }

        public bool Synchronous { get; set; }

        public Assert Assert { get; set; }

        public ScalarValue ScalarValue { get; set; }

        //public ExecQuery ExecQuery { get; set; }

        public Action()
        {

        }

        public Action(  string actionType, string source, bool synchronous, Assert assert)
        {
           
            ActionType = actionType;
            Source = source;
            Synchronous = synchronous;
            Assert = assert;
        }
        public string LogAction()
        {
            string ActionString = "ActionType:\"" + ActionType + "\",Source:\"" + Source + "\",Synchronous:\"" + Synchronous + "\",Assert:{" + Assert.LogAssert() + "}";
            return ActionString;
        }

    }
}
