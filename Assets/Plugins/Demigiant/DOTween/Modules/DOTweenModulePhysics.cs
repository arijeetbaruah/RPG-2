// Author: Daniele Giardini - http://www.demigiant.com
// Created: 2018/07/13

#if true // MODULE_MARKER
using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using UnityEngine;

#pragma warning disable 1591
namespace DG.Tweening
{
	public static class DOTweenModulePhysics
    {
        #region Shortcuts

        #region Rigidbody

        /// <summary>Tweens a Rigidbody's position to the given value.
        /// Also stores the rigidbody as the tween's target so it can be used for filtered operations</summary>
        /// <param name="endValue">The end value to reach</param><param name="duration">The duration of the tween</param>
        /// <summary>
        /// Tweens the Rigidbody's world position to the specified end value over the given duration.
        /// </summary>
        /// <param name="endValue">Target world-space position to reach.</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <param name="snapping">If true, tweened values will be snapped to integers.</param>
        /// <returns>A TweenerCore that animates the Rigidbody's position and has the Rigidbody set as its target.</returns>
        public static TweenerCore<Vector3, Vector3, VectorOptions> DOMove(this Rigidbody target, Vector3 endValue, float duration, bool snapping = false)
        {
            TweenerCore<Vector3, Vector3, VectorOptions> t = DOTween.To(() => target.position, target.MovePosition, endValue, duration);
            t.SetOptions(snapping).SetTarget(target);
            return t;
        }

        /// <summary>Tweens a Rigidbody's X position to the given value.
        /// Also stores the rigidbody as the tween's target so it can be used for filtered operations</summary>
        /// <param name="endValue">The end value to reach</param><param name="duration">The duration of the tween</param>
        /// <summary>
        /// Tweens a Rigidbody's world-space X position to the specified value over the given duration.
        /// </summary>
        /// <param name="target">The Rigidbody whose position will be tweened.</param>
        /// <param name="endValue">The target X position to reach.</param>
        /// <param name="duration">Time, in seconds, the tween will take to complete.</param>
        /// <param name="snapping">If true, tweened values are rounded to integers each frame.</param>
        /// <returns>A TweenerCore that animates the Rigidbody's position along the X axis.</returns>
        public static TweenerCore<Vector3, Vector3, VectorOptions> DOMoveX(this Rigidbody target, float endValue, float duration, bool snapping = false)
        {
            TweenerCore<Vector3, Vector3, VectorOptions> t = DOTween.To(() => target.position, target.MovePosition, new Vector3(endValue, 0, 0), duration);
            t.SetOptions(AxisConstraint.X, snapping).SetTarget(target);
            return t;
        }

        /// <summary>Tweens a Rigidbody's Y position to the given value.
        /// Also stores the rigidbody as the tween's target so it can be used for filtered operations</summary>
        /// <param name="endValue">The end value to reach</param><param name="duration">The duration of the tween</param>
        /// <summary>
        /// Tweens a Rigidbody's world-space Y position to a specified value over a given duration.
        /// </summary>
        /// <param name="target">The Rigidbody whose Y position will be animated.</param>
        /// <param name="endValue">The target world Y position.</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <param name="snapping">If true, intermediate position values are snapped to integer coordinates.</param>
        /// <returns>The configured tweener that animates the Rigidbody's Y position.</returns>
        public static TweenerCore<Vector3, Vector3, VectorOptions> DOMoveY(this Rigidbody target, float endValue, float duration, bool snapping = false)
        {
            TweenerCore<Vector3, Vector3, VectorOptions> t = DOTween.To(() => target.position, target.MovePosition, new Vector3(0, endValue, 0), duration);
            t.SetOptions(AxisConstraint.Y, snapping).SetTarget(target);
            return t;
        }

        /// <summary>Tweens a Rigidbody's Z position to the given value.
        /// Also stores the rigidbody as the tween's target so it can be used for filtered operations</summary>
        /// <param name="endValue">The end value to reach</param><param name="duration">The duration of the tween</param>
        /// <summary>
        /// Tweens the Rigidbody's world Z position to the specified value.
        /// </summary>
        /// <param name="endValue">Target Z coordinate in world space.</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <param name="snapping">If true, tweened values are rounded to the nearest integer.</param>
        /// <returns>A TweenerCore that animates the Rigidbody's world position Z to <paramref name="endValue"/>.</returns>
        public static TweenerCore<Vector3, Vector3, VectorOptions> DOMoveZ(this Rigidbody target, float endValue, float duration, bool snapping = false)
        {
            TweenerCore<Vector3, Vector3, VectorOptions> t = DOTween.To(() => target.position, target.MovePosition, new Vector3(0, 0, endValue), duration);
            t.SetOptions(AxisConstraint.Z, snapping).SetTarget(target);
            return t;
        }

        /// <summary>Tweens a Rigidbody's rotation to the given value.
        /// Also stores the rigidbody as the tween's target so it can be used for filtered operations</summary>
        /// <param name="endValue">The end value to reach</param><param name="duration">The duration of the tween</param>
        /// <summary>
        /// Tween a Rigidbody's rotation to the specified Euler angles over the given duration.
        /// </summary>
        /// <param name="target">The Rigidbody to rotate.</param>
        /// <param name="endValue">Target rotation as Euler angles (degrees).</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <param name="mode">Rotation mode that controls how the end Euler angles are interpreted.</param>
        /// <returns>A tweener configured to animate the Rigidbody's rotation to the specified Euler angles.</returns>
        public static TweenerCore<Quaternion, Vector3, QuaternionOptions> DORotate(this Rigidbody target, Vector3 endValue, float duration, RotateMode mode = RotateMode.Fast)
        {
            TweenerCore<Quaternion, Vector3, QuaternionOptions> t = DOTween.To(() => target.rotation, target.MoveRotation, endValue, duration);
            t.SetTarget(target);
            t.plugOptions.rotateMode = mode;
            return t;
        }

        /// <summary>Tweens a Rigidbody's rotation so that it will look towards the given position.
        /// Also stores the rigidbody as the tween's target so it can be used for filtered operations</summary>
        /// <param name="towards">The position to look at</param><param name="duration">The duration of the tween</param>
        /// <param name="axisConstraint">Eventual axis constraint for the rotation</param>
        /// <summary>
        /// Tweens a Rigidbody so it rotates to face a world-space point over the given duration.
        /// </summary>
        /// <param name="towards">The world-space point to look at.</param>
        /// <param name="axisConstraint">If set, constrains rotation to the specified axis.</param>
        /// <param name="up">The up direction used when computing the look rotation (defaults to Vector3.up).</param>
        /// <returns>A TweenerCore that animates the Rigidbody's rotation to look at the specified point.</returns>
        public static TweenerCore<Quaternion, Vector3, QuaternionOptions> DOLookAt(this Rigidbody target, Vector3 towards, float duration, AxisConstraint axisConstraint = AxisConstraint.None, Vector3? up = null)
        {
            TweenerCore<Quaternion, Vector3, QuaternionOptions> t = DOTween.To(() => target.rotation, target.MoveRotation, towards, duration)
                .SetTarget(target).SetSpecialStartupMode(SpecialStartupMode.SetLookAt);
            t.plugOptions.axisConstraint = axisConstraint;
            t.plugOptions.up = (up == null) ? Vector3.up : (Vector3)up;
            return t;
        }

        #region Special

        /// <summary>Tweens a Rigidbody's position to the given value, while also applying a jump effect along the Y axis.
        /// Returns a Sequence instead of a Tweener.
        /// Also stores the Rigidbody as the tween's target so it can be used for filtered operations</summary>
        /// <param name="endValue">The end value to reach</param>
        /// <param name="jumpPower">Power of the jump (the max height of the jump is represented by this plus the final Y offset)</param>
        /// <param name="numJumps">Total number of jumps</param>
        /// <param name="duration">The duration of the tween</param>
        /// <summary>
        /// Creates a Sequence that moves the Rigidbody toward a target position while applying one or more jump arcs.
        /// </summary>
        /// <param name="target">The Rigidbody to animate.</param>
        /// <param name="endValue">The world-space target position to reach at the end of the sequence.</param>
        /// <param name="jumpPower">The peak height (relative) of each jump.</param>
        /// <param name="numJumps">The number of jumps to perform; values less than 1 are treated as 1.</param>
        /// <param name="duration">The total duration of the sequence.</param>
        /// <param name="snapping">If true, position values are snapped to integer coordinates during the tween.</param>
        /// <returns>A Sequence that moves the Rigidbody to <paramref name="endValue"/> while performing the configured jumps; the Sequence's target is set to the provided Rigidbody.</returns>
        public static Sequence DOJump(this Rigidbody target, Vector3 endValue, float jumpPower, int numJumps, float duration, bool snapping = false)
        {
            if (numJumps < 1) numJumps = 1;
            float startPosY = 0;
            float offsetY = -1;
            bool offsetYSet = false;
            Sequence s = DOTween.Sequence();
            Tween yTween = DOTween.To(() => target.position, target.MovePosition, new Vector3(0, jumpPower, 0), duration / (numJumps * 2))
                .SetOptions(AxisConstraint.Y, snapping).SetEase(Ease.OutQuad).SetRelative()
                .SetLoops(numJumps * 2, LoopType.Yoyo)
                .OnStart(() => startPosY = target.position.y);
            s.Append(DOTween.To(() => target.position, target.MovePosition, new Vector3(endValue.x, 0, 0), duration)
                    .SetOptions(AxisConstraint.X, snapping).SetEase(Ease.Linear)
                ).Join(DOTween.To(() => target.position, target.MovePosition, new Vector3(0, 0, endValue.z), duration)
                    .SetOptions(AxisConstraint.Z, snapping).SetEase(Ease.Linear)
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

        /// <summary>Tweens a Rigidbody's position through the given path waypoints, using the chosen path algorithm.
        /// Also stores the Rigidbody as the tween's target so it can be used for filtered operations.
        /// <para>NOTE: to tween a rigidbody correctly it should be set to kinematic at least while being tweened.</para>
        /// <para>BEWARE: doesn't work on Windows Phone store (waiting for Unity to fix their own bug).
        /// If you plan to publish there you should use a regular transform.DOPath.</para></summary>
        /// <param name="path">The waypoints to go through</param>
        /// <param name="duration">The duration of the tween</param>
        /// <param name="pathType">The type of path: Linear (straight path), CatmullRom (curved CatmullRom path) or CubicBezier (curved with control points)</param>
        /// <param name="pathMode">The path mode: 3D, side-scroller 2D, top-down 2D</param>
        /// <param name="resolution">The resolution of the path (useless in case of Linear paths): higher resolutions make for more detailed curved paths but are more expensive.
        /// Defaults to 10, but a value of 5 is usually enough if you don't have dramatic long curves between waypoints</param>
        /// <summary>
        /// Tweens a Rigidbody along a world-space path over the given duration.
        /// </summary>
        /// <param name="target">The Rigidbody to move.</param>
        /// <param name="path">Array of world-space waypoints that define the path.</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <param name="pathType">Algorithm used to interpolate the path.</param>
        /// <param name="pathMode">How the path is interpreted with respect to orientation and axes.</param>
        /// <param name="resolution">Number of subdivisions per path segment (minimum 1) used to construct the path.</param>
        /// <param name="gizmoColor">Color used to draw the path when gizmos are shown in the editor play view.</param>
        /// <returns>A configured TweenerCore that moves the Rigidbody along the specified path.</returns>
        public static TweenerCore<Vector3, Path, PathOptions> DOPath(
            this Rigidbody target, Vector3[] path, float duration, PathType pathType = PathType.Linear,
            PathMode pathMode = PathMode.Full3D, int resolution = 10, Color? gizmoColor = null
        )
        {
            if (resolution < 1) resolution = 1;
            TweenerCore<Vector3, Path, PathOptions> t = DOTween.To(PathPlugin.Get(), () => target.position, target.MovePosition, new Path(pathType, path, resolution, gizmoColor), duration)
                .SetTarget(target).SetUpdate(UpdateType.Fixed);

            t.plugOptions.isRigidbody = true;
            t.plugOptions.mode = pathMode;
            return t;
        }
        /// <summary>Tweens a Rigidbody's localPosition through the given path waypoints, using the chosen path algorithm.
        /// Also stores the Rigidbody as the tween's target so it can be used for filtered operations
        /// <para>NOTE: to tween a rigidbody correctly it should be set to kinematic at least while being tweened.</para>
        /// <para>BEWARE: doesn't work on Windows Phone store (waiting for Unity to fix their own bug).
        /// If you plan to publish there you should use a regular transform.DOLocalPath.</para></summary>
        /// <param name="path">The waypoint to go through</param>
        /// <param name="duration">The duration of the tween</param>
        /// <param name="pathType">The type of path: Linear (straight path), CatmullRom (curved CatmullRom path) or CubicBezier (curved with control points)</param>
        /// <param name="pathMode">The path mode: 3D, side-scroller 2D, top-down 2D</param>
        /// <param name="resolution">The resolution of the path: higher resolutions make for more detailed curved paths but are more expensive.
        /// Defaults to 10, but a value of 5 is usually enough if you don't have dramatic long curves between waypoints</param>
        /// <summary>
        /// Tweens a Rigidbody along a path defined in the Rigidbody's local space, animating its localPosition over the given duration.
        /// </summary>
        /// <param name="target">The Rigidbody to animate.</param>
        /// <param name="path">Array of local-space points that define the path.</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <param name="pathType">Algorithm used to interpolate the path.</param>
        /// <param name="pathMode">How the path's orientation/axis is applied.</param>
        /// <param name="resolution">Number of subdivisions per path segment; values less than 1 are treated as 1.</param>
        /// <param name="gizmoColor">Color used to draw the path when gizmos are visible in the Play panel and the tween is running.</param>
        /// <returns>The configured TweenerCore<Vector3, Path, PathOptions> that animates the Rigidbody along the local path. </returns>
        public static TweenerCore<Vector3, Path, PathOptions> DOLocalPath(
            this Rigidbody target, Vector3[] path, float duration, PathType pathType = PathType.Linear,
            PathMode pathMode = PathMode.Full3D, int resolution = 10, Color? gizmoColor = null
        )
        {
            if (resolution < 1) resolution = 1;
            Transform trans = target.transform;
            TweenerCore<Vector3, Path, PathOptions> t = DOTween.To(PathPlugin.Get(), () => trans.localPosition, x => target.MovePosition(trans.parent == null ? x : trans.parent.TransformPoint(x)), new Path(pathType, path, resolution, gizmoColor), duration)
                .SetTarget(target).SetUpdate(UpdateType.Fixed);

            t.plugOptions.isRigidbody = true;
            t.plugOptions.mode = pathMode;
            t.plugOptions.useLocalPosition = true;
            return t;
        }
        /// <summary>
        /// Creates a tweener that moves the Rigidbody along a pre-compiled Path in world space.
        /// </summary>
        /// <param name="target">The Rigidbody to move; the tweener's target will be set to this Rigidbody.</param>
        /// <param name="path">A pre-compiled Path describing the world-space trajectory.</param>
        /// <param name="duration">The duration of the tween in seconds.</param>
        /// <param name="pathMode">Controls how the Rigidbody is oriented and followed along the path.</param>
        /// <returns>
        /// A TweenerCore configured to update the Rigidbody's position using MovePosition; its PathOptions have been marked as a Rigidbody path and the mode set to <paramref name="pathMode"/>.
        —
        internal static TweenerCore<Vector3, Path, PathOptions> DOPath(
            this Rigidbody target, Path path, float duration, PathMode pathMode = PathMode.Full3D
        )
        {
            TweenerCore<Vector3, Path, PathOptions> t = DOTween.To(PathPlugin.Get(), () => target.position, target.MovePosition, path, duration)
                .SetTarget(target);

            t.plugOptions.isRigidbody = true;
            t.plugOptions.mode = pathMode;
            return t;
        }
        /// <summary>
        /// Creates a tweener that moves the Rigidbody along a local-space path over the given duration.
        /// </summary>
        /// <param name="target">The Rigidbody to animate.</param>
        /// <param name="path">The precompiled Path describing the local-space trajectory.</param>
        /// <param name="duration">Time, in seconds, for the full path traversal.</param>
        /// <param name="pathMode">How the tween orients along the path (e.g., Full3D, TopDown2D).</param>
        /// <returns>A configured TweenerCore that animates the Rigidbody following the provided local-space path.</returns>
        internal static TweenerCore<Vector3, Path, PathOptions> DOLocalPath(
            this Rigidbody target, Path path, float duration, PathMode pathMode = PathMode.Full3D
        )
        {
            Transform trans = target.transform;
            TweenerCore<Vector3, Path, PathOptions> t = DOTween.To(PathPlugin.Get(), () => trans.localPosition, x => target.MovePosition(trans.parent == null ? x : trans.parent.TransformPoint(x)), path, duration)
                .SetTarget(target);

            t.plugOptions.isRigidbody = true;
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