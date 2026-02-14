using System;
using System.Linq;

namespace ejemplo_calculadora
{
    /// <summary>
    /// Utilidades para manipular y analizar expresiones matemáticas
    /// </summary>
    public static class ExpressionHelper
    {
        /// <summary>
        /// Obtiene el último número de una expresión
        /// </summary>
        public static string GetLastNumber(string expression)
        {
            if (string.IsNullOrEmpty(expression)) return "";

            int lastOperatorPosition = -1;

            // Buscar el último operador básico
            for (int i = expression.Length - 1; i >= 0; i--)
            {
                char c = expression[i];

                if (IsBasicOperator(c))
                {
                    // Verificar que no sea un signo negativo al inicio de un número
                    if (c == '-' && i > 0 && IsBasicOperator(expression[i - 1]))
                    {
                        continue; // Es un número negativo, seguir buscando
                    }
                    lastOperatorPosition = i;
                    break;
                }
            }

            if (lastOperatorPosition >= 0)
                return expression.Substring(lastOperatorPosition + 1).Trim();

            return expression.Trim();
        }

        /// <summary>
        /// Determina si la expresión termina en un número (incluyendo decimales)
        /// </summary>
        public static bool EndsWithNumber(string expression)
        {
            if (string.IsNullOrEmpty(expression)) return false;

            char lastChar = expression[expression.Length - 1];

            // Si termina en dígito, es un número
            if (char.IsDigit(lastChar)) return true;

            // Si termina en punto decimal, verificar si hay un número antes
            if (lastChar == '.' && expression.Length >= 2)
            {
                return char.IsDigit(expression[expression.Length - 2]);
            }

            return false;
        }

        /// <summary>
        /// Verifica si un carácter es un operador básico
        /// </summary>
        public static bool IsBasicOperator(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/' ||
                   c == '×' || c == '÷';
        }

        /// <summary>
        /// Cuenta paréntesis abiertos y cerrados en una expresión
        /// </summary>
        public static void CountParentheses(string expression, out int open, out int close)
        {
            open = 0;
            close = 0;

            foreach (char c in expression)
            {
                if (c == '(') open++;
                if (c == ')') close++;
            }
        }

        /// <summary>
        /// Determina si se debe agregar multiplicación implícita antes del siguiente carácter
        /// </summary>
        public static bool ShouldAddMultiplication(string currentText, char nextChar)
        {
            if (string.IsNullOrEmpty(currentText)) return false;

            char lastChar = currentText[currentText.Length - 1];

            // Casos donde se agrega multiplicación automática:
            // número + paréntesis abierto: 5( → 5×(
            // paréntesis cerrado + número: )5 → )×5
            // paréntesis cerrado + paréntesis abierto: )( → )×(
            // número + π: 5π → 5×π
            // π + número: π5 → π×5
            // ) + π: )π → )×π
            // π + (: π( → π×(

            bool lastIsNumber = EndsWithNumber(currentText);
            bool lastIsCloseParen = lastChar == ')';
            bool lastIsPi = lastChar == 'π';

            bool nextIsOpenParen = nextChar == '(';
            bool nextIsNumber = char.IsDigit(nextChar);
            bool nextIsPi = nextChar == 'π';

            return (lastIsNumber && (nextIsOpenParen || nextIsPi)) ||
                   (lastIsCloseParen && (nextIsNumber || nextIsOpenParen || nextIsPi)) ||
                   (lastIsPi && (nextIsNumber || nextIsOpenParen));
        }

        /// <summary>
        /// Determina si se debe agregar multiplicación antes de una función (√, etc)
        /// </summary>
        public static bool ShouldAddMultiplicationBeforeFunction(string currentText)
        {
            if (string.IsNullOrEmpty(currentText)) return false;

            char lastChar = currentText[currentText.Length - 1];

            return EndsWithNumber(currentText) ||
                   lastChar == ')' ||
                   lastChar == 'π';
        }

        /// <summary>
        /// Remueve el último carácter o grupo de caracteres de una expresión
        /// </summary>
        public static string RemoveLastCharacter(string expression)
        {
            if (string.IsNullOrEmpty(expression)) return expression;

            int length = expression.Length;
            char lastChar = expression[length - 1];

            // Manejar π
            if (lastChar == 'π')
            {
                return expression.Substring(0, length - 1);
            }
            // Manejar exponentes con paréntesis "^("
            else if (lastChar == '(' && length >= 2 && expression[length - 2] == '^')
            {
                return expression.Substring(0, length - 2);
            }
            // Manejar raíces "√("
            else if (lastChar == '(' && length >= 2 && expression[length - 2] == '√')
            {
                return expression.Substring(0, length - 2);
            }
            else
            {
                return expression.Substring(0, length - 1);
            }
        }

        /// <summary>
        /// Valida que no haya múltiples puntos decimales en el último número
        /// </summary>
        public static bool CanAddDecimalPoint(string expression)
        {
            string lastNumber = GetLastNumber(expression);
            return !lastNumber.Contains(".");
        }
    }
}