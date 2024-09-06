using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public float spawnTime;
    public GameObject prefab;
    public PlayerController playerController;
    private GameObject clone;
    public int maxClones = 3;
    private List<GameObject> clones = new List<GameObject>();
    private GameObject currentClone;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnBoulder();
    }

    // Update is called once per frame
    void Update()
    {
        if (clone.activeSelf == false)
        {
            SpawnBoulder();
        }
    }

    private void SpawnBoulder() {
        clone = Instantiate(prefab, gameObject.transform.position, gameObject.transform.rotation);
        clone.GetComponent<RollableObject>().playerController = this.playerController;
        clones.Add(currentClone);

        Vector3 originalScale = clone.transform.GetChild(0).localScale;
        clone.transform.GetChild(0).localScale = new Vector3(originalScale.x + .5f, originalScale.y + .5f, originalScale.z + .5f);
        LeanTween.scale(clone.transform.GetChild(0).gameObject, originalScale, spawnTime);
        //StartCoroutine(playerController.DoCutscene(spawnTime));
    }

    void DestroyExtraClones()
    {
        while (clones.Count > maxClones)
        {
            GameObject cloneToDestroy = clones[0];
            clones.RemoveAt(0);
            Destroy(cloneToDestroy);
        }
    }

}