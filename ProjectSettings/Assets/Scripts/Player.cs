
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
      xInicial = transform.position.x;
      yInicial = transform.position.y;
      rigidbody = gameObject.GetComponent<Rigidbody2D>();
      collider = gameObject.GetComponent<Collider2D>();
      spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
      animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    { 
         if (transform.position.x < -27.6f){
            transform.position = new Vector3(-27.6f, transform.position.y, transform.position.z);
         }
         Run();  
         Jump();
         FlipSprite();
         CheckForGround();
    }

    private void Run()
    {
       var getDirection = Input.GetAxis("Horizontal");
       rigidbody.linearVelocity = new Vector2(getDirection * runSpeed, rigidbody.linearVelocity.y);
       animator.SetBool("isRunning", true);
       if(getDirection == 0)
       {
         animator.SetBool("isRunning", false);
       }
    }
    
    private void Jump()
    {
        animator.SetFloat("jumpVelocity", rigidbody.linearVelocity.y);
       if(!collider.IsTouchingLayers(LayerMask.GetMask("ground"))){return;}
       if(Input.GetButton("Jump"))
       {
         ControladorSonidos.Instance.EjecutarSonido(jumpSound, 0.3f);
         rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, jumpSpeed);
       }
      
    }

   private void FlipSprite()
   {
      if(rigidbody.linearVelocity.x < 0)
      {
         spriteRenderer.flipX = true;
      }
      else if(rigidbody.linearVelocity.x > 0)
      {
          spriteRenderer.flipX = false;
      }
   }

   private void CheckForGround(){
    if(collider.IsTouchingLayers(LayerMask.GetMask("ground")))
    {
        animator.SetBool("isGrounded", true);
    } else {
        animator.SetBool("isGrounded", false);
    }
   }

   public void Recolocar()
   {
      transform.position = new Vector3(xInicial, yInicial, -2);
   }
}
