using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntractAble : MonoBehaviour
{
    public LayerMask IntractLayer;
    public GameObject Target;
    public GameObject Text;
    private GameObject text;
    [SerializeField] float Range = 4;

    private void Start()
    {
        text = Instantiate(Text);
    }

    // Update is called once per frame
    void Update()
    {
        // change the color of the old target if possible
        if (Target != null && Target.GetComponent<Outliner>() != null)
        {
            Target.GetComponent<Outliner>().AlwaysRed = true;
            Target.GetComponent<Outliner>().enabled = false;
        }

        Collider[] objects = Physics.OverlapSphere(transform.position, Range, IntractLayer);
        //GameObject[] objects = GameObject.FindGameObjectsWithTag("Objetivo");
        float dot = -2;

        foreach (Collider obj in objects)
        {
            // store the Dot compared to the camera's forward position (or where the object is locally in the camera's space)
            // Very important that the point is normalized.

            Vector3 localPoint = Camera.main.transform.InverseTransformPoint(obj.transform.position).normalized;
            Vector3 forward = Vector3.forward;
            float test = Vector3.Dot(localPoint, forward);
            if (test > dot)
            {
                dot = test;
                Target = obj.gameObject;
            }
        }

        // set the target to null if no target found
        if (objects.Length <= 0)
            Target = null;

        if (Target != null && Player.Current.PlayerInputSystem.Intract)
            Intract();

        if (Target != null)
        {
            if (Target.GetComponentInParent<Interactable>())
            {
                if (text.activeInHierarchy == false)
                    text.SetActive(true);
                text.transform.SetParent(Target.transform);
                text.transform.localPosition = Vector3.zero;
                text.GetComponentInChildren<TMPro.TMP_Text>().text = Target.GetComponentInParent<Interactable>().GetText();
            }
        }
        else if (text.activeInHierarchy) 
                text.SetActive(false);

    }

    public void Intract()
    {
        Target.transform.GetComponentInParent<Interactable>().OnInteracted();
        text.SetActive(false);
        Player.Current.PlayerInputSystem.Intract = false;
    }

}
