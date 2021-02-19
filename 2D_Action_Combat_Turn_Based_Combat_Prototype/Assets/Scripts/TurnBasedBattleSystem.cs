using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum BattleState { PLAYERTURN, ENEMYTURN, NONE}
public class TurnBasedBattleSystem : MonoBehaviour

    
{
    [SerializeField]
    GameObject Player;


    
    public GameObject CommandoMenu;

    public GameObject ChoiceBox;

    public GameObject AttackBox;

    public Player_Stats playerStats;

    public Enemy_Stats enemyStats;


    [SerializeField]
    GameObject Enemy;

    [SerializeField]
    GameObject Camera;


    public BattleState state; 

    void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            
            GetComponent<Player_Controller>().enabled = false;
            Player.transform.position = new Vector2(-5, -4);

            GameObject Enemy = GameObject.FindWithTag("Enemy");
            //Enemy.GetComponent<Enemy_Controller>().enabled = false;
            Enemy.GetComponent<Enemy_Controller>().m_Speed = 0F;
            Enemy.transform.position = new Vector2(5, -4);

            CommandoMenu.SetActive(true);

            state = BattleState.PLAYERTURN;
            PlayerTurn();


        }
    }

    void PlayerTurn()
    {

    }

    IEnumerator PlayerNeutralAttack()
    {

        Enemy.GetComponent<Enemy_Stats>().TakeTurnBasedNeutralDamage(playerStats.BaseAttack);
        Debug.Log("Enemy Hit");
        yield return new WaitForSeconds(2f);
    }
    IEnumerator EnemyNeutralAttack()
    {

        Player.GetComponent<Player_Stats>().TakeTurnBasedNeutralDamage(enemyStats.BaseAttack);
        Debug.Log("Player Hit");
        yield return new WaitForSeconds(2f);
    }


    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        ChoiceBox.SetActive(false);
        AttackBox.SetActive(true);
        
    }

    public void OnNeutralAttackButton()
    {
        StartCoroutine(PlayerNeutralAttack());

        if (Enemy.GetComponent<Enemy_Stats>().Health <= 0)
        {
            Enemy.GetComponent<Enemy_Stats>().Die();
            CommandoMenu.SetActive(false);
            AttackBox.SetActive(false);
            GetComponent<Player_Controller>().enabled = true;
            Player.transform.position = new Vector2(-5, -3);
            state = BattleState.NONE;
        }

        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyNeutralAttack());
            state = BattleState.PLAYERTURN;
        }





    }
}
