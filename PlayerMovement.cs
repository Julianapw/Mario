using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Playermoviment : MonoBehaviour
{
  Rigidbody2D rbPlayer;
  [SerializeField] float speed = 5f; //velocidade do mario //essa variavel esta dizendo para o Unity armazenar e manter o valor desse campo

  [SerializeField] float jumpForce = 15f; //forca do pulo do mario
  [SerializeField] bool isJump; //variavel verifica se o mario esta pulando ou nao
  [SerializeField] bool inFloor = true; //variavel verifica se o personagem esta no chao ou nao, como se inicia no chao, o valor esta verdadeiro
  [SerializeField] Transform Groundcheck; //auxilia na checagem do chao
  [SerializeField] LayerMask groundLayer; //definir uma camada especifica

  Animator AnimPlayer;

  private void Awake()
 {
    AnimPlayer = GetComponent<Animator>();
    rbPlayer = GetComponent<Rigidbody2D>();
 }

 private void Update()
 {
   inFloor = Physics2D.Linecast(transform.position, Groundcheck.position,groundLayer); //Linecast cria uma linha invisivel de um ponto inicial, que é o mario ate um ponto final, que é o groundcheck //groundlayes = se a camada colidir com essa linha, a variavel sera verdadeira
   Debug.DrawLine(transform.position, Groundcheck.position, Color.blue);

   AnimPlayer.SetBool("Jump", !inFloor);

   if(Input.GetButtonDown("Jump") && inFloor) //verifico se estou apertando o botao jump e se o mario esta no chao
      isJump = true; // se as duas condiçoes acima forem satisfeitas, o valor sera verdadeiro
   else if (Input.GetButtonUp("Jump") && rbPlayer.velocity.y > 0)
      rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, rbPlayer.velocity.y * 0.5f); //quando soltarmos o botao jump, a velocidade sera diminuida na metade

 }
 private void FixedUpdate()
 {
    Move();
    JumpPlayer();
 }
   void Move()
 {
   float xMove = UnityEngine.Input.GetAxis("Horizontal"); //teclas que vou usar para mover o personagem
   rbPlayer.velocity = new Vector2(xMove*speed, rbPlayer.velocity.y); 

   AnimPlayer.SetFloat("Speed", Mathf.Abs(xMove));

   if(xMove > 0)
   {
      transform.eulerAngles = new Vector2(0,0); //se eu clicar no botao direito d, o corpo do mario vai se virar para a direita
   }
   else if (xMove < 0)
   {
      transform.eulerAngles = new Vector2(0,180); // se eu clicar no botao esquerdo a , o corpo do mario vai se virar para a esquerda
   }
 }
   void JumpPlayer()
   {
      if(isJump) //verifico se isJump é verdadeira, se o botao esta pressionado e se o mario esta no chao
      {
         rbPlayer.velocity = Vector2.up * jumpForce; //adicionei velocidade na vertical
         isJump = false; //é falso para o mario nao subir sem parar
      }
   }
}
