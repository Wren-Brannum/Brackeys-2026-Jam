using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
public class TextBoxController : MonoBehaviour
{
    public TextMeshProUGUI textBox;
    public string line;
    public float textSpeed = 0.05f;
    private bool isEnabled = true;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(onInteraction);
    }

    void onInteraction()
    {
        if(isEnabled == false)
        {
            return;
        }
        isEnabled = false;
        textBox.text = string.Empty;
        print("Interacted");
        StartCoroutine(TypeLine());
        
    }

    IEnumerator TypeLine()
    {
        foreach (char c in line.ToCharArray())
        {
            textBox.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
