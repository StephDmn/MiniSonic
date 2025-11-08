using UnityEngine;
using TMPro; // Needed for TextMeshPro text
// Necessário para usar o TextMeshPro

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    // Reference to the UI text that displays the score
    // Referência para o texto de UI que mostra o placar (anéis)

    private int score = 0;
    // Current score (number of rings)
    // Placar atual (quantidade de anéis)

    private void Start()
    {
        // Make sure the UI is updated at the start
        // Garante que o texto comece com o valor certo
        UpdateScoreText();
    }

    public void AddScore(int amount)
    {
        // Increases the score by the given amount
        // Aumenta o placar pela quantidade recebida
        score += amount;
        UpdateScoreText();
    }

    public void ResetScore()
    {
        // Resets the score to zero
        // Zera o placar (perde todos os anéis)
        score = 0;
        UpdateScoreText();
    }

    public int GetScore()
    {
        // Returns the current score
        // Retorna o placar atual (quantos anéis o player tem)
        return score;
    }

    private void UpdateScoreText()
    {
        // Updates the UI text with the current score
        // Atualiza o texto da interface com o valor do placar
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }
}
