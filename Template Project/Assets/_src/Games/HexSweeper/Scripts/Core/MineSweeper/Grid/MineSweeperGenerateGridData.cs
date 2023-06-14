using com.eyerunnman.Helper;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace com.eyerunnman.MnSwpr
{
    public class MineSweeperGenerateGridData 
    {
        internal static MineSweeperGridData GenerateRandomGridData(Grid grid, MineSweeperSettings settings)
        {
            switch (settings.CellShape)
            {
                case MineSweeperEnums.CellShape.Square:
                    grid.cellLayout = GridLayout.CellLayout.Rectangle;
                    break;
                case MineSweeperEnums.CellShape.Hexagon:
                    grid.cellLayout = GridLayout.CellLayout.Hexagon;
                    break;
            }

            grid.cellSwizzle = settings.CellSwizzle;
            grid.cellSize = settings.CellSize;
            int TotalCellCount = settings.TotalCellCount;
            List<int> mineCellIdList = HelperClass.RandomUniqueInts(0, TotalCellCount, settings.TotalMineCount);

            List<MineSweeperCellData> cellDataList = new List<MineSweeperCellData>();

            for (int cellId = 0; cellId < TotalCellCount; cellId++)
            {
                Vector2Int coordinates = HelperClass.IdToCoordinates(cellId, settings.GridDimension);
                Vector2 position = grid.GetCellCenterLocal(new(coordinates.x, coordinates.y, 0));
                cellDataList.Add(new(cellId, coordinates, position, settings.CellShape));
            }

            foreach (var cellData in cellDataList.ToList())
            {
                int leftLimit = -1;
                int rightLimit = 1;
                int topLimit = 1;
                int bottomLimit = -1;

                List<int> adjecentcellId = new();
                Dictionary<Direction, int> adjecentDirectioncellId = new();


                for (int i = leftLimit; i <= rightLimit; i++)
                {
                    for (int j = bottomLimit; j <= topLimit; j++)
                    {
                        if (i == j && i == 0)
                        {
                            continue;
                        }


                        Vector2Int adjCoordinates = cellData.Coordinates + new Vector2Int(i, j);
                        int adjCellId = HelperClass.CoordinatesToId(adjCoordinates, settings.GridDimension);

                        if (HelperClass.AreCoordinatesValid(adjCoordinates, settings.GridDimension))
                        {
                            MineSweeperCellData data = cellDataList[adjCellId];
                            if (Vector2.Distance(data.Position, cellData.Position) <= settings.AdjecentCellMaxDist)
                            {
                                adjecentcellId.Add(data.CellId);

                                if (i == 0 || j == 0)
                                {
                                    if (j == 1)
                                        adjecentDirectioncellId.Add(Direction.North, data.CellId);
                                    else if (j == -1)
                                        adjecentDirectioncellId.Add(Direction.South, data.CellId);
                                    else if (i == 1)
                                        adjecentDirectioncellId.Add(Direction.East, data.CellId);
                                    else if (i == -1)
                                        adjecentDirectioncellId.Add(Direction.West, data.CellId);
                                }

                            }

                        }
                    }
                }

                MineSweeperCellData updateCellData = cellData;
                foreach (var item in adjecentcellId)
                {
                    updateCellData.AddAdjecentCellId(item);
                }
                foreach (var item in adjecentDirectioncellId)
                {
                    updateCellData.AddAdjecentDirectionCellId(item.Key, item.Value);
                }

                cellDataList[cellData.CellId] = updateCellData;
            }

            foreach (var cellData in cellDataList.ToList())
            {
                int adjMineCount = (
                                        from cellId in cellData.AdjecentCellIds
                                        where mineCellIdList.Contains(cellId)
                                        select cellId
                                    ).Count();

                MineSweeperCellData updateCellData = cellData;
                updateCellData.AdjecentMineCount = adjMineCount;

                if (mineCellIdList.Contains(cellData.CellId))
                {
                    updateCellData.CellType = MineSweeperEnums.CellType.Mine;
                }
                else if (adjMineCount > 0)
                {
                    updateCellData.CellType = MineSweeperEnums.CellType.Filled;
                }
                else
                {
                    updateCellData.CellType = MineSweeperEnums.CellType.Empty;
                }
                cellDataList[cellData.CellId] = updateCellData;
            }

            MineSweeperGridData generatedGridData = new(settings.GridDimension, cellDataList);
            return generatedGridData;
        }
    }
}