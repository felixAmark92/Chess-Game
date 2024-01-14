﻿using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;

namespace MainProject;

public static class StockFish
{
    public static string GetCommand(string boardState)
    {
        var localApp = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var appdataDirectory = Path.Combine(localApp, "ChessGame");
        Directory.CreateDirectory(appdataDirectory);

        var stockfishPathFile = Path.Combine(appdataDirectory, "StockfishPath");
        var stockfistPath = File.ReadAllText(stockfishPathFile);
        
        
        if (!File.Exists(stockfistPath))
        {
            throw new Exception(
                "Could not find stockfish. please download stockfish and make sure you enter its path into the stockfish project");
        }
        
        using var stockfishProcess = new Process();
        stockfishProcess.StartInfo.FileName = stockfistPath;
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

        stockfishProcess.WaitForExit();

        Console.WriteLine(output);

        var stockFishMoveRegex = new Regex("bestmove ([a-h][1-8][a-h][1-8])");

        var result = stockFishMoveRegex.Match(output);
        
        return result.Groups[1].ToString();

    }
}
