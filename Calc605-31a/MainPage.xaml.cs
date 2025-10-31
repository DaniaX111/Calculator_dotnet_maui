using Calc605_31a.CalcLogic;

namespace Calc605_31a
{
    public partial class MainPage : ContentPage
    {
        private Calculator _calculator = new Calculator();

        public MainPage()
        {
            InitializeComponent();
            Display.Text = _calculator.Display;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            Display.Text = _calculator.Input(button.Text);
        }
    }
}

