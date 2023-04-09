using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCam : MonoBehaviour
{
    bool isVisable = false;

    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    // Start is called before the first frame update
    void Start()
    {
        curserLock();
    }

    // Update is called once per frame
    void Update()
    {
        if (Static.InMenu() && !isVisable)
        { curserUnLock(); return; }
        if (!Static.InMenu() && isVisable)
            curserLock();

        if (Static.InMenu())
        { return; }

        // Get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.smoothDeltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.smoothDeltaTime * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 67f);

        // rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    void curserLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isVisable = false;
    }

    void curserUnLock()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        isVisable = true;
    }
}
