using System;
using System.Collections;
using UnityEngine;

public class Invincibility : MonoBehaviour
{
    public float duration = 2f;
    private BlinkEffect blinkEffect;

    private void Awake()
    {
        blinkEffect = GetComponent<BlinkEffect>();
    }

    public void StartInvincibility(Action onInvincibilityEnd)
    {
        StartCoroutine(InvincibilityCoroutine(onInvincibilityEnd));
    }

    private IEnumerator InvincibilityCoroutine(Action onInvincibilityEnd)
    {
        if (blinkEffect != null)
        {
            blinkEffect.StartBlinking(duration);
        }

        yield return new WaitForSeconds(duration);

        onInvincibilityEnd?.Invoke();
    }
}
