using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;

namespace CIT_110_CAPSTONE
{

    // **************************************************
    //
    // Title: CIT110 CAPSTONE - Maze Crawler
    // Description: Winform maze game where user controls an icon to
    // get to the end of the maze.
    // Application Type: Windows Forms
    // Author: Phinizy, Robin
    // Dated Created: 11/15/2020
    // Last Modified: 12/6/2020
    //
    // **************************************************
    public partial class Form1 : Form
    {
        bool maze = false;
        bool treasureFound = false;
        bool gateLocked = true;
        bool walkSpace = false;
        bool appleHidden = true;
        bool appleEaten = false;
        bool gameOver = false;
        bool portalUnlocked = false;
        bool appleAudioPlayed = false;
        bool gameStarted = false;

        int scoreTotal = 0;
        int charIconX=0;
        int charIconY= 20;
        int difficultySelected;
        int mode =2;
        int toggle = 1;
        int easy = 100;
        int medium = 80;
        int hard = 60;
        int mediumScoreMultiplier = 3;
        int hardScoreMultiplier = 6;
        int currentGameMode;

        string coordinates;

        public Form1()
        {
            difficultySelected = easy;
            InitializeComponent();
            mazePanel.Hide();
            brickLocations.Hide();
            ClearScore.Hide();
            textBox1.Text = string.Format("{0}", scoreTotal);
            textBox2.Text = string.Format("{0}", difficultySelected);
            this.Difficulty.Text = "easy";
        }
      
        /// *********************************
        ///            NEW GAME             *
        /// *********************************
        private void NewGame_Click(object sender, EventArgs e)
        {
            //First time launching app hides the maze panel. Show panel when "New Game" button clicked
            //default game mode to easy
            if (maze == false)
            {
                mazePanel.Show();
                maze = true;
                currentGameMode = easy; 
            }

            //*********************
            //    RESET GAME      *
            //*********************
            //
            //reset player position
            this.playerIcon.Location = new System.Drawing.Point(0,20);
            coordinates= "0,20";
            charIconX = 0;
            charIconY = 20;
            scoreTotal = 0;
            toggle = 1;

            //
            //reset moves variable to the difficulty selected
            difficultySelected = currentGameMode;

            //
            //reset game bools
            gameOver = false;
            treasureFound = false;
            appleEaten = false;
            appleHidden = true;
            gateLocked = true;
            portalUnlocked = false;
            appleAudioPlayed = false;
            gameStarted = false;

            //
            //reset forms
            playerIcon.Show();
            scoreboard.Hide();
            controlpanel.Show();
            scorelist.Items.Clear();
            treasureBox.Show();
            gate.Show();
            portal.Show();
            apple.Hide();
            ClearScore.Hide();

            //
            //play game music
            GameMusic();

            //
            // reset player icon, score, and moves left
            this.playerIcon.Image = global::CIT_110_CAPSTONE.Properties.Resources.pony;
            textBox1.Text = string.Format("{0}", scoreTotal);
            textBox2.Text = string.Format("{0}", difficultySelected);
            HighScores.Text = string.Format("High Score");

        }

        /// *************************
        ///         MOVE UP         *  
        /// *************************
        private void Click_Up(object sender, EventArgs e)
        {
            //check if players has moves left
            gameOver = ValidateMovesLeft();

            //if player has  moves left
            if (gameOver == false)
            {
                //record proposed move into variables for validation
                coordinates = charIconX + "," + (charIconY - 20);

                //verify proposed move is a walkable space.
                walkSpace = CheckBrickSpace(walkSpace);

                //continue with move if valid.
                if (walkSpace == true)
                {
                    this.playerIcon.Location = new System.Drawing.Point(charIconX, charIconY - 20);
                    charIconY = charIconY - 20;
                    scoreTotal = scoreTotal + 50;
                    difficultySelected--;
                    textBox1.Text = string.Format("{0}", scoreTotal);
                    textBox2.Text = string.Format("{0}", difficultySelected);

                    //check if player in special item location
                    CheckPlayerPosition();
                }

                //if proposed move is not valid. Do not move character. 
                //reset proposed move variable to match current player location
                if (walkSpace == false)
                {
                    coordinates = charIconX + "," + charIconY;
                    walkSpace = true;
                }
            }
        }
        /// *************************
        ///        MOVE DOWN        *  
        /// *************************
        private void Click_Down(object sender, EventArgs e)
        {
            //check if players has moves left
            gameOver = ValidateMovesLeft();

            //if player has  moves left
            if (gameOver == false)
            {
                //record proposed move into variables for validation
                coordinates = charIconX + "," + (charIconY + 20);

                //verify proposed move is a walkable space.
                walkSpace = CheckBrickSpace(walkSpace);

                //continue with move if valid.
                if (walkSpace == true)
                {
                    this.playerIcon.Location = new System.Drawing.Point(charIconX, charIconY + 20);
                    charIconY = charIconY + 20;
                    scoreTotal = scoreTotal + 50;
                    difficultySelected--;
                    textBox1.Text = string.Format("{0}", scoreTotal);
                    textBox2.Text = string.Format("{0}", difficultySelected);

                    //check if player in special item location
                    CheckPlayerPosition();
                }

                //if proposed move is not valid. Do not move character. 
                //reset proposed move variable to match current player location
                if (walkSpace == false)
                {
                    coordinates = charIconX + "," + charIconY;
                    walkSpace = true;
                }
            }
        
        }
        /// *************************
        ///        MOVE LEFT        *  
        /// *************************
        private void Click_Left(object sender, EventArgs e)
        {
            //check if players has moves left
            gameOver = ValidateMovesLeft();

            //if player has  moves left
            if (gameOver == false)
            {
                //record proposed move into variables for validation
                coordinates = (charIconX - 20) + "," + charIconY;

                //verify proposed move is a walkable space.
                walkSpace = CheckBrickSpace(walkSpace);

                //continue with move if valid.
                if (walkSpace == true)
                {
                    this.playerIcon.Location = new System.Drawing.Point(charIconX - 20, charIconY);
                    charIconX = charIconX - 20;
                    scoreTotal = scoreTotal + 50;
                    difficultySelected--;
                    textBox1.Text = string.Format("{0}", scoreTotal);
                    textBox2.Text = string.Format("{0}", difficultySelected);

                    //check if player in special item location
                    CheckPlayerPosition();
                }

                //if proposed move is not valid. Do not move character. 
                //reset proposed move variable to match current player location
                if (walkSpace == false)
                {
                    coordinates = charIconX + "," + charIconY;
                    walkSpace = true;
                }
            }

        }
        /// *************************
        ///       MOVE RIGHT        *  
        /// *************************
        private void Click_Right(object sender, EventArgs e)
        {
            //check if players has moves left
            gameOver = ValidateMovesLeft();

            //if player has  moves left
            if (gameOver == false)
            {
                //record proposed move into variables for validation
                coordinates = (charIconX + 20) + "," + charIconY;

                //verify proposed move is a walkable space.
                walkSpace = CheckBrickSpace(walkSpace);

                //continue with move if valid.
                if (walkSpace == true)
                {
                    this.playerIcon.Location = new System.Drawing.Point(charIconX + 20, charIconY);
                    charIconX = charIconX + 20;
                    scoreTotal = scoreTotal + 50;
                    difficultySelected--;
                    textBox1.Text = string.Format("{0}", scoreTotal);
                    textBox2.Text = string.Format("{0}", difficultySelected);

                    //check if player in special item location
                    CheckPlayerPosition();
                }

                //if proposed move is not valid. Do not move character. 
                //reset proposed move variable to match current player location
                if (walkSpace == false)
                {
                    coordinates = charIconX + "," + charIconY;
                    walkSpace = true;
                }
            }
        }

        /// *********************************
        ///        DIFFICULTY SELECT        *
        /// *********************************
        public void Difficulty_Click(object sender, EventArgs e)
        {
            //
            //allowes player to select between three difficulty settings at long as the game has not started. 
            if (gameStarted == false)
            {
                //easy mode
                if (mode == 1)
                {
                    this.Difficulty.Text = "easy";
                    difficultySelected = easy;
                    currentGameMode = difficultySelected;
                    textBox2.Text = string.Format("{0}", difficultySelected);
                }

                //medium mode
                if (mode == 2)
                {
                    this.Difficulty.Text = "medium";
                    difficultySelected = medium;
                    currentGameMode = difficultySelected;
                    textBox2.Text = string.Format("{0}", difficultySelected);
                }

                //hard mode and mode variable reset.
                if (mode == 3)
                {
                    this.Difficulty.Text = "hard";
                    difficultySelected = hard;
                    currentGameMode = difficultySelected;
                    textBox2.Text = string.Format("{0}", difficultySelected);
                    mode = 0;
                }
                mode++;
            }
        }

        /// *********************************
        ///    View or Clear High Scores    *
        /// *********************************
        private void HighScores_Click(object sender, EventArgs e)
        {
            // button functional only when maze panel is visible
            if (maze == true)
            {
                //
                //toggle between show and hide states if High Scores button
                if (gameStarted == false)
                {
                    // show the score panel and generate the current score list.
                    if (toggle == 1)
                    {
                        //only show clear score button if the maze panel is visible
                        ClearScore.Show();
                        scoreboard.Show();
                        HighScores.Text = string.Format("Hide");
                        GenerateScoreList();
                    }
                    //hide the score panel and reset toggle variable.
                    if (toggle == 2)
                    {
                        ClearScore.Hide();
                        scoreboard.Hide();
                        HighScores.Text = string.Format("High Score");
                        toggle = 0;
                    }
                    toggle++;
                }
            }
        }

        /// *********************************
        ///         CLEAR ALL SCORES        *
        /// *********************************
        public void ClearScore_Click(object sender, EventArgs e)
        {
            string dataPath = @"C:CIT_110_CAPSTONE.Properties.Resources.highscores.txt";
            //
            // delete the existing .txt file and create a new, empty .txt file. 
            if (File.Exists(dataPath))
            {
                File.Delete(dataPath);
            }
            using (FileStream fs = File.Create(dataPath)) ;

            //
            //hide clear button and clear list box
            ClearScore.Hide();
            scorelist.Items.Clear();
        }

        /// *************************
        ///      PLAY MUSIC         *  
        /// *************************
        public void GameMusic()
        {
            //
            //plays varios .wav files depending on the current state of the game. 
            if (treasureFound == false && gameOver == false)
            {
                //play main theme song
                SoundPlayer world = new SoundPlayer(CIT_110_CAPSTONE.Properties.Resources.newgame);
                world.PlayLooping();
            }

            if (appleEaten == true)
            {

                if (appleAudioPlayed == false)
                {
                    //play magical apple sound
                    SoundPlayer magic = new SoundPlayer(CIT_110_CAPSTONE.Properties.Resources.magic);
                    magic.PlaySync();
                    //play new theme song
                    SoundPlayer world = new SoundPlayer(CIT_110_CAPSTONE.Properties.Resources.unicorn);
                    world.PlayLooping();
                    //only play once per game
                    appleAudioPlayed = true;
                }
            }

            if (gameOver == true)
            {
                //play game over song
                SoundPlayer world = new SoundPlayer(CIT_110_CAPSTONE.Properties.Resources.endofgame);
                world.PlayLooping();
            }
        }

        /// *********************************
        ///        COLLISION CHECKER        *
        /// *********************************
        public bool CheckBrickSpace(bool walkSpace)
        {
            // Find the item in the list and store the index to the item.
            int index = brickLocations.FindStringExact(coordinates);

            // Determine if a valid index is returned. Select the item if it is valid.
            if (index != -1)
            {
              walkSpace = true;

                // check to see if gate is locked.
                if (gateLocked == true && coordinates == "540,440")
                {
                    walkSpace = false;
                }
            }
       
            // collision detected. Do not move player. 
            else
            {
                walkSpace = false;
            }

            return walkSpace;
        }

        /// *********************************
        ///        POSITION TRACKER         *
        /// *********************************
        public void CheckPlayerPosition()
        { 
            //
            // game started after first move
            if (charIconX == 20 && charIconY == 20)
            {
                gameStarted = true;
            }
            //
            //code for secret passage teleport and treasure reward. 
            if (treasureFound == false)
            {
                // teleport player into secret treasure room
                if (charIconX == 440 && charIconY == 260)
                {
                    charIconX = 420;
                    charIconY = 240;
                    this.playerIcon.Location = new System.Drawing.Point(charIconX, charIconY);
                }
                // teleport player out of treasure room and unlock gate
                if (charIconX == 400 && charIconY == 220)
                {
                    //
                    //rewarded extra moves for finding treasure!
                    difficultySelected = difficultySelected + 50;
                    textBox2.Text = string.Format("{0}", difficultySelected);
                    charIconX = 200;
                    charIconY = 460;
                    treasureBox.Hide();
                    gate.Hide();
                    treasureFound = true;
                    gateLocked = false;
                    this.playerIcon.Location = new System.Drawing.Point(charIconX, charIconY);
                }
                //reveal secret rainbow apple after treasure has been collected.
                if (treasureFound == true)
                {
                    scoreTotal = scoreTotal + 1000;
                    apple.Show();
                    appleHidden = false;
                }
            }
            //
            //change player icon and hide apple icon after it has been eaten. Double player points. 
            if (charIconX == 140 && charIconY == 420 && appleHidden == false && appleEaten == false)
            {
                this.playerIcon.Image = global::CIT_110_CAPSTONE.Properties.Resources.derpunicorn;
                scoreTotal = scoreTotal * 2;
                apple.Hide();
                appleHidden = true;
                appleEaten = true;
                portalUnlocked = true;
                GameMusic();
            }
            // teleport player to gate if they achieved derp unicorn
            if (portalUnlocked == true)
            {
                //teleport derp unicorn to end of maze
                if (charIconX == 320 && charIconY == 400)
                {
                    charIconX = 540;
                    charIconY = 440;
                    this.playerIcon.Location = new System.Drawing.Point(540, 420);
                    portal.Hide();
                }
            }
        }

        /// *********************************
        ///          MOVES TRACKER          *
        /// *********************************
        public bool ValidateMovesLeft()
        {
            if (difficultySelected > 0)
            {
                gameOver = false;
            }

            else
            {
                gameOver = true;
            }

            if (gameOver == true || coordinates == "660,460")
            {
                gameOver = true;
                EndOfGame();
            }
            return gameOver;
        }

        /// *********************************
        ///            Score List           *
        /// *********************************
        public void GenerateScoreList()
        {
            int lineNumber = 0;

            string[] highScoreArray = File.ReadAllLines(@"C:CIT_110_CAPSTONE.Properties.Resources.highscores.txt");

            //
            //convert string array to int array for sorting the scoreboard
            int[] scoreNumbers = new int[highScoreArray.Length];

            for (int i = 0; i < highScoreArray.Length; i++)
            {
                int.TryParse(highScoreArray[i].ToString(), out scoreNumbers[i]);
            }

            //int array to sort scores in decending order. 
            Array.Sort(scoreNumbers);
            Array.Reverse(scoreNumbers);

            //
            //write the top 10 high scores onto the scorelist ListBox
            for (int i = 0; i < scoreNumbers.Length && lineNumber < 10; i++)
            {
                scorelist.Items.Add(scoreNumbers[lineNumber].ToString());
                lineNumber++;
            }
        }
        /// *********************************
        ///      WRITE SCORE TO .TXT        *
        /// *********************************
        public void RecordScore()
        {
            //
            //record the current game score into highscores.txt.
            string scoreInfoText;
            string dataPath = @"C:CIT_110_CAPSTONE.Properties.Resources.highscores.txt";
            
            scoreInfoText = "\n"+scoreTotal.ToString();

            File.AppendAllText(dataPath, scoreInfoText);
        }

        /// *********************************
        ///            GAME OVER            *
        /// *********************************
        public void EndOfGame()
        {
            //
            //calculate bonus points into the score based on number of moves left.
            scoreTotal = scoreTotal + (difficultySelected * 100);

            //apply score multipliers for medium mode
            if (currentGameMode == medium)
            {
                scoreTotal = scoreTotal * mediumScoreMultiplier;
                if (appleEaten == true)
                {
                    scoreTotal = scoreTotal + (difficultySelected * 2);
                }

                if (portalUnlocked == true)
                {
                    scoreTotal = scoreTotal + 789;
                }
            }

            //apply score multipliers for hard mode
            if (currentGameMode == hard)
            {
                scoreTotal = scoreTotal * hardScoreMultiplier;
                if (appleEaten == true)
                {
                    scoreTotal = scoreTotal + (difficultySelected * 3);
                }

                if (portalUnlocked == true)
                {
                    scoreTotal = scoreTotal + 1234;
                }
            }

            //
            //easter egg. If player wins game with 42 moves left. Wins all the points
            if (difficultySelected == 42)
            {
                scoreTotal = 999999999;
            }

            //
            //change player score label to reflect current game score. 
            PlayersScore.Text = string.Format("" + scoreTotal);

            //
            //record the score to the .txt file and show scoreboard. Play end game music. 
            RecordScore();
            scoreboard.Show();
            controlpanel.Hide();
            GenerateScoreList();
            GameMusic();
            playerIcon.Hide();
        }

        /// *********************************
        ///             GAME EXIT           *
        /// *********************************
        private void Quit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}