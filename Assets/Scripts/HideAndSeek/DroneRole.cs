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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isIt)
        {
            Debug.Log("Found " + other.name);
            other.GetComponent<PhotonView>().RPC("Tag", RpcTarget.All, other.name);
            //PhotonView photonView = PhotonView.Get(this);
            //photonView.RPC("Tag", RpcTarget.All, other.name);
        }
    }

    void Tag(string name)
    {
        Debug.Log("Found " + name);
    }
}
