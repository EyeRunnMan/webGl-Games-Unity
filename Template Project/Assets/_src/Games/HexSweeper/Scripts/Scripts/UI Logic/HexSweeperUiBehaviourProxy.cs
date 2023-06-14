using com.eyerunnman.HexSweeper.Core;
using com.eyerunnman.patterns;
using UnityEngine;

namespace com.eyerunnman.HexSweeper.Ui
{
    [RequireComponent(typeof(HexSweeperUiBehaviour))]
    public class HexSweeperUiBehaviourProxy : MonoBehaviour, IProxy<HexSweeperUiBehaviour>
    {
        HexSweeperUiBehaviour context;

        private void Awake()
        {
            context = GetComponent<HexSweeperUiBehaviour>();
        }

        public void ExecuteCommand(ICommand<HexSweeperUiBehaviour> command)
        {
            command.Execute(context);
        }

    }
}