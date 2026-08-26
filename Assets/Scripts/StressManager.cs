using System;
using System.Linq;
using UnityEngine;

public class StressManager : MonoBehaviour
{
    [SerializeField] private DialogueScriptableObject _defaultHonestResponse;
    [SerializeField] private DialogueScriptableObject _defaultLieResponse;

    [SerializeField] private AccuseResponse[] _responses;

    [SerializeField] private SpeechController _speechController;

    public void ActivateResponse(string responseId, StatementType statementType)
    {
        var response = _responses.FirstOrDefault(x => x.ResponseId == responseId);

        // None found
        if (response == null)
        {
            if (statementType == StatementType.LIE)
            {
                _speechController.ActiveDialogue = _defaultLieResponse;
            } else
            {
                _speechController.ActiveDialogue = _defaultHonestResponse;
            }

            return;
        }

        if (response.Activated == true)
        {
            return;
        } else
        {
            response.Activated = true;
            _speechController.ActiveDialogue = response.DialogueResponse;
        }
    }

    [Serializable]
    public class AccuseResponse
    {
        public string ResponseId;
        public DialogueScriptableObject DialogueResponse;

        [HideInInspector] public bool Activated;
    }
}
