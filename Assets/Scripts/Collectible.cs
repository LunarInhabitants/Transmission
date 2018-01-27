using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

enum ChromulationConfiguration
{
    Red,
    Green,
    Blue
}

public class Collectible : MonoBehaviour
{
    private float y;

    private ChromulationConfiguration colour;

    // Use this for initialization
    void Start()
    {
        y = transform.position.y;
        colour = (ChromulationConfiguration)Random.Range(0, 2);

        switch (colour)
        {
            case ChromulationConfiguration.Blue:
                GetComponentInChildren<Renderer>().material.color = Color.blue;
                break;
            case ChromulationConfiguration.Red:
                GetComponentInChildren<Renderer>().material.color = Color.red;
                break;
            case ChromulationConfiguration.Green:
                GetComponentInChildren<Renderer>().material.color = Color.green;
                break;
        }
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
            collider.GetComponent<Player>().GetAmmo();
            Destroy(gameObject);
        }
    }
}
