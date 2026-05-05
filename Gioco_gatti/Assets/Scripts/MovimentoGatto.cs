using UnityEngine;

public class MovimentoGatto : MonoBehaviour
{
    [Header("Impostazioni")]
    public float velocita = 5f; // Quanto va veloce il gatto

    // Le "scatole" dove salviamo i componenti
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 movimento;

    void Start()
    {
        // Quando il gioco inizia, Unity cerca il motore fisico e il disegno del gatto
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 1. Leggiamo i tasti WASD (o le frecce direzionali)
        movimento.x = Input.GetAxisRaw("Horizontal");
        movimento.y = Input.GetAxisRaw("Vertical");

        // 2. Normalizziamo la velocità (evita che il gatto corra più veloce quando va in diagonale)
        movimento = movimento.normalized;

        // 3. Giriamo il disegno del gatto a destra o a sinistra
        if (movimento.x < 0) 
        {
            // Se premo A (vado a sinistra), specchio l'immagine
            spriteRenderer.flipX = true;  
        } 
        else if (movimento.x > 0) 
        {
            // Se premo D (vado a destra), rimetto l'immagine normale
            spriteRenderer.flipX = false; 
        }
    }

    void FixedUpdate()
    {
        // 4. Applichiamo la spinta al motore fisico per farlo scivolare
        rb.MovePosition(rb.position + movimento * velocita * Time.fixedDeltaTime);
    }
}