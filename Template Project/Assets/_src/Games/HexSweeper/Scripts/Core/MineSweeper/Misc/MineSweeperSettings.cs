using com.eyerunnman.Helper;
using UnityEngine;

namespace com.eyerunnman.MnSwpr
{
    [System.Serializable]
    public struct MineSweeperSettings
    {
        public int TotalCellCount => HelperClass.TotalCellCount(GridDimension);
        public Vector3 CellSize
        {
            get
            {
                return CellShape switch
                {
                    MineSweeperEnums.CellShape.Square => Vector3.one,
                    MineSweeperEnums.CellShape.Hexagon => new(Mathf.Sqrt(0.75f), 1, 1),
                    _ => new(),
                };
            }
        }
        public float AdjecentCellMaxDist
        {
            get
            {
                return CellShape switch
                {
                    MineSweeperEnums.CellShape.Square => 1.5f,
                    MineSweeperEnums.CellShape.Hexagon => 1f,
                    _ => 0,
                };
            }
        }
        public Grid.CellSwizzle CellSwizzle
        {
            get
            {
                return CellShape switch
                {
                    MineSweeperEnums.CellShape.Hexagon => GridLayout.CellSwizzle.YXZ,
                    _ => GridLayout.CellSwizzle.XYZ,
                };
            }
        }

        [SerializeField]
        private Vector2Int gridDimension;
        [SerializeField]
        private int totalMineCount;
        [SerializeField]
        private MineSweeperEnums.CellShape cellShape;

        public Vector2Int GridDimension => gridDimension;
        public int TotalMineCount => totalMineCount;
        public MineSweeperEnums.CellShape CellShape => cellShape;
    }
}
