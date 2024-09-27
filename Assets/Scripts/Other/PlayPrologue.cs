using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayPrologue : MonoBehaviour
{
    public Image[] frames; 
    public float displayTime = 2f; 
    public float hideTime = 1f; 

    //Decide: Do we want this before or after the title screen? Once we figure that out I assume we can just load next scene in this script and also whatever script 
    //might come before this one.
    //also, if we had time could leantween be used to make this look better?
    private void Start()
    {
        StartCoroutine(ControlImages());
    }

    private IEnumerator ControlImages()
    {
        foreach (Image frame in frames)
        {

            frame.gameObject.SetActive(true);
            yield return new WaitForSeconds(displayTime); 


            frame.gameObject.SetActive(false);
            yield return new WaitForSeconds(hideTime); 
        }
    }
}

