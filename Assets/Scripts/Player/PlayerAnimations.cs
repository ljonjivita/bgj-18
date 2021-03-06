﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour {

    Rigidbody2D rb;
    Animator animator;

    public float forwardZPos = 1;
    public float backwardZPos = -1;

    public float turnThreshold = -0.25f;

    public Transform target;
    public bool isPlayer;
    public bool lookingUp;
    public Vector3 lookDir;

    // Use this for initialization
    void Start () {
        rb = transform.parent.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        //rb
        animator.SetFloat("velocity", rb.velocity.magnitude);

        Vector3 lookPos;

        if(isPlayer)
        {
            lookPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else
        {
            lookPos = target.position;
        }

        lookDir = (lookPos - rb.transform.position).normalized;
        lookingUp = Vector3.Dot(lookDir, Vector3.down) < turnThreshold;
        animator.SetBool("isLookingUp", lookingUp);

        Vector3 pos = transform.position;
        if(lookingUp)
        {
            pos.z = backwardZPos;
        }
        else
        {
            pos.z = forwardZPos;
        }
        transform.position = pos;

        if((rb.velocity.x > 0 && transform.localScale.x > 0) ||
            (rb.velocity.x < 0 && transform.localScale.x < 0))
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
