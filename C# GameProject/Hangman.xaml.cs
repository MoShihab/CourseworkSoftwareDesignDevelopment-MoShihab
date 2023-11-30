using GameProjectFinal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using System.Windows.Shapes;

namespace C__GameProject
{
    /// <summary>
    /// Interaction logic for Hangman.xaml
    /// </summary>
    public partial class Hangman : Window
    {

        // The word to be guessed in the Hangman game
        private string _hangmanWord = "";
        public string HangmanWord
        {
            // Getter for _hangmanWord
            get { return _hangmanWord; }
            // Setter for _hangmanWord
            set { _hangmanWord = value; }
        }

        // Lists to store alphabet buttons, option buttons, images, and word categories
        List<Button> alphabetbuttons = new List<Button>();
        List<Button> optionButtons = new List<Button>();
        List<Image> optionImages = new List<Image>();
        List<Image> sirImageHealth = new List<Image>();
        List<string> carParts = new List<string>(), animals = new List<string>(), countryParts = new List<string>(), programmingParts = new List<string>(), fruitParts = new List<string>();

        // String to store the initial word state
        string initialWordString;

        // Arrays to store characters for the Hangman word and guessed letters
        public char[] charArrayHangmanWord = new char[20];
        public char[] charArrayLetters = new char[20];

        // Variables to track game statistics and settings
        int wordWins = 0, gameWins = 0, gameLosses = 0, lifePoints = 5;

        // Flags indicating the selected word category
        public bool car = false, animal = false, country = false, fruit = false, programmingLanguage = false;

        // Constructor for the Hangman class
        public Hangman()
        {
            // Initialize the Hangman window components
            InitializeComponent();

            // Initialize an empty string for the initial word state
            initialWordString = string.Empty;

            // Initialize lists and arrays for alphabet buttons, option buttons, images, and word categories
            alphabetbuttons = new List<Button> { btnA, btnB, btnC, btnD, btnE, btnF, btnG, btnH, btnI, btnJ, btnK, btnL, btnM, btnN, btnO, btnP, btnQ, btnR, btnS, btnT, btnU, btnV, btnW, btnX, btnY, btnZ };
            optionButtons = new List<Button> { btnCars, btnAnimals, btnCountries, btnFruits, btnProgrammingLanguages };
            optionImages = new List<Image> { imgAnimal, imgCar, imgCountries, imgFruit, imgProgramming };
            sirImageHealth = new List<Image> { imgHealthSir1, imgHealthSir2, imgHealthSir3, imgHealthSir4, imgHealthSir5 };
            charArrayLetters = new char[20];
            charArrayHangmanWord = new char[20];

            // Initialize word categories with lists of words
            carParts = new List<string>
    {
        // List of car parts
        "Engine", "Wheel", "Transmission", "Brakes", "Suspension",
        "Exhaust", "Radiator", "Battery", "Alternator", "Starter",
        "Ignition", "Fuel", "Steering", "Tires",
        "Headlights", "lights", "Windshield", "Wipers",
        "Converter"
    };
            animals = new List<string>
    {
        // List of animal names
        "Elephant", "Giraffe", "Lion", "Tiger", "Zebra",
        "Kangaroo", "Koala", "Penguin", "Panda", "Cheetah",
        "Gorilla", "Hippopotamus", "Rhinoceros", "Bear",
        "Kangaroo", "Koala", "Penguin", "Panda", "Cheetah",
        "Giraffe"
    };
            countryParts = new List<string>
    {
        // List of country names
        "Canada", "Brazil", "Australia", "Japan", "Germany",
        "India", "France", "Mexico", "China", "Italy",
        "Spain", "Argentina", "South Korea", "Russia", "UnitedKingdom",
        "Egypt", "South Africa", "Nigeria", "Kenya", "Saudi Arabia"
    };
            programmingParts = new List<string>
    {
        // List of programming-related terms
        "Algorithm", "Variable", "Function", "Database", "Framework",
        "Debugging", "Interface", "Syntax", "Compiler", "JavaScript",
        "Python", "Java", "HTML", "CSS", "API",
        "Binary", "Bug", "Code", "Git", "Framework"
    };
            fruitParts = new List<string>
    {
        // List of fruit names
        "Apple", "Banana", "Orange", "Grapes", "Watermelon",
        "Strawberry", "Pineapple", "Mango", "Peach", "Kiwi",
        "Pear", "Cherry", "Blueberry", "Plum", "Raspberry",
        "Blackberry", "Lemon", "Lime", "Coconut", "Avocado"
    };

            // Set initial visibility for alphabet buttons and health images
            foreach (Button button in alphabetbuttons)
            {
                button.Visibility = Visibility.Hidden;
            }
            foreach (Image image in sirImageHealth)
            {
                image.Visibility = Visibility.Hidden;
            }
        }


        // Event handler for the Exit button to shut down the application
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // Event handler for the MouseDown event on the window to allow dragging
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Check if the left mouse button is pressed
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // Allow dragging the window
                DragMove();
            }
        }

        // Event handler for the Cars category button click event
        public void btnCars_Click(object sender, RoutedEventArgs e)
        {
            // Get the clicked button
            var button = (Button)sender;

            // Disable the Cars button and show the corresponding image
            btnCars.IsEnabled = false;
            imgCar.Visibility = Visibility.Visible;

            // Change the background color of the clicked button
            ChangeButtonColour(button);

            // Make the game UI visible for the selected category
            GameUIVisible();

            // Set the flag for the Cars category to true
            car = true;

            // Check if the Cars button is disabled
            if (!btnCars.IsEnabled)
            {
                // Start the fade-out effect and restart the game with Cars category words
                FadeOutStart();
                RestartGameCar(carParts);
            }
        }

        // Event handler for the Animals category button click event
        private void btnAnimals_Click(object sender, RoutedEventArgs e)
        {
            // Get the clicked button
            var button = (Button)sender;

            // Disable the Animals button and show the corresponding image
            btnAnimals.IsEnabled = false;
            imgAnimal.Visibility = Visibility.Visible;

            // Change the background color of the clicked button
            ChangeButtonColour(button);

            // Make the game UI visible for the selected category
            GameUIVisible();

            // Set the flag for the Animals category to true
            animal = true;

            // Check if the Animals button is disabled
            if (!btnAnimals.IsEnabled)
            {
                // Start the fade-out effect and restart the game with Animals category words
                FadeOutStart();
                RestartGameCar(animals);
            }
        }


        // Event handler for the Countries category button click event
        private void btnCountries_Click(object sender, RoutedEventArgs e)
        {
            // Get the clicked button
            var button = (Button)sender;

            // Disable the Countries button and show the corresponding image
            btnCountries.IsEnabled = false;
            imgCountries.Visibility = Visibility.Visible;

            // Change the background color of the clicked button
            ChangeButtonColour(button);

            // Make the game UI visible for the selected category
            GameUIVisible();

            // Set the flag for the Countries category to true
            country = true;

            // Check if the Countries button is disabled
            if (!btnCountries.IsEnabled)
            {
                // Start the fade-out effect and restart the game with Countries category words
                FadeOutStart();
                RestartGameCar(countryParts);
            }
        }


        // Event handler for the Programming Languages category button click event
        private void btnProgrammingLanguages_Click(object sender, RoutedEventArgs e)
        {
            // Get the clicked button
            var button = (Button)sender;

            // Disable the Programming Languages button and show the corresponding image
            btnProgrammingLanguages.IsEnabled = false;
            imgProgramming.Visibility = Visibility.Visible;

            // Change the background color of the clicked button
            ChangeButtonColour(button);

            // Make the game UI visible for the selected category
            GameUIVisible();

            // Set the flag for the Programming Languages category to true
            programmingLanguage = true;

            // Check if the Programming Languages button is disabled
            if (!btnProgrammingLanguages.IsEnabled)
            {
                // Start the fade-out effect and restart the game with Programming Languages category words
                FadeOutStart();
                RestartGameCar(programmingParts);
            }
        }

        // Event handler for the Fruits category button click event
        private void btnFruits_Click(object sender, RoutedEventArgs e)
        {
            // Get the clicked button
            var button = (Button)sender;

            // Disable the Fruits button and show the corresponding image
            btnFruits.IsEnabled = false;
            imgFruit.Visibility = Visibility.Visible;

            // Change the background color of the clicked button
            ChangeButtonColour(button);

            // Make the game UI visible for the selected category
            GameUIVisible();

            // Set the flag for the Fruits category to true
            fruit = true;

            // Check if the Fruits button is disabled
            if (!btnFruits.IsEnabled)
            {
                // Start the fade-out effect and restart the game with Fruits category words
                FadeOutStart();
                RestartGameCar(fruitParts);
            }
        }

        // Event handlers for mouse enter and leave events for the Cars category button
        private void btnCars_MouseEnter(object sender, MouseEventArgs e)
        {
            // Show the Cars category image on mouse enter
            imgCar.Visibility = Visibility.Visible;
        }

        private void btnCars_MouseLeave(object sender, MouseEventArgs e)
        {
            // Hide the Cars category image on mouse leave if the button is enabled
            if (btnCars.IsEnabled)
                imgCar.Visibility = Visibility.Hidden;
        }

        // Event handlers for mouse enter and leave events for the Animals category button
        private void btnAnimals_MouseEnter(object sender, MouseEventArgs e)
        {
            // Show the Animals category image on mouse enter
            imgAnimal.Visibility = Visibility.Visible;
        }

        private void btnAnimals_MouseLeave(object sender, MouseEventArgs e)
        {
            // Hide the Animals category image on mouse leave if the button is enabled
            if (btnAnimals.IsEnabled)
                imgAnimal.Visibility = Visibility.Hidden;
        }

        // Event handlers for mouse enter and leave events for the Countries category button
        private void btnCountries_MouseEnter(object sender, MouseEventArgs e)
        {
            // Show the Countries category image on mouse enter
            imgCountries.Visibility = Visibility.Visible;
        }

        private void btnCountries_MouseLeave(object sender, MouseEventArgs e)
        {
            // Hide the Countries category image on mouse leave if the button is enabled
            if (btnCountries.IsEnabled)
                imgCountries.Visibility = Visibility.Hidden;
        }

        // Event handlers for mouse enter and leave events for the Fruits category button
        private void btnFruits_MouseEnter(object sender, MouseEventArgs e)
        {
            // Show the Fruits category image on mouse enter
            imgFruit.Visibility = Visibility.Visible;
        }

        private void btnFruits_MouseLeave(object sender, MouseEventArgs e)
        {
            // Hide the Fruits category image on mouse leave if the button is enabled
            if (btnFruits.IsEnabled)
                imgFruit.Visibility = Visibility.Hidden;
        }


        // Event handler for mouse enter and leave events for the Programming Languages category button
        private void btnProgrammingLanguages_MouseEnter(object sender, MouseEventArgs e)
        {
            // Show the Programming Languages category image on mouse enter
            imgProgramming.Visibility = Visibility.Visible;
        }

        private void btnProgrammingLanguages_MouseLeave(object sender, MouseEventArgs e)
        {
            // Hide the Programming Languages category image on mouse leave if the button is enabled
            if (btnProgrammingLanguages.IsEnabled)
                imgProgramming.Visibility = Visibility.Hidden;
        }

        // Method to initiate the fade-out effect and disable UI elements
        private void FadeOutStart()
        {
            // Disable other buttons and fade out UI elements
            foreach (Button x in optionButtons) { FadeOutUI(x); DisableButton(x); }
            foreach (Image y in optionImages) { FadeOutUI(y); }
            FadeOutUI(txthQuestion);
        }

        // Method to make various game UI elements visible
        private void GameUIVisible()
        {
            // Make alphabet buttons and health images visible
            foreach (Button button in alphabetbuttons)
            {
                button.Visibility = Visibility.Visible;
            }

            foreach (Image image in sirImageHealth)
            {
                image.Visibility = Visibility.Visible;
            }

            // Make health, letters, statement, wins, and losses textboxes visible
            txtHealth.Visibility = Visibility.Visible;
            txtLetters.Visibility = Visibility.Visible;
            txtStatement.Visibility = Visibility.Visible;
            txtLosses.Visibility = Visibility.Visible;
            txtWins.Visibility = Visibility.Visible;
        }

        // Method to initiate the fade-out effect for a specific UI element
        private void FadeOutUI(UIElement targetElement)
        {
            targetElement.Visibility = Visibility.Hidden;
        }

        // Method to disable a button
        private void DisableButton(Button button)
        {
            button.IsEnabled = false;
        }

        // Method to enable a button
        private void EnableButton(Button button)
        {
            button.IsEnabled = true;
        }


        // Method to change the background color of a button
        private void ChangeButtonColour(Button button)
        {
            button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4C70"));
        }



        private void RestartGameCar(List<string> wordList)
        {
            // Pick a word from the specified category
            HangmanWord = PickWord(wordList);
            charArrayHangmanWord = HangmanWord.ToCharArray();

            // Initialize a StringBuilder with underscores
            StringBuilder initialWord = new StringBuilder();
            foreach (char l in charArrayHangmanWord)
            {
                initialWord.Append('_');
                initialWord.Append(" ");
            }


            // Store the initial word string in initialWordString
            initialWordString = initialWord.ToString();
            charArrayLetters = initialWordString.ToCharArray();
            // Display the initial word string in the UI
            txtLetters.Text = initialWordString;
        }


        private string PickWord(List<string> randomWords)
        {
            // Check if there are words remaining
            if (randomWords.Count == 0)
            {
                // Handle the case where all words have been used.
                // You might want to reset the list or inform the user.
                // For now, let's return an empty string.
                return string.Empty;
            }

            // Create a random number generator
            Random random = new Random();

            // Generate a random index within the bounds of the list
            int index = random.Next(randomWords.Count);

            // Get the word at the randomly selected index
            HangmanWord = randomWords[index];

            // Remove the selected word from the list to avoid repetition
            randomWords.RemoveAt(index);

            // Return the selected word
            return HangmanWord;
        }


        // Event handler for the Home button click
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            // Create an instance of the MainWindow and display it, then close the current window
            GameProjectFinal.MainWindow mainWindow = new GameProjectFinal.MainWindow();
            mainWindow.Show();
            this.Close();
        }

        // Event handler for the Restart button click
        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            // Restart the game based on the selected category
            if (car)
            {
                RestartGameCar(carParts);
                wordWins = 0;
                txtHealth.Text = "Health Points: 5";

                // Enable alphabet buttons, reset their background color, and make health images visible
                foreach (Button b in alphabetbuttons)
                {
                    EnableButton(b);
                    b.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#39385D"));
                }
                foreach (Image image in sirImageHealth)
                {
                    image.Visibility = Visibility.Visible;
                }
            }
            if (animal)
            {
                RestartGameCar(animals);
                wordWins = 0;
                txtHealth.Text = "Health Points: 5";

                // Enable alphabet buttons, reset their background color, and make health images visible
                foreach (Button b in alphabetbuttons)
                {
                    EnableButton(b);
                    b.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#39385D"));
                }
                foreach (Image image in sirImageHealth)
                {
                    image.Visibility = Visibility.Visible;
                }
            }
            if (country)
            {
                RestartGameCar(countryParts);
                wordWins = 0;
                txtHealth.Text = "Health Points: 5";

                // Enable alphabet buttons, reset their background color, and make health images visible
                foreach (Button b in alphabetbuttons)
                {
                    EnableButton(b);
                    b.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#39385D"));
                }
                foreach (Image image in sirImageHealth)
                {
                    image.Visibility = Visibility.Visible;
                }
            }
            if (fruit)
            {
                RestartGameCar(fruitParts);
                wordWins = 0;
                txtHealth.Text = "Health Points: 5";

                // Enable alphabet buttons, reset their background color, and make health images visible
                foreach (Button b in alphabetbuttons)
                {
                    EnableButton(b);
                    b.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#39385D"));
                }
                foreach (Image image in sirImageHealth)
                {
                    image.Visibility = Visibility.Visible;
                }
            }
            if (programmingLanguage)
            {
                RestartGameCar(programmingParts);
                wordWins = 0;
                txtHealth.Text = "Health Points: 5";

                // Enable alphabet buttons, reset their background color, and make health images visible
                foreach (Button b in alphabetbuttons)
                {
                    EnableButton(b);
                    b.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#39385D"));
                }
                foreach (Image image in sirImageHealth)
                {
                    image.Visibility = Visibility.Visible;
                }
            }
        }


        // Method to check the guessed letter against the hangman word
        public void CheckGame(char guessedLetter)
        {
            // Iterate through each character in the hangman word
            for (int i = 0; i < charArrayHangmanWord.Length; i++)
            {
                char l = charArrayHangmanWord[i];

                // Check if the guessed letter matches the current character (case-insensitive)
                if (char.ToUpperInvariant(guessedLetter) == char.ToUpperInvariant(l))
                {
                    // Update the displayed letters with the correct guessed letter
                    charArrayLetters[i * 2] = l;  // Use i * 2 to update every second position
                    wordWins--;
                }
            }

            // Increment wordWins to track the total correct guesses
            wordWins++;

            // Update the displayed letters in the UI
            txtLetters.Text = new string(charArrayLetters);

            // Check if the player has won or lost
            CheckWinCon(wordWins);
        }

        // Event handler for the letter buttons
        private void btnA_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            // Get the guessed letter from the button content
            char UserGuessedLetter = Convert.ToChar(button.Content);

            // Change the button color, disable the button, and check the guessed letter
            ChangeButtonColour(button);
            button.IsEnabled = false;
            CheckGame(UserGuessedLetter);
        }

        // Method to check the win/loss condition
        private void CheckWinCon(int wordWins)
        {
            string lettersText = txtLetters.Text;
            string rightWord = lettersText.Replace(" ", "");

            // Calculate the remaining health points
            int lifePointsHits = lifePoints - wordWins;

            // Update the displayed health points and hide the corresponding health image
            if (lifePointsHits < lifePoints)
            {
                txtHealth.Text = "Health Points:" + lifePointsHits;
                sirImageHealth[lifePointsHits].Visibility = Visibility.Hidden;
            }

            // Check for a win
            if (wordWins < 5)
            {
                if (rightWord == HangmanWord)
                {
                    gameWins++;
                    MessageBox.Show("You got the word correct!");
                    txtWins.Text = ("Wins: " + gameWins);
                    foreach (Button button in alphabetbuttons)
                    {
                        button.IsEnabled = false;
                    }
                }
            }
            // Check for a loss
            else if (wordWins == 5)
            {
                foreach (Button b in alphabetbuttons)
                {
                    DisableButton(b);
                    b.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#39385D"));
                }
                gameLosses++;
                txtLetters.Text = HangmanWord;
                MessageBox.Show("You got the word wrong and ran out of guesses!");
                txtLosses.Text = ("Losses: " + gameLosses);
            }
        }


    }
}
