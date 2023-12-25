using System.Collections.Generic;
using MainProject.Components;

namespace MainProject.ChessMovements;

public interface IChessMovement
{
    List<Square> GetMovableSquares();
}