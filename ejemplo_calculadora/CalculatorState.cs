using System;

namespace ejemplo_calculadora
{
    /// <summary>
    /// Mantiene el estado actual de la calculadora
    /// </summary>
    public class CalculatorState
    {
        #region Propiedades

        /// <summary>
        /// Expresión matemática actual mostrada en pantalla
        /// </summary>
        public string CurrentExpression { get; private set; }

        /// <summary>
        /// Indica si la próxima entrada será una nueva expresión
        /// </summary>
        public bool IsNewEntry { get; set; }

        /// <summary>
        /// Indica si el modo angular es radianes (true) o grados (false)
        /// </summary>
        public bool IsRadianMode { get; private set; }

        /// <summary>
        /// Indica si el modo de funciones trigonométricas es inverso
        /// </summary>
        public bool IsInverseMode { get; private set; }

        #endregion

        #region Constructor

        public CalculatorState()
        {
            Reset();
            IsRadianMode = true; // Por defecto en radianes
            IsInverseMode = false;
        }

        #endregion

        #region Métodos Públicos

        /// <summary>
        /// Reinicia la expresión actual a "0"
        /// </summary>
        public void Reset()
        {
            CurrentExpression = "0";
            IsNewEntry = true;
        }

        /// <summary>
        /// Actualiza la expresión actual
        /// </summary>
        public void UpdateExpression(string expression)
        {
            CurrentExpression = expression ?? "0";
        }

        /// <summary>
        /// Agrega texto a la expresión actual
        /// </summary>
        public void AppendToExpression(string text)
        {
            if (string.IsNullOrEmpty(text)) return;

            if (CurrentExpression == "0" || IsNewEntry)
            {
                CurrentExpression = text;
                IsNewEntry = false;
            }
            else
            {
                CurrentExpression += text;
            }
        }

        /// <summary>
        /// Elimina el último carácter de la expresión
        /// </summary>
        public void DeleteLastCharacter()
        {
            if (string.IsNullOrEmpty(CurrentExpression) || CurrentExpression == "0")
                return;

            CurrentExpression = ExpressionHelper.RemoveLastCharacter(CurrentExpression);

            if (string.IsNullOrEmpty(CurrentExpression))
            {
                Reset();
            }
        }

        /// <summary>
        /// Alterna entre modo radianes y grados
        /// </summary>
        public void ToggleRadianMode()
        {
            IsRadianMode = !IsRadianMode;
        }

        /// <summary>
        /// Alterna el modo de funciones trigonométricas inversas
        /// </summary>
        public void ToggleInverseMode()
        {
            IsInverseMode = !IsInverseMode;
        }

        /// <summary>
        /// Verifica si la expresión actual indica un error
        /// </summary>
        public bool IsError()
        {
            return CurrentExpression == CalculatorConstants.ErrorMessages.GenericError ||
                   CurrentExpression == CalculatorConstants.ErrorMessages.Infinity;
        }

        #endregion
    }
}