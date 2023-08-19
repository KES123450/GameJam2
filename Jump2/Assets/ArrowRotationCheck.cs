using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRotationCheck : MonoBehaviour
{

    public int rotateHitNum;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("RotationCheck"))
        {
            Debug.Log("Collision");
            rotateHitNum++;
        }
    }
}
