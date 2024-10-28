using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Crane : MonoBehaviour, IStructure
{
    [SerializeField] private TextMeshProUGUI notiText;
    [SerializeField] private Camera craneCamera;
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private GameObject crane;

    private PlayerInput playerInput;
    private bool isControllingCrane = false;

    private void Start()
    {
        playerInput = CharacterManager.Instance.Player.GetComponent<PlayerInput>();
    }

    public void Activate()
    {
        var playerTransform = CharacterManager.Instance.Player.transform;

        playerTransform.position = new Vector3(-86.421f, 11.841f, -11.264f);
        OnControllerCrane();
    }

    private void OnControllerCrane()
    {
        isControllingCrane = true;
        notiText.text = "Q : 나가기 / A : 왼쪽 / D : 오른쪽";

        playerInput.enabled = false;

        craneCamera.gameObject.SetActive(true);
    }

    private void OffControllerCrane()
    {
        isControllingCrane = false;
        notiText.text = null;

        playerInput.enabled = true;

        craneCamera.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isControllingCrane)
        {
            float rotationInput = Input.GetAxis("Horizontal");
            crane.transform.Rotate(Vector3.up, rotationInput * rotationSpeed * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Q))
            {
                OffControllerCrane();
            }
        }
    }
}
