using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 10f;  // 걷는 속도
    public float swipeMovement = 2f;  // 좌우 스와이프 속도
    public float playerRotate = 90;
    public float tiltSpeed = 3f;


    public float jumpForce = 5f;

    public float swipeTimeAccum = 0f;
    public float swipeTimeDelay = 2f;

    public bool canLeftSwipe = false;
    public bool canRightSwipe = false;

    private bool IsJumping = false;
    private bool UnityJump = false;
    public bool isTurn = false;
    public bool playerDead = false;
    //스코어
    private Vector3 lastPostion;
    public float totalDistance;
    public float speedUpDistance = 500f;
    private float speedDistance = 0f;

    public Magnet magnet;
    public Protect protect;
    public Invincibility invincibility;
    public CoinDouble coinDouble;

    private int speedUPCount = 0;
    public int speedUpMaxCount = 10;
    public float plus = 1.5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        playerDead = false;
        lastPostion = transform.position;
        totalDistance = 0f;
    }

    private void Update()
    {
        //CheckHeight();
        //MoveWithTilt(); 
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Q))
        {
            swipeTimeAccum = 0;
            canLeftSwipe = true;
            canRightSwipe = false;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            swipeTimeAccum = 0;
            canRightSwipe = true;
            canLeftSwipe = false;
        }
#elif UNITY_STANDALONE_WIN
        if (Input.GetKeyDown(KeyCode.Q))
        {
            swipeTimeAccum = 0;
            canLeftSwipe = true;
            canRightSwipe = false;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            swipeTimeAccum = 0;
            canRightSwipe = true;
            canLeftSwipe = false;
        }
#elif UNITY_ANDROID || UNITY_IOS
        if (MultiTouch.Instance.SwipeDirection.sqrMagnitude != 0f)
        {
            if (Mathf.Abs(MultiTouch.Instance.SwipeDirection.x) > Mathf.Abs(MultiTouch.Instance.SwipeDirection.y))
            {
                CheckSwipeRotate();
            }
            else if (MultiTouch.Instance.SwipeDirection.y > 0)
            {
                CheckSwipeJump();
            }
        }
#endif
        if (canLeftSwipe)
        {
            swipeTimeAccum += Time.deltaTime;
            if (swipeTimeAccum >= swipeTimeDelay)
            {
                canLeftSwipe = false;
            }
        }
        if (canRightSwipe)
        {
            swipeTimeAccum += Time.deltaTime;
            if (swipeTimeAccum >= swipeTimeDelay)
            {
                canRightSwipe = false;
            }
        }

        //스코어
        Vector3 delta = transform.position - lastPostion;
        delta.y = 0f;
        var deltaMagnitude = delta.magnitude;
        totalDistance += deltaMagnitude;
        speedDistance += deltaMagnitude;
        SpeedUP();
        lastPostion = transform.position;
        if (Input.GetKeyDown(KeyCode.UpArrow) && !IsJumping)
        {
            UnityJump = true;
        }
    }

    private void FixedUpdate()
    {
        if (UnityJump)
        {
            UnityJump = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            IsJumping = true;
        }
        Vector3 forwardMovement = transform.forward * speed;

        var move = forwardMovement + GetMoveDirection();

        rb.MovePosition(rb.position + move * Time.deltaTime);
    }

    private void CheckHeight()
    {
        if (transform.position.y < -10)
        {
            //GameManager.Instance.Die();
        }
    }

    private void CheckSwipeRotate()
    {
        canLeftSwipe = (MultiTouch.Instance.SwipeDirection.x < 0);
        canRightSwipe = !canLeftSwipe;
        swipeTimeAccum = 0f;
    }

    private void CheckSwipeJump()
    {
        if (!IsJumping)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        IsJumping = true;
    }
    private void MoveWithTilt()
    {
        float tiltInput = Input.acceleration.x;

        Vector3 tiltMovement = new Vector3(tiltInput * tiltSpeed, 0, 0);

        Vector3 moveDirection = Quaternion.Euler(0, transform.eulerAngles.y, 0) * tiltMovement;

        rb.MovePosition(rb.position + moveDirection * Time.deltaTime);
    }
    private Vector3 GetMoveDirection()
    {
        float tiltInput = 0f;  // 기본값을 할당

#if UNITY_STANDALONE_WIN  // 윈도우에서만 작동하도록 수정
        tiltInput = Input.GetAxis("Horizontal");
#elif UNITY_EDITOR
        tiltInput = Input.GetAxis("Horizontal");
#elif UNITY_ANDROID || UNITY_IOS
    tiltInput = Input.acceleration.x;
#endif

        Vector3 tiltMovement = new Vector3(tiltInput * tiltSpeed, 0, 0);
        Vector3 moveDirection = Quaternion.Euler(0, transform.eulerAngles.y, 0) * tiltMovement;

        return moveDirection;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsJumping = false;
        }
    }
    private void SpeedUP()
    {
        if (speedDistance > speedUpDistance && speedUPCount < speedUpMaxCount)
        {
            speedUPCount++;
            speedDistance = 0f;
            speed += plus;
        }
    }
    public void SetItemEffect()
    {
    }
}