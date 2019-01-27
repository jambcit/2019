﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField]
    Transform target;
    
    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            transform.LookAt(target);
        }
        else
        {
            transform.LookAt(Camera.main.transform);
        }
    }
}