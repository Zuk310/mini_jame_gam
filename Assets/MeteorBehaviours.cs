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

    public GameManager gameManager;

    public float minSize = 3f;
    public float maxSize = 5f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        float randomMeteorLocation = Random.Range(meteorSpawnLocationLeftBound, meteorSpawnLocationRightBound);
        var size = Random.Range(minSize, maxSize);
        this.transform.position = new Vector3(randomMeteorLocation, meteorSpawnLocationHeight, 0);
        this.transform.localScale = new Vector2(size, size);
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-3, 3), Random.Range(0, -5)), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null) {
            if (collision.gameObject.name == "Player" || collision.gameObject.name == "Ground")
            {
                gameManager.MeteorDestroyed(this);
                Destroy(gameObject);
            }
        }
    }
}
