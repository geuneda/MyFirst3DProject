using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public Transform player;
    public Transform cameraTransform;
    public float rayLength = 2f;
    public float smoothSpeed = 5f;
    public float cameraYOffset = 1f;

    private float originalCameraY;

    private void Start()
    {
        originalCameraY = cameraTransform.localPosition.y;
    }

    private void Update()
    {
        AdjustCameraHeight();
    }

    private void AdjustCameraHeight()
    {
        Ray ray = new Ray(player.position + Vector3.up * cameraYOffset, Vector3.up);
        RaycastHit hit;

        float targetY = originalCameraY;

        if (Physics.Raycast(ray, out hit, rayLength))
        {
            float distanceToObstacle = hit.distance;
            targetY = originalCameraY - (rayLength - distanceToObstacle) - 1f;
        }
        
        Vector3 targetPosition = cameraTransform.localPosition;
        targetPosition.y = Mathf.Lerp(cameraTransform.localPosition.y, targetY, Time.deltaTime * smoothSpeed);
        cameraTransform.localPosition = targetPosition;
    }
}
