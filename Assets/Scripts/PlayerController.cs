using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Renderer playerRenderer;
    private Material playerMaterial;
    public float flashDuration = 0.1f;    // 깜빡이는 시간 (초)
    public float flashInterval = 0.2f;    // 깜빡이는 간격 (초)

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
        // C 키가 눌렸을 때 깜빡임 효과 시작
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(FlashAlpha());
        }
    }

    private IEnumerator FlashAlpha()
    {
        Color originalColor = playerMaterial.color;

        // 알파값을 0으로 만든 후 다시 복원
        for (float t = 0; t < flashDuration; t += flashInterval)
        {
            // 알파값을 0으로 설정 (투명)
            playerMaterial.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
            yield return new WaitForSeconds(flashInterval);

            // 원래 알파값으로 복원
            playerMaterial.color = originalColor;
            yield return new WaitForSeconds(flashInterval);
        }

        // 깜빡임 후 색상 복원
        playerMaterial.color = originalColor;
    }
}

