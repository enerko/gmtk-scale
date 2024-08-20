using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newSnakeScript : MonoBehaviour
{
    [SerializeField] private Transform[] respawnPoints;
    [SerializeField] private GameObject player;
    private int respawnIndex = 0;
    // Delay time
    private float delayBeforeRespawn = 1f;
    private HashSet<Collider2D> changedSpawnPoints = new HashSet<Collider2D>();
    // Default respawn face-direction is right
    private Vector3 respawnDirection = Vector3.right; 
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
        if(other.tag=="Player")
        {
            StartCoroutine(Death());
        }
    }
    IEnumerator Death()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(4).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        transform.GetChild(4).gameObject.SetActive(false);
        transform.GetChild(5).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        transform.GetChild(5).gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
    }
}
