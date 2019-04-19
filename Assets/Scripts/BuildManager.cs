using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{

	public static BuildManager instance;

	private void Awake()
	{
		if(instance != null)
		{
			Debug.LogError("More than one BuildManager in scene!");
		}
		instance = this;
	}

	public GameObject standartTurretPrefab;

	// Start is called before the first frame update
	void Start()
	{
		turretToBuild = standartTurretPrefab;
	}

	private GameObject turretToBuild;

	public GameObject GetTurretToBuild()
	{
		return turretToBuild;
	}
	
	

    // Update is called once per frame
    void Update()
    {
        
    }
}
