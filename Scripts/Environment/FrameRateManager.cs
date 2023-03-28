using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateManager : MonoBehaviour
{
     void Awake()
        {
            #if UNITY_EDITOR
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = 60;
            #endif
        }
    
        private void Update()
        {
            // #if UNITY_EDITOR
            //     Debug.Log($"Frame rate: {Time.deltaTime}");
            // #endif
        }
}
