using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathMover : MonoBehaviour
{

    [SerializeField] private NavMeshAgent navmeshagent;
    
    private Queue<Vector3> pathPoints = new Queue<Vector3>();
    public float destination = 0.7f;
    [SerializeField] GameManager gameManager;

    
   

    private void Awake()
    {
        navmeshagent = GetComponent<NavMeshAgent>();
        FindObjectOfType<PathCreator>().OnNewPathCreated += SetPoints;
    }

    private void SetPoints(IEnumerable<Vector3> points)
    {
        pathPoints = new Queue<Vector3>(points);
    }
    
    // Update is called once per frame
    public void Update()
    {
        UpdatePathing();
        if (navmeshagent.hasPath && Vector3.Distance(transform.position, navmeshagent.destination) <= 0.2f)
        {
            if (navmeshagent.velocity.sqrMagnitude <= 1f)
            {
                gameManager.EndGame();
            }
        }
    
    }
    
    private void UpdatePathing()
    {
        if (ShouldSetDestination())
        {
            navmeshagent.SetDestination(pathPoints.Dequeue());
            
        }
            
    }
    //Boolean method that control it should set destination or not
    private bool ShouldSetDestination()
    {
        if (pathPoints.Count == 0)
            return false;
        if (navmeshagent.hasPath == false || navmeshagent.remainingDistance < destination)
            return true;
        return false;
    }



    
}
