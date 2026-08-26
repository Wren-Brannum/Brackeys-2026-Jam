using System.Collections;
using TMPro;
using UnityEngine;

public class SpeechController : MonoBehaviour
{
    private string _textToDisplay;

    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private float _charactersPerSecond = 20f;

#if UNITY_EDITOR
    [Tooltip("Debug")]
    [SerializeField] private string _initialText;
#endif

    public string TextToDisplay
    {
        get => _textToDisplay;
        set
        {
            _textToDisplay = value;
            StartCoroutine(DisplayText());
        }
    }

    private void Awake()
    {
#if UNITY_EDITOR
        TextToDisplay = _initialText;
#endif
    }

    private IEnumerator DisplayText()
    {
        _text.text = "";

        foreach (char character in _textToDisplay)
        {
            _text.text += character;
            yield return new WaitForSeconds(1f / _charactersPerSecond);
        }
    }
}
