using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    // Nome da cena do jogo (a sua fase)
    // Se o nome for diferente, troca aqui
    [SerializeField] private string gameSceneName = "SampleScene";

    // Botão PLAY (menu inicial)
    public void PlayGame()
    {
        Time.timeScale = 1f; // garante que o tempo volte ao normal
        SceneManager.LoadScene(gameSceneName);
    }

    // Botão RESTART (na tela de vitória ou pause)
    public void RestartGame()
    {
        Time.timeScale = 1f;
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.name);
    }

    // Botão EXIT
    public void ExitGame()
    {
#if UNITY_EDITOR
        // Pra funcionar dentro do editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Pra build
        Application.Quit();
#endif
    }
}
