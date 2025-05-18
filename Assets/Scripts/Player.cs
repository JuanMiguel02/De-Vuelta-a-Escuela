using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Velocidad de carrera y salto del jugador
    [SerializeField] float runSpeed = 10;
    [SerializeField] float jumpSpeed = 5;

    // Referencias a componentes
    Rigidbody2D rigidbody;
    Collider2D collider;
    SpriteRenderer spriteRenderer;
    Animator animator;

    // Posición inicial del jugador
    float xInicial, yInicial;

    // Sonido de salto
    [SerializeField] private AudioClip jumpSound;

    // Parámetros de rebote tras recibir daño
    [Header("Rebote")]
    [SerializeField] private float velocidadRebote;

    // Estado de vida del jugador
    bool playerAlive = true;

    // Sonido de muerte
    [SerializeField] public AudioClip deathSound;

    // Referencia al menú de Game Over
    public GameObject menuGameOver;

    // Propiedad que retorna la velocidad horizontal del jugador
    public float DireccionX => rigidbody.linearVelocity.x;

    // Método Start:
    // Se ejecuta automáticamente una sola vez al inicio del juego o al activarse el objeto.
    // Aquí se inicializan variables y se obtienen referencias a componentes.
    void Start()
    {
        xInicial = transform.position.x;
        yInicial = transform.position.y;
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        collider = gameObject.GetComponent<Collider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Método Update:
    // Se llama una vez por frame (fotograma).
    // Aquí se detectan entradas del jugador, actualizaciones constantes y lógica de movimiento.
    void Update()
    {
        // Ejecuta acciones solo si el jugador está vivo
        if (playerAlive)
        {
            string escenaActual = SceneManager.GetActiveScene().name;

            // Limita el movimiento del jugador dentro de ciertos rangos según la escena actual
            if (transform.position.x < -40.12f)
                transform.position = new Vector3(-40.12f, transform.position.y, transform.position.z);

            if (escenaActual != "Escena 1" && escenaActual != "Escena 2" && transform.position.x < -27.8f)
                transform.position = new Vector3(-27.8f, transform.position.y, transform.position.z);

            if (transform.position.x > 175f)
                transform.position = new Vector3(175f, transform.position.y, transform.position.z);

            if (escenaActual != "Escena 1" && transform.position.x > 146.28f)
                transform.position = new Vector3(146.28f, transform.position.y, transform.position.z);

            if (escenaActual != "Escena 1" && escenaActual != "Escena 3" && transform.position.x > 134f)
                transform.position = new Vector3(134f, transform.position.y, transform.position.z);

            // Acciones de movimiento y animaciones
            Run();
            Jump();
            FlipSprite();
            CheckForGround();
        }
    }

    // Control del movimiento horizontal
    private void Run()
    {
        var getDirection = Input.GetAxis("Horizontal");
        rigidbody.linearVelocity = new Vector2(getDirection * runSpeed, rigidbody.linearVelocity.y);
        animator.SetBool("isRunning", getDirection != 0);
    }

    // Control del salto
    private void Jump()
    {
        animator.SetFloat("jumpVelocity", rigidbody.linearVelocity.y);

        // Solo puede saltar si está tocando el suelo
        if (!collider.IsTouchingLayers(LayerMask.GetMask("ground"))) return;

        if (Input.GetButton("Jump"))
        {
            // Reproduce sonido de salto y aplica velocidad vertical
            ControladorSonidos.Instance.EjecutarSonido(jumpSound, 0.3f);
            rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, jumpSpeed);
        }
    }

    // Voltea el sprite según la dirección en la que se mueve
    private void FlipSprite()
    {
        if (rigidbody.linearVelocity.x < 0)
            spriteRenderer.flipX = true;
        else if (rigidbody.linearVelocity.x > 0)
            spriteRenderer.flipX = false;
    }

    // Verifica si el jugador está tocando el suelo
    private void CheckForGround()
    {
        animator.SetBool("isGrounded", collider.IsTouchingLayers(LayerMask.GetMask("ground")));
    }

    // Método que pausa el juego y muestra el menú de Game Over
    public void Recolocar()
    {
        Time.timeScale = 0f; // Pausa el juego

        // Mostrar menú Game Over si está asignado
        if (menuGameOver != null)
        {
            menuGameOver.SetActive(true);
        }
        else
        {
            Debug.LogError("MenuGameOver no asignado en el inspector.");
        }   
    }

    // Detecta colisiones del jugador con otros objetos
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si colisiona con una plataforma, se hace hijo de esta para moverse con ella
        if (collision.gameObject.tag == "Platform")
        {
            transform.parent = collision.transform;
        }

        // Si colisiona con un objeto con layer "spike", recibe daño
        if (collision.gameObject.layer == LayerMask.NameToLayer("spike"))
        {
            RecibirDaño();
        }
    }

    // Se desasocia de la plataforma cuando deja de colisionar con ella
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            transform.parent = null;
        }
    }

    // Aplica un impulso vertical al jugador (rebote)
    public void Rebote()
    {
        rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, velocidadRebote);
    }

    // Método que se ejecuta cuando el jugador recibe daño
    public void RecibirDaño()
    {
        if (!playerAlive) return;

        // Reproduce sonido de muerte y detiene movimiento
        ControladorSonidos.Instance.EjecutarSonido(deathSound, 0.5f);
        playerAlive = false;
        rigidbody.linearVelocity = Vector2.zero;

        // Inicia rutina de parpadeo y reaparición
        StartCoroutine(ParpadeoYReaparicion());
    }

    // Corrutina que muestra un parpadeo del personaje antes de mostrar Game Over
    private IEnumerator ParpadeoYReaparicion()
    {
        float duracion = 1.5f;
        float intervalo = 0.2f;
        float tiempo = 0;

        // Guarda las restricciones actuales del rigidbody y las congela
        RigidbodyConstraints2D restriccionesOriginales = rigidbody.constraints;
        rigidbody.linearVelocity = Vector2.zero;
        rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;

        animator.enabled = false;

        // Alterna visibilidad del sprite para generar efecto de parpadeo
        while (tiempo < duracion)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(intervalo);

            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(intervalo);

            tiempo += intervalo * 2;
        }

        // Llama al método que pausa el juego y muestra Game Over
        Recolocar();
    }
}
