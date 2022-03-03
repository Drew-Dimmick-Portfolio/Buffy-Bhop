using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    //public PlayerController player;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * PlayerController.platformSpeed * Time.deltaTime;

        //speed += Time.deltaTime;
    }
}
