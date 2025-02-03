using UnityEngine;

public class LeftTurnPoint : MonoBehaviour
{
    public float playerRotate = -90f;
    private bool hasCollided = false;

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !hasCollided)
        {
            var player = other.GetComponent<Player>();
            if (player.canLeftSwipe || player.invincibility.gameObject.activeSelf)
            {
                player.transform.Rotate(0, playerRotate, 0);
                player.canLeftSwipe = false;
                Debug.Log("�浹 (Ʈ����)");
                hasCollided = true; // �浹�� �� ���� ó��
            }
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            var player = collision.collider.GetComponent<Player>();
            if (player.canLeftSwipe || player.invincibility.gameObject.activeSelf)
            {
                player.transform.Rotate(0, playerRotate, 0);
                player.canLeftSwipe = false;
                Debug.Log("�浹 (������)");
            }
        }
    }
}