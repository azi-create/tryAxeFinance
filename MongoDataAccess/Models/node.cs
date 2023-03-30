using System.Collections.Generic;

namespace MongoDataAccess.Models;
    public class Node
    {
        public int x { get; set; }
        public int y { get; set; }

        public List<Port> Ports { get; set; }

        public Node()
        {

        }
        public string LogNode()
        {
            string NodeString = "{x:\"" + x + "\",y:\"" + y + "\",Ports:[";
            foreach (Port Port in Ports)
            {
                NodeString = NodeString + Port.LogPort();
            }
            NodeString += "]}";
            return NodeString;
        }
    }
