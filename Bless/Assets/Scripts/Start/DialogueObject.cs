using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    public List<Joseph> jostar;
    [SerializeField] private Response[] responses;

    public bool HasResponses => Responses != null && Responses.Length > 0;

    public Response[] Responses => responses;
}

[System.Serializable]
public struct Joseph
{
    [TextArea] public string dialogue;

    public AudioClip talkSFX;
    public float pitch;
    public bool pitchChanger;
    public float textPerSecond;
    public bool wobbing;
    public float wobbing1;
    public float wobbing2;
}