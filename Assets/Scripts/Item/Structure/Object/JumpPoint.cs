using System.Runtime.CompilerServices;
using UnityEngine;

public class JumpPoint : MonoBehaviour, IStructure
{
    [SerializeField] private float jumpForce = 700f;
    public void Activate()
    {
        var playerTransform = CharacterManager.Instance.Player.transform;

        playerTransform.position = transform.position + Vector3.up * 1.5f;

        var playerRigidbody = CharacterManager.Instance.Player.GetComponent<Rigidbody>();

        if (playerRigidbody != null)
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
