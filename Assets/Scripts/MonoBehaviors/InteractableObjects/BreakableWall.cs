using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class BreakableWall : InteractableObject
{
    [SerializeField] private Object sceneToLoad;
    [SerializeField] private TileBase[] changeToTiles;
    private bool isBroken = false;

    public override void Interaction() {
        if (!isBroken) {
            gameObject.GetComponent<Tilemap>().SetTile(new Vector3Int(7,7,0), changeToTiles[0]);
            gameObject.GetComponent<Tilemap>().SetTile(new Vector3Int(8,7,0), changeToTiles[1]);
            gameObject.GetComponent<Tilemap>().SetTile(new Vector3Int(7,6,0), changeToTiles[2]);
            gameObject.GetComponent<Tilemap>().SetTile(new Vector3Int(8,6,0), changeToTiles[3]);
            gameObject.GetComponent<Tilemap>().SetTile(new Vector3Int(7,5,0), changeToTiles[4]);
            gameObject.GetComponent<Tilemap>().SetTile(new Vector3Int(8,5,0), changeToTiles[5]);

            isBroken = true;
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (isBroken) {
            SceneManager.LoadScene(sceneToLoad.name);
        }
    }
}