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
        GameObject[] existingSlots = GameObject.FindGameObjectsWithTag("QuestionInstance");
        Vector3 spawnPosition;

        if (existingSlots.Length > 0)
        {
            spawnPosition = new Vector3(0,0,0);
        }
        else
        {
            GameObject[] questionSlots = GameObject.FindGameObjectsWithTag("QuestionInstance");
            Vector3 currentQuestionPosition = questionSlots[questionSlots.Length - 1].transform.position;
            spawnPosition = new Vector3(currentQuestionPosition.x, 
            currentQuestionPosition.y - questionHeight, currentQuestionPosition.z);
        }

        GameObject newQ = Instantiate(questionPrefab, spawnPosition, Quaternion.identity);
        
        GameObject textContainer = new GameObject("QuestionText");
        RectTransform rectTransform = textContainer.AddComponent<RectTransform>();
        TextMeshProUGUI tmpComponent = textContainer.AddComponent<TextMeshProUGUI>();

        textContainer.transform.SetParent(newQ.transform, false);

        rectTransform.anchorMin = Vector2.zero; 
        rectTransform.anchorMax = Vector2.one;  
        rectTransform.offsetMin = Vector2.zero; 
        rectTransform.offsetMax = Vector2.zero; 

        tmpComponent.text = questionText;
        
        if (!newQ.CompareTag("QuestionSlot")) newQ.tag = "QuestionSlot";
    }
}
