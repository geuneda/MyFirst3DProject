using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BadWater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI notiText;
    public int damage;
    public float damageRate;

    private List<IDamagable> things = new List<IDamagable>();

    private void Start()
    {
        InvokeRepeating("DealDamage", 0, damageRate);
    }

    void DealDamage()
    {
        for (int i = 0; i < things.Count; i++)
        {
            things[i].TakePhysicalDamage(damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IDamagable damagable))
        {
            things.Add(damagable);
            notiText.text = "������ ���������� ���������� ü���� �Ҹ�˴ϴ�.";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IDamagable damagable))
        {
            things.Remove(damagable);
            notiText.text = null;
        }
    }
}