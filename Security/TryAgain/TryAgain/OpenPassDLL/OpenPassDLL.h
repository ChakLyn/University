#pragma once
// Macro for DLL exports in Win32, replaces Win16 __export 
// (Макрос для экспорта DLL в Win32 вместо 16-битной версии) 
#define DllExport extern "C" __declspec(dllexport)

// Prototype 
// (Прототип) 
DllExport void RunStopHook(bool State, HINSTANCE hInstance);