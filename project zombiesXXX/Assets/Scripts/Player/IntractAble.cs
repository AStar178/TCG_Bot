using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntractAble : MonoBehaviour
{
    public LayerMask IntractLayer;
    public Interactable Target;
    public GameObject Text;
    private GameObject text;
    [SerializeField] float Range = 4;


    // Update is called once per frame
    void Update()
    {
        if (text == null)
            text = Instantiate(Text);

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
                if (obj.gameObject.GetComponentInParent<Interactable>() != null)
                    if (obj.gameObject.GetComponentInParent<Interactable>().caninteracted == true)
                        Target = obj.gameObject.GetComponentInParent<Interactable>();
                    else if (obj.gameObject.GetComponent<Interactable>() != null)
                        if (obj.gameObject.GetComponent<Interactable>().caninteracted == true)
                            Target = obj.gameObject.GetComponent<Interactable>();
            }
        }

        // set the target to null if no target found
        if (objects.Length <= 0)
            Target = null;

        if (Target != null && Player.Current.PlayerInputSystem.Intract)
            Intract();

        if (Player.Current.PlayerInputSystem.Intract)
            Player.Current.PlayerInputSystem.Intract = false;

        if (Target != null)
        {
            if (Target.GetComponentInParent<Interactable>().caninteracted)
            {
                if (text.activeInHierarchy == false)
                    text.SetActive(true);
                text.transform.SetParent(Target.transform);
                text.transform.localPosition = Vector3.zero;
                text.GetComponentInChildren<TMPro.TMP_Text>().text = Target.GetComponentInParent<Interactable>().GetText();
            }
        }
        else
            text.SetActive(false);

    }

    public void Intract()
    {
        Target.transform.GetComponentInParent<Interactable>().OnInteracted();
        Target = null;
        text.SetActive(false);
    }

}
