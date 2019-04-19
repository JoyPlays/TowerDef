using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{

	public NavMeshAgent agent;
	public LevelSettings levelSettings;


	// Start is called before the first frame update
	void Start()
    {
		levelSettings = (LevelSettings)FindObjectOfType(typeof(LevelSettings));
		agent = gameObject.GetComponent<NavMeshAgent>();
		agent.SetDestination(levelSettings.destination.transform.position);
    }
}
