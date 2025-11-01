using System;
using System.Globalization;

namespace Calc605_31a.CalcLogic
{
    public class Calculator
    {
        private double? _operand1 = null;
        private double? _operand2 = null;
        private string _operation = string.Empty;
        private double _memory = 0; // память калькулятора

        public string Display { get; private set; } = "0";

        private static readonly CultureInfo Invariant = CultureInfo.InvariantCulture;

        public string Input(string InputParam)
        {
            switch (InputParam)
            {
                // ===== ЦИФРЫ =====
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
                    if (Display == "0")
                        Display = InputParam;
                    else
                        Display += InputParam;
                    return Display;

                // ===== ТОЧКА =====
                case ".":
                    if (!Display.Contains("."))
                        Display += ".";
                    return Display;

                // ===== ОПЕРАЦИИ =====
                case "+":
                case "-":
                case "x":
                case "/":
                    if (_operand1 == null)
                    {
                        _operand1 = double.Parse(Display, Invariant);
                    }
                    else if (!string.IsNullOrEmpty(_operation))
                    {
                        _operand2 = double.Parse(Display, Invariant);
                        Compute();
                    }

                    _operation = InputParam;
                    Display = "0";
                    return Display;

                // ===== РАВНО =====
                case "=":
                    if (_operand1 == null)
                        _operand1 = double.Parse(Display, Invariant);
                    else
                        _operand2 = double.Parse(Display, Invariant);

                    Compute();
                    Display = FormatResult(_operand1);
                    _operation = string.Empty;
                    return Display;

                // ===== СТЕРКА =====
                case "←":
                    if (Display.Length > 1)
                        Display = Display[..^1];
                    else
                        Display = "0";
                    return Display;

                // ===== СБРОС =====
                case "C":
                    _operand1 = null;
                    _operand2 = null;
                    _operation = string.Empty;
                    Display = "0";
                    return Display;

                // ======== ПАМЯТЬ ========

                // Очистка памяти
                case "MC":
                    _memory = 0;
                    return Display;

                // Восстановление из памяти
                case "MR":
                    Display = FormatResult(_memory);
                    return Display;

                // Добавить к памяти
                case "M+":
                    _memory += double.Parse(Display, Invariant);
                    return Display;

                // Вычесть из памяти
                case "M-":
                    _memory -= double.Parse(Display, Invariant);
                    return Display;

                default:
                    throw new Exception($"Неизвестная команда: {InputParam}");
            }
        }

        private void Compute()
        {
            if (_operand1 == null || _operand2 == null) return;

            switch (_operation)
            {
                case "+": _operand1 += _operand2; break;
                case "-": _operand1 -= _operand2; break;
                case "x": _operand1 *= _operand2; break;
                case "/": _operand1 = _operand2 == 0 ? double.NaN : _operand1 / _operand2; break;
            }

            _operand2 = null;
        }

        private string FormatResult(double? value)
        {
            if (value == null) return "0";
            var str = value.Value.ToString("G15", Invariant);
            return str.Contains('.') ? str.TrimEnd('0').TrimEnd('.') : str;
        }
    }
}
