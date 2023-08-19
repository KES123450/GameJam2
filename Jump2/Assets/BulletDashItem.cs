using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BulletDashItem : MonoBehaviour
{
    public PlayerController playerController;
    private Sequence itemSequence;

    void Start()
    {
        Vector3 minScale = new Vector3(0.1f, 0.1f, 0.1f);
        Vector3 currentScale = new Vector3(1.5f, 1.5f, 1.5f);
        itemSequence=DOTween.Sequence().Pause().SetAutoKill(false);
        itemSequence.Append(transform.DOScale(minScale, 0.2f))
            .AppendInterval(3)
            .Append(transform.DOScale(currentScale, 1f));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController.UpperBulletDashNum();
            itemSequence.Rewind();
            itemSequence.Play();
        }
    }

    void DisablePlay()
    {
        Vector3 minScale = new Vector3(0.1f, 0.1f, 0.1f);
        transform.DOScale(minScale, 0.2f);
    }
}
