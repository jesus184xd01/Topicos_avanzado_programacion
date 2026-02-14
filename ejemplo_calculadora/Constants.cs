using System;

namespace ejemplo_calculadora
{
    /// <summary>
    /// Constantes globales de la calculadora
    /// </summary>
    public static class CalculatorConstants
    {
        // Dimensiones de ventana
        public const int WINDOW_WIDTH = 330;
        public const int WINDOW_HEIGHT = 520;

        // Configuración de fuente
        public const string FONT_PATH = "fonts/digital-7.ttf";
        public const int DISPLAY_FONT_SIZE = 36;

        /// <summary>
        /// Símbolos matemáticos utilizados en la interfaz
        /// </summary>
        public static class Symbols
        {
            public const string Multiply = "×";
            public const string Divide = "÷";
            public const string SquareRoot = "√";
            public const string Pi = "π";
            public const string Power = "^";
        }

        /// <summary>
        /// Mensajes de error mostrados en pantalla
        /// </summary>
        public static class ErrorMessages
        {
            public const string GenericError = "Error";
            public const string Infinity = "∞";
            public const string InvalidRootFormat = "Formato de raíz inválido. Use √(número)";
            public const string UnbalancedParentheses = "Paréntesis desbalanceados";
            public const string InvalidExpression = "Expresión inválida";
        }

        /// <summary>
        /// Etiquetas de botones
        /// </summary>
        public static class ButtonLabels
        {
            // Modos angulares
            public const string Radians = "RAD";
            public const string Degrees = "DEG";

            // Funciones trigonométricas normales
            public const string Sin = "SEN";
            public const string Cos = "COS";
            public const string Tan = "TAN";

            // Funciones trigonométricas inversas
            public const string ArcSin = "sin⁻¹";
            public const string ArcCos = "cos⁻¹";
            public const string ArcTan = "tan⁻¹";
        }
    }
}