using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player;

    void Update()
    {
        Vector3 cameraPos = new Vector3(player.position.x, player.position.y, -10);
        transform.position = cameraPos;
    }
}
