using UnityEngine;

public class FollowMe : MonoBehaviour
{
    [SerializeField] private Transform target;
    // The object this camera will follow (our player)
    // Objeto que a câmera vai seguir (nosso player)

    [SerializeField] private float smoothSpeed = 0.125f;
    // How smooth the camera follows the target (0 = instant, 1 = muito lento)
    // Quão suave é o movimento da câmera seguindo o alvo

    [SerializeField] private Vector3 offset = new Vector3(0f, 1.8f, 0f);
    // Offset from the target position (y pra deixar o player mais embaixo da tela)
    // Distância da câmera em relação ao alvo (y maior = mostra mais pra cima)

    private void Start()
    {
        if (target != null)
        {
            // Put the camera directly on the target at the very beginning
            // Coloca a câmera direto em cima do player no primeiro frame
            Vector3 startPos = target.position + offset;
            transform.position = new Vector3(startPos.x, startPos.y, transform.position.z);
        }
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        // Desired position = target position + offset
        // Posição desejada = posição do alvo + offset
        Vector3 desiredPosition = target.position + offset;

        // Keep current Z
        // Mantém o Z atual da câmera
        desiredPosition.z = transform.position.z;

        // Smooth follow using Lerp
        // Segue suavemente usando Lerp
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }
}
