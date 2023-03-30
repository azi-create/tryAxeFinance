using System.Collections.Generic;

namespace MongoDataAccess.Models;

public class Port
{
    public string Id { get; set; }

    public List<string> Targets { get; set; }

    public Port()
    {

    }

    public string LogPort()
    {
        string PortString = "{Id:\"" + Id + "\",Targets:[";
        foreach (var target in Targets)
        {
            PortString += "\"" + target + "\",";
        }
        PortString += "]}";

        return PortString;
    }
}