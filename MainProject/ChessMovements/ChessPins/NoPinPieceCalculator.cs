using MainProject.ChessMovements.ChessPins;

namespace MainProject.Behaviours.ChessPieces;

public class NoPinPieceCalculator : IPinCalculator
{
    public ChessPin CalculatePin()
    {
        return null;
    }
}