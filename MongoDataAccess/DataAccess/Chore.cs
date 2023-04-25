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
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.InteropServices;

namespace MongoDataAccess.DataAccess;


public class Chore
{
    public  (JToken, JToken) AddChangedAttribute(JToken original, JToken modified)
    {
        List<string> listAttributs = new List<string>();
        listAttributs.Add("CreatedDate");
        listAttributs.Add("UpdatedDate");
        listAttributs.Add("Id");
        List<string> listID = new List<string>();
        listID.Add("Vocabularies");
        var originalObject = original.ToObject<JObject>();
        var modifiedObject = modified.ToObject<JObject>();
        AddChangedAttributeRecursive(originalObject, modifiedObject, listAttributs,listID);
        string jsonStringOne = JsonConvert.SerializeObject(originalObject);
        JToken originalJToken = JToken.Parse(jsonStringOne);
        string jsonStringTwo = JsonConvert.SerializeObject(modifiedObject);
        JToken modifiedJToken = JToken.Parse(jsonStringTwo);

        return (originalJToken, modifiedJToken);
    }

    private  void AddChangedAttributeRecursive(JObject originalObject, JObject modifiedObject, List<string> listAttributs, List<string> listID)
    {
        foreach (var property in modifiedObject.Properties())
        {
            
            var originalValue = originalObject[property.Name];
            var modifiedValue = modifiedObject[property.Name];



            if (originalValue != null && modifiedValue != null && !listAttributs.Contains(property.Name))
            {
                if (originalValue.Type == JTokenType.Object)
                {
                    AddChangedAttributeRecursive((JObject)originalValue, (JObject)modifiedValue, listAttributs, listID);
                }

            

                else if (modifiedValue.Type == JTokenType.Array)
                {
                    var originalArray = (JArray)originalValue;
                    var modifiedArray = (JArray)modifiedValue;

   

                        // eli mawjoudin fi wahda w mch mawjoudin f lokhra
                        var result1 = modifiedArray.Where(x => !originalArray.Any(y => JToken.DeepEquals(x, y)));
                        JArray array = JArray.FromObject(result1);
                        var result2 = originalArray.Where(x => !modifiedArray.Any(y => JToken.DeepEquals(x, y)));
                        JArray array2 = JArray.FromObject(result2);

                 
                    foreach (JObject obj1 in originalArray)
                    {

                        // ken aandou id compari
                        if (listID.Contains(property.Name) ) {
                            Console.WriteLine("works on " + property.Name);

                            string id = obj1["Id"].ToString();
                            JObject obj2 = modifiedArray.FirstOrDefault(o => o["Id"].ToString() == id) as JObject;

                            if (obj2 != null)
                            {
                                if (JToken.DeepEquals(obj1, obj2)){obj1["changed"] = "same with same id";} 
                                else { obj1["changed"] = "changed with same id"; }
                            }


                        }
                       
                        //ken fama added zid
                        else
                            {
                                if (array != null)
                                {
                                    for (int i = 0; i < modifiedArray.Count; i++)
                                    {
                                        if (array.Any(j => j.ToString() == modifiedArray[i].ToString()))
                                        {
                                            modifiedArray[i]["changed"] = "added";
                                        }
                                    }
                                }
                                //ken fama deleted zid

                                if (array2 != null)
                                {
                                    for (int i = 0; i < originalArray.Count; i++)
                                    {

                                        if (array2.Any(j => j.ToString() == originalArray[i].ToString()))
                                        {

                                            originalArray[i]["changed"] = "deleted";

                                        }
                                    }
                                }
                            }
                        
                    }

             
                    for (int i = 0; i < originalArray.Count; i++)
                    {
                        if (originalArray[i].Type == JTokenType.Object)
                        {
                            if (i<modifiedArray.Count)
                            {
                                AddChangedAttributeRecursive((JObject)originalArray[i], (JObject)modifiedArray[i], listAttributs, listID);

                            }

                        }
                    }   
                     
                 



                }
                else if (!JToken.DeepEquals(originalValue, modifiedValue))
                {




                    if (originalValue.Parent.Parent.SelectToken("changed") == null  ||  originalValue.Parent.Parent["changed"].HasValues.ToString() == "same")
                     
                    {
                        originalValue.Parent.Parent["changed"] = "changed";
                    }


                }
                else if (JToken.DeepEquals(originalValue, modifiedValue))
                {
                    if (originalValue.Parent.Parent.SelectToken("changed") == null)
                    {
                        originalValue.Parent.Parent["changed"] = "same";
                    }




                }
            }
        }
    }
}






