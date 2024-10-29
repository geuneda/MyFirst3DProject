using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkEffect : MonoBehaviour
{
    public float blinkInterval = 0.2f;
    public Renderer playerRenderer;

    private void Awake()
    {
    }

    public void StartBlinking(float duration)
    {
        StartCoroutine(BlinkCoroutine(duration));
    }

    private IEnumerator BlinkCoroutine(float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            playerRenderer.enabled = !playerRenderer.enabled;
            elapsedTime += blinkInterval;
            yield return new WaitForSeconds(blinkInterval);
        }
        playerRenderer.enabled = true;
    }
}
