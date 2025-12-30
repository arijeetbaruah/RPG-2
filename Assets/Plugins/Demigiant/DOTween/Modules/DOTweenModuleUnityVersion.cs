// Author: Daniele Giardini - http://www.demigiant.com
// Created: 2018/07/13

using System;
using UnityEngine;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
//#if UNITY_2018_1_OR_NEWER && (NET_4_6 || NET_STANDARD_2_0)
//using Task = System.Threading.Tasks.Task;
//#endif

#pragma warning disable 1591
namespace DG.Tweening
{
    /// <summary>
    /// Shortcuts/functions that are not strictly related to specific Modules
    /// but are available only on some Unity versions
    /// </summary>
	public static class DOTweenModuleUnityVersion
    {
        #region Material

        /// <summary>Tweens a Material's color using the given gradient
        /// (NOTE 1: only uses the colors of the gradient, not the alphas - NOTE 2: creates a Sequence, not a Tweener).
        /// Also stores the image as the tween's target so it can be used for filtered operations</summary>
        /// <summary>
        /// Tweens a Material's color through the provided Gradient over the given duration.
        /// </summary>
        /// <remarks>
        /// If the first gradient key has time <= 0, that color is applied immediately. The returned sequence animates the material through each gradient color key with linear easing and is targeted to the material.
        /// </remarks>
        /// <param name="gradient">Gradient whose color keys define the colors to animate through.</param>
        /// <param name="duration">Total duration of the sequence in seconds.</param>
        /// <returns>A Sequence that animates the material's color through the gradient's color keys.</returns>
        public static Sequence DOGradientColor(this Material target, Gradient gradient, float duration)
        {
            Sequence s = DOTween.Sequence();
            GradientColorKey[] colors = gradient.colorKeys;
            int len = colors.Length;
            for (int i = 0; i < len; ++i) {
                GradientColorKey c = colors[i];
                if (i == 0 && c.time <= 0) {
                    target.color = c.color;
                    continue;
                }
                float colorDuration = i == len - 1
                    ? duration - s.Duration(false) // Verifies that total duration is correct
                    : duration * (i == 0 ? c.time : c.time - colors[i - 1].time);
                s.Append(target.DOColor(c.color, colorDuration).SetEase(Ease.Linear));
            }
            s.SetTarget(target);
            return s;
        }
        /// <summary>Tweens a Material's named color property using the given gradient
        /// (NOTE 1: only uses the colors of the gradient, not the alphas - NOTE 2: creates a Sequence, not a Tweener).
        /// Also stores the image as the tween's target so it can be used for filtered operations</summary>
        /// <param name="gradient">The gradient to use</param>
        /// <param name="property">The name of the material property to tween (like _Tint or _SpecColor)</param>
        /// <summary>
        /// Creates a Sequence that tweens a Material's named color property through the provided Gradient over the given duration.
        /// </summary>
        /// <param name="target">The Material whose color property will be animated.</param>
        /// <param name="gradient">The Gradient whose color keys and times define the animation.</param>
        /// <param name="property">The name of the color property on the material to animate (for example, "_Tint").</param>
        /// <param name="duration">The total duration of the gradient animation.</param>
        /// <returns>The Sequence that performs the color transitions and is targeted to the given material.</returns>
        public static Sequence DOGradientColor(this Material target, Gradient gradient, string property, float duration)
        {
            Sequence s = DOTween.Sequence();
            GradientColorKey[] colors = gradient.colorKeys;
            int len = colors.Length;
            for (int i = 0; i < len; ++i) {
                GradientColorKey c = colors[i];
                if (i == 0 && c.time <= 0) {
                    target.SetColor(property, c.color);
                    continue;
                }
                float colorDuration = i == len - 1
                    ? duration - s.Duration(false) // Verifies that total duration is correct
                    : duration * (i == 0 ? c.time : c.time - colors[i - 1].time);
                s.Append(target.DOColor(c.color, property, colorDuration).SetEase(Ease.Linear));
            }
            s.SetTarget(target);
            return s;
        }

        #endregion

        #region CustomYieldInstructions

        /// <summary>
        /// Returns a <see cref="CustomYieldInstruction"/> that waits until the tween is killed or complete.
        /// It can be used inside a coroutine as a yield.
        /// <para>Example usage:</para><code>yield return myTween.WaitForCompletion(true);</code>
        /// <summary>
        /// Creates a CustomYieldInstruction that waits until the specified tween completes or is killed.
        /// </summary>
        /// <param name="t">The tween to observe.</param>
        /// <returns>The CustomYieldInstruction that keeps waiting while the tween is active and not complete, or <c>null</c> if the tween is not active.</returns>
        public static CustomYieldInstruction WaitForCompletion(this Tween t, bool returnCustomYieldInstruction)
        {
            if (!t.active) {
                if (Debugger.logPriority > 0) Debugger.LogInvalidTween(t);
                return null;
            }
            return new DOTweenCYInstruction.WaitForCompletion(t);
        }

        /// <summary>
        /// Returns a <see cref="CustomYieldInstruction"/> that waits until the tween is killed or rewinded.
        /// It can be used inside a coroutine as a yield.
        /// <para>Example usage:</para><code>yield return myTween.WaitForRewind();</code>
        /// <summary>
        /// Creates a CustomYieldInstruction that yields until the tween is killed or rewinded.
        /// </summary>
        /// <returns>`CustomYieldInstruction` that keeps yielding while the tween is active and has not been rewinded, or `null` if the provided tween is not active.</returns>
        public static CustomYieldInstruction WaitForRewind(this Tween t, bool returnCustomYieldInstruction)
        {
            if (!t.active) {
                if (Debugger.logPriority > 0) Debugger.LogInvalidTween(t);
                return null;
            }
            return new DOTweenCYInstruction.WaitForRewind(t);
        }

        /// <summary>
        /// Returns a <see cref="CustomYieldInstruction"/> that waits until the tween is killed.
        /// It can be used inside a coroutine as a yield.
        /// <para>Example usage:</para><code>yield return myTween.WaitForKill();</code>
        /// <summary>
        /// Provides a CustomYieldInstruction that yields until the specified tween is killed.
        /// </summary>
        /// <param name="t">The tween to observe.</param>
        /// <param name="returnCustomYieldInstruction">Request for a CustomYieldInstruction (unused by this overload but kept for API compatibility).</param>
        /// <returns>
        /// A <see cref="CustomYieldInstruction"/> that keeps yielding until the tween is killed, or `null` if the tween is not active (an invalid-tween message may be logged).
        /// </returns>
        public static CustomYieldInstruction WaitForKill(this Tween t, bool returnCustomYieldInstruction)
        {
            if (!t.active) {
                if (Debugger.logPriority > 0) Debugger.LogInvalidTween(t);
                return null;
            }
            return new DOTweenCYInstruction.WaitForKill(t);
        }

        /// <summary>
        /// Returns a <see cref="CustomYieldInstruction"/> that waits until the tween is killed or has gone through the given amount of loops.
        /// It can be used inside a coroutine as a yield.
        /// <para>Example usage:</para><code>yield return myTween.WaitForElapsedLoops(2);</code>
        /// </summary>
        /// <summary>
        /// Create a CustomYieldInstruction that waits until the tween has completed at least the specified number of loops.
        /// </summary>
        /// <param name="t">The tween to observe.</param>
        /// <param name="elapsedLoops">The target number of elapsed loops to wait for (inclusive).</param>
        /// <param name="returnCustomYieldInstruction">If true, returns a CustomYieldInstruction instance; otherwise callers may handle waiting differently. (Parameter retained for API symmetry.)</param>
        /// <returns>
        /// A CustomYieldInstruction that keeps yielding while the tween is active and has fewer than <paramref name="elapsedLoops"/> elapsed loops,
        /// or <c>null</c> if the provided tween is not active.
        /// </returns>
        public static CustomYieldInstruction WaitForElapsedLoops(this Tween t, int elapsedLoops, bool returnCustomYieldInstruction)
        {
            if (!t.active) {
                if (Debugger.logPriority > 0) Debugger.LogInvalidTween(t);
                return null;
            }
            return new DOTweenCYInstruction.WaitForElapsedLoops(t, elapsedLoops);
        }

        /// <summary>
        /// Returns a <see cref="CustomYieldInstruction"/> that waits until the tween is killed
        /// or has reached the given time position (loops included, delays excluded).
        /// It can be used inside a coroutine as a yield.
        /// <para>Example usage:</para><code>yield return myTween.WaitForPosition(2.5f);</code>
        /// </summary>
        /// <summary>
        /// Creates a CustomYieldInstruction that waits until the tween reaches the specified position (loops included, delays excluded) or is killed.
        /// </summary>
        /// <param name="position">Target position (loops included, delays excluded) at which the yield instruction completes.</param>
        /// <returns>A CustomYieldInstruction that keeps yielding while the tween is active and its position is less than <paramref name="position"/>, or null if the tween is not active.</returns>
        public static CustomYieldInstruction WaitForPosition(this Tween t, float position, bool returnCustomYieldInstruction)
        {
            if (!t.active) {
                if (Debugger.logPriority > 0) Debugger.LogInvalidTween(t);
                return null;
            }
            return new DOTweenCYInstruction.WaitForPosition(t, position);
        }

        /// <summary>
        /// Returns a <see cref="CustomYieldInstruction"/> that waits until the tween is killed or started
        /// (meaning when the tween is set in a playing state the first time, after any eventual delay).
        /// It can be used inside a coroutine as a yield.
        /// <para>Example usage:</para><code>yield return myTween.WaitForStart();</code>
        /// <summary>
        /// Create a CustomYieldInstruction that yields until the specified tween has started.
        /// </summary>
        /// <param name="t">The tween to observe for its start (first play after any delay).</param>
        /// <returns>A CustomYieldInstruction that keeps yielding while the tween has not yet started, or null if the tween is not active.</returns>
        public static CustomYieldInstruction WaitForStart(this Tween t, bool returnCustomYieldInstruction)
        {
            if (!t.active) {
                if (Debugger.logPriority > 0) Debugger.LogInvalidTween(t);
                return null;
            }
            return new DOTweenCYInstruction.WaitForStart(t);
        }

        #endregion

#if UNITY_2018_1_OR_NEWER
        #region Unity 2018.1 or Newer

        #region Material

        /// <summary>Tweens a Material's named texture offset property with the given ID to the given value.
        /// Also stores the material as the tween's target so it can be used for filtered operations</summary>
        /// <param name="endValue">The end value to reach</param>
        /// <param name="propertyID">The ID of the material property to tween (also called nameID in Unity's manual)</param>
        /// <summary>
        /// Tweens a Material's texture offset for the given shader property to the specified end value over the provided duration.
        /// </summary>
        /// <param name="target">The material whose texture offset will be animated.</param>
        /// <param name="endValue">The target texture offset to reach.</param>
        /// <param name="propertyID">Shader property ID of the texture offset to animate (e.g., obtained via Shader.PropertyToID).</param>
        /// <param name="duration">Animation duration in seconds.</param>
        /// <returns>The created tweener targeting the material, or `null` if the material does not contain the specified property.</returns>
        public static TweenerCore<Vector2, Vector2, VectorOptions> DOOffset(this Material target, Vector2 endValue, int propertyID, float duration)
        {
            if (!target.HasProperty(propertyID)) {
                if (Debugger.logPriority > 0) Debugger.LogMissingMaterialProperty(propertyID);
                return null;
            }
            TweenerCore<Vector2, Vector2, VectorOptions> t = DOTween.To(() => target.GetTextureOffset(propertyID), x => target.SetTextureOffset(propertyID, x), endValue, duration);
            t.SetTarget(target);
            return t;
        }

        /// <summary>Tweens a Material's named texture scale property with the given ID to the given value.
        /// Also stores the material as the tween's target so it can be used for filtered operations</summary>
        /// <param name="endValue">The end value to reach</param>
        /// <param name="propertyID">The ID of the material property to tween (also called nameID in Unity's manual)</param>
        /// <summary>
        /// Tweens a Material's texture scale (tiling) for the specified property to the provided end value over the given duration.
        /// </summary>
        /// <param name="target">The material whose texture scale will be tweened.</param>
        /// <param name="endValue">The target texture scale (tiling) to reach.</param>
        /// <param name="propertyID">The shader property ID of the texture scale (tiling) to tween.</param>
        /// <param name="duration">The duration of the tween.</param>
        /// <returns>
        /// A TweenerCore that animates the material's texture scale; the tweener's target is set to the provided material,
        /// or `null` if the material does not have the specified property.
        /// </returns>
        public static TweenerCore<Vector2, Vector2, VectorOptions> DOTiling(this Material target, Vector2 endValue, int propertyID, float duration)
        {
            if (!target.HasProperty(propertyID)) {
                if (Debugger.logPriority > 0) Debugger.LogMissingMaterialProperty(propertyID);
                return null;
            }
            TweenerCore<Vector2, Vector2, VectorOptions> t = DOTween.To(() => target.GetTextureScale(propertyID), x => target.SetTextureScale(propertyID, x), endValue, duration);
            t.SetTarget(target);
            return t;
        }

        #endregion

        #region .NET 4.6 or Newer

#if UNITY_2018_1_OR_NEWER && (NET_4_6 || NET_STANDARD_2_0)

        #region Async Instructions

        /// <summary>
        /// Returns an async <see cref="System.Threading.Tasks.Task"/> that waits until the tween is killed or complete.
        /// It can be used inside an async operation.
        /// <para>Example usage:</para><code>await myTween.WaitForCompletion();</code>
        /// <summary>
        /// Waits until the specified tween completes or is killed.
        /// </summary>
        /// <param name="t">The tween to observe.</param>
        /// <returns>A task that completes when the tween is complete or has been killed.</returns>
        /// <remarks>If the tween is not active, the method logs an invalid-tween message (depending on debugger settings) and returns immediately.</remarks>
        public static async System.Threading.Tasks.Task AsyncWaitForCompletion(this Tween t)
        {
            if (!t.active) {
                if (Debugger.logPriority > 0) Debugger.LogInvalidTween(t);
                return;
            }
            while (t.active && !t.IsComplete()) await System.Threading.Tasks.Task.Yield();
        }

        /// <summary>
        /// Returns an async <see cref="System.Threading.Tasks.Task"/> that waits until the tween is killed or rewinded.
        /// It can be used inside an async operation.
        /// <para>Example usage:</para><code>await myTween.AsyncWaitForRewind();</code>
        /// <summary>
        /// Waits asynchronously until the given tween has rewound after it has played once.
        /// </summary>
        /// <param name="t">The tween to monitor.</param>
        /// <returns>A task that completes when the tween is rewound after its first play, or completes immediately if the tween is not active.</returns>
        public static async System.Threading.Tasks.Task AsyncWaitForRewind(this Tween t)
        {
            if (!t.active) {
                if (Debugger.logPriority > 0) Debugger.LogInvalidTween(t);
                return;
            }
            while (t.active && (!t.playedOnce || t.position * (t.CompletedLoops() + 1) > 0)) await System.Threading.Tasks.Task.Yield();
        }

        /// <summary>
        /// Returns an async <see cref="System.Threading.Tasks.Task"/> that waits until the tween is killed.
        /// It can be used inside an async operation.
        /// <para>Example usage:</para><code>await myTween.AsyncWaitForKill();</code>
        /// <summary>
        /// Waits until the given tween is killed (no longer active).
        /// </summary>
        /// <param name="t">The tween to wait for; if the tween is not active the method returns immediately.</param>
        /// <returns>A task that completes when the tween is killed or immediately if the tween is inactive.</returns>
        public static async System.Threading.Tasks.Task AsyncWaitForKill(this Tween t)
        {
            if (!t.active) {
                if (Debugger.logPriority > 0) Debugger.LogInvalidTween(t);
                return;
            }
            while (t.active) await System.Threading.Tasks.Task.Yield();
        }

        /// <summary>
        /// Returns an async <see cref="System.Threading.Tasks.Task"/> that waits until the tween is killed or has gone through the given amount of loops.
        /// It can be used inside an async operation.
        /// <para>Example usage:</para><code>await myTween.AsyncWaitForElapsedLoops();</code>
        /// </summary>
        /// <summary>
        /// Waits until the given tween has completed at least the specified number of loops or becomes inactive.
        /// </summary>
        /// <param name="elapsedLoops">The number of completed loops to wait for.</param>
        /// <returns>A task that completes when the tween reaches the specified number of completed loops or becomes inactive.</returns>
        public static async System.Threading.Tasks.Task AsyncWaitForElapsedLoops(this Tween t, int elapsedLoops)
        {
            if (!t.active) {
                if (Debugger.logPriority > 0) Debugger.LogInvalidTween(t);
                return;
            }
            while (t.active && t.CompletedLoops() < elapsedLoops) await System.Threading.Tasks.Task.Yield();
        }

        /// <summary>
        /// Returns an async <see cref="System.Threading.Tasks.Task"/> that waits until the tween is killed or started
        /// (meaning when the tween is set in a playing state the first time, after any eventual delay).
        /// It can be used inside an async operation.
        /// <para>Example usage:</para><code>await myTween.AsyncWaitForPosition();</code>
        /// </summary>
        /// <summary>
        /// Waits until the tween reaches the specified position (loops included, delays excluded) or is killed.
        /// </summary>
        /// <param name="position">Target position within the tween; counts loops and excludes delays.</param>
        /// <returns>A task that completes when the tween reaches the given position or is no longer active.</returns>
        public static async System.Threading.Tasks.Task AsyncWaitForPosition(this Tween t, float position)
        {
            if (!t.active) {
                if (Debugger.logPriority > 0) Debugger.LogInvalidTween(t);
                return;
            }
            while (t.active && t.position * (t.CompletedLoops() + 1) < position) await System.Threading.Tasks.Task.Yield();
        }

        /// <summary>
        /// Returns an async <see cref="System.Threading.Tasks.Task"/> that waits until the tween is killed.
        /// It can be used inside an async operation.
        /// <para>Example usage:</para><code>await myTween.AsyncWaitForKill();</code>
        /// <summary>
        /// Waits until the tween has started (its first play) or is no longer active.
        /// </summary>
        /// <returns>A task that completes when the tween has started (played once) or when the tween becomes inactive.</returns>
        public static async System.Threading.Tasks.Task AsyncWaitForStart(this Tween t)
        {
            if (!t.active) {
                if (Debugger.logPriority > 0) Debugger.LogInvalidTween(t);
                return;
            }
            while (t.active && !t.playedOnce) await System.Threading.Tasks.Task.Yield();
        }

        #endregion
#endif

        #endregion

        #endregion
#endif
    }

    // █████████████████████████████████████████████████████████████████████████████████████████████████████████████████████
    // ███ CLASSES █████████████████████████████████████████████████████████████████████████████████████████████████████████
    // █████████████████████████████████████████████████████████████████████████████████████████████████████████████████████

    public static class DOTweenCYInstruction
    {
        public class WaitForCompletion : CustomYieldInstruction
        {
            public override bool keepWaiting { get {
                return t.active && !t.IsComplete();
            }}
            readonly Tween t;
            /// <summary>
            /// Initializes a new WaitForCompletion that continues to keep waiting while the specified tween is active and not complete.
            /// </summary>
            /// <param name="tween">The tween to observe; the instruction keeps waiting until this tween is complete or killed.</param>
            public WaitForCompletion(Tween tween)
            {
                t = tween;
            }
        }

        public class WaitForRewind : CustomYieldInstruction
        {
            public override bool keepWaiting { get {
                return t.active && (!t.playedOnce || t.position * (t.CompletedLoops() + 1) > 0);
            }}
            readonly Tween t;
            /// <summary>
            /// Creates a yield instruction that waits until the provided tween is rewinded or killed.
            /// </summary>
            /// <param name="tween">The Tween to monitor for rewind or kill.</param>
            public WaitForRewind(Tween tween)
            {
                t = tween;
            }
        }

        public class WaitForKill : CustomYieldInstruction
        {
            public override bool keepWaiting { get {
                return t.active;
            }}
            readonly Tween t;
            /// <summary>
            /// Initializes a yield instruction that keeps waiting until the provided tween is killed.
            /// </summary>
            /// <param name="tween">The tween to observe; the instruction remains waiting while this tween is active.</param>
            public WaitForKill(Tween tween)
            {
                t = tween;
            }
        }

        public class WaitForElapsedLoops : CustomYieldInstruction
        {
            public override bool keepWaiting { get {
                return t.active && t.CompletedLoops() < elapsedLoops;
            }}
            readonly Tween t;
            readonly int elapsedLoops;
            /// <summary>
            /// Creates a yield instruction that waits until the specified tween has completed at least the given number of loops.
            /// </summary>
            /// <param name="tween">The tween to monitor.</param>
            /// <param name="elapsedLoops">The number of completed loops to wait for.</param>
            public WaitForElapsedLoops(Tween tween, int elapsedLoops)
            {
                t = tween;
                this.elapsedLoops = elapsedLoops;
            }
        }

        public class WaitForPosition : CustomYieldInstruction
        {
            public override bool keepWaiting { get {
                return t.active && t.position * (t.CompletedLoops() + 1) < position;
            }}
            readonly Tween t;
            readonly float position;
            /// <summary>
            /// Creates a yield instruction that keeps yielding while the specified tween is active and its position is less than the given target.
            /// </summary>
            /// <param name="tween">The tween to monitor.</param>
            /// <param name="position">The target position in the tween's timeline (loops included, delays excluded) at which the instruction stops waiting.</param>
            public WaitForPosition(Tween tween, float position)
            {
                t = tween;
                this.position = position;
            }
        }

        public class WaitForStart : CustomYieldInstruction
        {
            public override bool keepWaiting { get {
                return t.active && !t.playedOnce;
            }}
            readonly Tween t;
            /// <summary>
            /// Creates a yield instruction that waits until the specified tween starts (has its first play) or is killed.
            /// </summary>
            /// <param name="tween">The tween to observe; the instruction will continue while the tween is active and has not yet started.</param>
            public WaitForStart(Tween tween)
            {
                t = tween;
            }
        }
    }
}