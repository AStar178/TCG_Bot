using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttom : MonoBehaviour
{
    public Color32 color;
    public Color32 onMouse;

    private void OnMouseEnter()
    {
        gameObject.GetComponent<SpriteRenderer>().color = onMouse;
        if (Input.GetKeyDown(KeyCode.Mouse0))
            Debug.Log("zzzzzzzzzzzzzzzzzzzzzz");
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<SpriteRenderer>().color = color;
    }
    
    
}
