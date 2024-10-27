using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface IDamagable
{
    void TakePhysicalDamage(int damageAmount);
}

public class PlayerCondition : MonoBehaviour, IDamagable
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }
    Condition hunger { get { return uiCondition.hunger; } }
    Condition stamina { get { return uiCondition.stamina; } }

    public float noHungerHealthDecay;
    public event Action onTakeDamage;

    private Coroutine activeSpeedBoostCoroutine;

    private void Update()
    {
        hunger.Subtract(hunger.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if (hunger.curValue <= 0.0f)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }

        if (health.curValue <= 0.0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Eat(float amount)
    {
        hunger.Add(amount);
    }

    public void Die()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void TakePhysicalDamage(int damageAmount)
    {
        health.Subtract(damageAmount);
        onTakeDamage?.Invoke();
    }

    public void SpeedItem(float mutiplier, float duration)
    {
        if (activeSpeedBoostCoroutine != null)
        {
            StopCoroutine(activeSpeedBoostCoroutine);
        }

        activeSpeedBoostCoroutine = StartCoroutine(SpeedBoostCoroutine(mutiplier, duration));
    }
    //다음부턴 코루틴의 시작 종료도 함수로 사용하자...
    private IEnumerator SpeedBoostCoroutine(float mutiplier, float duration)
    {
        // 시작
        float baseSpeed = CharacterManager.Instance.Player.controller.moveSpeed;
        CharacterManager.Instance.Player.controller.moveSpeed *= mutiplier;

        yield return new WaitForSeconds(duration);

        // 종료
        CharacterManager.Instance.Player.controller.moveSpeed = baseSpeed;
        activeSpeedBoostCoroutine = null;
    }
}