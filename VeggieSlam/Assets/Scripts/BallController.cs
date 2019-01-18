using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    private Rigidbody ball;
    [SerializeField]
    private float ballSpeedMultiplier = 10f;
    // Use this for initialization
    void Start()
    {
        ball = gameObject.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hitting");
        if (collision.collider.tag == "Weapon")
        {
            //ball.AddForce(new Vector3 (0f,10f,10f), ForceMode.Acceleration);
            //Get angle between paddle and ball
            Vector3 dir = collision.contacts[0].point - gameObject.transform.position;
            dir = -dir.normalized;
            //ball.velocity = dir * ballSpeedMultiplier;\
            dir.y = 0f;
            dir.x = 0f;
            ball.AddForce(dir * ballSpeedMultiplier, ForceMode.Impulse);
            ballSpeedMultiplier += 1f;
        }
    }
}
