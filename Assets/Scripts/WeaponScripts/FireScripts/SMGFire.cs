using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMGFire : MonoBehaviour, IFireWeapon
{
    public GameObject projectilePrefab;
    public Transform cam;

    [Header("Weapon Info")]
    public float projectileSpeed = 25f;
    public float spread = 30f;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
    }

    public void Fire()
    {
        // Create a projectile, offsetting it a bit from the camera spawnpoint
        GameObject projectile = Instantiate(
            projectilePrefab, cam.position + cam.forward, cam.rotation, GameObject.FindGameObjectWithTag("ProjectileParent").transform) as GameObject;

        // Get a reference to the rigidbody
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        // Generates a random angle to fire the projectile at
        float randomYAngle = Random.Range(-spread, spread);
        float randomXAngle = Random.Range(-spread, spread);
        Quaternion rotation = Quaternion.Euler(randomXAngle, randomYAngle, 0f);
        Vector3 fireAngle = rotation * transform.forward;

        // Applies force to the object
        rb.AddForce(fireAngle * projectileSpeed, ForceMode.VelocityChange);

        // Sets all projectiles as a child of projectile parent to make things more organized
        projectile.transform.SetParent(GameObject.FindGameObjectWithTag("ProjectileParent").transform);
    }
}
