using UnityEngine;
using TMPro;
using System.Collections;
public class ShiftInventoryFromClickControllerDepricated : MonoBehaviour
{
    public GameObject GameObjectToShift;
    public TextMeshProUGUI TextToChange;
    private RectTransform rectTransform;
    private bool ShiftedOut = false;
    private Coroutine currentMovementCoroutine = null;

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

    public void ShiftOut()
    {
        if (currentMovementCoroutine != null)
        {
            StopCoroutine(currentMovementCoroutine);
        }
        currentMovementCoroutine = StartCoroutine(MoveToTargetOverTime(545f, 1.0f));
    }

    public void ShiftIn()
    {
        if (currentMovementCoroutine != null)
        {
            StopCoroutine(currentMovementCoroutine);
        }
        currentMovementCoroutine = StartCoroutine(MoveToTargetOverTime(737f, 1.0f));
    }

    private IEnumerator MoveToTargetOverTime(float targetPosition, float duration)
    {
        float elapsedTime = 0f;
        float startPosition = rectTransform.localPosition.x;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration; 
            rectTransform.localPosition = Vector3.Lerp(new Vector3(startPosition, rectTransform.localPosition.y, rectTransform.localPosition.z), new Vector3(targetPosition, rectTransform.localPosition.y, rectTransform.localPosition.z), t);
            elapsedTime += Time.deltaTime; 
            
            yield return null; 
        }

        rectTransform.localPosition = new Vector3(targetPosition, rectTransform.localPosition.y, rectTransform.localPosition.z);
        currentMovementCoroutine = null;
    }
    private void Update()
    {
        if(ShiftedOut)
        {
            TextToChange.text = "-->";
        } else
        {
            TextToChange.text = "<--";
        }
    }
}
