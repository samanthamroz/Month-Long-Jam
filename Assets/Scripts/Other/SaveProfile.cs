using UnityEngine;

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
    public Vector2 position;
}

public record WorldSaveData : SaveProfileData {
    public Vector2 position;
}