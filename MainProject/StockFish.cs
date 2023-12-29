using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace MainProject;

public static class StockFish
{

    private static readonly string _stockFishPath = "C:\\Users\\felix\\Downloads\\stockfish-windows-x86-64-avx2\\stockfish\\stockfish-windows-x86-64-avx2.exe";

    public static string GetCommand(string boardState)
    {
        if (!File.Exists(_stockFishPath))
        {
            throw new Exception(
                "Could not find stockfish. please download stockfish and make sure you enter its path in the _stockFishPath string above");
        }
        
        using var stockfishProcess = new Process();
        stockfishProcess.StartInfo.FileName = _stockFishPath;
        stockfishProcess.StartInfo.UseShellExecute = false;
        stockfishProcess.StartInfo.RedirectStandardInput = true;
        stockfishProcess.StartInfo.RedirectStandardOutput = true;
        stockfishProcess.StartInfo.CreateNoWindow = true;

        stockfishProcess.Start();
            
        StreamWriter stockfishStreamWriter = stockfishProcess.StandardInput;
        StreamReader stockfishStreamReader = stockfishProcess.StandardOutput;
            
        stockfishStreamWriter.WriteLine($"position fen {boardState}");
        stockfishStreamWriter.WriteLine($"go depth 8");
        Thread.Sleep(1000);
        stockfishStreamWriter.WriteLine("quit");
        string output = stockfishStreamReader.ReadToEnd();
        // Close the Stockfish process
        stockfishProcess.WaitForExit();

        Console.WriteLine(output);

        var stockFishMoveRegex = new Regex("bestmove ([a-h][1-8][a-h][1-8])");

        var result = stockFishMoveRegex.Match(output);
        
        return result.Groups[1].ToString();

    }
}
