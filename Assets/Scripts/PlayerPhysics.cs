using UnityEngine;

// Ensures the GameObject has a Rigidbody2D component
// (garante que o objeto tenha um Rigidbody2D, mas não trava o tipo de collider)
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerPhysics : MonoBehaviour
{
    // --- MOVEMENT VARIABLES ---

    // Speed of horizontal movement
    // (velocidade lateral do personagem)
    [SerializeField] private float moveSpeed = 5f;

    // Force applied when jumping
    // (força do pulo — quanto maior, mais Sonic e menos ser humano)
    [SerializeField] private float jumpForce = 7f;

    // Rigidbody2D reference for applying physics
    // (pra mandar o player voar com estilo)
    private Rigidbody2D rb;

    // Whether the player is touching the ground
    // (pra saber se dá pra pular ou se vai ser só vergonha)
    private bool isGrounded;

    // --- UNITY EVENTS ---

    private void Awake()
    {
        // Gets the Rigidbody2D component when the game starts
        // (pega o rigidbody automaticamente, porque lembrar de adicionar é pedir demais)
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // --- HORIZONTAL MOVEMENT ---
        // Reads input from A/D or arrow keys
        // (pega o input do teclado — vamos dar aquele drift lateral)
        float move = Input.GetAxis("Horizontal");

        // Applies horizontal velocity while keeping vertical one
        // (só mexe no eixo X, o Y continua sob a lei da gravidade)
        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);

        // --- JUMP ---
        // If Jump (Space) is pressed and player is grounded
        // (pula se estiver no chão — sem truques de pular no ar)
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    // --- COLLISION DETECTION ---

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Checks if player touched the ground
        // (se encostou no chão, então é isso: chão confirmado)
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Checks if player left the ground
        // (saiu do chão, hora de se arrepender das decisões)
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
