using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable, System.Flags  ]
public enum UnitMoveMask {
    PathFinding   = 1 << 0,
    FlyBy         = 1 << 1,
    OnWater       = 1 << 2,
    OnField       = 1 << 3,
    OnMountain    = 1 << 4,
    NormalCells   = 1 << 5,
    SpecialCells  = 1 << 6,
    AllyCells     = 1 << 7,
    EnnemyCells   = 1 << 8,
    NeutralCells  = 1 << 9,
    AnyTerrain    = OnWater | OnField | OnMountain,
    AnyCellOwner  = AllyCells | EnnemyCells | NeutralCells,
}

public static class UnitMovement {
    public static IEnumerable<Cell> AvailableCells(UnitMoveMask value, Board board, Cell origin, int maxDistance) {
        if (Check(value, UnitMoveMask.PathFinding)) {
            foreach (var cell in PathFinding.CellsInReach(board, origin, maxDistance, value))
            {
                yield return cell;
            }
        } else if (Check(value, UnitMoveMask.FlyBy)) {
            foreach (var cell in board.Disc(origin, maxDistance))
            {
                if (CheckCell(value, board, cell))
                    yield return cell;
            }
        }
    }

    public static bool CanMove(UnitMoveMask value, Board board, Cell origin, Cell target, int maxDistance, out List<Cell> path) {
        if (Check(value, UnitMoveMask.PathFinding)) {
            return PathFinding.ComputePath(board, origin, target, maxDistance, value, out path);
        } else if (Check(value, UnitMoveMask.FlyBy) && CheckCell(value, board, target)) {
            if (target.Distance(origin) <= maxDistance*2) {
                path = new List<Cell> {origin, target};
                return true;
            }
        }
        path = null;
        return false;
    }

    private static bool Check(UnitMoveMask value, UnitMoveMask mask) {
        return (value & mask) != 0;
    }

    public static bool CheckCell(UnitMoveMask value, Board board, Cell cell) {
        return
            TestTerrain(value, board, cell) &&
            TestUnits(value, board, cell);
    }

    private static bool TestTerrain(UnitMoveMask value, Board board, Cell cell) {
        return
            value == UnitMoveMask.AnyTerrain ||
            Check(value, UnitMoveMask.OnWater) && cell.cellType == Cell.CellType.Water ||
            Check(value, UnitMoveMask.OnField) && cell.cellType == Cell.CellType.Field ||
            Check(value, UnitMoveMask.OnMountain) && cell.cellType == Cell.CellType.Mountain;
    }

    private static bool TestUnits(UnitMoveMask value, Board board, Cell cell) {
        var unit = board.GetUnit(cell);
        return unit == null || unit.Dead;
    }
}
