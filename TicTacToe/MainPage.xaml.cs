
namespace TicTacToe
{
    public partial class MainPage : ContentPage
    {
        bool move = true; //X = true, O = false
        char[][] board = {
            new char[] { ' ', ' ', ' ' },
            new char[] { ' ', ' ', ' ' },
            new char[] { ' ', ' ', ' ' }
        };
        int counter = 0;
        int xScore = 0;
        int oScore = 0;
        public MainPage()
        {
            InitializeComponent();

            for (int i = 1; i <= 9; i++)
            {
                Button button = this.FindByName<Button>("button" + i);

                if (button != null)
                {
                    button.Clicked += FieldClicked;
                }
            }
        }
        private void FieldClicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.Text = move ? 'X'.ToString() : 'O'.ToString();
            move = !move;
            button.IsEnabled = false;

            string s = button.ClassId;

            char name = s[s.Length - 1];
            int nameInt = name - '0';


            int row = 0;

            if (nameInt <= 3)
                row = 0;
            else if (nameInt >= 7)
                row = 2;
            else
                row = 1;

            int column = 0;
            if (nameInt % 3 == 0)
                column = 2;
            else if (nameInt % 3 == 2)
                column = 1;
            else
                column = 0;

            board[row][column] = button.Text[0];

            char c = CheckForWinner();
            if (c != ' ')
            {
                DisplayAlert("Game over", c + " player won", "Ok");

                ClearButtons();
                ClearBoard();

                move = true;
                counter = 0;

                if (c == 'X')
                {
                    xScore++;
                    xPoints.Text = xScore.ToString();
                }
                else
                {
                    oScore++;
                    oPoints.Text = oScore.ToString();
                }
            }
            else
                counter++;
            if (counter == 9)
            {
                DisplayAlert("Game over", "It's a draw", "Ok");

                ClearButtons();
                ClearBoard();

                move = true;
                counter = 0;
            }
        }
        private char CheckForWinner()
        {
            // Check horizontal lines
            for (int i = 0; i < 3; i++)
            {
                if (board[i][0] == board[i][1] && board[i][1] == board[i][2] && board[i][0] != ' ')
                    return board[i][0];
            }

            // Check vertical lines
            for (int i = 0; i < 3; i++)
            {
                if (board[0][i] == board[1][i] && board[1][i] == board[2][i] && board[0][i] != ' ')
                    return board[0][i];
            }

            // Check diagonals
            if (board[0][0] == board[1][1] && board[1][1] == board[2][2] && board[0][0] != ' ')
                return board[0][0];
            if (board[0][2] == board[1][1] && board[1][1] == board[2][0] && board[0][2] != ' ')
                return board[0][2];

            // No winner
            return ' ';
        }

        private void ClearButtons()
        {
            for (int i = 1; i <= 9; i++)
            {
                Button button = this.FindByName<Button>("button" + i);

                if (button != null)
                {
                    button.Text = ' '.ToString();
                    button.IsEnabled = true;
                }
            }
        }

        private void ClearBoard()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    board[i][j] = ' ';
        }

    }
}