using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapon/Gun")]
public class WeaponData : ScriptableObject
{
    [Header("Info")]
    public new string name;

    [Header("Shooting")]
    public float damage;
    public Vector3 muzzleLocation;

    [Header("Reloading")]
    public int currentAmmo;
    public int magSize;

    [Tooltip("In RPM")] public float fireRate;
    [Tooltip("In seconds")] public float reloadTime;
    [HideInInspector] public bool reloading;
}
