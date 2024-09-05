using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject prefab;
    GameObject clone;
    
    // Start is called before the first frame update
    void Start()
    {
        clone = Instantiate(prefab, gameObject.transform.position, gameObject.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (clone.activeSelf == false)
        {
            clone = Instantiate(prefab, gameObject.transform.position, gameObject.transform.rotation);
        }
    }
}