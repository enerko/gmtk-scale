using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animateEnd : MonoBehaviour
{
    public GameObject player, animation;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(end());
    }
    IEnumerator end()
    {
        yield return new WaitForSeconds(3);
        player.SetActive(true);
        animation.SetActive(false);
    }
}
