#pragma once
// Macro for DLL exports in Win32, replaces Win16 __export 
// (������ ��� �������� DLL � Win32 ������ 16-������ ������) 
#define DllExport extern "C" __declspec(dllexport)

// Prototype 
// (��������) 
DllExport void RunStopHook(bool State, HINSTANCE hInstance);