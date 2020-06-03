using System.Linq;
using UnityEngine;

[System.Serializable, System.Flags]
public enum CastMask {
    EmptyCell     = 1 << 0,
    OnUnit        = 1 << 1,
    NearUnit      = 1 << 2,
    OnWater       = 1 << 3,
    OnField       = 1 << 4,
    OnMountain    = 1 << 5,
    NormalCells   = 1 << 6,
    SpecialCells  = 1 << 7,
    AllyCells     = 1 << 8,
    EnnemyCells   = 1 << 9,
    NeutralCells  = 1 << 10,
    AnyPosition   = EmptyCell | OnUnit | NearUnit,
    AnyTerrain    = OnWater | OnField | OnMountain,
    AnyCellOwner  = AllyCells | EnnemyCells | NeutralCells,
}

public static class CardCast {
    public static bool CanCast(CastMask value, Board board, Cell target) {
        return
            TestTerrainCondition(value, board, target) &&
            TestOwnerCondition(value, board, target) &&
            TestUnitCondition(value, board, target);
    }

    private static bool TestUnitCondition(CastMask value, Board board, Cell target) {
        var unit = board.GetUnit(target);
        return
            value == CastMask.AnyPosition ||
            Check(value, CastMask.EmptyCell) && (unit == null || unit.Dead) ||
            Check(value, CastMask.OnUnit) && unit != null && !unit.Dead ||
            Check(value, CastMask.NearUnit) && board.Neighbors(target).Any(cell => board.GetUnit(cell) != null);
    }

    private static bool TestTerrainCondition(CastMask value, Board board, Cell target) {
        return
            value == CastMask.AnyTerrain ||
            Check(value, CastMask.OnWater) && target.cellType == Cell.CellType.Water ||
            Check(value, CastMask.OnField) && target.cellType == Cell.CellType.Field ||
            Check(value, CastMask.OnMountain) && target.cellType == Cell.CellType.Mountain;
    }

    private static bool TestOwnerCondition(CastMask value, Board board, Cell target) {
        return
            value == CastMask.AnyCellOwner ||
            Check(value, CastMask.NeutralCells) ||
            Check(value, CastMask.AllyCells) ||
            Check(value, CastMask.EnnemyCells);
    }

    private static bool Check(CastMask value, CastMask mask) {
        return (value & mask) != 0;
    }
}
