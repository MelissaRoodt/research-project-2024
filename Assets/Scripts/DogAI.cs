using System.Collections.Generic;
using UnityEngine;

/// Dog AI: Moves the dog between two calculated points based on player performance if triggered.
public class DogAI : MonoBehaviour
{
    public float minDistance = 1f;
    public float maxDistance = 10f;
    private float currentDistance; /// current distance to move to new position
    private float previousTime;

    private float weight = 1f; /// weight for neural network
    private float learningRate = 0.1f; /// learning rate for nearual network

    [SerializeField] private DataController dataController;

    void Start()
    {
        currentDistance = minDistance;
    }

    /// Calculates the next distance to move towards
    public void CalculateNewDistance()
    {
        float currentTime = Time.time;

        if (previousTime > 0)
        {
            float timeTaken = currentTime - previousTime;
            dataController.timeTakenList.Add(timeTaken);
            dataController.distanceList.Add(currentDistance);
            dataController.positionList.Add(transform.position.x);

            float avgTimeTaken = CalculateAverage(dataController.timeTakenList);
            float avgDistance = CalculateAverage(dataController.distanceList);

            /// Calculate error as the difference between current time taken and average time taken
            float error = timeTaken - avgTimeTaken;

            /// Update weight using the perceptron learning rule
            weight += learningRate * error;
            //Debug.Log("Weight: " + weight + " | Learning Rate: " + learningRate + " | Error: " + error + " | Avg Distance: " + avgDistance);

            /// Calculate new distance based on the weight and average distance
            currentDistance = Mathf.Clamp(weight * avgDistance, minDistance, maxDistance);
            ///Debug.Log("Avg Distance: " + avgDistance + " | Weight: " + weight + " | New Distance: " + currentDistance);
        }

        previousTime = currentTime;
    }

    /// Calculates the average of list elements
    /// 
    /// @param values is list datatype
    /// @returns the average of list elements 
    private float CalculateAverage(List<float> values)
    {
        float sum = 0f;
        foreach (float value in values)
        {
            sum += value;
        }
        return sum / values.Count;
    }

    /// Gets the next position to move towards based on current player position and distance size
    /// 
    /// @param initialPosition - current position of player.
    /// @returns a new position for ai to move towards 

    public Vector3 getNextPosition(Vector3 initialPosition)
    {
        Vector3 newPosition = initialPosition + new Vector3(currentDistance, 0, 0);
        //Debug.Log("Current Distance: " + currentDistance + " | New Position: " + newPosition);
        return newPosition;
    }
}
