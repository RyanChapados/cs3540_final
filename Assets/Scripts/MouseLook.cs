using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 130f;

    float pitch = 0;
    Transform playerBody;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = transform.parent.transform;

        // Hide the cursor and lock it to the center of the screen
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Get mouse input
        float moveX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float moveY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // yaw
        playerBody.Rotate(Vector3.up * moveX);

        // pitch, must subtract to show direction of movement
        pitch -= moveY;

        // Ensure pitch stays in a range
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        transform.localRotation = Quaternion.Euler(pitch, 0, 0);
    }
}
