// Author: Daniele Giardini - http://www.demigiant.com
// Created: 2018/07/13

#if true // MODULE_MARKER

using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening.Core;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins;
using DG.Tweening.Plugins.Options;
using Outline = UnityEngine.UI.Outline;
using Text = UnityEngine.UI.Text;

#pragma warning disable 1591
namespace DG.Tweening
{
	public static class DOTweenModuleUI
    {
        #region Shortcuts

        #region CanvasGroup

        /// <summary>Tweens a CanvasGroup's alpha color to the given value.
        /// Also stores the canvasGroup as the tween's target so it can be used for filtered operations</summary>
        /// <summary>
        /// Tweens the CanvasGroup's alpha to the specified value.
        /// </summary>
        /// <param name="target">The CanvasGroup whose alpha will be animated.</param>
        /// <param name="endValue">The target alpha value (typically between 0 and 1).</param>
        /// <param name="duration">How long the tween will take, in seconds.</param>
        /// <returns>A TweenerCore that animates the alpha, with its target set to the provided CanvasGroup.</returns>
        public static TweenerCore<float, float, FloatOptions> DOFade(this CanvasGroup target, float endValue, float duration)
        {
            TweenerCore<float, float, FloatOptions> t = DOTween.To(() => target.alpha, x => target.alpha = x, endValue, duration);
            t.SetTarget(target);
            return t;
        }

        #endregion

        #region Graphic

        /// <summary>Tweens an Graphic's color to the given value.
        /// Also stores the image as the tween's target so it can be used for filtered operations</summary>
        /// <summary>
        /// Tweens the specified Graphic's color to the given end value over the given duration.
        /// </summary>
        /// <param name="target">The Graphic whose color will be animated.</param>
        /// <param name="endValue">The color to reach at the end of the tween.</param>
        /// <param name="duration">The duration of the tween in seconds.</param>
        /// <returns>A TweenerCore that animates the Graphic's color to the specified end value.</returns>
        public static TweenerCore<Color, Color, ColorOptions> DOColor(this Graphic target, Color endValue, float duration)
        {
            TweenerCore<Color, Color, ColorOptions> t = DOTween.To(() => target.color, x => target.color = x, endValue, duration);
            t.SetTarget(target);
            return t;
        }

        /// <summary>Tweens an Graphic's alpha color to the given value.
        /// Also stores the image as the tween's target so it can be used for filtered operations</summary>
        /// <summary>
        /// Animates the alpha channel of a Graphic's color to the specified value.
        /// </summary>
        /// <param name="endValue">Target alpha value (0 = fully transparent, 1 = fully opaque).</param>
        /// <param name="duration">Duration of the animation in seconds.</param>
        /// <returns>A tweener that animates the Graphic's color alpha to <paramref name="endValue"/>.</returns>
        public static TweenerCore<Color, Color, ColorOptions> DOFade(this Graphic target, float endValue, float duration)
        {
            TweenerCore<Color, Color, ColorOptions> t = DOTween.ToAlpha(() => target.color, x => target.color = x, endValue, duration);
            t.SetTarget(target);
            return t;
        }

        #endregion

        #region Image

        /// <summary>Tweens an Image's color to the given value.
        /// Also stores the image as the tween's target so it can be used for filtered operations</summary>
        /// <summary>
        /// Animates the Image's color to the specified value over the given duration.
        /// </summary>
        /// <param name="endValue">Target color to reach.</param>
        /// <param name="duration">Time in seconds the animation will take.</param>
        /// <returns>The tween that animates the Image's color.</returns>
        public static TweenerCore<Color, Color, ColorOptions> DOColor(this Image target, Color endValue, float duration)
        {
            TweenerCore<Color, Color, ColorOptions> t = DOTween.To(() => target.color, x => target.color = x, endValue, duration);
            t.SetTarget(target);
            return t;
        }

        /// <summary>Tweens an Image's alpha color to the given value.
        /// Also stores the image as the tween's target so it can be used for filtered operations</summary>
        /// <summary>
        /// Tweens the Image's alpha channel to the specified value.
        /// </summary>
        /// <param name="target">The Image whose alpha will be animated.</param>
        /// <param name="endValue">Target alpha value (0 to 1).</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <returns>A tweener that animates the Image's color alpha to the specified value.</returns>
        public static TweenerCore<Color, Color, ColorOptions> DOFade(this Image target, float endValue, float duration)
        {
            TweenerCore<Color, Color, ColorOptions> t = DOTween.ToAlpha(() => target.color, x => target.color = x, endValue, duration);
            t.SetTarget(target);
            return t;
        }

        /// <summary>Tweens an Image's fillAmount to the given value.
        /// Also stores the image as the tween's target so it can be used for filtered operations</summary>
        /// <summary>
        /// Tweens the Image's fillAmount property to a specified value between 0 and 1.
        /// </summary>
        /// <param name="endValue">Target fill amount; values outside [0, 1] are clamped to that range.</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <returns>The tweener that animates the Image.fillAmount to the specified value.</returns>
        public static TweenerCore<float, float, FloatOptions> DOFillAmount(this Image target, float endValue, float duration)
        {
            if (endValue > 1) endValue = 1;
            else if (endValue < 0) endValue = 0;
            TweenerCore<float, float, FloatOptions> t = DOTween.To(() => target.fillAmount, x => target.fillAmount = x, endValue, duration);
            t.SetTarget(target);
            return t;
        }

        /// <summary>Tweens an Image's colors using the given gradient
        /// (NOTE 1: only uses the colors of the gradient, not the alphas - NOTE 2: creates a Sequence, not a Tweener).
        /// Also stores the image as the tween's target so it can be used for filtered operations</summary>
        /// <summary>
        /// Creates a Sequence that animates the Image's color through the provided Gradient over the specified duration.
        /// </summary>
        /// <param name="target">The Image whose color will be animated.</param>
        /// <param name="gradient">The Gradient whose color keys will be used, played in order across the duration.</param>
        /// <param name="duration">Total time, in seconds, for the sequence to complete.</param>
        /// <returns>A Sequence that tweens the Image's color through the gradient's color keys over the specified duration and has the Image set as its target.</returns>
        public static Sequence DOGradientColor(this Image target, Gradient gradient, float duration)
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

        #endregion

        #region LayoutElement

        /// <summary>Tweens an LayoutElement's flexibleWidth/Height to the given value.
        /// Also stores the LayoutElement as the tween's target so it can be used for filtered operations</summary>
        /// <param name="endValue">The end value to reach</param><param name="duration">The duration of the tween</param>
        /// <summary>
        /// Animates a LayoutElement's flexibleWidth and flexibleHeight to the specified values.
        /// </summary>
        /// <param name="target">The LayoutElement whose flexible size will be animated.</param>
        /// <param name="endValue">Target values for flexibleWidth (x) and flexibleHeight (y).</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <param name="snapping">If true, snapped values will be rounded to integers during the animation.</param>
        /// <returns>A TweenerCore that animates the LayoutElement's flexibleWidth and flexibleHeight to <paramref name="endValue"/>.</returns>
        public static TweenerCore<Vector2, Vector2, VectorOptions> DOFlexibleSize(this LayoutElement target, Vector2 endValue, float duration, bool snapping = false)
        {
            TweenerCore<Vector2, Vector2, VectorOptions> t = DOTween.To(() => new Vector2(target.flexibleWidth, target.flexibleHeight), x => {
                    target.flexibleWidth = x.x;
                    target.flexibleHeight = x.y;
                }, endValue, duration);
            t.SetOptions(snapping).SetTarget(target);
            return t;
        }

        /// <summary>Tweens an LayoutElement's minWidth/Height to the given value.
        /// Also stores the LayoutElement as the tween's target so it can be used for filtered operations</summary>
        /// <param name="endValue">The end value to reach</param><param name="duration">The duration of the tween</param>
        /// <summary>
        /// Animates the LayoutElement's minimum width and minimum height to the specified values.
        /// </summary>
        /// <param name="target">The LayoutElement whose <c>minWidth</c> and <c>minHeight</c> will be animated.</param>
        /// <param name="endValue">Target minimum width (x) and minimum height (y).</param>
        /// <param name="duration">Duration of the animation in seconds.</param>
        /// <param name="snapping">If true, values will be rounded to the nearest integer during the animation.</param>
        /// <returns>A Tweener that animates the element's <c>minWidth</c> and <c>minHeight</c> to <paramref name="endValue"/>.</returns>
        public static TweenerCore<Vector2, Vector2, VectorOptions> DOMinSize(this LayoutElement target, Vector2 endValue, float duration, bool snapping = false)
        {
            TweenerCore<Vector2, Vector2, VectorOptions> t = DOTween.To(() => new Vector2(target.minWidth, target.minHeight), x => {
                target.minWidth = x.x;
                target.minHeight = x.y;
            }, endValue, duration);
            t.SetOptions(snapping).SetTarget(target);
            return t;
        }

        /// <summary>Tweens an LayoutElement's preferredWidth/Height to the given value.
        /// Also stores the LayoutElement as the tween's target so it can be used for filtered operations</summary>
        /// <param name="endValue">The end value to reach</param><param name="duration">The duration of the tween</param>
        /// <summary>
        /// Tweens the LayoutElement's preferredWidth and preferredHeight to the given values.
        /// </summary>
        /// <param name="endValue">Target preferred width and height.</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <param name="snapping">If true, snaps intermediate values to integers during the animation.</param>
        /// <returns>A TweenerCore that animates the target's preferred size (width and height).</returns>
        public static TweenerCore<Vector2, Vector2, VectorOptions> DOPreferredSize(this LayoutElement target, Vector2 endValue, float duration, bool snapping = false)
        {
            TweenerCore<Vector2, Vector2, VectorOptions> t = DOTween.To(() => new Vector2(target.preferredWidth, target.preferredHeight), x => {
                target.preferredWidth = x.x;
                target.preferredHeight = x.y;
            }, endValue, duration);
            t.SetOptions(snapping).SetTarget(target);
            return t;
        }

        #endregion

        #region Outline

        /// <summary>Tweens a Outline's effectColor to the given value.
        /// Also stores the Outline as the tween's target so it can be used for filtered operations</summary>
        /// <summary>
        /// Animates the Outline component's effectColor to the specified color.
        /// </summary>
        /// <param name="endValue">Target color for the Outline's effectColor.</param>
        /// <param name="duration">Duration of the animation in seconds.</param>
        /// <returns>TweenerCore<Color, Color, ColorOptions> that animates the Outline's effectColor to the specified color.</returns>
        public static TweenerCore<Color, Color, ColorOptions> DOColor(this Outline target, Color endValue, float duration)
        {
            TweenerCore<Color, Color, ColorOptions> t = DOTween.To(() => target.effectColor, x => target.effectColor = x, endValue, duration);
            t.SetTarget(target);
            return t;
        }

        /// <summary>Tweens a Outline's effectColor alpha to the given value.
        /// Also stores the Outline as the tween's target so it can be used for filtered operations</summary>
        /// <summary>
        /// Animates the alpha channel of an Outline's effect color to a specified value.
        /// </summary>
        /// <param name="endValue">Target alpha value to reach (0 = transparent, 1 = fully opaque).</param>
        /// <param name="duration">Time, in seconds, the tween will take to complete.</param>
        /// <returns>A TweenerCore that tweens the Outline's effectColor alpha to <paramref name="endValue"/> over the specified duration.</returns>
        public static TweenerCore<Color, Color, ColorOptions> DOFade(this Outline target, float endValue, float duration)
        {
            TweenerCore<Color, Color, ColorOptions> t = DOTween.ToAlpha(() => target.effectColor, x => target.effectColor = x, endValue, duration);
            t.SetTarget(target);
            return t;
        }

        /// <summary>Tweens a Outline's effectDistance to the given value.
        /// Also stores the Outline as the tween's target so it can be used for filtered operations</summary>
        /// <summary>
        /// Animates the Outline component's effectDistance to the specified vector.
        /// </summary>
        /// <param name="endValue">The target effectDistance vector.</param>
        /// <param name="duration">The duration of the animation in seconds.</param>
        /// <returns>A TweenerCore that animates the Outline's effectDistance to <paramref name="endValue"/>.</returns>
        public static TweenerCore<Vector2, Vector2, VectorOptions> DOScale(this Outline target, Vector2 endValue, float duration)
        {
            TweenerCore<Vector2, Vector2, VectorOptions> t = DOTween.To(() => target.effectDistance, x => target.effectDistance = x, endValue, duration);
            t.SetTarget(target);
            return t;
        }

        #endregion

        #region RectTransform

        /// <summary>Tweens a RectTransform's anchoredPosition to the given value.
        /// Also stores the RectTransform as the tween's target so it can be used for filtered operations</summary>
        /// <param name="endValue">The end value to reach</param><param name="duration">The duration of the tween</param>
        /// <summary>
        /// Animates a RectTransform's anchoredPosition to the specified target value.
        /// </summary>
        /// <param name="target">The RectTransform whose anchoredPosition will be animated.</param>
        /// <param name="endValue">The target anchoredPosition to reach.</param>
        /// <param name="duration">The duration of the animation in seconds.</param>
        /// <param name="snapping">If true, tweened position values will be rounded to integers each update.</param>
        /// <returns>The TweenerCore that performs the anchoredPosition animation.</returns>
        public static TweenerCore<Vector2, Vector2, VectorOptions> DOAnchorPos(this RectTransform target, Vector2 endValue, float duration, bool snapping = false)
        {
            TweenerCore<Vector2, Vector2, VectorOptions> t = DOTween.To(() => target.anchoredPosition, x => target.anchoredPosition = x, endValue, duration);
            t.SetOptions(snapping).SetTarget(target);
            return t;
        }
        /// <summary>Tweens a RectTransform's anchoredPosition X to the given value.
        /// Also stores the RectTransform as the tween's target so it can be used for filtered operations</summary>
        /// <param name="endValue">The end value to reach</param><param name="duration">The duration of the tween</param>
        /// <summary>
        /// Tweens the RectTransform's anchoredPosition X component to the specified value.
        /// </summary>
        /// <param name="target">The RectTransform whose anchoredPosition X will be animated.</param>
        /// <param name="endValue">The target X value for anchoredPosition (in pixels).</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <param name="snapping">If true, intermediate values will be rounded to the nearest integer.</param>
        /// <returns>A TweenerCore that animates the anchoredPosition's X component.</returns>
        public static TweenerCore<Vector2, Vector2, VectorOptions> DOAnchorPosX(this RectTransform target, float endValue, float duration, bool snapping = false)
        {
            TweenerCore<Vector2, Vector2, VectorOptions> t = DOTween.To(() => target.anchoredPosition, x => target.anchoredPosition = x, new Vector2(endValue, 0), duration);
            t.SetOptions(AxisConstraint.X, snapping).SetTarget(target);
            return t;
        }
        /// <summary>Tweens a RectTransform's anchoredPosition Y to the given value.
        /// Also stores the RectTransform as the tween's target so it can be used for filtered operations</summary>
        /// <param name="endValue">The end value to reach</param><param name="duration">The duration of the tween</param>
        /// <summary>
        /// Animates the RectTransform's anchored Y position to the given value while leaving the X position unchanged.
        /// </summary>
        /// <param name="target">The RectTransform whose anchoredPosition will be animated.</param>
        /// <param name="endValue">The target Y value for anchoredPosition.</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <param name="snapping">If true, tweened values will be snapped to integer values.</param>
        /// <returns>The tweener that animates the anchored Y position.</returns>
        public static TweenerCore<Vector2, Vector2, VectorOptions> DOAnchorPosY(this RectTransform target, float endValue, float duration, bool snapping = false)
        {
            TweenerCore<Vector2, Vector2, VectorOptions> t = DOTween.To(() => target.anchoredPosition, x => target.anchoredPosition = x, new Vector2(0, endValue), duration);
            t.SetOptions(AxisConstraint.Y, snapping).SetTarget(target);
            return t;
        }

        /// <summary>Tweens a RectTransform's anchoredPosition3D to the given value.
        /// Also stores the RectTransform as the tween's target so it can be used for filtered operations</summary>
        /// <param name="endValue">The end value to reach</param><param name="duration">The duration of the tween</param>
        /// <summary>
        /// Animates the RectTransform's anchoredPosition3D to the specified target position over the given duration.
        /// </summary>
        /// <param name="target">The RectTransform whose anchoredPosition3D will be animated.</param>
        /// <param name="endValue">Target anchoredPosition3D value.</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <param name="snapping">If `true`, the tween will round intermediate values to integers.</param>
        /// <returns>A Tweener that animates the RectTransform's anchoredPosition3D to `endValue`.</returns>
        public static TweenerCore<Vector3, Vector3, VectorOptions> DOAnchorPos3D(this RectTransform target, Vector3 endValue, float duration, bool snapping = false)
        {
            TweenerCore<Vector3, Vector3, VectorOptions> t = DOTween.To(() => target.anchoredPosition3D, x => target.anchoredPosition3D = x, endValue, duration);
            t.SetOptions(snapping).SetTarget(target);
            return t;
        }
        /// <summary>Tweens a RectTransform's anchoredPosition3D X to the given value.
        /// Also stores the RectTransform as the tween's target so it can be used for filtered operations</summary>
        /// <param name="endValue">The end value to reach</param><param name="duration">The duration of the tween</param>
        /// <summary>
        /// Tweens the RectTransform's anchoredPosition3D X component to the specified value.
        /// </summary>
        /// <param name="target">The RectTransform whose anchoredPosition3D X will be animated.</param>
        /// <param name="endValue">The target X position for anchoredPosition3D.</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <param name="snapping">If true, values will be rounded to the nearest integer during the tween.</param>
        /// <returns>A Tweener that animates only the X axis of the RectTransform's anchoredPosition3D to <paramref name="endValue"/>.</returns>
        public static TweenerCore<Vector3, Vector3, VectorOptions> DOAnchorPos3DX(this RectTransform target, float endValue, float duration, bool snapping = false)
        {
            TweenerCore<Vector3, Vector3, VectorOptions> t = DOTween.To(() => target.anchoredPosition3D, x => target.anchoredPosition3D = x, new Vector3(endValue, 0, 0), duration);
            t.SetOptions(AxisConstraint.X, snapping).SetTarget(target);
            return t;
        }
        /// <summary>Tweens a RectTransform's anchoredPosition3D Y to the given value.
        /// Also stores the RectTransform as the tween's target so it can be used for filtered operations</summary>
        /// <param name="endValue">The end value to reach</param><param name="duration">The duration of the tween</param>
        /// <summary>
        /// Animates the target RectTransform's anchoredPosition3D Y component to the specified value over the given duration.
        /// </summary>
        /// <param name="target">The RectTransform whose anchoredPosition3D Y will be animated.</param>
        /// <param name="endValue">The target Y value for anchoredPosition3D.</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <param name="snapping">If true, intermediate and final values are rounded to integers during the tween.</param>
        /// <returns>The tween that animates the target's anchoredPosition3D Y component to the specified value.</returns>
        public static TweenerCore<Vector3, Vector3, VectorOptions> DOAnchorPos3DY(this RectTransform target, float endValue, float duration, bool snapping = false)
        {
            TweenerCore<Vector3, Vector3, VectorOptions> t = DOTween.To(() => target.anchoredPosition3D, x => target.anchoredPosition3D = x, new Vector3(0, endValue, 0), duration);
            t.SetOptions(AxisConstraint.Y, snapping).SetTarget(target);
            return t;
        }
        /// <summary>Tweens a RectTransform's anchoredPosition3D Z to the given value.
        /// Also stores the RectTransform as the tween's target so it can be used for filtered operations</summary>
        /// <param name="endValue">The end value to reach</param><param name="duration">The duration of the tween</param>
        /// <summary>
        /// Tweens the RectTransform's anchoredPosition3D Z component to the specified value.
        /// </summary>
        /// <param name="target">The RectTransform whose anchoredPosition3D will be animated.</param>
        /// <param name="endValue">Target Z value for the anchoredPosition3D.</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <param name="snapping">If true, tweened values are rounded to the nearest integer during the animation.</param>
        /// <returns>A TweenerCore<Vector3, Vector3, VectorOptions> configured to animate only the Z axis of anchoredPosition3D.</returns>
        public static TweenerCore<Vector3, Vector3, VectorOptions> DOAnchorPos3DZ(this RectTransform target, float endValue, float duration, bool snapping = false)
        {
            TweenerCore<Vector3, Vector3, VectorOptions> t = DOTween.To(() => target.anchoredPosition3D, x => target.anchoredPosition3D = x, new Vector3(0, 0, endValue), duration);
            t.SetOptions(AxisConstraint.Z, snapping).SetTarget(target);
            return t;
        }

        /// <summary>Tweens a RectTransform's anchorMax to the given value.
        /// Also stores the RectTransform as the tween's target so it can be used for filtered operations</summary>
        /// <param name="endValue">The end value to reach</param><param name="duration">The duration of the tween</param>
        /// <summary>
        /// Tweens the RectTransform's anchorMax to the specified value.
        /// </summary>
        /// <param name="snapping">If true, each animated value will be rounded to the nearest integer during the tween.</param>
        /// <returns>A TweenerCore that animates the target's anchorMax to <paramref name="endValue"/> over <paramref name="duration"/> seconds.</returns>
        public static TweenerCore<Vector2, Vector2, VectorOptions> DOAnchorMax(this RectTransform target, Vector2 endValue, float duration, bool snapping = false)
        {
            TweenerCore<Vector2, Vector2, VectorOptions> t = DOTween.To(() => target.anchorMax, x => target.anchorMax = x, endValue, duration);
            t.SetOptions(snapping).SetTarget(target);
            return t;
        }

        /// <summary>Tweens a RectTransform's anchorMin to the given value.
        /// Also stores the RectTransform as the tween's target so it can be used for filtered operations</summary>
        /// <param name="endValue">The end value to reach</param><param name="duration">The duration of the tween</param>
        /// <summary>
        /// Animates the RectTransform's anchorMin property to the specified value over time.
        /// </summary>
        /// <param name="target">The RectTransform whose anchorMin will be animated.</param>
        /// <param name="endValue">Target anchorMin value (anchored coordinates, typically in the 0–1 range).</param>
        /// <param name="duration">Duration of the animation in seconds.</param>
        /// <param name="snapping">If true, values will be rounded to the nearest integer during the tween.</param>
        /// <returns>The tween that animates the anchorMin to the specified value.</returns>
        public static TweenerCore<Vector2, Vector2, VectorOptions> DOAnchorMin(this RectTransform target, Vector2 endValue, float duration, bool snapping = false)
        {
            TweenerCore<Vector2, Vector2, VectorOptions> t = DOTween.To(() => target.anchorMin, x => target.anchorMin = x, endValue, duration);
            t.SetOptions(snapping).SetTarget(target);
            return t;
        }

        /// <summary>Tweens a RectTransform's pivot to the given value.
        /// Also stores the RectTransform as the tween's target so it can be used for filtered operations</summary>
        /// <summary>
        /// Animates the RectTransform's pivot to the specified value.
        /// </summary>
        /// <param name="target">The RectTransform whose pivot will be animated.</param>
        /// <param name="endValue">Target pivot as a Vector2 (typically normalized [0,1] coordinates where (0,0) is bottom-left and (1,1) is top-right).</param>
        /// <param name="duration">Duration of the animation in seconds.</param>
        /// <returns>A TweenerCore&lt;Vector2, Vector2, VectorOptions&gt; that animates the pivot to the given value.</returns>
        public static TweenerCore<Vector2, Vector2, VectorOptions> DOPivot(this RectTransform target, Vector2 endValue, float duration)
        {
            TweenerCore<Vector2, Vector2, VectorOptions> t = DOTween.To(() => target.pivot, x => target.pivot = x, endValue, duration);
            t.SetTarget(target);
            return t;
        }
        /// <summary>Tweens a RectTransform's pivot X to the given value.
        /// Also stores the RectTransform as the tween's target so it can be used for filtered operations</summary>
        /// <summary>
        /// Tweens the RectTransform's pivot X component to the specified value.
        /// </summary>
        /// <param name="endValue">Target pivot X value (typically between 0 and 1).</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <returns>A tweener that animates the pivot's X component to <paramref name="endValue"/>.</returns>
        public static TweenerCore<Vector2, Vector2, VectorOptions> DOPivotX(this RectTransform target, float endValue, float duration)
        {
            TweenerCore<Vector2, Vector2, VectorOptions> t = DOTween.To(() => target.pivot, x => target.pivot = x, new Vector2(endValue, 0), duration);
            t.SetOptions(AxisConstraint.X).SetTarget(target);
            return t;
        }
        /// <summary>Tweens a RectTransform's pivot Y to the given value.
        /// Also stores the RectTransform as the tween's target so it can be used for filtered operations</summary>
        /// <summary>
        /// Tweens the RectTransform's pivot Y component to the specified value.
        /// </summary>
        /// <param name="target">The RectTransform whose pivot Y will be animated.</param>
        /// <param name="endValue">The target Y value for the pivot (typically between 0 and 1).</param>
        /// <param name="duration">Duration in seconds over which the pivot Y is animated.</param>
        /// <returns>A tween that animates the RectTransform's pivot Y to the specified value.</returns>
        public static TweenerCore<Vector2, Vector2, VectorOptions> DOPivotY(this RectTransform target, float endValue, float duration)
        {
            TweenerCore<Vector2, Vector2, VectorOptions> t = DOTween.To(() => target.pivot, x => target.pivot = x, new Vector2(0, endValue), duration);
            t.SetOptions(AxisConstraint.Y).SetTarget(target);
            return t;
        }

        /// <summary>Tweens a RectTransform's sizeDelta to the given value.
        /// Also stores the RectTransform as the tween's target so it can be used for filtered operations</summary>
        /// <param name="endValue">The end value to reach</param><param name="duration">The duration of the tween</param>
        /// <summary>
        /// Animates a RectTransform's sizeDelta to the specified value over the given duration.
        /// </summary>
        /// <param name="target">The RectTransform whose sizeDelta will be animated.</param>
        /// <param name="endValue">The target sizeDelta value.</param>
        /// <param name="duration">The duration, in seconds, of the tween.</param>
        /// <param name="snapping">If true, animated values will be snapped to integers.</param>
        /// <returns>A TweenerCore that animates the RectTransform's sizeDelta and has its target set to the provided RectTransform.</returns>
        public static TweenerCore<Vector2, Vector2, VectorOptions> DOSizeDelta(this RectTransform target, Vector2 endValue, float duration, bool snapping = false)
        {
            TweenerCore<Vector2, Vector2, VectorOptions> t = DOTween.To(() => target.sizeDelta, x => target.sizeDelta = x, endValue, duration);
            t.SetOptions(snapping).SetTarget(target);
            return t;
        }

        /// <summary>Punches a RectTransform's anchoredPosition towards the given direction and then back to the starting one
        /// as if it was connected to the starting position via an elastic.
        /// Also stores the RectTransform as the tween's target so it can be used for filtered operations</summary>
        /// <param name="punch">The direction and strength of the punch (added to the RectTransform's current position)</param>
        /// <param name="duration">The duration of the tween</param>
        /// <param name="vibrato">Indicates how much will the punch vibrate</param>
        /// <param name="elasticity">Represents how much (0 to 1) the vector will go beyond the starting position when bouncing backwards.
        /// 1 creates a full oscillation between the punch direction and the opposite direction,
        /// while 0 oscillates only between the punch and the start position</param>
        /// <summary>
        /// Animates the RectTransform by applying a punch (impulse) to its anchoredPosition.
        /// </summary>
        /// <param name="punch">The strength and direction of the punch applied to the anchoredPosition.</param>
        /// <param name="duration">Duration of the punch animation in seconds.</param>
        /// <param name="vibrato">Number of oscillations during the punch.</param>
        /// <param name="elasticity">How much the punch will stretch beyond the starting position (0 = no overshoot, 1 = full overshoot).</param>
        /// <param name="snapping">If true, values will be rounded to the nearest integer during the animation.</param>
        /// <returns>A Tweener that animates the RectTransform's anchoredPosition.</returns>
        public static Tweener DOPunchAnchorPos(this RectTransform target, Vector2 punch, float duration, int vibrato = 10, float elasticity = 1, bool snapping = false)
        {
            return DOTween.Punch(() => target.anchoredPosition, x => target.anchoredPosition = x, punch, duration, vibrato, elasticity)
                .SetTarget(target).SetOptions(snapping);
        }

        /// <summary>Shakes a RectTransform's anchoredPosition with the given values.
        /// Also stores the RectTransform as the tween's target so it can be used for filtered operations</summary>
        /// <param name="duration">The duration of the tween</param>
        /// <param name="strength">The shake strength</param>
        /// <param name="vibrato">Indicates how much will the shake vibrate</param>
        /// <param name="randomness">Indicates how much the shake will be random (0 to 180 - values higher than 90 kind of suck, so beware). 
        /// Setting it to 0 will shake along a single direction.</param>
        /// <param name="snapping">If TRUE the tween will smoothly snap all values to integers</param>
        /// <param name="fadeOut">If TRUE the shake will automatically fadeOut smoothly within the tween's duration, otherwise it will not</param>
        /// <summary>
        /// Animates the RectTransform's anchoredPosition with a shake effect.
        /// </summary>
        /// <param name="target">The RectTransform to shake.</param>
        /// <param name="duration">Total duration of the shake animation in seconds.</param>
        /// <param name="strength">Maximum distance the anchoredPosition moves from its start position.</param>
        /// <param name="vibrato">Number of shake oscillations during the animation.</param>
        /// <param name="randomness">Degree of randomness applied to shake directions (0 = fully uniform, higher = more random).</param>
        /// <param name="snapping">Whether to round position values to integers during the shake.</param>
        /// <param name="fadeOut">Whether the shake strength fades out over the duration.</param>
        /// <param name="randomnessMode">Mode that controls how randomness is applied to the shake.</param>
        /// <returns>A Tweener that animates the RectTransform's anchoredPosition according to the configured shake parameters.</returns>
        public static Tweener DOShakeAnchorPos(this RectTransform target, float duration, float strength = 100, int vibrato = 10, float randomness = 90, bool snapping = false, bool fadeOut = true, ShakeRandomnessMode randomnessMode = ShakeRandomnessMode.Full)
        {
            return DOTween.Shake(() => target.anchoredPosition, x => target.anchoredPosition = x, duration, strength, vibrato, randomness, true, fadeOut, randomnessMode)
                .SetTarget(target).SetSpecialStartupMode(SpecialStartupMode.SetShake).SetOptions(snapping);
        }
        /// <summary>Shakes a RectTransform's anchoredPosition with the given values.
        /// Also stores the RectTransform as the tween's target so it can be used for filtered operations</summary>
        /// <param name="duration">The duration of the tween</param>
        /// <param name="strength">The shake strength on each axis</param>
        /// <param name="vibrato">Indicates how much will the shake vibrate</param>
        /// <param name="randomness">Indicates how much the shake will be random (0 to 180 - values higher than 90 kind of suck, so beware). 
        /// Setting it to 0 will shake along a single direction.</param>
        /// <param name="snapping">If TRUE the tween will smoothly snap all values to integers</param>
        /// <param name="fadeOut">If TRUE the shake will automatically fadeOut smoothly within the tween's duration, otherwise it will not</param>
        /// <summary>
        /// Shakes the RectTransform's anchoredPosition with a 2D noise-based shake.
        /// </summary>
        /// <param name="target">The RectTransform to animate.</param>
        /// <param name="duration">Total duration of the shake in seconds.</param>
        /// <param name="strength">Maximum shake displacement for x and y axes.</param>
        /// <param name="vibrato">Frequency of shake oscillations (higher = more shakes).</param>
        /// <param name="randomness">Degree of randomness in shake directions (0–100).</param>
        /// <param name="snapping">If true, final positions will be snapped to integers.</param>
        /// <param name="fadeOut">If true, the shake amplitude decreases over time.</param>
        /// <param name="randomnessMode">Mode that controls how randomness is applied to the shake.</param>
        /// <returns>A Tweener that controls the anchoredPosition shake animation.</returns>
        public static Tweener DOShakeAnchorPos(this RectTransform target, float duration, Vector2 strength, int vibrato = 10, float randomness = 90, bool snapping = false, bool fadeOut = true, ShakeRandomnessMode randomnessMode = ShakeRandomnessMode.Full)
        {
            return DOTween.Shake(() => target.anchoredPosition, x => target.anchoredPosition = x, duration, strength, vibrato, randomness, fadeOut, randomnessMode)
                .SetTarget(target).SetSpecialStartupMode(SpecialStartupMode.SetShake).SetOptions(snapping);
        }

        #region Special

        /// <summary>Tweens a RectTransform's anchoredPosition to the given value, while also applying a jump effect along the Y axis.
        /// Returns a Sequence instead of a Tweener.
        /// Also stores the RectTransform as the tween's target so it can be used for filtered operations</summary>
        /// <param name="endValue">The end value to reach</param>
        /// <param name="jumpPower">Power of the jump (the max height of the jump is represented by this plus the final Y offset)</param>
        /// <param name="numJumps">Total number of jumps</param>
        /// <param name="duration">The duration of the tween</param>
        /// <summary>
        /// Creates a tween Sequence that moves the RectTransform horizontally to the given x target while performing a specified number of vertical jumps toward the given y target.
        /// </summary>
        /// <param name="target">The RectTransform to animate.</param>
        /// <param name="endValue">Target anchored position; x is the horizontal destination, y is the vertical offset or target depending on context.</param>
        /// <param name="jumpPower">The height of each jump.</param>
        /// <param name="numJumps">Number of jumps to perform (will be treated as at least 1).</param>
        /// <param name="duration">Total duration of the composed animation.</param>
        /// <param name="snapping">If true, all tweened values will be rounded to the nearest integer.</param>
        /// <returns>A Sequence that moves the RectTransform horizontally while applying vertical jump motion; the Sequence's target is the provided RectTransform.</returns>
        public static Sequence DOJumpAnchorPos(this RectTransform target, Vector2 endValue, float jumpPower, int numJumps, float duration, bool snapping = false)
        {
            if (numJumps < 1) numJumps = 1;
            float startPosY = 0;
            float offsetY = -1;
            bool offsetYSet = false;

            // Separate Y Tween so we can elaborate elapsedPercentage on that insted of on the Sequence
            // (in case users add a delay or other elements to the Sequence)
            Sequence s = DOTween.Sequence();
            Tween yTween = DOTween.To(() => target.anchoredPosition, x => target.anchoredPosition = x, new Vector2(0, jumpPower), duration / (numJumps * 2))
                .SetOptions(AxisConstraint.Y, snapping).SetEase(Ease.OutQuad).SetRelative()
                .SetLoops(numJumps * 2, LoopType.Yoyo)
                .OnStart(()=> startPosY = target.anchoredPosition.y);
            s.Append(DOTween.To(() => target.anchoredPosition, x => target.anchoredPosition = x, new Vector2(endValue.x, 0), duration)
                    .SetOptions(AxisConstraint.X, snapping).SetEase(Ease.Linear)
                ).Join(yTween)
                .SetTarget(target).SetEase(DOTween.defaultEaseType);
            s.OnUpdate(() => {
                if (!offsetYSet) {
                    offsetYSet = true;
                    offsetY = s.isRelative ? endValue.y : endValue.y - startPosY;
                }
                Vector2 pos = target.anchoredPosition;
                pos.y += DOVirtual.EasedValue(0, offsetY, s.ElapsedDirectionalPercentage(), Ease.OutQuad);
                target.anchoredPosition = pos;
            });
            return s;
        }

        #endregion

        #endregion

        #region ScrollRect

        /// <summary>Tweens a ScrollRect's horizontal/verticalNormalizedPosition to the given value.
        /// Also stores the ScrollRect as the tween's target so it can be used for filtered operations</summary>
        /// <param name="endValue">The end value to reach</param><param name="duration">The duration of the tween</param>
        /// <summary>
        /// Animates the ScrollRect's normalized horizontal and vertical positions to the specified value.
        /// </summary>
        /// <param name="endValue">Target normalized positions (x = horizontal, y = vertical), each expected in the range [0, 1].</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <param name="snapping">If true, snap interpolated values to the nearest integer during the animation.</param>
        /// <returns>The created Tweener that animates the ScrollRect's normalized position.</returns>
        public static Tweener DONormalizedPos(this ScrollRect target, Vector2 endValue, float duration, bool snapping = false)
        {
            return DOTween.To(() => new Vector2(target.horizontalNormalizedPosition, target.verticalNormalizedPosition),
                x => {
                    target.horizontalNormalizedPosition = x.x;
                    target.verticalNormalizedPosition = x.y;
                }, endValue, duration)
                .SetOptions(snapping).SetTarget(target);
        }
        /// <summary>Tweens a ScrollRect's horizontalNormalizedPosition to the given value.
        /// Also stores the ScrollRect as the tween's target so it can be used for filtered operations</summary>
        /// <param name="endValue">The end value to reach</param><param name="duration">The duration of the tween</param>
        /// <summary>
        /// Animates the ScrollRect's horizontal normalized position to the specified target.
        /// </summary>
        /// <param name="target">The ScrollRect to animate.</param>
        /// <param name="endValue">Target horizontal normalized position (0 to 1).</param>
        /// <param name="duration">Tween duration in seconds.</param>
        /// <param name="snapping">If true, values will be snapped to integer values during the tween.</param>
        /// <returns>A Tweener that animates the ScrollRect.horizontalNormalizedPosition to <paramref name="endValue"/>.</returns>
        public static Tweener DOHorizontalNormalizedPos(this ScrollRect target, float endValue, float duration, bool snapping = false)
        {
            return DOTween.To(() => target.horizontalNormalizedPosition, x => target.horizontalNormalizedPosition = x, endValue, duration)
                .SetOptions(snapping).SetTarget(target);
        }
        /// <summary>Tweens a ScrollRect's verticalNormalizedPosition to the given value.
        /// Also stores the ScrollRect as the tween's target so it can be used for filtered operations</summary>
        /// <param name="endValue">The end value to reach</param><param name="duration">The duration of the tween</param>
        /// <summary>
        /// Animates a ScrollRect's vertical normalized position to a specified value.
        /// </summary>
        /// <param name="endValue">Target vertical normalized position (typically between 0 and 1).</param>
        /// <param name="duration">Duration of the animation in seconds.</param>
        /// <param name="snapping">If true, tweened values will be snapped to integers during the animation.</param>
        /// <returns>A Tweener that animates the ScrollRect's <c>verticalNormalizedPosition</c> to <paramref name="endValue"/>.</returns>
        public static Tweener DOVerticalNormalizedPos(this ScrollRect target, float endValue, float duration, bool snapping = false)
        {
            return DOTween.To(() => target.verticalNormalizedPosition, x => target.verticalNormalizedPosition = x, endValue, duration)
                .SetOptions(snapping).SetTarget(target);
        }

        #endregion

        #region Slider

        /// <summary>Tweens a Slider's value to the given value.
        /// Also stores the Slider as the tween's target so it can be used for filtered operations</summary>
        /// <param name="endValue">The end value to reach</param><param name="duration">The duration of the tween</param>
        /// <summary>
        /// Animates a Slider's value to the specified target value over the given duration.
        /// </summary>
        /// <param name="target">The Slider whose value will be animated.</param>
        /// <param name="endValue">The target value the Slider will reach.</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <param name="snapping">If true, values produced by the tween are rounded to the nearest integer.</param>
        /// <returns>A TweenerCore that animates the Slider's value to <paramref name="endValue"/>.</returns>
        public static TweenerCore<float, float, FloatOptions> DOValue(this Slider target, float endValue, float duration, bool snapping = false)
        {
            TweenerCore<float, float, FloatOptions> t = DOTween.To(() => target.value, x => target.value = x, endValue, duration);
            t.SetOptions(snapping).SetTarget(target);
            return t;
        }

        #endregion

        #region Text

        /// <summary>Tweens a Text's color to the given value.
        /// Also stores the Text as the tween's target so it can be used for filtered operations</summary>
        /// <summary>
        /// Animates the Text component's color to the specified end color over the given duration.
        /// </summary>
        /// <param name="target">The Text component whose color will be animated.</param>
        /// <param name="endValue">The color to reach by the end of the tween.</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <returns>A TweenerCore<Color, Color, ColorOptions> that animates the Text's color.</returns>
        public static TweenerCore<Color, Color, ColorOptions> DOColor(this Text target, Color endValue, float duration)
        {
            TweenerCore<Color, Color, ColorOptions> t = DOTween.To(() => target.color, x => target.color = x, endValue, duration);
            t.SetTarget(target);
            return t;
        }

        /// <summary>
        /// Tweens a Text's text from one integer to another, with options for thousands separators
        /// </summary>
        /// <param name="fromValue">The value to start from</param>
        /// <param name="endValue">The end value to reach</param>
        /// <param name="duration">The duration of the tween</param>
        /// <param name="addThousandsSeparator">If TRUE (default) also adds thousands separators</param>
        /// <summary>
        /// Animates the Text component to display an integer counter from a starting value to an end value.
        /// </summary>
        /// <param name="target">The Text component to update.</param>
        /// <param name="fromValue">Initial integer value of the counter.</param>
        /// <param name="endValue">Final integer value to reach.</param>
        /// <param name="duration">Duration of the animation in seconds.</param>
        /// <param name="addThousandsSeparator">If true, formats numbers with thousands separators.</param>
        /// <param name="culture">The <see cref="CultureInfo"/> to use for formatting; uses <see cref="CultureInfo.InvariantCulture"/> when null and thousands separators are enabled.</param>
        /// <returns>A TweenerCore that animates the integer value and updates the Text component.</returns>
        public static TweenerCore<int, int, NoOptions> DOCounter(
            this Text target, int fromValue, int endValue, float duration, bool addThousandsSeparator = true, CultureInfo culture = null
        ){
            int v = fromValue;
            CultureInfo cInfo = !addThousandsSeparator ? null : culture ?? CultureInfo.InvariantCulture;
            TweenerCore<int, int, NoOptions> t = DOTween.To(() => v, x => {
                v = x;
                target.text = addThousandsSeparator
                    ? v.ToString("N0", cInfo)
                    : v.ToString();
            }, endValue, duration);
            t.SetTarget(target);
            return t;
        }

        /// <summary>Tweens a Text's alpha color to the given value.
        /// Also stores the Text as the tween's target so it can be used for filtered operations</summary>
        /// <summary>
        /// Tweens the Text component's color alpha to the specified value.
        /// </summary>
        /// <param name="target">The Text component whose color alpha will be animated.</param>
        /// <param name="endValue">The target alpha value to reach (typically between 0 and 1).</param>
        /// <param name="duration">Animation duration in seconds.</param>
        /// <returns>A TweenerCore that animates the Text color's alpha and has the Text set as its target.</returns>
        public static TweenerCore<Color, Color, ColorOptions> DOFade(this Text target, float endValue, float duration)
        {
            TweenerCore<Color, Color, ColorOptions> t = DOTween.ToAlpha(() => target.color, x => target.color = x, endValue, duration);
            t.SetTarget(target);
            return t;
        }

        /// <summary>Tweens a Text's text to the given value.
        /// Also stores the Text as the tween's target so it can be used for filtered operations</summary>
        /// <param name="endValue">The end string to tween to</param><param name="duration">The duration of the tween</param>
        /// <param name="richTextEnabled">If TRUE (default), rich text will be interpreted correctly while animated,
        /// otherwise all tags will be considered as normal text</param>
        /// <param name="scrambleMode">The type of scramble mode to use, if any</param>
        /// <param name="scrambleChars">A string containing the characters to use for scrambling.
        /// Use as many characters as possible (minimum 10) because DOTween uses a fast scramble mode which gives better results with more characters.
        /// <summary>
        /// Tweens a Text component's displayed string to the specified value over the given duration.
        /// </summary>
        /// <param name="target">The Text component whose text will be animated.</param>
        /// <param name="endValue">The target string; if null, an empty string will be used and a warning may be logged.</param>
        /// <param name="duration">Animation duration in seconds.</param>
        /// <param name="richTextEnabled">If true, rich text tags in the string are honored during the tween.</param>
        /// <param name="scrambleMode">Determines the scrambling mode to use while animating characters.</param>
        /// <param name="scrambleChars">Optional custom characters to use when scrambling; leave null to use the default set.</param>
        /// <returns>A tweener that animates the Text.text to the specified string.</returns>
        public static TweenerCore<string, string, StringOptions> DOText(this Text target, string endValue, float duration, bool richTextEnabled = true, ScrambleMode scrambleMode = ScrambleMode.None, string scrambleChars = null)
        {
            if (endValue == null) {
                if (Debugger.logPriority > 0) Debugger.LogWarning("You can't pass a NULL string to DOText: an empty string will be used instead to avoid errors");
                endValue = "";
            }
            TweenerCore<string, string, StringOptions> t = DOTween.To(() => target.text, x => target.text = x, endValue, duration);
            t.SetOptions(richTextEnabled, scrambleMode, scrambleChars)
                .SetTarget(target);
            return t;
        }

        #endregion

        #region Blendables

        #region Graphic

        /// <summary>Tweens a Graphic's color to the given value,
        /// in a way that allows other DOBlendableColor tweens to work together on the same target,
        /// instead than fight each other as multiple DOColor would do.
        /// Also stores the Graphic as the tween's target so it can be used for filtered operations</summary>
        /// <summary>
        /// Animates the Graphic's color by applying incremental (blendable) color changes toward the specified target color.
        /// </summary>
        /// <param name="target">The Graphic whose color will be animated.</param>
        /// <param name="endValue">The target color to reach (specified as a delta from the current color).</param>
        /// <param name="duration">Time, in seconds, for the animation to complete.</param>
        /// <returns>The blendable Tweener that applies incremental color changes to the target Graphic.</returns>
        public static Tweener DOBlendableColor(this Graphic target, Color endValue, float duration)
        {
            endValue = endValue - target.color;
            Color to = new Color(0, 0, 0, 0);
            return DOTween.To(() => to, x => {
                Color diff = x - to;
                to = x;
                target.color += diff;
            }, endValue, duration)
                .Blendable().SetTarget(target);
        }

        #endregion

        #region Image

        /// <summary>Tweens a Image's color to the given value,
        /// in a way that allows other DOBlendableColor tweens to work together on the same target,
        /// instead than fight each other as multiple DOColor would do.
        /// Also stores the Image as the tween's target so it can be used for filtered operations</summary>
        /// <summary>
        /// Animates the Image's color by applying an additive, blendable color change toward the specified color.
        /// </summary>
        /// <param name="endValue">Target color; the tween applies the difference between this color and the Image's current color as an additive (blendable) change.</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <returns>A Tweener that applies incremental (blendable) color changes to the Image.</returns>
        public static Tweener DOBlendableColor(this Image target, Color endValue, float duration)
        {
            endValue = endValue - target.color;
            Color to = new Color(0, 0, 0, 0);
            return DOTween.To(() => to, x => {
                Color diff = x - to;
                to = x;
                target.color += diff;
            }, endValue, duration)
                .Blendable().SetTarget(target);
        }

        #endregion

        #region Text

        /// <summary>Tweens a Text's color BY the given value,
        /// in a way that allows other DOBlendableColor tweens to work together on the same target,
        /// instead than fight each other as multiple DOColor would do.
        /// Also stores the Text as the tween's target so it can be used for filtered operations</summary>
        /// <summary>
        /// Animates the Text's color by applying incremental (blendable) color changes toward the specified color.
        /// </summary>
        /// <param name="endValue">The target color to reach.</param>
        /// <param name="duration">The duration of the tween, in seconds.</param>
        /// <returns>A Tweener that applies blendable color changes to the Text's color until it reaches the target color.</returns>
        public static Tweener DOBlendableColor(this Text target, Color endValue, float duration)
        {
            endValue = endValue - target.color;
            Color to = new Color(0, 0, 0, 0);
            return DOTween.To(() => to, x => {
                Color diff = x - to;
                to = x;
                target.color += diff;
            }, endValue, duration)
                .Blendable().SetTarget(target);
        }

        #endregion

        #endregion

        #region Shapes

        /// <summary>Tweens a RectTransform's anchoredPosition so that it draws a circle around the given center.
        /// Also stores the RectTransform as the tween's target so it can be used for filtered operations.<para/>
        /// IMPORTANT: SetFrom(value) requires a <see cref="Vector2"/> instead of a float, where the X property represents the "from degrees value"</summary>
        /// <param name="center">Circle-center/pivot around which to rotate (in UI anchoredPosition coordinates)</param>
        /// <param name="endValueDegrees">The end value degrees to reach (to rotate counter-clockwise pass a negative value)</param>
        /// <param name="duration">The duration of the tween</param>
        /// <param name="relativeCenter">If TRUE the <see cref="center"/> coordinates will be considered as relative to the target's current anchoredPosition</param>
        /// <summary>
        /// Animates the RectTransform's anchoredPosition along a circular arc around the specified center.
        /// </summary>
        /// <param name="center">The center point of the circle. If <paramref name="relativeCenter"/> is true, this is an offset from the target's starting anchoredPosition.</param>
        /// <param name="endValueDegrees">The angle in degrees defining the arc's end position relative to the start angle.</param>
        /// <param name="duration">The duration of the tween in seconds.</param>
        /// <param name="relativeCenter">If true, treats <paramref name="center"/> as an offset from the target's starting anchoredPosition; otherwise treats it as an absolute position.</param>
        /// <param name="snapping">If true, position values are snapped to whole integers during the tween.</param>
        /// <returns>A tween that animates the target's anchoredPosition along the specified circular arc.</returns>
        public static TweenerCore<Vector2, Vector2, CircleOptions> DOShapeCircle(
            this RectTransform target, Vector2 center, float endValueDegrees, float duration, bool relativeCenter = false, bool snapping = false
        )
        {
            TweenerCore<Vector2, Vector2, CircleOptions> t = DOTween.To(
                CirclePlugin.Get(), () => target.anchoredPosition, x => target.anchoredPosition = x, center, duration
            );
            t.SetOptions(endValueDegrees, relativeCenter, snapping).SetTarget(target);
            return t;
        }

        #endregion

        #endregion

        // █████████████████████████████████████████████████████████████████████████████████████████████████████████████████████
        // ███ INTERNAL CLASSES ████████████████████████████████████████████████████████████████████████████████████████████████
        // █████████████████████████████████████████████████████████████████████████████████████████████████████████████████████

        public static class Utils
        {
            /// <summary>
            /// Converts the anchoredPosition of the first RectTransform to the second RectTransform,
            /// taking into consideration offset, anchors and pivot, and returns the new anchoredPosition
            /// <summary>
            /// Converts the position of a source RectTransform to the equivalent anchored position in a destination RectTransform.
            /// </summary>
            /// <param name="from">The source RectTransform whose position will be converted.</param>
            /// <param name="to">The destination RectTransform whose local anchored position space to convert into.</param>
            /// <returns>The anchored position in the destination RectTransform that corresponds to the source RectTransform's world position.</returns>
            public static Vector2 SwitchToRectTransform(RectTransform from, RectTransform to)
            {
                Vector2 localPoint;
                Vector2 fromPivotDerivedOffset = new Vector2(from.rect.width * 0.5f + from.rect.xMin, from.rect.height * 0.5f + from.rect.yMin);
                Vector2 screenP = RectTransformUtility.WorldToScreenPoint(null, from.position);
                screenP += fromPivotDerivedOffset;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(to, screenP, null, out localPoint);
                Vector2 pivotDerivedOffset = new Vector2(to.rect.width * 0.5f + to.rect.xMin, to.rect.height * 0.5f + to.rect.yMin);
                return to.anchoredPosition + localPoint - pivotDerivedOffset;
            }
        }
	}
}
#endif