using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    public void CheckForWater() {
        if (gameObject.layer == 3 || gameObject.transform.GetChild(0).gameObject.layer == 3
        || gameObject.transform.GetChild(1).gameObject.layer == 3) {
            transform.GetChild(1).position = transform.GetChild(0).position;

            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
