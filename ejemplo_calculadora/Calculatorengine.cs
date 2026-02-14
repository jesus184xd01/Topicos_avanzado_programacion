using System;
using System.Data;
using System.Text.RegularExpressions;
using ejemplo_calculadora;

namespace ejemplo_calculadora
{
    /// <summary>
    /// Motor de cálculo que procesa y evalúa expresiones matemáticas
    /// </summary>
    public class CalculatorEngine
    {
        #region Métodos Públicos

        /// <summary>
        /// Evalúa una expresión matemática y retorna el resultado
        /// </summary>
        public double Evaluate(string expression)
        {
            expression = expression.Replace(" ", "");

            // Validar la expresión antes de procesarla
            if (!ValidateExpression(expression))
            {
                throw new Exception(CalculatorConstants.ErrorMessages.InvalidExpression);
            }

            expression = ProcessSquareRoots(expression);
            expression = ProcessExponents(expression);

            // Normalizar para DataTable.Compute
            expression = NormalizeForDataTable(expression);

            try
            {
                DataTable dt = new DataTable();
                var result = dt.Compute(expression, "");
                return Convert.ToDouble(result);
            }
            catch (SyntaxErrorException ex)
            {
                throw new Exception("Error de sintaxis: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al evaluar: " + ex.Message);
            }
        }

        /// <summary>
        /// Normaliza una expresión matemática a formato estándar
        /// </summary>
        public string NormalizeExpression(string expression)
        {
            // Eliminar TODOS los espacios primero
            expression = expression.Replace(" ", "");

            // Reemplazar 'x' minúscula por multiplicación ANTES de procesar decimales
            expression = Regex.Replace(expression, @"(\d)\s*x\s*(\d)", "$1*$2", RegexOptions.IgnoreCase);

            // Reemplazar operadores visuales por operadores estándar
            expression = expression.Replace(CalculatorConstants.Symbols.Multiply, "*")
                                   .Replace(CalculatorConstants.Symbols.Divide, "/")
                                   .Replace("X", "*")  // X mayúscula también
                                   .Replace(CalculatorConstants.Symbols.Pi, Math.PI.ToString());

            // Normalizar decimales para evitar errores
            // Caso 1: .5 → 0.5 (agregar 0 antes del punto decimal)
            expression = Regex.Replace(expression, @"(?<![0-9])\.(?=[0-9])", "0.");

            // Caso 2: 5. → 5.0 (agregar 0 después del punto decimal si va seguido de operador)
            expression = Regex.Replace(expression, @"(\d)\.(?=[\+\-\*/\)\^])", "$1.0");

            // Caso 3: 5. al final de la expresión → 5.0
            expression = Regex.Replace(expression, @"(\d)\.$", "$1.0");

            return expression;
        }

        /// <summary>
        /// Valida que una expresión sea correcta antes de evaluar
        /// </summary>
        public bool ValidateExpression(string expression)
        {
            // Verificar paréntesis balanceados
            int level = 0;
            foreach (char c in expression)
            {
                if (c == '(') level++;
                if (c == ')') level--;
                if (level < 0) return false;
            }
            if (level != 0) return false;

            // Verificar que no haya múltiples puntos decimales en un número
            if (Regex.IsMatch(expression, @"\d+\.\d*\.\d*"))
                return false;

            // Verificar que no termine con un operador (excepto paréntesis)
            if (expression.Length > 0)
            {
                char lastChar = expression[expression.Length - 1];
                if (lastChar == '+' || lastChar == '-' || lastChar == '*' || lastChar == '/')
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Formatea un resultado numérico para mostrar en pantalla
        /// </summary>
        public string FormatResult(double value)
        {
            // Enteros pequeños
            if (Math.Abs(value - Math.Floor(value)) < 1e-10 && Math.Abs(value) < 1e10)
                return ((long)value).ToString();

            // Notación científica para números muy grandes o muy pequeños
            if (Math.Abs(value) < 1e-6 || Math.Abs(value) > 1e10)
                return value.ToString("E6");

            // Números decimales normales
            return value.ToString("G10");
        }

        /// <summary>
        /// Calcula funciones trigonométricas
        /// </summary>
        public double CalculateTrigonometric(string function, double value, bool isRadianMode, bool isInverse)
        {
            double result = 0;
            double valueInRadians = isRadianMode ? value : value * Math.PI / 180;

            if (isInverse)
            {
                // Funciones inversas
                switch (function.ToLower())
                {
                    case "sen":
                    case "sin":
                        result = Math.Asin(value);
                        if (!isRadianMode) result = result * 180 / Math.PI;
                        break;
                    case "cos":
                        result = Math.Acos(value);
                        if (!isRadianMode) result = result * 180 / Math.PI;
                        break;
                    case "tan":
                        result = Math.Atan(value);
                        if (!isRadianMode) result = result * 180 / Math.PI;
                        break;
                }
            }
            else
            {
                // Funciones normales
                switch (function.ToLower())
                {
                    case "sen":
                    case "sin":
                        result = Math.Sin(valueInRadians);
                        break;
                    case "cos":
                        result = Math.Cos(valueInRadians);
                        break;
                    case "tan":
                        result = Math.Tan(valueInRadians);
                        break;
                }
            }

            return result;
        }

        #endregion

        #region Métodos Privados

        /// <summary>
        /// Procesa todas las raíces cuadradas en la expresión
        /// </summary>
        private string ProcessSquareRoots(string expression)
        {
            while (expression.Contains(CalculatorConstants.Symbols.SquareRoot))
            {
                int index = expression.IndexOf(CalculatorConstants.Symbols.SquareRoot);
                if (index + 1 >= expression.Length || expression[index + 1] != '(')
                    throw new Exception(CalculatorConstants.ErrorMessages.InvalidRootFormat);

                int closeParenIndex = FindClosingParenthesis(expression, index + 2);
                string content = expression.Substring(index + 2, closeParenIndex - (index + 2));
                double value = Evaluate(content);
                double result = Math.Sqrt(value);

                expression = expression.Substring(0, index) + result.ToString() +
                            expression.Substring(closeParenIndex + 1);
            }
            return expression;
        }

        /// <summary>
        /// Procesa todos los exponentes en la expresión
        /// </summary>
        private string ProcessExponents(string expression)
        {
            while (expression.Contains(CalculatorConstants.Symbols.Power))
            {
                int index = expression.IndexOf(CalculatorConstants.Symbols.Power);
                int baseStart = GetBaseStartPosition(expression, index);
                string baseStr = expression.Substring(baseStart, index - baseStart);

                string exponent;
                int expEnd;

                if (index + 1 < expression.Length && expression[index + 1] == '(')
                {
                    int closeParenIndex = FindClosingParenthesis(expression, index + 2);
                    exponent = expression.Substring(index + 2, closeParenIndex - (index + 2));
                    expEnd = closeParenIndex + 1;
                }
                else
                {
                    int expStart = index + 1;
                    expEnd = expStart;
                    while (expEnd < expression.Length &&
                           (char.IsDigit(expression[expEnd]) || expression[expEnd] == '.'))
                        expEnd++;
                    exponent = expression.Substring(expStart, expEnd - expStart);
                }

                double baseValue = Evaluate(baseStr);
                double expValue = Evaluate(exponent);
                double result = Math.Pow(baseValue, expValue);

                expression = expression.Substring(0, baseStart) + result.ToString() +
                            expression.Substring(expEnd);
            }
            return expression;
        }

        /// <summary>
        /// Encuentra el índice de cierre de paréntesis correspondiente
        /// </summary>
        private int FindClosingParenthesis(string expression, int startIndex)
        {
            int level = 1;
            int i = startIndex;

            while (i < expression.Length && level > 0)
            {
                if (expression[i] == '(') level++;
                else if (expression[i] == ')') level--;
                i++;
            }

            if (level != 0)
                throw new Exception(CalculatorConstants.ErrorMessages.UnbalancedParentheses);

            return i - 1;
        }

        /// <summary>
        /// Obtiene la posición de inicio de la base en una expresión de potencia
        /// </summary>
        private int GetBaseStartPosition(string expression, int exponentPosition)
        {
            int start = exponentPosition - 1;

            if (start >= 0 && expression[start] == ')')
            {
                int level = 1;
                start--;
                while (start >= 0 && level > 0)
                {
                    if (expression[start] == ')') level++;
                    else if (expression[start] == '(') level--;
                    start--;
                }
                start++;
            }
            else
            {
                while (start >= 0 &&
                       (char.IsDigit(expression[start]) || expression[start] == '.'))
                    start--;
                start++;
            }

            return start;
        }

        /// <summary>
        /// Normaliza expresión específicamente para DataTable.Compute
        /// </summary>
        private string NormalizeForDataTable(string expression)
        {
            // Manejar multiplicación por negativos: 5*-3 → 5*(-3)
            expression = Regex.Replace(expression, @"\*-(\d+\.?\d*)", "*(-$1)");

            // Manejar división por negativos: 10/-2 → 10/(-2)
            expression = Regex.Replace(expression, @"/-(\d+\.?\d*)", "/(-$1)");

            // Manejar suma de negativos: 5+-3 → 5+(-3)
            expression = Regex.Replace(expression, @"\+-(\d+\.?\d*)", "+(-$1)");

            return expression;
        }

        #endregion
    }
}