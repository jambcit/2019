using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneRole : MonoBehaviour
{

    public bool isIt;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isIt)
        {
            Debug.Log("Found " + other.gameObject.name);
            Destroy(other.gameObject);
            //collision.gameObject.GetComponent<PhotonView>().RPC("Tag", RpcTarget.All, collision.gameObject.name);
            //PhotonView photonView = PhotonView.Get(this);
            //photonView.RPC("Tag", RpcTarget.All, other.name);
        }
    }

    void Tag(string name)
    {
        Debug.Log("Found " + name);
    }
}
