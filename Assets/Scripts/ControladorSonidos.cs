using UnityEngine;

public class ControladorSonidos : MonoBehaviour
{
    public static ControladorSonidos Instance;
    private AudioSource audioSource;

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        audioSource = GetComponent<AudioSource>();
    }
    public void EjecutarSonido(AudioClip sonido, float volumen = 1f)
    {
        audioSource.loop = false;
        audioSource.PlayOneShot(sonido, volumen);
    }
}
