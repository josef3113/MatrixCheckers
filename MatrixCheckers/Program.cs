using System;
using System.Text;

namespace MatrixCheckers
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Playing();

            // Console.ReadLine();
        }

        public static void Playing()
        {

            MatrixCheckers Board = new MatrixCheckers(8);
            // Board.PrintBoard();

            Board.PlayingVessel("Ac>Bd"); // Bf>Ce
            Board.PlayingVessel("Bf>Ce");
            Board.PlayingVessel("Cc>Dd");
            Board.PlayingVessel("Hf>Ge");
            Board.PlayingVessel("Dd>Ee");
            Board.PlayingVessel("Df>Fd");
            Board.PlayingVessel("Ec>Dd");
            Board.PlayingVessel("Fd>Ec");
            Board.PlayingVessel("Bb>Cc");
            //Board.PlayingVessel("Ce>Ec"); // didnt work and it is Good!!!
            Board.PlayingVessel("Ce>Ac");
            Board.PlayingVessel("Ca>Bb");
            //Board.PlayingVessel("Ac>Ca"); // should become to King by number 1King(3), 2King(4)
            //Board.PlayingVessel(""); 
            //Board.PlayingVessel("");
            //Board.PlayingVessel("");
            //Board.PlayingVessel("");
            //Board.PlayingVessel("");
            //Board.PlayingVessel("");

            Board.PrintBoard();

            string str = Console.ReadLine();
            while (char.ToUpper(str[0]) != 'Q')
            {
                Board.PlayingVessel(str);
                Board.PrintBoard();
                str = Console.ReadLine();
            }

        }

    }


    public class MatrixCheckers
    {
        private uint[,] m_Mat;
        private byte m_Size;
        private bool m_GameOn;
        private byte m_NowPlaying;

        public MatrixCheckers(byte i_Size = 8)
        {
            m_Size = i_Size;
            m_Mat = createBoard(m_Size);
            m_GameOn = true;
            m_NowPlaying = 1;
        }

        private uint[,] createBoard(byte i_Size)
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

            StringBuilder buttomLines = new StringBuilder(m_Size * 2 + 20);

            buttomLines.Append('=', m_Size + 2);
            buttomLines.AppendFormat("{0}Playing now -> {1}{0}", Environment.NewLine, m_NowPlaying);
            buttomLines.Append('-', m_Size + 2);

            Console.WriteLine(buttomLines);
        }

        private void changePlayer()
        {
            m_NowPlaying = m_NowPlaying == 1 ? (byte)2 : (byte)1;
        }

        public void PlayingVessel(string i_MovePos) // maybe change to PlayingTurn .
        {

            if (i_MovePos[2] != '>')
            {
                return;
            }

            sbyte vesselOneX, vesselOneY, vesselTwoX, vesselTwoY;
            charsToIndex(out vesselOneX, i_MovePos[0], out vesselOneY, i_MovePos[1]);
            charsToIndex(out vesselTwoX, i_MovePos[3], out vesselTwoY, i_MovePos[4]);

            if (m_Mat[vesselOneY, vesselOneX] != 0 && m_Mat[vesselOneY, vesselOneX] % 2 == m_NowPlaying % 2)
            // the weird -if- is for to know that im on vessel and not empty, and then to know if the vessel is my team. (odd team -1,3-) , (even team -2,4-) .
            // if you pick the right vessel of your team. // Check if i dont out of indexes range!!
            {

                bool goFront = true;

                if (m_Mat[vesselOneY, vesselOneX] <= 3) // mean it's king of one side.!!
                {
                    sbyte distLineY = (sbyte)(vesselOneY - vesselTwoY);
                    isMoveFront(distLineY);
                }

                sbyte vesselMovementLineX = (sbyte)Math.Abs(vesselOneX - vesselTwoX), vesselMovementLineY = (sbyte)Math.Abs(vesselOneY - vesselTwoY);

                if (goFront == true)
                {
                    if (vesselMovementLineX == 1 && vesselMovementLineY == 1)
                    {
                        moveVessel(vesselOneX, vesselOneY, vesselTwoX, vesselTwoY);
                    }
                    else if (vesselMovementLineX == 2 && vesselMovementLineY == 2)
                    {
                        eatEnemyVessel(vesselOneX, vesselOneY, vesselTwoX, vesselTwoY);
                    }
                }
            }
        }

        //private kingVessel() {}

        //private simpleVessel() { }

        private bool moveVessel(sbyte i_IndexOfVesselOneX, sbyte i_IndexOfVesselOneY, sbyte i_IndexOfVesselTwoX, sbyte i_IndexOfVesselTwoY)
        { //// te bool return is for check if all did appened and not need replay turn . if the bool not needed so to replace to void .
            bool isMoved = false;

            if (m_Mat[i_IndexOfVesselTwoY, i_IndexOfVesselTwoX] == 0)
            {
                m_Mat[i_IndexOfVesselTwoY, i_IndexOfVesselTwoX] = m_NowPlaying;
                changePlayer();
                m_Mat[i_IndexOfVesselOneY, i_IndexOfVesselOneX] = 0;

                isMoved = true;
            }

            return isMoved;
        }

        private bool eatEnemyVessel(sbyte i_IndexOfVesselOneX, sbyte i_IndexOfVesselOneY, sbyte i_IndexOfVesselTwoX, sbyte i_IndexOfVesselTwoY)
        { //// te bool return is for check if all did appened and not need replay turn . if the bool not needed so to replace to void .

            bool isEated = false;

            sbyte middleIndexX = (sbyte)((i_IndexOfVesselTwoX + i_IndexOfVesselOneX) / 2), middleIndexY = (sbyte)((i_IndexOfVesselOneY + i_IndexOfVesselTwoY) / 2);

            if (m_Mat[i_IndexOfVesselTwoY, i_IndexOfVesselTwoX] == 0 && m_Mat[middleIndexY, middleIndexX] != m_NowPlaying)
            {
                m_Mat[i_IndexOfVesselTwoY, i_IndexOfVesselTwoX] = m_NowPlaying;
                m_Mat[middleIndexY, middleIndexX] = 0; ////
                m_Mat[i_IndexOfVesselOneY, i_IndexOfVesselOneX] = 0;
                changePlayer();

                isEated = true;
            }

            return isEated;
        }

        private void charsToIndex(out sbyte i_VesselIndexX, char i_CapitalLetterX, out sbyte i_VesselIndexY, char i_SmallLetterY)
        {
            i_VesselIndexX = (sbyte)(i_CapitalLetterX - 'A');
            i_VesselIndexY = (sbyte)(i_SmallLetterY - 'a');
        }

        private bool isMoveFront(sbyte i_DistLineY) // checking if it is go front.!
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

        public bool GameOn() { return m_GameOn; }

    };







}