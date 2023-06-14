using com.eyerunnman.Helper;
using com.eyerunnman.patterns;

namespace com.eyerunnman.HexSweeper.Core.Commands
{
    public class MoveHighlightCellInDirection : ICommand<HexSweeperBehaviour>
    {
        Direction moveToDirection;
        public MoveHighlightCellInDirection(Direction moveToDirection)
        {
            this.moveToDirection = moveToDirection;
        }
        public void Execute(HexSweeperBehaviour context)
        {
            context.MoveHighlightCell(moveToDirection);
        }
    }
}
