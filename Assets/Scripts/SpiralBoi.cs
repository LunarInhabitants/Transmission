using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralBoi : MonoBehaviour
{
    public Vector3 _startPoint = new Vector3(0,0,0);
    public  Vector3 _endPoint = new Vector3(0, 0, 10);

    [SerializeField] private int _resolution = 50;
    [SerializeField] private float _var1 = 4.0f;
    [SerializeField] private float _var2 = 0.1f;
    [SerializeField] private float _var3 = 0.02f;

    private LineRenderer _lineRenderer;
    private List<Vector3> positions;

	// Use this for initialization
	void Start ()
	{
        positions = new List<Vector3>();
	    _lineRenderer = transform.parent.GetComponentInChildren<LineRenderer>();
	    _lineRenderer.positionCount = _resolution;
        _lineRenderer.enabled = false;
	    for (int i = 0; i < _resolution; i++)
	    {
            positions.Add(new Vector3(0,0,0));
	    }
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (_lineRenderer.enabled)
	    {
	        Vector3 movementVector = _endPoint - _startPoint;
	        _resolution = (int)((float)movementVector.magnitude / _var3);
	        _lineRenderer.positionCount = _resolution;
	        if (positions.Count < _resolution)
	        {
	            for (int i = positions.Count; i <= _resolution; i++)
	            {
                    positions.Add(new Vector3());
	            }
	        }
	        float distance = movementVector.magnitude / _resolution;
	        for (int i = 0; i < positions.Count; i++)
	        {
	            positions[i] = new Vector3(
                    Mathf.Sin((Time.time + i) * (_var1 + Mathf.Sin(Time.time) * (_var1 / 3.0f))) * _var2,
                    Mathf.Cos((Time.time + i) * (_var1 + Mathf.Sin(Time.time) * (_var1 / 3.0f))) * _var2,
                    distance * i
                );
	        }
	        _lineRenderer.SetPositions(positions.ToArray());
	    }
	}

    public void DoSpiral(Vector3 start, Vector3 end)
    {
        _lineRenderer.enabled = true;
        _startPoint = start;
        _endPoint = end;
    }

    public void StopSpiral()
    {
        _lineRenderer.enabled = false;

    }
}
