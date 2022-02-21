using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveForce = 10f;
    
    [SerializeField]
    private float jumpForce = 11f;
    
    private float _movementX;

    private Rigidbody2D _myBody;
    private SpriteRenderer _sr;
    private Animator _anim;
    private string WALK_ANIMATION = "isWalking";
    
    private bool isGrounded = true;
    private string GROUND_TAG = "Ground";
    private string ENEMY_TAG = "Enemy";
    
    // Start is called before the first frame update
    void Start()
    {
        _myBody = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveKeyboard();
        AnimatePlayer();
        PlayerJump();
    }

    void PlayerMoveKeyboard()
    {
        this._movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(_movementX, 0f, 0f) * (Time.deltaTime * moveForce);
    }

    void AnimatePlayer()
    {
        if (_movementX > 0)
        {
            _anim.SetBool(WALK_ANIMATION, true);
            _sr.flipX = false;
        }
        else if (_movementX < 0)
        {
            _anim.SetBool(WALK_ANIMATION, true);
            _sr.flipX = true;
        }
        else
        {
            _anim.SetBool(WALK_ANIMATION, false);
        }
    }

    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;
            _myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(GROUND_TAG))
        {
            isGrounded = true;
        }

        if (col.gameObject.CompareTag(ENEMY_TAG))
        {
            Destroy(gameObject);
        }
    }
} // class
