using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GroundEnemy : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 2f;          // Velocidade do inimigo
    [SerializeField] private float moveDuration = 2f;   // Quanto tempo ele anda em uma direção antes de inverter

    private Rigidbody2D rb;
    private bool movingRight = true;
    private float moveTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        moveTimer = moveDuration;
    }

    private void FixedUpdate()
    {
        // Movimento na horizontal
        float direction = movingRight ? 1f : -1f;

        // Usa linearVelocity no lugar de velocity (corrige o aviso)
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);

        // Cronômetro pra inverter o movimento
        moveTimer -= Time.fixedDeltaTime;
        if (moveTimer <= 0f)
        {
            Flip();
            moveTimer = moveDuration;
        }
    }

    private void Flip()
    {
        movingRight = !movingRight;

        // Espelha o sprite no eixo X
        Vector3 scale = transform.localScale;
        scale.x *= -1f;
        transform.localScale = scale;
    }
}
