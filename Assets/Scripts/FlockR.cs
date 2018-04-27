using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockR : MonoBehaviour {

    public GameObject boidPrefab;
    public int numBoids = 30;
    public GameObject[] allBoids;
    public Vector3 range = new Vector3(5, 5, 5);
    public static Vector3 goalPos = Vector3.zero;

    public bool seekGoal = true;
    public bool mouseGoal = true;
    public bool obedient = true;
    public bool willful = false;

    [Range(0, 200)]
    public int neighbourDistance = 50;

    [Range(0, 2)]
    public float maxForce = 0.5f;

    [Range(0, 5)]
    public float maxVelocity = 2.0f;

    // chance to change random target each frame is 1 / this number
    [Range(1, 600)]
    public float changeInterval = 100f;

    private void OnDrawGizmosSelected()
    {
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(this.transform.position, range * 2);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(this.transform.position, 0.2f);
        }
    }

	// Use this for initialization
	void Start () {
        goalPos = randomPos();

        allBoids = new GameObject[numBoids];

        for (int i = 0; i < numBoids; i++)
        {
            Vector3 pos = randomPos();
            allBoids[i] = Instantiate(boidPrefab, pos, Quaternion.identity);
            allBoids[i].GetComponent<BoidR>().manager = this.gameObject;
        }
	}

    // Update is called once per frame
    private void Update()
    {
        updateGoal();
    }

    // change goal position
    void updateGoal () {
        // update the goal position
        if (mouseGoal)
        {
            // follow the mouse
            goalPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else
        {
            // follow a random target that changes after a random delay
            if(Random.Range(0,changeInterval) < 1)
            {
                goalPos = randomPos();
            }
        }

        Debug.Log(this.gameObject.name + " goalPos: " + goalPos + " mouse?" + mouseGoal);
	}

    // choose a random position inside the range
    Vector3 randomPos()
    {
        return new Vector3(Random.Range(-range.x, range.x),
                           Random.Range(-range.y, range.y),
                           0);
    }
}
