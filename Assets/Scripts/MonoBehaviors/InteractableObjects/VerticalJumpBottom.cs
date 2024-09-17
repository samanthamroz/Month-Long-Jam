using UnityEngine;

public class VerticalJumpBottom : InteractableObject
{
    public float jumpTime;
    public float jumpHeight;
    public override string HoverText()
    {
        return "Jump";
    }

    public override void Interaction(GameObject player)
    {
        StartCoroutine(player.GetComponent<PlayerController>().DoCutscene(jumpTime));
        LeanTween.cancel(player);
        LeanTween.moveLocalY(player, player.transform.position.y + 2, jumpTime);
        LeanTween.scale(player, new Vector3(player.transform.localScale.x + jumpHeight, player.transform.localScale.y + jumpHeight, player.transform.localScale.z), jumpTime).setLoopCount(2).setLoopPingPong().setEaseInOutQuad();
    
    
    }
}