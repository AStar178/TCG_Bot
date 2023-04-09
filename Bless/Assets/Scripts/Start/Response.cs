using UnityEngine;

[System.Serializable]
public class Response
{
    [SerializeField] private string responseText;
    [SerializeField] private DialogueObject dialogueObject;

    public string ResponseText => responseText;

    public DialogueObject DialogueObject => dialogueObject;

    public AudioClip TalkSFXRe => dialogueObject.jostar[0].talkSFX;

    public float Pitch => dialogueObject.jostar[0].pitch;

    public float TextPerSecond => dialogueObject.jostar[0].textPerSecond;
}
