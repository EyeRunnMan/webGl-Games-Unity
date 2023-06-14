using System.Collections.Generic;
using UnityEngine;

namespace com.eyerunnman.Helper
{
    public static class HelperClass
    {
        public static int CoordinatesToId(Vector2Int coordinates, Vector2Int gridDimension)
                => gridDimension.x * coordinates.y + coordinates.x;
        public static Vector2Int IdToCoordinates(int cellId, Vector2Int gridDimension)
            => new(cellId % gridDimension.x, cellId / gridDimension.x);
        public static bool AreCoordinatesValid(Vector2Int coordinates, Vector2Int gridDimension)
            => coordinates.x >= 0 && coordinates.x < gridDimension.x && coordinates.y >= 0 && coordinates.y < gridDimension.y;
        public static bool IsCellIdValid(int cellId, Vector2Int gridDimension)
            => cellId >= 0 && cellId < gridDimension.x * gridDimension.y;

        public static int TotalCellCount(Vector2Int gridDimension) => gridDimension.x * gridDimension.y;

        public static List<int> RandomUniqueInts(int start, int end, int count)
        {
            var randomNumberList = new List<int>();
            for (int i = 0; i < count; i++)
            {
                int randomNumber;

                do
                {
                    randomNumber = Random.Range(start, end);
                }
                while (randomNumberList.Contains(randomNumber));

                randomNumberList.Add(randomNumber);
            }
            return randomNumberList;
        }
    }
}