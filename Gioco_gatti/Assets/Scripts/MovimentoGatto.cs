using UnityEngine;

public class MovimentoGatto : MonoBehaviour
{
    [Header("Impostazioni")]
    public float velocita = 5f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator anim;

    private Vector2 movimento;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>(); // <-- NUOVO: collega l'Animator
    }

    void Update()
    {
        // 1. Leggiamo i tasti WASD
        movimento.x = Input.GetAxisRaw("Horizontal");
        movimento.y = Input.GetAxisRaw("Vertical");

        // 2. Normalizziamo (evita velocità extra in diagonale)
        movimento = movimento.normalized;

        // 3. Aggiorniamo il parametro Speed dell'Animator
        //    Speed = 0 → Idle
        //    Speed > 0 → Walk
        anim.SetFloat("Speed", movimento.sqrMagnitude);

        // 4. Flip del gatto (solo per sinistra/destra)
        if (movimento.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (movimento.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    void FixedUpdate()
    {
        // 5. Movimento fisico
        rb.MovePosition(rb.position + movimento * velocita * Time.fixedDeltaTime);
    }
}
