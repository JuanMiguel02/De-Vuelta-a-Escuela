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
    public void EjecutarSonidoLoop(AudioClip sonido, float volumen = 1f)
    {
        if (audioSource.clip == sonido && audioSource.isPlaying)
            return;
        audioSource.clip = sonido;
        audioSource.volume = volumen;
        audioSource.loop = true;
        audioSource.Play();
    }
    public void DetenerLoop()
    {
        audioSource.Stop();
        audioSource.loop = false;
        audioSource.clip = null;
    }
}
