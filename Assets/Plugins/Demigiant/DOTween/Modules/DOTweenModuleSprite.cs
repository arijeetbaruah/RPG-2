// Author: Daniele Giardini - http://www.demigiant.com
// Created: 2018/07/13

#if true // MODULE_MARKER
using System;
using UnityEngine;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

#pragma warning disable 1591
namespace DG.Tweening
{
	public static class DOTweenModuleSprite
    {
        #region Shortcuts

        #region SpriteRenderer

        /// <summary>Tweens a SpriteRenderer's color to the given value.
        /// Also stores the spriteRenderer as the tween's target so it can be used for filtered operations</summary>
        /// <summary>
        /// Tweens the SpriteRenderer's color to the specified end color over the given duration.
        /// </summary>
        /// <param name="endValue">Target color to reach.</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <returns>A TweenerCore that animates the renderer's color; the SpriteRenderer is set as the tween's target.</returns>
        public static TweenerCore<Color, Color, ColorOptions> DOColor(this SpriteRenderer target, Color endValue, float duration)
        {
            TweenerCore<Color, Color, ColorOptions> t = DOTween.To(() => target.color, x => target.color = x, endValue, duration);
            t.SetTarget(target);
            return t;
        }

        /// <summary>Tweens a Material's alpha color to the given value.
        /// Also stores the spriteRenderer as the tween's target so it can be used for filtered operations</summary>
        /// <summary>
        /// Tweens the SpriteRenderer's color alpha to the specified value.
        /// </summary>
        /// <param name="endValue">Target alpha value (0 = transparent, 1 = opaque).</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <returns>A tween that animates the SpriteRenderer's alpha to <paramref name="endValue"/>; its target is set to the provided SpriteRenderer.</returns>
        public static TweenerCore<Color, Color, ColorOptions> DOFade(this SpriteRenderer target, float endValue, float duration)
        {
            TweenerCore<Color, Color, ColorOptions> t = DOTween.ToAlpha(() => target.color, x => target.color = x, endValue, duration);
            t.SetTarget(target);
            return t;
        }

        /// <summary>Tweens a SpriteRenderer's color using the given gradient
        /// (NOTE 1: only uses the colors of the gradient, not the alphas - NOTE 2: creates a Sequence, not a Tweener).
        /// Also stores the image as the tween's target so it can be used for filtered operations</summary>
        /// <summary>
        /// Animates the SpriteRenderer's color through the provided Gradient over the given total duration.
        /// </summary>
        /// <remarks>
        /// Only gradient color keys are used (alpha is not animated). If the first color key has time &lt;= 0 the color is applied immediately.
        /// The method builds a Sequence whose segments correspond to the gradient's color keys and adjusts the final segment so the sequence's total duration equals <paramref name="duration"/>.
        /// The returned Sequence has its target set to the provided <paramref name="target"/>.
        /// </remarks>
        /// <param name="gradient">Gradient whose color keys define the colors and relative timings for the animation; alpha channels are ignored.</param>
        /// <param name="duration">Total duration of the whole gradient color animation, in seconds.</param>
        /// <returns>A Sequence that animates the SpriteRenderer's color through the gradient's colors.</returns>
        public static Sequence DOGradientColor(this SpriteRenderer target, Gradient gradient, float duration)
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

        #region Blendables

        #region SpriteRenderer

        /// <summary>Tweens a SpriteRenderer's color to the given value,
        /// in a way that allows other DOBlendableColor tweens to work together on the same target,
        /// instead than fight each other as multiple DOColor would do.
        /// Also stores the SpriteRenderer as the tween's target so it can be used for filtered operations</summary>
        /// <summary>
        /// Animates the SpriteRenderer's color by blending toward the specified color so multiple color tweens can coexist.
        /// </summary>
        /// <param name="endValue">Target color to reach; the value is applied as a blendable delta relative to the SpriteRenderer's current color.</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <returns>A Tweener configured to perform a blendable color animation on the target SpriteRenderer.</returns>
        public static Tweener DOBlendableColor(this SpriteRenderer target, Color endValue, float duration)
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

        #endregion
	}
}
#endif