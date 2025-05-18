using UnityEngine;
using TMPro;
using System.Collections;

public class TMPTextBlink : MonoBehaviour
{
    public TMP_Text targetText; // Asigna aquí el TMP_Text del botón
    public float blinkSpeed = 0.5f; // Tiempo entre cada parpadeo

    private void Start()
    {
        StartCoroutine(BlinkText());
    }

    IEnumerator BlinkText()
    {
        while (true)
        {
            Color color = targetText.color;
            color.a = 0f;
            targetText.color = color;
            yield return new WaitForSeconds(blinkSpeed);

            color.a = 1f;
            targetText.color = color;
            yield return new WaitForSeconds(blinkSpeed);
        }
    }
}