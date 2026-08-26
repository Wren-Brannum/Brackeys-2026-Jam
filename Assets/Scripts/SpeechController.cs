using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeechController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private float _charactersPerSecond = 20f;

    private DialogueScriptableObject _activeDialogue;
    private int _dialogueIndex;

    private Coroutine _displayTextCoroutine;

#if UNITY_EDITOR
    [Tooltip("Debug")]
    [SerializeField] private DialogueScriptableObject _initialDialogue;
#endif

    public DialogueScriptableObject ActiveDialogue
    {
        get => _activeDialogue;
        set
        {
            _activeDialogue = value;
            _dialogueIndex = 0;

            DisplayText();
        }
    }

    private void Awake()
    {
#if UNITY_EDITOR
        ActiveDialogue = _initialDialogue;
#endif
    }

    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(AdvanceText);
    }

    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(AdvanceText);
    }

    private void DisplayText()
    {
        if (_displayTextCoroutine != null)
        {
            StopCoroutine(_displayTextCoroutine);
        }

        _displayTextCoroutine = StartCoroutine(DisplayTextCoroutine());
    }

    private IEnumerator DisplayTextCoroutine()
    {
        _text.text = "";

        var dialogueLine = _activeDialogue.DialogueLines[_dialogueIndex];

        foreach (char character in dialogueLine.Text)
        {
            _text.text += character;
            yield return new WaitForSeconds(1f / _charactersPerSecond);
        }

        _displayTextCoroutine = null;
    }

    private void AdvanceText()
    {
        if (_displayTextCoroutine != null)
        {
            StopCoroutine(_displayTextCoroutine);
            _text.text = _activeDialogue.DialogueLines[_dialogueIndex].Text;
            _displayTextCoroutine = null;
        } else if (_dialogueIndex < _activeDialogue.DialogueLines.Length)
        {
            _dialogueIndex++;
            DisplayText();
        }
    }
}
