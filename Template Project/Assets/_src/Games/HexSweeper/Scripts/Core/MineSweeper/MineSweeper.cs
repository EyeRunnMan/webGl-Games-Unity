using com.eyerunnman.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace com.eyerunnman.MnSwpr
{
    public partial class MineSweeper
    {
        private readonly MineSweeperGrid MineSweeperGridData;
        Dictionary<int, IMineSweeperCell> HexSweeperCellRefList;
        public MineSweeper(Grid grid, MineSweeperSettings settings, HashSet<IMineSweeperCell> hexSweeperCellSet)
        {
            if (hexSweeperCellSet.Count != settings.TotalCellCount)
            {
                Debug.Log("Invalid HexSweeper Constructor Params");
                return;
            }

            MineSweeperGridData = new(GeneratedGridData(grid, settings));

            SetupCellRefs(hexSweeperCellSet);

            #region Local Methods
            void SetupCellRefs(HashSet<IMineSweeperCell> hexSweeperCellSet)
            {
                HexSweeperCellRefList = new();
                foreach (var hexSweeperCell in hexSweeperCellSet.Select((refrence, idx) => new { refrence, idx }))
                {
                    HexSweeperCellRefList[hexSweeperCell.idx] = hexSweeperCell.refrence;
                    MineSweeperCellData cellData = MineSweeperGridData.GetCellData(hexSweeperCell.idx);

                    hexSweeperCell.refrence.OnCellDataSetup(cellData, this);
                }
            }
            static MineSweeperGridData GeneratedGridData(Grid grid, MineSweeperSettings settings)
            {
                return MnSwpr.MineSweeperGenerateGridData.GenerateRandomGridData(grid, settings);
            }
            #endregion
        }
        public Action<MineSweeperEnums.Status> OnUpdateHexStatus { get; set; }

        public List<MineSweeperCellData> RevealCell(MineSweeperCellData hexSweeperCellData)
        {
            List<int> revealedcellIdList = MineSweeperGridData.RevealCell(hexSweeperCellData.Coordinates);
            List<MineSweeperCellData> revealedCellDataList = new();
            foreach (var cellId in revealedcellIdList)
            {
                revealedCellDataList.Add(MineSweeperGridData.GetCellData(cellId));
            }

            OnUpdateHexStatus?.Invoke(MineSweeperGridData.ComputStatus());
            Debug.Log(MineSweeperGridData.ComputStatus());

            UpdateCellRefs(revealedCellDataList);

            void UpdateCellRefs(List<MineSweeperCellData> revealedCellDataList)
            {
                foreach (var cellData in revealedCellDataList)
                {
                    if (HexSweeperCellRefList.TryGetValue(cellData.CellId, out IMineSweeperCell cellRef))
                    {
                        cellRef.OnCellDataUpdate(cellData);
                    }
                }
            }

            return revealedCellDataList;
        }

        public List<MineSweeperCellData> RevealCell(int hexSweeperCellId)
        {
            MineSweeperCellData cellToReveal = MineSweeperGridData.GetCellData(hexSweeperCellId);

            if (cellToReveal.IsDefault)
            {
                return new();
            }

            return RevealCell(cellToReveal);
        }

        public MineSweeperCellData GetCellDataInDirection(MineSweeperCellData data,Direction directions) => MineSweeperGridData.GetCellDataInDirection(data,directions);
        public int GetCellIdInDirection(int cellId, Direction directions) => MineSweeperGridData.GetCellIdInDirection(cellId, directions);

        public MineSweeperCellData GetCellData(int cellId)=> MineSweeperGridData.GetCellData(cellId);
        public MineSweeperEnums.Status Status => MineSweeperGridData.ComputStatus();
        public int TotalCellCount => MineSweeperGridData.TotalCellCount;
        public int MineCount => MineSweeperGridData.MineCount;
    }
}
