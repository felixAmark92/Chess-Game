using MainProject.BehaviourScripts.ChessPieces;

namespace MainProject.BehaviourScripts;

public class GhostPawn
{
    public PawnPiece PawnPiece { get; set; }

    public Square Square { get; set; }


    public GhostPawn(PawnPiece pawnPiece, Square square)
    {
        PawnPiece = pawnPiece;
        Square = square;
    }
}