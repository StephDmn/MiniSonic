using UnityEngine;

// Makes the ring spin and pulse visually
// deixa o anel girando bonitinho, do jeito que o Sonic aprovaria
public class RingSpin : MonoBehaviour
{
    // Rotation speed in degrees per second
    // velocidade da rotação do anel
    [SerializeField] private float rotationSpeed = 200f;

    // Pulse effect settings (scale animation)
    // configurações do pulso pra dar aquele brilho dramático
    [SerializeField] private float pulseSpeed = 3f;
    [SerializeField] private float minScale = 0.9f;
    [SerializeField] private float maxScale = 1.1f;

    // Store the original scale
    // guarda o tamanho original pra não virar um kaiju do nada
    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        // Rotate around Z axis
        // gira o anel em torno do eixo Z
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        // Simple pulsing effect using sine wave
        // faz o anel “respirar” um pouco, tipo brilho de item raro
        float t = (Mathf.Sin(Time.time * pulseSpeed) + 1f) * 0.5f;
        float scale = Mathf.Lerp(minScale, maxScale, t);

        transform.localScale = originalScale * scale;
    }
}
