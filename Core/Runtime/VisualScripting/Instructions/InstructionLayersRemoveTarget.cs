using System;
using System.Threading.Tasks;
using d4160.Runtime.Common;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace Gameframework
{
    [Version(1, 0, 2)]
    [Title("Removes Target Layer")]
    [Description(
        "Plays Audio Source"
    )]
    [Category("Layers/Remove Target Layer")]
    [Parameter(
        "Layer",
        "The layer where the game object belongs to"
    )]
    [Keywords("Layers", "Remove", "Target")]
    [Image(typeof(IconLayers), ColorTheme.Type.Yellow)]
    [Serializable]

    public class InstructionLayersRemove : TInstructionGameObject
    {
        public override string Title => string.Format(
            "Remove Target Layer {0} on {1}",
            this.m_Layer,
            this.m_GameObject
        );

        [SerializeField] private LayerMaskValue m_Layer = new LayerMaskValue();

        protected override Task Run(Args args)
        {
            GameObject gameObject = this.m_GameObject.Get(args);
            if (gameObject == null) return DefaultResult;

            ILayerTarget layerTarget = gameObject.GetComponent<ILayerTarget>();
            if (layerTarget == null) return DefaultResult;

            layerTarget.RemoveTeleportLayer(this.m_Layer.Value);

            return DefaultResult;
        }
    }
}