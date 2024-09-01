using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameColors;
using System.Linq;
public class playerController : MonoBehaviour
{
    [SerializeField]
    private float impulseSpeed = 5.0f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Transform FinishLine;

    [SerializeField]
    private Vector3 playerPosition;

    [SerializeField]
    private GameObject DeathEffect;

    [SerializeField]
    private GameObject collectibleEffect;

    [SerializeField]
    private float boundary = -10.0f;

    [SerializeField]
    private float finishLineOffset = 3.0f;

    //  [SerializeField] private AudioClip jumpSound; very annoying
    int collectiblesCount = 0;

    void Start()
    {
        transform.position = playerPosition;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        setRandomColor();
        rb.simulated = false;
        FinishLine = GameObject.Find("Finish").transform;
    }

    void Update()
    {
        if (
            Input.GetMouseButtonDown(0)
            || Input.GetKeyDown(KeyCode.Space)
            || Input.GetKeyDown(KeyCode.UpArrow)
        )
        {
            if (!rb.simulated)
            {
                rb.simulated = true;
            }
            //  GetComponent<AudioSource>().PlayOneShot(jumpSound); very annoying
            rb.velocity = Vector2.up * impulseSpeed;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            //reolaod the scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
            );
        }
        if (
            FinishLine.position.y + finishLineOffset < transform.position.y
            && FinishLine.gameObject.activeSelf
        )
        {
            Finish();
        }
        if (transform.position.y < boundary)
        {
            Death();
        }
    }

    void Finish()
    {
        rb.simulated = false;
        this.enabled = false;
        PlayerActions.Finish?.Invoke();
    }

    void Death()
    {
        Debug.Log("Game Over");
        Instantiate(DeathEffect, transform.position, Quaternion.identity);
        rb.simulated = false;
        collectiblesCount = 0;
        transform.position = playerPosition;
        PlayerActions.Death?.Invoke();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered");
        triggerObstacle(other);
        triggerColorChange(other);
        triggerCollectible(other);
    }

    void triggerObstacle(Collider2D other)
    {
        if (other.tag == "Obstacle")
        {
            Debug.Log(other.GetComponent<SpriteRenderer>().color + " " + spriteRenderer.color);
            if (other.GetComponent<SpriteRenderer>().color != spriteRenderer.color)
            {
                Death();
            }
        }
    }

    void triggerColorChange(Collider2D other)
    {
        if (other.tag == "ColorChanger")
        {
            setRandomColor();
            Destroy(other.gameObject);
            PlayerActions.ChangeColor?.Invoke();
        }
    }

    void setRandomColor()
    {
        Color currentColor = spriteRenderer.color;
        List<Color> availableColors = GameColors.Colors.Where(color => color != currentColor).ToList();
        int randomIndex = Random.Range(0, availableColors.Count);
        spriteRenderer.color = availableColors[randomIndex];
    }

    void triggerCollectible(Collider2D other)
    {
        if (other.tag == "Collectible")
        {
            Debug.Log("Collectible");
            collectiblesCount++;
            PlayerActions.Collect?.Invoke(collectiblesCount);
            Instantiate(collectibleEffect, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
    }
}
