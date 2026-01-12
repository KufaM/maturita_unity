using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public float fadeTime = 0.5f;
    private bool startFading = false;

    void Start()
    {
        Invoke("StartFading", 0.25f);
    }

    void StartFading()
    {
        startFading = true;
    }

    void Update()
    {
        if (startFading)
        {
            Image image = GetComponent<Image>();
            Color imageColor = image.color;
            float newAlpha = Mathf.MoveTowards(image.color.a, 0, fadeTime * Time.deltaTime);
            image.color = new Color(imageColor.r, imageColor.g, imageColor.b, newAlpha);

            if (image.color.a == 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}