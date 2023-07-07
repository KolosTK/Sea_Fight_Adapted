namespace SeaFight
{
    public partial class Form1 : Form
    {
        public const int buttonHeight = 10;
        public const int buttonWidth = 10;
        public const int buttonSize = 35;
        public const int distanseMaps = 450;

        public static string latters = "ABCDEFGHI";
        public static bool IsTheGameStarts = false;

        public Button[,] myButtons = new Button[buttonHeight,buttonWidth];  
        static Button[,] enemyButtons = new Button[buttonHeight, buttonWidth];  

        static int [,] enemyMap = new int[buttonHeight, buttonWidth];  
        static int [,] myMap = new int [buttonHeight, buttonWidth];

        Enemy enemy;


        public Form1()
        {
            InitializeComponent();
            Realization();
        }

        public void Realization ()
        {

            enemy = new Enemy(enemyButtons, myButtons, myMap, enemyMap);

            enemyMap = enemy.GenerateRandomShips();

            MapsOutput(Initialization());
                      

        }

        public void MapsOutput(Button startButton)
        {
            for (int i = 0; i < buttonHeight; i++)
            {
                for (int k = 0; k < buttonWidth; k++)
                {
                    if (i!=0 || k!=0)
                    {
                        myButtons[i, k].Click += new EventHandler(PlaceShips);
                    }
                    this.Controls.Add(myButtons[i, k]);
                }
                for (int k = 0; k < buttonWidth; k++)
                {
                    if (i != 0 || k != 0)
                    {
                        enemyButtons[i, k].Click += new EventHandler(IsAtackSuccesfull);

                    }
                    this.Controls.Add(enemyButtons[i, k]);
                }

                startButton.Click += new EventHandler(StartTheGame);
                this.Controls.Add(startButton);

            }
        }

        public Button Initialization ()
        {
            Button startButton = new Button();
            for (int i = 0; i < buttonHeight; i++)
            {
                //my realization
                for (int k = 0; k < buttonWidth; k++)
                {
                    myMap[i,k] = 0;
                    myButtons[i, k] = new Button();

                    myButtons[i, k].Location = new Point(k * buttonSize, i * buttonSize);
                    myButtons[i, k].Size = new Size(buttonSize, buttonSize);
                    myButtons[i, k].BackColor = Color.AliceBlue;

                    if (k == 0 || i == 0)
                    {
                        myButtons[i, k].BackColor = Color.Gray;

                        if (k == 0 && i != 0)
                        {
                            myButtons[i, k].Text = i.ToString();
                        }
                        else if (i == 0 && k != 0)
                        {
                            myButtons[i, k].Text = latters[k-1].ToString();
                        }
                    }
                }

                //enemy realization
                for (int k = 0; k < buttonWidth; k++)
                {
                    enemyButtons[i, k] = new Button();

                    enemyButtons[i, k].Location = new Point(k * buttonSize + distanseMaps, i * buttonSize);
                    enemyButtons[i, k].Size = new Size(buttonSize, buttonSize);

                    if (k == 0 || i == 0)
                    {
                        enemyButtons[i, k].BackColor = Color.Gray;
                        if (k == 0 && i != 0)
                        {
                        enemyButtons[i, k].Text = i.ToString();
                        }
                        else if (i == 0 && k != 0)
                        {
                        enemyButtons[i, k].Text = latters[k-1].ToString();
                        }

                    }
                }
                startButton.Location = new Point(363, 380);
                startButton.Text = "Start";

            }

            return startButton;
        }
        public void StartTheGame (object sender, EventArgs e)
        {
            Button currentButton = sender as Button;
            if (IsTheGameStarts == false) 
            {
                IsTheGameStarts = true;
            }
        }

        public void PlaceShips (object sender, EventArgs e)
        {
            if (!IsTheGameStarts)
            {
                Button currentButton = sender as Button;
                if (myMap[currentButton.Location.Y / buttonSize, currentButton.Location.X / buttonSize] == 0)
                {
                    myMap[currentButton.Location.Y / buttonSize, currentButton.Location.X / buttonSize] = 1;
                    currentButton.BackColor = Color.Green;
                }
                else
                {
                    myMap[currentButton.Location.Y / buttonSize, currentButton.Location.X / buttonSize] = 0;
                    currentButton.BackColor = Color.AliceBlue;
                }
            }
        }
        public void IsAtackSuccesfull(object sender, EventArgs e)
        {
            if (IsTheGameStarts) 
            {
                Button currentButton = sender as Button;
                if (enemyMap[currentButton.Location.Y / buttonSize, (currentButton.Location.X-distanseMaps) / buttonSize ] == 1)
                {
                    currentButton.BackColor = Color.Red;
                }
                else { currentButton.BackColor = Color.Blue; }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}