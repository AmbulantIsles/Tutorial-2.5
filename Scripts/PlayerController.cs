using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	private Rigidbody2D rb2d;
	public float speed;
    public float jumpForce;
    public Text countText;
    public Text winText;
    private int count;
    public Text livesText;
    public Text loseText;
    public GameObject spawnPoint;
    private int lives;
    private bool grounded;
    public Camera gameCamera;

    Animator anim;
    private SpriteRenderer playerSpriteRenderer;

    void Start()
    {
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        lives = 3;
        winText.text = "";
        loseText.text = "";
        SetCountText();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            grounded = true;
            anim.SetInteger("State", 0);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {

            grounded = true;
            anim.SetInteger("State", 0);
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                grounded = false;
                anim.SetInteger("State", 2);
                rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow) & (collision.gameObject.tag == "Ground"))
        {
            grounded = true;
            anim.SetInteger("State", 1);
            playerSpriteRenderer.flipX = true;
        }

        if (Input.GetKey(KeyCode.RightArrow) & (collision.gameObject.tag == "Ground"))
        {
            grounded = true;
            anim.SetInteger("State", 1);
            playerSpriteRenderer.flipX = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
            anim.SetInteger("State", 2);
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
		Vector2 movement = new Vector2(moveHorizontal, 0);
		rb2d.AddForce(speed * movement);
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private void LateUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow) & (grounded == true))
        {
            anim.SetInteger("State", 1);
            playerSpriteRenderer.flipX = true;
        }

        if (Input.GetKey(KeyCode.RightArrow) & (grounded == true))
        {
            anim.SetInteger("State", 1);
            playerSpriteRenderer.flipX = false;
        }

        if (grounded == false)
        {
            anim.SetInteger("State", 2);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            lives = lives - 1;
            SetLivesText();
        }

    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count == 4)
        {
            gameCamera.transform.position = new Vector3 (0, -12, -1);
            gameObject.transform.position = new Vector2 (0, -12);
            lives = 3;
        }
        if (count >= 8)
        {
            winText.text = "You Win!";
        }
    }

    // sets the life counter and lose state
    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
        if (lives <= 0)
        {
            gameObject.SetActive(false);
            loseText.text = "You Lose";
        }
    }
}
