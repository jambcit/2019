using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidLocomotion : MonoBehaviour {

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_animator == null) return;
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        Move(x, y);

        if(Input.GetKeyDown(KeyCode.E))
        {
            _animator.SetTrigger("Pickup");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            _animator.SetTrigger("Punch");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (_animator.GetBool("Crouching"))
            {
                _animator.SetBool("Crouching", false);
            }
            else
            {
                _animator.SetBool("Crouching", true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger("Jump");
        }
    }

    private void Move(float x, float y)
    {
        _animator.SetFloat("VelX", x);
        _animator.SetFloat("VelY", y);
    }
}
