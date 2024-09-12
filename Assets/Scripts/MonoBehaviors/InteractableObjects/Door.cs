using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : InteractableObject
{
    [SerializeField] string sceneToLoad;
    public override string HoverText()
    {
        return "Open";
    }
    
    public override void Interaction(GameObject player) {
        if (sceneToLoad != null)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}