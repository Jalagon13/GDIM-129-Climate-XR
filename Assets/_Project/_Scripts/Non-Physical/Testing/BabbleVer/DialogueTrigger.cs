using UnityEngine;
using TMPro;

public class DialogueTrigger : MonoBehaviour
{
    public Canvas dialogueCanvas; // 新的小型 Canvas
    public TextMeshProUGUI dialogueText; // Canvas 中的 TextMeshPro 组件
    public DialogueData dialogueData;
    public string currentDialogueKey;
    public float bubbleHeightAboveObject = 2.0f;

    private string[] currentDialogueLines;
    private int currentLineIndex = 0;

    private void Start()
    {
        dialogueCanvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!dialogueCanvas.gameObject.activeSelf)
            {
                StartDialogue();
            }
            else
            {
                ContinueDialogue();
            }
        }

        if (dialogueCanvas.gameObject.activeSelf)
        {
            UpdateCanvasPosition();
        }
    }

    private void StartDialogue()
    {
        string dialogue = GetDialogueFromKey(currentDialogueKey);
        currentDialogueLines = ProcessDialogueText(dialogue).Split('\n');
        currentLineIndex = 0;

        if (currentDialogueLines.Length > 0)
        {
            dialogueText.text = currentDialogueLines[currentLineIndex];
            dialogueCanvas.gameObject.SetActive(true);
            UpdateCanvasPosition();
        }
    }

    private void ContinueDialogue()
    {
        if (currentLineIndex < currentDialogueLines.Length - 1)
        {
            currentLineIndex++;
            dialogueText.text = currentDialogueLines[currentLineIndex];
            UpdateCanvasPosition();
        }
        else
        {
            dialogueCanvas.gameObject.SetActive(false);
        }
    }

    private void UpdateCanvasPosition()
    {
        Vector3 canvasPosition = transform.position + Vector3.up * bubbleHeightAboveObject;
        dialogueCanvas.transform.position = canvasPosition;
    }

    private string GetDialogueFromKey(string key)
    {
        foreach (var entry in dialogueData.dialogues)
        {
            if (entry.key == key)
            {
                return entry.dialogueText;
            }
        }
        return "Dialogue not found!";
    }

    private string ProcessDialogueText(string rawText)
    {
        return rawText.Replace(";", "\n");
    }
}
