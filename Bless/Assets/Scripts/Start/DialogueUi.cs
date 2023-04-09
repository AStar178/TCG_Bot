using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueUi : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text text_label;
    [SerializeField] private DialogueObject dialogue;

    private ResponseHandler responseHandler;
    private TypeWriter typeWriterEffect;
    [HideInInspector] public AudioClip talkSFX;
    [HideInInspector] public float pitch = 1f;
    [HideInInspector] public bool pitchChanger = false;

    [HideInInspector] public float textPerSecond = 50f;

    [HideInInspector] public bool wobbing = false;
    [HideInInspector] public float wobbing1 = 10f;
    [HideInInspector] public float wobbing2 = 10f;

    private void Start()
    {
        typeWriterEffect = GetComponent<TypeWriter>();
        responseHandler = GetComponent<ResponseHandler>();

        ShowDialogue(dialogue);
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    { 
        for (int i = 0; i < dialogueObject.jostar.Count; i++)
        {
            string dialogue = dialogueObject.jostar[i].dialogue;
            typeWriterEffect.audioClip = dialogueObject.jostar[i].talkSFX;
            typeWriterEffect.pitch = dialogueObject.jostar[i].pitch;
            typeWriterEffect.pitchChanger = dialogueObject.jostar[i].pitchChanger;
            typeWriterEffect.textPerSecond = dialogueObject.jostar[i].textPerSecond;
            yield return typeWriterEffect.Run(dialogue, text_label);


            if (i == dialogueObject.jostar.Count - 1 && dialogueObject.HasResponses) break;

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space));
        }

        if (dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else
        {
            CloseDialogueBox();
        }
    }

    private void CloseDialogueBox()
    {
        dialogueBox.SetActive(false);
        text_label.text = string.Empty;
    }

    #region
    Mesh mesh;

    Vector3[] vertices;

    // Update is called once per frame
    void Update()
    {
        if (wobbing == true)
        {
            text_label.ForceMeshUpdate();
            mesh = text_label.mesh;
            vertices = mesh.vertices;

            for (int i = 0; i < text_label.textInfo.characterCount; i++)
            {
                TMP_CharacterInfo c = text_label.textInfo.characterInfo[i];

                int index = c.vertexIndex;

                Vector3 offset = Wobble(Time.time + i);
                vertices[index] += offset;
                vertices[index + 1] += offset;
                vertices[index + 2] += offset;
                vertices[index + 3] += offset;
            }

            mesh.vertices = vertices;
            text_label.canvasRenderer.SetMesh(mesh);
        }
    }

    Vector2 Wobble(float time)
    {
        return new Vector2(Mathf.Sin(time * wobbing1), Mathf.Cos(time * wobbing2));
    }
    #endregion
}
