using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private PhotonView photonView;
    [SerializeField] private Vector3 selfPosition;
    [SerializeField] private GameObject paddle;
    [SerializeField] private LineRenderer teleportLine;
    [SerializeField] private Transform startLinePos;


    private float range = 10f;
    private RaycastHit hit;
    private List<Vector3> positions;
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

        if (GvrControllerInput.AppButton)
        {
            teleportLine.enabled = true;
            positions = curvedRaycast(startLinePos.transform.position, startLinePos.transform.forward, 2, 30);
            teleportLine.positionCount = positions.Count + 1;
            teleportLine.SetPosition(0, startLinePos.transform.position);

            for (int i = 0; i < positions.Count; ++i)
            {
                if (positions[i] != null)
                    teleportLine.SetPosition(i + 1, positions[i]);
            }
        }
        if (GvrControllerInput.AppButtonUp)
        {
            Vector3 currPos = gameObject.transform.position;
            Vector3 teleportPos = positions[positions.Count - 1];
            teleportPos.y = currPos.y;
            teleportLine.enabled = false;
            gameObject.transform.position = teleportPos;
        }
    }

    private List<Vector3> curvedRaycast(Vector3 start, Vector3 direction, int velocity, int numberOfIterations)
    {
        RaycastHit hit;
        List<Vector3> positions = new List<Vector3>();
        Ray ray = new Ray(start, direction);
        //shootLine.SetPosition(0, ray.origin);
        int count = 0;
        for (int i = 0; i < numberOfIterations; ++i)
        {
            //If it hits, return
            if (Physics.Raycast(ray, out hit, 1f))
            {
                return positions;
            }

            Debug.DrawRay(ray.origin, ray.direction, Color.blue);
            ray = new Ray(ray.origin + ray.direction, ray.direction + (Physics.gravity / numberOfIterations / velocity));
            positions.Add(ray.origin);
        }
        Debug.Log(positions);
        return positions;

    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
            stream.SendNext(transform.position);
        else
            selfPosition = (Vector3)stream.ReceiveNext();
    }
}
