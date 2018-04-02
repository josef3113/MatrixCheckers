using System;

namespace MatrixCheckers
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Playing();

            Console.ReadLine();
        }

        public static void Playing()
        {

            MatrixCheckers Board = new MatrixCheckers(8);
            Board.PrintBoard();

            Board.MoveVessel("Ac>Bd"); // Bf>Ce
            Board.MoveVessel("Bf>Ce");
            Board.PrintBoard();

            for (int i = 0; i < 4; i++)
            {
                string str = Console.ReadLine();
                Board.MoveVessel(str);
                Board.PrintBoard();
            }


        }


    }


    public class MatrixCheckers
    {
        uint[,] mat;
        byte size;
        bool gameOn;
        byte nowPlaying;

        public MatrixCheckers(byte i_Size = 8)
        {
            size = i_Size;
            mat = CreateBoard(size);
            gameOn = true;
            nowPlaying = 1;
        }

        private uint[,] CreateBoard(byte size)
        {
            uint[,] matBoard = new uint[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i < (size / 2 - 1) && (i + j) % 2 == 0)
                    {
                        matBoard[i, j] = 1;
                    }

                    if (i >= (size / 2 + 1) && (i + j) % 2 == 0)
                    {
                        matBoard[i, j] = 2;
                    }
                }
            }

            return matBoard;
        }

        public void PrintBoard()
        {
            Console.WriteLine();
            Console.Write(" ");
            for (int i = 0; i < size; i++)
            {
                Console.Write(" {0}", (char)('A' + i));
            }
            Console.WriteLine();
            for (int i = 0; i < size; i++)
            {
                Console.Write("{0} ", (char)('a' + i));
                for (int j = 0; j < size; j++)
                {
                    Console.Write("{0} ", mat[i, j]);
                }

                Console.WriteLine();
            }

        }

        void changePlayer()
        {
            nowPlaying = nowPlaying == 1 ? (byte) 2 : (byte) 1;
        }

        public void MoveVessel(string movePos)
        {
            
            if (movePos[2] != '>')
            {
                return;
            }

            short vesselOneX = (short)(movePos[0] - 'A'), vesselOneY = (short)(movePos[1] - 'a')
                , vesselTwoX = (short)(movePos[3] - 'A'), vesselTwoY = (short)(movePos[4] - 'a');

            if (mat[vesselOneY, vesselOneX] == nowPlaying) // if you pick the right vessel of your team.
            {
                short distLineY = (short) (vesselOneY - vesselTwoY);

                bool goFront = false;

                if (nowPlaying == 1)
                {
                    goFront = vesselOneY - vesselTwoY < 0 ? true : false ;
                }
                else
                {
                    goFront = vesselOneY - vesselTwoY < 0 ? false : true ;
                }
                    

                if (Math.Abs(vesselOneX - vesselTwoX) == 1 && Math.Abs(vesselOneY - vesselTwoY) == 1 && goFront == true ) 
                    // if you go only one move in cross. && the right vessel go front and not to the back.
                { // need to back apart later becuase will be kings and they allowd to go back or front.

                    if (mat[vesselTwoY, vesselTwoX] == 0)
                    {
                        mat[vesselTwoY, vesselTwoX] = nowPlaying;
                        changePlayer();
                        mat[vesselOneY, vesselOneX] = 0;
                    }

                }
            }
        }

        bool isMoveFront(short i_DistLineY) // checking if it is go front.!
        {
           
            if (nowPlaying != 1)
            {
                i_DistLineY *= -1;
            }

            return i_DistLineY < 0 ? true : false;

        }



        //public bool MoveVesselCheck()
        //{


        //}

        bool GameOn() { return gameOn; }

    };







}