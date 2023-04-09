using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWriter : MonoBehaviour
{
    [SerializeField] private AudioSource audioS;
    [HideInInspector] public AudioClip audioClip;
    [HideInInspector] public float pitch;
    [HideInInspector] public bool pitchChanger = false;
    [HideInInspector] public float textPerSecond;

    public Coroutine Run(string textToType, TMP_Text text_label)
    {
        return StartCoroutine(TypeText(textToType, text_label));
    }

    private IEnumerator TypeText(string textToType, TMP_Text text_label)
    {
        float t = 0;
        int charIndex = 0;

        audioS.clip = audioClip;
        audioS.pitch = pitch;
        audioS.loop = true;
        audioS.Play();


        while (charIndex < textToType.Length)
        {
            t += Time.deltaTime * textPerSecond;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            if (pitchChanger == true)
                audioS.pitch = pitch + Random.Range(-.15f, .15f);

            text_label.text = textToType.Substring(0, charIndex);

            yield return null;
        }

        audioS.loop = false;
        audioS.Stop();
        pitchChanger = false;
        text_label.text = textToType;
    }

    IEnumerator Pitcher()
    {
        yield return new WaitForSeconds(1);
        audioS.pitch = pitch + Random.Range(-.25f, .25f);
    }
}
