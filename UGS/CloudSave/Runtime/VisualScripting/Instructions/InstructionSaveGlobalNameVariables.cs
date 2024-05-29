using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using GameCreator.Runtime.VisualScripting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.CloudSave;
using UnityEngine;

namespace d4160.Runtime.UGS.CloudSave
{
    [Title("Save Name Global Variable")]
    [Description("Saves all variables from a specific Global Name variables asset.")]

    [Category("UGS/Cloud Save/Save Global Name Variables")]

    [Keywords("Cloud", "File", "Multiplayer", "Networking", "Save", "UGS", "Global", "Name", "Variables")]

    [Image(typeof(IconDiskSolid), ColorTheme.Type.Green)]

    [Parameter("NameVariables", "The Global Name Variables asset to save")]
    [Parameter("Split", "Whatever or not split in different CloudSave slots")]
    [Parameter("Key", "If not split, the CloudSave key for all variables to save as")]

    [Serializable]
    public class InstructionSaveGlobalNameVariables : Instruction
    {
        [SerializeField] private GlobalNameVariables m_NameVariables;
        [SerializeField] private bool m_Split = false;
        [SerializeField] private PropertyGetString m_Key = new PropertyGetString();

        public override string Title => $"Save Global Name Variables {m_NameVariables}";

        protected override async Task Run(Args args)
        {
            if (m_NameVariables)
            {
                try
                {
                    string[] names = m_NameVariables.Names;
                    Dictionary<string, object> data = new();

                    for (int i = 0; i < names.Length; i++)
                    {
                        object value = m_NameVariables.Get(names[i]);
                        string valueJson = value.ToString(); //JsonUtility.ToJson();
                        data[names[i]] = valueJson;
                    }
                    await CloudSaveService.Instance.Data.Player.SaveAsync(data);
                }
                catch (Exception exception)
                {
                    Debug.LogException(exception);
                }
            }
        }
    }
}