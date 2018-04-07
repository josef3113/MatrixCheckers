using System;
using System.Text;

namespace MatrixCheckers
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // Playing();

            Playing2();

            // Console.ReadLine();
        }

        public static void Playing2()
        {
            MatrixCheckers Board = new MatrixCheckers(8);
            Board.PlayingVessel("Ac>Bd"); // Bf>Ce
            Board.PlayingVessel("Bf>Ce");
            Board.PlayingVessel("Cc>Dd");
            Board.PlayingVessel("Hf>Ge");
            Board.PlayingVessel("Dd>Ee");
            Board.PlayingVessel("Df>Fd");
            Board.PlayingVessel("Ec>Dd");
            Board.PlayingVessel("Fd>Ec");
            Board.PlayingVessel("Bb>Cc");

            Board.PlayingVessel("Gg>Hf");
            Board.PlayingVessel("Aa>Bb");
            Board.PlayingVessel("Fh>Gg");


            //// ------- Multi eat
            //Board.PlayingVessel("Db>Fd");
            //Board.PlayingVessel("Ge>Ec");
            //Board.PlayingVessel("Gc>Hd");
            //Board.PlayingVessel("Hf>Ge");
            //Board.PlayingVessel("Bd>Df");
            //Board.PlayingVessel("");
            //Board.PlayingVessel("");
            //// ------ end.


            Board.PrintBoard();
            
            

            string str = Console.ReadLine();
            while (char.ToUpper(str[0]) != 'Q')
            {
                Board.PlayingVessel(str);
                Board.PrintBoard();
                str = Console.ReadLine();
            }

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
            Board.PlayingVessel("Ce>Bf");
            Board.PlayingVessel("Ff>Ee");
            Board.PlayingVessel("Cc>Bd");
            Board.PlayingVessel("Ag>Ce");
            Board.PlayingVessel("Ce>Ac");
            Board.PlayingVessel("Db>Fd");
            //Board.PlayingVessel("");
            //Board.PlayingVessel("");
            // -- from  here to
            //Board.PlayingVessel("Db>Fd");
            //Board.PlayingVessel("Cg>Df");
            //Board.PlayingVessel("Fd>Ee");
            //Board.PlayingVessel("Bb>Ac");
            //Board.PlayingVessel("Gc>Hd");
            //Board.PlayingVessel("Ff>Dd");
            // -- to here its just to see if all work back and front eating.            
            //Board.PlayingVessel("");
            //Board.PlayingVessel("");
            //Board.PlayingVessel("");
            //Board.PlayingVessel("");
            //Board.PlayingVessel("");
            //Board.PlayingVessel("");
            //Board.PlayingVessel("");
            //Board.PlayingVessel("");
            //Board.PlayingVessel("");

            //Board.PrintBoard();

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
        private const bool k_Player1 = true;
        private bool m_NowPlaying;


        public MatrixCheckers(byte i_Size = 8)
        {
            m_Size = i_Size;
            m_Mat = createBoard(m_Size);
            m_GameOn = true;
            m_NowPlaying = k_Player1;
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
            buttomLines.AppendFormat("{0}Playing now -> {1}{0}", Environment.NewLine, m_NowPlaying == true ? '1' : '2');
            buttomLines.Append('-', m_Size + 2);

            Console.WriteLine(buttomLines);
        }

        private void changePlayer()
        {
            m_NowPlaying = m_NowPlaying == k_Player1 ? !k_Player1 : k_Player1;
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

            eCheckers checkers = (eCheckers)m_Mat[vesselOneY, vesselOneX];
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
                case eCheckers.CheckerO:
                case eCheckers.CheckerX:
                    playRegularVesselAndCheckMoveDirection(vesselOneX, vesselOneY, vesselTwoX, vesselTwoY);
                    break;
                case eCheckers.CheckerU:
                case eCheckers.CheckerK:
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
            if (i_LineY == 0 || i_LineY == (m_Size - 1))
            {
                if (io_Vessel == (byte)eCheckers.CheckerO || io_Vessel == (byte)eCheckers.CheckerX)
                {
                    io_Vessel = m_NowPlaying == k_Player1 ? (byte)eCheckers.CheckerU : (byte)eCheckers.CheckerK;
                }
            }
        }

        private bool moveVessel(sbyte i_IndexOfVesselOneX, sbyte i_IndexOfVesselOneY, sbyte i_IndexOfVesselTwoX, sbyte i_IndexOfVesselTwoY)
        { //// te bool return is for check if all did appened and not need replay turn . if the bool not needed so to replace to void .
            bool isMoved = false;

            if (m_Mat[i_IndexOfVesselTwoY, i_IndexOfVesselTwoX] == (byte)eCheckers.Non)
            {
                m_Mat[i_IndexOfVesselTwoY, i_IndexOfVesselTwoX] = m_Mat[i_IndexOfVesselOneY, i_IndexOfVesselOneX];
                m_Mat[i_IndexOfVesselOneY, i_IndexOfVesselOneX] = (byte)eCheckers.Non; // (byte) eCheckers.Non == 0
                checkIfBecomeKing(ref m_Mat[i_IndexOfVesselTwoY, i_IndexOfVesselTwoX], i_IndexOfVesselTwoY);
                changePlayer();

                isMoved = true;
            }

            return isMoved;
        }

        private bool eatEnemyVessel(sbyte i_IndexOfVesselOneX, sbyte i_IndexOfVesselOneY, sbyte i_IndexOfVesselTwoX, sbyte i_IndexOfVesselTwoY)
        { //// te bool return is for check if all did appened and not need replay turn . if the bool not needed so to replace to void .

            bool isEated = false;
            Console.WriteLine("Here You in Eat Method");
            sbyte middleIndexX = (sbyte)((i_IndexOfVesselTwoX + i_IndexOfVesselOneX) / 2);
            sbyte middleIndexY = (sbyte)((i_IndexOfVesselOneY + i_IndexOfVesselTwoY) / 2);


            eCheckers checkers = (eCheckers)m_Mat[middleIndexY, middleIndexX];
            eCheckers enemySoilders = soilderKind(), freeSpot = (eCheckers)m_Mat[i_IndexOfVesselTwoY, i_IndexOfVesselTwoX];

            if (freeSpot == eCheckers.Non && ((checkers & enemySoilders) != checkers && checkers != eCheckers.Non))
            {
                m_Mat[i_IndexOfVesselTwoY, i_IndexOfVesselTwoX] = m_Mat[i_IndexOfVesselOneY, i_IndexOfVesselOneX];
                m_Mat[middleIndexY, middleIndexX] = (byte)eCheckers.Non; // eCheckers.Non = 0
                m_Mat[i_IndexOfVesselOneY, i_IndexOfVesselOneX] = (byte)eCheckers.Non;
                checkIfBecomeKing(ref m_Mat[i_IndexOfVesselTwoY, i_IndexOfVesselTwoX], i_IndexOfVesselTwoY);
                mulitiEatingCheckAndDo(i_IndexOfVesselTwoX, i_IndexOfVesselTwoY);

                changePlayer();

                isEated = true;
            }
            else
            {
                Console.WriteLine("Cant jump so far without Eat. - you cant eat nothing or yourself - . try again.");
            }

            return isEated;
        }


        private void mulitiEatingCheckAndDo(sbyte i_VesselIndexX, sbyte i_VesselIndexY)
        {
            // bool oneTimeToCheck = true;

            bool continueEating = true;
                       

            while (checkingBounderis(i_VesselIndexX, i_VesselIndexY) == true && continueEating == true)
           // if (try1Z == true)
            {
                if (m_NowPlaying == k_Player1)
                {
                    Console.WriteLine("Hey im player1 and i can eat double.!!11");
                }
                else // p2
                {
                    Console.WriteLine("Hey im player2 and i can eat double.!!22");
                }

                // oneTimeToCheck = eatWithSameSoilder(i_VesselIndexX, i_VesselIndexY);
                continueEating = eatWithSameSoilder(ref i_VesselIndexX,ref i_VesselIndexY); // ref this for the new indexes!!

               
            }

            return ;
        }

        private bool eatWithSameSoilder(ref sbyte i_IndexOfVesselOneX,ref sbyte i_IndexOfVesselOneY)
        {
            bool isEated = false;
            PrintBoard();
            Console.WriteLine("Eat Again !!!");
            string i_MovePos = Console.ReadLine();
            if (char.ToUpper(i_MovePos[0]) == 'Q' || i_MovePos[2] != '>' || i_MovePos.Length != 5)
            {
                return false;
            }

            sbyte indexInput1X, indexInput1Y, indexInput2X, indexInput2Y;
            charsToIndex(out indexInput1X, i_MovePos[0], out indexInput1Y, i_MovePos[1]);
            charsToIndex(out indexInput2X, i_MovePos[3], out indexInput2Y, i_MovePos[4]);

            sbyte vesselMovementLineX = (sbyte)Math.Abs(indexInput1X - indexInput2X), vesselMovementLineY = (sbyte)Math.Abs(indexInput1Y - indexInput2Y);

            if (vesselMovementLineX == 2 && vesselMovementLineY == 2)
            {

                if (i_IndexOfVesselOneX == indexInput1X && i_IndexOfVesselOneY == indexInput1Y)
                {
                    sbyte middleIndexX = (sbyte)((indexInput1X + indexInput2X) / 2); // vesselMovementLineX
                    sbyte middleIndexY = (sbyte)((indexInput1Y + indexInput2Y) / 2); // vesselMovementLineY

                    eCheckers checkers = (eCheckers)m_Mat[middleIndexY, middleIndexX];
                    eCheckers enemySoilders = soilderKind(), freeSpot = (eCheckers)m_Mat[indexInput2Y, indexInput2X];

                    if (freeSpot == eCheckers.Non && ((checkers & enemySoilders) != checkers && checkers != eCheckers.Non))
                    {
                        m_Mat[indexInput2Y, indexInput2X] = m_Mat[indexInput1Y, indexInput1X];
                        m_Mat[middleIndexY, middleIndexX] = (byte)eCheckers.Non; // eCheckers.Non = 0
                        m_Mat[indexInput1Y, indexInput1X] = (byte)eCheckers.Non;

                        // else if (vesselMovementLineX == 2 && vesselMovementLineY == 2) 
                        //sbyte vesselMovementLineX = (sbyte)Math.Abs(vesselOneX - vesselTwoX), vesselMovementLineY = (sbyte)Math.Abs(vesselOneY - vesselTwoY);

                        checkIfBecomeKing(ref m_Mat[indexInput2Y, indexInput2X], indexInput2Y);

                        i_IndexOfVesselOneX = indexInput2X;


                        i_IndexOfVesselOneY = indexInput2Y;

                        isEated = true;
                    }
                }
            }

            return isEated;
        }

        private bool checkingBounderis(sbyte i_VesselIndexX, sbyte i_VesselIndexY) // check that the indexes still in limit.
        {////////////////////////////// Right ///////////////////////////// Down ////////////////////////////////// Left /////////////////////// Up //////////
         //   bool  isInLimit = (i_VesselIndexX + 2 <= m_Size - 1) && (i_VesselIndexY + 2 <= m_Size - 1) && (i_VesselIndexX - 2 >= 0) && (i_VesselIndexY - 2 >= 0);

            byte start = 0, end = (byte)(m_Size - 1);
            bool isRightUpSpotLegal = (i_VesselIndexX + 2 <= end) && (i_VesselIndexY - 2 >= start);
            bool isRightDownSpotLegal = (i_VesselIndexX + 2 <= end) && (i_VesselIndexY + 2 <= end); // !!
            bool isLeftUpSpotLegal = (i_VesselIndexX - 2 >= start) && (i_VesselIndexY - 2 >= start);
            bool isLeftDownSpotLegal = (i_VesselIndexX - 2 >= start) && (i_VesselIndexY + 2 <= end); // !!

            eCheckers enemySoilders = soilderKind();
            bool isCanEatAgain = false;
            //((checkers & enemySoilders) != checkers && checkers != eCheckers.Non) // is to the enemy soilder .!!

            if (isRightUpSpotLegal == true)
            {
                isCanEatAgain = isHaveEnemyInCrossToEat(enemySoilders, (sbyte)(i_VesselIndexX + 1), (sbyte)(i_VesselIndexY - 1), (sbyte)(i_VesselIndexX + 2), (sbyte)(i_VesselIndexY - 2)) || isCanEatAgain;

                if (isHaveEnemyInCrossToEat(enemySoilders, (sbyte)(i_VesselIndexX + 1), (sbyte)(i_VesselIndexY - 1), (sbyte)(i_VesselIndexX + 2), (sbyte)(i_VesselIndexY - 2)))
                    Console.WriteLine("isRightUpSpotClear");
            }
            if (isRightDownSpotLegal == true)
            {
                isCanEatAgain = isHaveEnemyInCrossToEat(enemySoilders, (sbyte)(i_VesselIndexX + 1), (sbyte)(i_VesselIndexY + 1), (sbyte)(i_VesselIndexX + 2), (sbyte)(i_VesselIndexY + 2)) || isCanEatAgain;

                if (isHaveEnemyInCrossToEat(enemySoilders, (sbyte)(i_VesselIndexX + 1), (sbyte)(i_VesselIndexY + 1), (sbyte)(i_VesselIndexX + 2), (sbyte)(i_VesselIndexY + 2)))
                    Console.WriteLine("isRightDownSpotClear");
            }
            if (isLeftUpSpotLegal == true)
            {
                isCanEatAgain = isHaveEnemyInCrossToEat(enemySoilders, (sbyte)(i_VesselIndexX - 1), (sbyte)(i_VesselIndexY - 1), (sbyte)(i_VesselIndexX - 2), (sbyte)(i_VesselIndexY - 2)) || isCanEatAgain;
                if (isHaveEnemyInCrossToEat(enemySoilders, (sbyte)(i_VesselIndexX - 1), (sbyte)(i_VesselIndexY - 1), (sbyte)(i_VesselIndexX - 2), (sbyte)(i_VesselIndexY - 2)))
                    Console.WriteLine("isLeftUpSpotClear");
            }
            if (isLeftDownSpotLegal == true)
            {
                isCanEatAgain = isHaveEnemyInCrossToEat(enemySoilders, (sbyte)(i_VesselIndexX - 1), (sbyte)(i_VesselIndexY + 1), (sbyte)(i_VesselIndexX - 2), (sbyte)(i_VesselIndexY + 2)) || isCanEatAgain;
                if (isHaveEnemyInCrossToEat(enemySoilders, (sbyte)(i_VesselIndexX - 1), (sbyte)(i_VesselIndexY + 1), (sbyte)(i_VesselIndexX - 2), (sbyte)(i_VesselIndexY + 2)))
                    Console.WriteLine("isLeftDownSpotLegal");
            }

            return isCanEatAgain;
        }

        // goal is yaad
        private bool isHaveEnemyInCrossToEat(eCheckers soildersTeam, sbyte indexMiddleX, sbyte indexMiddleY, sbyte indexGoalX, sbyte indexGoalY)
        {
            eCheckers checker = (eCheckers)m_Mat[indexMiddleY, indexMiddleX], freeSpot = (eCheckers)m_Mat[indexGoalY, indexGoalX];
            return (freeSpot == eCheckers.Non && ((checker & soildersTeam) != checker && checker != eCheckers.Non));
        }

        private void charsToIndex(out sbyte o_VesselIndexX, char i_CapitalLetterX, out sbyte o_VesselIndexY, char i_SmallLetterY)
        {
            o_VesselIndexX = (sbyte)(i_CapitalLetterX - 'A');
            o_VesselIndexY = (sbyte)(i_SmallLetterY - 'a');
        }

        private bool isMoveFront(sbyte i_DistLineY) // checking if it is go front.!
        {
            if (m_NowPlaying != k_Player1)
            {
                i_DistLineY *= -1;
            }

            return i_DistLineY < 0 ? true : false;
        }

        private eCheckers soilderKind()
        {
            return m_NowPlaying == k_Player1 ? eCheckers.CheckerO | eCheckers.CheckerU : eCheckers.CheckerX | eCheckers.CheckerK;
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