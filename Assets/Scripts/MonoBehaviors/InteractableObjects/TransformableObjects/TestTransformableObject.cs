using UnityEngine;

public class TestTransformableObject : TransformableObject
{
    public override void Interaction(GameObject player) {
        transform.position = Vector3.zero;
    }
}