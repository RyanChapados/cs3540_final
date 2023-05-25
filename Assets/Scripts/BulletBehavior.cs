using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float destroyDuration = 5f;
    public GameObject bullectImpact;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyDuration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.transform.name);
        // Ensures the bullet does not hit the player
        if (!other.transform.CompareTag("Player"))
        {
            if (other.transform.CompareTag("Enemy"))
            {
                // Do damage to the enemy
            }
            else
            {
                // Stops the bullet completely on impact
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.velocity = Vector3.zero;

                // Finds a vector normal to the surface at the point of collision
                ContactPoint contact = other.contacts[0];
                Vector3 surfaceNormal = contact.normal;

                // Makes the bullet look in the direction of the normal vector
                transform.rotation = Quaternion.LookRotation(surfaceNormal);

                // Creates a particle effect where the bullect impacted
                gameObject.SetActive(false);
                Instantiate(bullectImpact, transform.position, transform.rotation);

            }
        }
    }
}
