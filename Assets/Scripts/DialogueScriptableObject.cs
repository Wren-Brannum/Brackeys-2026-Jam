using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData", menuName = "Data/DialogueObject", order = 0)]
public class DialogueScriptableObject : ScriptableObject
{
    [SerializeField] private DialogueLine[] _dialogueLines;

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

    public bool AffectStress;
}
