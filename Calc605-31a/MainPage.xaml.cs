using Calc605_31a.CalcLogic;

namespace Calc605_31a
{
    public partial class MainPage : ContentPage
    {
        private readonly Calculator _calculator = new Calculator();

        public MainPage()
        {
            InitializeComponent();
            Display.Text = _calculator.Display;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (sender is not Button button) return;

            // Нормализуем текст кнопки для корректной работы на iOS
            string inputParam = button.Text?.Normalize(System.Text.NormalizationForm.FormC).Trim() ?? "";

            try
            {
                Display.Text = _calculator.Input(inputParam);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Button click error: {ex.GetType().Name}: {ex.Message}");
            }
        }
    }
}