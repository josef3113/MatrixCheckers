using System;
using System.Text;


namespace MatrixCheckers
{
    class Program
    {
        static void Main(string[] args)
        {


            // Playing();

            // Playing2();

            // Playing3();

            Playing4();
            

            // Console.ReadLine();
        }

        public static void Playing4()
        {


            GamePlay game = new GamePlay(8);

            game.StartGameToPlay();




        }

        public static void Playing3()
        {
            CheckersLogic Board = new CheckersLogic(8);

            Board.PrintBoard();

            BordToGame bord = new BordToGame(8);

            bord.PrintBoardGame();
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
            Console.Title = "Damka , Good Luck" ;

            CheckersLogic Board = new CheckersLogic(8);
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

            CheckersLogic Board = new CheckersLogic(8);
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

}

/*

class GamePlay
{

     P
    GameLogic logi;
    BoradUI borad;


    borad[1, 2];

        logi.PlayMove(if , j , i+ , j+)== true { borad[if , j, i + , j +] 
};
  if (isAte()) {
  
    }

};

 */