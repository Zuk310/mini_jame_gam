using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public GameObject meteorPrefab;
    public GameObject player;
    public const int maxMeteors = 10;
    private Sprite[] meteorSprites;


    private int meteorCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        // Load all sprites named "meteor_1" to "meteor_6" from the Resources folder
        meteorSprites = new Sprite[6];
        for (int i = 1; i <= 6; i++)
        {
            meteorSprites[i - 1] = Resources.Load<Sprite>("Meteor_" + i);
        }
        StartCoroutine (SpawnMeteors());

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
            yield return new WaitForSeconds(0.5f);
        }
    }


    public void meteorDestroyed(MeteorBehaviours meteor)
    {
        meteorCount--;
    }
}
