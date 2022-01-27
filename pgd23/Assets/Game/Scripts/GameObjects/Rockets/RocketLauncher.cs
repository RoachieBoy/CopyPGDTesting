using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float viewDistance;
    public List<HeatSeekingRocket> rockets = new List<HeatSeekingRocket>();

    private bool targetInDistance = false;

    private void Start()
    {
            
    }

    private void FixedUpdate()
    {
        targetDetection();

        if (targetInDistance)
        {
            attackState();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void targetDetection()
    {
        Vector2 targetDirection = target.position - transform.position;
        float targetDistance = (targetDirection.x * targetDirection.x) + (targetDirection.y * targetDirection.y);
        Mathf.Sqrt(targetDistance);


        if (targetDistance < viewDistance) 
        {
            targetInDistance = true;
        }
        else
        {
            targetInDistance = false;
        }
    }

    private void attackState()
    {
        HeatSeekingRocket rocket = GetComponent<HeatSeekingRocket>();
        //rocket.Initialize(transform.position, target);
        rockets.Add(rocket);
    }
}
