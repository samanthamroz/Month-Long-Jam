using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    void Start() {
        Debug.Log(Application.persistentDataPath);
    }
}