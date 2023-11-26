using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinder
{
    private static readonly List<(int, int)> Neighbours = new()
    {
        (-1, -1), (-1, 0), (-1, 1),
        (0, -1),           (0, 1),
        (1, -1),  (1, 0),  (1, 1),
    };
    
    public static List<Vector3Int> FindPath(Vector3 startPosition, Vector3 targetPosition)
    {
        // bring those out of water
        startPosition = FindNearestBank(startPosition);
        targetPosition = FindNearestBank(targetPosition);
        
        Vector2Int start = new((int) startPosition.x, (int) startPosition.y);
        Vector2Int target = new((int) targetPosition.x, (int) targetPosition.y);
        
        // cells that are being searched 
        SortedSet<WeightedCell> searching = new(new WeightedCellComparer());
        
        // cells that has been searched
        Dictionary<Vector2Int, int> visited = new();
        
        // the previous cell that this cell came from
        Dictionary<Vector2Int, Vector2Int> traverse = new();

        // add starting point
        int startDistance = (start - target).sqrMagnitude;
        searching.Add(new WeightedCell(start, startDistance));
        visited.Add(start, 0);

        // set starting search point
        Vector2Int current = start;

        while (searching.Count > 0)
        {
            // get current nearest searching cell
            current = searching.Min.Cell;
            if (current == target) break;
            searching.Remove(searching.Min);

            // check all adjacent tiles
            foreach (var relative in Neighbours)
            {
                Vector2Int check = new Vector2Int(current.x + relative.Item1, current.y + relative.Item2);
                if (!MapManager.Instance.CheckPassable(new Vector3(check.x, check.y, 0))) continue;

                visited.TryGetValue(current, out int currentScore);
                int score = currentScore + 1;
                
                // if not visited or this path has better score then save it
                if (!visited.TryGetValue(check, out int oldScore) || score < oldScore)
                {
                    traverse[check] = current;
                    visited[check] = score;
                    
                    // add or re-add to searching list
                    if (searching.All(s => s.Cell != check))
                    {
                        int distance = (check - target).sqrMagnitude;
                        searching.Add(new WeightedCell(check, score + distance));
                    }
                }
            }
        }

        List<Vector3Int> foundPath = new();
        // reconstruct found path
        while (current != start)
        {
            foundPath.Add(new Vector3Int(current.x, current.y));
            var previous = traverse[current];
            Debug.DrawLine((Vector3Int) current, (Vector3Int) previous, Color.red, 2.0f);
            current = previous;
        }

        return foundPath;
    }
    
    private static Vector3 FindNearestBank(Vector3 position)
    {
        Queue<Vector3> checkQueue = new Queue<Vector3>();
        HashSet<Vector3> failed = new HashSet<Vector3>();
        checkQueue.Enqueue(position);
        
        while (checkQueue.Count > 0)
        {
            Vector3 current = checkQueue.Dequeue();
            if (MapManager.Instance.CheckPassable(current))
            {
                return current;
            }
            failed.Add(current);
            
            foreach (var relative in Neighbours)
            {
                Vector3 check = new Vector3(current.x + relative.Item1, current.y + relative.Item2);
                if (!failed.Contains(check))
                {
                    checkQueue.Enqueue(check);
                }
            } 
        }

        return Vector3.zero;
    }
}


public struct WeightedCell
{
    public Vector2Int Cell;
    public int Weight;

    public WeightedCell(Vector2Int cell, int weight)
    {
        Cell = cell;
        Weight = weight;
    }
}

internal class WeightedCellComparer : IComparer<WeightedCell>
{
    public int Compare(WeightedCell x, WeightedCell y)
    {
        return x.Weight.CompareTo(y.Weight);
    }
}
