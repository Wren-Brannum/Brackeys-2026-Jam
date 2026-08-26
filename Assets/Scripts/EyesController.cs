using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EyesController : MonoBehaviour
{
    public Sprite[] beautifulEyes;
    public Image leftEye;
    public Image rightEye;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(onInteraction);
    }
    private void onInteraction()
    {
        leftEye.sprite = beautifulEyes[Random.Range(0, beautifulEyes.Length)];
        rightEye.sprite = beautifulEyes[Random.Range(0, beautifulEyes.Length)];
    }
}
