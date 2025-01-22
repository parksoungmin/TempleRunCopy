using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Renderer playerRenderer;
    private Material playerMaterial;
    public float flashDuration = 0.1f;    // �����̴� �ð� (��)
    public float flashInterval = 0.2f;    // �����̴� ���� (��)

    private void Start()
    {
        playerRenderer = GetComponent<Renderer>();
        playerMaterial = playerRenderer.material;
        if (playerRenderer == null)
        {
            Debug.LogError("Player does not have a Renderer component!");
        }
    }
    private void Update()
    {
        // C Ű�� ������ �� ������ ȿ�� ����
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(FlashAlpha());
        }
    }

    private IEnumerator FlashAlpha()
    {
        Color originalColor = playerMaterial.color;

        // ���İ��� 0���� ���� �� �ٽ� ����
        for (float t = 0; t < flashDuration; t += flashInterval)
        {
            // ���İ��� 0���� ���� (����)
            playerMaterial.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
            yield return new WaitForSeconds(flashInterval);

            // ���� ���İ����� ����
            playerMaterial.color = originalColor;
            yield return new WaitForSeconds(flashInterval);
        }

        // ������ �� ���� ����
        playerMaterial.color = originalColor;
    }
}

