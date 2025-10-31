namespace Calc605_31a.CalcLogic
{
    public class Calculator
    {
        private double? _operand1 = null;
        private double? _operand2 = null;
        private string _operation = string.Empty;

        public string Display { get; private set; } = "0";

        public string Input(string InputParam)
        {
            switch (InputParam)
            {
                // ===== ЧИСЛА =====
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    if (!string.IsNullOrEmpty(_operation) && _operand1 == null)
                    {
                        _operand1 = Convert.ToDouble(Display);
                        Display = "0";
                    }

                    if (Display == "0")
                    {
                        Display = InputParam;
                    }
                    else
                    {
                        Display += InputParam;
                    }
                    return Display;

                // ===== ОПЕРАЦИИ =====
                case "+":
                case "-":
                case "x":
                case "/":
                    _operation = InputParam;
                    _operand2 = null; // сбросить второй операнд
                    return Display;

                // ===== РАВНО =====
                case "=":
                    if (_operand1 == null) _operand1 = Convert.ToDouble(Display);
                    if (_operand2 == null) _operand2 = Convert.ToDouble(Display);

                    if (_operation == "+") _operand1 = _operand1 + _operand2;
                    if (_operation == "-") _operand1 = _operand1 - _operand2;
                    if (_operation == "x") _operand1 = _operand1 * _operand2;
                    if (_operation == "/") _operand1 = _operand2 == 0 ? double.NaN : _operand1 / _operand2;

                    Display = _operand1.ToString();
                    return Display;

                // ===== СТЁРКА: Удалить один символ =====
                case "←":
                    if (Display.Length > 1)
                        Display = Display.Substring(0, Display.Length - 1);
                    else
                        Display = "0";
                    return Display;

                // ===== CLEAR: Полная очистка =====
                case "C":
                    _operand1 = null;
                    _operand2 = null;
                    _operation = string.Empty;
                    Display = "0";
                    return Display;

                default:
                    throw new Exception("O_o");
            }
        }
    }
}
