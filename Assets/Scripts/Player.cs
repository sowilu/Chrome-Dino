using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public float jumpHeight = 2;

    [Header("Animation")]
    public Sprite standing;
    public float standingHeight = 0.7f;
    public Sprite bendOver;
    public float bendOverHeight = 0.4f;

    [Header("UI")]
    public TextMeshProUGUI scoreTxt;
    public GameObject gameOverScreen;

    [Header("Sounds")]
    public AudioClip death;

    bool grounded;
    Rigidbody2D rb;
    CapsuleCollider2D collider;
    SpriteRenderer spriteRenderer;
    AudioSource audioSource;
    

    private void Start()
    {
        //get component that is on our gameObject
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        scoreTxt.text = Mathf.Round(Time.time).ToString();

        if((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && grounded)
        {
            var force = Mathf.Sqrt(jumpHeight * 2 * -Physics2D.gravity.y);
            rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            audioSource.Play();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            spriteRenderer.sprite = bendOver;
            collider.size = new Vector2(collider.size.x, bendOverHeight);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            spriteRenderer.sprite = standing;
            collider.size = new Vector2(collider.size.x, standingHeight);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            audioSource.clip = death;
            audioSource.Play();

            gameOverScreen.SetActive(true);
            Spawner.stop = true;
            Destroy(this);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }
}
