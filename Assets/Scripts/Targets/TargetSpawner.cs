using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetSpawner : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private NavMeshData data;
	[SerializeField] private GameObject enemyPrefab;

	[Header("Spawning Configs")]
	[SerializeField] private int maxTargets;

	[Header("Publics")]
	public NavMeshDataInstance[] instances = new NavMeshDataInstance[1];

	private int currentTargets;

	NavMeshData navMeshData;


	private void Start()
	{
		TargetScript.targetDestroyed += SubtractTargetCount;

		instances[0] = NavMesh.AddNavMeshData(data);
	}
	
	private void Update()
	{
		while (currentTargets < maxTargets)
		{
			GameObject target = Instantiate(enemyPrefab);

			NavMeshAgent navMeshAgent = target.GetComponent<NavMeshAgent>();

			NavMeshTriangulation triangulation = NavMesh.CalculateTriangulation();

			// Pick the first indice of a random triangle in the nav mesh
			int t = Random.Range(0, triangulation.indices.Length - 3);

			// Select a random point on it
			Vector3 point = Vector3.Lerp(triangulation.vertices[triangulation.indices[t]], triangulation.vertices[triangulation.indices[t+1]], Random.value);
			Vector3.Lerp(point, triangulation.vertices[triangulation.indices[t+2]], Random.value);

			currentTargets++;
		}
	}
	
	
	private void SubtractTargetCount()
	{
		currentTargets--;
	}
}
