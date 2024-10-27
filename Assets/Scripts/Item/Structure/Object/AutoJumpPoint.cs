using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoJumpPoint : MonoBehaviour
{
    [SerializeField] private float jumpForce = 300f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject != null)
        {
            other.transform.position = transform.position + Vector3.up * 1.5f;

            var playerRigidbody = other.GetComponent<Rigidbody>();

            if (playerRigidbody != null)
            {
                playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }
}
