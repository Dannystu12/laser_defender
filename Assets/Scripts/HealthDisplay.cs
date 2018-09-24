using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour {

    private Player player;
    private TextMeshProUGUI healthText;
	
    // Use this for initialization
	void Start () {
        player = FindObjectOfType<Player>();
        healthText = GetComponent<TextMeshProUGUI>();
    }
	
	// Update is called once per frame
	void Update () {
        healthText.text = Mathf.Max(0, player.GetHealth()).ToString("000");
	}
}
