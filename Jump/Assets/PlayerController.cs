using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject player;
    public Rigidbody2D playerRigid;
    public float playerMoveSpeed;
    public float playerBoost;
    public Vector2 jumpDirection;
    public float jumpForce;
    public float jumpLimitTime;
    public int maxJumpStep;
    private float jumpTimer;

    public float minBackBounceX;
    public float maxBackBounceX;

    public float minBackBounceY;
    public float maxBackBounceY;

    public float maxBackBounceForce;
    public float minBackBounceForce;


    void Start()
    {
        player = gameObject;
        playerRigid = gameObject.GetComponent<Rigidbody2D>();
    }

    void PlayerMove()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            player.transform.Translate(playerMoveSpeed, 0, 0);
        }

        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            player.transform.Translate(-playerMoveSpeed, 0, 0);
        }
    }

    void PlayerJump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (jumpTimer < jumpLimitTime)
            {
                jumpTimer += Time.deltaTime;
                Debug.Log(jumpTimer);
            }
            
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            float interval = jumpLimitTime / (float)maxJumpStep;
            Debug.Log("interval"+interval);
            float jumpStep=0;
            for(int i=0; i < maxJumpStep; i++)
            {
                if (jumpStep >= jumpTimer)
                {
                    break;
                }
                jumpStep += interval;
            }
            Debug.Log(jumpStep);

            Vector2 jumpForceVector = jumpDirection.normalized * jumpStep *jumpForce;
            playerRigid.AddForce(jumpForceVector, ForceMode2D.Impulse);
            jumpTimer = 0;

        }
    }

    void PlayerBounceBack()
    {
        float randomDirectionX = Random.Range(minBackBounceX, maxBackBounceX);
        float randomDirectionY = Random.Range(minBackBounceY, maxBackBounceY);
        Vector2 randomDirection = new Vector2(randomDirectionX, randomDirectionY);
        float randomJumpForce = Random.Range(minBackBounceForce, maxBackBounceForce);
        Debug.Log("nowForce : " + randomJumpForce);
        Vector2 jumpForceVector = randomDirection.normalized *randomJumpForce;

        playerRigid.AddForce(jumpForceVector, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            PlayerBounceBack();
        }
    }

    void Update()
    {
        PlayerJump();
    }

    void FixedUpdate()
    {
        PlayerMove();
    }
}
