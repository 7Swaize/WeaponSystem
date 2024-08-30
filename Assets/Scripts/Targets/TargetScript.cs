using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

// not my script. NavMesh is not part of my focus concept so I did not want to create a random movement script from scratch. I understand how it works though.

public class TargetScript : MonoBehaviour, IDamageable
{
    [Header("References")]
    [SerializeField] private float range; //radius of sphere

    [Header("Target Configs")]
	[SerializeField] private float health;
    [SerializeField] private Color lerpTargetColor;

	[Header("Actions")]
	public static Action targetDestroyed;

    private Transform centrePoint; //centre of the area the agent wants to move around in
    //instead of centrePoint you can set it as the transform of the agent if you don't care about a specific area

    private GameObject model;
    private Renderer renderer;
    private Color currentColor;

    private NavMeshAgent agent;
    
    void Start()
    {
        model = transform.GetChild(0).gameObject;

        agent = GetComponent<NavMeshAgent>();
        renderer = model.GetComponent<Renderer>();

        currentColor = renderer.material.color;

        centrePoint = gameObject.transform;
    }
    
    
    void Update()
    {
        if(agent.remainingDistance <= agent.stoppingDistance) //done with path
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                agent.SetDestination(point);
            }
        }

    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        { 
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
    
    private void HandleColorLerp()
    {
        Color lerpedColor = Color.Lerp(lerpTargetColor, currentColor, health / 100);

        renderer.material.color = lerpedColor;
    }

    public void Damage(float damageTaken)
	{
		health -= damageTaken;

        HandleColorLerp();

		if (health <= 0)
		{
			if (gameObject != null && gameObject.activeSelf) targetDestroyed?.Invoke();

            gameObject.SetActive(false);
			Destroy(gameObject);
		}
	}
}