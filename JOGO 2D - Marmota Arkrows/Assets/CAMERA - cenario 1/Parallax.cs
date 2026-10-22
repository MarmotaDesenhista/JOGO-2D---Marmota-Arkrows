using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform cameraTransform;
    public float velocidadeParallax = 0.1f;

    private Vector3 ultimaPosicaoCamera;

    void Start()
    {
        ultimaPosicaoCamera = cameraTransform.position;
    }

    void LateUpdate()
    {
        Vector3 movimentoCamera =
            cameraTransform.position - ultimaPosicaoCamera;

        transform.position += new Vector3(
            movimentoCamera.x * velocidadeParallax,
            movimentoCamera.y * velocidadeParallax,
            0f
        );

        ultimaPosicaoCamera = cameraTransform.position;
    }
}
