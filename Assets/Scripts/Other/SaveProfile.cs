using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class SaveProfile<T> where T : SaveProfileData {
    public string name;
    public T saveData;

    private SaveProfile() { }

    public SaveProfile(string name, T saveData) {
        this.name = name;
        this.saveData = saveData;
    }
}

public abstract record SaveProfileData { }

public record PlayerSaveData : SaveProfileData {
    public Vector3 position;
}

public record SceneSaveData : SaveProfileData {
    public Scene scene;
    public List<Vector3> objectPositions;
    public List<Quaternion> objectRotations;
    public List<bool> objectStates;
}