using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

public class LifeDisplay : MonoBehaviour
{
    public static float maxHp = 100;
    public static float currentHp;
    public TMP_Text hp;

    private void Start()
    {
        currentHp = maxHp;
    }
    void Update()
    {
        hp.SetText($"{(int)currentHp}");
        if (currentHp == 0)
        {
            SceneManager.LoadScene(3);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}