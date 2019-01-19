using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private PhotonView photonView;
    private Rigidbody ball;
    private Vector3 oldVel;
    [SerializeField]
    private float ballSpeedMultiplier = 10f;
    [SerializeField] private Vector3 selfPosition;
    // Use this for initialization
    void Start()
    {
        ball = gameObject.GetComponent<Rigidbody>();
        StartCoroutine(ChangeRotation());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        oldVel = ball.velocity;
        Debug.Log("Recorded Velocity Magnitude: " + oldVel.magnitude);
    }

    private IEnumerator ChangeRotation()
    {
        while(true)
        {
            ball.AddTorque(new Vector3(10 * UnityEngine.Random.Range(-3f, 3f), UnityEngine.Random.Range(-3f, 3f), UnityEngine.Random.Range(-3f, 3f)), ForceMode.VelocityChange);
            Debug.Log("Random ball rotation applied");
            yield return new WaitForSeconds(1);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
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
        if (collision.collider.tag == "Wall")
        //Bumper "Creep" effect. (Ball will bounce off the walls and slowly gain speed)
        //Current Top Speed before clipping: ~105ish
        {
           ContactPoint cp = collision.contacts[0];
         // calculate with addition of normal vector
         // myRigidbody.velocity = oldVel + cp.normal*2.0f*oldVel.magnitude;
         
         // calculate with Vector3.Reflect
         ball.velocity = Vector3.Reflect(oldVel,cp.normal);
         
         // bumper effect to speed up ball
         ball.velocity += cp.normal*1.0f;
        }
        
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
            stream.SendNext(transform.position);
        else
            selfPosition = (Vector3)stream.ReceiveNext();
    }

}
