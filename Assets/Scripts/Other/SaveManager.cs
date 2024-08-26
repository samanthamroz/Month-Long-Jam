using System.IO;
using System;
using Newtonsoft.Json;
using UnityEngine;

public static class SaveManager {
    private static readonly string saveFolder = Application.persistentDataPath + "/GameData";

    public static SaveProfile<T> Load<T>(string profileName) where T : SaveProfileData {
        if (!File.Exists($"{saveFolder}/{profileName}")) {
            throw new System.Exception($"Save profile {profileName} not found");
        }

        var fileContents = File.ReadAllText($"{saveFolder}/{profileName}");
        Debug.Log($"Successfully loaded {saveFolder}/{profileName}");

        return JsonConvert.DeserializeObject<SaveProfile<T>>(fileContents);
    }
}