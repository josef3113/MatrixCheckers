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
            Board.PlayingVessel("Ac>Ca"); // should become to King by number 1King(3), 2King(4)
            Board.PlayingVessel("Dd>Ce");
            Board.PlayingVessel("Ca>Bb");
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
        private byte[,] m_Mat;
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

        private byte[,] createBoard(byte i_Size)
        {
            byte[,] matBoard = new byte[i_Size, i_Size];

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
            
            eCheckers checkers = (eCheckers) m_Mat[vesselOneY, vesselOneX];
            eCheckers soilderToPlay;

            soilderToPlay = soilderKind(); // m_NowPlaying == 1 ? eCheckers.CheckerO | eCheckers.CheckerU : eCheckers.CheckerX | eCheckers.CheckerK;
           
            if ((checkers & soilderToPlay) == checkers && checkers != eCheckers.Non)
            {
                choisesToPlay(checkers, vesselOneX, vesselOneY, vesselTwoX, vesselTwoY);
            }
            else
            {
                Console.WriteLine("Not Your Vessel . try again.");
            }

        }

        void choisesToPlay(eCheckers soilderPlay, sbyte vesselOneX, sbyte vesselOneY, sbyte vesselTwoX, sbyte vesselTwoY)
        {            
            switch (soilderPlay) // the vessel that going to play.
            {
                case eCheckers.CheckerO :
                case eCheckers.CheckerX :
                    playRegularVesselAndCheckMoveDirection(vesselOneX, vesselOneY, vesselTwoX, vesselTwoY);
                    break;
                case eCheckers.CheckerU :
                case eCheckers.CheckerK :
                    eatOrMoveVessel(vesselOneX, vesselOneY, vesselTwoX, vesselTwoY);
                    break;
            }
        }

        void playRegularVesselAndCheckMoveDirection(sbyte vesselOneX, sbyte vesselOneY, sbyte vesselTwoX, sbyte vesselTwoY)
        {
            sbyte distLineY = (sbyte)(vesselOneY - vesselTwoY);

            if (isMoveFront(distLineY) == true)
            {
                eatOrMoveVessel(vesselOneX, vesselOneY, vesselTwoX, vesselTwoY);
                checkIfBecomeKing(ref m_Mat[vesselTwoY, vesselTwoX], vesselTwoY);
            }
            else
            {
                Console.WriteLine("Cant go back . play again.");
            }
        }

        private bool eatOrMoveVessel(sbyte vesselOneX, sbyte vesselOneY, sbyte vesselTwoX, sbyte vesselTwoY)
        {
            bool turnWellPlayed = false;

            sbyte vesselMovementLineX = (sbyte)Math.Abs(vesselOneX - vesselTwoX), vesselMovementLineY = (sbyte)Math.Abs(vesselOneY - vesselTwoY);
            if (vesselMovementLineX == 1 && vesselMovementLineY == 1)
            {
                turnWellPlayed = moveVessel(vesselOneX, vesselOneY, vesselTwoX, vesselTwoY);
            }
            else if (vesselMovementLineX == 2 && vesselMovementLineY == 2)
            {
                turnWellPlayed = eatEnemyVessel(vesselOneX, vesselOneY, vesselTwoX, vesselTwoY);
            }

            return turnWellPlayed;
        }

        private void checkIfBecomeKing(ref byte io_Vessel, sbyte i_LineY)
        {
            
            if (io_Vessel == (byte) eCheckers.CheckerO)
            {
                io_Vessel = i_LineY == (m_Size - 1) ? (byte) eCheckers.CheckerU : io_Vessel;
            }
            else if (io_Vessel == (byte) eCheckers.CheckerX)
            {
                io_Vessel = i_LineY == 0 ? (byte) eCheckers.CheckerK : io_Vessel;
            }
           
        }

        private bool moveVessel(sbyte i_IndexOfVesselOneX, sbyte i_IndexOfVesselOneY, sbyte i_IndexOfVesselTwoX, sbyte i_IndexOfVesselTwoY)
        { //// te bool return is for check if all did appened and not need replay turn . if the bool not needed so to replace to void .
            bool isMoved = false;
           
            if (m_Mat[i_IndexOfVesselTwoY, i_IndexOfVesselTwoX] == 0)
            {
                m_Mat[i_IndexOfVesselTwoY, i_IndexOfVesselTwoX] = m_Mat[i_IndexOfVesselOneY, i_IndexOfVesselOneX];
                m_Mat[i_IndexOfVesselOneY, i_IndexOfVesselOneX] = 0;
                changePlayer();

                isMoved = true;
            }

            return isMoved;
        }

        private bool eatEnemyVessel(sbyte i_IndexOfVesselOneX, sbyte i_IndexOfVesselOneY, sbyte i_IndexOfVesselTwoX, sbyte i_IndexOfVesselTwoY)
        { //// te bool return is for check if all did appened and not need replay turn . if the bool not needed so to replace to void .

            bool isEated = false;
          
            sbyte middleIndexX = (sbyte)((i_IndexOfVesselTwoX + i_IndexOfVesselOneX) / 2), middleIndexY = (sbyte)((i_IndexOfVesselOneY + i_IndexOfVesselTwoY) / 2);

            eCheckers checkers = (eCheckers)m_Mat[middleIndexY, middleIndexX];
            eCheckers enemySoilders = soilderKind(), freeSpot = (eCheckers) m_Mat[i_IndexOfVesselTwoY, i_IndexOfVesselTwoX];

             if (freeSpot == (byte) eCheckers.Non && ((checkers & enemySoilders) != checkers && checkers != eCheckers.Non))                                  
            {
                m_Mat[i_IndexOfVesselTwoY, i_IndexOfVesselTwoX] = m_Mat[i_IndexOfVesselOneY, i_IndexOfVesselOneX];
                m_Mat[middleIndexY, middleIndexX] = 0; ////
                m_Mat[i_IndexOfVesselOneY, i_IndexOfVesselOneX] = 0;

               // mulitiEatingCheckAndDo(i_IndexOfVesselTwoX, i_IndexOfVesselTwoY);

                changePlayer();

                isEated = true;
            }
            else
            {
                Console.WriteLine("Cant jump so far without Eat. - you cant eat nothing or yourself - . try again.");
            }

            return isEated;
        }

        private eCheckers soilderKind()
        {
           return m_NowPlaying == 1 ? eCheckers.CheckerO | eCheckers.CheckerU : eCheckers.CheckerX | eCheckers.CheckerK;
        }

        private void mulitiEatingCheckAndDo(sbyte i_VesselIndexX, sbyte i_VesselIndexY)
        {
            bool oneTimeToCheck = true;

            while (checkingBounderis(i_VesselIndexX, i_VesselIndexY) == true && oneTimeToCheck == true)
            {
                if (m_NowPlaying == 1)
                {
                    Console.WriteLine("Hey im player1 and i can eat double.!!11");
                }
                else // p2
                {
                    Console.WriteLine("Hey im player2 and i can eat double.!!22");
                }

                oneTimeToCheck = false;
            }
        }

        private bool checkingBounderis(sbyte i_VesselIndexX, sbyte i_VesselIndexY) // check that the indexes still in limit.
        {
            bool isInLimit = (i_VesselIndexX + 2 <= m_Size - 1) && (i_VesselIndexY + 2 <= m_Size - 1) && (i_VesselIndexX - 2 >= 0) && (i_VesselIndexY - 2 >= 0);
            /*
            bool isEnemyPlayerInRange = false;
            if (isInLimit == true)
            {
                
                if (m_Mat[i_VesselIndexY+1, i_VesselIndexX+1] == m_NowPlaying || m_Mat[i_VesselIndexY + 1, i_VesselIndexX + 1] == m_NowPlaying + 2)
                {
                    isEnemyPlayerInRange = true;
                }
                if (m_Mat[i_VesselIndexY + 1, i_VesselIndexX - 1] == m_NowPlaying || m_Mat[i_VesselIndexY + 1, i_VesselIndexX + 1] == m_NowPlaying + 2)
                {
                    isEnemyPlayerInRange = true;
                }
                if (m_Mat[i_VesselIndexY - 1, i_VesselIndexX + 1] == m_NowPlaying || m_Mat[i_VesselIndexY + 1, i_VesselIndexX + 1] == m_NowPlaying + 2)
                {
                    isEnemyPlayerInRange = true;
                }
                if (m_Mat[i_VesselIndexY - 1, i_VesselIndexX - 1] == m_NowPlaying || m_Mat[i_VesselIndexY + 1, i_VesselIndexX + 1] == m_NowPlaying + 2)
                {
                    isEnemyPlayerInRange = true;
                }

            }
            */

            return isInLimit;
        }

        private void charsToIndex(out sbyte o_VesselIndexX, char i_CapitalLetterX, out sbyte o_VesselIndexY, char i_SmallLetterY)
        {
            o_VesselIndexX = (sbyte)(i_CapitalLetterX - 'A');
            o_VesselIndexY = (sbyte)(i_SmallLetterY - 'a');
        }

        private bool isMoveFront(sbyte i_DistLineY) // checking if it is go front.!
        {
            if (m_NowPlaying != 1)
            {
                i_DistLineY *= -1;
            }

            return i_DistLineY < 0 ? true : false;
        }

        public bool GameOn() { return m_GameOn; }

        [Flags]
        private enum eCheckers : byte
        {
            Non = 0,
            CheckerO = 1,
            CheckerX = 2,
            CheckerU = 4,
            CheckerK = 8
        }

        //public bool MoveVesselCheck() {}
        //private void kingVessel() {} // i think it not needed at all !!!
        //private void simpleVessel() {} // i think it not needed at all !!!


    };



}