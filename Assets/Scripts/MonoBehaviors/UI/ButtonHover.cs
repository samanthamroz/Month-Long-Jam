using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHover : MonoBehaviour
{
    public Vector3 normalScale = new Vector3(1, 1, 1);
    public Vector3 hoverScale = new Vector3(1.2f, 1.2f, 1.2f);
    public float speed = 2f;

    public void OnMouseEnter()
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, hoverScale, speed);
    }

    public void OnMouseExit()
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, normalScale, speed);
    }
}
