using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autosave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var playerSave = new PlayerSaveData{
            position = gameObject.transform.position
        };

        var saveProfile  = new SaveProfile<PlayerSaveData>("playerSaveData", playerSave);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
