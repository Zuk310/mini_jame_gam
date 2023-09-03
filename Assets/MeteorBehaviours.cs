using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeteorBehaviours : MonoBehaviour
{

    private int metalType;

    public const float meteorSpawnLocationLeftBound = -7.76f;
    public const float meteorSpawnLocationRightBound = 7.76f;
    public const float meteorSpawnLocationHeight = 6.61f;

    private GameManager gameManager;
    private Rigidbody2D rb;
    private GameObject player;
    public float minSize = 0.1f;
    public float maxSize = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        float randomMeteorLocation = Random.Range(meteorSpawnLocationLeftBound, meteorSpawnLocationRightBound);
        var size = Random.Range(minSize, maxSize);
        rb.transform.position = new Vector3(randomMeteorLocation, meteorSpawnLocationHeight, 0);
        rb.velocity = new Vector2(0f, 0f);
        this.transform.localScale = new Vector2(size, size);
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-2, 2), Random.Range(0, -1)), ForceMode2D.Impulse);
    }

    void changeDirection()
    {
        // rb.velocity = new Vector2(- rb.velocity.x,rb.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null) {
            if (collision.gameObject.tag != "Meteor" )
            {
                gameManager.MeteorDestroyed(this);
                Destroy(gameObject);
            }
            else
            {
                changeDirection();
            }
        }
    }
}
