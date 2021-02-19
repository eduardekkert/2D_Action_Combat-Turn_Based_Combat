using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
	float playerHealthValue;
	public Player_Stats player;
    public Image healthFillImage;
    public GameObject heartIcon;
    void Start()
    {
        
    }

   
    void Update()
    {
		
		playerHealthValue = player.Health;
		healthFillImage.fillAmount = playerHealthValue / player.MaxHealth;
		
		print("Health Fill Image Value: " + playerHealthValue / player.MaxHealth + " Player's Current Health: " + player.Health +
		" Player's Max Health: " + player.MaxHealth);
		
	}
}

