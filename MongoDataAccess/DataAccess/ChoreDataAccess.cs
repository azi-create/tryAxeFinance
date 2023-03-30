



using MongoDataAccess.Models;
using MongoDB.Driver;
using MongoDataAccess.Models.Enums;
using System;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//using JsonDiffPatchDotNet;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection;
using System.Xml.Linq;
using System.Text.Json.Nodes;
using System.Xml;
using SharpCompress.Compressors.Xz;
using System.ComponentModel;
using System.IO;
using System.Reflection.Metadata;

namespace MongoDataAccess.DataAccess;

public class ChoreDataAccess
{
    private const string ConnectionString = "mongodb://localhost:27017";
    private const string DatabaseName = "choredb";

    //private const string ChoreCollection = "chore_chart";
    //private const string UserCollection = "users";
    //private const string ChoreHistoryCollection = "chore_history";
    //private const string ChangesCollection = "changes";
    private const string PolicyCollection = "policyset";




    private IMongoCollection<T> ConnectToMongo<T>(in string collection)
    {
        var client = new MongoClient(ConnectionString);
        var db = client.GetDatabase(DatabaseName);
        return db.GetCollection<T>(collection);

    }


    public PolicySet GetPolicySetByName(string name)
    {
        var policyCollection = ConnectToMongo<PolicySet>(PolicyCollection);
        var entity = policyCollection.Find(document => document.Name == name).FirstOrDefault();
        //Console.WriteLine("entity =   "+ entity);
        //Console.WriteLine("entity type = "+  entity.GetType());
        return entity;


    }


   


    




















    public Dictionary<string, object> ParcourDics(Dictionary<string, object> dicOne, Dictionary<string, object> dicTwo, string attribue = "content")
    {
        Dictionary<string, object> dictionary = new Dictionary<string, object>();

        foreach (KeyValuePair<string, object> kvp in dicOne)
        {
            if (kvp.Value == null)
            {
                //dictionary.Add(kvp.Key, "null value ");
                if (dictionary.ContainsKey(attribue) == false) 
                
                { dictionary.Add(attribue, "changed"); };


            }
            else
           if (kvp.Value.GetType() == typeof(string))
            {
                if (kvp.Value != dicTwo[kvp.Key])
                {

                    if (dictionary.ContainsKey(attribue) == false) { dictionary.Add(attribue, "changed"); };


                }
            }
            else if (kvp.Value.GetType() == typeof(DateTime))
            {
                if (dicTwo.ContainsKey(kvp.Key) && dicTwo.ContainsValue(kvp.Value))
                {

                    if (dictionary.ContainsKey(attribue) == false) { dictionary.Add(attribue, "changed"); };

                }


            }

            else if (kvp.Value.GetType() == typeof(JObject))
            {
                Dictionary<string, object> NewDictionary = new Dictionary<string, object>();
                string jsonStringOne = JsonConvert.SerializeObject(kvp.Value);
                string jsonStringTwo = JsonConvert.SerializeObject(dicTwo[kvp.Key]);

                JToken jTokenOne = JToken.Parse(jsonStringOne);
                JToken jTokenTwo = JToken.Parse(jsonStringTwo);


                Dictionary<string, object> ChildDictionaryOne = jTokenOne.ToObject<Dictionary<string, object>>();
                Dictionary<string, object> ChildDictionaryTwo = jTokenTwo.ToObject<Dictionary<string, object>>();


                NewDictionary = ParcourDics(ChildDictionaryOne, ChildDictionaryTwo, kvp.Key);
                dictionary.Add(kvp.Key, NewDictionary);


            }
            else if (kvp.Value.GetType() == typeof(JArray))
            {

                var jArrayOne = (JArray)kvp.Value;
                var jArrayTwo = (JArray)dicTwo[kvp.Key];

                List<Dictionary<string, object>> myDictList = new List<Dictionary<string, object>>();

                Dictionary<string, object> NewDictionary = new Dictionary<string, object>();
                for (int i = 0; i < jArrayOne.Count; i++)
                {


                    string jsonStringOne = JsonConvert.SerializeObject(jArrayOne[i]);
                    string jsonStringTwo = JsonConvert.SerializeObject(jArrayTwo[i]);


                    JToken jTokenOne = JToken.Parse(jsonStringOne);
                    JToken jTokenTwo = JToken.Parse(jsonStringTwo);


                    Dictionary<string, object> ChildDictionaryOne = jTokenOne.ToObject<Dictionary<string, object>>();
                    Dictionary<string, object> ChildDictionaryTwo = jTokenTwo.ToObject<Dictionary<string, object>>();

                    NewDictionary = ParcourDics(ChildDictionaryOne, ChildDictionaryTwo, kvp.Key);




                    myDictList.Add(NewDictionary);

                }

                dictionary.Add(kvp.Key, myDictList);



            }


        }

        return dictionary;


    }






















    public JToken FindDiff(JToken original, JToken modified)

    {

        // Trouver les différences entre les deux objets JToken

        JToken diff = null;

        JsonComparer comparer = new JsonComparer();

        if (!JToken.DeepEquals(original, modified))

        {

            diff = comparer.GetDiff(original, modified, new List<object>());

        }

        return diff;

    }



    public void MarkChanges(JToken modified )

    {

        // Parcourir le résultat de la différence et marquer les nœuds modifiés

        using (JTokenReader reader = new JTokenReader(modified))
     



        {

            while (reader.Read())

            {

                if (reader.TokenType == JsonToken.PropertyName)

                {

                    // Propriété modifiée, ajouter l'attribut "changed"

                   
                    string propertyName = (string)reader.Value;

                    
                    JObject parent = (JObject)reader.CurrentToken.Parent;
                    


                    Console.WriteLine(parent+"//////");



                    if (propertyName != "changed" && !parent.ContainsKey("changed"))
                    {
                        parent[propertyName].Parent.AddAfterSelf(new JProperty("changed", true));
                    }




                }

                else if (reader.TokenType == JsonToken.StartArray || reader.TokenType == JsonToken.StartObject)

                {

                    // Début d'un objet ou d'un tableau, ne rien faire

                }

                else if (reader.TokenType == JsonToken.EndArray || reader.TokenType == JsonToken.EndObject)

                {

                    // Fin d'un objet ou d'un tableau, ne rien faire

                }

            }

        }

    }









    class JsonComparer : JTokenEqualityComparer

    {

        public JToken GetDiff(JToken original, JToken modified, List<object> processedObjects)

        {

            // Comparer les deux objets JToken

            if (JToken.DeepEquals(original, modified))

            {

                return null;

            }



            // Gérer les cas où l'un des objets JToken est nul

            if (original == null)

            {

                if (modified is JValue)

                {

                    return new JProperty("value", modified);

                }

                if (original is JValue)

                {

                    return new JProperty("value", modified);

                }



                // Si les deux objets JToken sont des tableaux, trouver les différences entre les éléments des tableaux

                if (original is JArray && modified is JArray)

                {

                    JArray originalArray = (JArray)original;

                    JArray modifiedArray = (JArray)modified;



                    JArray diff = new JArray();

                    for (int i = 0; i < modifiedArray.Count; i++)

                    {

                        JToken childDiff = GetDiff(originalArray.ElementAtOrDefault(i), modifiedArray.ElementAt(i), processedObjects);

                        if (childDiff != null)

                        {

                            diff.Add(childDiff);

                        }

                    }

                    return diff;

                }



                // Si les deux objets JToken sont des objets, trouver les différences entre les propriétés des objets

                if (original is JObject && modified is JObject)

                {

                    JObject originalObject = (JObject)original;

                    JObject modifiedObject = (JObject)modified;



                    JObject diff = new JObject();

                    foreach (var property in modifiedObject.Properties())

                    {

                        JToken childDiff = GetDiff(originalObject[property.Name], property.Value, processedObjects);

                        if (childDiff != null)

                        {

                            diff.Add(property.Name, childDiff);

                        }

                    }

                    return diff;

                }



                // Les deux objets JToken ont des types différents, donc ils sont différents

                return new JProperty("value", modified);

            }
            return modified;
        }

    }

















    public  string FindDifferentChild(JToken token1, JToken token2, string path = "")
    {
        if (!JToken.DeepEquals(token1, token2))
        {
            return path;
        }

        if (token1.Type == JTokenType.Object)
        {
            foreach (var child in token1.Children<JProperty>())
            {
                var newPath = path + "." + child.Name;
                var result = FindDifferentChild(child.Value, token2[child.Name], newPath);
                if (result != null)
                {
                    return result;
                }
            }
        }
        else if (token1.Type == JTokenType.Array)
        {
            for (int i = 0; i < token1.Count(); i++)
            {
                var newPath = path + "[" + i + "]";
                var result = FindDifferentChild(token1[i], token2[i], newPath);
                if (result != null)
                {
                    return result;
                }
            }
        }
        return null;
    }
  



















    //    // return dictionary;


//}














//public void ParcourDics(Dictionary<string, object> dicOne, Dictionary<string, object> dicParse)
//{
//    Dictionary<string, object> dictionary = new Dictionary<string, object>();

//    foreach (KeyValuePair<string, object> kvp in dicOne)
//    {

//        if (dicParse.ContainsKey(kvp.Key)) {  } 
//        else Console.WriteLine(kvp.Key+"   dont exist in parse ");



//    }




//}




//public async Task<List<changesModel>> GetAllChanges()
//{
//    var changesCollection = ConnectToMongo<changesModel>(ChangesCollection);
//    var results = await changesCollection.FindAsync(_ => true);
//    return results.ToList();

//}


//public Task CreateChange(changesModel change)
//{
//    var changesCollections = ConnectToMongo<changesModel>(ChangesCollection);
//    return changesCollections.InsertOneAsync(change);
//}

    public Task CreatePolicySet(PolicySet policyset)
{
    var policyCollections = ConnectToMongo<PolicySet>(PolicyCollection);
    Console.WriteLine("create policy set works");
    return policyCollections.InsertOneAsync(policyset);
}









//public PolicySet ComparePolicySet(PolicySet policyOne, PolicySet policyTwo)
//{
//    var newName = "";
//    var newDesc = "";
//    var newOwner = "";
//    var policies = new List<Policy>();
//    var newType = new ContextType();


//    if (policyOne.Name == policyTwo.Name) newName = "no change"; else newName = policyOne.Name;
//    if (policyOne.Owner == policyTwo.Owner) newOwner = "no change"; else newOwner = policyOne.Owner;
//    if (policyOne.Description == policyTwo.Description) newDesc = "no change"; else newDesc = policyOne.Description;
//    if (policyOne.Type == policyTwo.Type) newType = 0; else newType = policyOne.Type;
//    var newPolicy = new PolicySet(newName, newDesc, newType, newOwner, policies);

//    return newPolicy;

//}




    public async Task<List<PolicySet>> GetAllPolicySet()
{
    var policyCollection = ConnectToMongo<PolicySet>(PolicyCollection);
    var results = await policyCollection.FindAsync(_ => true);
    return results.ToList();

    }

    //public async Task GetPolicySetById(PolicySet policyset)
    //{
    //    var policyCollection = ConnectToMongo<PolicySet>(PolicyCollection);
    //    var results = await policyCollection.Find()
    //    return results.ToList();

    //}




    //public async Task<List<userModel>> GetAllUsers()
    //{
    //    var userCollection = ConnectToMongo<userModel>(UserCollection);
    //    var results = await userCollection.FindAsync(_=> true);
    //    return results.ToList();

    //}

    //public async Task<List<choreModel>> GetAllChores()
    //{
    //    var ChoresCollection = ConnectToMongo<choreModel>(ChoreCollection);
    //    var results = await ChoresCollection.FindAsync(_ => true);
    //    return results.ToList();
    //}

    //public async Task<List<choreModel>> GetAllChoresForAUser(userModel user)
    //{
    //    var ChoresCollection = ConnectToMongo<choreModel>(ChoreCollection);
    //    var results = await ChoresCollection.FindAsync(c => c.AssignedTo.Id == user.Id);
    //    return results.ToList();
    //}

    //public async Task CreateUser(userModel user)
    //{
    //    ChoreDataAccess mth = new ChoreDataAccess();
    //    DateTime queryDateTime = DateTime.Now;

    //    var usersCollections = ConnectToMongo<userModel>(UserCollection);
    //    var changesCollections = ConnectToMongo<changesModel>(ChangesCollection);
    //    var change = new changesModel { timeOfOperation = queryDateTime.ToString() , operation = "update" };
    //    await mth.CreateChange(change);
    //    await usersCollections.InsertOneAsync(user);
    //    //await Console.Out.WriteLineAsync("the time now is :" + queryDateTime.ToString());



    //}

    //public Task UpdateUser(userModel user)
    //{
    //    var userCollection = ConnectToMongo<userModel>(UserCollection);

    //    var filer = Builders<userModel>.Filter.Eq("Id", user.Id);
    //    Console.WriteLine("update user works");
    //    return userCollection.ReplaceOneAsync(filer, user, new ReplaceOptions { IsUpsert = true });

    //}






    //public Task CreateChore(choreModel chore) 
    //{
    //    var ChoresCollection = ConnectToMongo<choreModel>(ChoreCollection);
    //    return ChoresCollection.InsertOneAsync(chore);
    //}

    //public Task UpdateChore(choreModel chore)
    //{
    //    var ChoresCollection = ConnectToMongo<choreModel>(ChoreCollection);
    //    var filer = Builders<choreModel>.Filter.Eq("Id", chore.Id);
    //    return ChoresCollection.ReplaceOneAsync(filer, chore,new ReplaceOptions { IsUpsert = true});

    //}
    //public Task DeleteChore(choreModel chore)
    //{
    //    var choresCollection = ConnectToMongo<choreModel>(ChoreCollection);
    //    return choresCollection.DeleteOneAsync(c => c.Id == chore.Id);
    //}
    //public Task DeleteUser(string name)
    //{
    //    var usersCollections = ConnectToMongo<userModel>(UserCollection);
    //    return usersCollections.DeleteOneAsync(c => c.name == name);
    //}



    //public async Task CompleteChore(choreModel chore)
    //{
    //    var ChoresCollection = ConnectToMongo<choreModel>(ChoreCollection);
    //    var filer = Builders<choreModel>.Filter.Eq("Id", chore.Id);
    //    await ChoresCollection.ReplaceOneAsync(filer, chore);

    //    var choreHistoryCollection = ConnectToMongo<choreHistoryModel>(ChoreHistoryCollection);
    //    await choreHistoryCollection.InsertOneAsync(new choreHistoryModel(chore));




    //}

}