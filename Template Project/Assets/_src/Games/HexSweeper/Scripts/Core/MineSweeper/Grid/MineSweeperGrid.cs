using com.eyerunnman.Helper;
using System.Collections.Generic;
using UnityEngine;

namespace com.eyerunnman.MnSwpr
{
    internal partial class MineSweeperGrid
    {
        private MineSweeperGridData gridData;
        internal MineSweeperGrid(MineSweeperGridData gridData)
        {
            this.gridData = gridData;
        }
        internal List<int> RevealCell(Vector2Int cellCoordinates, List<int> revealedIds = null)
        {
            List<int> revealedcellIds = revealedIds ?? new();

            MineSweeperCellData cellData = gridData.GetCellData(cellCoordinates);
            int cellId = cellData.CellId;

            if (cellData.CellState == MineSweeperEnums.CellState.Revealed)
                return revealedcellIds;

            switch (cellData.CellType)
            {
                case MineSweeperEnums.CellType.Empty:
                    RevealAllCellsInEmptyRegion();
                    break;
                case MineSweeperEnums.CellType.Mine:
                    RevealAllMineCells();
                    break;
                case MineSweeperEnums.CellType.Filled:
                    RevealIndividualCellCallback(cellId);
                    break;
                default:
                    break;
            }

            return revealedcellIds;

            #region LocalFunctions
            void RevealAllMineCells()
            {
                foreach (var cellId in gridData.MineCellList)
                {
                    RevealIndividualCellCallback(cellId);
                }
            }
            void RevealAllCellsInEmptyRegion()
            {
                revealedcellIds.Add(cellId);
                gridData.RevealCell(cellCoordinates);

                foreach (var cellId in cellData.AdjecentCellIds)
                {
                    Vector2Int cellCoordinates = HelperClass.IdToCoordinates(cellId, gridData.GridDimension);
                    MineSweeperCellData cellData = gridData.GetCellData(cellCoordinates);
                    switch (cellData.CellType)
                    {
                        case MineSweeperEnums.CellType.Empty:
                            RevealCell(cellCoordinates, revealedcellIds);
                            break;
                        case MineSweeperEnums.CellType.Filled:
                            RevealIndividualCellCallback(cellId);
                            break;
                        default:
                            break;
                    }
                }

            }

            void RevealIndividualCellCallback(int cellId)
            {
                Vector2Int cellCoordinates = HelperClass.IdToCoordinates(cellId, gridData.GridDimension);
                revealedcellIds.Add(cellId);
                gridData.RevealCell(cellCoordinates);
            }
            #endregion
        }

        internal MineSweeperCellData GetCellDataInDirection(MineSweeperCellData cellData, Direction directions)
        {
            int otherCellId = cellData.GetCellIdInDirection(directions);
            if (HelperClass.IsCellIdValid(otherCellId, gridData.GridDimension))
            {
                return GetCellData(otherCellId);
            }

            return MineSweeperCellData.Default;
        }

        internal int GetCellIdInDirection(int cellId, Direction directions)
        {
            MineSweeperCellData CellData = GetCellData(cellId);

            MineSweeperCellData otherCellData = GetCellDataInDirection(CellData, directions);

            return otherCellData.CellId;
        }
        internal MineSweeperCellData GetCellData(int cellId) => gridData.GetCellData(cellId);
        internal MineSweeperCellData GetCellData(Vector2Int cellCoordinates) => gridData.GetCellData(cellCoordinates);
        internal MineSweeperEnums.Status ComputStatus() => gridData.ComputeStatus();
        internal int TotalCellCount => gridData.TotalCellCount;
        internal int MineCount => gridData.MineCount;
        internal List<MineSweeperCellData> MineSweeperCellDataList
        {
            get
            {
                List<MineSweeperCellData> cellDataList = new();
                for (int cellId = 0; cellId < TotalCellCount; cellId++)
                {
                    cellDataList.Add(GetCellData(cellId));
                }

                return cellDataList;
            }
        }

    }
}

