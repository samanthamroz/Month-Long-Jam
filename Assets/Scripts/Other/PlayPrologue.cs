using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PlayPrologue : MonoBehaviour
{
    public Image[] frames; 
    public float displayTime = 4f; 
    public float hideTime = 1f; 
    [SerializeField] private string firstScene;

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

            CanvasGroup canvasGroup = frame.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0f;

            LeanTween.alphaCanvas(frame.GetComponent<CanvasGroup>(), 1f, 1f); 
            yield return new WaitForSeconds(hideTime); 

            yield return new WaitForSeconds(displayTime - 1f); 

            LeanTween.alphaCanvas(frame.GetComponent<CanvasGroup>(), 0f, 1f); 
            yield return new WaitForSeconds(hideTime); 

            frame.gameObject.SetActive(false);
        }

        SceneManager.LoadScene(firstScene);
    }
}

