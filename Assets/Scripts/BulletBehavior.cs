using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float destroyDuration = 5f;
    public int damage;

    [Header("Impact Effects")]
    public GameObject groundImpact;
    public GameObject zombieImpact;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyDuration);
    }

    public void SetDamage(int damageAmount)
    {
        damage = damageAmount;
    }

    void OnCollisionEnter(Collision other)
    {
        // Ensures the bullet does not hit the player
        if (!other.transform.CompareTag("Player"))
        {
            StopBullet();
            // Finds a vector normal to the surface at the point of collision
            ContactPoint contact = other.contacts[0];
            Vector3 surfaceNormal = contact.normal;

            // Makes the bullet look in the direction of the normal vector
            transform.rotation = Quaternion.LookRotation(surfaceNormal);

            // Creates a particle effect where the bullect impacted
            GameObject impact = Instantiate(groundImpact, transform.position, transform.rotation);
            impact.transform.SetParent(other.transform);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            StopBullet();

            // Creates a particle effect where the bullect impacted
            GameObject impact = Instantiate(zombieImpact, transform.position, transform.rotation);

            // Deals damage to the zombie
            other.transform.GetComponent<ZombieHealth1>().TakeDamage(damage);
        }
    }

    private void StopBullet()
    {
        // Stops the bullet completely on impact
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
