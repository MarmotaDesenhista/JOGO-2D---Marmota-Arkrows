using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float velocidade = 5f;

    [Header("Pulo")]
    public float forcaDoPulo = 10f;
    public float corteDoPulo = 0.5f;

    [Header("Verificar Chão")]
    public Transform groundCheck;
    public float groundCheckRaio = 0.2f;
    public LayerMask camadaDoChao;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private float movimento;
    private bool noChao;
    private bool agachado;
    private bool solicitouPulo;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Movimento horizontal
        movimento = Input.GetAxisRaw("Horizontal");

	animator.SetBool("estaAndando", movimento != 0);

	Debug.Log("Bool estaAndando: " +
          animator.GetBool("estaAndando"));

        // Flip X
        if (movimento > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (movimento < 0)
        {
            spriteRenderer.flipX = true;
        }

        // Verificar se está no chão
        noChao = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRaio,
            camadaDoChao
        );
	Debug.Log("Está no chão: " + noChao);

        // Agachar enquanto segura S
        agachado = Input.GetKey(KeyCode.S);

        // Registrar o comando de pulo
        if (Input.GetKeyDown(KeyCode.Space))
        {
            solicitouPulo = true;
        }

        // Pulo variável: soltar Espaço reduz a altura
        if (Input.GetKeyUp(KeyCode.Space) &&
            rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                rb.linearVelocity.y * corteDoPulo
            );
        }

        // Atualizar animações
	animator.SetBool("estaAndando", movimento != 0);

        animator.SetBool("estaAgachado", agachado);

        animator.SetBool("noChao", noChao);

        animator.SetFloat("velocidadeY",
            rb.linearVelocity.y);
    }

    void FixedUpdate()
    {
        // Movimentação horizontal
        rb.linearVelocity = new Vector2(
            agachado ? 0 : movimento * velocidade,
            rb.linearVelocity.y
        );

        // Pular somente quando estiver no chão
        if (solicitouPulo)
        {
            if (noChao && !agachado)
            {
                rb.linearVelocity = new Vector2(
                    rb.linearVelocity.x,
                    forcaDoPulo
                );
            }

            solicitouPulo = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;

            Gizmos.DrawWireSphere(
                groundCheck.position,
                groundCheckRaio
            );
        }
    }
}
