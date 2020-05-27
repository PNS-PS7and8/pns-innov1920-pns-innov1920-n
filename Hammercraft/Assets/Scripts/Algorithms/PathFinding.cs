using System.Collections.Generic;

public static class PathFinding {
    public static bool ComputePath(Board board, Cell origin, Cell destination, int maxDistance, out List<Cell> path) {
        var tree = DepthFirstSearch(board, origin, maxDistance);
        if ((tree.ContainsKey(origin) || tree.ContainsValue(origin)) &&
            (tree.ContainsKey(destination) || tree.ContainsValue(destination)))
        {
            path = new List<Cell>();
            path.Add(origin);
            while(tree[destination] != origin) {
                destination = tree[destination];
                path.Add(destination);
            }
            path.Add(origin);
            path.Reverse();
            return true;
        }
        path = null;
        return false;
    }

    public static IEnumerable<Cell> CellsInReach(Board board, Cell center, int maxDistance) {
        var tree = DepthFirstSearch(board, center, maxDistance);
        foreach(var cell in tree.Keys)
            yield return cell;
        foreach(var cell in tree.Values)
            if (!tree.ContainsKey(cell))
                yield return cell;
    }

    private static Dictionary<Cell, Cell> DepthFirstSearch(Board board, Cell center, int maxDistance) {
        Dictionary<Cell, Cell> tree = new Dictionary<Cell, Cell>();

        Dictionary<Cell, int> distances = new Dictionary<Cell, int>();
        distances.Add(center, 0);

        List<Cell> reachable = new List<Cell>();
        foreach(Cell cell in board.FreeNeighbors(center)) {
            tree[cell] = center;
            distances.Add(cell, 1);
            reachable.Add(cell);
        }
        
        while(reachable.Count > 0) {
            Cell cell = reachable[0];
            int distance = distances[cell];

            if(distance+1 <= maxDistance) {
                distance++;
                foreach(Cell neighbor in board.FreeNeighbors(cell)) {
                    if(!distances.ContainsKey(neighbor) || distances[neighbor] > distance) {
                        distances[neighbor] = distance;
                        tree[neighbor] = cell;
                        reachable.Add(neighbor);
                    }
                }
            }

            reachable.RemoveAt(0);
        }
        
        return tree;
    }
}