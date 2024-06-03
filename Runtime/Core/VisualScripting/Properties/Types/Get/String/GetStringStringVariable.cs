using System;
using d4160.Variables;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace d4160.Runtime.Core.GameCreator
{
    [Title("String Variable SO")]
    [Category("Variables/String Variable SO")]

    [Image(typeof(IconTextArea), ColorTheme.Type.Blue)]
    [Description("Returns the ScriptableObject string variable")]

    [Serializable]
    public class GetStringStringVariable : PropertyTypeGetString
    {
        [SerializeField]
        private StringVariableSO m_StringVariable;

        public override string Get(Args args) => this.GetStringVariable(args);

        private string GetStringVariable(Args args)
        {
            return m_StringVariable != null ? m_StringVariable.Value : string.Empty;
        }

        public GetStringStringVariable() : base()
        { }

        public static PropertyGetString Create => new PropertyGetString(
            new GetStringStringVariable()
        );

        public override string String => $"{(this.m_StringVariable ? this.m_StringVariable.name : "(none)")}'s String Variable";

        public override string EditorValue => this.m_StringVariable != null
            ? this.m_StringVariable.Value
            : default;
    }
}