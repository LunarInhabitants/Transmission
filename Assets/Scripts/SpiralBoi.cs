using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralBoi : MonoBehaviour
{
    public Vector3 _startPoint = new Vector3(0,0,0);
    public  Vector3 _endPoint = new Vector3(0, 0, 10);

    [SerializeField] private int _resolution = 50;

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
	        float distance = movementVector.magnitude / _resolution;
	        for (int i = 0; i < positions.Count; i++)
	        {
	            positions[i] = new Vector3(
                    Mathf.Sin((Time.time + i) * 4.0f) * 0.02f,
                    Mathf.Cos((Time.time + i) * 4.0f) * 0.02f,
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
