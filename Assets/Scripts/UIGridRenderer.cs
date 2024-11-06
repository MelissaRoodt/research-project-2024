using UnityEngine;
using UnityEngine.UI;

/// Renders the grid of the data analysis menu. 
/// Inherits from Unity's Graphic class to create custom UI grid visuals.
public class UIGridRenderer : Graphic
{
    public Vector2Int gridSize = new Vector2Int(1,1);
    public float thickness = 10f;

    float width;
    float height;
    float cellWidth;
    float cellHeight;

    /// Called by Unity to update the mesh used for rendering the grid.
    /// This method clears any existing mesh data and recalculates the grid's vertices and cells.
    /// @param vh VertexHelper that defines the mesh for the UI element
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        width = rectTransform.rect.width;
        height = rectTransform.rect.height;

        cellWidth = width / (float) gridSize.x;
        cellHeight = height / (float)gridSize.y;

        int count = 0;

        for(int y=0; y<gridSize.y; y++)
        {
            for(int x=0; x<gridSize.x; x++)
            {
                DrawCell(x, y, count, vh);
                count++;
            }
        }
       
    }
    /// Draws a single cell in the grid.
    /// Each cell is represented by 8 vertices (corners and inner edges for thickness).
    /// @param x Column index of the cell.
    /// @param y Row index of the cell.
    /// @param index Index of the cell, used for vertex offset calculation.
    /// @param vh VertexHelper to add vertices to.
    private void DrawCell(int x, int y, int index, VertexHelper vh)
    {
        float xPos = cellWidth * x;
        float yPos = cellHeight * y;

        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = color;

        vertex.position = new Vector3(xPos, yPos);
        vh.AddVert(vertex);

        vertex.position = new Vector3(xPos, yPos + cellHeight);
        vh.AddVert(vertex);

        vertex.position = new Vector3(xPos + cellWidth, yPos + cellHeight);
        vh.AddVert(vertex);

        vertex.position = new Vector3(xPos + cellWidth, yPos);
        vh.AddVert(vertex);

        /*vh.AddTriangle(0, 1, 2);
        vh.AddTriangle(2, 3, 0);*/

        float widthSqr = thickness * thickness;
        float distanceSqr = widthSqr / 2f;
        float distance = Mathf.Sqrt(distanceSqr);

        vertex.position = new Vector3(xPos + distance, yPos + distance);
        vh.AddVert(vertex);

        vertex.position = new Vector3(xPos + distance, yPos + (cellHeight - distance));
        vh.AddVert(vertex);

        vertex.position = new Vector3(xPos + (cellWidth - distance), yPos + (cellHeight - distance));
        vh.AddVert(vertex);

        vertex.position = new Vector3(xPos + (cellWidth - distance), yPos + distance);
        vh.AddVert(vertex);

        int offset = index * 8;

        // left edge
        vh.AddTriangle(offset + 0, offset + 1, offset + 5);
        vh.AddTriangle(offset + 5, offset + 4, offset + 0);

        // top edge
        vh.AddTriangle(offset + 1, offset + 2, offset + 6);
        vh.AddTriangle(offset + 6, offset + 5, offset + 1);

        // right edge
        vh.AddTriangle(offset + 2, offset + 3, offset + 7);
        vh.AddTriangle(offset + 7, offset + 6, offset + 2);

        // bottom edge
        vh.AddTriangle(offset + 3, offset + 0, offset + 4);
        vh.AddTriangle(offset + 4, offset + 7, offset + 3);
    }
}
