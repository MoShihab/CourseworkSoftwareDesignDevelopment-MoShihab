using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using GameProjectFinal;

namespace C__GameProject
{
    /// <summary>
    /// Interaction logic for TicTacToe.xaml
    /// </summary>
    /// 
    public partial class TicTacToe : Window
    {
        // This variable keeps track of whether it's currently Player 1's turn.
        private bool tPlayer1Turn;
        // This variable indicates whether the player is a human or computer player.
        private bool isHuman;
        // This variable signals whether the game has ended.
        private bool gameEnded;
        // This list stores references to the buttons on the game board.
        private List<Button> buttons;
        // This random object is used to generate random moves for the computer player.
        Random random = new Random();
        // This variable tracks the number of wins for Player 1.
        public int tPwins = 0;
        // This variable tracks the number of losses for Player 1 or the computer player.
        public int tPlosses = 0;

        // Constructor for the TicTacToe class.
        public TicTacToe(bool playerType)
        {
            // Initialize the graphical components of the window.
            InitializeComponent();

            // Create a list to store references to the buttons on the game board.
            buttons = new List<Button>();

            // Start the tip loop to display informational tips or jokes.
            StartTipLoop();

            // Populate the list of buttons with references to the buttons on the game board.
            populateListButtons();

            // Set up the initial state for a new game.
            NewGame();

            // Assign the provided playerType parameter value to the class-level field isHuman.
            isHuman = playerType;

        }


        // This method starts a thread that displays tips or jokes about Tic Tac Toe at regular intervals.
        private void StartTipLoop()
        {
            // Start a new thread for the loop
            Thread tipThread = new Thread(() =>
            {
                // Array of tips or jokes about Tic Tac Toe
                string[] tips = {
                "Ancient Origins: Tic Tac Toe, also known as Noughts and Crosses, has ancient roots and is believed to have originated in ancient Egypt over 3,500 years ago. The game evolved through different cultures over time.",
                "Simplest Possible Game: Tic Tac Toe is often cited as the simplest game with strategic depth. Despite its simplicity, the game can lead to interesting strategies and outcomes, making it a classic choice for recreational play and educational purposes.",
                "Mathematical Complexity: The game has been studied in the field of combinatorics and game theory. It's been proven that with perfect play from both players, every game of Tic Tac Toe will end in a tie. The number of possible unique Tic Tac Toe games is relatively small, providing a finite and determinable set of outcomes.",
                "Educational Value: Tic Tac Toe is commonly used as a teaching tool for children to develop basic strategic thinking and pattern recognition. It helps in understanding concepts like lines, symmetry, and winning conditions.",
                "Versatility and Cultural Variations: The game is played worldwide, and its simplicity has led to various cultural adaptations. In some regions, it's played on different board sizes or with additional rules. The game's adaptability contributes to its enduring popularity across diverse cultures and age groups.",
                "Why did the scarecrow become a Tic Tac Toe champion?\nBecause he was outstanding in his field!",
                "I asked the computer to play Tic Tac Toe with me.\nIt said, \"I'm sorry, I can't play. You already have the X-factor.\"",
                "Why did the pencil refuse to play Tic Tac Toe?\nIt was afraid of getting too many \"draws.\"",
                "What do you call two Tic Tac Toe players who never lose?\nCrossword experts!",
                "I challenged a computer to Tic Tac Toe, and it beat me three times in a row.\nI guess I need to upgrade my \"X\" factor!"
};

                // Index to keep track of the current tip or joke
                int currentIndex = 0;

                // Infinite loop to continuously display tips or jokes
                while (true)
                {
                    // Update the UI on the main thread with the current tip or joke
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        txtTipsJokes.Text = tips[currentIndex];
                    });

                    // Wait for 20 seconds before moving to the next tip or joke
                    Thread.Sleep(20000);

                    // Move to the next tip or joke, looping back to the beginning if needed
                    currentIndex = (currentIndex + 1) % tips.Length;
                }
            });

            // Set the thread as a background thread to allow the application to exit
            tipThread.IsBackground = true;

            // Start the thread to initiate the continuous display of tips or jokes
            tipThread.Start();
        }


        // This method sets up the initial state for a new Tic Tac Toe game.
        private void NewGame()
        {
            // Reset the flag indicating whether the game has ended to false.
            gameEnded = false;

            // Populate the list of buttons with references to the buttons on the game board.
            populateListButtons();

            // Set the initial turn to Player 1's turn.
            tPlayer1Turn = true;

            // Hide the text displaying game results.
            txtWinLose.Visibility = Visibility.Hidden;

            // Iterate through every button on the grid.
            foreach (var x in buttons)
            {
                // Reset each button to its default state.
                // Enable the button for interaction.
                x.IsEnabled = true;

                // Clear the content (X or O) on the button.
                x.Content = string.Empty;

                // Set the background color of the button to default (white).
                x.Background = Brushes.White;
            }
        }


        // This method handles the button click event for Tic Tac Toe buttons.
        private void HandleButtonClick(Button button)
        {
            // Update the content of the clicked button based on the current player's turn.
            button.Content = tPlayer1Turn ? "X" : "O";

            // Disable the clicked button to prevent further interactions.
            button.IsEnabled = false;

            // Set the background color of the clicked button to a specific color.
            button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#39385D"));

            // Remove the clicked button from the list of available buttons.
            buttons.Remove(button);

            // Check the game state after each move.
            checkGame();

            // Update the turn to the next player.
            if (isHuman)
            {
                // If the game is played by a human, switch the turn to the other player.
                tPlayer1Turn = !tPlayer1Turn;
            }
            else if (!isHuman)
            {
                // If the game is played against the computer, let the computer take its turn.
                CPUTurn();
            }
        }


        // This method represents the computer player's turn in the Tic Tac Toe game.
        private void CPUTurn()
        {
            // Check if there are available buttons and the game is not yet ended.
            if (buttons != null && buttons.Count > 0 && (!gameEnded))
            {
                // Generate a random index to select a button for the computer's move.
                int index = random.Next(buttons.Count);

                // Ensure the button at the selected index is not null before accessing its properties.
                if (buttons[index] != null)
                {
                    // Disable the selected button to prevent further interactions.
                    buttons[index].IsEnabled = false;

                    // Set the content of the selected button to "O" representing the computer's move.
                    buttons[index].Content = "O";

                    // Set the background color of the selected button to a specific color (e.g., red).
                    buttons[index].Background = Brushes.Red;

                    // Remove the selected button from the list of available buttons.
                    buttons.RemoveAt(index);
                }
            }

            // Check the game state after the computer's move.
            checkGame();
        }


        // This method checks the current state of the Tic Tac Toe game to determine if it has ended.
        private void checkGame()
        {
            // Check if Player 1 has won by getting three in a row horizontally, vertically, or diagonally with "X".
            if ((string.Equals(btn1.Content, "X") && string.Equals(btn2.Content, "X") && string.Equals(btn3.Content, "X"))
                || string.Equals(btn4.Content, "X") && string.Equals(btn5.Content, "X") && string.Equals(btn6.Content, "X")
                || string.Equals(btn7.Content, "X") && string.Equals(btn8.Content, "X") && string.Equals(btn9.Content, "X")
                || string.Equals(btn1.Content, "X") && string.Equals(btn4.Content, "X") && string.Equals(btn7.Content, "X")
                || string.Equals(btn2.Content, "X") && string.Equals(btn5.Content, "X") && string.Equals(btn8.Content, "X")
                || string.Equals(btn3.Content, "X") && string.Equals(btn6.Content, "X") && string.Equals(btn9.Content, "X")
                || string.Equals(btn3.Content, "X") && string.Equals(btn5.Content, "X") && string.Equals(btn7.Content, "X")
                || string.Equals(btn1.Content, "X") && string.Equals(btn5.Content, "X") && string.Equals(btn9.Content, "X"))
            {
                // Check if the game has not already ended
                if (!gameEnded)
                {
                    // Set the game as ended and disable buttons
                    gameEnded = true;
                    disableButtons();

                    // Display the result and update Player 1's score
                    txtWinLose.Visibility = Visibility.Visible;
                    txtWinLose.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7DDDC2"));
                    tPwins++;
                    txtWinLose.Text = "Player one wins!";
                    txtPlayerScore.Text = "Player Score: " + tPwins;
                }
            }
            // Check if Player 2 or CPU has won with "O"
            else if ((string.Equals(btn1.Content, "O") && string.Equals(btn2.Content, "O") && string.Equals(btn3.Content, "O"))
                    || string.Equals(btn4.Content, "O") && string.Equals(btn5.Content, "O") && string.Equals(btn6.Content, "O")
                    || string.Equals(btn7.Content, "O") && string.Equals(btn8.Content, "O") && string.Equals(btn9.Content, "O")
                    || string.Equals(btn1.Content, "O") && string.Equals(btn4.Content, "O") && string.Equals(btn7.Content, "O")
                    || string.Equals(btn2.Content, "O") && string.Equals(btn5.Content, "O") && string.Equals(btn8.Content, "O")
                    || string.Equals(btn3.Content, "O") && string.Equals(btn6.Content, "O") && string.Equals(btn9.Content, "O")
                    || string.Equals(btn3.Content, "O") && string.Equals(btn5.Content, "O") && string.Equals(btn7.Content, "O")
                    || string.Equals(btn1.Content, "O") && string.Equals(btn5.Content, "O") && string.Equals(btn9.Content, "O"))
            {
                // Check if the game has not already ended
                if (!gameEnded)
                {
                    // Set the game as ended and disable buttons
                    gameEnded = true;
                    disableButtons();

                    // Display the result and update Player 2 or CPU score
                    txtWinLose.Visibility = Visibility.Visible;
                    txtWinLose.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7DDDC2"));
                    if (isHuman)
                    {
                        txtWinLose.Text = "Player two wins!";
                        tPlosses++;
                        txtPlayer2Score.Text = "Player Score: " + tPlosses;
                    }
                    else
                    {
                        txtWinLose.Text = "CPU wins!";
                        tPlosses++;
                        txtPlayer2Score.Text = "CPU Score: " + tPlosses;
                    }
                }
            }
            // Check if the game is a draw
            else if (buttons.Count == 0)
            {
                // Check if the game has not already ended
                if (!gameEnded)
                {
                    // Set the game as ended
                    gameEnded = true;

                    // Display the result for a draw
                    txtWinLose.Visibility = Visibility.Visible;
                    txtWinLose.Text = "Draw!";
                    txtWinLose.Foreground = Brushes.Silver;
                }
            }
        }


        // This method initializes the 'buttons' list with references to the Tic Tac Toe buttons.
        private void populateListButtons()
        {
            buttons = new List<Button> { btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9 };
        }

        // This event handler allows the main grid to be dragged when the left mouse button is pressed.
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Check if the left mouse button is pressed
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // Allow dragging of the window by calling the DragMove() method
                DragMove();
            }
        }


        // This event handler shuts down the application when the associated button is clicked.
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // This event handler restarts the Tic Tac Toe game when the associated button is clicked.
        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }

        // This event handler navigates to the main window of the GameProjectFinal application and closes the current window.
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            // Create an instance of the MainWindow from the GameProjectFinal application
            GameProjectFinal.MainWindow mainWindow = new GameProjectFinal.MainWindow();

            // Show the main window
            mainWindow.Show();

            // Close the current window
            this.Close();
        }

        // This method disables all buttons in the 'buttons' list, preventing further interactions.
        private void disableButtons()
        {
            foreach (var x in buttons)
            {
                // Reset the buttons to their default state by disabling them
                x.IsEnabled = false;
            }
        }


        // These event handlers are associated with the Click event of each button in the Tic Tac Toe grid.
        // They delegate the button click handling to the common method 'HandleButtonClick' passing the respective button as an argument.

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            // Delegate the button click handling to the common method
            HandleButtonClick(btn1);
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            // Delegate the button click handling to the common method
            HandleButtonClick(btn2);
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            // Delegate the button click handling to the common method
            HandleButtonClick(btn3);
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            // Delegate the button click handling to the common method
            HandleButtonClick(btn4);
        }

        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            // Delegate the button click handling to the common method
            HandleButtonClick(btn5);
        }

        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            // Delegate the button click handling to the common method
            HandleButtonClick(btn6);
        }

        private void btn7_Click(object sender, RoutedEventArgs e)
        {
            // Delegate the button click handling to the common method
            HandleButtonClick(btn7);
        }

        private void btn8_Click(object sender, RoutedEventArgs e)
        {
            // Delegate the button click handling to the common method
            HandleButtonClick(btn8);
        }

        private void btn9_Click(object sender, RoutedEventArgs e)
        {
            // Delegate the button click handling to the common method
            HandleButtonClick(btn9);
        }

    }
}
