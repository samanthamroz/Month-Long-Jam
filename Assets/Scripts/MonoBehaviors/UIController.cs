using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject canvasPrefab;
    private GameObject canvasRef;

    void Awake()
    {
        canvasRef = Instantiate(canvasPrefab);
        SetInteractPopupActive(false);
    }

    public void SetInteractPopupActive(bool isActive, string text = "null") {
        GameObject popup = canvasRef.transform.GetChild(0).gameObject;
        popup.SetActive(isActive);
        popup.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = text;
    }
}
