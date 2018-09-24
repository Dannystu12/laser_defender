using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {

    [SerializeField] string gameSceneName;
    [SerializeField] string gameOverSceneName;
    [SerializeField] AudioClip gameOverSound;
    [SerializeField] float volume = 0.75f;
    [SerializeField] float gameOverDelay = 2f;
    [SerializeField] float menuDelay = 0.25f;

    public void LoadGameScene()
    {
        StartCoroutine(LoadScene(gameSceneName, menuDelay));
        GameSession session = FindObjectOfType<GameSession>();
        if (session)
        {
            session.ResetScore();
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadGameOver()
    {
        AudioSource.PlayClipAtPoint(gameOverSound, Camera.main.transform.position, volume);
        StartCoroutine(LoadScene(gameOverSceneName, gameOverDelay));
    }

    private IEnumerator LoadScene(string sceneName,float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}  
 