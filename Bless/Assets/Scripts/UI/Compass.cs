using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    public GameObject MarkerPrefap;
    List<Marker> markers = new List<Marker>();

    public RawImage compassImage;
    public Transform Orientation;
    public GameObject MarkersParent;

    public float maxDistance = 50f;

    float compasUnit;

    private void Awake()
    {
        compasUnit = compassImage.rectTransform.rect.width / 360f;
    }

    // Update is called once per frame
    void Update()
    {
        compassImage.uvRect = new Rect(Orientation.localEulerAngles.y / 360f, 0f, 1f, 1f);

        foreach (Marker marker in markers)
        {
            marker.image.rectTransform.anchoredPosition = GetPosOnCompass(marker);

            float dst = Vector2.Distance(new Vector2(Orientation.transform.position.x, Orientation.transform.position.z), marker.position);
            float scale = 0f;

            if (dst < maxDistance)
            {
                scale = 1f - (dst / maxDistance);
            } if (dst > maxDistance)
            {
                scale = 0f;
            }

            marker.image.rectTransform.localScale = Vector3.one * scale;

        }
    }

    public void AddMarker(Marker marker)
    {
        GameObject newMarker = Instantiate(MarkerPrefap, MarkersParent.transform);
        marker.newMark = newMarker;
        marker.image = newMarker.GetComponent<Image>();
        marker.image.sprite = marker.icon;

        markers.Add(marker);
    }

    public void RemoveMarker(Marker marker)
    {
        Destroy(marker.newMark);
        markers.Remove(marker);
    }

    Vector2 GetPosOnCompass(Marker marker)
    {
        Vector2 playerPos = new Vector2(Orientation.transform.position.x, Orientation.transform.position.z);
        Vector2 Playerfwd = new Vector2(Orientation.transform.forward.x, Orientation.transform.forward.z);

        float angle = Vector2.SignedAngle(marker.position - playerPos, Playerfwd);

        return new Vector2(compasUnit * angle, 0f);
    }
}
