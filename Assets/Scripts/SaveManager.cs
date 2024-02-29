using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class SaveManager
{
    private static readonly string saveFolder = Application.persistentDataPath + "/GameData";

    public static void Delete(string profile)
    {
        if (!File.Exists($"{saveFolder}/{profile}"))
        {
            return;
        }
        File.Delete($"{saveFolder}/{profile}");
    }

    public static saveProfile Load(string profileName)
    {
        if (!File.Exists($"{saveFolder}/{profileName}"))
        {
            throw new System.Exception($"Save Profile {profileName} not found!");
        }

        var fileContents = File.ReadAllText($"{saveFolder}/{profileName}");
        Debug.Log($"Successfully loaded profile: {saveFolder}/{profileName}");
        return JsonUtility.FromJson<saveProfile>(fileContents);
    }

    public static void Save(saveProfile save)
    {
        if (!Directory.Exists(saveFolder))
        {
            Directory.CreateDirectory(saveFolder);
        }
        var JsonString = JsonUtility.ToJson(save);
        File.WriteAllText($"{saveFolder}/{save.name}", JsonString);
    }
}
