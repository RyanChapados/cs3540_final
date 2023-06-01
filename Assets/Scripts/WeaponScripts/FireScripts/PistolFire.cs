using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolFire : MonoBehaviour, IFireWeapon
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 100f;
    public Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        // Create a projectile
        GameObject projectile = Instantiate(
            projectilePrefab, cam.position + cam.forward, cam.rotation, GameObject.FindGameObjectWithTag("ProjectileParent").transform) as GameObject;

        // Get a reference to the rigidbody
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        // Applies force to the object
        rb.AddForce(transform.forward * projectileSpeed, ForceMode.VelocityChange);

        // Sets all projectiles as a child of projectile parent to make things more organized
        projectile.transform.SetParent(GameObject.FindGameObjectWithTag("ProjectileParent").transform);
    }
}
