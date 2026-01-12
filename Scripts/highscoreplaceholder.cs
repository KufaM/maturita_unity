using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class highscoreplaceholder : MonoBehaviour
{
    public void ResetHighscoreForBuild()
    {
        PlayerPrefs.SetFloat("Highscore", 0);
    }
}
