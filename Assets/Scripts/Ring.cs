using UnityEngine;

public class Ring : MonoBehaviour
{
    [SerializeField] private int value = 1;
    // How many points this ring gives
    // Quantos pontos (anéis) este ring vale

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only react if the object that touched is the player
        // Só reage se quem encostou for o player
        if (!other.CompareTag("Player"))
            return;

        // Try to find the ScoreUI in the scene
        // Tenta encontrar o ScoreUI na cena
        ScoreUI scoreUI = FindFirstObjectByType<ScoreUI>();

        if (scoreUI != null)
        {
            // Add this ring's value to the score
            // Adiciona o valor deste anel ao placar
            scoreUI.AddScore(value);
        }

        // Destroy the ring after being collected
        // Destrói o anel depois de ser coletado
        Destroy(gameObject);
    }
}
