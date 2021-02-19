using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy_Attack : MonoBehaviour
{

    private Enemy_Stats enemy;
    void Start()
    {
        enemy = GetComponentInParent<Enemy_Stats>();
        print("Enemy Attack Value: " + enemy.BaseAttack);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player_Stats>().ApplyDamage(enemy.BaseAttack);
        }
    }

}
