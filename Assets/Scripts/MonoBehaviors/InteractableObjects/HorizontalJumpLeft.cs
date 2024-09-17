using UnityEngine;

public class HorizontalJumpLeft : InteractableObject
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
        LeanTween.moveLocalX(player, player.transform.position.x + 2, jumpTime);
        LeanTween.scale(player, new Vector3(player.transform.localScale.x + jumpHeight, player.transform.localScale.y + jumpHeight, player.transform.localScale.z), jumpTime).setLoopCount(2).setLoopPingPong().setEaseInOutQuad();
    }
}