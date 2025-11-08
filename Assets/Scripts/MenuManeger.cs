using UnityEngine;
using UnityEngine.SceneManagement;

// Simple menu manager / Gerenciador simples de menu
public class MenuManager : MonoBehaviour
{
    [Header("Scene to load / Cena para carregar")]
    [SerializeField] private string gameSceneName = "SampleScene";
    // Coloque aqui exatamente o nome da cena do jogo
    // Put here exactly the name of your game scene

    // Called by the Play button / Chamado pelo botão Play
    public void PlayGame()
    {
        // Load the game scene / Carrega a cena do jogo
        SceneManager.LoadScene(gameSceneName);

        // Make sure time is running normally (in case it was paused)
        // Garante que o tempo volte ao normal (caso estivesse pausado)
        Time.timeScale = 1f;
    }

    // Called by the Restart button on the main menu
    // Chamado pelo botão Restart no menu principal
    public void RestartGame()
    {
        // Just load the game scene again / Apenas recarrega a cena do jogo
        SceneManager.LoadScene(gameSceneName);
        Time.timeScale = 1f;
    }

    // Called by the Exit button / Chamado pelo botão Exit
    public void QuitGame()
    {
        // Log message so we see something in the editor
        // Mensagem de log para vermos algo no editor
        Debug.Log("Quit Game / Sair do jogo");

        // Close the application (works in build, not in editor)
        // Fecha o aplicativo (funciona no build, não no editor)
        Application.Quit();

        // Pelamor dos Deuses, funcione!!
    }
}
