using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{

    public GameObject projectilePrefab;
    public float projectileSpeed = 100f;
    public AudioClip fireSFX;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            // Create a projectile
            GameObject projectile = Instantiate(projectilePrefab, transform.position + transform.forward, transform.rotation) as GameObject;

            // Get a reference to the rigidbody
            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            // Applies force to the object
            rb.AddForce(transform.forward * projectileSpeed, ForceMode.VelocityChange);

            // Sets all projectiles as a child of projectile parent to make things more organized
            projectile.transform.SetParent(GameObject.FindGameObjectWithTag("ProjectileParent").transform);

            // Plays a sound clip on fire
            AudioSource.PlayClipAtPoint(fireSFX, transform.position);
        }
    }
}
