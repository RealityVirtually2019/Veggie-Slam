using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitController : MonoBehaviour {

    [SerializeField] private float forceMultiplier = 1f/9.8f;
    public FruitPlayerController playerController;
    // Use this for initialization
    void Start () {
        //playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<FruitPlayerController>();
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.back / 100000f, ForceMode.Impulse);
        StartCoroutine("Destroyed");
	}

    IEnumerator Destroyed ()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Weapon")
        {
            gameObject.GetComponentInChildren<ParticleSystem>().Play();
            FruitPlayerController.Score++;
        }
        Destroy(gameObject);
    }
}
