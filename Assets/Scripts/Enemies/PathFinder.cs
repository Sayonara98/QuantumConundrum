using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder
{

    public static List<Vector3Int> FindPath(Vector3 startPosition, Vector3 targetPosition)
    {
        Vector3Int startCell = MapManager.Instance.GetCell(startPosition);
        Vector3Int targetCell = MapManager.Instance.GetCell(targetPosition);

        Vector2Int start = new(startCell.x, startCell.y);
        Vector2Int target = new(targetCell.x, targetCell.y);

        // Cells that are being searched 
        SortedSet<WeightedCell> searching = new(new WeightedCellComparer());

        // The previous cell that this cell came from
        Dictionary<Vector2Int, Vector2Int> traverse = new();

        // Add starting point
        int startDistSqr = (start - target).sqrMagnitude;
        searching.Add(new WeightedCell(start, startDistSqr));

        Vector2Int current = start;

        //while (current != destination)
        for (int i=0; i<100; i++)
        {
            current = searching.Min.cell;
            searching.Remove(searching.Min);
            int nextDist = int.MaxValue;
            Vector2Int nextPos = Vector2Int.zero;
            bool hasNext = false;

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0) continue;

                    Vector2Int check = current + new Vector2Int(x, y);
                    if (!MapManager.Instance.CheckPassable(new Vector3(check.x, check.y, 0))) continue;
                    foreach (var s in searching)
                    {
                        if (s.cell == check) continue;
                    }

                    int distSqr = (check - target).sqrMagnitude;
                    if (distSqr < nextDist)
                    {
                        nextDist = distSqr;
                        nextPos = check;
                        hasNext = true;
                    }
                }
            }

            if (hasNext)
            {
                Debug.Log($"{nextPos} : {nextDist}");
                Debug.DrawLine(((Vector3Int)current), (Vector3Int)nextPos, Color.red, 3.0f);
                searching.Add(new WeightedCell(nextPos, nextDist));
                traverse[nextPos] = current;
            }
        }

        List<Vector3Int> foundPath = new();
        // Reconstruct
        //while (current != start)
        for (int i=0; i<100; i++)
        {
            foundPath.Add(new Vector3Int(current.x, current.y));
            var previous = traverse[current];
            current = previous;
        }

        return foundPath;
    }
}

public struct WeightedCell
{
    public Vector2Int cell;
    public int weight;

    public WeightedCell(Vector2Int cell, int weight)
    {
        this.cell = cell;
        this.weight = weight;
    }

    public static bool operator ==(WeightedCell left, WeightedCell right)
    {
        return left.cell == right.cell;
    }

    public static bool operator !=(WeightedCell left, WeightedCell right)
    {
        return left.cell != right.cell;
    }

    public override bool Equals(object obj)
    {
        if (obj is WeightedCell compare)
        {
            return cell == compare.cell;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return cell.GetHashCode();
    }
}

internal class WeightedCellComparer : IComparer<WeightedCell>
{
    public int Compare(WeightedCell x, WeightedCell y)
    {
        return x.weight.CompareTo(y.weight);
    }
}
