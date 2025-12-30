// Author: Daniele Giardini - http://www.demigiant.com
// Created: 2018/07/13

#if true // MODULE_MARKER
using System;
using DG.Tweening.Core;
using DG.Tweening.Plugins;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using UnityEngine;

#pragma warning disable 1591
namespace DG.Tweening
{
	public static class DOTweenModulePhysics2D
    {
        #region Shortcuts

        #region Rigidbody2D Shortcuts

        /// <summary>Tweens a Rigidbody2D's position to the given value.
        /// Also stores the Rigidbody2D as the tween's target so it can be used for filtered operations</summary>
        /// <param name="endValue">The end value to reach</param><param name="duration">The duration of the tween</param>
        /// <summary>
        /// Tweens the Rigidbody2D's world position to the specified end value over the given duration.
        /// </summary>
        /// <param name="snapping">If true, each tweened value will be rounded to the nearest integer during the animation.</param>
        /// <returns>The configured TweenerCore that animates the Rigidbody2D's position to the specified end value.</returns>
        public static TweenerCore<Vector2, Vector2, VectorOptions> DOMove(this Rigidbody2D target, Vector2 endValue, float duration, bool snapping = false)
        {
            TweenerCore<Vector2, Vector2, VectorOptions> t = DOTween.To(() => target.position, target.MovePosition, endValue, duration);
            t.SetOptions(snapping).SetTarget(target);
            return t;
        }

        /// <summary>Tweens a Rigidbody2D's X position to the given value.
        /// Also stores the Rigidbody2D as the tween's target so it can be used for filtered operations</summary>
        /// <param name="endValue">The end value to reach</param><param name="duration">The duration of the tween</param>
        /// <summary>
        /// Tweens the Rigidbody2D's X position to the specified value over the given duration.
        /// </summary>
        /// <param name="target">The Rigidbody2D to animate.</param>
        /// <param name="endValue">The target X coordinate.</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <param name="snapping">If true, tweened values will be rounded to whole integers during the animation.</param>
        /// <returns>A configured tweener that animates the Rigidbody2D's position on the X axis.</returns>
        public static TweenerCore<Vector2, Vector2, VectorOptions> DOMoveX(this Rigidbody2D target, float endValue, float duration, bool snapping = false)
        {
            TweenerCore<Vector2, Vector2, VectorOptions> t = DOTween.To(() => target.position, target.MovePosition, new Vector2(endValue, 0), duration);
            t.SetOptions(AxisConstraint.X, snapping).SetTarget(target);
            return t;
        }

        /// <summary>Tweens a Rigidbody2D's Y position to the given value.
        /// Also stores the Rigidbody2D as the tween's target so it can be used for filtered operations</summary>
        /// <param name="endValue">The end value to reach</param><param name="duration">The duration of the tween</param>
        /// <summary>
        /// Tweens a Rigidbody2D's world Y position to the specified value over the given duration.
        /// </summary>
        /// <param name="endValue">The target world Y position.</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <param name="snapping">If true, tweened values will be rounded to integer values when applied.</param>
        /// <returns>A configured TweenerCore that animates the Rigidbody2D's position along the Y axis.</returns>
        public static TweenerCore<Vector2, Vector2, VectorOptions> DOMoveY(this Rigidbody2D target, float endValue, float duration, bool snapping = false)
        {
            TweenerCore<Vector2, Vector2, VectorOptions> t = DOTween.To(() => target.position, target.MovePosition, new Vector2(0, endValue), duration);
            t.SetOptions(AxisConstraint.Y, snapping).SetTarget(target);
            return t;
        }

        /// <summary>Tweens a Rigidbody2D's rotation to the given value.
        /// Also stores the Rigidbody2D as the tween's target so it can be used for filtered operations</summary>
        /// <summary>
        /// Tweens the Rigidbody2D's rotation to the specified angle.
        /// </summary>
        /// <param name="target">The Rigidbody2D whose rotation will be animated.</param>
        /// <param name="endValue">Target rotation angle in degrees.</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <returns>A TweenerCore that animates the Rigidbody2D's rotation to <paramref name="endValue"/>.</returns>
        public static TweenerCore<float, float, FloatOptions> DORotate(this Rigidbody2D target, float endValue, float duration)
        {
            TweenerCore<float, float, FloatOptions> t = DOTween.To(() => target.rotation, target.MoveRotation, endValue, duration);
            t.SetTarget(target);
            return t;
        }

        #region Special

        /// <summary>Tweens a Rigidbody2D's position to the given value, while also applying a jump effect along the Y axis.
        /// Returns a Sequence instead of a Tweener.
        /// Also stores the Rigidbody2D as the tween's target so it can be used for filtered operations.
        /// <para>IMPORTANT: a rigidbody2D can't be animated in a jump arc using MovePosition, so the tween will directly set the position</para></summary>
        /// <param name="endValue">The end value to reach</param>
        /// <param name="jumpPower">Power of the jump (the max height of the jump is represented by this plus the final Y offset)</param>
        /// <param name="numJumps">Total number of jumps</param>
        /// <param name="duration">The duration of the tween</param>
        /// <summary>
        /// Creates a jump-style motion for the Rigidbody2D that moves it horizontally to the specified end position while performing a number of vertical jumps.
        /// </summary>
        /// <param name="target">The Rigidbody2D to animate.</param>
        /// <param name="endValue">The final world position to reach; X is used for horizontal movement and Y for the final vertical offset.</param>
        /// <param name="jumpPower">The peak vertical displacement of each jump.</param>
        /// <param name="numJumps">The number of jumps to perform; values less than 1 are treated as 1.</param>
        /// <param name="duration">Total duration of the whole jump sequence.</param>
        /// <param name="snapping">If true, interpolated values will be snapped to integer values.</param>
        /// <returns>A Sequence that performs the combined horizontal move and vertical jump(s) on the Rigidbody2D.</returns>
        public static Sequence DOJump(this Rigidbody2D target, Vector2 endValue, float jumpPower, int numJumps, float duration, bool snapping = false)
        {
            if (numJumps < 1) numJumps = 1;
            float startPosY = 0;
            float offsetY = -1;
            bool offsetYSet = false;
            Sequence s = DOTween.Sequence();
            Tween yTween = DOTween.To(() => target.position, x => target.position = x, new Vector2(0, jumpPower), duration / (numJumps * 2))
                .SetOptions(AxisConstraint.Y, snapping).SetEase(Ease.OutQuad).SetRelative()
                .SetLoops(numJumps * 2, LoopType.Yoyo)
                .OnStart(() => startPosY = target.position.y);
            s.Append(DOTween.To(() => target.position, x => target.position = x, new Vector2(endValue.x, 0), duration)
                    .SetOptions(AxisConstraint.X, snapping).SetEase(Ease.Linear)
                ).Join(yTween)
                .SetTarget(target).SetEase(DOTween.defaultEaseType);
            yTween.OnUpdate(() => {
                if (!offsetYSet) {
                    offsetYSet = true;
                    offsetY = s.isRelative ? endValue.y : endValue.y - startPosY;
                }
                Vector3 pos = target.position;
                pos.y += DOVirtual.EasedValue(0, offsetY, yTween.ElapsedPercentage(), Ease.OutQuad);
                target.MovePosition(pos);
            });
            return s;
        }

        /// <summary>Tweens a Rigidbody2D's position through the given path waypoints, using the chosen path algorithm.
        /// Also stores the Rigidbody2D as the tween's target so it can be used for filtered operations.
        /// <para>NOTE: to tween a Rigidbody2D correctly it should be set to kinematic at least while being tweened.</para>
        /// <para>BEWARE: doesn't work on Windows Phone store (waiting for Unity to fix their own bug).
        /// If you plan to publish there you should use a regular transform.DOPath.</para></summary>
        /// <param name="path">The waypoints to go through</param>
        /// <param name="duration">The duration of the tween</param>
        /// <param name="pathType">The type of path: Linear (straight path), CatmullRom (curved CatmullRom path) or CubicBezier (curved with control points)</param>
        /// <param name="pathMode">The path mode: 3D, side-scroller 2D, top-down 2D</param>
        /// <param name="resolution">The resolution of the path (useless in case of Linear paths): higher resolutions make for more detailed curved paths but are more expensive.
        /// Defaults to 10, but a value of 5 is usually enough if you don't have dramatic long curves between waypoints</param>
        /// <summary>
        /// Animates a Rigidbody2D along a world-space path defined by the provided Vector2 waypoints.
        /// The returned tween is configured to update in FixedUpdate and to operate on the Rigidbody2D.
        /// </summary>
        /// <param name="path">An array of world-space waypoints that define the path.</param>
        /// <param name="duration">Total time, in seconds, the tween will take to complete.</param>
        /// <param name="pathType">The algorithm used to interpolate between waypoints.</param>
        /// <param name="pathMode">How the Rigidbody2D's orientation is applied while following the path.</param>
        /// <param name="resolution">Number of subdivisions per segment used to build the path (minimum 1).</param>
        /// <param name="gizmoColor">Optional color used to draw the path gizmo in the Play panel when the tween is running.</param>
        /// <returns>A TweenerCore that moves the Rigidbody2D's position along the constructed Path.</returns>
        public static TweenerCore<Vector3, Path, PathOptions> DOPath(
            this Rigidbody2D target, Vector2[] path, float duration, PathType pathType = PathType.Linear,
            PathMode pathMode = PathMode.Full3D, int resolution = 10, Color? gizmoColor = null
        )
        {
            if (resolution < 1) resolution = 1;
            int len = path.Length;
            Vector3[] path3D = new Vector3[len];
            for (int i = 0; i < len; ++i) path3D[i] = path[i];
            TweenerCore<Vector3, Path, PathOptions> t = DOTween.To(PathPlugin.Get(), () => target.position, x => target.MovePosition(x), new Path(pathType, path3D, resolution, gizmoColor), duration)
                .SetTarget(target).SetUpdate(UpdateType.Fixed);

            t.plugOptions.isRigidbody2D = true;
            t.plugOptions.mode = pathMode;
            return t;
        }
        /// <summary>Tweens a Rigidbody2D's localPosition through the given path waypoints, using the chosen path algorithm.
        /// Also stores the Rigidbody2D as the tween's target so it can be used for filtered operations
        /// <para>NOTE: to tween a Rigidbody2D correctly it should be set to kinematic at least while being tweened.</para>
        /// <para>BEWARE: doesn't work on Windows Phone store (waiting for Unity to fix their own bug).
        /// If you plan to publish there you should use a regular transform.DOLocalPath.</para></summary>
        /// <param name="path">The waypoint to go through</param>
        /// <param name="duration">The duration of the tween</param>
        /// <param name="pathType">The type of path: Linear (straight path), CatmullRom (curved CatmullRom path) or CubicBezier (curved with control points)</param>
        /// <param name="pathMode">The path mode: 3D, side-scroller 2D, top-down 2D</param>
        /// <param name="resolution">The resolution of the path: higher resolutions make for more detailed curved paths but are more expensive.
        /// Defaults to 10, but a value of 5 is usually enough if you don't have dramatic long curves between waypoints</param>
        /// <summary>
        /// Tweens a Rigidbody2D along a sequence of local-space waypoints over the given duration.
        /// </summary>
        /// <param name="path">Array of local-space waypoints (Vector2) defining the path.</param>
        /// <param name="duration">Total time, in seconds, for the tween to complete.</param>
        /// <param name="pathType">Algorithm used to interpolate between waypoints.</param>
        /// <param name="pathMode">How the path is applied (2D/3D/full), affecting orientation and calculations.</param>
        /// <param name="resolution">Number of subdivisions per segment for path smoothing; values less than 1 are treated as 1.</param>
        /// <param name="gizmoColor">Optional color used to draw the path gizmo when gizmos are visible during playback.</param>
        /// <returns>A tweener configured to move the Rigidbody2D along the specified local-space path, updating on FixedUpdate and using Rigidbody2D-safe positioning.</returns>
        public static TweenerCore<Vector3, Path, PathOptions> DOLocalPath(
            this Rigidbody2D target, Vector2[] path, float duration, PathType pathType = PathType.Linear,
            PathMode pathMode = PathMode.Full3D, int resolution = 10, Color? gizmoColor = null
        )
        {
            if (resolution < 1) resolution = 1;
            int len = path.Length;
            Vector3[] path3D = new Vector3[len];
            for (int i = 0; i < len; ++i) path3D[i] = path[i];
            Transform trans = target.transform;
            TweenerCore<Vector3, Path, PathOptions> t = DOTween.To(PathPlugin.Get(), () => trans.localPosition, x => target.MovePosition(trans.parent == null ? x : trans.parent.TransformPoint(x)), new Path(pathType, path3D, resolution, gizmoColor), duration)
                .SetTarget(target).SetUpdate(UpdateType.Fixed);

            t.plugOptions.isRigidbody2D = true;
            t.plugOptions.mode = pathMode;
            t.plugOptions.useLocalPosition = true;
            return t;
        }
        /// <summary>
        /// Creates a tween that moves the given Rigidbody2D along a precompiled Path.
        /// </summary>
        /// <param name="path">The precompiled path describing the waypoint positions to follow.</param>
        /// <param name="duration">How long, in seconds, the tween takes to complete.</param>
        /// <param name="pathMode">Specifies how the path is interpreted (position-only, top-down 2D, full 3D, etc.).</param>
        /// <returns>The configured TweenerCore that moves the Rigidbody2D along the provided Path and is set up for Rigidbody2D motion.</returns>
        internal static TweenerCore<Vector3, Path, PathOptions> DOPath(
            this Rigidbody2D target, Path path, float duration, PathMode pathMode = PathMode.Full3D
        )
        {
            TweenerCore<Vector3, Path, PathOptions> t = DOTween.To(PathPlugin.Get(), () => target.position, x => target.MovePosition(x), path, duration)
                .SetTarget(target);

            t.plugOptions.isRigidbody2D = true;
            t.plugOptions.mode = pathMode;
            return t;
        }
        /// <summary>
        /// Creates a tween that moves the Rigidbody2D along a precompiled path expressed in the transform's local space.
        /// </summary>
        /// <param name="target">The Rigidbody2D to animate.</param>
        /// <param name="path">A precompiled Path whose waypoints are interpreted in the target's local coordinate space.</param>
        /// <param name="duration">The duration of the tween in seconds.</param>
        /// <param name="pathMode">Specifies how the tween orients along the path.</param>
        /// <returns>A tweener configured to move the Rigidbody2D along the provided local-space path.</returns>
        internal static TweenerCore<Vector3, Path, PathOptions> DOLocalPath(
            this Rigidbody2D target, Path path, float duration, PathMode pathMode = PathMode.Full3D
        )
        {
            Transform trans = target.transform;
            TweenerCore<Vector3, Path, PathOptions> t = DOTween.To(PathPlugin.Get(), () => trans.localPosition, x => target.MovePosition(trans.parent == null ? x : trans.parent.TransformPoint(x)), path, duration)
                .SetTarget(target);

            t.plugOptions.isRigidbody2D = true;
            t.plugOptions.mode = pathMode;
            t.plugOptions.useLocalPosition = true;
            return t;
        }

        #endregion

        #endregion

        #endregion
	}
}
#endif