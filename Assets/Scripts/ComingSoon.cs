using UnityEngine;
using System.Collections;

public class ComingSoonScreen : MonoBehaviour
{
    [SerializeField] private RectTransform mainImage;
    [SerializeField] private float animationTime = 0.5f;

    private void Start()
    {
        // Se não arrastar nada no Inspector, tenta usar o próprio RectTransform
        if (mainImage == null)
            mainImage = GetComponent<RectTransform>();

        StartCoroutine(PopIn());
    }

    private IEnumerator PopIn()
    {
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = Vector3.one;
        float t = 0f;

        mainImage.localScale = startScale;

        while (t < 1f)
        {
            t += Time.deltaTime / animationTime;
            float smooth = Mathf.SmoothStep(0f, 1f, t);
            mainImage.localScale = Vector3.Lerp(startScale, endScale, smooth);
            yield return null;
        }

        mainImage.localScale = endScale;
    }

    private void Update()
    {
        // ESC pra fechar o jogo (funciona no build)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Saindo pela tela COMING SOON.");
            Application.Quit();

#if UNITY_EDITOR
            // Pra parar o Play dentro do editor
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
