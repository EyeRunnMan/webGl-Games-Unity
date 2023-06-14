using com.eyerunnman.MnSwpr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.eyerunnman.HexSweeper.Core
{ 
    public class HexSweeperSelectedCellHighlight : MonoBehaviour
    {
        public MineSweeperCellData HilightedCellData { get; private set; }
        public void ToggleHighlight(bool status)
        {
            gameObject.SetActive(status);
        }

        public void SetHighlightPosition(MineSweeperCellData mineSweeperCellData)
        {
            HilightedCellData = mineSweeperCellData;
            transform.localPosition = new(mineSweeperCellData.Position.x,0, mineSweeperCellData.Position.y);
        }
    }
}