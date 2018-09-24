using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour {

    private int score = 0;

    public void Awake()
    {
        if(FindObjectsOfType<GameSession>().Length > 1)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void UpdateScore(int score)
    {
        this.score += score;
    }

    public void ResetScore()
    {
        score = 0;
    }
    public int GetScore()
    {
        return score;
    }
	

} 
