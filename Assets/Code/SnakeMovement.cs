using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeMovement : MonoBehaviour
{
    public float MoveSpeed = 5;
    public float SteerSpeed = 180;
    public float BodySpeed = 5;
    public int Gap = 15;
    public Vector3 AppleSpawnPosition;
    public Joystick SnakeJoystick;

    public GameObject BodyPrefab;
    public GameObject ApplePrefab;

    private List<GameObject> BodyParts = new List<GameObject>();
    private List<Vector3> PositionsHistory = new List<Vector3>();
     

    // Start is called before the first frame update
    void Start()
    {
        GrowSnake();
        
    }

    // Update is called once per frame
    void Update()
    {
        // move forward
        transform.position += transform.forward * MoveSpeed * Time.deltaTime;

        // steer
        //float steerDirection = Input.GetAxis("Horizontal");
        float steerDirection = SnakeJoystick.Direction.x;
        transform.Rotate(Vector3.up * steerDirection * SteerSpeed * Time.deltaTime);

        // store position history

        PositionsHistory.Insert(0, transform.position);

        // move body
        int index = 0;
        foreach (var body in BodyParts)
        {
            Vector3 point = PositionsHistory[Mathf.Min(index * Gap, PositionsHistory.Count - 1)];
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * BodySpeed * Time.deltaTime;
            body.transform.LookAt(point);
            index++;
        }
    }
    private void GrowSnake()
    {
        Vector3 BodySpawn = new Vector3(0, 5, 0);
        GameObject body = Instantiate(BodyPrefab, BodySpawn, Quaternion.identity);
        BodyParts.Add(body);

    }
    private void GameEnd()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Apple" || other.gameObject.name == "Apple(Clone)")   
        {
            GrowSnake();
            Destroy(other.gameObject);
            Vector3 SpawnPosition = new Vector3(Random.Range(-9f, 9f), -0.24f, Random.Range(-9f, 9f));
            Instantiate(ApplePrefab,SpawnPosition,Quaternion.identity);
            ScoreManager.scoreCount += 1;
        }
        if (other.gameObject.name == "Wall")
        {
            GameEnd();
        }

    }

    
}