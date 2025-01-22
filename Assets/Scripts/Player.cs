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

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // ���� �ÿ��� Ư�� �ӵ��� �������� �̵��� ���� �ֽ��ϴ�.
    }

    private void Update()
    {
        CheckHeight();
        MoveWithTilt();
        // �������� ���⿡ ���� ��/�� �̵�
        if (MultiTouch.Instance.SwipeDirection.sqrMagnitude != 0f)
        {
            // X��� Y�� �������� ������ ��
            if (Mathf.Abs(MultiTouch.Instance.SwipeDirection.x) > Mathf.Abs(MultiTouch.Instance.SwipeDirection.y))
            {
                // �¿� ���������� �� Ŭ ��� �����̼�
                CheckSwipeRotete();
            }
            else if (MultiTouch.Instance.SwipeDirection.y > 0)
            {
                // ���� ���������ϸ� ����
                CheckSwipeJump();
            }
        }

    }

    private void FixedUpdate()
    {
        // ������ �ȴ� �ӵ�
        Vector3 forwardMovement = transform.forward * speed * Time.deltaTime;

        // ������ �̵�: AddForce�� �ε巴�� �̵�
        rb.MovePosition(rb.position + forwardMovement); // MovePosition�� ����Ͽ� �ε巴�� �̵�
    }

    private void CheckHeight()
    {
        // Y�� ��ġ�� -10 �̸��̸� ���� ������ ����
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
        // ���� ���� �޾Ƽ� �¿�� �̵�
        float tiltInput = Input.acceleration.x;

        // �¿�� �̵��ϴ� ���� ���� (���� ���� ���� �̵�)
        Vector3 tiltMovement = new Vector3(tiltInput * tiltSpeed, 0, 0);

        // Rigidbody�� �̵� ����
        rb.MovePosition(rb.position + tiltMovement * Time.deltaTime);
    }
}