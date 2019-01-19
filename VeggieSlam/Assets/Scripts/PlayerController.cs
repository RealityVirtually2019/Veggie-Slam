using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private PhotonView photonView;
    [SerializeField] private Vector3 selfPosition;

    private void Start()
    {
        if (photonView.isMine)
        {
            gameObject.GetComponentInChildren<Camera>().enabled = true;
            selfPosition = transform.position;
            Debug.Log("view is mine");
        }
    }

    void Update()
    {
        if (photonView.isMine)
            selfPosition = transform.position;
        else
            transform.position = selfPosition;
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
            stream.SendNext(transform.position);
        else
            selfPosition = (Vector3)stream.ReceiveNext();
    }
}
