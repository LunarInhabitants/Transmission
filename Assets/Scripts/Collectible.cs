using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private float y;

    // Use this for initialization
    void Start()
    {
        y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * 100, Space.World);
        transform.position = new Vector3(transform.position.x, y + Mathf.Sin(Time.time) / 2, transform.position.z);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Player>())
        {

        }
    }
}
