using UnityEngine;
using TMPro;
using System.Collections;

public class ShiftGameObjectMasterController : MonoBehaviour
{
    public GameObject GameObjectToShift;
    public float ShiftOutY = 0f;
    public float ShiftInY = 0f;
    public float ShiftOutX = 0f;
    public float ShiftInX = 0f;
    public float Duration = 1.0f;
    public string InText = "";
    public string OutText = "";
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
        print("pressed");
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
        currentMovementCoroutine = StartCoroutine(MoveToTargetOverTime(ShiftOutX, ShiftOutY));
    }

    public void ShiftIn()
    {
        if (currentMovementCoroutine != null)
        {
            StopCoroutine(currentMovementCoroutine);
        }
        currentMovementCoroutine = StartCoroutine(MoveToTargetOverTime(ShiftInX, ShiftInY));
    }

    private IEnumerator MoveToTargetOverTime(float targetXPosition, float targetYPosition)
    {
        float elapsedTime = 0f;

        while (elapsedTime < Duration)
        {
            float t = elapsedTime / Duration; 
            rectTransform.localPosition = 
            Vector3.Lerp(new Vector3(rectTransform.localPosition.x, rectTransform.localPosition.y, rectTransform.localPosition.z), 
                new Vector3(targetXPosition == 0f ? rectTransform.localPosition.x : targetXPosition, 
                targetYPosition == 0f ? rectTransform.localPosition.y : targetYPosition, 
                rectTransform.localPosition.z), t);
            elapsedTime += Time.deltaTime; 
            
            yield return null; 
        }

        rectTransform.localPosition = 
            new Vector3(targetXPosition == 0f ? rectTransform.localPosition.x : targetXPosition, 
                targetYPosition == 0f ? rectTransform.localPosition.y : targetYPosition, 
                rectTransform.localPosition.z);
        currentMovementCoroutine = null;
    }
    private void Update()
    {
        if(ShiftedOut)
        {
            TextToChange.text = OutText;
        } else
        {
            TextToChange.text = InText;
        }
    }
}
