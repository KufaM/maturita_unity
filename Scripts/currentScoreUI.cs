using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class currentScoreUI : MonoBehaviour
{
    public TextMeshProUGUI text;
    public static float score;

    private void Start()
    {
        scorehighscore.score = 0;
        score = scorehighscore.score;
    }

    private void Update()
    {
        score = scorehighscore.score;
        text.text = "Score: "+ score.ToString();
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadSceneAsync(0);
            }
        }
    }
}