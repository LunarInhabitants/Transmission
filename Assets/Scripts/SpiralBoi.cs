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
	            Vector3 vec = _startPoint;
	            vec.z += distance * i;
	            //vec.x += Mathf.Sin(i);
	           // vec.y += Mathf.Cos(i);
	            positions[i] = vec;
	        }
	        _lineRenderer.SetPositions(positions.ToArray());
            Debug.DrawLine(_startPoint, _endPoint, Color.black);
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
