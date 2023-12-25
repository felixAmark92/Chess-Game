using System.Collections.Generic;
using MainProject.Behaviours.ChessPieces;

namespace MainProject.ChessMovements.ChessPins;

public interface IPinCalculator
{
    ChessPin CalculatePin();
}