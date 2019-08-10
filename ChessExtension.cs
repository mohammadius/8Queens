using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace _8Queens
{
    public static class ChessExtension
    {
        public static Task Search_ForwardChecking(this List<List<ChessSquare>> chessBoard, int delay)
        {
            int queenCount = 0;
            int[] availableQueenY = { 0, 0, 0, 0, 0, 0, 0, 0 };
            return Task.Run(() =>
            {
                while (queenCount != 8)
                {
                    chessBoard.ForEach(cb => cb.ForEach(c => c.Reset()));
                    queenCount = 0;
                    int[] aq = { 0, 0, 0, 0, 0, 0, 0, 0 };

                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (!chessBoard[j][i].Accessible) continue;
                            if (availableQueenY[i] != aq[i]++) continue;
                            chessBoard[j][i].HasQueen = true;
                            queenCount++;
                            MarkUnAccessible(j, i);
                            break;
                        }
                        Thread.Sleep(delay);
                    }

                    IncrementQueenPick();
                }
            });

            void IncrementQueenPick()
            {
                availableQueenY[0]++;
                for (int j = 0; j < availableQueenY.Length - 1; j++)
                {
                    if (availableQueenY[j] == 8)
                    {
                        availableQueenY[j] = 0;
                        availableQueenY[j + 1]++;
                    }
                }
            }

            void MarkUnAccessible(int x, int y)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (j == y) continue;
                    chessBoard[x][j].Accessible = false;
                }
                for (int j = 0; j < 8; j++)
                {
                    if (j == x) continue;
                    chessBoard[j][y].Accessible = false;
                }
                for (int j = 1; x + j <= 7 && y + j <= 7; j++)
                {
                    chessBoard[x + j][y + j].Accessible = false;
                }
                for (int j = 1; x - j >= 0 && y - j >= 0; j++)
                {
                    chessBoard[x - j][y - j].Accessible = false;
                }
                for (int j = 1; x + j <= 7 && y - j >= 0; j++)
                {
                    chessBoard[x + j][y - j].Accessible = false;
                }
                for (int j = 1; x - j >= 0 && y + j <= 7; j++)
                {
                    chessBoard[x - j][y + j].Accessible = false;
                }
            }
        }
    }
}