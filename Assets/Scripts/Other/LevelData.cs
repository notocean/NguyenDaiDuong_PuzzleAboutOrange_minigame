using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Data/LevelData")]
public class LevelData: ScriptableObject 
{
    public int index;

    public Vector2Int orange1Coordinate;
    public Vector2Int orange2Coordinate;
    public Vector2Int orange3Coordinate;
    public Vector2Int orange4Coordinate;
    public Vector2Int block1Coordinate;
    public Vector2Int block2Coordinate;
    public Vector2Int block3Coordinate;

    public bool isActive;
    public int starCount;
}
