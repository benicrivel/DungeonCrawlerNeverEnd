using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Velocidade do jogador
    public float speed = 5f;

    private Rigidbody2D rb2d;
    private Vector2 movement;

    // Inicialização
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Atualização
    private void Update()
    {     
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }   
    private void FixedUpdate()
    {
        // Move o jogador com base nas entradas do teclado
        rb2d.MovePosition(rb2d.position + movement * speed * Time.fixedDeltaTime);
    }
}