using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;

    public float speed = 6f;  // 걷는 속도
    public float swipeMovement = 2f;  // 좌우 스와이프 속도
    public float playerRotate = 90;
    public float tiltSpeed = 3f;

    public float jumpForce = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // 시작 시에는 특정 속도와 방향으로 이동할 수도 있습니다.
    }

    private void Update()
    {
        CheckHeight();
        MoveWithTilt();
        // 스와이프 방향에 따라 좌/우 이동
        if (MultiTouch.Instance.SwipeDirection.sqrMagnitude != 0f)
        {
            // X축과 Y축 스와이프 방향을 비교
            if (Mathf.Abs(MultiTouch.Instance.SwipeDirection.x) > Mathf.Abs(MultiTouch.Instance.SwipeDirection.y))
            {
                // 좌우 스와이프가 더 클 경우 로테이션
                CheckSwipeRotete();
            }
            else if (MultiTouch.Instance.SwipeDirection.y > 0)
            {
                // 위로 스와이프하면 점프
                CheckSwipeJump();
            }
        }

    }

    private void FixedUpdate()
    {
        // 앞으로 걷는 속도
        Vector3 forwardMovement = transform.forward * speed * Time.deltaTime;

        // 물리적 이동: AddForce로 부드럽게 이동
        rb.MovePosition(rb.position + forwardMovement); // MovePosition을 사용하여 부드럽게 이동
    }

    private void CheckHeight()
    {
        // Y축 위치가 -10 미만이면 죽은 것으로 간주
        if (transform.position.y < -10)
        {
            //GameManager.Instance.Die();
        }
    }
    private void CheckSwipeRotete()
    {
        var dir = (MultiTouch.Instance.SwipeDirection.x < 0) ? Vector3.left : Vector3.right;
        if (dir == Vector3.left)
        {
            transform.Rotate(0, -playerRotate, 0);
        }
        else
        {
            transform.Rotate(0, playerRotate, 0);
        }
    }
    private void CheckSwipeJump()
    {

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

    }
    private void MoveWithTilt()
    {
        // 기울기 값을 받아서 좌우로 이동
        float tiltInput = Input.acceleration.x;

        // 좌우로 이동하는 방향 벡터 (기울기 값에 따라 이동)
        Vector3 tiltMovement = new Vector3(tiltInput * tiltSpeed, 0, 0);

        // Rigidbody로 이동 적용
        rb.MovePosition(rb.position + tiltMovement * Time.deltaTime);
    }
}