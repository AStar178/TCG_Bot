using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Camera;
    public Transform player;
    public Vector3 offset;

    void Update()
    {
        Camera.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, player.position.z + offset.z); // Camera follows the player with specified offset position
    }
}
