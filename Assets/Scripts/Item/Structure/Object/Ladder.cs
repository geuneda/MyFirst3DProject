using UnityEngine;

public class Ladder : MonoBehaviour, IStructure
{
    [SerializeField] private float climbSpeed = 7f;
    [SerializeField] private float yMinLimit = 12f;
    [SerializeField] private float yMaxLimit = 17f;
    [SerializeField] private float exitCooldown = 0.5f;

    private bool isClimbing = false;
    private bool canExit = false;
    private float lastClimbTime;
    private Transform playerTransform;
    private Rigidbody playerRigidbody;

    private float fixedX = -64.02f;
    private float fixedZ = -2.42f;
    private float initialY;

    private void Update()
    {
        if (isClimbing)
        {
            HandleClimbing();

            if (Input.GetKeyDown(KeyCode.E) && canExit)
            {
                ExitLadder();
            }
            // 상호작용시 바로 탈출하는 버그 => 쿨타임으로 해결
            else if (Time.time - lastClimbTime > exitCooldown)
            {
                canExit = true;
            }
        }
    }

    public void Activate()
    {
        if (!isClimbing)
        {
            StartClimbing();
        }
        else
        {
            ExitLadder();
        }
    }

    private void StartClimbing()
    {
        playerTransform = CharacterManager.Instance.Player.transform;
        playerRigidbody = CharacterManager.Instance.Player.GetComponent<Rigidbody>();

        playerRigidbody.useGravity = false;
        playerRigidbody.velocity = Vector3.zero;

        initialY = playerTransform.position.y;

        playerTransform.position = new Vector3(fixedX, initialY, fixedZ);

        isClimbing = true;
        canExit = false;
        lastClimbTime = Time.time;
    }

    private void HandleClimbing()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float newY = playerTransform.position.y + (verticalInput * climbSpeed * Time.deltaTime);

        newY = Mathf.Clamp(newY, yMinLimit, yMaxLimit);

        playerTransform.position = new Vector3(fixedX, newY, fixedZ);
    }

    private void ExitLadder()
    {
        isClimbing = false;
        playerRigidbody.useGravity = true;

        if (playerTransform.position.y < (yMinLimit + yMaxLimit) / 2)
        {
            playerTransform.position = new Vector3(-64, 13, -1);
        }
        else
        {
            playerTransform.position = new Vector3(-64, 16, -4);
        }
    }
}
