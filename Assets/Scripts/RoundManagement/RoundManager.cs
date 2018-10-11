using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoundState
{
  Starting, Setup, preAttack, Attack, postAttack, Victory, Defeat
}

[RequireComponent(typeof(Spawner))]
public class RoundManager : MonoBehaviour
{
  public RoundState currentState = RoundState.Starting;

  public int round = 0;

  public Spawner mySpawner;

  private void Start()
  {
    mySpawner = GetComponent<Spawner>();
  }
}
