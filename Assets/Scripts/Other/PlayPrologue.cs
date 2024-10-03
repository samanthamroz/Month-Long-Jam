using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PlayPrologue : MonoBehaviour
{
    public Image[] frames; 
    public float displayTime = 6f; 
    public float hideTime = 1f; 
    [SerializeField] private string firstScene;
    public AudioSource source;

    private void Start()
    {
        //source.Play();
        StartCoroutine(ControlImages());
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(firstScene);
        }
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

