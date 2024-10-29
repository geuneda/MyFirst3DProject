using UnityEngine;

public class TeleportPanel : MonoBehaviour
{
    private Transform playerTransform;
    private float? playerCameraSensitivity;

    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject exit;

    private void Awake()
    {
        panel.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);
    }

    private void Start()
    {
        playerTransform = CharacterManager.Instance.Player.transform;
    }

    public void OnOpenTeleportPanel()
    {
        if (panel.activeSelf)
            return;

        panel.gameObject.SetActive(true);
        exit.gameObject.SetActive(true);

        if (playerCameraSensitivity == null)
        {
            playerCameraSensitivity = CharacterManager.Instance.Player.controller.lookSensitivity;
        }

        CharacterManager.Instance.Player.controller.lookSensitivity = 0;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void OffPanel()
    {
        if (!panel.activeSelf)
            return;

        if (playerCameraSensitivity != null)
        {
            CharacterManager.Instance.Player.controller.lookSensitivity = (float)playerCameraSensitivity;
        }

        panel.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void MoveToCrane()
    {
        playerTransform.position = new Vector3(-85.827f, 11.8f, -12.737f);
        OffPanel();
    }

    public void MoveToLadder()
    {
        playerTransform.position = new Vector3(-64.045f, 12.3f, -0.394f);
        OffPanel();
    }

    public void MoveToLaser()
    {
        playerTransform.position = new Vector3(-45.499f, 11.8f, 16.258f);
        OffPanel();
    }

    public void MoveToDoor()
    {
        playerTransform.position = new Vector3(-38.252f, 11.8f, 17.955f);
        OffPanel();
    }

    public void MoveToSpawn()
    {
        playerTransform.position = new Vector3(-30.53f, 11.9f, -11.02f);
        OffPanel();
    }
}
