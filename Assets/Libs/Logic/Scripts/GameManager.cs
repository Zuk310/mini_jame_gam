using System.Collections;
using UnityEngine;

// Use Unity's Random instead of System's
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    // Public variables
    public GameObject meteorPrefab;
    public GameObject player;
    public const int maxMeteors = 20;
    public float cameraXHouseScreen = -13.63f;

    // Private variables
    private Sprite[] meteorSprites;
    private Camera cam;
    private int meteorCount = 0;
    private bool cameraOnHouse = false;
    private Vector3 leftCameraLocation;
    private Vector3 rightCameraLocation;
    private Coroutine cameraMovment;

    void Start()
    {
        cam = Camera.main;

        // Initialize camera locations
        leftCameraLocation = new Vector3(cameraXHouseScreen, 0, -10);
        rightCameraLocation = cam.transform.position;

        // Load meteor sprites from Resources folder
        meteorSprites = new Sprite[6];
        for (int i = 1; i <= 6; i++)
        {
            meteorSprites[i - 1] = Resources.Load<Sprite>("Meteor_" + i);
        }

        StartCoroutine(SpawnMeteors());
    }

    void Update() { } // If Update is not required, it can be removed

    // Coroutine to spawn meteors
    IEnumerator SpawnMeteors()
    {
        while (true)
        {
            if (meteorCount < maxMeteors)
            {
                GameObject newMeteor = Instantiate(meteorPrefab);
                SpriteRenderer spriteRenderer = newMeteor.GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = meteorSprites[Random.Range(0, meteorSprites.Length)];
                spriteRenderer.sortingOrder = 1; // Bring to the front
                meteorCount++;
            }
            yield return new WaitForSeconds(Random.value);
        }
    }

    // Handle the meteor destroyed event
    public void MeteorDestroyed(MeteorBehaviours meteor)
    {
        meteorCount--;
    }

    // Handle player exiting the screen to the left
    public void PlayerLeftScreen()
    {
        if (!cameraOnHouse)
        {
            if (cameraMovment != null)
            {
                StopCoroutine(cameraMovment);
            }
            cameraMovment = StartCoroutine(MoveCamera(leftCameraLocation));
            cameraOnHouse = true;
        }
    }

    // Handle player re-entering the game
    public void PlayerReenterGame()
    {
        if (cameraOnHouse)
        {
            if (cameraMovment != null)
            {
                StopCoroutine(cameraMovment);
            }
            cameraMovment = StartCoroutine(MoveCamera(rightCameraLocation));
            cameraOnHouse = false;
        }
    }

    // Coroutine to move the camera
    private IEnumerator MoveCamera(Vector3 targetPosition)
    {
        float i = 0f;
        Vector3 curCameraPosition = cam.transform.position;
        while (i < 1)
        {
            cam.transform.position = Vector3.Lerp(curCameraPosition, targetPosition, i);
            i += 0.5f * Time.deltaTime;
            yield return null;
        }
    }
}
