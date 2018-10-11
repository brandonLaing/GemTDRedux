using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClicker : MonoBehaviour
{
  public GameObject towerOne;
  public TileGenerator tileGen;
  public LayerMask tileLayermask;
  public LayerMask towerMask;

  public RotateAroundObject rotatorCamera;

  private delegate void DoOnRaycast(RaycastHit hit);

  private void Update()
  {
    // interact with tile
    if (Input.GetMouseButtonDown(1) && !Input.GetKey(KeyCode.LeftShift))
    {
      RaycastToTile(tileLayermask, InteractWithTile);

    }

    // destroy tower
    if (Input.GetMouseButtonDown(1) && Input.GetKey(KeyCode.LeftShift))
    {
      RaycastToTile(tileLayermask, DestroyTower);
    }

    // set new showcase tower
    if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift))
    {
      RaycastToTile(towerMask, ShowcaseTower);
    }

    // show connections
    if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift))
    {
      RaycastToTile(tileLayermask, DisplayTileConnections);
    }
  }

  private void RaycastToTile(LayerMask mask, DoOnRaycast method)
  {
    RaycastHit hit;
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    if (Physics.Raycast(ray, out hit, 100F, mask))
    {
      method(hit);
    }
  }

  private void InteractWithTile(RaycastHit hit)
  {
    TileInfo parentsInfo = hit.transform.GetComponentInParent<TileInfo>();
    parentsInfo.InteractWithTile();
  }

  private void DestroyTower(RaycastHit hit)
  {
    TileInfo parentsInfo = hit.transform.GetComponentInParent<TileInfo>();
    parentsInfo.DestroyTower();
  }

  private void ShowcaseTower(RaycastHit hit)
  {
    rotatorCamera.RotateAround(hit.transform);
  }

  private void DisplayTileConnections(RaycastHit hit)
  {
    TileInfo parentsInfo = hit.transform.GetComponentInParent<TileInfo>();
    parentsInfo.tileNode.DisplayConnections();
  }
}
