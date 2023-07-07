using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SeaFight
{
    internal class Enemy
    {

        public Button[,] myButtons = new Button[Form1.buttonHeight, Form1.buttonWidth];
        public Button[,] enemyButtons = new Button[Form1.buttonHeight, Form1.buttonWidth];

        public int[,] enemyMap = new int[Form1.buttonHeight, Form1.buttonWidth];
        public int[,] myMap = new int[Form1.buttonHeight, Form1.buttonWidth];

        public const int buttonSize = 35;



        public Enemy(Button[,] myButtons, Button[,] enemyButtons, int[,] enemyMap, int[,] myMap)
        {
            this.myButtons = myButtons;
            this.enemyButtons = enemyButtons;
            this.enemyMap = enemyMap;
            this.myMap = myMap;
        }

        public bool IsCellEmpty(int cell)
        {

            return (cell == 1) ? false : true;
        }

        public bool IsBuildShipExists(int y,int x, int shipLength)
        {
            for (int k = 0; k < shipLength; k++)
            {
                try
                {
                    if (IsCellEmpty(myMap[y, x + k]))
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public void ShipsFilling (int y, int x, int shipLength)
        {
            for (int i = 0; i < shipLength; i++)
            {
                myMap[y,x + i] = 1;
            }
        }

        public int[,] GenerateRandomShips()
        {
            int shipLength = 1;
            int shipCount = 10;
            int shipAmount = 4;

            Random rand = new Random();

            int xCoordinate;
            int yCoordinate;


            for (int i = shipCount; i >= 0; i--)
            {
                for (int s = shipAmount; s > 0; s--)
                {
                    xCoordinate = rand.Next(1, Form1.buttonHeight);
                    yCoordinate = rand.Next(1, Form1.buttonWidth);

                    Button button = myButtons[yCoordinate,xCoordinate];

                    if (IsBuildShipExists(yCoordinate,xCoordinate, shipLength) == true) 
                    {
                        ShipsFilling(yCoordinate,xCoordinate, shipLength);
                        shipCount--;
                    }
                    else 
                    {
                        s++;
                        continue;
                    }
                }
                shipAmount--;
                shipLength++;
            }
            return myMap;

        }
        public void Atack()
        {
            Random rand = new Random();
            while (true)
            {
                Button button = enemyButtons[rand.Next(1, Form1.buttonHeight), rand.Next(1, Form1.buttonWidth)];
                if (!IsCellEmpty(enemyMap[button.Location.Y/ buttonSize, button.Location.X/ buttonSize]))
                {
                    button.BackColor = Color.Red;
                    enemyMap[button.Location.Y / buttonSize, button.Location.X / buttonSize] = 0;
                    continue;
                }
                else
                {
                    button.BackColor = Color.Black;
                    break;
                }
            }
        }
    }
}
