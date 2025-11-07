using UnityEngine;

// Handles ring collection and score update
// Gerencia o drama inteiro de pegar o anel e atualizar o bendito placar
public class Ring : MonoBehaviour
{
    // Number of points this ring gives
    // Valor do anel — quanto mais, mais ryca
    [SerializeField] private int value = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is the player
        // Se quem encostou foi o player, então bora pegar o anel
        if (other.CompareTag("Player"))
        {
            // Try to find the ScoreUI in the scene
            // Unity, eu imploro, acha esse script, pelo amor dos deuses do C#
            ScoreUI scoreUI = FindFirstObjectByType<ScoreUI>();

            if (scoreUI != null)
            {
                // Add the ring's value to the score
                // Ufa! Finalmente alguma coisa deu certo
                scoreUI.AddScore(value);
            }

            // Destroy the ring after it's collected
            // Adeus, pequeno anel pixelado, cumpriste teu destino
            Destroy(gameObject);
        }
    }
}
