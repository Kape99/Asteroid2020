using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    GameObject asteroid;
    Camera camera;
    GameObject target;

    private bool hasEntered = false;

    [SerializeField] private float speed = 20f;

    private float leftCostraint;
    private float rightCostraint;
    private float bottomCostraint;
    private float topCostraint;

    private float distanceZ;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        camera = Camera.main;
        asteroid = this.gameObject;
        distanceZ = Mathf.Abs(camera.transform.position.z + transform.position.z);
        Debug.Log("distance Z: " + distanceZ);
        leftCostraint = camera.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, distanceZ)).x;
        rightCostraint = camera.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, distanceZ)).x;
        bottomCostraint = camera.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, distanceZ)).y;
        topCostraint = camera.ScreenToWorldPoint(new Vector3(0.0f, Screen.height, distanceZ)).y;

        gameObject.GetComponent<Rigidbody2D>().AddForce((target.transform.position - this.transform.position) * speed);
    }

    // Update is called once per frame
   



    void FixedUpdate()
    {
        if (!hasEntered)
        {
            checkIfEntered();
            return;
        }
        if (asteroid.transform.position.x < leftCostraint)
        {
            asteroid.transform.position = new Vector3(rightCostraint, asteroid.transform.position.y, asteroid.transform.position.z);
        }
        if (asteroid.transform.position.x > rightCostraint)
        {
            asteroid.transform.position = new Vector3(leftCostraint, asteroid.transform.position.y, asteroid.transform.position.z);
        }
        if (asteroid.transform.position.y < bottomCostraint)
        {
            asteroid.transform.position = new Vector3(asteroid.transform.position.x, topCostraint, asteroid.transform.position.z);
        }
        if (asteroid.transform.position.y > topCostraint)
        {
            asteroid.transform.position = new Vector3(asteroid.transform.position.x, bottomCostraint, asteroid.transform.position.z);
        }

    }

    void checkIfEntered()
    {
        if (asteroid.transform.position.x > leftCostraint && asteroid.transform.position.x < rightCostraint && asteroid.transform.position.y > bottomCostraint && asteroid.transform.position.y < topCostraint)
        {
            hasEntered = true;
        }
    }
}
