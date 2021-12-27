using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    public float damage;
    //public Animator anim;
    public int maxHealth;
    int currentHealth;
    public bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        //anim = GetComponent<Animator>();
    }

    void Update()
    {

    }
    public void TakeDamage(int damage)
    {
        //anim.SetTrigger("Hurt");
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
            isDead = true;
        }
    }

    void Die()
    {
        //anim.SetBool("isDead", true);
        this.enabled = false;
        Destroy(gameObject, 2);
    }

}
