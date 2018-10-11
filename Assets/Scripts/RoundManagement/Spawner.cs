using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
  public GameObject basicAIPrefab;

  public static Node startNode;
  public static Node endNode;

  public static Node waypointOne;
  public static Node waypointTwo;
  public static Node waypointThree;
  public static Node waypointFour;
  public static Node waypointFive;

  public static Node[] waypoints = new Node[5];


  public bool meep;

  private void Update()
  {
    if (meep || Input.GetKeyDown(KeyCode.S))
    {
      SpawnAI(basicAIPrefab);
      meep = false;
    }
  }
  public void SpawnAI(GameObject enemyToSpawn)
  {
    GameObject spawned = Instantiate(basicAIPrefab, startNode.tileTransform.position + Vector3.up, basicAIPrefab.transform.rotation);
    BasicAI spawnedScript = spawned.GetComponent<BasicAI>();
    spawnedScript.startNode = startNode;
    spawnedScript.endNode = endNode;
    spawnedScript.PathThoughMaze();
  }

}
