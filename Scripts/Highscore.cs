using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Highscore : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float highscore;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Load the high score from PlayerPrefs
        highscore = PlayerPrefs.GetFloat("Highscore", 0);
        text.text = "Highscore: " + highscore.ToString();
    }

    private void Update()
    {
        text.text = "Highscore: " + highscore.ToString();
    }
    public static void ResetHighscoreForBuild()
    {
        PlayerPrefs.SetFloat("Highscore", 0);
    }
}