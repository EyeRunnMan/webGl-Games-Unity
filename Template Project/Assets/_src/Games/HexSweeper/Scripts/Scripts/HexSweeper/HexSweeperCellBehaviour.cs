using com.eyerunnman.Helper;
using com.eyerunnman.HexSweeper.Audio;
using com.eyerunnman.HexSweeper.Core.Commands;
using com.eyerunnman.MnSwpr;
using com.eyerunnman.patterns;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace com.eyerunnman.HexSweeper.Core
{
    public class HexSweeperCellBehaviour : MonoBehaviour, IMineSweeperCell 
    {
        MnSwpr.MineSweeper mineSweeper = null;

        HexSweeperBehaviourProxy hexSweeperBehaviourProxy;

        internal MineSweeperCellData MineSweeperCellData { get; private set; }

        [SerializeField]
        GameObject CellHiderGO;
        [SerializeField]
        GameObject CellInfoPanelGO;
        [SerializeField]
        GameObject MineSpriteGO;
        [SerializeField]
        TextMeshPro AdjMineCountTextMesh;
        [SerializeField]
        GameObject FlagGO;

        [SerializeField]
        AudioClip flagClip;
        [SerializeField]
        AudioClip unflagClip;
        [SerializeField]
        AudioClip revealClip;
        [SerializeField]
        AudioClip explosionClip;


        AudioManager AudioManager;

        Cooldown flagCooldown;

        [SerializeField]
        Direction directions;

        private void Awake()
        {
            hexSweeperBehaviourProxy = transform.parent.GetComponent<HexSweeperBehaviourProxy>();
            flagCooldown = new Cooldown(0.5f);
        }

        private void Start()
        {
            AudioManager = ServiceLocator.Current.Get<AudioManager>();
        }

        private void OnEnable()
        {
            hexSweeperBehaviourProxy.OnLose += UnFlagCell;
        }

        private void OnDisable()
        {
            hexSweeperBehaviourProxy.OnLose -= UnFlagCell;

        }

        void IMineSweeperCell.OnCellDataSetup(MineSweeperCellData updatedCellData, MnSwpr.MineSweeper mineSweeper)
        {
            this.mineSweeper = mineSweeper;

            MineSweeperCellData = updatedCellData;
            SetCellVisualState(updatedCellData);
            transform.localPosition = new Vector3(updatedCellData.Position.x, 0, updatedCellData.Position.y);

            name = updatedCellData.CellId.ToString() + " " + "(" +updatedCellData.Coordinates+ ")";
            UnFlagCell();
       }

        void IMineSweeperCell.OnCellDataUpdate(MineSweeperCellData updatedCellData)
        {
            MineSweeperCellData = updatedCellData;
            SetCellVisualState(updatedCellData);
        }

        private void Update()
        {
            flagCooldown.OnUpdate();
        }
        internal void FlagCell()
        {
            AudioManager?.PlaySfx(flagClip);
            FlagGO.SetActive(true);
        }
        internal void UnFlagCell()
        {
            AudioManager?.PlaySfx(unflagClip);
            FlagGO.SetActive(false);
        }

        public void OnMouseDown()
        {
            RevealCell();
        }
        public void OnMouseOver()
        {
            if (Input.GetMouseButton(1) && flagCooldown.IsCooldownAvailable)
            {
                FlagCellCommand();
                flagCooldown.OnReset();
            }

            HighlightCell();
        }

        [ContextMenu("Reveal Cell")]
        private void RevealCell()
        {
            ICommand<HexSweeperBehaviour> revealCellCommand = new RevealCell();
            hexSweeperBehaviourProxy.ExecuteCommand(revealCellCommand);
        }

        private void FlagCellCommand()
        {
            ICommand<HexSweeperBehaviour> revealCellCommand = new FlagCell();
            hexSweeperBehaviourProxy.ExecuteCommand(revealCellCommand);
        }

        [ContextMenu("Highlight Cell")]
        private void HighlightCell()
        {
            ICommand<HexSweeperBehaviour> highlightCellCommand = new HighlightCell(MineSweeperCellData.CellId);
            hexSweeperBehaviourProxy.ExecuteCommand(highlightCellCommand);
        }

        private void SetCellVisualState(MineSweeperCellData updatedCellData)
        {
            switch (updatedCellData.CellState)
            {
                case MineSweeperEnums.CellState.Revealed:
                    SetCellRevealed();
                    break;
                default:
                    SetCellHidden();
                    break;
            }

            void SetCellRevealed()
            {
                CellHiderGO.SetActive(false);
                CellInfoPanelGO.SetActive(true);

                switch (updatedCellData.CellType)
                {
                    case MineSweeperEnums.CellType.Empty:
                        AudioManager?.PlaySfx(revealClip);
                        MineSpriteGO.SetActive(false);
                        AdjMineCountTextMesh.gameObject.SetActive(false);
                        break;
                    case MineSweeperEnums.CellType.Mine:
                        AudioManager?.PlaySfx(explosionClip);
                        MineSpriteGO.SetActive(true);
                        AdjMineCountTextMesh.gameObject.SetActive(false);
                        break;
                    case MineSweeperEnums.CellType.Filled:
                        AudioManager?.PlaySfx(revealClip);
                        AdjMineCountTextMesh.gameObject.SetActive(true);
                        AdjMineCountTextMesh.text = updatedCellData.AdjecentMineCount.ToString();
                        MineSpriteGO.SetActive(false);
                        break;
                    default:
                        break;
                }
            }

            void SetCellHidden()
            {
                CellHiderGO.SetActive(true);
                CellInfoPanelGO.SetActive(false);
            }
        }

    }
}
