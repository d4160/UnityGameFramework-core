using System;
using GameCreator.Runtime.Common;
using UnityEngine;
using UnityEngine.UI;

namespace d4160.Runtime.Common
{
    [Title("String")]
    [Category("Values/String")]

    [Description("Gets the string value as a decimal number")]
    [Image(typeof(IconTextArea), ColorTheme.Type.TextLight)]

    [Serializable]
    [HideLabelsInEditor]
    public class GetDecimalString : PropertyTypeGetDecimal
    {
        [SerializeField]
        protected PropertyGetString m_String = new PropertyGetString();

        public override double Get(Args args)
        {
            string value = this.m_String.Get(args);
            //Debug.Log($"String value: {value}");

            if (string.IsNullOrEmpty(value)) return 0f;

            bool converted = float.TryParse(value, out float number);

            //Debug.Log($"Decimal value: {number}");

            return converted ? number : 0f;
        }

        public static PropertyGetDecimal Create => new PropertyGetDecimal(
            new GetDecimalString()
        );

        public override string String => this.m_String.ToString();
    }
}