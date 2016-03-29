#if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.
/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 2.0.11
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */


using System;
using System.Runtime.InteropServices;

public class AkOutputSettings : IDisposable {
  private IntPtr swigCPtr;
  protected bool swigCMemOwn;

  internal AkOutputSettings(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = cPtr;
  }

  internal static IntPtr getCPtr(AkOutputSettings obj) {
    return (obj == null) ? IntPtr.Zero : obj.swigCPtr;
  }

  ~AkOutputSettings() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          AkSoundEnginePINVOKE.CSharp_delete_AkOutputSettings(swigCPtr);
        }
        swigCPtr = IntPtr.Zero;
      }
      GC.SuppressFinalize(this);
    }
  }

  public AkPanningRule ePanningRule {
    set {
      AkSoundEnginePINVOKE.CSharp_AkOutputSettings_ePanningRule_set(swigCPtr, (int)value);

    } 
    get {
      AkPanningRule ret = (AkPanningRule)AkSoundEnginePINVOKE.CSharp_AkOutputSettings_ePanningRule_get(swigCPtr);

      return ret;
    } 
  }

  public AkChannelConfig channelConfig {
    set {
      AkSoundEnginePINVOKE.CSharp_AkOutputSettings_channelConfig_set(swigCPtr, AkChannelConfig.getCPtr(value));

    } 
    get {
      IntPtr cPtr = AkSoundEnginePINVOKE.CSharp_AkOutputSettings_channelConfig_get(swigCPtr);
      AkChannelConfig ret = (cPtr == IntPtr.Zero) ? null : new AkChannelConfig(cPtr, false);

      return ret;
    } 
  }

  public AkOutputSettings() : this(AkSoundEnginePINVOKE.CSharp_new_AkOutputSettings(), true) {

  }

}
#endif // #if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.