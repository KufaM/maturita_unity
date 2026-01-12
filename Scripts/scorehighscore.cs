using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scorehighscore : MonoBehaviour
{
    public static int score = 0;
    public static scorehighscore instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void Kill()
    {
        score++;
        if (score > PlayerPrefs.GetFloat("Highscore", 0))
        {
            PlayerPrefs.SetFloat("Highscore", score);
        }
    }

    /* 
    !!! RESET HIGHSCORE NA KLÁVESNICI R !!!
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetScore();
        }
    }

    public static void ResetScore()
    {
        PlayerPrefs.SetFloat("Highscore", 0);
    }

    */ 
}