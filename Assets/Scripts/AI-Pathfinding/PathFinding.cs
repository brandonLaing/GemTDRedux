using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PathFinding
{
  public static List<Node> AStar(Node startNode, Node endNode)
  {
    List<Node> wapoints = new List<Node>();
    List<PathFindingNode> openList = new List<PathFindingNode>(), closedList = new List<PathFindingNode>();

    Dictionary<Node, PathFindingNode> pathFindingNodes = new Dictionary<Node, PathFindingNode>();
    pathFindingNodes.Add(startNode, new PathFindingNode(startNode));
    openList.Add(pathFindingNodes[startNode]);

    while (!DoesListContainNode(endNode, closedList) && openList.Count > 0)
    {
      openList.Sort();
      PathFindingNode smallestCostSoFar = openList[0];

      for (int i = 0; i < smallestCostSoFar.graphNode.connections.Count; i++)
      {
        NodeConnection connectedNode = smallestCostSoFar.graphNode.connections[i];
        if (smallestCostSoFar.graphNode.connections[i].connected)
        {
          if (!DoesListContainNode(connectedNode.node, closedList))
          {
            if (!DoesListContainNode(connectedNode.node, openList))
            {
              float costToConnect = smallestCostSoFar.costSoFar + 1 + Vector3.Distance(connectedNode.node.tileTransform.position, endNode.tileTransform.position);
              PathFindingNode predecessor = smallestCostSoFar;

              pathFindingNodes.Add(connectedNode.node, new PathFindingNode(connectedNode.node, costToConnect, predecessor));
              openList.Add(pathFindingNodes[connectedNode.node]);
            }
            else
            {
              float currentCostToConnect = pathFindingNodes[connectedNode.node].costSoFar + Vector3.Distance(connectedNode.node.tileTransform.position, endNode.tileTransform.position);
              float costToTargetThroughCurrentNode = smallestCostSoFar.costSoFar + 1 + Vector3.Distance(connectedNode.node.tileTransform.position, endNode.tileTransform.position);

              if (costToTargetThroughCurrentNode < currentCostToConnect)
              {
                pathFindingNodes[connectedNode.node].costSoFar = costToTargetThroughCurrentNode;
                pathFindingNodes[connectedNode.node].predecessor = smallestCostSoFar;
              }
            }
          }
        }
      }

      closedList.Add(smallestCostSoFar);
      openList.Remove(smallestCostSoFar);
    }

    for (PathFindingNode wapoint = pathFindingNodes[endNode]; wapoint != null; wapoint = wapoint.predecessor)
    {
      wapoints.Add(wapoint.graphNode);
    }

    wapoints.Reverse();

    return wapoints;
  }

  private static bool DoesListContainNode(Node searchedNode, List<PathFindingNode> pathfindingNodeList)
  {
    foreach (PathFindingNode pathFindingNode in pathfindingNodeList)
    {
      if (pathFindingNode.graphNode == searchedNode)
      {
        return true;
      }
    }
    return false;
  }
}

class PathFindingNode : IComparable<PathFindingNode>
{
  public Node graphNode;
  public float costSoFar;
  public PathFindingNode predecessor;

  public PathFindingNode(Node newNode)
  {
    graphNode = newNode;
    costSoFar = 0;
  }

  public PathFindingNode(Node graphNode, float costSoFar, PathFindingNode predecessor)
  {
    this.graphNode = graphNode;
    this.costSoFar = costSoFar;
    this.predecessor = predecessor;
  }

  public int CompareTo(PathFindingNode other)
  {
    return costSoFar.CompareTo(other.costSoFar);
  }
}