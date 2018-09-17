using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Int32;

namespace HowToMove
{
    public partial class Form1 : Form
    {
        public const int ArraySize = 10;
        private readonly Button[,] _exploringArray = new Button[ArraySize, ArraySize];
        private readonly Button[,] _retreatingArray = new Button[ArraySize, ArraySize];

        private Button _setupButton;
        private Button _startButton;
        private Button _startOverButton;
        private Button _goBackButton;
        private TextBox _stepCounterTextBox;
        private Label _stepCounterLabel;

        private int _counter;
        private bool _isSetupActive;
        private const int BatteryCapacity = 38;
        

        #region CreateForm

        public Form1()
        {
            InitializeComponent();
            Size = new Size(900, 545);
            Text = @"How To Walk";
            Icon = null;
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimizeBox = false;
            MaximizeBox = false;
        }
        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            CreateSetupButton();
            CreateStartButton();
            CreateStartOverButton();
            CreateGoBackButton();
            CreateStepCounterTextBox();
            //CreateBatteryButton(); //TODO

            DisableGoBackButton();
            DisableStartOverButton();

            CreateExploringArea();
            PaintButtons(_exploringArray, Color.BurlyWood);
            CreateRetreatingArea();
            DisableButtons();
            
        }


        #region CreateTextBoxes

        private void CreateStepCounterTextBox()
        {
            _stepCounterTextBox = new TextBox
            {
                Size = new Size(50, 50),
                Location = new Point(750, 30),
                Font = new Font(Font.FontFamily, 20),
                TextAlign = HorizontalAlignment.Center,
                Text = _counter.ToString()
            };
            Controls.Add(_stepCounterTextBox);

            _stepCounterLabel = new Label
            {
                Size = new Size(200, 50),
                Location = new Point(550, 30),
                Font = new Font(Font.FontFamily, 20),
                TextAlign = ContentAlignment.TopCenter,
                Text = @"Steps counter:"
            };
            Controls.Add(_stepCounterLabel);
        }

        #endregion


        #region CreateButtons

        private void CreateSetupButton()
        {
            _setupButton = new Button
            {
                Size = new Size(80, 30),
                Location = new Point(350, 20),
                Text = @"Setup",
                TextAlign = ContentAlignment.MiddleCenter,
                Enabled = true
            };
            _setupButton.Click += Setup;
            Controls.Add(_setupButton);
        }
        private void CreateStartButton()
        {
            _startButton = new Button
            {
                Size = new Size(80, 30),
                Location = new Point(450, 20),
                Text = @"Start",
                TextAlign = ContentAlignment.MiddleCenter,
                Enabled = true
            };
            _startButton.Click += Start;
            Controls.Add(_startButton);
        }
        private void CreateBatteryButton() //TODO
        {
            var batteryButton = new Button
            {
                Size = new Size(80, 30),
                Location = new Point(400, 20),
                Text = @"Low Battery!",
                TextAlign = ContentAlignment.MiddleCenter,
                Enabled = true
            };
            batteryButton.Click += Return;
            Controls.Add(batteryButton);
        }
        private void CreateStartOverButton()
        {
            _startOverButton = new Button
            {
                Size = new Size(80, 30),
                Location = new Point(350, 50),
                Text = @"Start Over!",
                TextAlign = ContentAlignment.MiddleCenter,
                Enabled = true
            };
            _startOverButton.Click += StartOver;
            Controls.Add(_startOverButton);
        }
        private void CreateGoBackButton()
        {
            _goBackButton = new Button
            {
                Size = new Size(80, 30),
                Location = new Point(450, 50),
                Text = @"Go Back!",
                TextAlign = ContentAlignment.MiddleCenter,
                Enabled = true
            };
            _goBackButton.Click += GoBack;
            Controls.Add(_goBackButton);
        }

        #endregion

        #region Enabling/DisablingButtons

        private void DisableSetupButton()
        {
            _setupButton.Enabled = false;
        }
        private void EnableSetupButton()
        {
            _setupButton.Enabled = true;
        }
        private void DisableStartButton()
        {
            _startButton.Enabled = false;
        }
        private void EnableStartButton()
        {
            _startButton.Enabled = true;
        }
        private void DisableStartOverButton()
        {
            _startOverButton.Enabled = false;
        }
        private void EnableStartOverButton()
        {
            _startOverButton.Enabled = true;
        }
        private void DisableGoBackButton()
        {
            _goBackButton.Enabled = false;
        }
        private void EnableGoBackButton()
        {
            _goBackButton.Enabled = true;
        }

        #endregion

        #region CreateEvents

        private void Return(object sender, EventArgs e) //TODO
        {
            GoBack(sender, e);
            System.Threading.Thread.Sleep(1000);
            StartOver(sender, e);
        }
        private void StartOver(object sender, EventArgs e)
        {
            EnableGoBackButton();
            PaintButtons(_exploringArray, Color.BurlyWood);
            PaintButtons(_retreatingArray, Color.Aquamarine);
            EnableOnlyStartButton();
            _counter = 0;
            UpdateStepCounter();
        }
        private void GoBack(object sender, EventArgs e)
        {
            var lastClicked = FindLastClickedButton();
            if (lastClicked == null) return;
            GetCoordinates(lastClicked, _exploringArray, out int x, out int y);
            _retreatingArray[x, y].BackColor = Color.Red;
            int old_x = x;
            int old_y = y;
            int new_x = old_x;
            int new_y = old_y;
            int minimum;
            do
            {
                GetSurroundingButtonsValues(_retreatingArray[old_x, old_y], _retreatingArray, out int right,
                    out int left, out int above, out int below);
                if (right == -1) right = MaxValue;
                if (left == -1) left = MaxValue;
                if (above == -1) above = MaxValue;
                if (below == -1) below = MaxValue;
                int min = FindMinimum(right, left, above, below);
                if (min == above) GoUp(old_x, out new_x, old_y, out new_y);
                else if (min == left) GoLeft(old_x, out new_x, old_y, out new_y);
                else if (min == right) GoRight(old_x, out new_x, old_y, out new_y);
                else if (min == below) GoDown(old_x, out new_x, old_y, out new_y);
                old_x = new_x;
                old_y = new_y;
                minimum = min;
            } while (minimum != 0);
        }
        private void Setup(object sender, EventArgs e)
        {
            _isSetupActive = true;
            PaintButtons(_exploringArray, Color.Chartreuse);
            EnableButtons();
        }
        private void Start(object sender, EventArgs e)
        {
            _isSetupActive = false;
            PaintButtons(_exploringArray, Color.BurlyWood);
            EnableOnlyStartButton();
            DisableSetupButton();
            DisableStartButton();
            EnableStartOverButton();
            EnableGoBackButton();
        }


        #endregion

        #endregion

        #region ExploringArea

        private void CreateExploringArea()
        {
            var horizotal = 30;
            var vertical = 90;
            const int initialValue = 0;

            for (var i = 0; i < ArraySize; i++)
            {
                for (var j = 0; j < ArraySize; j++)
                {
                    _exploringArray[i, j] = new Button
                    {
                        Size = new Size(40, 40),
                        Location = new Point(horizotal, vertical),
                        Text = initialValue.ToString(),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Enabled = false
                    };
                    _exploringArray[i, j].Click += ExploreNextField;
                    if ((j + 1) % ArraySize == 0)
                    {
                        vertical = 90;
                        horizotal = horizotal + 40;
                    }
                    else
                        vertical = vertical + 40;
                    Controls.Add(_exploringArray[i, j]);
                }
            }
        }
        public void ExploreNextField(object sender, EventArgs e)
        {
            if (_isSetupActive)
            {
                ((Button) sender).Text = MaxValue.ToString();
                ((Button)sender).BackColor = Color.Black;
            }
            else
            {
                PaintButtons(_exploringArray, Color.BurlyWood);
                ((Button)sender).BackColor = Color.Chartreuse;
                AddValue(sender);
                DisableButtons();
                SaveInformationToRetreatingArray(sender);
                EnableButtonsForNextStep(sender);
                UpdateStepCounter();
                if (_counter != BatteryCapacity / 2) return;
                GoBack(sender, e);
                DisableGoBackButton();
                DisableButtons();
            }
        }
        private Button FindLastClickedButton()
        {
            for (var i = 0; i < ArraySize; i++)
            {
                for (var j = 0; j < ArraySize; j++)
                {
                    if (_exploringArray[i, j].BackColor == Color.Chartreuse)
                        return _exploringArray[i, j];
                }
            }
            return null;
        }
        private void SaveInformationToRetreatingArray(object sender)
        {
            GetCoordinates(sender, _exploringArray, out int x, out int y);
            if (Parse(_retreatingArray[x, y].Text) >= _counter || Parse(_retreatingArray[x, y].Text) == -1)
                _retreatingArray[x, y].Text = _counter.ToString();
            _retreatingArray[x, y].BackColor = Color.Aquamarine;
            _counter++;
        }
        private static void AddValue(object sender)
        {
            var value = Parse(((Button)sender).Text);
            value++;
            ((Button)sender).Text = value.ToString();
        }

        #region EnablingAndDisablingButtons

        private void EnableOnlyStartButton()
        {
            DisableButtons();
            _exploringArray[0, 0].Enabled = true;
            _exploringArray[0, 0].BackColor = Color.Chartreuse;
        }
        private void DisableButtons()
        {
            for (var i = 0; i < ArraySize; i++)
            {
                for (var j = 0; j < ArraySize; j++)
                {
                    _exploringArray[i, j].Enabled = false;
                }
            }
        }
        private void EnableButtons()
        {
            for (var i = 0; i < ArraySize; i++)
            {
                for (var j = 0; j < ArraySize; j++)
                {
                    _exploringArray[i, j].Enabled = true;
                }
            }
        }
        private void EnableButtonsForNextStep(object sender)
        {
            GetSurroundingButtonsValues(sender, _exploringArray, out int right, out int left, out int above, out int below);
            int min = FindMinimum(right, left, above, below);
            if (right == min)
                EnableButtonOnRight(sender);
            if (left == min)
                EnableButtonOnLeft(sender);
            if (above == min)
                EnableButtonAbove(sender);
            if (below == min)
                EnableButtonBelow(sender);
        }
        private void EnableButtonOnRight(object sender)
        {
            GetCoordinates(sender, _exploringArray, out int x, out int y);
            if (x == -1 || y == -1) return;
            _exploringArray[x + 1, y].Enabled = true;
            _exploringArray[x + 1, y].BackColor = Color.Aquamarine;
        }
        private void EnableButtonOnLeft(object sender)
        {
            GetCoordinates(sender, _exploringArray, out int x, out int y);
            if (x == -1 || y == -1) return;
            _exploringArray[x - 1, y].Enabled = true;
            _exploringArray[x - 1, y].BackColor = Color.Aquamarine;
        }
        private void EnableButtonAbove(object sender)
        {
            GetCoordinates(sender, _exploringArray, out int x, out int y);
            if (x == -1 || y == -1) return;
            _exploringArray[x, y - 1].Enabled = true;
            _exploringArray[x, y - 1].BackColor = Color.Aquamarine;
        }
        private void EnableButtonBelow(object sender)
        {
            GetCoordinates(sender, _exploringArray, out int x, out int y);
            if (x == -1 || y == -1) return;
            _exploringArray[x, y + 1].Enabled = true;
            _exploringArray[x, y + 1].BackColor = Color.Aquamarine;
        }

        #endregion

        #endregion

        #region RetreatingArea

        private void CreateRetreatingArea()
        {
            var horizotal = 450;
            var vertical = 90;
            const int initialValue = MaxValue;

            for (var i = 0; i < ArraySize; i++)
            {
                for (var j = 0; j < ArraySize; j++)
                {
                    _retreatingArray[i, j] = new Button
                    {
                        Size = new Size(40, 40),
                        Location = new Point(horizotal, vertical),
                        Text = initialValue.ToString(),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Enabled = false,
                        BackColor = Color.Black
                    };
                    if ((j + 1) % ArraySize == 0)
                    {
                        vertical = 90;
                        horizotal = horizotal + 40;
                    }
                    else
                        vertical = vertical + 40;
                    Controls.Add(_retreatingArray[i, j]);
                }
            }
        }

        #region ChooseDirectionToMove

        private void GoDown(int old_x, out int new_x, int old_y, out int new_y)
        {
            if (GetCoordinates(_retreatingArray[old_x, old_y], _retreatingArray, out new_x, out new_y) &&
                        old_y + 1 <= 9)
            {
                new_x = old_x;
                new_y = old_y + 1;
                _retreatingArray[new_x, new_y].BackColor = Color.Red;
            }
            GetCoordinates(_retreatingArray[new_x, new_y], _retreatingArray, out new_x, out new_y);
        }
        private void GoUp(int old_x, out int new_x, int old_y, out int new_y)
        {
            if (GetCoordinates(_retreatingArray[old_x, old_y], _retreatingArray, out new_x, out new_y) &&
                        old_y - 1 >= 0)
            {
                new_x = old_x;
                new_y = old_y - 1;
                _retreatingArray[new_x, new_y].BackColor = Color.Red;
            }

            GetCoordinates(_retreatingArray[new_x, new_y], _retreatingArray, out new_x, out new_y);
        }
        private void GoLeft(int old_x, out int new_x, int old_y, out int new_y)
        {
            if (GetCoordinates(_retreatingArray[old_x, old_y], _retreatingArray, out new_x, out new_y) &&
                        old_x - 1 >= 0)
            {
                new_x = old_x - 1;
                new_y = old_y;
                _retreatingArray[new_x, new_y].BackColor = Color.Red;
            }
            GetCoordinates(_retreatingArray[new_x, new_y], _retreatingArray, out new_x, out new_y);
        }
        private void GoRight(int old_x, out int new_x, int old_y, out int new_y)
        {
            if (GetCoordinates(_retreatingArray[old_x, old_y], _retreatingArray, out new_x, out new_y) &&
                        old_x + 1 <= 9)
            {
                new_x = old_x + 1;
                new_y = old_y;
                _retreatingArray[new_x, new_y].BackColor = Color.Red;
            }
            GetCoordinates(_retreatingArray[new_x, new_y], _retreatingArray, out new_x, out new_y);
        }

        #endregion

        #endregion

        #region Common

        private void PaintButtons(Button[,] array, Color color)
        {
            void Work()
            {
                for (var i = 0; i < ArraySize; i++)
                {
                    for (var j = 0; j < ArraySize; j++)
                    {
                        if (array == _retreatingArray)
                        {
                            if (array[i, j].BackColor == Color.Red)
                                array[i, j].BackColor = color;
                            if (Parse(array[i, j].Text) == -1)
                                array[i, j].BackColor = Color.Black;
                        }
                        else
                        {
                            array[i, j].BackColor = color;
                            if ((Parse(array[i, j].Text) == 0 && !_isSetupActive) || array[i, j].Text == MaxValue.ToString())
                                array[i, j].BackColor = Color.Black;
                        }
                    }
                }
            }

            Work();
        }
        private static int FindMinimum(int right, int left, int above, int below)
        {
            var minX = Math.Min(right, left);
            var minY = Math.Min(above, below);
            var min = Math.Min(minX, minY);
            return min;
        }
        private static void GetSurroundingButtonsValues(object button, Button[,] array, out int right, out int left, out int above, out int below)
        {
            right = MaxValue;
            left = MaxValue;
            above = MaxValue;
            below = MaxValue;
            if (GetCoordinates(button, array, out var x, out var y) && x + 1 <= 9)
                right = GetButtonValue(array[x + 1, y]);
            if (GetCoordinates(button, array, out x, out y) && x - 1 >= 0)
                left = GetButtonValue(array[x - 1, y]);
            if (GetCoordinates(button, array, out x, out y) && y + 1 <= 9)
                below = GetButtonValue(array[x, y + 1]);
            if (GetCoordinates(button, array, out x, out y) && y - 1 >= 0)
                above = GetButtonValue(array[x, y - 1]);
        }
        private static int GetButtonValue(object button)
        {
            return Parse(((Button)button).Text);
        }
        private static bool GetCoordinates(object button, Button[,] array, out int x, out int y)
        {
            x = -1;
            y = -1;

            for (var i = 0; i < ArraySize; i++)
            {
                for (var j = 0; j < ArraySize; j++)
                {
                    if (array[i, j] != button) continue;
                    x = i;
                    y = j;
                    return true;
                }
            }
            return false;
        }
        private void UpdateStepCounter()
        {
            _stepCounterTextBox.Text = _counter.ToString();
        }

        #endregion
    }
}