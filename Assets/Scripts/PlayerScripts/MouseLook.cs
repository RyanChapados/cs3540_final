using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 130f;
    public GameObject interactIcon;

    float pitch = 0;
    Transform playerBody;
    private GameObject currentIcon;

    // Start is called before the first frame update
    void Start()
    {
        // Sets up sensitivity
        if (PlayerPrefs.HasKey("sensitivity"))
            sensitivity = PlayerPrefs.GetFloat("sensitivity");

        playerBody = transform.parent.transform;

        // Hide the cursor and lock it to the center of the screen
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelManager.isGameOver && !PauseMenu.isGamePaused)
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

            // Allows the player to interact with objects
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 5f) && hit.collider.CompareTag("Interactable"))
            {
                // Fixes a bug which causes the icon to stick to the held gun (WILL BREAK IF YOU ADD OTHER CHILDREN TO THE GUN)
                if (hit.transform.childCount == 0)
                    Destroy(currentIcon);

                // Creates an icon if the player looks at an object
                if (currentIcon == null || hit.transform.childCount == 0)
                {
                    Vector3 iconPosition = hit.transform.position - (hit.transform.position - transform.position).normalized * .3f;
                    currentIcon = Instantiate(interactIcon, iconPosition, transform.rotation, hit.collider.transform);
                }
                        
                // Handles interaction
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.GetComponent<IInteractable>().Interact(GameObject.FindGameObjectWithTag("GunHolder").transform.GetChild(0));
                    Destroy(currentIcon);
                }
            }
            else
            {
                // Destroys the icon if it is not null
                if (currentIcon != null)
                {
                    Destroy(currentIcon);
                }
            }
        }
    }
}
