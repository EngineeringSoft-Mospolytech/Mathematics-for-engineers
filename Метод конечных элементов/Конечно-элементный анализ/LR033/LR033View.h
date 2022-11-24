
// LR033View.h: интерфейс класса CLR033View
//

#pragma once


class CLR033View : public CView
{
protected: // создать только из сериализации
	CLR033View() noexcept;
	DECLARE_DYNCREATE(CLR033View)

// Атрибуты
public:
	CLR033Doc* GetDocument() const;

// Операции
public:

	float malfx, malfy;
	int jx, jy;
	int iflag;
	int ipline = 0; // 0 - не рисуем линии; 1 - рисуем линии

protected: 

	BOOL bSetupPixelFormat(HDC hdc);
	BOOL CreateGLContext(HDC hdc);
	CClientDC* m_pDC;

// Переопределение
public:
	virtual void OnDraw(CDC* pDC);  // переопределено для отрисовки этого представления
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
protected:
	virtual BOOL OnPreparePrinting(CPrintInfo* pInfo);
	virtual void OnBeginPrinting(CDC* pDC, CPrintInfo* pInfo);
	virtual void OnEndPrinting(CDC* pDC, CPrintInfo* pInfo);

// Реализация
public:
	virtual ~CLR033View();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// Созданные функции схемы сообщений
protected:
	DECLARE_MESSAGE_MAP()
public:
	afx_msg int OnCreate(LPCREATESTRUCT lpCreateStruct);
	afx_msg void OnDestroy();
	afx_msg BOOL OnEraseBkgnd(CDC* pDC);
	virtual void OnInitialUpdate();
	afx_msg void OnMButtonDown(UINT nFlags, CPoint point);
	afx_msg void OnMButtonUp(UINT nFlags, CPoint point);
	afx_msg void OnMouseMove(UINT nFlags, CPoint point);
	afx_msg BOOL OnMouseWheel(UINT nFlags, short zDelta, CPoint pt);
	afx_msg void OnMouseLeave();
	afx_msg void OnButLin();
	afx_msg void OnButDeform();
};

#ifndef _DEBUG  // версия отладки в LR033View.cpp
inline CLR033Doc* CLR033View::GetDocument() const
   { return reinterpret_cast<CLR033Doc*>(m_pDocument); }
#endif

