using UnityEngine;
using TMPro;  // TextMeshPro for UI text
// importa o tipo de texto chique da UI

/// Handles the score display on screen
/// responsável por mostrar quantos anéis eu já roubei
public class ScoreUI : MonoBehaviour
{
    // Reference to the TextMeshProUGUI component that shows the score
    // a caixinha de texto "Rings: 0"
    [SerializeField] private TextMeshProUGUI scoreText;

    // Current score value
    // contador oficial de riqueza
    private int currentScore = 0;

    private void Start()
    {
        // Initialize UI text when the scene starts
        // garante que o texto começa certinho com o valor inicial
        UpdateScoreText();
    }

    // Adds value to the score and updates the UI
    // somar pontos porque merecemos
    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateScoreText();
    }

    // Updates the text component with the current score
    // atualiza o HUD pra mostrar q estamos rycos
    private void UpdateScoreText()
    {
        scoreText.text = "Rings: " + currentScore;
    }
}

