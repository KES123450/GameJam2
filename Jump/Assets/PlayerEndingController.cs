using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class PlayerEndingController : MonoBehaviour
{
    private Sequence endSequence;
    public TextMeshProUGUI endingText;
    public TextMeshProUGUI creditText;
    public Vector3 endingPos;
    public GameManager GameManager;

    void Start()
    {
        endSequence = DOTween.Sequence().Pause()
            .Append(transform.DOMove(endingPos, 10f))
            .Append(endingText.DOFade(1f, 3f))
            .Append(endingText.DOFade(1f, 3f))
            .Append(endingText.DOFade(0f, 3f))
            .Append(creditText.DOFade(1f, 3f))
            .Append(creditText.DOFade(0f, 3f))
            .OnComplete(() => GameManager.ActiveEndUI());

    }

    public void EndingPlay()
    {
        endSequence.Play();
    }
}
