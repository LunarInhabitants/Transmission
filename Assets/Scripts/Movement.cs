﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 6.0f;
    [SerializeField] private float _jumpSpeed = 8.0F;
    [SerializeField] private float _gravity = 20.0F;

    private Vector3 moveDirection;

    private CharacterController _characterController;
    private Camera _cameraGameObject;

	// Use this for initialization
	void Start ()
	{
	    _characterController = GetComponent<CharacterController>();
	    _cameraGameObject = GetComponentInChildren<Camera>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    if (true || _characterController.isGrounded)
	    {
            float horizontalAxis = Input.GetAxis("Horizontal");
            float verticalAxis = Input.GetAxis("Vertical");

            Vector3 vec = new Vector3(horizontalAxis, 0, verticalAxis);

            Vector3 forward = _cameraGameObject.transform.forward;
            Vector3 right = _cameraGameObject.transform.right;

            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

	        moveDirection = forward * verticalAxis + right * horizontalAxis;


            //moveDirection = transform.forward * (vec * _moveSpeed * Time.deltaTime);
	        if (Input.GetButton("Jump"))
	            moveDirection.y = _jumpSpeed;
	    }
	    moveDirection.y -= _gravity * Time.fixedDeltaTime;
        Debug.Log((moveDirection));
	    _characterController.Move((moveDirection * _moveSpeed) * Time.fixedDeltaTime);
	    //_characterController.Move(moveDirection);
    }
}