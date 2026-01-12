using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    // Stamina bar

    public Slider slider;

    private void Update()
    {
        slider.value = PlayerMove.staminapoints;
    }
}
