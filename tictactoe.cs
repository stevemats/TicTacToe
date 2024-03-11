using System;

public class TicTacToe
{
    private char[,] board;
    private char currentPlayer;
    private char player1Symbol;
    private char player2Symbol;
    private int boardSize;

    public TicTacToe(int size, char symbol1, char symbol2)
    {
        boardSize = size;
        board = new char[size, size];
        currentPlayer = ' ';
        player1Symbol = symbol1;
        player2Symbol = symbol2;
        InitializeBoard();
    }

    private void InitializeBoard()
    {
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                board[i, j] = '-';
            }
        }
    }

    public void PrintBoard()
    {
        Console.Clear();
        Console.WriteLine("Current Board:");
        Console.Write("  ");
        for (int i = 0; i < boardSize; i++)
        {
            Console.Write(i + " ");
        }
        Console.WriteLine();
        for (int i = 0; i < boardSize; i++)
        {
            Console.Write(i + " ");
            for (int j = 0; j < boardSize; j++)
            {
                Console.Write(board[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    public bool MakeMove(int row, int col)
    {
        if (!IsMoveValid(row, col))
        {
            Console.WriteLine("Invalid move! Please enter valid row and column numbers.");
            return false;
        }

        board[row, col] = currentPlayer;
        currentPlayer = (currentPlayer == player1Symbol) ? player2Symbol : player1Symbol;
        return true;
    }

    private bool IsMoveValid(int row, int col)
    {
        return row >= 0 && row < boardSize &&
               col >= 0 && col < boardSize &&
               board[row, col] == '-';
    }

    public bool CheckWin()
    {
        // Check rows and columns
        for (int i = 0; i < boardSize; i++)
        {
            if (board[i, 0] != '-' && CheckLine(i, 0, 0, 1))
                return true;

            if (board[0, i] != '-' && CheckLine(0, i, 1, 0))
                return true;
        }

        // Check diagonals
        if (board[0, 0] != '-' && CheckLine(0, 0, 1, 1))
            return true;

        if (board[0, boardSize - 1] != '-' && CheckLine(0, boardSize - 1, 1, -1))
            return true;

        return false;
    }

    private bool CheckLine(int startX, int startY, int dx, int dy)
    {
        char symbol = board[startX, startY];
        for (int i = 0; i < boardSize; i++)
        {
            if (board[startX + i * dx, startY + i * dy] != symbol)
                return false;
        }
        return true;
    }

    public bool IsBoardFull()
    {
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                if (board[i, j] == '-')
                    return false;
            }
        }
        return true;
    }

    public char GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public void StartNewGame()
    {
        InitializeBoard();
        currentPlayer = player1Symbol;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Tic-Tac-Toe!");

        Console.Write("Enter the size of the board (e.g., 3 for 3x3): ");
        int size;
        while (!int.TryParse(Console.ReadLine(), out size) || size < 3)
        {
            Console.WriteLine("Invalid input! Please enter an integer greater than or equal to 3.");
            Console.Write("Enter the size of the board (e.g., 3 for 3x3): ");
        }

        Console.Write("Enter symbol for player 1 (X or O): ");
        char symbol1 = char.ToUpper(Console.ReadKey().KeyChar);
        Console.WriteLine();

        char symbol2 = (symbol1 == 'X') ? 'O' : 'X';

        TicTacToe game = new TicTacToe(size, symbol1, symbol2);
        bool gameOver = false;

        while (!gameOver)
        {
            game.PrintBoard();

            Console.WriteLine("Player " + game.GetCurrentPlayer() + "'s turn.");
            int row, col;
            do
            {
                Console.Write("Enter row number (0 to " + (size - 1) + "): ");
            } while (!int.TryParse(Console.ReadLine(), out row) || row < 0 || row >= size);

            do
            {
                Console.Write("Enter column number (0 to " + (size - 1) + "): ");
            } while (!int.TryParse(Console.ReadLine(), out col) || col < 0 || col >= size);

            if (game.MakeMove(row, col))
            {
                if (game.CheckWin())
                {
                    game.PrintBoard();
                    Console.WriteLine("Player " + game.GetCurrentPlayer() + " wins!");
                    gameOver = true;
                }
                else if (game.IsBoardFull())
                {
                    game.PrintBoard();
                    Console.WriteLine("It's a draw!");
                    gameOver = true;
                }
            }
            else
            {
                Console.WriteLine("Invalid move! Please try again.");
            }

            if (gameOver)
            {
                Console.Write("Do you want to play again? (Y/N): ");
                char playAgain = char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine();
                if (playAgain == 'Y')
                {
                    game.StartNewGame();
                    gameOver = false;
                }
            }
        }
    }
}
