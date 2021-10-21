using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveForce = 10f;

    [SerializeField]
    private float jumpForce = 11f;

    private float movementX;

    private Rigidbody2D myBody;

    private Animator anim;

    private SpriteRenderer sr;
    private string WALK_ANIMATION = "Walk";
    private string GROUND_TAG = "Ground";

    private bool isGrounded = true;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }


    // Start is called before the first frame update
    void Start()
    {
        //sr.flipX = true;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveKeyboard();
        AnimatePlayer();
    }

    void FixedUpdate()
    {
        PlayerJump();
    }


    void PlayerMoveKeyboard()
    {
        movementX = Input.GetAxisRaw ("Horizontal");

        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * moveForce;

    }

    void AnimatePlayer()
    {
        if (movementX > 0 )
            // moving to the right
        {
            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = false;
        }
        else if (movementX < 0)
        {
            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = true;
        }
        else {
            anim.SetBool(WALK_ANIMATION, false);
        }
    }

    void PlayerJump() {
        // predefined by Unity to use keys for each platform
        // if on computer, space bar
        // if on console, maybe X or whatever button assigned
        // if on mobile
        // so it is default button platform neutral
        // button down

        if (Input.GetButtonDown("Jump") && isGrounded) {
            isGrounded = false;

            myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        // button released
        //if (Input.GetButtonUp("Jump"))

        // down and released
        //if (Input.GetButton("Jump"))

         Debug.Log("Jump pressed");

        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag(GROUND_TAG)){
            isGrounded = true;
            Debug.Log("We landed on ground");
        }  
    }
}
