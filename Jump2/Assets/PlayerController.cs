using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{

    public GameObject player;
    public Rigidbody2D playerRigid;
    public int nowHP;
    public int bulletDashNum;

    public PlayerInfoUIManager HPManager;
    public PlayerInfoUIManager DashManager;
    public CameraController cameraController;

    public float playerFallGravityScale;
    public float playerMaxVelocity;
    public float playerMoveSpeed;
    public float playerMaxSpeed;
    public Vector2 jumpDirection;
    public bool isPlayerJump;
    public bool isPlayerMove;
    private bool isGrounded;
    public float jumpForce;
    public float bulletJumpForce;
    public float SuperBulletJumpForce;
    public float jumpLimitTime;
    public int maxJumpStep;
    public float jumpTimer;

    public float minBackBounceX;
    public float maxBackBounceX;

    public float minBackBounceY;
    public float maxBackBounceY;

    public float maxBackBounceForce;
    public float minBackBounceForce;

    public ForceGameController forceGameController;

    public float minJumpValue;

    public Transform leg1;
    public Transform leg2;
    public float stepDuration = 0.1f;
    private bool isWalking = false;

    [SerializeField]
    private float BounceAnmiationTime = 0.2f;
    public GameObject BounceEye;
    private bool BounceAnimOn = false;
    public Transform bulletDirectionArrow;
    public float dragDuration;
    public float targetDrag = 5f;
    private float initialDrag = 0;
    public AnimationCurve DragCurve;
    public BulletEffectController superEffect;
    public BulletEffectController effect;
    public Transform arrowFilling;
    public float startFillingPosY;
    public ArrowRotationCheck arrowRotationCheck;

    void Start()
    {
        player = gameObject;
        playerRigid = gameObject.GetComponent<Rigidbody2D>();
    }

    public void LowerHP()
    {
        nowHP -= 1;
        HPManager.DeletePrefab();
    }

    public void UpperHP()
    {
        nowHP += 1;
        HPManager.AddPrefab();
    }

    public void UpperBulletDashNum()
    {
        if (bulletDashNum >= 4)
        {
            return;
        }
        bulletDashNum += 1;
        DashManager.AddPrefab();
    }

    public void LowerBulletDashNum()
    {
        bulletDashNum -= 1;
        DashManager.DeletePrefab();
    }

    public void InitBulletDashNum()
    {
        bulletDashNum  = 1;
        DashManager.InitInfo();
    }

    void PlayerMove()
    {
        if (isPlayerMove)
        {
            if (Input.GetKey(KeyCode.D))
            {
                if (playerRigid.velocity.magnitude < playerMaxSpeed)
                {
                    playerRigid.AddForce(Vector2.right * playerMoveSpeed);
                }
                
            }

            else if (Input.GetKey(KeyCode.A))
            {
                if (playerRigid.velocity.magnitude < playerMaxSpeed)
                {
                    playerRigid.AddForce(-Vector2.right * playerMoveSpeed);
                }
            }

            if(isWalking == false&&Mathf.Abs(playerRigid.velocity.x)>0.2f)
            {
                MoveLeg(leg1, leg2, 10f);
            }

        }
        
    }

    private void MoveLeg(Transform leg1, Transform leg2, float MoveAngleAbs)
    {
        isWalking = true;
        leg1.DOLocalRotate(new Vector3(0f, 0f, MoveAngleAbs), stepDuration).OnComplete(() => leg1.DOLocalRotate(new Vector3(0f, 0f, -MoveAngleAbs), stepDuration).OnComplete(() => isWalking = false));
        leg2.DOLocalRotate(new Vector3(0f, 0f, -MoveAngleAbs), stepDuration).OnComplete(() => leg2.DOLocalRotate(new Vector3(0f, 0f, MoveAngleAbs), stepDuration).OnComplete(() => isWalking = false));
    }



    void PlayerJump()
    {
        if (isPlayerJump)
        {
            if (Input.GetKeyDown(KeyCode.W)) { 
                isGrounded = true;

                Vector2 jumpForceVector = jumpDirection.normalized * jumpForce;
                playerRigid.AddForce(jumpForceVector, ForceMode2D.Impulse);
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                isGrounded = false;
                
            }
        }
    }

    

    void PlayerBounceBack()
    {
        float randomDirectionX = playerRigid.velocity.x;
        float randomDirectionY = playerRigid.velocity.y;
        Vector2 randomDirection = new Vector2(randomDirectionX, randomDirectionY);
        /*if (randomDirection.x == 0 || randomDirection.y == 0)
        {
            //randomDirection = new Vector3(0.2f, 1f);
        }*/
        //float randomJumpForce = Random.Range(minBackBounceForce, maxBackBounceForce);
        Vector2 jumpForceVector = randomDirection.normalized * maxBackBounceForce;

        playerRigid.AddForce(jumpForceVector, ForceMode2D.Impulse);
    }

    IEnumerator BounceAnimation()
    {
        BounceAnimOn = true;
        BounceEye.SetActive(true);
        yield return new WaitForSeconds(BounceAnmiationTime);
        BounceEye.SetActive(false);
        BounceAnimOn = false;

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            PlayerBounceBack();
            if (BounceAnimOn == false)
            {
                StartCoroutine(BounceAnimation());
            }
            InitBulletDashNum();
            LowerHP();
        }
    }


    void LimitPlayerVelocity()
    {
        if (playerRigid.velocity.magnitude > playerMaxVelocity)
        {
            playerRigid.velocity = playerRigid.velocity.normalized * playerMaxVelocity;
        }
    }


    private float GetAngle(Vector3 vStart, Vector3 vEnd)
    {
        Vector3 v = vEnd - vStart;
        //  Debug.Log(Mathf.Atan2(v.y, v.x));
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }

    private float preciousAngle;
    private float changeAngleSum;
    public bool isSuperBullet;

    private Vector3 GetBulletAngle()
    {
        float bulletAngle;
        Vector3 mousePos= Input.mousePosition;
        Vector3 mouseWorldPos = (Camera.main.ScreenToWorldPoint(mousePos));
        Vector3 targetVector = (mouseWorldPos - transform.position);
        bulletAngle = GetAngle(transform.position, mouseWorldPos)-90;
        
        Vector3 bulletDirection= new Vector3(0,0,bulletAngle);
/*
        bulletAngle += 180;
        if(Mathf.Abs(preciousAngle - bulletAngle) <=355)
        {
            changeAngleSum += Mathf.Abs(preciousAngle - bulletAngle);
        }
       

        if (Mathf.Abs(changeAngleSum)>=275)
        {
            isSuperBullet = true;
            bulletDirectionArrow.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
            bulletDirectionArrow.GetChild(1).GetComponent<SpriteRenderer>().color = Color.red;

        }
        preciousAngle = bulletAngle;*/


       

        return bulletDirection;
        
    }

    private void StartBulletTime()
    {
        
        if (bulletDashNum > 0&&isPlayerJump)
        {
            if (Input.GetMouseButtonDown(0))
            {
                cameraController.StartBulletDash();
            }

            if (Input.GetMouseButton(0))
            {
                //Time.timeScale = 0.1f;
                Vector3 bulletDirection = GetBulletAngle();
                bulletDirectionArrow.gameObject.SetActive(true);
                bulletDirectionArrow.eulerAngles = bulletDirection;

                if (arrowRotationCheck.rotateHitNum >= 4)
                {
                    isSuperBullet = true;
                    bulletDirectionArrow.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
                    bulletDirectionArrow.GetChild(1).GetComponent<SpriteRenderer>().color = Color.red;
                }


            }

            if (Input.GetMouseButtonUp(0))
            {
                bulletDirectionArrow.GetChild(0).GetComponent<SpriteRenderer>().color = Color.gray;
                bulletDirectionArrow.GetChild(1).GetComponent<SpriteRenderer>().color = Color.gray;
                arrowRotationCheck.rotateHitNum = 0;
                if (isSuperBullet)
                {
                    isSuperBullet = false;
                    changeAngleSum = 0;
                    superEffect.EffectPlay();
                    cameraController.EndBulletDash();
                    LowerBulletDashNum();
                    bulletDirectionArrow.gameObject.SetActive(false);
                    Vector3 mousePos = Input.mousePosition;
                    Vector3 mouseWorldPos = (Camera.main.ScreenToWorldPoint(mousePos));
                    playerRigid.velocity = new Vector2(0, 0);
                    playerRigid.drag = targetDrag;
                    Vector2 bulletDirection = ((Vector2)(mouseWorldPos - transform.position)).normalized;
                    playerRigid.AddForce(bulletDirection * SuperBulletJumpForce, ForceMode2D.Impulse);

                    StartCoroutine(DecreaseDrag());

                    Time.timeScale = 1.5f;

                }
                else
                {
                    isSuperBullet = false;
                    changeAngleSum = 0;
                    effect.EffectPlay();
                    cameraController.EndBulletDash();
                    LowerBulletDashNum();
                    bulletDirectionArrow.gameObject.SetActive(false);
                    Vector3 mousePos = Input.mousePosition;
                    Vector3 mouseWorldPos = (Camera.main.ScreenToWorldPoint(mousePos));
                    playerRigid.velocity = new Vector2(0, 0);
                    playerRigid.drag = targetDrag;
                    Vector2 bulletDirection = ((Vector2)(mouseWorldPos - transform.position)).normalized;
                    playerRigid.AddForce(bulletDirection * bulletJumpForce, ForceMode2D.Impulse);

                    StartCoroutine(DecreaseDrag());

                    Time.timeScale = 1.5f;
                }



            }
        }
        
    }

    private IEnumerator DecreaseDrag()
    {
        float timer=0;
        while (timer < dragDuration)
        {
            playerRigid.drag = DragCurve.Evaluate(timer);
            timer += Time.deltaTime;
            yield return null;
        }

        playerRigid.drag = initialDrag;

    }
    void Update()
    {
        LimitPlayerVelocity();
        StartBulletTime();
        PlayerJump();
    }



    void FixedUpdate()
    {
        PlayerMove();
    }
}
