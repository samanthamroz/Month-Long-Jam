using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Autosave : MonoBehaviour
{
    void Start()
    {
        LoadRoom();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) {
            LoadRoom();
        }

        if (Input.GetKeyDown(KeyCode.V)) {
            SaveRoom();
        }
    }

    public static void SaveRoom() {
        //create blank sceneSave
        var sceneSave = new SceneSaveData {
            scene = SceneManager.GetActiveScene(),
            objectPositions = new List<Vector3>(),
            objectRotations = new List<Quaternion>(),
            objectStates = new List<bool>()
        };
        
        //add data
        foreach (TransformableObject t in (TransformableObject[])FindObjectsOfType(typeof(TransformableObject))) {
            sceneSave.objectPositions.Add(t.transform.position);
            sceneSave.objectRotations.Add(t.transform.rotation);
        }
        foreach (ToggleableObject g in (ToggleableObject[])FindObjectsOfType(typeof(ToggleableObject))) {
            sceneSave.objectStates.Add(g.GetSaveState());
        }

        //save to file
        var saveProfile  = new SaveProfile<SceneSaveData>(sceneSave, sceneSave.scene.name);
        SaveManager.Save(saveProfile);
    }

    public static void LoadRoom() {
        var saveData = SaveManager.Load<SceneSaveData>(SceneManager.GetActiveScene().name).saveData;
        
        TransformableObject[] tObjects = (TransformableObject[])FindObjectsOfType(typeof(TransformableObject));
        for (int i = 0; i < tObjects.Count(); i++) {
            tObjects[i].transform.SetPositionAndRotation(saveData.objectPositions[i], saveData.objectRotations[i]);
        }

        ToggleableObject[] sObjects = (ToggleableObject[])FindObjectsOfType(typeof(ToggleableObject));
        for (int i = 0; i < sObjects.Count(); i++) {
            sObjects[i].LoadFromSavedState(saveData.objectStates[i]);
        }
    }
}