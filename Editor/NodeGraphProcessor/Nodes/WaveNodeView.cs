﻿using System.Collections.Generic;
using d4160.Core.Editors.Utilities;
using UnityEngine.UIElements;
using GraphProcessor;
using d4160.NodeGraphProcessor;

namespace d4160.Editor.NodeGraphProcessor
{
    [NodeCustomEditor(typeof(WaveNode))]
    public class WaveNodeView : BaseNodeView
    {
        private VisualElement _libraryVisualElement;
        private List<RangeField> _chances = new List<RangeField>();

        public override void Enable()
        {
            var node = nodeTarget as WaveNode;
            _libraryVisualElement = new VisualElement();

            IStyle mainStyle = mainContainer.style;
            mainStyle.width = 315;

            var spawnsNumberField = UIElementsUtility.MinMaxIntField("Spawns Number", ref node.spawnsNumber, -1, 50,
                (v) => node.spawnsNumber = v, (v) => node.spawnsNumber.x = v,
                (v) => node.spawnsNumber.y = v);

            var timeBetweenSpawnsField = UIElementsUtility.MinMaxFloatField("Time Between", ref node.timeBetweenSpawns, 1f, 10f,
                (v) => node.timeBetweenSpawns = v, (v) => node.timeBetweenSpawns.x = v,
                (v) => node.timeBetweenSpawns.y = v);

            var instancesInEachSpawnField = UIElementsUtility.MinMaxIntField("Instances Number", ref node.instancesInEachSpawn, -1, 10,
                (v) => node.instancesInEachSpawn = v, (v) => node.instancesInEachSpawn.x = v,
                (v) => node.instancesInEachSpawn.y = v);

            controlsContainer.Add(spawnsNumberField);
            controlsContainer.Add(timeBetweenSpawnsField);
            controlsContainer.Add(instancesInEachSpawnField);

            // var libraryField = UIElementsUtility.ObjectField(node.spawnLibrary, "Spawn Library",
            //     (v) =>
            //     {
            //         node.spawnLibrary = v;
            //         if (v != null) AddLibraryVisualElements(node); else RemoveLibraryVisualElements();
            //     });

            //controlsContainer.Add(libraryField);

            AddLibraryVisualElements(node);
        }

        private void AddLibraryVisualElements(WaveNode node)
        {
            // if (node.spawnLibrary)
            // {
            //     _libraryVisualElement.Clear();
            //     node.libraryChances.ClearWeights();

            //     for (int i = 0; i < node.spawnLibrary.Items.Length; i++)
            //     {
            //         node.libraryChances.AddOrUpdateWeight(node.spawnLibrary.Items[i], .5f);
            //         var index = i;
            //         var weightField = new RangeField($"Library Object {index + 1}", ref node.libraryChances.Items[index]._weight, 0f, 1f,
            //             (v) => node.libraryChances.Items[index].Weight = v, (v) => node.libraryChances.Items[index].Weight = v);
            //         _libraryVisualElement.Add(weightField);
            //         _chances.Add(weightField);
            //     }

            //     var button = UIElementsUtility.Button("Automatic Chances", () => AdjustWeights(node));
            //     _libraryVisualElement.Add(button);

            //     if (!controlsContainer.Contains(_libraryVisualElement))
            //         controlsContainer.Add(_libraryVisualElement);
            // }
            // else
            // {
            //     RemoveLibraryVisualElements();
            // }
        }

        private void AdjustWeights(WaveNode node)
        {
            // node.libraryChances.FixRandomizerWeights();
            // for (int i = 0; i < _chances.Count; i++)
            // {
            //     _chances[i].UpdateValue(node.libraryChances.Items[i].Weight);
            // }
        }

        private void RemoveLibraryVisualElements()
        {
            if (controlsContainer.Contains(_libraryVisualElement))
                controlsContainer.Remove(_libraryVisualElement);
        }
    }
}