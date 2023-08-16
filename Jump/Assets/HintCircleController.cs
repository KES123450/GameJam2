using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HintCircleController : MonoBehaviour
{
    public GameObject exclamationPoint;
    public GameObject exclamationPointMask;

    public float initialMove = 3f;
    public float jumpingDistance = 5f;
    public float finalMove = 5f;
    public float shrinkValue = 0.5f;
    private Rigidbody2D rb;
    public float DirectionX;
    public float DirectionY;

    public float randomJumpForce;
    private bool isJumping = false;

    public float exclamationPointTime = 0.5f;

    public float gaugeUpPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void StartMovement()
    {
        exclamationPoint.SetActive(true);

        StartInitialMove();
    }

    private void StartInitialMove()
    {
        exclamationPoint.SetActive(true);

        exclamationPointMask.transform.DOLocalMoveY(gaugeUpPos, exclamationPointTime).OnComplete(() =>
        {
            exclamationPoint.SetActive(false);
        });

        transform.DOMoveX(transform.position.x + initialMove, 1f)
            .SetEase(Ease.Linear)
            .OnComplete(() => StartBounce());
    }

    private void StartBounce()
    {
       
        Vector2 randomDirection = new Vector2(DirectionX, DirectionY);
        Vector2 jumpForceVector = randomDirection.normalized * randomJumpForce;

        rb.AddForce(jumpForceVector, ForceMode2D.Impulse);

        isJumping = true;
    }

    private void MoveStraight()
    {
        transform.DOMoveX(transform.position.x + finalMove, 1f)
            .SetEase(Ease.Linear).OnComplete(() => Destroy(gameObject));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isJumping == true)
        {
            if (collision.gameObject.CompareTag("Ground"))
                MoveStraight();
        }
    }

    

}
