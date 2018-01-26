using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseChromable : MonoBehaviour
{
    [SerializeField, Range(0.0f, 1.0f)] private float _r = 1.0f;
    [SerializeField, Range(0.0f, 1.0f)] private float _g = 1.0f;
    [SerializeField, Range(0.0f, 1.0f)] private float _b = 1.0f;
    public float R { get { return _r; } set { _r = Mathf.Clamp01(value); } }
    public float G { get { return _g; } set { _g = Mathf.Clamp01(value); } }
    public float B { get { return _b; } set { _b = Mathf.Clamp01(value); } }

    protected new Renderer renderer;

    protected virtual void Start()
    {
        renderer = GetComponentInChildren<Renderer>();
    }

    protected virtual void Update ()
    {
        renderer.material.SetColor("_Color", new Color(_r, _g, _b));
    }
}
