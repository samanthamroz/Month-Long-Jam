using UnityEngine;

public class VerticalJumpTop : InteractableObject
{
    public float jumpTime;
    public float jumpHeight;
    public override string HoverText()
    {
        return "Jump";
    }

    public override void Interaction(GameObject player)
    {
        StartCoroutine(player.GetComponent<PlayerController>().DoCutscene(jumpTime, true));
        LeanTween.cancel(player);
        LeanTween.moveLocalY(player, player.transform.position.y - 2.5f, jumpTime).setOnUpdate((float val) => 
            {
                // Update the Rigidbody2D position to follow the LeanTween
                player.GetComponent<Rigidbody2D>().position = Vector2.Lerp(player.GetComponent<Rigidbody2D>().position, player.transform.position, val);
            });
        LeanTween.scale(player, new Vector3(player.transform.localScale.x + jumpHeight, player.transform.localScale.y + jumpHeight, player.transform.localScale.z), jumpTime).setLoopCount(2).setLoopPingPong().setEaseInOutQuad();
    }
}