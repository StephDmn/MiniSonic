using UnityEngine;
using UnityEngine.SceneManagement;

// Handles the Main Menu logic (Play, Restart, Exit)
// Gerencia o menu principal (Jogar, Reiniciar, Sair)
public class MainMenu : MonoBehaviour
{
    // Called when the player clicks the Play button
    // Chamado quando o jogador clica em "Jogar"
    public void PlayGame()
    {
        // Load the main gameplay scene
        // Carrega a cena principal do jogo
        SceneManager.LoadScene("SampleScene"); // make sure your scene name matches this one
    }

    // Called when the player clicks the Restart button
    // Chamado quando o jogador clica em "Reiniciar"
    public void RestartGame()
    {
        // Reload the current active scene
        // Recarrega a cena atual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Called when the player clicks the Exit button
    // Chamado quando o jogador clica em "Sair"
    public void ExitGame()
    {
        // Quit the game or stop play mode in the editor
        // Sai do jogo ou encerra o modo de jogo no editor
        Debug.Log("Game closed / Jogo encerrado!");
        Application.Quit();

#if UNITY_EDITOR
        // Stops play mode inside the Unity Editor
        // Interrompe o modo Play dentro do Editor Unity
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
