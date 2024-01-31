using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class FileHandler 
{
    public static void SaveToJSON<T> (List<T> toSave, string filename) 
    {
        Debug.Log (GetPath (filename));
        var content = JsonHelper.ToJson<T> (toSave.ToArray ());
        WriteFile (GetPath (filename), content);
    }

    public static void SaveToJSON<T> (T toSave, string filename) 
    {
        var content = JsonUtility.ToJson (toSave);
        WriteFile (GetPath (filename), content);
    }

    public static List<T> ReadListFromJSON<T> (string filename) 
    {
        var content = ReadFile (GetPath (filename));

        if (string.IsNullOrEmpty (content) || content == "{}") {
            return new List<T> ();
        }
        var res = JsonHelper.FromJson<T> (content).ToList ();
        return res;
    }

    public static T ReadFromJSON<T> (string filename) 
    {
        var content = ReadFile (GetPath (filename));

        if (string.IsNullOrEmpty (content) || content == "{}") {
            return default (T);
        }

        var res = JsonUtility.FromJson<T> (content);
        return res;
    }
    private static void WriteFile (string path, string content) 
    {
        var fileStream = new FileStream (path, FileMode.Create);

        using var writer = new StreamWriter (fileStream);
        writer.Write (content);
    }

    private static string ReadFile (string path) 
    {
        if (!File.Exists(path)) return "";
        
        using var reader = new StreamReader (path);
        var content = reader.ReadToEnd ();
        return content;
    }
    
    private static string GetPath (string filename) => Application.persistentDataPath + "/" + filename;
}