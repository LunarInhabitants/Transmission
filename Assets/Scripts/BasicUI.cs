using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicUI : MonoBehaviour
{
    [SerializeField]
    Player player;

    [SerializeField]
    Text healthText;
	
	void Update ()
	{
        healthText.text = string.Format("Health: {0}", player.Health);
    }
}