using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform personagem;

    public float suavidade = 5f;

    void LateUpdate()
    {
        Vector3 posicaoDesejada = new Vector3(
            personagem.position.x,
            transform.position.y,
            transform.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            posicaoDesejada,
            suavidade * Time.deltaTime
        );
    }
}