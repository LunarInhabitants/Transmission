using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float _beamDamage = 1.0f;
    [SerializeField] private Vector3 _beamColour;

    private GameObject _cameraGameObject;

	// Use this for initialization
	void Start ()
	{
	    _cameraGameObject = transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetButton("Fire"))
	    {
            Fire();
	    }
	}

    private void Fire()
    {
        Ray ray = new Ray(_cameraGameObject.transform.position, _cameraGameObject.transform.forward);
        RaycastHit hitObject = new RaycastHit();
        if(Physics.Raycast(ray, out hitObject, 1000))
        {

        }
    }
}
