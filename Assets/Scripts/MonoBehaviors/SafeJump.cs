using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeJump : MonoBehaviour
{
    //this script should allow the player to, when on water trigger, be sent outside of trigger area. ive been avoiding using a method i've seen, which is
    //predetermined safe points, because i think i'd have to create More empty objects surrounding the water to do that and that sounds only a little annoying? 
    //i mean we could maybe make a ground tag to fix that issue? i dont know i need to go to bed so i'll work that out later. if you're seeing this comment then hi
    
    public float offset = 1.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Water")) 
        {
            TeleportOutsideTrigger(collision);
        }
    }

    private void TeleportOutsideTrigger(Collider2D triggerCollider)
    {
        if (triggerCollider == null) return;

        Bounds triggerBounds = triggerCollider.bounds;
        Vector3 triggerCenter = triggerBounds.center;

        Vector3 direction = (transform.position - triggerCenter).normalized;
    
        float maxExtent = Mathf.Max(triggerBounds.extents.x, triggerBounds.extents.y, triggerBounds.extents.z);
        float safeDistance = maxExtent + offset;

        Vector3 newPosition = triggerCenter + direction * safeDistance;

        transform.position = newPosition;

    }
}