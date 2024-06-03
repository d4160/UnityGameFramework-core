using System;
using System.Threading.Tasks;
using d4160.SceneManagement;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace Gameframework
{
    [Version(1, 1, 0)]

    [Title("Load SceneCollection")]
    [Description(
        "Loads a scene collection"
    )]
    [Category("Scenes/Load SceneCollection")]

    [Parameter("SceneCollection", "The SceneCollection object to load")]

    [Keywords("Load", "Scene", "Collection")]
    [Image(typeof(IconUnity), ColorTheme.Type.TextNormal)]

    [Serializable]
    public class InstructionScenesLoadCollection : Instruction
    {
        [SerializeField] private SceneCollectionSO m_SceneCollection;

        public override string Title => string.Format(
            "Load scene collection {0}",
            this.m_SceneCollection
        );

        protected override Task Run(Args args)
        {
            if (m_SceneCollection == null) return DefaultResult;

            m_SceneCollection.LoadScenesAsync();

            return DefaultResult;
        }
    }
}