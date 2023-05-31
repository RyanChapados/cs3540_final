using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieHealth : MonoBehaviour
{
    public int startingHealth = 100;
    //public AudioClip deadSFX;
    //public Slider healthSlider;
    int currentHealth;
    public Animator animator;
    MoveTo moveTo;
    Rigidbody rb;

    void Start()
    {
        currentHealth = startingHealth;
        //healthSlider.value = currentHealth;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;
            //healthSlider.value = currentHealth;
            animator.Play("gethit");
        }
        if (currentHealth <= 0)
        {
            ZombieDies();
        }
    }
    void ZombieDies()
    {
        //AudioSource.PlayClipAtPoint(deadSFX, transform.position);
        moveTo.Moving(false);
        animator.Play("death1");
        rb.isKinematic = true;
        Invoke("DeactivateObject", 5);
    }
    void DeactivateObject()
    {
        gameObject.SetActive(false);
    }
}
