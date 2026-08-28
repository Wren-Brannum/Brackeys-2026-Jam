using System.Collections;
using TMPro;
using UnityEngine;

public class questionController : MonoBehaviour
{
    public GameObject questionToDestroy;
    [SerializeField] public GameObject questionPrefab;
    private float questionHeight = 45f;

    public void deleteQuestion()
    {
        Destroy(questionToDestroy);
    }

    public void createQuestion(string questionText)
    {
        GameObject chatBoard = GameObject.FindGameObjectWithTag("ChatBoard");
        if (chatBoard == null) return;

        GameObject[] existingSlots = GameObject.FindGameObjectsWithTag("QuestionInstance");
        
        Vector3 localSpawnPosition;

        if (existingSlots.Length > 0)
        {
            Vector3 currentSlotWorldPos = existingSlots[existingSlots.Length - 1].transform.position;
            
            localSpawnPosition = chatBoard.transform.InverseTransformPoint(currentSlotWorldPos);
            
            localSpawnPosition.y -= questionHeight;
        }
        else
        {
            localSpawnPosition = new Vector3(0, 0, 0);
        }

        GameObject newQ = Instantiate(questionPrefab, chatBoard.transform.position, Quaternion.identity);

        newQ.transform.SetParent(chatBoard.transform);
        
        newQ.transform.localPosition = localSpawnPosition;

        if (!newQ.CompareTag("QuestionInstance")) newQ.tag = "QuestionInstance";

        GameObject textContainer = new GameObject("QuestionText");
        RectTransform rectTransform = textContainer.AddComponent<RectTransform>();
        TextMeshProUGUI tmpComponent = textContainer.AddComponent<TextMeshProUGUI>();

        textContainer.transform.SetParent(newQ.transform, false);

        rectTransform.anchorMin = Vector2.zero; 
        rectTransform.anchorMax = Vector2.one;  
        rectTransform.offsetMin = Vector2.zero; 
        rectTransform.offsetMax = Vector2.zero; 

        tmpComponent.text = questionText;
    }
}
