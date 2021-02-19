using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Stats : MonoBehaviour
{
    private Animator animator;
    public string hurtAnimationsString;
    public string deathAnimationString;
    public BoxCollider2D EnemyWeapon;
    SpriteRenderer Enraged_Rogue_Face;


    [SerializeField]
    int currentHealth, maxHealth = 100;
    [SerializeField]
    int currentMana, maxMana = 100;
    public int Health
    {
        get { return currentHealth; }
        private set { currentHealth = value; }
    }
    public int Mana
    {
        get { return currentMana; }
        private set { currentMana = value; }
    }

    [SerializeField]
    int baseAttack = 10;
    public int BaseAttack
    {
        get { return baseAttack; }
        private set { baseAttack = value; }
    }

    [SerializeField]
    int baseDefense = 1;
    public int BaseDefense
    {
        get { return baseDefense; }
        private set { baseDefense = value; }
    }

    [SerializeField]
    int baseSpeed = 10;
    public int BaseSpeed
    {
        get { return baseSpeed; }
        private set { baseSpeed = value; }
    }

    [SerializeField]
    int baseFireDamage = 5;
    public int BaseFireDamage
    {
        get { return baseFireDamage; }
        private set { baseFireDamage = value; }
    }

    [SerializeField]
    int baseFireDefense = 5;
    public int BaseFireDefense
    {
        get { return baseFireDefense; }
        private set { baseFireDefense = value; }
    }

    [SerializeField]
    int baseWaterAttack = 5;
    public int BaseWaterAttack
    {
        get { return baseWaterAttack; }
        private set { baseWaterAttack = value; }
    }

    [SerializeField]
    int baseWaterDefense = 5;
    public int BaseWaterDefense
    {
        get { return baseWaterDefense; }
        private set { baseWaterDefense = value; }
    }

    [SerializeField]
    int baseIceAttack = 5;
    public int BaseIceAttack
    {
        get { return baseIceAttack; }
        private set { baseIceAttack = value; }
    }

    [SerializeField]
    int baseIceDefense = 5;
    public int BaseIceDefense
    {
        get { return baseIceDefense; }
        private set { baseIceDefense = value; }
    }




    void Start()
    {
        Enraged_Rogue_Face = GetComponentInChildren<SpriteRenderer>();
        IsEnemyWeaponActive(0);
        animator = GetComponent<Animator>();
    }

    void Awake()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (Health <= 50)
        { Enraged_Rogue_Face.color = new Color(1, 0, 0, 1); }
        

    }

    public void Die()
    {
        animator.Play(deathAnimationString);
        Debug.Log("Enemy has died");
        Collider2D[] colliders = this.GetComponents<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }
        GetComponent<Enemy_Controller>().enabled = false;
    }

    public void ApplyDamage(int dmg)
    {


        
        if (Health >= 50)
        {
            animator.Play(hurtAnimationsString);

            dmg -= this.BaseDefense;

            dmg = Mathf.Clamp(dmg, 1, int.MaxValue);
            AffectHealth(-dmg);
            Debug.Log("Enemy Health is" + this.Health);
        }
            
        if (Health < 50) 
        {
            
            dmg -= (this.BaseDefense + 30);

            dmg = Mathf.Clamp(dmg, 1, int.MaxValue);
            AffectHealth(-dmg);
            Debug.Log("Enemy Health is" + this.Health);
        }
            
        if (currentHealth <= 0)
            Die();
    }

    int AttackAction()
    {
        Debug.Log("Player has attacked for" + this.BaseAttack + "damage");
        return this.BaseAttack;
    }

    void AffectHealth(int value)
    {
        currentHealth += value;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public void IsEnemyWeaponActive(int value)
    {
        if (value == 1)
        {
            EnemyWeapon.enabled = true;
        }
        else
        {
            EnemyWeapon.enabled = false;
        }
    }

    public void TakeTurnBasedNeutralDamage(int dmg)
    {
        dmg -= this.BaseDefense;

        dmg = Mathf.Clamp(dmg, 1, int.MaxValue);
        AffectHealth(-dmg);
        Debug.Log("Enemy Health is" + this.Health);

        
    }

    

}
