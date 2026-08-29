using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData", menuName = "Data/DialogueObject", order = 0)]
public class DialogueScriptableObject : ScriptableObject
{
    [SerializeField] private string _dialogueId;
    [SerializeField] private DialogueLine[] _dialogueLines;

    public string DialogueId
    {
        get => _dialogueId;
    }

    public DialogueLine[] DialogueLines
    {
        get => _dialogueLines;
    }
}

[Serializable]
public struct DialogueLine
{
    public string Text;

    public StatementType StatementType;

    public string ResponseId;
}
