using com.eyerunnman.MnSwpr;
using UnityEngine;

namespace com.eyerunnman.HexSweeper.Core
{
    [CreateAssetMenuAttribute(fileName = "HexSweeperSettings_Container", menuName = "HexSweeper/HexSweeperSettings_Container", order = 1)]
    public class HexSweeperSettingsContainer : ScriptableObject
    {
        [SerializeField]
        private HexSweeperCellBehaviour cellPrefab;

        [SerializeField]
        private HexSweeperSelectedCellHighlight cellHighlightSelector;

        [SerializeField]
        private MineSweeperSettings mineSweeperSettings;

        public MineSweeperSettings MineSweeperSettings => mineSweeperSettings;
        public HexSweeperCellBehaviour CellPrefab => cellPrefab;
        public HexSweeperSelectedCellHighlight CellHighlightSelector => cellHighlightSelector;

    }
}

