using System.Collections.Generic;
using UnityEngine;

/// Control the public access to ai & data analysis data
/// 
/// timeTakenList   - time taken to reach a goal position
/// distanceList    - distance between current point and last point
/// positionList    - goal position reached
public class DataController: MonoBehaviour
{
    public List<float> timeTakenList = new List<float>();
    public List<float> distanceList = new List<float>();
    public List<float> positionList = new List<float>();

    /// Reset the data analysis data 
    public void ResetData() {
        timeTakenList.Clear();
        positionList.Clear();
    }
}
