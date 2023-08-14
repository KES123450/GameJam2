using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComboCounter : MonoBehaviour
{
    public int comboCount;
    public float playerBoostFactor;
    public PlayerController playerController;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            comboCount++;
            playerController.playerBoost += playerBoostFactor * comboCount;
        }
    }

}
