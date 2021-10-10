using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathCreator : MonoBehaviour
{
    [SerializeField]
    Camera cam1;
    bool mouseEnabled = true;
    

    private LineRenderer lineRenderer;

    private List<Vector3> points = new List<Vector3>();

    public Action<IEnumerable<Vector3>> OnNewPathCreated = delegate { };

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if there is any path before mouse click it will clear it
        if (Input.GetMouseButtonDown(0))
            points.Clear();
        
        if (Input.GetMouseButton(0)&&mouseEnabled)
        {
            Ray ray = cam1.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (DistanceToLastPoint(hitInfo.point) > 1f)
                {
                    points.Add(hitInfo.point);
                    lineRenderer.positionCount = points.Count;
                    lineRenderer.SetPositions(points.ToArray());
                }
                
            }
        }
        if (Input.GetButtonUp("Fire1"))
        {
            OnNewPathCreated(points);
            mouseEnabled = false;
        }
           
        
    }

    private float DistanceToLastPoint(Vector3 point)
    {
        if (!points.Any())
            return Mathf.Infinity;
        return Vector3.Distance(points.Last(), point);
    }

    //?
    public void PathCleaner()
    {
        points.Clear();
    }
}
