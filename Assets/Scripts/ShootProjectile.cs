using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public WeaponData weaponData;
    public GameObject projectilePrefab;
    public float projectileSpeed = 100f;

    public AudioClip fireSFX;
    public AudioClip reloadSFX;

    public GameObject muzzleFlash;

    private float timeSinceLastShot;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(weaponData.currentAmmo);
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        // Fires the weapon if it has not been fired
        if (Input.GetButtonDown("Fire1") && CanShoot())
        {
            Shoot();
        }

        // Begins the reload if the weapon is not already reloading and the button has been pressed, 
        if ((Input.GetKeyDown(KeyCode.R) || weaponData.currentAmmo == 0) && !weaponData.reloading)
        {
            StartCoroutine(Reload());
        }
    }

    // Returns true if the player is not reloading and the shot does not exceed the fire rate
    private bool CanShoot() => !weaponData.reloading && timeSinceLastShot > 1f / (weaponData.fireRate / 60f) && weaponData.currentAmmo > 0;

    // Fires the projectile
    private void Shoot()
    {
        // Create a projectile
        GameObject projectile = Instantiate(
            projectilePrefab, transform.position + transform.forward, transform.rotation, GameObject.FindGameObjectWithTag("ProjectileParent").transform) as GameObject;

        // Get a reference to the rigidbody
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        // Applies force to the object
        rb.AddForce(transform.forward * projectileSpeed, ForceMode.VelocityChange);

        // Creates a muzzle flash
        Instantiate(
            muzzleFlash, transform.position + transform.TransformDirection(weaponData.muzzleLocation), 
            transform.rotation, GameObject.FindGameObjectWithTag("GunHolder").transform);

        // Sets all projectiles as a child of projectile parent to make things more organized
        projectile.transform.SetParent(GameObject.FindGameObjectWithTag("ProjectileParent").transform);

        // Plays a sound clip on fire
        AudioSource.PlayClipAtPoint(fireSFX, transform.position);

        // Reduces the ammo count, and sets the time since last shot to 0
        weaponData.currentAmmo--;
        Debug.Log(weaponData.currentAmmo);
        timeSinceLastShot = 0;
    }

    // Reloads the gun after a waiting certain duration
    private IEnumerator Reload()
    {
        weaponData.reloading = true;
        AudioSource.PlayClipAtPoint(reloadSFX, transform.position);

        yield return new WaitForSeconds(weaponData.reloadTime);

        weaponData.currentAmmo = weaponData.magSize;

        weaponData.reloading = false;
    }
}
