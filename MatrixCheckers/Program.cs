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

            // Playing3();

            // Console.ReadLine();
        }

        public static void Playing3()
        {
            MatrixCheckers Board = new MatrixCheckers(10);

            Board.PrintBoard();

            string str = Console.ReadLine();
            while (char.ToUpper(str[0]) != 'Q')
            {
                Board.PlayingVessel(str);
                Board.PrintBoard();
                str = Console.ReadLine();
            }
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
            Board.PlayingVessel("Db>Fd");
            Board.PlayingVessel("Ge>Ec");
            Board.PlayingVessel("Gc>Hd");
            Board.PlayingVessel("Hf>Ge");
            Board.PlayingVessel("Bd>Df");
            //// ------ end.

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
            // here add if  to cover all the method for right or wrong input ! .!!
            if (i_MovePos[2] != '>')
            {
                return;
            }

            byte vesselOneX, vesselOneY, vesselTwoX, vesselTwoY;
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

        void choisesToPlay(eCheckers soilderPlay, byte vesselOneX, byte vesselOneY, byte vesselTwoX, byte vesselTwoY)
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

        void playRegularVesselAndCheckMoveDirection(byte vesselOneX, byte vesselOneY, byte vesselTwoX, byte vesselTwoY)
        {
            if (isMoveFront(vesselOneY, vesselTwoY) == true)
            {
                eatOrMoveVessel(vesselOneX, vesselOneY, vesselTwoX, vesselTwoY);
            }
            else
            {
                Console.WriteLine("Cant go back . play again.");
            }
        }

        private bool eatOrMoveVessel(byte vesselOneX, byte vesselOneY, byte vesselTwoX, byte vesselTwoY)
        {
            bool turnWellPlayed = false;

            const byte oneStepMoveInCross = 1, twoStepsMoveInCross = 2;

            if (checkMoveInCross(oneStepMoveInCross, vesselOneX, vesselOneY, vesselTwoX, vesselTwoY))
            {
                turnWellPlayed = moveVessel(vesselOneX, vesselOneY, vesselTwoX, vesselTwoY);
            }
            else if (checkMoveInCross(twoStepsMoveInCross, vesselOneX, vesselOneY, vesselTwoX, vesselTwoY))
            {
                turnWellPlayed = eatEnemyVessel(vesselOneX, vesselOneY, vesselTwoX, vesselTwoY);
            }
            else
            {
                Console.WriteLine("illegal move , you can only move in cross one step or eat in cross . try again.");
            }

            return turnWellPlayed;
        }

        private void checkIfBecomeKing(ref byte io_Vessel, byte i_LineY)
        {
            if (i_LineY == 0 || i_LineY == (m_Size - 1))
            {
                if (io_Vessel == (byte)eCheckers.CheckerO || io_Vessel == (byte)eCheckers.CheckerX)
                {
                    io_Vessel = m_NowPlaying == k_Player1 ? (byte)eCheckers.CheckerU : (byte)eCheckers.CheckerK;
                }
            }
        }

        private bool moveVessel(byte i_IndexOfVesselOneX, byte i_IndexOfVesselOneY, byte i_IndexOfVesselTwoX, byte i_IndexOfVesselTwoY)
        {
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

        private bool eatEnemyVessel(byte i_IndexOfVesselOneX, byte i_IndexOfVesselOneY, byte i_IndexOfVesselTwoX, byte i_IndexOfVesselTwoY)
        { //// te bool return is for check if all did appened and not need replay turn . if the bool not needed so to replace to void .

            bool isEated = false;

            byte middleIndexX = (byte)((i_IndexOfVesselTwoX + i_IndexOfVesselOneX) / 2);
            byte middleIndexY = (byte)((i_IndexOfVesselOneY + i_IndexOfVesselTwoY) / 2);

            if (isHaveEnemyInCrossToEat(middleIndexX, middleIndexY, i_IndexOfVesselTwoX, i_IndexOfVesselTwoY))
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

        private void mulitiEatingCheckAndDo(byte i_VesselIndexX, byte i_VesselIndexY)
        {
            bool continueEating = true;
            bool checkIfOptionToEat = checkingBounderis(i_VesselIndexX, i_VesselIndexY);

            while (checkIfOptionToEat == true && continueEating == true)
            { /// -- delete1
                if (m_NowPlaying == k_Player1)
                {
                    Console.WriteLine("Hey im player1 and i can eat double.!!11");
                }
                else // p2
                {
                    Console.WriteLine("Hey im player2 and i can eat double.!!22");
                }
                /// -- delete2
                ///                

                continueEating = checkIfOptionToEat = false;

                string inputMove = allInputIsOk();
                if (inputMove != null)
                {
                    byte indexInput1X, indexInput1Y, indexInput2X, indexInput2Y;
                    charsToIndex(out indexInput1X, inputMove[0], out indexInput1Y, inputMove[1]);
                    charsToIndex(out indexInput2X, inputMove[3], out indexInput2Y, inputMove[4]);

                    if (indexInput1X == i_VesselIndexX && indexInput1Y == i_VesselIndexY)
                    {
                        continueEating = eatWithSameSoilder(indexInput1X, indexInput1Y, indexInput2X, indexInput2Y);
                        checkIfOptionToEat = checkingBounderis(indexInput2X, indexInput2Y);
                        i_VesselIndexX = indexInput2X;
                        i_VesselIndexY = indexInput2Y;                        
                    }
                }
            }
        }

        private string allInputIsOk()
        {
            string rightInput = null;
            PrintBoard();
            Console.WriteLine("Eat Again");
            string inputGameMove = Console.ReadLine();
            char capitalEnd = (char)((m_Size - 1) + 'A'), littleEnd = (char)((m_Size - 1) + 'a');
            if (inputGameMove.Length >= 5)
            {
                if (checkNotPassTheLimitChars(inputGameMove[0], inputGameMove[1], inputGameMove[3], inputGameMove[4]) && inputGameMove[2] == '>')
                {
                    rightInput = inputGameMove;
                }
            }
            else if (inputGameMove.Length == 0)
            {
                Console.WriteLine("wow there is nothing here.");
            }
            else if (char.ToUpper(inputGameMove[0]) == 'Q')
            {
                Console.WriteLine("You are sure you want to end the game ? if yes enter Q or q again.");
            }

            return rightInput;
        }

        private bool checkNotPassTheLimitChars(char i_CapitalLetterA, char i_LittleLetterA, char i_CapitalLetterB, char i_LittleLetterB)
        {
            char capitalEnd = (char)((m_Size - 1) + 'A'), littleEnd = (char)((m_Size - 1) + 'a');
            bool capitalLetters = (i_CapitalLetterA >= 'A' && i_CapitalLetterA <= capitalEnd) && (i_CapitalLetterB >= 'A' && i_CapitalLetterB <= capitalEnd);
            bool littleLetters = (i_LittleLetterA >= 'A' && i_LittleLetterA <= littleEnd) && (i_LittleLetterB >= 'A' && i_LittleLetterB <= littleEnd);
            return capitalLetters && littleLetters;
        }

        private bool eatWithSameSoilder(byte indexInput1X, byte indexInput1Y, byte indexInput2X, byte indexInput2Y)
        {
            bool isEated = false;

            if (checkMoveInCross(2, indexInput1X, indexInput1Y, indexInput2X, indexInput2Y))
            {
                byte middleIndexX = (byte)((indexInput1X + indexInput2X) / 2); // vesselMovementLineX
                byte middleIndexY = (byte)((indexInput1Y + indexInput2Y) / 2); // vesselMovementLineY

                if (isHaveEnemyInCrossToEat(middleIndexX, middleIndexY, indexInput2X, indexInput2Y))
                {
                    m_Mat[indexInput2Y, indexInput2X] = m_Mat[indexInput1Y, indexInput1X];
                    m_Mat[middleIndexY, middleIndexX] = (byte)eCheckers.Non; // eCheckers.Non = 0
                    m_Mat[indexInput1Y, indexInput1X] = (byte)eCheckers.Non;

                    checkIfBecomeKing(ref m_Mat[indexInput2Y, indexInput2X], indexInput2Y);

                    isEated = true;
                }
            }

            return isEated;
        }
        
        private bool checkMoveInCross(byte moveDist, byte indexX1, byte indexY1, byte indexX2, byte indexY2)
        {
            byte stepsLineX = (byte)Math.Abs(indexX1 - indexX2), stepsLineY = (byte)Math.Abs(indexY1 - indexY2);
            return stepsLineX == moveDist && stepsLineY == moveDist;
        }

        private bool checkingBounderis(byte i_VesselIndexX, byte i_VesselIndexY) // check that the indexes still in limit.
        {////////////////////////////// Right ///////////////////////////// Down ////////////////////////////////// Left /////////////////////// Up //////////
         //   bool  isInLimit = (i_VesselIndexX + 2 <= m_Size - 1) && (i_VesselIndexY + 2 <= m_Size - 1) && (i_VesselIndexX - 2 >= 0) && (i_VesselIndexY - 2 >= 0);

            byte start = 0, end = (byte)(m_Size - 1);
            bool isRightUpSpotLegal = (i_VesselIndexX + 2 <= end) && (i_VesselIndexY - 2 >= start);
            bool isRightDownSpotLegal = (i_VesselIndexX + 2 <= end) && (i_VesselIndexY + 2 <= end); 
            bool isLeftUpSpotLegal = (i_VesselIndexX - 2 >= start) && (i_VesselIndexY - 2 >= start);
            bool isLeftDownSpotLegal = (i_VesselIndexX - 2 >= start) && (i_VesselIndexY + 2 <= end); 

            bool isCanEatAgain = false;

            if (isRightUpSpotLegal == true && isCanEatAgain == false)
            {
                isCanEatAgain = isHaveEnemyInCrossToEat((byte)(i_VesselIndexX + 1), (byte)(i_VesselIndexY - 1), (byte)(i_VesselIndexX + 2), (byte)(i_VesselIndexY - 2));
                /// -- delete1
                if (isHaveEnemyInCrossToEat((byte)(i_VesselIndexX + 1), (byte)(i_VesselIndexY - 1), (byte)(i_VesselIndexX + 2), (byte)(i_VesselIndexY - 2)))
                    Console.WriteLine("isRightUpSpotClear");
                /// -- delete2
            }
            if (isRightDownSpotLegal == true && isCanEatAgain == false)
            {
                isCanEatAgain = isHaveEnemyInCrossToEat((byte)(i_VesselIndexX + 1), (byte)(i_VesselIndexY + 1), (byte)(i_VesselIndexX + 2), (byte)(i_VesselIndexY + 2));
                /// -- delete1
                if (isHaveEnemyInCrossToEat((byte)(i_VesselIndexX + 1), (byte)(i_VesselIndexY + 1), (byte)(i_VesselIndexX + 2), (byte)(i_VesselIndexY + 2)))
                    Console.WriteLine("isRightDownSpotClear");
                /// -- delete2
            }
            if (isLeftUpSpotLegal == true && isCanEatAgain == false)
            {
                isCanEatAgain = isHaveEnemyInCrossToEat((byte)(i_VesselIndexX - 1), (byte)(i_VesselIndexY - 1), (byte)(i_VesselIndexX - 2), (byte)(i_VesselIndexY - 2));
                /// -- delete1
                if (isHaveEnemyInCrossToEat((byte)(i_VesselIndexX - 1), (byte)(i_VesselIndexY - 1), (byte)(i_VesselIndexX - 2), (byte)(i_VesselIndexY - 2)))
                    Console.WriteLine("isLeftUpSpotClear");
                /// -- delete2
            }
            if (isLeftDownSpotLegal == true && isCanEatAgain == false)
            {
                isCanEatAgain = isHaveEnemyInCrossToEat((byte)(i_VesselIndexX - 1), (byte)(i_VesselIndexY + 1), (byte)(i_VesselIndexX - 2), (byte)(i_VesselIndexY + 2));
                /// -- delete1
                if (isHaveEnemyInCrossToEat((byte)(i_VesselIndexX - 1), (byte)(i_VesselIndexY + 1), (byte)(i_VesselIndexX - 2), (byte)(i_VesselIndexY + 2)))
                    Console.WriteLine("isLeftDownSpotLegal");
                /// -- delete2
            }

            return isCanEatAgain;
        }

        // goal is yaad
        private bool isHaveEnemyInCrossToEat(byte indexMiddleX, byte indexMiddleY, byte indexGoalX, byte indexGoalY)
        {
            eCheckers soildersTeam = soilderKind();
            eCheckers checker = (eCheckers)m_Mat[indexMiddleY, indexMiddleX], freeSpot = (eCheckers)m_Mat[indexGoalY, indexGoalX];
            return (freeSpot == eCheckers.Non && ((checker & soildersTeam) != checker && checker != eCheckers.Non));
        }

        private void charsToIndex(out byte o_VesselIndexX, char i_CapitalLetterX, out byte o_VesselIndexY, char i_SmallLetterY)
        {
            o_VesselIndexX = (byte)(i_CapitalLetterX - 'A');
            o_VesselIndexY = (byte)(i_SmallLetterY - 'a');
        }

        private bool isMoveFront(byte vesselOneY, byte vesselTwoY) // checking if it is go front.!
        {
            sbyte distLineY = (sbyte)(vesselOneY - vesselTwoY);

            if (m_NowPlaying != k_Player1)
            {
                distLineY *= -1;
            }

            return distLineY < 0 ? true : false;
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

    };



}