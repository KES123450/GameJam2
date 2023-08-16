using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ForceGameController : MonoBehaviour
{
    public Vector3 guageStartPos;
    public Vector3 guageEndPos;
    public Transform gaugeFilling;

    public void IncreaseSpeedGauge(float jumpLimitTime)
    {
        gaugeFilling.DOLocalMove(guageEndPos,jumpLimitTime);
    }

    public void InitGauge()
    {
        gaugeFilling.DOKill();
        gaugeFilling.localPosition = guageStartPos;
        Debug.Log("gaugePosition"+gaugeFilling.position);
    }
   
}
