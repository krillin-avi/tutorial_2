using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text lives;
    private int scoreValue = 0;
    public int livesRemaining = 3;
    public Text gameMessage;
    // Audio Variables
    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    // Animation Variable
    Animator anim;
    // Flip Player Variable
    private bool facingRight = true;
   

    // Start is called before the first frame update
    void Start()
    {
        // Call Ridgid Body
        rd2d = GetComponent<Rigidbody2D>();

        // Set Score and Life Text
        score.text = scoreValue.ToString();
        lives.text = livesRemaining.ToString();
        gameMessage.text = " ";

        // Start Background Music
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;

        // Animation Start
        anim = GetComponent<Animator>();

    }

    void Update()
    {  

    }

    void LateUpdate()
    {
        // Movement Controlls -- This can be a Switch Statement
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 2);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 2);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetInteger("State", 3);
        }
         
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        // Call Player Flip Method
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
            else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);

            if (scoreValue == 4)
            {
                transform.position = new Vector2(33.86f, 0.2f);
                livesRemaining = 3;
                lives.text = livesRemaining.ToString();
            }

            if (scoreValue == 8) 
            {
                gameMessage.text = "You Win \n Created By: Christian Esdaille";
                anim.SetInteger("State", 0);
                musicSource.Stop();
                musicSource.clip = musicClipTwo;
                musicSource.Play();
                musicSource.loop = false;
                Destroy(this);
            }
        }

        if (collision.collider.tag == "Enemy")
        {
            livesRemaining -= 1;
            lives.text = livesRemaining.ToString();
            Destroy(collision.collider.gameObject);

            if (livesRemaining == 0) 
            {
                gameMessage.text = "You Lose \n Try Again";
                anim.SetInteger("State", 0);
                Destroy(this);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            // Checks if the Jump State is Active, and Sets to idle if active 
            int noGroundJump = anim.GetInteger("State");
            if (noGroundJump == 3)
            {
                anim.SetInteger("State", 0);
            }

            if (Input.GetKey(KeyCode.W))
            {
                //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors.  You can also create a public variable for it and then edit it in the inspector.
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse); 
            }            
        }
        
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

}