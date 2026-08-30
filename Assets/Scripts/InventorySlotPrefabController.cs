using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class InventorySlotPrefabController : MonoBehaviour
{
    public TextMeshProUGUI QuestionText;
    public string Question;
    public int questionId = -1; 
    public bool isEnabled = true;

    private void onClick()
    {
        if (isEnabled)
        {
            QuestionText.text = Question;
        }
    }

    private void Start()
    {
        if (GetComponent<Button>() == null)
        {
            gameObject.AddComponent<Button>();
        }
        GetComponent<Button>().onClick.AddListener(onClick);
    }
}
