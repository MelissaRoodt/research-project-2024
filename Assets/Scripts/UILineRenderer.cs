using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

/// Renders a line on a grid based on data points.
/// Inherits from Unity's Graphic class to create custom UI visuals.
public class UILineRenderer : Graphic
{
    public Vector2Int gridSize;
    public UIGridRenderer grid;

    [Header("Data")]
    public DataController controller;
    public List<Vector2> points;
    public UnityEngine.UI.Button btnAddData;

    float width;
    float height;
    float unitWidth;
    float unitHeight;

    public float thickness = 10f;

    protected override void Awake()
    {
        btnAddData.onClick.AddListener(() =>
        {
            AddDataPoints();
        });
    }

    /// Adds data points from the DataController to the points list.
    public void AddDataPoints()
    {
        points.Clear();

        if(controller.positionList.Count == controller.timeTakenList.Count)
        {
            for (int i = 0; i < controller.positionList.Count; i++)
            {
                points.Add(new Vector2(controller.positionList[i], controller.timeTakenList[i]));
            }
        }

        SetVerticesDirty();
    }

    /// Called by Unity to populate the mesh used for rendering the lines.
    /// It recalculates the vertices based on the updated points.
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        width = rectTransform.rect.width;
        height = rectTransform.rect.height;

        unitWidth = width / (float) gridSize.x;
        unitHeight = height / (float) gridSize.y;

        if(points.Count < 2)
        {
            return;
        }

        float angle = 0;

        for(int i=0; i<points.Count; i++)
        {
            Vector2 point = points[i];

            if (i < points.Count - 1)
            {
                angle = GetAngle(points[i], points[i +1]) + 45f;
            }

            DrawVerticesForPoint(point, vh, angle);
        }

        for(int i=0; i<points.Count-1 ; i++)
        {
            int index = i * 2;
            vh.AddTriangle(index + 0, index + 1, index + 3);
            vh.AddTriangle(index + 3, index + 2, index + 0);
        }
    }

    /// Calculates the angle between two points (me and target).
    /// @param me The current point.
    /// @param target The next point in the line.
    /// @returns The angle in degrees between the two points.
    public float GetAngle(Vector2 me, Vector2 target)
    {
        return (float)(Mathf.Atan2(target.y - me.y, target.x - me.x) * (180 / Mathf.PI));
    }

    void DrawVerticesForPoint(Vector2 point, VertexHelper vh, float angle)
    {
        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = color;

        vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(-thickness / 2, 0);
        vertex.position += new Vector3(unitWidth * point.x, unitHeight * point.y);
        vh.AddVert(vertex);

        vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(thickness / 2, 0);
        vertex.position = new Vector3(unitWidth * point.x, unitHeight * point.y);
        vh.AddVert(vertex);
    }

    private void Update()
    {
        if(grid != null)
        {
            if(gridSize != grid.gridSize)
            {
                gridSize = grid.gridSize;
                SetVerticesDirty();
            }
        }
    }
}
