
// LR033Doc.h: интерфейс класса CLR033Doc 
//


#pragma once


class CLR033Doc : public CDocument
{
protected: // создать только из сериализации
	CLR033Doc() noexcept;
	DECLARE_DYNCREATE(CLR033Doc)

// Атрибуты
public:

	CString sNameFile;
	CString sFileExt;
	CString sFileTitle;
	CString sPathFile;
	int ncar;
	int maxcar;
	float car[3][150000];
	int klin;
	int maxlin;
	int jt[3][150000];
	float tnormal[3][150000];// нормали для тругольников (x,y,z и количество треугольников)
	int filesost = 0; // открытия файла с перемещениями
	int koor;
	float perenkoor[2][150000];
	double xmin; double xmax; double xmod;
	double ymin; double ymax; double ymod;
	double zmin; double zmax; double zmod;

// Операции
public:
	void normal_formir(); //вычисление нормалей по треуг
	void mashtab_mod();

// Переопределение
public:
	virtual BOOL OnNewDocument();
	virtual void Serialize(CArchive& ar);
#ifdef SHARED_HANDLERS
	virtual void InitializeSearchContent();
	virtual void OnDrawThumbnail(CDC& dc, LPRECT lprcBounds);
#endif // SHARED_HANDLERS

// Реализация
public:
	virtual ~CLR033Doc();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// Созданные функции схемы сообщений
protected:
	DECLARE_MESSAGE_MAP()

#ifdef SHARED_HANDLERS
	// Вспомогательная функция, задающая содержимое поиска для обработчика поиска
	void SetSearchContent(const CString& value);
#endif // SHARED_HANDLERS
public:
	afx_msg void OnFileOpen();
	afx_msg void OnUpdateFileOpen(CCmdUI* pCmdUI);
	afx_msg void OnButPerem();
};
