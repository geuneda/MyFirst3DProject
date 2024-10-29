using UnityEngine;

public class LaserController : MonoBehaviour
{
    public float segmentLength = 1f;
    public float laserLength = 10f;
    public LayerMask hitLayer;
    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = Mathf.CeilToInt(laserLength / segmentLength) + 1;
    }

    private void Update()
    {
        ShootLaser();
    }

    private void ShootLaser()
    {
        Vector3 start = transform.position;
        Vector3 direction = transform.forward;

        for (int i = 0; i < lineRenderer.positionCount - 1; i++)
        {
            RaycastHit hit;
            Vector3 segmentEnd = start + direction * segmentLength;

            if (Physics.Raycast(start, direction, out hit, segmentLength, hitLayer))
            {
                lineRenderer.SetPosition(i, start);
                lineRenderer.SetPosition(i + 1, hit.point);
                CharacterManager.Instance.Player.condition.TakePhysicalDamage(10);
            }
            else
            {
                lineRenderer.SetPosition(i, start);
                start = segmentEnd;
            }
        }
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, start);
    }
}
