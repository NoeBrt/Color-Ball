using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameColors;

public class playerController : MonoBehaviour
{
    [SerializeField] private float impulseSpeed = 5.0f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private GameObject deathEffect;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        setRandomColor();
        rb.simulated = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!rb.simulated)
            {
                rb.simulated = true;
            }
            rb.velocity = Vector2.up * impulseSpeed;
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
         //reolaod the scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        Debug.Log("Triggered");
        triggerObstacle(other);
        triggerColorChange(other);
    }

    void triggerObstacle(Collider2D other)
    {
        if (other.tag == "Obstacle")
        {
            if (other.GetComponent<SpriteRenderer>().color != spriteRenderer.color)
            {
                Debug.Log("Game Over");
                spriteRenderer.enabled = false;
                GetComponent<Collider2D>().enabled = false;
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                // Pause the game
            }
        }
    }

    void triggerColorChange(Collider2D other)
    {
        if (other.tag == "ColorChanger")
        {
            setRandomColor();
            Destroy(other.gameObject);
        }
    }

    void setRandomColor()
    {
        int randomIndex = Random.Range(0, GameColors.Colors.Count); // Adjusted to use the correct count
        spriteRenderer.color = GameColors.Colors[randomIndex];
    }
}
