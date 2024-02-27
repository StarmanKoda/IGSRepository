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

    public static saveProfile<T> Load<T>(string profileName) where T : SaveProfileData
    {
        if (!File.Exists($"{saveFolder}/{profileName}"))
        {
            throw new System.Exception($"Save Profile {profileName} not found!");
        }

        var fileContents = File.ReadAllText($"{saveFolder}/{profileName}");
        Debug.Log("Successfully loaded profile");
        return JsonUtility.FromJson<saveProfile<T>>(fileContents);
    }

    public static void Save<T>(saveProfile<T> save) where T : SaveProfileData
    {
        if (!Directory.Exists(saveFolder))
        {
            Directory.CreateDirectory(saveFolder);
        }
        var JsonString = JsonUtility.ToJson(save);
        File.WriteAllText($"{saveFolder}/{save.name}", JsonString);
    }
}
