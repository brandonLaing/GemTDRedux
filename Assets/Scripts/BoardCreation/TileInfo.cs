using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour
{
  public Node tileNode;
  public bool slotOpen = true;
  public GameObject tower;
  public Transform towerSpawnLocation;

  public GameObject[] towers;

  public void InteractWithTile()
  {
    if (slotOpen)
    {
      Debug.Log(transform.name + " was interacted with");

      GameObject towerToBuild = towers[Random.Range(0, towers.Length)];

      if (towerToBuild != null)
      {
        tower = Instantiate(towerToBuild, towerSpawnLocation.position, Quaternion.identity, this.transform);
        slotOpen = false;
        tileNode.BlockThisTile();
      }
    }
    else
    {
      Debug.LogWarning("Cant interact with " + transform.name);
    }
  }

  public void DestroyTower()
  {
    if (!slotOpen)
    {
      Debug.Log(transform.name + " destroyed its " + tower.transform.name);

      Destroy(tower);
      slotOpen = true;
      tower = null;
      tileNode.UnBlockThisTile();
    }
    else
    {
      Debug.LogWarning("Couldn't destroy a tower for " + transform.name);
    }
  }
}
