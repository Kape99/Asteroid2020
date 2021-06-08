using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    GameObject spaceShip;
    Camera camera;
    [SerializeField] GameObject pew;
    GameObject pewClone;


    [SerializeField] private int throttle = 0;
    [SerializeField] private int health = 0;
    [SerializeField] private float accelleration = .5f;
    [SerializeField] private float rotation = 2f;
    [SerializeField] private float pewSpeed = 300f;
    const int MAX_THROTTLE = 5;


    private float leftCostraint;
    private float rightCostraint;
    private float bottomCostraint;
    private float topCostraint;
    
    private float distanceZ;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        spaceShip = this.gameObject;
        distanceZ = Mathf.Abs(camera.transform.position.z + transform.position.z);
        Debug.Log("distance Z: " + distanceZ);
        leftCostraint = camera.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, distanceZ)).x;
        rightCostraint = camera.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, distanceZ)).x;
        bottomCostraint = camera.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, distanceZ)).y;
        topCostraint = camera.ScreenToWorldPoint(new Vector3(0.0f, Screen.height, distanceZ)).y;


    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && throttle < MAX_THROTTLE)
        {
            throttle++;
            Debug.Log("increase throttle");
        }
        if (Input.GetKeyDown(KeyCode.S) && throttle > 0)
        {
            throttle--;
            Debug.Log("decrese throttle");
        }
        if (Input.GetKey(KeyCode.A))
        {
            spaceShip.transform.Rotate(Vector3.forward, rotation);
            Debug.Log("left rotation");
        }
        if (Input.GetKey(KeyCode.D))
        {
            spaceShip.transform.Rotate(Vector3.forward, -rotation);
            Debug.Log("right rotation");
        }
        if (Input.GetKey(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pewClone = Instantiate(pew, spaceShip.transform.position, spaceShip.transform.rotation);
            pewClone.GetComponent<Rigidbody2D>().AddForce(pewClone.transform.up * pewSpeed);
            Debug.Log("BOOM");
        }
        Debug.Log(spaceShip.transform.up);
        spaceShip.GetComponent<Rigidbody2D>().AddForce(spaceShip.transform.up * throttle * accelleration);
    }
    
    
    
    void FixedUpdate()
    {
        if (spaceShip.transform.position.x < leftCostraint)
        {
            spaceShip.transform.position = new Vector3(rightCostraint, spaceShip.transform.position.y, spaceShip.transform.position.z);
        }
        if (spaceShip.transform.position.x > rightCostraint)
        {
            spaceShip.transform.position = new Vector3(leftCostraint, spaceShip.transform.position.y, spaceShip.transform.position.z);
        }
        if (spaceShip.transform.position.y < bottomCostraint)
        {
            spaceShip.transform.position = new Vector3(spaceShip.transform.position.x, topCostraint, spaceShip.transform.position.z);
        }
        if (spaceShip.transform.position.y > topCostraint)
        {
            spaceShip.transform.position = new Vector3(spaceShip.transform.position.x, bottomCostraint, spaceShip.transform.position.z);
        }

    }
}
