using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixCheckers
{
     class GamePlay
    {

        CheckersLogic m_ActiveGame;
        BordToGame m_UiOfGame;



        public GamePlay(byte i_Size = 8)
        {
            m_ActiveGame = new CheckersLogic(i_Size);
            m_UiOfGame = new BordToGame(i_Size);
        }

        public void StartGameToPlay()
        {
                byte indexMoves = 0; // // rember to erase one day 


            /*
            Ai comp;

            if (vsComp == true)
            {
                comp = new Ai();

            }
            else
            {

                P2 name2 = "Hello";
            }
            */

            while (m_ActiveGame.GameOn() == true)
            {
                string[] gameMoveLazy = {}; // rember to erase one day 

                string moveInString;

                m_UiOfGame.PrintBoardGame();

                if (indexMoves <  gameMoveLazy.Length)
                {

                    // some legal move/eat of the computer. return .
                    moveInString = gameMoveLazy[indexMoves];
                    // the move done.
                    indexMoves++;
                }
                else
                {
                    moveInString = Console.ReadLine();
                }

                // string moveInString = Console.ReadLine(); // replace to method

                


                m_ActiveGame.PlayingVessel(moveInString);

                if (m_ActiveGame.IsTurnPass)
                {

                    /*
                    if (m_ActiveGame.IsEated == true)
                    {
                       

                    }
                    */
                    // moveInBoard

                    moveInBoard(moveInString);


                }


                m_UiOfGame.PrintBoardGame();

            }


        }

        private void moveInBoard(string i_MoveInString)
        {
            const char emptyPlace = ' '; // check to naming 
            char yaadX = i_MoveInString[3], yaadY = i_MoveInString[4], makorX = i_MoveInString[0], makorY = i_MoveInString[1];
            
            m_UiOfGame[yaadY, yaadX] = m_UiOfGame[makorY, makorX];
            m_UiOfGame[makorY, makorX] = emptyPlace;

            if (m_ActiveGame.IsEated)
            {
                char MiddleX = (char)((yaadX + makorX) / 2), MiddleY = (char)((yaadY + makorY) / 2);
                m_UiOfGame[MiddleY, MiddleX] = emptyPlace;

            }
        }


    }
}
