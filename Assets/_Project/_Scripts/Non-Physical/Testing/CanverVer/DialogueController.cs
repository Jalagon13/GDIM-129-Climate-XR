using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public StoryMode storyMode;
    public Dialogue exampleDialogue;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!storyMode.dialogueBox.activeSelf)
            {
                storyMode.StartDialogue(exampleDialogue);
            }
            else
            {
                storyMode.ContinueDialogue();
            }
        }
    }
}
