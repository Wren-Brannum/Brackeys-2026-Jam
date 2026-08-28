using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class questionController : MonoBehaviour
{
    [SerializeField] public GameObject questionPrefab;
    
    public DialogueScriptableObject questionDialogue; 
    public SpeechController activeSpeechController;

    private float questionHeight = 45f;

    public void deleteQuestion(GameObject question)
    {
        Destroy(question);
    }

    public void createQuestion(string questionText)
    {
        GameObject chatBoard = GameObject.FindGameObjectWithTag("ChatBoard");
        if (chatBoard == null) return; 
        Vector3 spawnPosition = CalculateSpawnPosition(chatBoard);
        GameObject newQ = InstantiatePrefab(spawnPosition, chatBoard);
        
        CreateTextContainer(newQ, questionText);

        SetupButtonInteraction(newQ, activeSpeechController, questionDialogue);
    }

    private Vector3 CalculateSpawnPosition(GameObject parent)
    {
        GameObject[] existingSlots = GameObject.FindGameObjectsWithTag("QuestionInstance");

        if (existingSlots.Length > 0)
        {
            Vector3 lastSlotWorldPos = existingSlots[existingSlots.Length - 1].transform.position;
            Vector3 localPos = parent.transform.InverseTransformPoint(lastSlotWorldPos);
            localPos.y -= questionHeight;
            return localPos;
        }

        return new Vector3(0, 0, 0);
    }

    private GameObject InstantiatePrefab(Vector3 position, GameObject parent)
    {
        GameObject newQ = Instantiate(questionPrefab, parent.transform.position, Quaternion.identity);
        newQ.transform.SetParent(parent.transform);
        newQ.transform.localPosition = position;

        if (!newQ.CompareTag("QuestionInstance")) 
            newQ.tag = "QuestionInstance";

        return newQ;
    }

    private void CreateTextContainer(GameObject parent, string text)
    {
        GameObject textObj = new GameObject("QuestionText");
        RectTransform rectTransform = textObj.AddComponent<RectTransform>();
        TextMeshProUGUI tmpComponent = textObj.AddComponent<TextMeshProUGUI>();
        tmpComponent.fontSize = 18;

        textObj.transform.SetParent(parent.transform, false);

        rectTransform.anchorMin = Vector2.zero; 
        rectTransform.anchorMax = Vector2.one;  
        rectTransform.offsetMin = Vector2.zero; 
        rectTransform.offsetMax = Vector2.zero; 

        tmpComponent.text = text;
    }

    private void SetupButtonInteraction(GameObject prefabInstance, SpeechController speechController, DialogueScriptableObject dialogue)
    {
        Button targetButton = prefabInstance.GetComponent<Button>();

        if (speechController != null && dialogue != null)
        {
            targetButton.onClick.AddListener(() => 
            {
                speechController.setDialogue(dialogue);
            });
            targetButton.onClick.AddListener(() => 
            {
                deleteQuestion(prefabInstance);
            });
        }
        else
        {
            Debug.LogError("Failed to set dialogue: SpeechController or QuestionDialogue is missing in Inspector!");
        }
    }
}
