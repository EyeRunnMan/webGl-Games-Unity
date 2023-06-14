using com.eyerunnman.Helper;
using System.Collections.Generic;
using UnityEngine;

namespace com.eyerunnman.MnSwpr
{
    [System.Serializable]
    public struct MineSweeperCellData
    {
        public int CellId { get; private set; }
        public Vector2Int Coordinates { get; private set; }
        public MineSweeperEnums.CellState CellState { get; private set; }
        public MineSweeperEnums.CellType CellType { get; internal set; }
        public int AdjecentMineCount { get; internal set; }
        public Vector2 Position { get; private set; }
        public MineSweeperEnums.CellShape CellShape { get; private set; }

        private readonly List<int> adjecentCellIds;

        private readonly Dictionary<Direction,int> adjecentDirectionCellIds;

        public readonly List<int> AdjecentCellIds => new(adjecentCellIds);
        public int GetCellIdInDirection(Direction dir) => adjecentDirectionCellIds.ContainsKey(dir)? adjecentDirectionCellIds[dir]:-1;
        internal MineSweeperCellData(int cellId, Vector2Int coordinates, Vector2 position, MineSweeperEnums.CellShape cellShape)
        {
            CellId = cellId;
            Coordinates = coordinates;
            Position = position;
            adjecentCellIds = new();
            adjecentDirectionCellIds = new();
            CellType = MineSweeperEnums.CellType.Invalid;
            CellState = MineSweeperEnums.CellState.Hidden;
            CellShape = cellShape;
            AdjecentMineCount = 0;
        }

        internal void RevealCell() => CellState = MineSweeperEnums.CellState.Revealed;
        internal void AddAdjecentCellId(int cellId) => adjecentCellIds.Add(cellId);
        internal void AddAdjecentDirectionCellId(Direction dir,int cellId) => adjecentDirectionCellIds.Add(dir,cellId);
        public static MineSweeperCellData Default => new MineSweeperCellData(-1, new Vector2Int(-1, -1), new Vector2Int(-1, -1), MineSweeperEnums.CellShape.Hexagon);

        public bool IsDefault => CellId == -1;
        public override string ToString()
        {
            return CellId + " " + Coordinates.ToString() + " " + CellState.ToString() + " " + CellType.ToString() + " " + AdjecentMineCount + " " + adjecentCellIds.ToString();
        }

    }
}
