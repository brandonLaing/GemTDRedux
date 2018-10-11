using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Node : ScriptableObject
{
  public List<NodeConnection> connections = new List<NodeConnection>();
  public Transform tileTransform;

  public void SetNodeTransform(Transform tileTransform)
  {
    this.tileTransform = tileTransform;
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
      // then for each connection to the connected nodes
      foreach (NodeConnection connectionConnection in connection.node.connections)
      {
        if (connectionConnection.node == this)
        {
          connectionConnection.connected = false;
        }
      }
    }
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

