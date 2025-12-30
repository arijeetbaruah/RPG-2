using UnityEngine;

#if false || EPO_DOTWEEN // MODULE_MARKER

using EPOOutline;
using DG.Tweening.Plugins.Options;
using DG.Tweening;
using DG.Tweening.Core;

namespace DG.Tweening
{
    public static class DOTweenModuleEPOOutline
    {
        /// <summary>
        /// Kills all tweens targeting the specified SerializedPass.
        /// </summary>
        /// <param name="target">The SerializedPass whose associated tweens will be killed.</param>
        /// <param name="complete">If true, completes each tween before killing it.</param>
        /// <returns>The number of tweens that were killed.</returns>
        public static int DOKill(this SerializedPass target, bool complete)
        {
            return DOTween.Kill(target, complete);
        }

        /// <summary>
        /// Creates a tween that animates the float property on the given SerializedPass identified by name.
        /// </summary>
        /// <param name="target">The SerializedPass whose property will be animated.</param>
        /// <param name="propertyName">The name of the float property to animate (shader/property key).</param>
        /// <param name="endValue">The target value the property will be tweened to.</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <returns>A TweenerCore that animates the property's value to <paramref name="endValue"/> over <paramref name="duration"/>.</returns>
        public static TweenerCore<float, float, FloatOptions> DOFloat(this SerializedPass target, string propertyName, float endValue, float duration)
        {
            var tweener = DOTween.To(() => target.GetFloat(propertyName), x => target.SetFloat(propertyName, x), endValue, duration);
            tweener.SetOptions(true).SetTarget(target);
            return tweener;
        }

        /// <summary>
        /// Tweens the alpha component of a named color property on a SerializedPass to a target value.
        /// </summary>
        /// <param name="target">The SerializedPass whose property will be tweened.</param>
        /// <param name="propertyName">The name of the color property to animate.</param>
        /// <param name="endValue">The target alpha value (0 to 1).</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <returns>A TweenerCore instance that animates the property's color alpha to <paramref name="endValue"/>.</returns>
        public static TweenerCore<Color, Color, ColorOptions> DOFade(this SerializedPass target, string propertyName, float endValue, float duration)
        {
            var tweener = DOTween.ToAlpha(() => target.GetColor(propertyName), x => target.SetColor(propertyName, x), endValue, duration);
            tweener.SetOptions(true).SetTarget(target);
            return tweener;
        }

        /// <summary>
        /// Creates a tween that animates a color property on a SerializedPass to the specified target color.
        /// </summary>
        /// <param name="target">The SerializedPass containing the color property to animate.</param>
        /// <param name="propertyName">The name of the color property to animate.</param>
        /// <param name="endValue">The target color value.</param>
        /// <param name="duration">The duration of the tween in seconds.</param>
        /// <returns>A TweenerCore that animates the property's color to <paramref name="endValue"/> over the specified <paramref name="duration"/>.</returns>
        public static TweenerCore<Color, Color, ColorOptions> DOColor(this SerializedPass target, string propertyName, Color endValue, float duration)
        {
            var tweener = DOTween.To(() => target.GetColor(propertyName), x => target.SetColor(propertyName, x), endValue, duration);
            tweener.SetOptions(false).SetTarget(target);
            return tweener;
        }

        /// <summary>
        /// Animates a Vector4 property on a SerializedPass from its current value to the specified end value over the given duration.
        /// </summary>
        /// <param name="target">The SerializedPass that owns the vector property.</param>
        /// <param name="propertyName">The name of the vector property to animate.</param>
        /// <param name="endValue">The target Vector4 value.</param>
        /// <param name="duration">Time, in seconds, for the animation to complete.</param>
        /// <returns>A TweenerCore&lt;Vector4, Vector4, VectorOptions&gt; that animates the property's Vector4 value to the specified end value.</returns>
        public static TweenerCore<Vector4, Vector4, VectorOptions> DOVector(this SerializedPass target, string propertyName, Vector4 endValue, float duration)
        {
            var tweener = DOTween.To(() => target.GetVector(propertyName), x => target.SetVector(propertyName, x), endValue, duration);
            tweener.SetOptions(false).SetTarget(target);
            return tweener;
        }

        /// <summary>
        /// Animates the float property on a SerializedPass identified by the given property ID.
        /// </summary>
        /// <param name="target">The SerializedPass containing the property to animate.</param>
        /// <param name="propertyId">The integer identifier of the float property to tween.</param>
        /// <param name="endValue">The target float value at the end of the tween.</param>
        /// <param name="duration">The duration of the tween in seconds.</param>
        /// <returns>A TweenerCore configured to animate the property's value to <paramref name="endValue"/> over <paramref name="duration"/> seconds and targeted to <paramref name="target"/>.</returns>
        public static TweenerCore<float, float, FloatOptions> DOFloat(this SerializedPass target, int propertyId, float endValue, float duration)
        {
            var tweener = DOTween.To(() => target.GetFloat(propertyId), x => target.SetFloat(propertyId, x), endValue, duration);
            tweener.SetOptions(true).SetTarget(target);
            return tweener;
        }

        /// <summary>
        /// Animates the alpha channel of a color property on the given SerializedPass to the specified value.
        /// </summary>
        /// <param name="target">The SerializedPass whose color property will be animated.</param>
        /// <param name="propertyId">The shader/property identifier of the color to animate.</param>
        /// <param name="endValue">The target alpha value (0 to 1).</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <returns>A tweener that animates the property's color alpha to <paramref name="endValue"/>.</returns>
        public static TweenerCore<Color, Color, ColorOptions> DOFade(this SerializedPass target, int propertyId, float endValue, float duration)
        {
            var tweener = DOTween.ToAlpha(() => target.GetColor(propertyId), x => target.SetColor(propertyId, x), endValue, duration);
            tweener.SetOptions(true).SetTarget(target);
            return tweener;
        }

        /// <summary>
        /// Animates a color property on a SerializedPass (by property ID) to the specified target color.
        /// </summary>
        /// <param name="target">The SerializedPass containing the color property to animate.</param>
        /// <param name="propertyId">The shader property ID of the color to animate.</param>
        /// <param name="endValue">The target color value.</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <returns>A TweenerCore&lt;Color, Color, ColorOptions&gt; configured to animate the specified color property.</returns>
        public static TweenerCore<Color, Color, ColorOptions> DOColor(this SerializedPass target, int propertyId, Color endValue, float duration)
        {
            var tweener = DOTween.To(() => target.GetColor(propertyId), x => target.SetColor(propertyId, x), endValue, duration);
            tweener.SetOptions(false).SetTarget(target);
            return tweener;
        }

        /// <summary>
        /// Creates a tween that animates a Vector4 property on the given SerializedPass identified by the property ID.
        /// </summary>
        /// <param name="target">The SerializedPass whose vector property will be animated.</param>
        /// <param name="propertyId">The integer identifier of the vector property to animate (shader property ID).</param>
        /// <param name="endValue">The target Vector4 value to reach.</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <returns>The configured TweenerCore that animates the property's Vector4 value to <paramref name="endValue"/>.</returns>
        public static TweenerCore<Vector4, Vector4, VectorOptions> DOVector(this SerializedPass target, int propertyId, Vector4 endValue, float duration)
        {
            var tweener = DOTween.To(() => target.GetVector(propertyId), x => target.SetVector(propertyId, x), endValue, duration);
            tweener.SetOptions(false).SetTarget(target);
            return tweener;
        }

        /// <summary>
        /// Kills all tweens targeting the given outline properties.
        /// </summary>
        /// <param name="target">The outline properties whose tweens will be killed.</param>
        /// <param name="complete">If true, tweens will be completed before being killed.</param>
        /// <returns>The number of tweens that were killed.</returns>
        public static int DOKill(this Outlinable.OutlineProperties target, bool complete = false)
        {
            return DOTween.Kill(target, complete);
        }

        /// <summary>
        /// Kills all tweens associated with the specified Outliner.
        /// </summary>
        /// <param name="target">The Outliner whose tweens will be killed.</param>
        /// <param name="complete">If true, completes each tween before killing it.</param>
        /// <returns>The number of tweens that were killed.</returns>
        public static int DOKill(this Outliner target, bool complete = false)
        {
            return DOTween.Kill(target, complete);
        }

        /// <summary>
        /// Controls the alpha (transparency) of the outline
        /// <summary>
        /// Animates the outline's color alpha from its current value to <paramref name="endValue"/> over <paramref name="duration"/> seconds.
        /// </summary>
        /// <param name="target">The OutlineProperties instance whose color alpha will be animated.</param>
        /// <param name="endValue">Target alpha value (0 to 1).</param>
        /// <param name="duration">Animation duration in seconds.</param>
        /// <returns>A TweenerCore that animates the outline color's alpha.</returns>
        public static TweenerCore<Color, Color, ColorOptions> DOFade(this Outlinable.OutlineProperties target, float endValue, float duration)
        {
            var tweener = DOTween.ToAlpha(() => target.Color, x => target.Color = x, endValue, duration);
            tweener.SetOptions(true).SetTarget(target);
            return tweener;
        }

        /// <summary>
        /// Controls the color of the outline
        /// <summary>
        /// Tweens the outline's color from its current value to the specified color over the given duration.
        /// </summary>
        /// <param name="target">The OutlineProperties instance whose Color will be animated.</param>
        /// <param name="endValue">The color to reach at the end of the tween.</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <returns>The TweenerCore that controls the color tween.</returns>
        public static TweenerCore<Color, Color, ColorOptions> DOColor(this Outlinable.OutlineProperties target, Color endValue, float duration)
        {
            var tweener = DOTween.To(() => target.Color, x => target.Color = x, endValue, duration);
            tweener.SetOptions(false).SetTarget(target);
            return tweener;
        }

        /// <summary>
        /// Controls the amount of blur applied to the outline
        /// <summary>
        /// Creates a tweener that animates the outline's BlurShift property to the specified value over the given duration.
        /// </summary>
        /// <param name="target">The outline properties whose BlurShift will be animated.</param>
        /// <param name="endValue">The target BlurShift value at the end of the tween.</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <param name="snapping">If true, values will be rounded to integers while animating.</param>
        /// <returns>A <see cref="TweenerCore{T1,T2,TPlugOptions}"/> that animates the BlurShift from its current value to <paramref name="endValue"/>.</returns>
        public static TweenerCore<float, float, FloatOptions> DOBlurShift(this Outlinable.OutlineProperties target, float endValue, float duration, bool snapping = false)
        {
            var tweener = DOTween.To(() => target.BlurShift, x => target.BlurShift = x, endValue, duration);
            tweener.SetOptions(snapping).SetTarget(target);
            return tweener;
        }

        /// <summary>
        /// Controls the amount of blur applied to the outline
        /// <summary>
        /// Animates the Outliner's BlurShift property to the specified value over the given duration.
        /// </summary>
        /// <param name="target">The Outliner whose BlurShift will be animated.</param>
        /// <param name="endValue">The target BlurShift value.</param>
        /// <param name="duration">The duration of the animation in seconds.</param>
        /// <param name="snapping">Whether to snap values to whole integers while tweening.</param>
        /// <returns>A Tweener configured to animate the Outliner's BlurShift from its current value to <paramref name="endValue"/>.</returns>
        public static TweenerCore<float, float, FloatOptions> DOBlurShift(this Outliner target, float endValue, float duration, bool snapping = false)
        {
            var tweener = DOTween.To(() => target.BlurShift, x => target.BlurShift = x, endValue, duration);
            tweener.SetOptions(snapping).SetTarget(target);
            return tweener;
        }

        /// <summary>
        /// Controls the amount of dilation applied to the outline
        /// <summary>
        /// Animates the outline's DilateShift from its current value to the specified end value.
        /// </summary>
        /// <param name="target">The outline properties to animate.</param>
        /// <param name="endValue">The target DilateShift value at the end of the tween.</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <param name="snapping">If true, values will be snapped to integers during the tween.</param>
        /// <returns>A TweenerCore that animates the DilateShift value from its current value to <paramref name="endValue"/>.</returns>
        public static TweenerCore<float, float, FloatOptions> DODilateShift(this Outlinable.OutlineProperties target, float endValue, float duration, bool snapping = false)
        {
            var tweener = DOTween.To(() => target.DilateShift, x => target.DilateShift = x, endValue, duration);
            tweener.SetOptions(snapping).SetTarget(target);
            return tweener;
        }

        /// <summary>
        /// Controls the amount of dilation applied to the outline
        /// <summary>
        /// Animates the Outliner's DilateShift property to the specified value over the given duration.
        /// </summary>
        /// <param name="snapping">If true, values will be snapped to whole integers during the tween.</param>
        /// <returns>A TweenerCore configured to animate the DilateShift value.</returns>
        public static TweenerCore<float, float, FloatOptions> DODilateShift(this Outliner target, float endValue, float duration, bool snapping = false)
        {
            var tweener = DOTween.To(() => target.DilateShift, x => target.DilateShift = x, endValue, duration);
            tweener.SetOptions(snapping).SetTarget(target);
            return tweener;
        }
    }
}
#endif