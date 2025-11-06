using UnityEngine;

// Ensures that the GameObject always has Rigidbody2D and Collider2D components
// (garante que o objeto tenha esses componentes — evita erro se eu esquecer de adicionar)
[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerPhysics : MonoBehaviour
{
    // Speed of horizontal movement
    // (velocidade que o player se move para os lados)
    [SerializeField] private float moveSpeed = 5f;

    // Force applied when jumping
    // (força do pulo — se aumentar, ele voa mais alto)
    [SerializeField] private float jumpForce = 7f;

    // Reference to the Rigidbody2D component for physics interactions
    // (pra eu conseguir aplicar velocidade e força via código)
    private Rigidbody2D rb;

    // Indicates whether the player is touching the ground
    // (saber se tá encostando no chão pra não pular no ar)
    private bool isGrounded;

    void Awake()
    {
        // Gets the Rigidbody2D component attached to the player
        // (pega o rigidbody automaticamente quando o jogo inicia)
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // --- Movement ---
        // Reads horizontal input (A/D or arrow keys)
        // (pega o valor do teclado pra mover o personagem)
        float move = Input.GetAxis("Horizontal");

        // Sets the velocity for movement while keeping vertical speed from gravity
        // (mexe só no eixo X, o Y continua sendo controlado pela gravidade)
        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);

        // --- Jump ---
        // Checks if Jump button (Space) is pressed and player is on the ground
        // (só deixa pular se estiver encostando no chão)
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Adds an upward impulse to make the player jump
            // (impulse = empurrão instantâneo pra cima)
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // When colliding with an object tagged as Ground, set isGrounded to true
        // (se encostar no chão, libera o pulo de novo)
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // When no longer touching the Ground, disable jumping
        // (saiu do chão, então não pode mais pular)
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
// added comment to test commit
