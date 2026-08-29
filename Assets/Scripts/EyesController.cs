using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EyesController : MonoBehaviour
{
    public Sprite[] beautifulEyes;
    public Image leftEye;
    public Image rightEye;
    public void ChangeBothEyesViaIndex(int eyeIndex)
    {
        leftEye.sprite = beautifulEyes[eyeIndex];
        rightEye.sprite = beautifulEyes[eyeIndex];
    }
}
