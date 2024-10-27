using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    public Vector3 resetPosition;
    public TextMeshProUGUI notiText;

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = resetPosition;
            StartCoroutine(ShowMessageCoroutine());
        }
    }

    private IEnumerator ShowMessageCoroutine()
    {
        notiText.text = "�⺻ ��ġ�� ���ư��ϴ�.";
        yield return new WaitForSeconds(3f);
        notiText.text = null;
    }
}
