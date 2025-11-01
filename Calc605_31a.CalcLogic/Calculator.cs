using System;
using System.Globalization;

namespace Calc605_31a.CalcLogic
{
    public class Calculator
    {
        private double? _operand1;
        private double? _operand2;
        private string _operation;
        private double _memory;

        private static readonly CultureInfo Invariant = CultureInfo.InvariantCulture;

        public string Display { get; private set; }

        public Calculator()
        {
            _operand1 = null;
            _operand2 = null;
            _operation = string.Empty;
            _memory = 0;
            Display = "0";
        }

        public string Input(string inputParam)
        {
            try
            {
                switch (inputParam)
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
                            Display = inputParam;
                        else
                            Display += inputParam;
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
                            if (!TryParseDisplay(out var v1)) return Display;
                            _operand1 = v1;
                        }
                        else if (!string.IsNullOrEmpty(_operation))
                        {
                            if (!TryParseDisplay(out var v2)) return Display;
                            _operand2 = v2;
                            Compute();
                        }

                        _operation = inputParam;
                        Display = "0";
                        return Display;

                    // ===== РАВНО =====
                    case "=":
                        if (_operand1 == null)
                        {
                            if (!TryParseDisplay(out var v3)) return Display;
                            _operand1 = v3;
                        }
                        else
                        {
                            if (!TryParseDisplay(out var v4)) return Display;
                            _operand2 = v4;
                        }

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

                    // ===== ПАМЯТЬ =====
                    case "MC":
                        _memory = 0;
                        Display = "0";  // очищаем экран
                        _operand1 = null;
                        _operand2 = null;
                        _operation = string.Empty;
                        return Display;

                    case "MR":
                        Display = FormatResult(_memory);
                        return Display;

                    case "M+":
                        if (!TryParseDisplay(out var addVal)) return Display;
                        _memory += addVal;
                        return Display;

                    case "M-":
                        if (!TryParseDisplay(out var subVal)) return Display;
                        _memory -= subVal;
                        return Display;

                    default:
                        throw new Exception($"Неизвестная команда: {inputParam}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Calculator error: {ex.GetType().Name}: {ex.Message}");
                return Display;
            }
        }

        private bool TryParseDisplay(out double value)
        {
            value = 0;
            if (string.IsNullOrWhiteSpace(Display)) return false;
            if (Display.Equals("NaN", StringComparison.OrdinalIgnoreCase) ||
                Display.Equals("Infinity", StringComparison.OrdinalIgnoreCase) ||
                Display.Equals("-Infinity", StringComparison.OrdinalIgnoreCase)) return false;

            return double.TryParse(Display, NumberStyles.Float | NumberStyles.AllowThousands, Invariant, out value);
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
