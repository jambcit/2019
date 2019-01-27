using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneRole : MonoBehaviour
{

    public bool isIt;
    private int tagCount = 0;
    private bool isClient = false;
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
            other.gameObject.GetComponent<DroneRole>().isIt = true;
            ++tagCount;
            //Destroy(other.gameObject);
            //collision.gameObject.GetComponent<PhotonView>().RPC("Tag", RpcTarget.All, collision.gameObject.name);
            //PhotonView photonView = PhotonView.Get(this);
            //photonView.RPC("Tag", RpcTarget.All, other.name);
        }
    }

    void Tag(string name)
    {
        Debug.Log("Found " + name);
    }


    // Called by the HideAndSeekGameModeManager GameOver()
    void PostScore()
    {
        if (isClient)
        {

        }
    }
}
