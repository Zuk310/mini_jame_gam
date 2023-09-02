using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    
    public GameObject meteorPrefab;
    public GameObject player;
    public const int maxMeteors = 10;

    public float cameraXHouseScreen = -13.63f;

    private Sprite[] meteorSprites;

    private Camera cam;
    private BoxCollider2D LeftScreenTrigger;

    private int meteorCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        LeftScreenTrigger = GetComponentInChildren<BoxCollider2D>();

        // Load all sprites named "meteor_1" to "meteor_6" from the Resources folder
        meteorSprites = new Sprite[6];
        for (int i = 1; i <= 6; i++)
        {
            meteorSprites[i - 1] = Resources.Load<Sprite>("Meteor_" + i);
        }
        StartCoroutine (SpawnMeteors());
        // StartCoroutine(PlayerLeftScreen()); // Uncomment to watch camera move to left


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnMeteors()
    {
        while (true)
        {
            if (meteorCount < maxMeteors)
            {
                // Instantiate the new Meteor object from the prefab
                var newMeteor = Instantiate(meteorPrefab);

                // Get the SpriteRenderer component from the new instantiated object
                SpriteRenderer spriteRenderer = newMeteor.GetComponent<SpriteRenderer>();

                // Get a random sprite from the loaded sprites
                Sprite randomSprite = meteorSprites[Random.Range(0, meteorSprites.Length)];

                // Assign the random sprite to the instantiated object
                spriteRenderer.sprite = randomSprite;  // This line should refer to spriteRenderer, not meteorPrefab

                // Optionally, set the sorting order if you want to bring it to the front
                spriteRenderer.sortingOrder = 1;

                // Increment meteorCount
                meteorCount++;
            }
            yield return new WaitForSeconds(Random.value);
        }
    }


    public void MeteorDestroyed(MeteorBehaviours meteor)
    {
        meteorCount--;
    }

    private IEnumerator PlayerLeftScreen()
    {
        var i = 0f;
        while (i < 1 )
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(cameraXHouseScreen, 0, -10), i);
            i += 0.005f * Time.deltaTime;
            yield return null;
        }

    }
}
