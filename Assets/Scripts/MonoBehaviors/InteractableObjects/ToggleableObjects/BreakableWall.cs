using UnityEngine;

public class BreakableWall : ToggleableObject
{
    //public AudioSource source;
    public AudioClip breaking;
    public override bool GetSaveState()
    {
        return gameObject.activeSelf;
    }

    public override void LoadFromSavedState(bool savedState)
    {
        if (!savedState) {
            gameObject.SetActive(false);
        }
    }

    public override void Interaction(GameObject player) {
        var saveData = SaveManager.Load<PlayerSaveData>().saveData;
        breaking = Resources.Load<AudioClip>("rockbreak2");
        //source.clip = breaking;
        if (saveData.itemsCollected.Contains(Tool.MIXER)) {
            SoundFXManager.instance.PlaySoundFXClip(breaking, transform, 1f);
            gameObject.SetActive(false);
            
            //source.Play();
        }
    }

    public override string HoverText()
    {
        var saveData = SaveManager.Load<PlayerSaveData>().saveData;
        if (saveData.itemsCollected.Contains(Tool.MIXER)) {
            return "Break";
        } else {
            return "Missing Item";
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        breaking = Resources.Load<AudioClip>("rockbreak2");
        if (other.gameObject.tag == "Breaker") {
            SoundFXManager.instance.PlaySoundFXClip(breaking, transform, 1f);
            //source.clip = breaking;
            //source.Play();
            try {
                other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            } finally {
                gameObject.SetActive(false);
            }
        }
    }
}