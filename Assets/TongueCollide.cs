using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueCollide : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Path")
        {
            player.GetComponent<Movement>().foundPath();
        }
    }
}
