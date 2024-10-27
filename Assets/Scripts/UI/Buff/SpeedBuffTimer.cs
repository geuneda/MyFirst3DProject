using System.Collections;
using TMPro;
using UnityEngine;

public class SpeedBuffTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    public GameObject timerObject;

    private Coroutine activeTimerCoroutine;

    private void Start()
    {
        timerObject.SetActive(false);
    }

    public void StartTimer(float duration)
    {
        if (activeTimerCoroutine != null)
        {
            StopCoroutine(activeTimerCoroutine);
        }

        timerObject.SetActive(true);

        activeTimerCoroutine = StartCoroutine(TimerCoroutine(duration));
    }

    private IEnumerator TimerCoroutine(float duration)
    {
        float remainingTime = duration;

        while (remainingTime > 0)
        {
            int min = Mathf.FloorToInt(remainingTime / 60);
            int sec = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = $"{min:00}:{sec:00}";

            yield return new WaitForSeconds(1f);

            remainingTime -= 1f;
        }

        timerText.text = null;
        timerObject.SetActive(false);

        activeTimerCoroutine = null;
    }
}
