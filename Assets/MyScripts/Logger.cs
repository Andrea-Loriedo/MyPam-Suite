#if ENABLE_LOGS
#define LOGGER
#endif

using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngineInternal;
using System.Diagnostics;

// Enables Debug functions based on whether the "ENABLE_LOGS" macro is defined 

public static class Logger {
    
    [System.Diagnostics.Conditional("ENABLE_LOGS")]
    public static void Debug(object message, UnityEngine.Object context = null)
    {   
        UnityEngine.Debug.Log(message, context);
    }

    [System.Diagnostics.Conditional("ENABLE_LOGS")]
    public static void DebugError(object logMsg, UnityEngine.Object context = null) 
    {
        UnityEngine.Debug.LogError(logMsg, context);
    }
}