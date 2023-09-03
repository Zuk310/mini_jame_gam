using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool _jumping = false;
    private Rigidbody2D _rigidbody;
    private GameManager gameManager;

   

    private void Walking(float dirX)
    {
        _rigidbody.velocity = new Vector2(dirX * 7f, _rigidbody.velocity.y);
    }

    private void Jumping()
    {
        // _rigidbody.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
        _rigidbody.AddForce(new Vector2(0, 20), ForceMode2D.Impulse);
        _jumping = false;
    }
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    
    private void Update()
    {
        if (Input.GetButtonDown("Jump") && Mathf.Abs(_rigidbody.velocity.y) < 0.001f)
        {
            _jumping = true;
        }
    }
    private void FixedUpdate()
    {
        float dirX = Input.GetAxis("Horizontal");
        Walking(dirX);
            
        if (!_jumping)
        {
            return;
        }

        Jumping();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "PlayerLeftScreenTrigger")
        {
            gameManager.PlayerLeftScreen();
        }
        else if (collision.gameObject.name == "PlayerRightScreenTrigger")
        {
            gameManager.PlayerReenterGame();
        }
        
    }
}
