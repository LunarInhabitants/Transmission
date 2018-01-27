using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Remoting.Channels;
using UnityEngine;

public enum GunMode
{
    Red,
    Green,
    Blue
}

public class Gun : MonoBehaviour
{
    public static Gun Instance { get; private set; }

    [SerializeField] private float _beamDamage = 1.0f;
    [SerializeField] private Color _beamColour = new Color(1.0f, 0, 0);
    [SerializeField] private GameObject _firePoint;

    public float scopeFOV = 10.0f;
    float _defaultFOV = 0.0f;
    float _defaultGunFOV = 0.0f;
    Camera _gunCamera;

    private Material _material;
    private GameObject _cameraGameObject;
    private GunMode _gunMode = GunMode.Red;
    public GunMode GunMode
    {
        get { return _gunMode; }
        set
        {
            _gunMode = value;
            switch (_gunMode)
            {
                case GunMode.Blue:
                    _beamColour = Color.blue;
                    break;
                case GunMode.Red:
                    _beamColour = Color.red;
                    break;
                case GunMode.Green:
                    _beamColour = Color.green;
                    break;
            }
        }
    }


    private float mouseScrollBoi;

	// Use this for initialization
	void Start ()
	{
        Instance = this;

        _material = GetComponentInChildren<Renderer>().material;
	    _cameraGameObject = transform.parent.gameObject;

	    _material.color = _beamColour;

        _defaultFOV = Camera.main.fieldOfView;
        _gunCamera = GetComponentInParent<Camera>();
        _defaultGunFOV = _gunCamera.fieldOfView;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Fire1"))
        {
            Fire();
        }

        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, Input.GetButton("Fire2") ? scopeFOV : _defaultFOV, 0.5f);
        _gunCamera.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, Input.GetButton("Fire2") ? scopeFOV * (_defaultGunFOV / _defaultFOV): _defaultGunFOV, 0.5f);

        mouseScrollBoi += Input.mouseScrollDelta.y;

	    if (mouseScrollBoi < -1)
	    {
            NextMode();
	        mouseScrollBoi = 0;
	    }
        else if (mouseScrollBoi > 1)
	    {
            PreviousMode();
	        mouseScrollBoi = 0;
        }

        if (Input.GetKeyDown(KeyCode.E))
	    {
	        Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
	        Cursor.visible = !Cursor.visible;
	    }
	}

    private void Fire()
    {
        Ray ray = new Ray(_cameraGameObject.transform.position, _cameraGameObject.transform.forward);

        RaycastHit hitObject = new RaycastHit();
        if(Physics.Raycast(ray, out hitObject, 1000))
        {
            BaseChromable chromie = hitObject.transform.gameObject.GetComponent<BaseChromable>();
            if (chromie != null)
            {
                Debug.DrawLine(_firePoint.transform.position, hitObject.point, Color.magenta);
                chromie.Chromatize(_beamColour * Time.deltaTime);
            }
            else
            {
                Debug.DrawLine(_firePoint.transform.position, hitObject.point, Color.green);

            }
        }
        else
        {
            Debug.DrawLine(_firePoint.transform.position, hitObject.point, Color.red);      
        }
    }

    private void NextMode()
    {
        if ((int)_gunMode == 0)
        {
            GunMode = (GunMode)2;
        }
        else
        {
            GunMode = (GunMode)(int)_gunMode - 1;
        }

        UpdateGunVisual();
    }

    private void PreviousMode()
    {
        if ((int) _gunMode == 2)
        {
            GunMode = 0;
        }
        else
        {
            GunMode = (GunMode)(int)_gunMode + 1;
        }

        UpdateGunVisual();
    }

    private void UpdateGunVisual()
    {       
        switch (_gunMode)
        {
            case GunMode.Red:
                _material.color = Color.red;
                break;
            case GunMode.Green:
                _material.color = Color.green;
                break;
            case GunMode.Blue:
                _material.color = Color.blue;
                break;
        }
    }
}
