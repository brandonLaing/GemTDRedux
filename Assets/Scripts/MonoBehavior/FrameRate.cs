using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRate : MonoBehaviour
{
  public float mesureWind = 1.0F;

  private float frameRateTimer = 0F;

  private float frameRate = 0F;

  private int frameCount = 0;

  private System.DateTime lastFrameUpdate;

  private void Start()
  {
    lastFrameUpdate = System.DateTime.Now;
    ResetTimer();
    
  }

  void ResetTimer()
  {
    frameRateTimer = 0F;
    frameCount = 0;
  }

  private void Update()
  {
    float elapsedTime = (float)(System.DateTime.Now - lastFrameUpdate).TotalSeconds;
    lastFrameUpdate = System.DateTime.Now;

    frameCount++;
    frameRateTimer += elapsedTime;

    if (frameRateTimer >= mesureWind)
    {
      frameRate = (float)frameCount / frameRateTimer;
      ResetTimer();
    }

  }

  private void OnGUI()
  {
    if (frameRate > 0F)
    {
      GUILayout.Label(frameRate.ToString("0.00"));

    }
  }

}

