using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
  public int mapWidth = 37;
  public int mapHeight = 37;

  [Header("Specific Points")]
  // start is 4 down and 5 right from the top left
  private readonly int[] startPosition = new int[2] { 5, 32 };
  // end is 5 left from the right edge and 5 up from the bottom
  public readonly int[] endPosition = new int[2] { 32, 5 };

  private readonly int[] waypointOnePosition = new int[2] { 5, 19 };
  private readonly int[] waypointTwoPosition = new int[2] { 32, 19 };
  private readonly int[] waypointThreePosition = new int[2] { 32, 32 };
  private readonly int[] waypointFourPosition = new int[2] { 19, 32 };
  private readonly int[] waypointFivePosition = new int[2] { 19, 5 };

  public GameObject tilePrefab;

  private List<Node> allNodes = new List<Node>();

  private List<GizmoConnections> connections = new List<GizmoConnections>();

  private void Start()
  {
    GenerateTiles();
  }

  public void GenerateTiles()
  {
    int i = 0;
    for (int x = 0; x < mapWidth; x++)
    {
      for (int z = 0; z < mapHeight; z++)
      {
        #region Make the New Tile
        GameObject newTile = Instantiate(tilePrefab, new Vector3(x, 0, z), tilePrefab.transform.rotation, this.transform);
        newTile.name = "Tile: " + i;
        TileInfo tileInfoComp = newTile.GetComponent<TileInfo>();
        tileInfoComp.tileNode = ScriptableObject.CreateInstance<Node>();
        tileInfoComp.tileNode.SetNodeTransform(newTile.transform);
        tileInfoComp.tileNode.name = "Node: " + i;
        allNodes.Insert(0, tileInfoComp.tileNode);
        i++;
        #endregion

        #region CheckConnections
        int connectionsSoFar = 0;

        Node baseNode = tileInfoComp.tileNode;

        foreach (Node crossNode in allNodes)
        {
          if (connectionsSoFar < 2)
          {
            if (baseNode.tileTransform.position == crossNode.tileTransform.position + Vector3.forward ||
                baseNode.tileTransform.position == crossNode.tileTransform.position + Vector3.right)
            {
              connectionsSoFar++;

              baseNode.MakeNewConnection(crossNode, true);
              crossNode.MakeNewConnection(baseNode, true);

              connections.Add(new GizmoConnections(baseNode, crossNode, true));

            }
          }
        }
        #endregion

        #region Set the start and end nodes
        SetNodeStuff(x, z, startPosition, ref Spawner.startNode, tileInfoComp.tileNode);
        SetNodeStuff(x, z, endPosition, ref Spawner.endNode, tileInfoComp.tileNode);

        SetWaypoints(x, z, waypointOnePosition, tileInfoComp.tileNode, ref Spawner.waypoints, 0);
        SetWaypoints(x, z, waypointTwoPosition, tileInfoComp.tileNode, ref Spawner.waypoints, 1);
        SetWaypoints(x, z, waypointThreePosition, tileInfoComp.tileNode, ref Spawner.waypoints, 2);
        SetWaypoints(x, z, waypointFourPosition, tileInfoComp.tileNode, ref Spawner.waypoints, 3);
        SetWaypoints(x, z, waypointFivePosition, tileInfoComp.tileNode, ref Spawner.waypoints, 4);
        #endregion
      }
    }
  }

  private void SetWaypoints(int x, int z, int[] desiredPosition, Node nodeChecked, ref Node[] waypointsArray, int position)
  {
    if (x == desiredPosition[0] && z == desiredPosition[1])
    {
      waypointsArray[position] = nodeChecked;
    }
  }

  private void SetNodeStuff(int x, int z, int[] desiredPosition, ref Node nodeDestination, Node nodeChecked)
  {
    if (x == desiredPosition[0] && z == desiredPosition[1])
    {
      nodeDestination = nodeChecked;
    }
  }

  private void OnDrawGizmos()
  {
    try
    {
      if (allNodes.Count > 0)
      {
        Gizmos.color = Color.black;
        foreach (Node node in allNodes)
        {
          Gizmos.DrawSphere(node.tileTransform.position + new Vector3(0,.5F,0), .2F);
        }

        foreach (GizmoConnections connection in connections)
        {
          if (connection.conneted)
          {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(connection.positionOne, connection.positionTwo);
          }
          else
          {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(connection.positionOne, connection.positionTwo);
          }
        }
      }
    }
    catch
    {
      Debug.LogWarning("Couldn't draw connection gizmo's right now\n");
    }
  }

  public void UpdateConnectionBetween(Node node1, Node node2, bool connected)
  {
    foreach (GizmoConnections connection in connections)
    {
      if (node1 == connection.startNode && node2 == connection.endNode)
      {
        connection.conneted = connected;
      }
      else if (node2 == connection.startNode && node1 == connection.endNode)
      {
        connection.conneted = connected;
      }
    }
  }

  private void OnDestroy()
  {
    allNodes = new List<Node>();
    connections = new List<GizmoConnections>();
  }
}


class GizmoConnections
{
  public Vector3 positionOne;
  public Vector3 positionTwo;
  public Vector3 direction;
  public bool conneted;

  public Node startNode;
  public Node endNode;

  public GizmoConnections(Node nodeOne, Node nodeTwo, bool connected)
  {
    this.positionOne = nodeOne.tileTransform.position;
    this.positionTwo = nodeTwo.tileTransform.position;

    this.positionOne.y = .5F;
    this.positionTwo.y = .5F;

    direction = this.positionOne - this.positionTwo;

    this.conneted = connected;

    startNode = nodeOne;
    endNode = nodeTwo;
  }
}
