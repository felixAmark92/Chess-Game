using System.Collections.Generic;
using MainProject.BehaviourScripts;
using MainProject.Components;

namespace MainProject.ChessMovements;

public interface IChessMovement
{
    List<Square> GetDefaultSquares();
}