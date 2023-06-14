using com.eyerunnman.Helper;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace com.eyerunnman.MnSwpr
{
    public struct MineSweeperGridData
    {
        private readonly Dictionary<int, MineSweeperCellData> cellDataDict;
        private readonly List<int> mineCellList;

        internal Vector2Int GridDimension { get; private set; }

        internal readonly List<int> MineCellList => mineCellList;

        internal readonly void SetCellData(int cellId, MineSweeperCellData cellData) => cellDataDict[cellId] = cellData;

        internal readonly MineSweeperCellData GetCellData(int cellId)
        {
            return HelperClass.IsCellIdValid(cellId, GridDimension) ? cellDataDict[cellId] : MineSweeperCellData.Default;
        }
        internal readonly MineSweeperCellData GetCellData(Vector2Int coordinates)
        {
            int cellId = HelperClass.CoordinatesToId(coordinates, GridDimension);
            return GetCellData(cellId);
        }

        internal MineSweeperGridData(Vector2Int gridDimension, List<MineSweeperCellData> cellDataList)
        {
            GridDimension = gridDimension;
            mineCellList = cellDataList.Where((celldata) => { return celldata.CellType == MineSweeperEnums.CellType.Mine; })
                                            .Select((MineSweeperCellData cellData) => cellData.CellId)
                                            .ToList();

            cellDataDict = new();

            foreach (var cellData in cellDataList)
            {
                cellDataDict.Add(cellData.CellId, cellData);
            }
        }

        internal void RevealCell(Vector2Int cellCoordinates)
        {
            MineSweeperCellData cellData = GetCellData(cellCoordinates);

            if (cellData.CellState == MineSweeperEnums.CellState.Revealed)
                return;

            cellData.RevealCell();
            SetCellData(cellData.CellId, cellData);

        }
        internal int TotalCellCount => HelperClass.TotalCellCount(GridDimension);
        internal int MineCount => mineCellList.Count;
        internal MineSweeperEnums.Status ComputeStatus()
        {
            int nonMineRevealedCount = 0;

            for (int i = 0; i < TotalCellCount; i++)
            {
                MineSweeperCellData cellData = GetCellData(i);
                if (cellData.CellType == MineSweeperEnums.CellType.Mine && cellData.CellState == MineSweeperEnums.CellState.Revealed)
                {
                    return MineSweeperEnums.Status.RevealedAllMines;
                }
                if (cellData.CellType != MineSweeperEnums.CellType.Mine && cellData.CellState == MineSweeperEnums.CellState.Revealed)
                {
                    nonMineRevealedCount++;
                }
            }

            if ((TotalCellCount - MineCount) == nonMineRevealedCount)
            {
                return MineSweeperEnums.Status.RevealedAllNonMines;
            }

            return MineSweeperEnums.Status.Default;
        }

    }
}
