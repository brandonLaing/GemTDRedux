using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class Node : ScriptableObject
{
  public List<NodeConnection> connections = new List<NodeConnection>();
  public Transform tileTransform;

  private TileGenerator tileGen;

  public void SetNodeTransform(Transform tileTransform)
  {
    this.tileTransform = tileTransform;
  }

  public void SetNodeTileGen(TileGenerator tileGen)
  {
    this.tileGen = tileGen;
  }

  public void MakeNewConnection(Node connection, bool connected)
  {
    connections.Add(new NodeConnection(connection, connected));

  }

  public void DisplayConnections()
  {
    StringBuilder sb = new StringBuilder();

    sb.Append("Connections for " + this.name + ":\n");
    foreach (NodeConnection connection in connections)
    {
      sb.Append(connection.node.name + ": " + connection.connected + "\n");
    }

    Debug.Log(sb);
  }

  public void BlockThisTile()
  {
    // foreach node connected this one
    foreach (NodeConnection connection in connections)
    {
      connection.connected = false;

      // then for each connection to the connected nodes
      foreach (NodeConnection connectionConnection in connection.node.connections)
      {
        if (connectionConnection.node == this)
        {
          connectionConnection.connected = false;
          UpdateGizmoOnTileGen(connection.node, connectionConnection.node, false);
        }
      }
    }
  }

  public void UnBlockThisTile()
  {
    foreach (NodeConnection connection in connections)
    {
      connection.connected = true;

      // then for each connection to the connected nodes
      foreach (NodeConnection connectionConnection in connection.node.connections)
      {
        if (connectionConnection.node == this)
        {
          connectionConnection.connected = true;
          UpdateGizmoOnTileGen(connection.node, connectionConnection.node, true);
        }
      }

    }
  }

  private void UpdateGizmoOnTileGen(Node node1, Node node2, bool connectionState)
  {
    tileGen.UpdateConnectionBetween(node1, node2, connectionState);
  }
}

public class NodeConnection
{
  public Node node;
  public bool connected;

  public NodeConnection(Node node, bool connected)
  {
    this.node = node;
    this.connected = connected;
  }
}

