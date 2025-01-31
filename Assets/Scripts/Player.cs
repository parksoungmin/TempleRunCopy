using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private Rigidbody rb;

    public float speed = 6f;  // 걷는 속도
    public float swipeMovement = 2f;  // 좌우 스와이프 속도
    public float playerRotate = 90;
    public float tiltSpeed = 3f;


    public float jumpForce = 5f;

    public float swipeTimeAccum = 0f;
    public float swipeTimeDelay = 2f;

    public bool canLeftSwipe = false;
    public bool canRightSwipe = false;

    private bool IsJumping = false;

    //스코어
    private Vector3 lastPostion;
    public float totalDistance;

    public Magnet magnet;
    public Protect protect;
    public Invincibility invincibility;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        lastPostion = transform.position;
        totalDistance = 0f;
    }

    private void Update()
    {
        //CheckHeight();
        //MoveWithTilt();
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
                canLeftSwipe = false;
            }

        }

        //스코어
        Vector3 delta = transform.position - lastPostion;

        totalDistance += Mathf.Abs(delta.x) + Mathf.Abs(delta.z);

        lastPostion = transform.position;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        Vector3 forwardMovement = transform.forward * speed;

        var move = forwardMovement + GetMoveDirection();
        rb.MovePosition(rb.position + move * Time.deltaTime);
    }

    private void CheckHeight()
    {
        // Y축 위치가 -10 미만이면 죽은 것으로 간주
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
#if UNITY_EDITOR
        float tiltInput = Input.GetAxis("Horizontal");
#elif UNITY_ANDROID || UNITY_IOS
        float tiltInput = Input.acceleration.x;
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
}