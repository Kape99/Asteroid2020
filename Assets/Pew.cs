using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pew : MonoBehaviour
{

    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 10)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.LogWarning("hit:" + collision.tag);
        if (collision.tag == "Asteroid")
        {

            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }

        
    }
}
