﻿using Assets.Scripts.Utility;
using UnityEngine;
using static Assets.Scripts.Effects.RagnarokEffectData;

namespace Assets.Scripts.Effects.PrimitiveHandlers
{
    [RoPrimitive("Casting3D")]
    public class CastingCylinderPrimitive : IPrimitiveHandler
    {
        public PrimitiveUpdateDelegate GetDefaultUpdateHandler() => Update3DCasting;
        public PrimitiveRenderDelegate GetDefaultRenderHandler() => Render3DCasting;
        
        //used in main elemental casting
        public static void Update3DCasting(RagnarokPrimitive primitive)
        {
            var step = primitive.Step;

            var mid = (EffectPart.SegmentCount - 1) / 2;
            var m2 = 90 / mid;
            
            for (var ec = 0; ec < primitive.Parts.Length; ec++)
            {
                //if (Mathf.Approximately(size, 3f) && ec < 3)
                //    ec = 3;

                var p = primitive.Parts[ec];

                if (p.Active)
                {
                    if (ec < 3)
                        p.Angle += (ec + 3) * 60 * Time.deltaTime;
                    else
                        p.Angle += 0.2f * 60 * Time.deltaTime;

                    if (p.Angle > 360)
                        p.Angle -= 360;

                    if (step > primitive.Duration * 60 - 40)
                    {
                        if (ec < 3)
                            p.Alpha -= 5 * 60 * Time.deltaTime;
                        else
                            p.Alpha -= 3 * 60 * Time.deltaTime;
                    }
                }
                else if (step < 20)
                {
                    if (p.AlphaTime != 1)
                    {
                        if (ec == 3)
                            p.Alpha = Mathf.Clamp(p.Alpha + 8 * 60 * Time.deltaTime, 0, 60);
                        else
                            p.Alpha = Mathf.Clamp(p.Alpha + 10 * 60 * Time.deltaTime, 0, 180);
                    }
                }

                for (var i = 0; i < EffectPart.SegmentCount; i++)
                {
                    if (p.Flags[i] == 0)
                    {
                        var sinLimit = 90 + ((i - mid) * m2);

                        if (primitive.Step <= 90)
                            p.Heights[i] = p.MaxHeight * Mathf.Sin(sinLimit * Mathf.Deg2Rad) * Mathf.Sin(primitive.CurrentPos * 60 * Mathf.Deg2Rad);

                        if (p.Heights[i] > p.MaxHeight * Mathf.Sin(sinLimit * Mathf.Deg2Rad))
                        {
                            p.Heights[i] = p.MaxHeight * Mathf.Sin(sinLimit * Mathf.Deg2Rad);
                            p.Flags[i] = 1;
                        }

                        if (p.Heights[i] < 0)
                            p.Heights[i] = 0;
                    }
                }
            }

            primitive.IsDirty = true;
        }
        
        
        //used for warp portal effect
        public static void Update3DCasting4(RagnarokPrimitive primitive)
        {
            var mid = (EffectPart.SegmentCount - 1) / 2;
            var m2 = 90 / mid;

            for (var ec = 0; ec < primitive.Parts.Length; ec++)
            {
                var p = primitive.Parts[ec];

                if (!p.Active)
                    continue;

                p.Distance -= 0.05f * 60 * Time.deltaTime;
                if (p.Distance <= 0)
                {
                    p.Distance += 10f;
                    p.Alpha = 0f;
                }

                p.RiseAngle = (90f - p.Distance * 9f);
                p.MaxHeight = p.Distance;

                if (primitive.Step > primitive.Duration * 60 + 40)
                {
                    p.Alpha -= 3 * 60 * Time.deltaTime;
                    if (p.Alpha < 0)
                        p.Alpha = 0;
                }
                else if (p.Alpha < 70)
                {
                    p.Alpha += 1 * 60 * Time.deltaTime;
                }


                for (var i = 0; i < EffectPart.SegmentCount; i++)
                {
                    if (p.Flags[i] == 0)
                        p.Heights[i] = p.MaxHeight;
                }
            }
            
            primitive.IsDirty = true;
        }
        
        
        //used in main elemental casting
        public static void UpdateMapPillar(RagnarokPrimitive primitive)
        {
            var step = primitive.Step;

            var mid = (EffectPart.SegmentCount - 1) / 2;
            var m2 = 90 / mid;
            
            for (var ec = 0; ec < primitive.Parts.Length; ec++)
            {
                //if (Mathf.Approximately(size, 3f) && ec < 3)
                //    ec = 3;

                var p = primitive.Parts[ec];

                if (!p.Active)
                    continue;

                if (primitive.IsStepFrame)
                    p.Step++;

                p.Angle += 60 * Time.deltaTime;

                if (p.Step >= 800)
                {
                    p.MaxHeight -= 60f * Time.deltaTime;
                    if (p.MaxHeight < 0)
                        p.MaxHeight = 0;
                }

                for (var i = 0; i < EffectPart.SegmentCount; i++)
                {
                    if (p.Flags[i] == 0)
                    {
                        if (p.Step < 200)
                        {
                            p.Heights[i] = 0;
                            continue;
                        }

                        if (p.Step <= 290)
                            p.Heights[i] = p.MaxHeight * Mathf.Sin((p.Step - 200) * Mathf.Deg2Rad);
                        else
                            p.Heights[i] = p.MaxHeight;
                        
                        if (p.Heights[i] < 0)
                            p.Heights[i] = 0;
                    }
                }
            }

            primitive.IsDirty = true;
        }
        
        
        
        public static void Render3DCasting(RagnarokPrimitive primitive, MeshBuilder mb)
        {
            mb.Clear();

            for (var ec = 0; ec < primitive.Parts.Length; ec++)
            {
                var p = primitive.Parts[ec];
                if (!p.Active)
                    continue;
                
                var baseAngle = p.CoverAngle / (EffectPart.SegmentCount - 1);
                var pos = 0;

                //Debug.Log(baseAngle);

                var bottomLast = Vector3.zero;
                var topLast = Vector3.zero;

                var color = new Color32(p.Color.r, p.Color.g, p.Color.b, (byte)Mathf.Clamp((int)p.Alpha, 0, 255));
                
                for (var i = 0f; i <= p.CoverAngle; i += baseAngle)
                {
                    var angle = i + p.Angle;
                    if (angle > 360)
                        angle -= 360;

                    if (p.CoverAngle >= 359.9f)
                    {
                        if (pos == EffectPart.SegmentCount - 1)
                            angle = p.Angle;
                    }

                    var c1 = Mathf.Cos(angle * Mathf.Deg2Rad);
                    var s1 = Mathf.Sin(angle * Mathf.Deg2Rad);

                    var bottom = new Vector3(c1 * p.Distance, 0f, s1 * p.Distance);

                    //if (ec == 3)
                    //    bottom.y = -2f;

                    var rx = Mathf.Cos(p.RiseAngle * Mathf.Deg2Rad) * p.Heights[pos];
                    var ry = Mathf.Sin(p.RiseAngle * Mathf.Deg2Rad) * p.Heights[pos];

                    var rz = s1 * rx;
                    rx = c1 * rx;

                    var top = new Vector3(bottom.x + rx, ry, bottom.z + rz);

                    if (pos > 0)
                    {
                        primitive.AddTexturedSliceQuad(topLast, top, bottomLast, bottom, pos, EffectPart.SegmentCount, color, 0.1f);
                    }

                    pos++;
                    if (pos >= EffectPart.SegmentCount)
                        pos = EffectPart.SegmentCount - 1;

                    bottomLast = bottom;
                    topLast = top;
                }
            }
        }
    }
}