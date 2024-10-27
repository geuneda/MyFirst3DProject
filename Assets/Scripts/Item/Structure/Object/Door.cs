using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour, IStructure
{
    [SerializeField] private float openRotationY = -180f;
    [SerializeField] private float rotationSpeed = 2f;

    private bool isOpen = false;
    private Quaternion targetRotation;

    public void Activate()
    {
        if (isOpen)
        {
            targetRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            targetRotation = Quaternion.Euler(0, openRotationY, 0);
        }

        isOpen = !isOpen;
        StopAllCoroutines();
        StartCoroutine(RotateDoor());
    }

    private IEnumerator RotateDoor()
    {
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            yield return null;
        }

        transform.rotation = targetRotation;
    }
}
