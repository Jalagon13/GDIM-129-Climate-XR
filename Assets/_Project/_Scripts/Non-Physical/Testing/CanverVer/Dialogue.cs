using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string[] lines;
    public DialogueImage[] dialogueImages;
}

[System.Serializable]
public class DialogueImage
{
    public Sprite sprite;
    public Vector2 position;
    public ImageAnimation animation;
}

public enum ImageAnimation
{
    None, Move, Shake, FadeIn, FadeOut
}
