using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public float maxTime = 1;
    private float startingMaxTime;
    private float timer = 0;
    private float startingPlatformSpeed;
    public GameObject platform;
    public float leftSpawn;
    public float rightSpawn;

    // Start is called before the first frame update
    void Start()
    {
        startingPlatformSpeed = PlayerController.platformSpeed;
        startingMaxTime = maxTime;

        GameObject newPlatform = Instantiate(platform);
        newPlatform.transform.position = transform.position + new Vector3(Random.Range(leftSpawn, rightSpawn), 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        maxTime = (startingMaxTime * startingPlatformSpeed) / PlayerController.platformSpeed;

        if (timer > maxTime)
        {
            GameObject newPlatform = Instantiate(platform);
            newPlatform.transform.position = transform.position + new Vector3(Random.Range(leftSpawn, rightSpawn), 0, 0);
            Destroy(newPlatform, 15);
            timer = 0;
        }

        timer += Time.deltaTime;
    }
}
