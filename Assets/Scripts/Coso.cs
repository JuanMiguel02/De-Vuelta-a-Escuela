using UnityEngine;
using System.Collections;

public class Coso : MonoBehaviour
{  
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool golpeado = false;
    [SerializeField] private AudioClip SonidoGolpe;
    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && !golpeado)
        {
            bool fueDesdeArriba = false;

            foreach (ContactPoint2D punto in other.contacts)
            {
                if (punto.normal.y <= -0.9f)
                {
                    fueDesdeArriba = true;
                    break;
                }
            }

            if (fueDesdeArriba)
            {
                golpeado = true;
                animator.SetTrigger("Golpe");
                StartCoroutine(Morir());
                ControladorSonidos.Instance.EjecutarSonido(SonidoGolpe, 0.8f);
                other.gameObject.GetComponent<Player>().Rebote();
            }
            else
            {
                // Si el jugador lo tocó por otro lado, recibe daño
                other.gameObject.GetComponent<Player>().RecibirDaño();
            }
        }
    }

    public IEnumerator Morir()
    {
        StartCoroutine(Parpadear());
        yield return new WaitForSeconds(0.8f);

        GameManager.Instance.SumarPuntos(100);
        Destroy(gameObject);
    }

    private IEnumerator Parpadear()
    {
        for (int i = 0; i < 4; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }
}