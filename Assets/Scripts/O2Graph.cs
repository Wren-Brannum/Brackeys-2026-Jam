using UnityEngine;
using UnityEngine.UI;

public class O2Graph : MaskableGraphic
{

    public float breathingSpeed = 1.5f;
    private float initialY;

    void Start()
    {
        initialY = GetComponent<RectTransform>().anchoredPosition.y;
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * breathingSpeed) * 94f;

        GetComponent<RectTransform>().anchoredPosition = new Vector2(
            GetComponent<RectTransform>().anchoredPosition.x,
            initialY + yOffset
        );
    }
}
