using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior : MonoBehaviour, IInteractable
{
    [Header("Weapon Data")]
    public WeaponData weaponData;

    [Header("SFX")]
    public AudioClip fireSFX;
    public AudioClip reloadSFX;
    public AudioClip emptySFX;
    public float emptyCooldown = 0.5f;

    [Header("GameObjects")]
    public GameObject muzzleFlash;
    public Transform cam;

    private float timeSinceLastShot;
    private IFireWeapon fireWeapon;

    // Start is called before the first frame update
    void Start()
    {
        fireWeapon = gameObject.GetComponent<IFireWeapon>();
        cam = Camera.main.transform;
        weaponData.currentAmmo = weaponData.magSize;
        weaponData.reloading = false;

        // Deactivates the weapon if it is not being held
        if (transform.parent == null)
        {
            DisableScript();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelManager.isGameOver)
        {
            timeSinceLastShot += Time.deltaTime;

            // Fires the weapon if it has not been fired
            if (Input.GetButton("Fire1") && CanShoot())
            {
                if (WeaponEmpty())
                {
                    if (timeSinceLastShot > emptyCooldown)
                    {
                        AudioSource.PlayClipAtPoint(emptySFX, cam.position);
                        timeSinceLastShot = 0f;
                    }
                }
                else
                {
                    Shoot();
                    KickBack();
                }
            }

            // Begins the reload if the weapon is not already reloading and the button has been pressed, 
            if (Input.GetKeyDown(KeyCode.R) && !weaponData.reloading)
            {
                StartCoroutine(Reload());
            }
        }
    }

    // Allows the gun to be picked up when a user interacts with it
    public void Interact(Transform t)
    {
        enabled = true;

        // Moves the held gun to the correct location and disables it
        t.SetParent(null);
        t.position = transform.position;
        t.rotation = transform.rotation;
        t.GetComponent<WeaponBehavior>().DisableScript();

        // Sets this gun to be the active one
        transform.SetParent(GameObject.FindGameObjectWithTag("GunHolder").transform);
        transform.localPosition = weaponData.holdLocation;
        transform.rotation = transform.parent.rotation;
    }

    public void DisableScript()
    {
        enabled = false;
    }

    // Returns true if the weapon is out of ammo
    private bool WeaponEmpty() => weaponData.currentAmmo == 0;

    // Returns true if the player is not reloading and the shot does not exceed the fire rate
    private bool CanShoot() => !weaponData.reloading && timeSinceLastShot > 1f / (weaponData.fireRate / 60f);

    // Fires the projectile
    private void Shoot()
    {
        fireWeapon.Fire(weaponData.damage);

        // Creates a muzzle flash
        Instantiate(
            muzzleFlash, cam.position + cam.TransformDirection(weaponData.muzzleLocation),
            transform.rotation, GameObject.FindGameObjectWithTag("GunHolder").transform);

        // Plays a sound clip on fire
        AudioSource.PlayClipAtPoint(fireSFX, transform.position);

        // Reduces the ammo count, and sets the time since last shot to 0
        weaponData.currentAmmo--;
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

    private void KickBack()
    {
        // Rotate the gun by the kickback angle around its local X-axis
        Quaternion kickbackRotation = Quaternion.Euler(-Random.Range(weaponData.kickBack-3, weaponData.kickBack+3), 0f, 0f);
        transform.localRotation *= kickbackRotation;

        // Reset the gun's rotation after the kickback duration
        StartCoroutine(ResetRotationAfterDelay());
    }

    private System.Collections.IEnumerator ResetRotationAfterDelay()
    {
        // Lerp the gun's rotation back to the original rotation
        Quaternion targetRotation = new Quaternion(0,0,0,1);
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / weaponData.kickBackDuration;
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, t);
            yield return null;
        }

        // Ensure the gun's rotation is set to the original rotation exactly
        transform.localRotation = targetRotation;
    }
}
