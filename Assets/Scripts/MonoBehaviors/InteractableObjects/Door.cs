using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : InteractableObject
{
    [SerializeField] SceneAsset sceneAsset;
    public override string HoverText()
    {
        return "Open";
    }
    
    public override void Interaction(GameObject player) {
        if (sceneAsset != null)
        {
            SceneManager.LoadScene(sceneAsset.name);
        }
    }
}