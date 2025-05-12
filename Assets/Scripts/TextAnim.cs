using UnityEngine;
using TMPro;
using System.Collections;

public class TextAnim : MonoBehaviour
{
    [SerializeField] private string textToShow;
    [SerializeField] private TextMeshProUGUI textUI;
    [SerializeField] private float time;

    [Header("Sonido")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip typingSound;

    void Start()
    {
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        textUI.text = textToShow;
        textUI.maxVisibleCharacters = 0;

        foreach (char c in textToShow)
        {
            textUI.maxVisibleCharacters++;

            // Sonido por cada letra (excepto espacios)
            if (!char.IsWhiteSpace(c) && typingSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(typingSound);
            }

            yield return new WaitForSeconds(time);
        }
    }
}