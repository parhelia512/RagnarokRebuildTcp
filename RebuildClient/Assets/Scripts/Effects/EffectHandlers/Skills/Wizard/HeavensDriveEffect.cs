﻿using System;
using Assets.Scripts.Effects.PrimitiveData;
using Assets.Scripts.Objects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Effects.EffectHandlers.Skills
{
    [RoEffect("HeavensDrive")]
    public class HeavensDriveEffect : IEffectHandler
    {
        public static Ragnarok3dEffect Create(Vector2Int tilePosition, Span<bool> mask, float startTime)
        {
            var effect = RagnarokEffectPool.Get3dEffect(EffectType.HeavensDrive);
            effect.FollowTarget = null;
            effect.DestroyOnTargetLost = false;
            effect.SetDurationByTime(4.2f);
            effect.UpdateOnlyOnFrameChange = true;
            effect.transform.position = Vector3.zero; //each child will be placed separately
            effect.transform.localScale = new Vector3(1f, 1f, 1f);
            effect.Material = EffectSharedMaterialManager.GetMaterial(EffectMaterialType.StoneMaterial);
            effect.ActiveDelay = startTime;

            // var len = 4f;

            //position.SnapToWorldHeight()

            if (mask.Length < 25)
                throw new Exception($"Effect mask for HeavensDrive is too small! Expecting 5x5 masked area.");

            for (var x1 = 0; x1 < 5; x1++)
            {
                for (var y1 = 0; y1 < 5; y1++)
                {
                    var x = x1 - 2;
                    var y = y1 - 2;

                    if (!mask[x1 + y1 * 5])
                        continue;

                    var pos = new Vector2Int(x + tilePosition.x, y + tilePosition.y).ToWorldPosition();

                    var rndLen = Random.Range(0, 30);

                    //spawn a new spike
                    var prim = effect.LaunchPrimitive(PrimitiveType.Spike3D, effect.Material, 3.2f + (rndLen / 60f));
                    var data = prim.GetPrimitiveData<Spike3DData>();

                    prim.transform.position = new Vector3(pos.x, pos.y - 2f, pos.z);
                    prim.transform.localScale = Vector3.one;
                    prim.transform.localRotation = Quaternion.Euler(0, Random.Range(0, 360), Random.Range(0, 10));
                    // prim.DelayTime = startTime;

                    data.Height = Random.Range(10f, 16f) / 5f;
                    data.Size = Random.Range(3f, 3.5f) / 5f;
                    data.Alpha = 255;
                    data.AlphaSpeed = 0;
                    data.AlphaMax = 255;
                    data.Speed = 1f / 5f;
                    data.Acceleration = 0.01f;
                    data.Flags = Spike3DFlags.SpeedLimit | Spike3DFlags.ReturnDown;
                    data.StopStep = 14;
                    data.ChangeStep = 11;
                    data.ChangeSpeed = -1.2f / 5f;
                    data.ChangeAccel = 0;
                    data.ReturnStep = 180 + rndLen;
                    data.ReturnSpeed = -1.2f / 5f;
                    data.ReturnAccel = 0;
                    data.FadeOutLength = 0.167f;
                }
            }

            return effect;
        }

        public bool Update(Ragnarok3dEffect effect, float pos, int step)
        {
            if (step == 0 && effect.IsStepFrame)
            {
                AudioManager.Instance.OneShotSoundEffect(-1, "wizard_earthspike.ogg", effect.transform.position, 0.7f);
                CameraFollower.Instance.ShakeTime = 0.3f;
            }
            
            return effect.IsTimerActive;
        }
    }
}