using System.Collections; // Needed for coroutines
// Necessário para usar corrotinas (IEnumerator / StartCoroutine)
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
// Guarantees this GameObject always has a Rigidbody2D and Collider2D
// Garante que este GameObject sempre tenha um Rigidbody2D e um Collider2D
public class PlayerPhysics : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;
    // Horizontal movement speed
    // Velocidade de movimento horizontal

    [SerializeField] private float jumpForce = 10f;
    // Upward velocity applied when jumping
    // Velocidade vertical aplicada ao pular

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    // Point used to check if the player is standing on the ground
    // Ponto usado para verificar se o player está encostando no chão

    [SerializeField] private float groundCheckRadius = 0.2f;
    // Radius of the ground check circle
    // Raio do círculo usado para checar o chão

    [SerializeField] private LayerMask groundLayer;
    // Which layers count as ground
    // Quais camadas contam como chão

    [Header("Damage / Knockback")]
    [SerializeField] private float knockbackHorizontal = 8f;
    // Horizontal force applied to the player when taking damage
    // Força horizontal aplicada no player quando leva dano

    [SerializeField] private float knockbackVertical = 4f;
    // Vertical force applied to the player when taking damage
    // Força vertical aplicada no player quando leva dano

    [SerializeField] private float invincibilityTime = 1.0f;
    // Time after getting hit during which the player cannot be hit again
    // Tempo de invencibilidade depois de levar dano (evita hit em loop)

    [Header("Visual Feedback")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    // Sprite used to flash when taking damage
    // Sprite usado para piscar quando leva dano

    private bool isInvincible = false;
    // Prevents taking multiple hits in a single contact
    // Impede que o player leve vários danos seguidos no mesmo encosto

    private bool isDead = false;
    // True after the player dies
    // Verdadeiro depois que o player morre

    private Rigidbody2D rb;
    // Reference to the Rigidbody2D component
    // Referência para o Rigidbody2D

    private Collider2D col;
    // Reference to the Collider2D component
    // Referência para o Collider2D

    private float inputX;
    // Horizontal input value (-1, 0, 1)
    // Valor do input horizontal (-1, 0, 1)

    private bool isGrounded;
    // True if the player is on the ground
    // Verdadeiro se o player estiver no chão

    private bool facingRight = true;
    // Used to flip the sprite left/right
    // Usado para virar o sprite para esquerda/direita

    private ScoreUI scoreUI;
    // Reference to the score/ring UI script
    // Referência para o script que mostra os anéis (ScoreUI)

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        // Cache components on awake
        // Guarda as referências dos componentes assim que o objeto acorda

        // If no SpriteRenderer was set in the Inspector, try to find one in children
        // Se nenhum SpriteRenderer foi ligado no Inspector, tenta achar em algum filho
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
    }

    private void Start()
    {
        // Find the ScoreUI in the scene (there should be only one)
        // Procura o ScoreUI na cena (deve existir só um)
        scoreUI = FindFirstObjectByType<ScoreUI>();
    }

    private void Update()
    {
        // If the player is dead, no more input or control
        // Se o player estiver morto, não lê mais input nem controla nada
        if (isDead)
            return;

        // Read horizontal input from A/D or arrow keys
        // Lê o input horizontal das teclas A/D ou setas
        inputX = Input.GetAxisRaw("Horizontal");

        // Check if the player is on the ground
        // Checa se o player está no chão
        CheckGround();

        // Jump when pressing Space while grounded
        // Pula quando aperta Espaço e está no chão
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        // Flip the sprite according to movement direction
        // Vira o sprite de acordo com a direção do movimento
        HandleFlip();
    }

    private void FixedUpdate()
    {
        // If the player is dead, no movement is applied
        // Se o player estiver morto, não aplica movimento
        if (isDead)
            return;

        // Apply horizontal movement in physics step
        // Aplica o movimento horizontal na etapa de física
        Move();
    }

    // -------------------------- MOVEMENT -------------------------- //

    private void Move()
    {
        // Keep the current vertical velocity
        // Mantém a velocidade vertical atual
        float currentY = rb.linearVelocity.y;

        // Set horizontal velocity based on input
        // Define a velocidade horizontal com base no input
        rb.linearVelocity = new Vector2(inputX * moveSpeed, currentY);
    }

    private void Jump()
    {
        // Simple jump: directly set the vertical velocity
        // Pulo simples: define diretamente a velocidade vertical

        // Keep current horizontal velocity, only change Y
        // Mantém a velocidade horizontal, muda só o Y
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    private void CheckGround()
    {
        // Uses a small circle below the player to check for ground
        // Usa um pequeno círculo abaixo do player para checar o chão
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );
    }

    private void HandleFlip()
    {
        // Only flip if we are actually moving horizontally
        // Só vira o sprite se estiver realmente se movendo horizontalmente
        if (inputX > 0 && !facingRight)
        {
            Flip();
        }
        else if (inputX < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        // Switch direction flag
        // Inverte a flag de direção
        facingRight = !facingRight;

        // Multiply localScale.x by -1 to flip the sprite
        // Multiplica o localScale.x por -1 para virar o sprite
        Vector3 scale = transform.localScale;
        scale.x *= -1f;
        transform.localScale = scale;
    }

    // -------------------------- DAMAGE / DEATH -------------------------- //

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If we touched an object tagged as "Enemy", take damage
        // Se encostamos em um objeto com tag "Enemy", leva dano
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(collision);
        }
    }

    private void TakeDamage(Collision2D collision)
    {
        // Do nothing if we are currently invincible or already dead
        // Não faz nada se estivermos em invencibilidade ou já mortos
        if (isInvincible || isDead)
            return;

        // Check if we have rings (score > 0)
        // Verifica se temos anéis (score > 0)
        bool hasRings = scoreUI != null && scoreUI.GetScore() > 0;

        if (hasRings)
        {
            // FIRST HIT: lose all rings and get knockback
            // PRIMEIRO HIT: perde todos os anéis e leva knockback
            scoreUI.ResetScore();
            ApplyKnockback(collision);
            StartCoroutine(InvincibilityCoroutine());
        }
        else
        {
            // SECOND HIT WITH NO RINGS: player dies
            // SEGUNDO HIT SEM ANÉIS: player morre
            StartCoroutine(DeathCoroutine());
        }
    }

    private void ApplyKnockback(Collision2D collision)
    {
        // Determine knockback direction: by default, push backwards
        // Determina a direção do knockback: por padrão empurra para trás
        float dirX = facingRight ? -1f : 1f;

        if (collision != null && collision.contactCount > 0)
        {
            Vector2 contactPoint = collision.GetContact(0).point;
            dirX = (transform.position.x - contactPoint.x) >= 0 ? 1f : -1f;
        }

        rb.linearVelocity = Vector2.zero;
        Vector2 force = new Vector2(dirX * knockbackHorizontal, knockbackVertical);
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    // -------------------------- INVINCIBILITY FLASH (RED) -------------------------- //

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        Color originalColor = spriteRenderer != null ? spriteRenderer.color : Color.white;
        float elapsed = 0f;
        bool toggle = false;

        while (elapsed < invincibilityTime)
        {
            elapsed += 0.1f;
            toggle = !toggle;
            if (spriteRenderer != null)
                spriteRenderer.color = toggle ? Color.red : originalColor;

            yield return new WaitForSeconds(0.1f);
        }

        if (spriteRenderer != null)
            spriteRenderer.color = originalColor;

        isInvincible = false;
    }

    // -------------------------- DEATH (WHITE FLASH + JUMP + FALL) -------------------------- //

    private IEnumerator DeathCoroutine()
    {
        isDead = true;
        isInvincible = true;

        rb.linearVelocity = Vector2.zero;

        Color originalColor = spriteRenderer != null ? spriteRenderer.color : Color.white;
        float elapsed = 0f;
        bool toggle = false;

        // 🔸 NEW: stop the camera from following the player
        // 🔸 NOVO: para a câmera de seguir o jogador quando ele morre
        Camera mainCam = Camera.main;
        if (mainCam != null)
        {
            FollowMe follow = mainCam.GetComponent<FollowMe>();
            if (follow != null)
            {
                follow.enabled = false;
            }
        }

        // Flash white before the jump
        // Pisca em branco antes do pulo
        while (elapsed < 0.6f)
        {
            elapsed += 0.1f;
            toggle = !toggle;
            if (spriteRenderer != null)
                spriteRenderer.color = toggle ? Color.white : originalColor;

            yield return new WaitForSeconds(0.1f);
        }

        if (spriteRenderer != null)
            spriteRenderer.color = Color.white;

        if (col != null)
            col.enabled = false;

        // Small upward jump before falling
        // Pequeno pulo pra cima antes de cair
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(Vector2.up * 6f, ForceMode2D.Impulse);

        // Wait a bit while he goes up
        // Espera um pouco enquanto ele sobe
        yield return new WaitForSeconds(0.5f);

        // Then fall down fast
        // Depois despenca pra baixo
        rb.AddForce(Vector2.down * 12f, ForceMode2D.Impulse);

        yield break;
    }

    // -------------------------- GIZMOS -------------------------- //

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
