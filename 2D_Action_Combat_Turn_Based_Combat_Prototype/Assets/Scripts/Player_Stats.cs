using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{
    private Animator animator;
    public BoxCollider2D weapon;

    [SerializeField]
    int currentHealth;
    [SerializeField]
    int currentMana;
    public int Health
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }
    [SerializeField]
    int maxHealth = 100;
    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
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
        IsWeaponActive(0);
        animator= GetComponent<Animator>();
    }

    void Awake()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (Input.GetKeyDown("h"))
        {
            ApplyDamage(12);
        }
    }

    void Die()
    {
        Debug.Log("Player has died");
    }

    public void ApplyDamage(int dmg)
    {
        if (!weapon.enabled)
        {
            animator.Play("Hurt");
            dmg -= this.BaseDefense;
        }
        
        dmg = Mathf.Clamp(dmg, 1, int.MaxValue);
        AffectHealth(-dmg);
        Debug.Log("Player Health is" + this.Health);
        if (currentHealth <= 0)
            Die();
    }

    int AttackAction()
    {
        Debug.Log("Player has attacked for" + this.BaseAttack + "damage");
        return this.BaseAttack;
    }

    void AffectHealth (int value)
    {
        currentHealth += value;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public void IsWeaponActive(int value)
    {
        if(value ==1)
        {
            weapon.enabled = true;
        }
        else
        {
            weapon.enabled = false;
        }
    }

    public void TakeTurnBasedNeutralDamage(int dmg)
    {
        dmg -= this.BaseDefense;

        dmg = Mathf.Clamp(dmg, 1, int.MaxValue);
        AffectHealth(-dmg);
        Debug.Log("Player Health is" + this.Health);


    }




}
