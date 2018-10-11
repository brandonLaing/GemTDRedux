using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAI : MonoBehaviour
{
  public Node startNode;
  public Node endNode;

  public List<Node> moveQue = new List<Node>();

  public float speed;

  public void MakePath()
  {
    moveQue = PathFinding.AStar(startNode, endNode);
  }

  /** PathThoughMaze:
   * This makes a move que that will path the AI though the entire maze making sure to hit each waypoint
   */
  public void PathThoughMaze()
  {
    StartCoroutine(StaggerdPathThroughMaze(startNode));
  }

  private IEnumerator StaggerdPathThroughMaze(Node lastNode)
  {
    foreach (Node waypoint in Spawner.waypoints)
    {
      List<Node> pathingPoints = PathFinding.AStar(lastNode, waypoint);

      yield return new WaitForEndOfFrame();

      foreach (Node pathingPoint in pathingPoints)
      {
        moveQue.Add(pathingPoint);
      }
      lastNode = moveQue[moveQue.Count - 1];

      yield return new WaitForSeconds(.05F);
    }

    yield return new WaitForSeconds(.1F);

    List<Node> toEndPathPoints = PathFinding.AStar(lastNode, endNode);

    yield return new WaitForEndOfFrame();

    foreach (Node pathingPoint in toEndPathPoints)
    {
      moveQue.Add(pathingPoint);
    }
  }

  private void Update()
  {
    if (moveQue.Count > 0)
    {
      transform.position = Vector3.MoveTowards(this.transform.position, moveQue[0].tileTransform.position + Vector3.up, speed * Time.deltaTime);

      if (Vector3.Distance(this.transform.position, moveQue[0].tileTransform.position + Vector3.up) < .1F)
      {
        moveQue.Remove(moveQue[0]);
      }
    }
  }
}
