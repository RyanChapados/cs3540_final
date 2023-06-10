using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public int startingHealth = 100;
    public Slider healthSlider;

    [Header("SFX")]
    public AudioClip deadSFX;
    //public AudioClip damageSFX;

    [Header("Death Screen Effects")]
    public Color startColor;
    public Color targetColor;
    public Image deathScreen;

    private float lerpTimer = 0f;
    private AudioSource damageSource;
    int currentHealth;
    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        deathScreen.color = startColor;
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
        damageSource = GetComponent<AudioSource>();
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
       if (isDead)
        {
            lerpTimer += Time.deltaTime;

            // Update the panel's color
            deathScreen.color = Color.Lerp(startColor, targetColor, lerpTimer); ;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (!LevelManager.isGameOver)
        {
            // Causes the player to take 
            if (currentHealth > 0)
            {
                currentHealth -= damageAmount;

                // Updates the slider when you take damage
                healthSlider.value = currentHealth;
            }

            // Causes the player to die if they have 0 health
            if (currentHealth <= 0)
                PlayerDies();
        }
        
        // Checks again to ensure damage sound does not play after player death
        if (!LevelManager.isGameOver)
            damageSource.Play(0);
    }

    public void Heal(int healAmount)
    {
        if (!LevelManager.isGameOver)
        {
            currentHealth = Mathf.Clamp(currentHealth + healAmount, 0, 100);

            // Updates the slider when you heal
            healthSlider.value = currentHealth;
        }
    }

    void PlayerDies()
    {
        isDead = true;
        LevelManager.isGameOver = true;
        AudioSource.PlayClipAtPoint(deadSFX, transform.position);
        GetComponent<Animator>().SetTrigger("playerDeath");
    }
}
