using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 10;
    [SerializeField] float jumpSpeed = 5;
    Rigidbody2D rigidbody;
    Collider2D collider;
    SpriteRenderer spriteRenderer;
    Animator animator;
    float xInicial, yInicial;   
    [SerializeField] private AudioClip jumpSound;
    [Header("Rebote")]
    [SerializeField] private float velocidadRebote;
    bool playerAlive = true;
    [SerializeField] public AudioClip deathSound;
    bool sonidoReproducido = false;

    void Start()
    {
        xInicial = transform.position.x;
        yInicial = transform.position.y;
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        collider = gameObject.GetComponent<Collider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        
    }

    void Update()
    {    
        if (playerAlive)
        {
            string escenaActual = SceneManager.GetActiveScene().name;

            if (transform.position.x < -27.6f)
                transform.position = new Vector3(-27.6f, transform.position.y, transform.position.z);

            if (transform.position.x > 186.89f)
                transform.position = new Vector3(186.89f, transform.position.y, transform.position.z);

            if (escenaActual != "Escena 1" && transform.position.x > 146.28f)
                transform.position = new Vector3(146.28f, transform.position.y, transform.position.z);

            Run();  
            Jump();
            FlipSprite();
            CheckForGround();
            
        }
    }

    private void Run()
    {
        var getDirection = Input.GetAxis("Horizontal");
        rigidbody.linearVelocity = new Vector2(getDirection * runSpeed, rigidbody.linearVelocity.y);
        animator.SetBool("isRunning", getDirection != 0);
    }

    private void Jump()
    {
        animator.SetFloat("jumpVelocity", rigidbody.linearVelocity.y);

        if (!collider.IsTouchingLayers(LayerMask.GetMask("ground"))) return;

        if (Input.GetButton("Jump"))
        {
            ControladorSonidos.Instance.EjecutarSonido(jumpSound, 0.3f);
            rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, jumpSpeed);
        }
    }

    private void FlipSprite()
    {
        if (rigidbody.linearVelocity.x < 0)
            spriteRenderer.flipX = true;
        else if (rigidbody.linearVelocity.x > 0)
            spriteRenderer.flipX = false;
    }

    private void CheckForGround()
    {
        animator.SetBool("isGrounded", collider.IsTouchingLayers(LayerMask.GetMask("ground")));
    }

    public void Recolocar()
    {
        rigidbody.linearVelocity = Vector2.zero;// Detiene completamente el movimiento

        if(!sonidoReproducido)
        {
            ControladorSonidos.Instance.EjecutarSonido(deathSound, 0.5f);
            sonidoReproducido = true;
        }  
        
        transform.position = new Vector3(xInicial, yInicial, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            transform.parent = collision.transform;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("spike"))
    {
        RecibirDaño();
    }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            transform.parent = null;
        }
    }

    public void Rebote()
    {
        rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, velocidadRebote);
    }

    public void RecibirDaño()
    {
        if (!playerAlive) return;
        
        if(!sonidoReproducido){

        ControladorSonidos.Instance.EjecutarSonido(deathSound, 0.5f);
        sonidoReproducido = true;
        }

        playerAlive = false;
        rigidbody.linearVelocity = Vector2.zero;
        StartCoroutine(ParpadeoYReaparicion());
    }

    private IEnumerator ParpadeoYReaparicion()
    {
        float duracion = 1.5f;
        float intervalo = 0.2f;
        float tiempo = 0;

        RigidbodyConstraints2D restriccionesOriginales = rigidbody.constraints;

        rigidbody.linearVelocity = Vector2.zero;
        rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;

        animator.enabled = false;

        
            while (tiempo < duracion)
            {
                spriteRenderer.enabled = false;
                yield return new WaitForSeconds(intervalo);

                spriteRenderer.enabled = true;
                yield return new WaitForSeconds(intervalo);

                tiempo += intervalo * 2;
            }

        Recolocar();
        rigidbody.constraints = restriccionesOriginales;
        animator.enabled = true;
        sonidoReproducido = false;
        playerAlive = true;
    }
}