using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Velocidade do jogador
    public float speed = 5f;

    private Rigidbody2D rb2d;
    private Vector2 movement;





    public float invincibilityTime = 2.0f;
    public float blinkTime = 0.05f;
    public float shakeMagnitude = 0.1f;
    public float shakeDuration = 0.5f;
    private bool isInvincible = false;

    private SpriteRenderer spriteRenderer;
    private SpriteRenderer spriteRendererParent;

    // Inicialização
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRendererParent = GetComponentInParent<SpriteRenderer>();
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isInvincible)
        {
            StartCoroutine(TakeDamage());
        }
    }
    IEnumerator TakeDamage()
    {
        isInvincible = true;
        StartCoroutine(Blink());
        StartCoroutine(CameraShake());

        yield return new WaitForSeconds(invincibilityTime);

        isInvincible = false;
        spriteRenderer.enabled = true;
    }
    IEnumerator Blink()
    {
        while (isInvincible)
        {
            yield return new WaitForSeconds(blinkTime);
            spriteRenderer.enabled = spriteRenderer.enabled = false;
            spriteRendererParent.enabled = spriteRenderer.enabled = false;

            yield return new WaitForSeconds(blinkTime);
            spriteRenderer.enabled = spriteRenderer.enabled = true;
            spriteRendererParent.enabled = spriteRenderer.enabled = true;

            yield return new WaitForSeconds(blinkTime);
            spriteRenderer.enabled = spriteRenderer.enabled = false;
            spriteRendererParent.enabled = spriteRenderer.enabled = false;

            yield return new WaitForSeconds(blinkTime);
            spriteRenderer.enabled = spriteRenderer.enabled = true;
            spriteRendererParent.enabled = spriteRenderer.enabled = true;
        }
    }
    IEnumerator CameraShake()
    {
        Vector3 originalPosition = Camera.main.transform.position;
        float elapsedTime = 0.0f;

        while (elapsedTime < shakeDuration)
        {
            float x = originalPosition.x + Random.Range(-1f, 1f) * shakeMagnitude;
            float y = originalPosition.y + Random.Range(-1f, 1f) * shakeMagnitude;
            Camera.main.transform.position = new Vector3(x, y, originalPosition.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Camera.main.transform.position = originalPosition;
    }
}