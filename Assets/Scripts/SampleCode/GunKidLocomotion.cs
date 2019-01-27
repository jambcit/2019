using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunKidLocomotion : MonoBehaviour
{

    private Animator _gunAnimator;
    private const float timer = 5f;

    // Start is called before the first frame update
    void Start()
    {
        _gunAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gunAnimator == null) return;
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        Move(x, y);

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (_gunAnimator.GetBool("Crouching"))
            {
                _gunAnimator.SetBool("Crouching", false);
            }
            else
            {
                _gunAnimator.SetBool("Crouching", true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _gunAnimator.SetTrigger("GunJump");
        }

        if (Input.GetMouseButtonDown(0))
        {
            _gunAnimator.Play("GunShooting", -1, 0);
        }
    }

    private void Move(float x, float y)
    {
        _gunAnimator.SetFloat("VelX", x);
        _gunAnimator.SetFloat("VelY", y);
    }
    
}
