using UnityEngine;

public class MovimentoGatto : MonoBehaviour
{
    public float speed = 3f;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 input;

    // 1 = su, -1 = giù
    private int lastVerticalDir = -1;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        input = new Vector2(0f, vertical);

        // Speed per Idle/Walk
        animator.SetFloat("Speed", Mathf.Abs(vertical));

        // CAMBIO DIREZIONE IMMEDIATO
        if (vertical > 0)
        {
            lastVerticalDir = 1;
            animator.Play("Walk_su");   // gira SUBITO verso su
        }
        else if (vertical < 0)
        {
            lastVerticalDir = -1;
            animator.Play("Walk_giu");  // gira SUBITO verso giù
        }
        else
        {
            // fermo → idle corretto
            if (lastVerticalDir == 1)
                animator.Play("Idle_su");
            else
                animator.Play("Idle_giu");
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = input * speed;
    }
}
