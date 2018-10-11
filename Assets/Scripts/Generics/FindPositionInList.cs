using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FindPositionInList
{
  public static int Find<TOne, TTwo>(List<TOne> listToCheck, TTwo variableToCheckAgainst) where TOne : class where TTwo : class
  {
    for (int i = 0; i < listToCheck.Count; i++)
    {
      if (listToCheck[i] == variableToCheckAgainst)
      {
        return i;
      }
    }
    return -1;
  }
}
