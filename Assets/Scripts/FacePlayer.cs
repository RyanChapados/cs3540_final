using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    private Transform cam;

    private void Start()
    {
        // Find the player's transform
        cam = Camera.main.transform;
    }

    private void Update()
    {
        // Calculate the direction from the object to the player
        Vector3 directionToPlayer = cam.position - transform.position;

        // Rotate the object to face the player
        if (directionToPlayer != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(-directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }
}
