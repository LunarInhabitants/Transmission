using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private float _walkSpeed = 6.0f;
    [SerializeField] private float _sprintSpeed = 20.0f;

    [SerializeField] private float _jumpSpeed = 8.0F;
    [SerializeField] private float _gravity = 20.0F;
    [SerializeField] private Transform gunSpawnPoint;
    [SerializeField] private GameObject gunPrefab;

    public float Health { get; private set; }
    public float MaxHealth = 100.0f;

    private Vector3 moveDirection;

    private float _moveSpeed = 6.0f;

    private CharacterController _characterController;
    private Camera _cameraGameObject;

    private void Awake()
    {
        GameObject gunInstance = (GameObject)Instantiate(gunPrefab, gunSpawnPoint.transform.position, gunSpawnPoint.rotation, gunSpawnPoint.transform);
        
    }
    // Use this for initialization
    void Start()
    {
        Instance = this;

        _characterController = GetComponent<CharacterController>();
        _cameraGameObject = GetComponentInChildren<Camera>();

        Health = MaxHealth;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        _moveSpeed = Input.GetButton("Sprint") ? _sprintSpeed : _walkSpeed;

        if (_characterController.isGrounded)
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
        _characterController.Move((moveDirection * _moveSpeed) * Time.fixedDeltaTime);
        //_characterController.Move(moveDirection);
    }

    public void Hurt(float damage)
    {
        Health = Mathf.Max(0.0f, Health - damage);
        if (Health <= 0.0f)
        {
            Die();
        }
    }

    public void GetAmmo()
    {

    }

    public void Die()
    {
        Debug.Log("Experiment Failure: Human is Dead");
    }
}
