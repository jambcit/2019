using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandboxObjectInteraction : MonoBehaviour
{

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
        if(other.name == "Character")
        {
            MeshRenderer myMeshRenderer = GetComponent<MeshRenderer>();
            myMeshRenderer.material.color = Color.red;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Character")
        {
            MeshRenderer myMeshRenderer = GetComponent<MeshRenderer>();
            myMeshRenderer.material.color = Color.white;
        }
    }

    public void StartInteraction()
    {
        Debug.Log(name + "Start Interact!");
    }
}
