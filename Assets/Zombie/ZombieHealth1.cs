using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieHealth1 : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public int damage;
    MoveTo1 moveTo;
    bool dead;
    public Animator anim;

    void Start()
    {
        currentHealth = startingHealth;
        moveTo = GetComponent<MoveTo1>();
        dead = false;
        damage = 30;
        anim = GetComponent<Animator>();
    }

    void Update()
    {

    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            anim.Play("Hit");
            currentHealth -= damageAmount;
            moveTo.ChangeStates(MoveTo1.ZombStates.Hit);
        }
        if (currentHealth <= 0)
        {
            if (!dead)
            {
                moveTo.ChangeStates(MoveTo1.ZombStates.Death);
                //GameObject.Find("GameManager").GetComponent<GameManager>().UpdateScore();
                dead = true;
            }
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Projectile"))
    //    {
    //        TakeDamage(damage);
    //        Destroy(other.gameObject);
    //    }
    //}

    public void ChangeBool()
    {
        dead = false;
    }

    public void TakeHealth(int health)
    {
        currentHealth += health;
    }
}