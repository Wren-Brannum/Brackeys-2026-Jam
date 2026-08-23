using UnityEngine;
using TMPro;
public class ShiftInventoryFromClickController : MonoBehaviour
{
    public GameObject GameObjectToShift;
    private RectTransform rectTransform;
    private bool ShiftedOut = false; 

    void Start()
    {
        if (GameObjectToShift != null)
            rectTransform = GameObjectToShift.GetComponent<RectTransform>();
    }

    public void ButtonPressed()
    {
        if (rectTransform == null) return;
        if(!ShiftedOut)
        {
            ShiftOut();
        }
        else
        {
            ShiftIn();
        }
        ShiftedOut = !ShiftedOut;
    }

    private void ShiftOut()
    {
        print("shifted out");
        if (rectTransform == null) return;
        Vector3 currentPosition = rectTransform.localPosition;
        rectTransform.localPosition = new Vector3(837f, currentPosition.y, currentPosition.z);
    }
    private void ShiftIn()
    {
        print("shifted in");
        if (rectTransform == null) return;
        Vector3 currentPosition = rectTransform.localPosition;
        rectTransform.localPosition = new Vector3(1131f, currentPosition.y, currentPosition.z);
    }
}
