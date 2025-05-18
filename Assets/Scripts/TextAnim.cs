using UnityEngine;
using TMPro;
using System.Collections;

public class TextAnim : MonoBehaviour
{
    // Texto que se mostrará con la animación de tipeo
    [SerializeField] private string textToShow;

    // Referencia al componente de texto TextMeshPro en la UI
    [SerializeField] private TextMeshProUGUI textUI;

    // Tiempo de espera entre cada letra (velocidad de tipeo)
    [SerializeField] private float time;

    [Header("Sonido")]
    // Fuente de audio para reproducir el sonido
    [SerializeField] private AudioSource audioSource;

    // Sonido que se reproducirá al escribir cada letra
    [SerializeField] private AudioClip typingSound;

    // Start se llama al iniciar la escena o al activarse el objeto
    void Start()
    {
        // Inicia la animación de mostrar texto letra por letra
        StartCoroutine(ShowText());
    }

    // Corrutina que muestra el texto con efecto de escritura
    IEnumerator ShowText()
    {
        // Establece el texto completo pero oculta todos los caracteres al principio
        textUI.text = textToShow;
        textUI.maxVisibleCharacters = 0;

        // Itera sobre cada carácter del texto
        foreach (char c in textToShow)
        {
            // Muestra un carácter más
            textUI.maxVisibleCharacters++;

            // Si el carácter no es un espacio y hay sonido disponible, lo reproduce
            if (!char.IsWhiteSpace(c) && typingSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(typingSound);
            }

            // Espera el tiempo indicado antes de mostrar el siguiente carácter
            yield return new WaitForSeconds(time);
        }
    }
}
