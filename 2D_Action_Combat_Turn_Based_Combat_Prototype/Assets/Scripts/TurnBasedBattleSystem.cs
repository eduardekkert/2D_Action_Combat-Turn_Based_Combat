using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum BattleState { PLAYERTURN, ENEMYTURN, NONE}
public enum AttackType { NEUTRAL, FIRE, WATER, ICE}
public class TurnBasedBattleSystem : MonoBehaviour

    
{
    [SerializeField]
    GameObject Player;


    
    public GameObject CommandoMenu;

    public GameObject ChoiceBox;

    public GameObject AttackBox;

    public Player_Stats playerStats;

    public Enemy_Stats enemyStats;

    public GameObject EnemyTargetBox;


    [SerializeField]
    GameObject Enemy;


    [SerializeField]
    List<GameObject> enemies = new List<GameObject>();

    [SerializeField]
    List<Transform> enemySpots = new List<Transform>();

    [SerializeField]
    List<GameObject> enemyButtons = new List<GameObject>();


    [SerializeField]
    GameObject Camera;


    public BattleState state;

    public AttackType attackType;

    void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            
            GetComponent<Player_Controller>().enabled = false;
            Player.transform.position = new Vector2(-5, -4);

            //GameObject Enemy = GameObject.FindWithTag("Enemy");
            //Enemy.GetComponent<Enemy_Controller>().enabled = false;



            for (int i=0; i< enemies.Count;i++)
            {
                enemies[i].GetComponent<Enemy_Controller>().m_Speed = 0F;
                enemies[i].transform.position = enemySpots[i].position;
            }
            //Enemy.GetComponent<Enemy_Controller>().m_Speed = 0F;
            //Enemy.transform.position = new Vector2(5, -4);
            //Enemy.GetComponent<Animator>().Play("Rogue_Idle_01");
            
                 for (int i = 0; i < enemyButtons.Count; i++)
            {
                if (i < enemies.Count)
                {
                    enemyButtons[i].SetActive(true);
                }
                else
                {
                    enemyButtons[i].SetActive(false);
                }
            }


            CommandoMenu.SetActive(true);

            state = BattleState.PLAYERTURN;
            PlayerTurn();


        }
    }

    void PlayerTurn()
    {

    }

    IEnumerator PlayerNeutralAttack(Enemy_Stats enemy)
    {

        enemy.TakeTurnBasedNeutralDamage(playerStats.BaseAttack);
        Debug.Log("Enemy Hit");
        yield return new WaitForSeconds(2f);
    }

    IEnumerator PlayerFireAttack(Enemy_Stats enemy)
    {

        enemy.TakeTurnBasedFireDamage(playerStats.BaseFireDamage);
        Debug.Log("Enemy Hit by FireAttack");
        yield return new WaitForSeconds(2f);
    }

    IEnumerator PlayerWaterAttack(Enemy_Stats enemy)
    {

        enemy.TakeTurnBasedWaterDamage(playerStats.BaseWaterAttack);
        Debug.Log("Enemy Hit by WaterAttack");
        yield return new WaitForSeconds(2f);
    }

    IEnumerator PlayerIceAttack(Enemy_Stats enemy)
    {

        enemy.TakeTurnBasedIceDamage(playerStats.BaseIceAttack);
        Debug.Log("Enemy Hit by IceAttack");
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

        EnemyTargetBox.SetActive(false);
        ChoiceBox.SetActive(false);
        AttackBox.SetActive(true);
        
    }

    public void OnEnemyTargetButtonClicked(int enemyId)
    {
        switch (attackType)
        {
            case AttackType.NEUTRAL:
                NeutralAttack(enemies[enemyId].GetComponent<Enemy_Stats>());
                break;
          
               

            case AttackType.FIRE:
                FireAttack(enemies[enemyId].GetComponent<Enemy_Stats>());
                break;

            case AttackType.WATER:
                WaterAttack(enemies[enemyId].GetComponent<Enemy_Stats>());
                break;

            case AttackType.ICE:
                IceAttack(enemies[enemyId].GetComponent<Enemy_Stats>());
                break;
        }


        
    }

    private void NeutralAttack(Enemy_Stats enemy)
    {
        StartCoroutine(PlayerNeutralAttack(enemy));

        if (enemy.Health <= 0)
        {
            enemy.Die();
            if (enemies.Count <= 0)
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

    public void OnNeutralAttackButton()
    {
        attackType = AttackType.NEUTRAL;
        EnemyTargetBox.SetActive(true);
        ChoiceBox.SetActive(false);
        AttackBox.SetActive(false);
       





    }
    private void FireAttack(Enemy_Stats enemy)
    {
        StartCoroutine(PlayerFireAttack(enemy));

        if (enemy.Health <= 0)
        {
            enemy.Die();
            if (enemies.Count <= 0)
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
    public void OnFireAttackButton()
    {
        attackType = AttackType.FIRE;
        EnemyTargetBox.SetActive(true);
        ChoiceBox.SetActive(false);
        AttackBox.SetActive(false);






    }

    private void WaterAttack(Enemy_Stats enemy)
    {
        StartCoroutine(PlayerWaterAttack(enemy));

        if (enemy.Health <= 0)
        {
            enemy.Die();
            if (enemies.Count <= 0)
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

    public void OnWaterAttackButton()
    {
        attackType = AttackType.WATER;
        EnemyTargetBox.SetActive(true);
        ChoiceBox.SetActive(false);
        AttackBox.SetActive(false);




    }

    private void IceAttack(Enemy_Stats enemy)
    {
        StartCoroutine(PlayerIceAttack(enemy));

        if (enemy.Health <= 0)
        {
            enemy.Die();
            if (enemies.Count <= 0)
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
    public void OnIceAttackButton()
    {
        attackType = AttackType.ICE;
        EnemyTargetBox.SetActive(true);
        ChoiceBox.SetActive(false);
        AttackBox.SetActive(false);





    }

    public void OnBackButton()
    {
        AttackBox.SetActive(false);
        ChoiceBox.SetActive(true);
    }

    public void OnBackToAttackBoxButton()
    {
        AttackBox.SetActive(true);
        EnemyTargetBox.SetActive(false);
    }
    public void OnRevertButton()
    {
        GetComponent<Player_Controller>().enabled = true;
        Player.transform.position = new Vector2(-5, -3);

        Enemy.GetComponent<Enemy_Controller>().m_Speed = 1F;
        Enemy.transform.position = new Vector2(5, -3);

        CommandoMenu.SetActive(false);

        state = BattleState.NONE;
        
    }

}
