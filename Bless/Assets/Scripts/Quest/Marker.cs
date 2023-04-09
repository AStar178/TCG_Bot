using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Marker : MonoBehaviour
{
    private bool IsActive;
    public bool dontStart;
    [HideInInspector]
    public GameObject newMark;
    public Sprite icon;
    [HideInInspector]
    public Image image;

    public void Start()
    {
        if (!dontStart)
            AddMark();
    }

    public void OnDestroy()
    {
        RemoveMark();
    }

    public void AddMark()
    {
        if (IsActive)
            return;
        Static.Compass.AddMarker(this);
        IsActive = true;
    }

    public void RemoveMark()
    {
        Static.Compass.RemoveMarker(this);
        IsActive = false;
    }

    public Vector2 position
    {
        get { return new Vector2(transform.position.x, transform.position.z); }
    }
}
