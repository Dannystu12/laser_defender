using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour {

    private GameSession gameSession;
    private TextMeshProUGUI scoreText;

	// Use this for initialization
	void Start () {
        gameSession = FindObjectOfType<GameSession>();
        scoreText = GetComponent<TextMeshProUGUI>();
	}
	
	// Update is called once per frame

    void Update()
    {
        scoreText.text = gameSession.GetScore().ToString("00000");
    }
    
}
