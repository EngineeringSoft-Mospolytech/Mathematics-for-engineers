
// LR033Doc.cpp: реализация класса CLR033Doc 
//

#include "pch.h"
#include "framework.h"
// SHARED_HANDLERS можно определить в обработчиках фильтров просмотра реализации проекта ATL, эскизов
// и поиска; позволяет совместно использовать код документа в данным проекте.
#ifndef SHARED_HANDLERS
#include "LR033.h"
#endif

#include "LR033Doc.h"

#include <propkey.h>

#include <stdio.h>
#include <stdlib.h>

#include <limits.h>

#include <TCHAR.H>

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

// CLR033Doc

IMPLEMENT_DYNCREATE(CLR033Doc, CDocument)

BEGIN_MESSAGE_MAP(CLR033Doc, CDocument)
	ON_COMMAND(ID_FILE_OPEN, &CLR033Doc::OnFileOpen)
	ON_UPDATE_COMMAND_UI(ID_FILE_OPEN, &CLR033Doc::OnUpdateFileOpen)
	ON_COMMAND(ID_BUT_PEREM, &CLR033Doc::OnButPerem)
END_MESSAGE_MAP()


// Создание или уничтожение CLR033Doc

CLR033Doc::CLR033Doc() noexcept
{
	// TODO: добавьте код для одноразового вызова конструктора
	ncar = 3; //текущее количество точек (3)
	maxcar = 150000; //максимальное количество точек (150000)
	car[0][0] = 0.; car[1][0] = 0.; car[2][0] = 0.; // координаты X Y Z для первой точки
	car[0][1] = 1000.; car[1][1] = 0.; car[2][1] = 0.; // координаты X Y Z для второй точки
	car[0][2] = 1000.; car[1][2] = 1000.; car[2][2] = 0.;// координаты X Y Z для третьей точки
	mashtab_mod();
	klin = 1; //текущее количество элементов (1)
	maxlin = 150000; //максимальное количество элементов (150000)
	jt[0][0] = 1; jt[1][0] = 2; jt[2][0] = 3;// номера 3 точек из массива car, связанных элементом
	tnormal[0][0] = 0.; tnormal[1][0] = 0.; tnormal[2][0] = 1.; // нормаль к элементу
	UpdateAllViews(NULL, 0, this); // перерисовать все виды
}

CLR033Doc::~CLR033Doc()
{
}

void CLR033Doc::normal_formir()
{

	for (int iel = 0; iel < klin; iel++)
	{
		//
		int i1 = jt[0][iel] - 1;
		int i2 = jt[1][iel] - 1;
		int i3 = jt[2][iel] - 1;
		//
		float ax = car[0][i2] - car[0][i1];
		float ay = car[1][i2] - car[1][i1];
		float az = car[2][i2] - car[2][i1];
		//
		float bx = car[0][i3] - car[0][i1];
		float by = car[1][i3] - car[1][i1];
		float bz = car[2][i3] - car[2][i1];
		//
		tnormal[0][iel] = ay * bz - az * by;
		tnormal[1][iel] = az * bx - ax * bz;
		tnormal[2][iel] = ax * by - ay * bx;
	}
	return;
}

void CLR033Doc::mashtab_mod()
{
	xmax = car[0][0]; xmin = car[0][0];
	ymax = car[1][0]; ymin = car[1][0];
	zmin = car[2][0]; zmax = car[2][0];
	for (int i = 0; i < ncar; i++) {
		if (xmin > car[0][i]) xmin = car[0][i];
		if (xmax < car[0][i]) xmax = car[0][i];
	}
	for (int i = 0; i < ncar; i++) {
		if (ymin > car[1][i]) ymin = car[1][i];
		if (ymax < car[1][i]) ymax = car[1][i];
	}
	for (int i = 0; i < ncar; i++) {
		if (zmin > car[1][i]) zmin = car[1][i];
		if (zmax < car[1][i]) zmax = car[1][i];
	}
	if (abs(xmax) > abs(xmin)) xmod = abs(xmax);
		else xmod = abs(xmin);
	if (abs(ymax) > abs(ymin)) ymod = abs(ymax);
	else ymod = abs(ymin);
	if (abs(zmax) > abs(zmin)) zmod = abs(zmax);
	else zmod = abs(zmin);
	return;
}

BOOL CLR033Doc::OnNewDocument()
{
	if (!CDocument::OnNewDocument())
		return FALSE;

	// TODO: добавьте код повторной инициализации
	// (Документы SDI будут повторно использовать этот документ)

	return TRUE;
}




// Сериализация CLR033Doc

void CLR033Doc::Serialize(CArchive& ar)
{
	if (ar.IsStoring())
	{
		// TODO: добавьте код сохранения
	}
	else
	{
		// TODO: добавьте код загрузки
	}
}

#ifdef SHARED_HANDLERS

// Поддержка для эскизов
void CLR033Doc::OnDrawThumbnail(CDC& dc, LPRECT lprcBounds)
{
	// Измените этот код для отображения данных документа
	dc.FillSolidRect(lprcBounds, RGB(255, 255, 255));

	CString strText = _T("TODO: implement thumbnail drawing here");
	LOGFONT lf;

	CFont* pDefaultGUIFont = CFont::FromHandle((HFONT) GetStockObject(DEFAULT_GUI_FONT));
	pDefaultGUIFont->GetLogFont(&lf);
	lf.lfHeight = 36;

	CFont fontDraw;
	fontDraw.CreateFontIndirect(&lf);

	CFont* pOldFont = dc.SelectObject(&fontDraw);
	dc.DrawText(strText, lprcBounds, DT_CENTER | DT_WORDBREAK);
	dc.SelectObject(pOldFont);
}

// Поддержка обработчиков поиска
void CLR033Doc::InitializeSearchContent()
{
	CString strSearchContent;
	// Задание содержимого поиска из данных документа.
	// Части содержимого должны разделяться точкой с запятой ";"

	// Например:  strSearchContent = _T("точка;прямоугольник;круг;объект ole;");
	SetSearchContent(strSearchContent);
}

void CLR033Doc::SetSearchContent(const CString& value)
{
	if (value.IsEmpty())
	{
		RemoveChunk(PKEY_Search_Contents.fmtid, PKEY_Search_Contents.pid);
	}
	else
	{
		CMFCFilterChunkValueImpl *pChunk = nullptr;
		ATLTRY(pChunk = new CMFCFilterChunkValueImpl);
		if (pChunk != nullptr)
		{
			pChunk->SetTextValue(PKEY_Search_Contents, value, CHUNK_TEXT);
			SetChunkValue(pChunk);
		}
	}
}

#endif // SHARED_HANDLERS

// Диагностика CLR033Doc

#ifdef _DEBUG
void CLR033Doc::AssertValid() const
{
	CDocument::AssertValid();
}

void CLR033Doc::Dump(CDumpContext& dc) const
{
	CDocument::Dump(dc);
}
#endif //_DEBUG


// Команды CLR033Doc


void CLR033Doc::OnFileOpen()
{
	// TODO: добавьте свой код обработчика команд
	CFileDialog fileDlg(TRUE);
	CString titl("Считать файл");
	fileDlg.m_ofn.lpstrTitle = titl;
	CString str("Документы txt (*.txt)"); str += (TCHAR)NULL;
	str += "*.txt"; str += (TCHAR)NULL;
	str += "Все файлы (*.*)"; str += (TCHAR)NULL;
	fileDlg.m_ofn.lpstrFilter = str;
	if (fileDlg.DoModal() == IDOK)
	{
		sNameFile = fileDlg.GetFileName();
		sFileExt = fileDlg.GetFileExt();
		sFileTitle = fileDlg.GetFileTitle();
		sPathFile = fileDlg.GetPathName();
		int lenbuf;
		lenbuf = sPathFile.GetLength();
		char nam[_MAX_PATH];
		nam[0] = (TCHAR)NULL;
		int i;
		for (i = 0; i < lenbuf; i++) nam[i] = sPathFile[i]; nam[i] = (TCHAR)NULL;
		FILE* myfile2;
		fopen_s(&myfile2, nam, "rt");
		if (filesost == 0) {
			float x1, y1, z1;
			x1 = 0.; y1 = 0.; z1 = 0.;
			fscanf_s(myfile2, "%d", &ncar);
			if (ncar > 0) for (i = 0; i < ncar; i++) {
				fscanf_s(myfile2, "%f%f%f", &x1, &y1, &z1);
				car[0][i] = x1; car[1][i] = y1; car[2][i] = z1;
			}
			int in, ik, it; in = 0; ik = 0; it = 0;
			fscanf_s(myfile2, "%d", &klin);
			if (klin > 0) for (i = 0; i < klin; i++) {
				fscanf_s(myfile2, "%d%d%d", &in, &ik, &it);
				jt[0][i] = in; jt[1][i] = ik; jt[2][i] = it;
			}
		}
		if (filesost == 1) {
			float x1, y1;
			x1 = 0; y1 = 0;
			fscanf_s(myfile2, "%d", &koor);
			if (koor > 0) {
				for (i = 0; i < koor; i++) {
					fscanf_s(myfile2, "%f%f", &x1, &y1);
					perenkoor[0][i] = x1; perenkoor[1][i] = x1;
				}
			}

		}
		fclose(myfile2);
	}
	UpdateAllViews(NULL, 0, this);
	mashtab_mod();
	normal_formir();
}


void CLR033Doc::OnUpdateFileOpen(CCmdUI* pCmdUI)
{
	// TODO: добавьте свой код обработчика ИП обновления команд

}


void CLR033Doc::OnButPerem()
{
	// TODO: добавьте свой код обработчика команд
	if (filesost == 1) filesost = 0;
	else if (filesost == 0) filesost = 1;
}
