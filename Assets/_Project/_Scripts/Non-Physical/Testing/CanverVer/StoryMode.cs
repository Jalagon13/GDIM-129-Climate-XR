using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using static System.Net.Mime.MediaTypeNames;
using System;

public class StoryMode : MonoBehaviour
{
    public GameObject dialogueBox;
    public UnityEngine.UI.Text dialogueText;
    public UnityEngine.UI.Image displayImage;
    private int lineIndex = 0;
    private Dialogue currentDialogue;

    void Start()
    {
        dialogueBox.SetActive(false); 
    }

    public void StartDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        lineIndex = 0;
        dialogueBox.SetActive(true);
        UpdateDialogue();
    }

    public void ContinueDialogue()
    {
        if (currentDialogue == null)
            return;

        if (lineIndex < currentDialogue.lines.Length - 1)
        {
            lineIndex++;
            UpdateDialogue();
        }
        else
        {
            EndDialogue();
        }
    }

    private void UpdateDialogue()
    {
        dialogueText.text = currentDialogue.lines[lineIndex];
        var imageInfo = currentDialogue.dialogueImages[lineIndex];
        displayImage.sprite = imageInfo.sprite;
        displayImage.rectTransform.anchoredPosition = imageInfo.position;
        ApplyAnimation(displayImage, imageInfo); 
    }

    private void ApplyAnimation(UnityEngine.UI.Image image, DialogueImage imageInfo)
    {
        switch (imageInfo.animation)
        {
            case ImageAnimation.FadeIn:
                StartCoroutine(FadeInImage(image, 1f)); // fadein time
                break;
            case ImageAnimation.FadeOut:
                StartCoroutine(FadeOutImage(image, 1f));
                break;
            case ImageAnimation.Move:
                // 假设移动到指定位置，时间为1秒
                StartCoroutine(MoveImage(image, imageInfo.position, 1f));
                break;
            case ImageAnimation.Shake:
                StartCoroutine(ShakeImage(image, 1f, 10f)); // shaking time, power
                break;
        }
    }

    IEnumerator FadeInImage(UnityEngine.UI.Image image, float duration)
    {
        float elapsedTime = 0;
        Color color = image.color;
        color.a = 0;
        image.color = color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / duration);
            image.color = color;
            yield return null;
        }
    }

    IEnumerator FadeOutImage(UnityEngine.UI.Image image, float duration)
    {
        float elapsedTime = 0;
        Color color = image.color;
        color.a = 1;
        image.color = color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1 - elapsedTime / duration);
            image.color = color;
            yield return null;
        }

        image.gameObject.SetActive(false); // Optionally hide the image after fade out
    }

    IEnumerator MoveImage(UnityEngine.UI.Image image, Vector2 targetPosition, float duration)
    {
        Vector2 startPosition = image.rectTransform.anchoredPosition;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            image.rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, elapsedTime / duration);
            yield return null;
        }
    }

    IEnumerator ShakeImage(UnityEngine.UI.Image image, float duration, float magnitude)
    {
        Vector2 originalPosition = image.rectTransform.anchoredPosition;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float x = originalPosition.x + UnityEngine.Random.Range(-1f, 1f) * magnitude;
            float y = originalPosition.y + UnityEngine.Random.Range(-1f, 1f) * magnitude;
            image.rectTransform.anchoredPosition = new Vector2(x, y);
            yield return null;
        }

        image.rectTransform.anchoredPosition = originalPosition;
    }


    public void EndDialogue()
    {
        dialogueBox.SetActive(false);
    }

}
