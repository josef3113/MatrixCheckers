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
            Board.MoveVessel("Cc>Dd");
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
        uint[,] m_Mat;
        byte m_Size;
        bool m_GameOn;
        byte m_NowPlaying;

        public MatrixCheckers(byte i_Size = 8)
        {
            m_Size = i_Size;
            m_Mat = CreateBoard(m_Size);
            m_GameOn = true;
            m_NowPlaying = 1;
        }

        private uint[,] CreateBoard(byte i_Size)
        {
            uint[,] matBoard = new uint[i_Size, i_Size];

            for (int i = 0; i < i_Size; i++)
            {
                for (int j = 0; j < i_Size; j++)
                {
                    if (i < (i_Size / 2 - 1) && (i + j) % 2 == 0)
                    {
                        matBoard[i, j] = 1;
                    }

                    if (i >= (i_Size / 2 + 1) && (i + j) % 2 == 0)
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
            for (int i = 0; i < m_Size; i++)
            {
                Console.Write(" {0}", (char)('A' + i));
            }
            Console.WriteLine();
            for (int i = 0; i < m_Size; i++)
            {
                Console.Write("{0} ", (char)('a' + i));
                for (int j = 0; j < m_Size; j++)
                {
                    Console.Write("{0} ", m_Mat[i, j]);
                }

                Console.WriteLine();
            }

        }

        void changePlayer()
        {
            m_NowPlaying = m_NowPlaying == 1 ? (byte)2 : (byte)1;
        }

        public void MoveVessel(string i_MovePos)
        {

            if (i_MovePos[2] != '>')
            {
                return;
            }

            short vesselOneX = (short)(i_MovePos[0] - 'A'), vesselOneY = (short)(i_MovePos[1] - 'a')
                , vesselTwoX = (short)(i_MovePos[3] - 'A'), vesselTwoY = (short)(i_MovePos[4] - 'a');

            if (m_Mat[vesselOneY, vesselOneX] == m_NowPlaying) // if you pick the right vessel of your team.
            {
                short distLineY = (short)(vesselOneY - vesselTwoY);

                bool goFront = isMoveFront(distLineY);

                if (Math.Abs(vesselOneX - vesselTwoX) == 1 && Math.Abs(vesselOneY - vesselTwoY) == 1 && goFront == true)
                // if you go only one move in cross. && the right vessel go front and not to the back.
                { // need to back apart later becuase will be kings and they allowd to go back or front.

                    if (m_Mat[vesselTwoY, vesselTwoX] == 0)
                    {
                        m_Mat[vesselTwoY, vesselTwoX] = m_NowPlaying;
                        changePlayer();
                        m_Mat[vesselOneY, vesselOneX] = 0;
                    }

                }

                if (Math.Abs(vesselOneX - vesselTwoX) == 2 && Math.Abs(vesselOneY - vesselTwoY) == 2 && goFront == true)
                {// eat / beat vessel of the enemy.

                    short middleIndexX = (short)((vesselTwoX + vesselOneX) / 2), middleIndexY = (short)((vesselOneY + vesselTwoY) / 2);

                    if (m_Mat[vesselTwoY, vesselTwoX] == 0 && m_Mat[middleIndexY, middleIndexX] != m_NowPlaying)
                    {
                        m_Mat[vesselTwoY, vesselTwoX] = m_NowPlaying;
                        m_Mat[middleIndexY, middleIndexX] = 0; ////
                        m_Mat[vesselOneY, vesselOneX] = 0;
                        changePlayer();
                    }

                }
            }
        }

        bool isMoveFront(short i_DistLineY) // checking if it is go front.!
        {

            if (m_NowPlaying != 1)
            {
                i_DistLineY *= -1;
            }

            return i_DistLineY < 0 ? true : false;

        }



        //public bool MoveVesselCheck()
        //{


        //}

        bool GameOn() { return m_GameOn; }

    };







}