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
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interaction() {
        gameObject.GetComponent<Tilemap>().SetTile(new Vector3Int(7,7,0), changeToTiles[0]);
        gameObject.GetComponent<Tilemap>().SetTile(new Vector3Int(8,7,0), changeToTiles[1]);
        gameObject.GetComponent<Tilemap>().SetTile(new Vector3Int(7,6,0), changeToTiles[2]);
        gameObject.GetComponent<Tilemap>().SetTile(new Vector3Int(8,6,0), changeToTiles[3]);
        gameObject.GetComponent<Tilemap>().SetTile(new Vector3Int(7,5,0), changeToTiles[4]);
        gameObject.GetComponent<Tilemap>().SetTile(new Vector3Int(8,5,0), changeToTiles[5]);

        isBroken = true;
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    void OnInteract() {
        if (!isBroken) {

        } else {

        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        SceneManager.LoadScene(sceneToLoad.name);
    }
}