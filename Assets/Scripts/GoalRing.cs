using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalRing : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // só reage se o player encostar
        if (!other.CompareTag("Player")) return;

        Debug.Log("Player entrou no anel!");

        // troca pra cena final
        SceneManager.LoadScene("ComingSoon");
    }
}
