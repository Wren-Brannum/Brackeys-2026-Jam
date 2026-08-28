using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeechController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private float _charactersPerSecond = 20f;

    [SerializeField] private Image _nextIcon;

    [SerializeField] private IntervieweeController _activeInterviewee; // Make this gotten somewhere else if we can swap
    [SerializeField] private StressManager _stressManager;

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

        var dialogueLine = _activeDialogue.DialogueLines[_dialogueIndex];

        if (dialogueLine.AffectStress)
        {
            _activeInterviewee.AffectStressFromStatement(dialogueLine.StatementType);
        }

        _displayTextCoroutine = StartCoroutine(DisplayTextCoroutine(dialogueLine));
    }

    private IEnumerator DisplayTextCoroutine(DialogueLine dialogueLine)
    {
        _text.text = "";
        _nextIcon.gameObject.SetActive(false);

        foreach (char character in dialogueLine.Text)
        {
            _text.text += character;
            yield return new WaitForSeconds(1f / _charactersPerSecond);
        }

        ShowFullText();

        _displayTextCoroutine = null;
    }

    private void AdvanceText()
    {
        if (_displayTextCoroutine != null)
        {
            StopCoroutine(_displayTextCoroutine);
            ShowFullText();
            _displayTextCoroutine = null;
        } else if (_dialogueIndex < _activeDialogue.DialogueLines.Length - 1)
        {
            _dialogueIndex++;
            DisplayText();
        }
    }

    private void ShowFullText()
    {
        _text.text = _activeDialogue.DialogueLines[_dialogueIndex].Text;
        
        if (_dialogueIndex != _activeDialogue.DialogueLines.Length - 1)
        {
            _nextIcon.gameObject.SetActive(true);
        } else
        {
            _nextIcon.gameObject.SetActive(false);
        }
    }

    public void Accuse()
    {
        var dialogueLine = _activeDialogue.DialogueLines[_dialogueIndex];

        if (dialogueLine.StatementType == StatementType.LIE)
        {
            _activeInterviewee.Accuse(true);
        } else
        {
            _activeInterviewee.Accuse(false);
        }

        _stressManager.ActivateResponse(dialogueLine.ResponseId, dialogueLine.StatementType);
    }
    public void setDialogue(DialogueScriptableObject dialogue)
    {
        ActiveDialogue = dialogue;
    }
}
