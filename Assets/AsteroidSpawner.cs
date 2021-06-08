using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    GameObject spaceShip;
    [SerializeField] GameObject asteroid;
    Camera camera;

    [SerializeField] private int throttle = 0;
    [SerializeField] private int health = 0;
    [SerializeField] private float accelleration = 10f;
    [SerializeField] private float rotation = 1f;
    [SerializeField] private const int MAX_THROTTLE = 5;

    private float distanceZ;
    [SerializeField] private float leftCameraCostraint;
    [SerializeField] private float rightCameraCostraint;
    [SerializeField] private float bottomCameraCostraint;
    [SerializeField] private float topCameraCostraint;
    [SerializeField] private const float BUFFER = 1.3f;

    [SerializeField] private float leftCostraint;
    [SerializeField] private float rightCostraint;
    [SerializeField] private float bottomCostraint;
    [SerializeField] private float topCostraint;

    float timer = 0.0f;
    GameObject asteroidClone;
    


    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        spaceShip = GameObject.FindGameObjectWithTag("Player");
        distanceZ = Mathf.Abs(camera.transform.position.z + transform.position.z);
        Debug.Log("distance Z: " + distanceZ);
        leftCameraCostraint = camera.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, distanceZ)).x * BUFFER;
        rightCameraCostraint = camera.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, distanceZ)).x * BUFFER;
        bottomCameraCostraint = camera.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, distanceZ)).y * BUFFER;
        topCameraCostraint = camera.ScreenToWorldPoint(new Vector3(0.0f, Screen.height, distanceZ)).y * BUFFER;

        leftCostraint = leftCameraCostraint * 3;
        rightCostraint = rightCameraCostraint * 3;
        bottomCostraint = bottomCameraCostraint * 3;
        topCostraint = topCameraCostraint * 3;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float xPos = 0;
        float yPos = 0;
        bool found = false;

        if (Input.GetKeyDown(KeyCode.P) || timer > 1)
        {
            while (!found)
            {
                xPos = Random.Range(leftCostraint, rightCostraint);
                yPos = Random.Range(bottomCostraint, topCostraint);
                if (xPos < leftCameraCostraint || xPos > rightCameraCostraint || yPos < bottomCameraCostraint || yPos > topCameraCostraint)
                {
                    found = true;
                }
            }
            found = false;
            timer = 0.0f;
            asteroidClone = Instantiate(asteroid, new Vector3(xPos, yPos, distanceZ), Quaternion.identity);
            Debug.Log(asteroidClone.transform);
        }
    }
}
