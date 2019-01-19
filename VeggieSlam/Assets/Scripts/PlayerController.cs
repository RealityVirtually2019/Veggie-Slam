using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private PhotonView photonView;
    [SerializeField] private Vector3 selfPosition;
    [SerializeField] private GameObject paddle;
    //[SerializeField] private GameObject paddle;

    //private Vector3 paddlePosition;

    private void Start()
    {
        if (photonView.isMine)
        {
            gameObject.GetComponentInChildren<Camera>().enabled = true;
            selfPosition = transform.position;
            paddle.SetActive(true);
            //paddlePosition = paddle.transform.position;
            Debug.Log("view is mine");
        }
        else
        {
            paddle.SetActive(false);
            gameObject.GetComponentInChildren<Camera>().enabled = false;
        }
    }

    void Update()
    {
        //Writing
        if (photonView.isMine)
        {
            selfPosition = transform.position;
            //paddlePosition = paddle.transform.position;
        }
        //Reading
        else
        {
            transform.position = selfPosition;
            //paddle.transform.position = paddlePosition;
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
