// Author: Daniele Giardini - http://www.demigiant.com
// Created: 2018/07/13

using System;
using System.Reflection;
using UnityEngine;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;

#pragma warning disable 1591
namespace DG.Tweening
{
    /// <summary>
    /// Utility functions that deal with available Modules.
    /// Modules defines:
    /// - DOTAUDIO
    /// - DOTPHYSICS
    /// - DOTPHYSICS2D
    /// - DOTSPRITE
    /// - DOTUI
    /// Extra defines set and used for implementation of external assets:
    /// - DOTWEEN_TMP ► TextMesh Pro
    /// - DOTWEEN_TK2D ► 2D Toolkit
    /// </summary>
	public static class DOTweenModuleUtils
    {
        static bool _initialized;

        #region Reflection

        /// <summary>
        /// Called via Reflection by DOTweenComponent on Awake
        /// </summary>
#if UNITY_2018_1_OR_NEWER
        /// <summary>
        /// Performs one-time DOTween module initialization and registers engine integration callbacks.
        /// </summary>
        /// <remarks>
        /// Sets an internal initialized flag, wires DOTweenExternalCommand's orientation-on-path handler to the Physics implementation,
        /// and (in the Unity Editor) subscribes a playmode state change handler to propagate pause state to DOTween.
        /// Calling this method multiple times has no effect after the first call.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
#endif
        public static void Init()
        {
            if (_initialized) return;

            _initialized = true;
            DOTweenExternalCommand.SetOrientationOnPath += Physics.SetOrientationOnPath;

#if UNITY_EDITOR
#if UNITY_4_3 || UNITY_4_4 || UNITY_4_5 || UNITY_4_6 || UNITY_5 || UNITY_2017_1
            UnityEditor.EditorApplication.playmodeStateChanged += PlaymodeStateChanged;
#else
            UnityEditor.EditorApplication.playModeStateChanged += PlaymodeStateChanged;
#endif
#endif
        }

#if UNITY_2018_1_OR_NEWER
#pragma warning disable
        /// <summary>
        /// Keeps referenced types and methods from being stripped by Unity's code-stripping/linker during build.
        /// </summary>
        /// <remarks>
        /// Exists solely to create explicit references so the build pipeline preserves required methods and assemblies; it is not intended to be invoked at runtime.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        // Just used to preserve methods when building, never called
        static void Preserver()
        {
            Assembly[] loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            MethodInfo mi = typeof(MonoBehaviour).GetMethod("Stub");
        }
#pragma warning restore
#endif

        #endregion

#if UNITY_EDITOR
        // Fires OnApplicationPause in DOTweenComponent even when Editor is paused (otherwise it's only fired at runtime)
#if UNITY_4_3 || UNITY_4_4 || UNITY_4_5 || UNITY_4_6 || UNITY_5 || UNITY_2017_1
        /// <summary>
/// Forwards the Unity Editor playmode pause state to DOTween so the tween engine updates when the editor is paused or resumed.
/// </summary>
/// <remarks>
/// Invoked when the editor's playmode/pause state changes to propagate the paused state to the DOTween instance.
/// </remarks>
static void PlaymodeStateChanged()
        #else
        /// <summary>
        /// Propagates the Unity Editor playmode pause state to DOTween when playmode changes.
        /// </summary>
        /// <param name="state">The play mode state reported by the Unity Editor.</param>
        static void PlaymodeStateChanged(UnityEditor.PlayModeStateChange state)
#endif
        {
            if (DOTween.instance == null) return;
            DOTween.instance.OnApplicationPause(UnityEditor.EditorApplication.isPaused);
        }
#endif

        // █████████████████████████████████████████████████████████████████████████████████████████████████████████████████████
        // ███ INTERNAL CLASSES ████████████████████████████████████████████████████████████████████████████████████████████████
        // █████████████████████████████████████████████████████████████████████████████████████████████████████████████████████

        public static class Physics
        {
            /// <summary>
            /// Applies the given rotation to the path tween's target, using the Rigidbody when the tween targets a rigidbody and the Transform otherwise.
            /// </summary>
            /// <param name="options">Path options indicating whether the tween should affect a Rigidbody (options.isRigidbody).</param>
            /// <param name="t">The active tween whose target will receive the rotation; expected target is a Rigidbody when options.isRigidbody is true.</param>
            /// <param name="newRot">The rotation to apply to the target.</param>
            /// <param name="trans">The Transform to update when the tween target is not a Rigidbody.</param>
            public static void SetOrientationOnPath(PathOptions options, Tween t, Quaternion newRot, Transform trans)
            {
#if true // PHYSICS_MARKER
                if (options.isRigidbody) ((Rigidbody)t.target).rotation = newRot;
                else trans.rotation = newRot;
#else
                trans.rotation = newRot;
#endif
            }

            /// <summary>
            /// Determines whether the specified component's GameObject has a Rigidbody2D and the Physics2D module is available.
            /// </summary>
            /// <param name="target">The component whose GameObject will be checked for a Rigidbody2D.</param>
            /// <returns>`true` if a Rigidbody2D is present and the Physics2D module is enabled, `false` otherwise.</returns>
            public static bool HasRigidbody2D(Component target)
            {
#if true // PHYSICS2D_MARKER
                return target.GetComponent<Rigidbody2D>() != null;
#else
                return false;
#endif
            }

            #region Called via Reflection


            // Called via Reflection by DOTweenPathInspector
            // Returns FALSE if the DOTween's Physics Module is disabled, or if there's no rigidbody attached
#if UNITY_2018_1_OR_NEWER
            /// <summary>
            /// Checks whether the given component's GameObject has a Rigidbody attached.
            /// </summary>
            /// <param name="target">The component whose GameObject will be inspected.</param>
            /// <returns>`true` if a Rigidbody is attached to the component's GameObject, `false` otherwise.</returns>
            [UnityEngine.Scripting.Preserve]
#endif
            public static bool HasRigidbody(Component target)
            {
#if true // PHYSICS_MARKER
                return target.GetComponent<Rigidbody>() != null;
#else
                return false;
#endif
            }

            // Called via Reflection by DOTweenPath
#if UNITY_2018_1_OR_NEWER
            /// <summary>
            /// Creates a path tween for the given MonoBehaviour, preferring to tween a Rigidbody or Rigidbody2D when requested and available.
            /// </summary>
            /// <param name="target">The MonoBehaviour whose Rigidbody, Rigidbody2D, or Transform will be animated along the path.</param>
            /// <param name="tweenRigidbody">If true, attempt to create the tween on a Rigidbody or Rigidbody2D component if present; otherwise use the Transform.</param>
            /// <param name="isLocal">If true, create a local-space path tween; otherwise create a world-space path tween.</param>
            /// <param name="path">The Path defining the trajectory to follow.</param>
            /// <param name="duration">Duration of the tween in seconds.</param>
            /// <param name="pathMode">The PathMode that controls orientation/alignment along the path.</param>
            /// <returns>A TweenerCore&lt;Vector3, Path, PathOptions&gt; that animates the target along the specified path, targeting a Rigidbody/Rigidbody2D when applicable or the target's Transform otherwise.</returns>
            [UnityEngine.Scripting.Preserve]
#endif
            public static TweenerCore<Vector3, Path, PathOptions> CreateDOTweenPathTween(
                MonoBehaviour target, bool tweenRigidbody, bool isLocal, Path path, float duration, PathMode pathMode
            ){
                TweenerCore<Vector3, Path, PathOptions> t = null;
                bool rBodyFoundAndTweened = false;
#if true // PHYSICS_MARKER
                if (tweenRigidbody) {
                    Rigidbody rBody = target.GetComponent<Rigidbody>();
                    if (rBody != null) {
                        rBodyFoundAndTweened = true;
                        t = isLocal
                            ? rBody.DOLocalPath(path, duration, pathMode)
                            : rBody.DOPath(path, duration, pathMode);
                    }
                }
#endif
#if true // PHYSICS2D_MARKER
                if (!rBodyFoundAndTweened && tweenRigidbody) {
                    Rigidbody2D rBody2D = target.GetComponent<Rigidbody2D>();
                    if (rBody2D != null) {
                        rBodyFoundAndTweened = true;
                        t = isLocal
                            ? rBody2D.DOLocalPath(path, duration, pathMode)
                            : rBody2D.DOPath(path, duration, pathMode);
                    }
                }
#endif
                if (!rBodyFoundAndTweened) {
                    t = isLocal
                        ? target.transform.DOLocalPath(path, duration, pathMode)
                        : target.transform.DOPath(path, duration, pathMode);
                }
                return t;
            }

            #endregion
        }
    }
}