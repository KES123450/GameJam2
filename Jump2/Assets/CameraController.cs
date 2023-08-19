using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{

    public Transform player;

    public void StartBulletDash()
    {
        DOTween.To(() => 7f, x => Camera.main.orthographicSize = x, 7.5f, 0.05f);
    }

    public void EndBulletDash()
    {
        DOTween.To(() => Camera.main.orthographicSize, x => Camera.main.orthographicSize = x, 7f, 0.1f)
            .SetEase(Ease.InElastic);
    }
    void Update()
    {
        Vector3 cameraPos = new Vector3(player.position.x, player.position.y+3f, -10);
        transform.position = cameraPos;
    }

    
}
