
// LR033.h: основной файл заголовка для приложения LR033
//
#pragma once

#ifndef __AFXWIN_H__
	#error "включить pch.h до включения этого файла в PCH"
#endif

#include "resource.h"       // основные символы


// CLR033App:
// Сведения о реализации этого класса: LR033.cpp
//

class CLR033App : public CWinApp
{
public:
	CLR033App() noexcept;


// Переопределение
public:
	virtual BOOL InitInstance();
	virtual int ExitInstance();

// Реализация
	afx_msg void OnAppAbout();
	DECLARE_MESSAGE_MAP()
};

extern CLR033App theApp;
