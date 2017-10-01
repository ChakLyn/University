// OpenPassDLL.cpp: определяет экспортированные функции для приложения DLL.

// OpenPassDLL.cpp : Defines the entry point for the DLL application 
// OpenPassDLL.cpp : Определяет точку входа для DLL-приложение
#include <windows.h> 
#include "stdafx.h" 
#include "OpenPassDLL.h"
HHOOK SysHook;
HWND Wnd;
HINSTANCE hInst;

BOOL APIENTRY DllMain(HANDLE hModule,
	DWORD  ul_reason_for_call,
	LPVOID lpReserved)
{
	hInst = (HINSTANCE)hModule;
	return TRUE;
}

LRESULT CALLBACK SysMsgProc(
	int code, // hook code (код ловушки) 
	WPARAM wParam, // removal flag (флаг)
	LPARAM lParam // address of structure with message
				  // (адрес структуры с сообщением) 
)
{
	//Передать сообщение другим ловушкам в системе 
	CallNextHookEx(SysHook, code, wParam, lParam);
	//Проверяю сообщение
	if (code == HC_ACTION) {
		//Получаю идентификатор окна сгенерировавшего сообщение
		Wnd = ((tagMSG*)lParam)->hwnd;
		//Проверяю тип сообщения.
		//Если была нажата левая кнопка мыши
		if (((tagMSG*)lParam)->message == WM_RBUTTONDOWN)
		{
			SendMessage(Wnd, EM_SETPASSWORDCHAR, 0, 0);
			InvalidateRect(Wnd, 0, true);
		}
	}
	return 0;
}
///////////////////////////////////////////////////////////////////
DllExport void RunStopHook(bool State, HINSTANCE hInstance)
{
	if (State)
		SysHook = SetWindowsHookEx(WH_GETMESSAGE, &SysMsgProc, hInst, 0);
	else
		UnhookWindowsHookEx(SysHook);
}
