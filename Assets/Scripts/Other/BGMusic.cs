using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMusic : MonoBehaviour
{
    //this BGMusic is ONLY for the dungeon music! the scenes listed in update have their own audio sources
    //also i dont like using so many if else statements in update since those get called like
    //every frame right? if theres a better way let me know 
    public static BGMusic instance;
    public bool paused = false;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null) {
            Destroy(gameObject);
        }
        else {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Update() {
        if (SceneManager.GetActiveScene().name == "TitleScreen") {
            BGMusic.instance.GetComponent<AudioSource>().Pause();
            paused = true;
        }
        else if (SceneManager.GetActiveScene().name == "Prologue") {
            BGMusic.instance.GetComponent<AudioSource>().Pause();
            paused = true;
        }
        else if (SceneManager.GetActiveScene().name == "Ending") {
            BGMusic.instance.GetComponent<AudioSource>().Pause();
            paused = true;
        }
        else if (paused) {
            BGMusic.instance.GetComponent<AudioSource>().UnPause();
        }
    }
}
