using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;

    public float speed = 6f;  // �ȴ� �ӵ�
    public float swipeMovement = 2f;  // �¿� �������� �ӵ�
    public float playerRotate = 90;
    public float tiltSpeed = 3f;

    public float jumpForce = 5f;

    public float swipeTimeAccum = 0f;
    public float swipeTimeDelay = 2f;

    public bool canLeftSwipe = false;
    public bool canRightSwipe = false;

    private bool IsJumping = false;

    //���ھ�
    private Vector3 lastPostion;
    public float totalDistance;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // ���� �ÿ��� Ư�� �ӵ��� �������� �̵��� ���� �ֽ��ϴ�.
        lastPostion = transform.position;
        totalDistance = 0f;
    }

    private void Update()
    {
        CheckHeight();
        MoveWithTilt();
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

        //���ھ�
        Vector3 delta = transform.position - lastPostion;

        totalDistance += Mathf.Abs(delta.x) + Mathf.Abs(delta.z);

        lastPostion = transform.position;

        Debug.Log(totalDistance);
    }

    private void FixedUpdate()
    {
        Vector3 forwardMovement = transform.forward * speed * Time.deltaTime;

        rb.MovePosition(rb.position + forwardMovement);
    }

    private void CheckHeight()
    {
        // Y�� ��ġ�� -10 �̸��̸� ���� ������ ����
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
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);


        IsJumping = true;
    }
    private void MoveWithTilt()
    {
        float tiltInput = Input.acceleration.x;

        Vector3 tiltMovement = new Vector3(tiltInput * tiltSpeed, 0, 0);

        Vector3 moveDirection = Quaternion.Euler(0, transform.eulerAngles.y, 0) * tiltMovement;

        rb.MovePosition(rb.position + moveDirection * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        // �ٴڿ� ������ ��� IsJumping�� false�� ����
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsJumping = false;
        }
    }


}