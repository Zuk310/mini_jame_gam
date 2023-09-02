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


    private int meteorCount = 0;


    // Start is called before the first frame update
    void Start()
    {
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
                
                var newMeteor = Instantiate(meteorPrefab);
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
