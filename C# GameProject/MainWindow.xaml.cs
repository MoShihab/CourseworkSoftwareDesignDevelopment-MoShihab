using C__GameProject;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace GameProjectFinal
{
    /// <summary>
    /// Interaction logic for the main window of the game launcher.
    /// </summary>
    public partial class MainWindow : Window
    {
        // Indicates the selected player type (Player vs. Player or Player vs. CPU)
        public bool playerType { get; set;}
        // Constructor for the MainWindow
        public MainWindow()
        {
            InitializeComponent();
        }
        // Button click event to show information about the game
        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            var Button = (Button)sender;

            MessageBox.Show("Game Developed By Mo Shihab. Enjoy :) ");
        }
        // Button click event to exit the application
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        // Event handler for dragging the window
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed) 
            {
                DragMove();
            }
        }
        // Button click event for Tic Tac Toe game selection
        private void ButtonTickTackToe_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;

            // Disable the clicked button to prevent further clicks
            button.IsEnabled = false;

            // Changes back color
            btnTickTackToe.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF4C70"));

            btnHangman.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#39385D")); ;
            btnBattleships.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#39385D")); ;

            // Enable other buttons
            btnBattleships.IsEnabled = true;
            btnHangman.IsEnabled = true;
            btnPlayer.Visibility = Visibility.Visible;
            btnCPU.Visibility = Visibility.Visible;
            txtComingSoon.Visibility = Visibility.Hidden;

            // Sets opacity of other txt
            txtBattleships.Opacity = 0;
            txtHangman.Opacity = 0;

            // Create a DoubleAnimation to animate the Opacity property
            DoubleAnimation fadeInAnimation = new DoubleAnimation
            {
                From = 0.0,   // Starting opacity (fully transparent)
                To = 0.8,     // Ending opacity (fully opaque)
                Duration = TimeSpan.FromSeconds(.3)  // Duration of the animation (1 second in this example)
            };

            DoubleAnimation fadeOutAnimation = new DoubleAnimation
            {
                From = 0.8,   // Starting opacity (fully transparent)
                To = 0.0,     // Ending opacity (fully opaque)
                Duration = TimeSpan.FromSeconds(.3)  // Duration of the animation (1 second in this example)
            };

            // Start the animation on the Opacity property of txtTicTacToe
            txtTicTacToe.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);

            if (txtBattleships.Opacity > .1)
            {
                txtBattleships.BeginAnimation(UIElement.OpacityProperty, fadeOutAnimation);
            }
            else if (txtHangman.Opacity > .1)
            {
                txtHangman.BeginAnimation(UIElement.OpacityProperty, fadeOutAnimation);
            }
        }

        private void btnHangman_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;

            // Disable the clicked button to prevent further clicks
            button.IsEnabled = false;

            // Changes back color
            btnHangman.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF4C70"));

            btnTickTackToe.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#39385D"));
            btnBattleships.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#39385D"));

            // Enable other buttons
            btnBattleships.IsEnabled = true;
            btnTickTackToe.IsEnabled = true;
            btnCPU.Visibility = Visibility.Visible;
            // Disables Player button and hides it
            btnPlayer.Visibility = Visibility.Hidden;
            txtComingSoon.Visibility = Visibility.Hidden;   

            // Sets opacity of other txt
            txtBattleships.Opacity = 0;
            txtTicTacToe.Opacity = 0;

            // Create a DoubleAnimation to animate the Opacity property
            DoubleAnimation fadeInAnimation = new DoubleAnimation
            {
                From = 0.0,   // Starting opacity (fully transparent)
                To = 0.8,     // Ending opacity (fully opaque)
                Duration = TimeSpan.FromSeconds(.3)  // Duration of the animation (1 second in this example)
            };
            DoubleAnimation fadeOutAnimation = new DoubleAnimation
            {
                From = 0.8,   // Starting opacity (fully transparent)
                To = 0.0,     // Ending opacity (fully opaque)
                Duration = TimeSpan.FromSeconds(.3)  // Duration of the animation (1 second in this example)
            };

            // Start the animation on the Opacity property of txtTicTacToe and other txt
            txtHangman.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);

            if (txtTicTacToe.Opacity > .1)
            {
                txtTicTacToe.BeginAnimation(UIElement.OpacityProperty, fadeOutAnimation);
            }
            else if (txtBattleships.Opacity > .1)
            {
                txtBattleships.BeginAnimation(UIElement.OpacityProperty, fadeOutAnimation);
            }
        }

        // Button click event handler for selecting Battleships game
        private void btnBattleships_Click(object sender, RoutedEventArgs e)
        {
            // Get the button that triggered the event
            var button = (Button)sender;

            // Disable the clicked button to prevent further clicks
            button.IsEnabled = false;

            // Change the background color of the selected button
            button.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF4C70"));

            // Reset background colors of other game buttons
            btnTickTackToe.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#39385D"));
            btnHangman.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#39385D"));

            // Enable other game buttons
            btnHangman.IsEnabled = true;
            btnTickTackToe.IsEnabled = true;

            // Hide player and CPU buttons, show "Coming Soon" text
            btnPlayer.Visibility = Visibility.Hidden;
            btnCPU.Visibility = Visibility.Hidden;
            txtComingSoon.Visibility = Visibility.Visible;

            // Set opacity of other text elements
            txtHangman.Opacity = 0;
            txtTicTacToe.Opacity = 0;

            // Create animations to fade in/out the Battleships text
            DoubleAnimation fadeInAnimation = new DoubleAnimation
            {
                From = 0.0,   // Starting opacity (fully transparent)
                To = 0.8,     // Ending opacity (fully opaque)
                Duration = TimeSpan.FromSeconds(.3)  // Duration of the fadeIn animation
            };

            DoubleAnimation fadeOutAnimation = new DoubleAnimation
            {
                From = 0.8,   // Starting opacity (fully opaque)
                To = 0.0,     // Ending opacity (fully transparent)
                Duration = TimeSpan.FromSeconds(.3)  // Duration of the fadeOut animation
            };

            // Start the fadeIn animation on the Opacity property of the Battleships text
            txtBattleships.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);

            // Check the opacity of other texts and start fadeOut animations accordingly
            if (txtTicTacToe.Opacity > .1)
            {
                txtTicTacToe.BeginAnimation(UIElement.OpacityProperty, fadeOutAnimation);
            }
            else if (txtHangman.Opacity > .1)
            {
                txtHangman.BeginAnimation(UIElement.OpacityProperty, fadeOutAnimation);
            }
        }


        // Button click event handler for selecting Player mode
        private void btnPlayer_Click(object sender, RoutedEventArgs e)
        {
            // Get the button that triggered the event
            var button = (Button)sender;

            // Show player images and hide the robot image
            imgBoy.Visibility = Visibility.Visible;
            imgGirl.Visibility = Visibility.Visible;
            imgRobot.Visibility = Visibility.Hidden;

            // Disable the Player button to prevent further selection
            button.IsEnabled = false;

            // Change the background color of the selected button
            button.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF4C70"));

            // Enable the CPU button for mode selection
            btnCPU.IsEnabled = true;

            // Change the background color of the CPU button
            btnCPU.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#39385D"));

            // Set the playerType to true, indicating Player mode
            playerType = true;
        }


        // Button click event handler for selecting CPU mode
        private void btnCPU_Click(object sender, RoutedEventArgs e)
        {
            // Get the button that triggered the event
            var button = (Button)sender;

            // Hide player images and show the robot image
            imgBoy.Visibility = Visibility.Hidden;
            imgGirl.Visibility = Visibility.Hidden;
            imgRobot.Visibility = Visibility.Visible;

            // Disable the CPU button to prevent further selection
            button.IsEnabled = false;

            // Change the background color of the selected button
            button.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF4C70"));

            // Enable the Player button for mode selection
            btnPlayer.IsEnabled = true;

            // Change the background color of the Player button
            btnPlayer.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#39385D"));

            // Set the playerType to false, indicating CPU mode
            playerType = false;
        }


        // Button click event handler for starting the selected game
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            // Check if Tic Tac Toe game is selected (either Player vs. Player or Player vs. CPU)
            if ((btnTickTackToe.IsEnabled == false && btnPlayer.IsEnabled == false)
                || btnTickTackToe.IsEnabled == false && btnCPU.IsEnabled == false)
            {
                // Create an instance of the Tic Tac Toe game, passing the selected player type
                TicTacToe ticTacToe = new TicTacToe(playerType);
                // Show the Tic Tac Toe game window
                ticTacToe.Show();
                // Hide the current game launcher window
                this.Hide();
            }
            // Check if Hangman game is selected (Player vs. CPU)
            else if (btnHangman.IsEnabled == false && btnCPU.IsEnabled == false)
            {
                // Create an instance of the Hangman game
                Hangman hangman = new Hangman();
                // Show the Hangman game window
                hangman.Show();
                // Hide the current game launcher window
                this.Hide();
            }
            // Check if Battleships game is selected (either Player vs. Player or Player vs. CPU)
            else if ((btnBattleships.IsEnabled == false && btnPlayer.IsEnabled == false)
                || btnBattleships.IsEnabled == false && btnCPU.IsEnabled == false)
            {
                // Add logic for starting the Battleships game (currently empty)
            }
            else
            {
                // Show a message if no game or mode is selected
                MessageBox.Show("Please select a game and the mode.");
            }
        }


        // Mouse enter event handler for the Player button
        private void btnPlayer_MouseEnter(object sender, MouseEventArgs e)
        {
            // Check if both CPU and Player buttons are enabled
            if (btnCPU.IsEnabled && btnPlayer.IsEnabled)
            {
                // Show the boy and girl images when the mouse enters if both buttons are enabled
                imgBoy.Visibility = Visibility.Visible;
                imgGirl.Visibility = Visibility.Visible;
            }
        }

        // Mouse leave event handler for the Player button
        private void btnPlayer_MouseLeave(object sender, MouseEventArgs e)
        {
            // Check if the Player button is not enabled
            if (!btnPlayer.IsEnabled)
            {
                // If the Player button is disabled, show the boy and girl images when the mouse leaves
                imgBoy.Visibility = Visibility.Visible;
                imgGirl.Visibility = Visibility.Visible;
            }
            // Check if the Player button is enabled
            else if (btnPlayer.IsEnabled)
            {
                // If the Player button is enabled, hide the boy and girl images when the mouse leaves
                imgBoy.Visibility = Visibility.Hidden;
                imgGirl.Visibility = Visibility.Hidden;
            }
        }


        // Mouse enter event handler for the CPU button
        private void btnCPU_MouseEnter(object sender, MouseEventArgs e)
        {
            // Check if both Player and CPU buttons are enabled
            if (btnPlayer.IsEnabled && btnCPU.IsEnabled)
            {
                // Show the robot image when the mouse enters if both buttons are enabled
                imgRobot.Visibility = Visibility.Visible;
            }
        }

        // Mouse leave event handler for the CPU button
        private void btnCPU_MouseLeave(object sender, MouseEventArgs e)
        {
            // Check if the CPU button is not enabled
            if (!btnCPU.IsEnabled)
            {
                // If the CPU button is disabled, show the robot image when the mouse leaves
                imgRobot.Visibility = Visibility.Visible;
            }
            // Check if the CPU button is enabled
            else if (btnCPU.IsEnabled)
            {
                // If the CPU button is enabled, hide the robot image when the mouse leaves
                imgRobot.Visibility = Visibility.Hidden;
            }
        }

    }
}
