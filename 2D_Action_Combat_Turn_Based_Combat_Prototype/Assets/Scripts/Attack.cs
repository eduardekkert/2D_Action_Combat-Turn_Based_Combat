using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Player_Stats player;

    void Start()
    {
        player = GetComponentInParent<Player_Stats>();
        print("Player Attack Value: " + player.BaseAttack);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="Enemy")
        {
            other.gameObject.GetComponent<Enemy_Stats>().ApplyDamage(player.BaseAttack);
            Debug.Log("Enemy Hit");
        }
    }
}
