using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunFire : MonoBehaviour, IFireWeapon
{
    [Header("GameObjects")]
    public GameObject projectilePrefab;
    public Transform cam;

    [Header("Weapon Info")]
    public float projectileSpeed = 100f;
    public float spread = 30f;
    public int bulletCount = 10;
    

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
    }

    public void Fire(int damageAmount)
    {
        for (int i=0; i<bulletCount; i++)
        {
            FireOne(damageAmount);
        }
    }

    // Fires a single shotgun bullet
    private void FireOne(int damageAmount)
    {
        // Create a projectile, offsetting it a bit from the camera spawnpoint
        Vector3 placement = new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f));
        GameObject projectile = Instantiate(
            projectilePrefab, cam.position + cam.forward + placement, cam.rotation, GameObject.FindGameObjectWithTag("ProjectileParent").transform) as GameObject;
        projectile.GetComponent<BulletBehavior>().SetDamage(damageAmount);

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
