using UnityEngine;

[System.Serializable]
public class DialogueEntry
{
    public string key;
    [TextArea(3, 10)]
    public string dialogueText;
}

[CreateAssetMenu(fileName = "DialogueData", menuName = "ScriptableObjects/DialogueData", order = 1)]
public class DialogueData : ScriptableObject
{
    public DialogueEntry[] dialogues;
}
