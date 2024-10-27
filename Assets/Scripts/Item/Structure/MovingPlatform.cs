using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public enum MovementType { Vertical, Horizontal, Rotate }
    public MovementType movementType = MovementType.Vertical;

    [Header("상하, 좌우")]
    public float movementDistance = 5f;
    public float movementSpeed = 2f;

    [Header("회전")]
    public float rotationSpeed = 50f;
    public Vector3 rotationAxis = Vector3.up;

    private Vector3 startPosition;
    private Vector3 previousPosition;

    private void Start()
    {
        startPosition = transform.position; // 시작위치
        previousPosition = startPosition; // 직전프레임위치
    }

    private void Update()
    {
        switch (movementType)
        {
            case MovementType.Vertical:
                MoveVertical();
                ApplyMovementToObjectsOnPlatform();
                break;
            case MovementType.Horizontal:
                MoveHorizontal();
                ApplyMovementToObjectsOnPlatform();
                break;
            case MovementType.Rotate:
                RotatePlatform();
                break;
        }

        previousPosition = transform.position;
    }

    private void MoveVertical()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * movementSpeed) * movementDistance;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void MoveHorizontal()
    {
        float newX = startPosition.x + Mathf.Sin(Time.time * movementSpeed) * movementDistance;
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    private void RotatePlatform()
    {
        transform.Rotate(rotationAxis.normalized, rotationSpeed * Time.deltaTime, Space.Self);
    }

    private void ApplyMovementToObjectsOnPlatform()
    {
        Vector3 movementDelta = transform.position - previousPosition;
        Vector3 boxCenter = transform.position + Vector3.up * 0.5f - Vector3.right * 1.3f + Vector3.forward * 1.1f;
        Vector3 boxSize = new Vector3(2.5f, 1f, 2.5f);

        Collider[] colliders = Physics.OverlapBox(boxCenter, boxSize / 2);

        foreach (var col in colliders)
        {
            if (col.CompareTag("Player"))
            {
                col.transform.position += movementDelta;
            }
        }
    }
}
