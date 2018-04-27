using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidB : MonoBehaviour {

    public GameObject manager; // FlockB instance
    public Vector2 location = Vector2.zero;
    public Vector2 velocity;
    Vector2 goalPos = Vector2.zero;
    Vector2 currentForce;
    /*
    public float speed = 0.1f;
    public float rotationSpeed = 4.0f;
    Vector2 averageHeading;
    Vector2 averagePosition;
    float neighbourDistance = 3.0f;
    */

	// Use this for initialization
	void Start () {
        velocity = new Vector2(Random.Range(0.01f, 0.1f), Random.Range(0.01f, 0.1f));
        location = new Vector2(this.transform.position.x, this.transform.position.y);
        setRotation();
	}
	
    Vector2 seek(Vector2 target)
    {
        return (target - location);
    }

    void applyForce(Vector2 f)
    {
        Vector3 force = new Vector3(f.x, f.y, 0);
        if(force.magnitude > manager.GetComponent<FlockB>().maxForce)
        {
            force = force.normalized;
            force *= manager.GetComponent<FlockB>().maxForce;
        }
        this.GetComponent<Rigidbody2D>().AddForce(force);

        if(this.GetComponent<Rigidbody2D>().velocity.magnitude > manager.GetComponent<FlockB>().maxVelocity)
        {
            this.GetComponent<Rigidbody2D>().velocity = this.GetComponent<Rigidbody2D>().velocity.normalized;
            this.GetComponent<Rigidbody2D>().velocity *= manager.GetComponent<FlockB>().maxVelocity;
        }

        //re-orient the boid to the new resultant velocity
        setRotation();

        Debug.DrawRay(this.transform.position, velocity * 10, Color.black);
    }

    Vector2 align()
    {
        float neighbourDist = manager.GetComponent<FlockB>().neighbourDistance;
        Vector2 sum = Vector2.zero;
        int count = 0;
        foreach (GameObject other in manager.GetComponent<FlockB>().allBoids)
        {
            if (other == this.gameObject) continue;

            float d = Vector2.Distance(location, other.GetComponent<BoidB>().location);

            if (d < neighbourDist)
            {
                sum += other.GetComponent<BoidB>().velocity;
                count++;
            }
        }

        // check they're not counting the wrong boids too
        //Debug.Log(manager.name + " neighbour count: " + count); 
        // result: count isn't exceeding 29, so they're not

        if (count > 0)
        {
            sum /= count;
            Vector2 steer = sum - velocity;
            return steer;
        }

        return Vector2.zero;
    }

    Vector2 cohesion()
    {
        float neighbourDist = manager.GetComponent<FlockB>().neighbourDistance;
        Vector2 sum = Vector2.zero;
        int count = 0;
        foreach (GameObject other in manager.GetComponent<FlockB>().allBoids)
        {
            if (other == this.gameObject) continue;

            float d = Vector2.Distance(location, other.GetComponent<BoidB>().location);
            if (d < neighbourDist)
            {
                sum += other.GetComponent<BoidB>().location;
                count++;
            }
        }

        if (count > 0)
        {
            sum /= count;
            return seek(sum);
        }

        return Vector2.zero;
    }

    void flock()
    {
        location = this.transform.position;
        velocity = this.GetComponent<Rigidbody2D>().velocity;

        if(manager.GetComponent<FlockB>().obedient && Random.Range(0,50)<= 1)
        {
            Vector2 ali = align();
            Vector2 coh = cohesion();
            Vector2 gl;
            if (manager.GetComponent<FlockB>().seekGoal)
            {
                gl = seek(goalPos);
                currentForce = gl + ali + coh;
            }
            else
            {
                currentForce = ali + coh;
            }

            currentForce = currentForce.normalized;
        }

        // willful boid resists peer pressure sometimes
        if(manager.GetComponent<FlockB>().willful && Random.Range(0,50) <= 1)
        {
            if(Random.Range(0,50) < 1) // change direction
            {
                currentForce = new Vector2(
                    Random.Range(0.01f, 0.1f), Random.Range(0.01f, 0.1f));
            }
        }

        applyForce(currentForce);
    }

	// Update is called once per frame
	void Update () {
        /*
        transform.Translate(0, Time.deltaTime * speed, 0);
        ApplyRules();
        */
        flock();
        goalPos = FlockB.goalPos;
	}

    void setRotation()
    {
        // the sprite points up by default, so the angle from up to
        // the velocity vector is the angle from default rotation to
        // the rotation needed for the boid's velocity to be forward
        this.GetComponent<Transform>().rotation = Quaternion.Euler(
            0, 0, Vector2.Angle(Vector2.up, this.GetComponent<Rigidbody2D>().velocity));
    }

    /*
    void ApplyRules()
    {
        GameObject[] gos;
        gos = manager.allBoids;

        Vector2 vcentre = Vector2.zero;
        Vector2 vavoid = Vector2.zero;
        float gSpeed = 0.1f;

        Vector2 goalPos = FlockB.goalPos;

        float dist;

        int groupSize = 0;
        foreach (GameObject go in gos)
        {
            if(go != this.gameObject)
            {
                dist = Vector2.Distance(go.transform.position, this.transform.position);
                if(dist <= neighbourDistance)
                {
                    vcentre += (Vector2) go.transform.position;
                    groupSize++;

                    if(dist < 1.0f)
                    {
                        vavoid += (Vector2) (this.transform.position - go.transform.position);
                    }

                    BoidB anotherBoid = go.GetComponent<BoidB>();
                    gSpeed += anotherBoid.speed;
                }
            }
        }

        if(groupSize > 0)
        {
            vcentre = vcentre / groupSize + (goalPos - (Vector2) this.transform.position);
            speed = gSpeed / groupSize;

            Vector2 direction = (vcentre + vavoid) - (Vector2) transform.position;
            if(direction != Vector2.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                      Quaternion.LookRotation(direction),
                                                      rotationSpeed * Time.deltaTime);
                
            }
        }
    }
    */
}
