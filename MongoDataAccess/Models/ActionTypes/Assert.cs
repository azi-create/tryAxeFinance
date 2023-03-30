using MongoDataAccess.Models;

namespace MongoDataAccess.Models.ActionTypes
{
    public class Assert
    {
        public ActionHandSide LeftHandSide { get; set; }
        public ActionHandSide RightHandSide { get; set; }

        public Assert()
        {

        }

        public Assert(ActionHandSide leftHandSide, ActionHandSide rightHandSide)
        {
            LeftHandSide = leftHandSide;
            RightHandSide = rightHandSide;
        }

        public string LogAssert()
        {
            return "LeftHandSide:{" + LeftHandSide.LogActionHandside() + "},RightHandSide:{" + RightHandSide.LogActionHandside() + "},";
           
        }
    }
}