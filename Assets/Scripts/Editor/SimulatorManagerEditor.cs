/**
 * Copyright (c) 2019 LG Electronics, Inc.
 *
 * This software contains code licensed as described in LICENSE.
 *
 */

using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class SimulatorManagerEditor
{
    static SimulatorManagerEditor()
    {
        EditorApplication.playModeStateChanged += LogPlayModeState;
    }

    private static void LogPlayModeState(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            Scene scene = SceneManager.GetActiveScene();
            if (scene.name != "LoaderScene")
            {
                var simObj = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Managers/SimulatorManager.prefab");
                if (simObj == null)
                {
                    Debug.LogError("Missing SimulatorManager.prefab in Resources folder!");
                    return;
                }
                var sim = Object.Instantiate(simObj).GetComponent<SimulatorManager>();
                sim.name = "SimulatorManager";
                sim.Init();

                sim.AgentManager.SetupDevAgents();
            }
        }
    }
}
