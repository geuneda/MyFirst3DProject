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
    public Invincibility invincibility;
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }
    Condition hunger { get { return uiCondition.hunger; } }
    Condition stamina { get { return uiCondition.stamina; } }

    private bool isInvincible = false;
    public float noHungerHealthDecay;
    
    //public event Action onTakeDamage;

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
        SceneManager.LoadScene("MainScene");
    }

    public bool UseStamina(float amount)
    {
        if(stamina.curValue - amount < 0)
        {
            return false;
        }
        stamina.Subtract(amount);
        return true;
    }

    public void TakePhysicalDamage(int damageAmount)
    {
        if (isInvincible)
            return;

        health.Subtract(damageAmount);
        StartInvincibility();
    }

    private void StartInvincibility()
    {
        if (invincibility != null)
        {
            isInvincible = true;
            invincibility.StartInvincibility(() => isInvincible = false);
        }
    }

    public void SpeedItem(float mutiplier, float duration)
    {
        if (activeSpeedBoostCoroutine != null)
        {
            StopCoroutine(activeSpeedBoostCoroutine);
        }

        activeSpeedBoostCoroutine = StartCoroutine(SpeedBoostCoroutine(mutiplier, duration));
    }
    //�������� �ڷ�ƾ�� ���� ���ᵵ �Լ��� �������...
    private IEnumerator SpeedBoostCoroutine(float mutiplier, float duration)
    {
        // ����
        float baseSpeed = CharacterManager.Instance.Player.controller.moveSpeed;
        CharacterManager.Instance.Player.controller.moveSpeed *= mutiplier;

        yield return new WaitForSeconds(duration);

        // ����
        CharacterManager.Instance.Player.controller.moveSpeed = baseSpeed;
        activeSpeedBoostCoroutine = null;
    }
}