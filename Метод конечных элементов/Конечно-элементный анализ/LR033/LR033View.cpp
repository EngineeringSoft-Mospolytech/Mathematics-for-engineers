
// LR033View.cpp: реализация класса CLR033View
//

#include "pch.h"
#include "framework.h"
// SHARED_HANDLERS можно определить в обработчиках фильтров просмотра реализации проекта ATL, эскизов
// и поиска; позволяет совместно использовать код документа в данным проекте.
#ifndef SHARED_HANDLERS
#include "LR033.h"
#endif

#include "LR033Doc.h"
#include "LR033View.h"

#include "gl\gl.h"
#include "gl\glu.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CLR033View

IMPLEMENT_DYNCREATE(CLR033View, CView)

BEGIN_MESSAGE_MAP(CLR033View, CView)
	// Стандартные команды печати
	ON_COMMAND(ID_FILE_PRINT, &CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_DIRECT, &CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_PREVIEW, &CView::OnFilePrintPreview)
	ON_WM_CREATE()
	ON_WM_DESTROY()
	ON_WM_ERASEBKGND()
	ON_WM_MBUTTONDOWN()
	ON_WM_MBUTTONUP()
	ON_WM_MOUSEMOVE()
	ON_WM_MOUSEWHEEL()
	ON_WM_MOUSELEAVE()
	ON_COMMAND(ID_BUT_LIN, &CLR033View::OnButLin)
	ON_COMMAND(ID_BUT_DEFORM, &CLR033View::OnButDeform)
END_MESSAGE_MAP()
// Создание или уничтожение CLR033View

CLR033View::CLR033View() noexcept
{
	// TODO: добавьте код создания
	glMatrixMode(GL_MODELVIEW); // устанавливаем в качестве текущей видовую матрицу
	glLoadIdentity(); // устанавливаем единичную видовую матрицу
	glPushMatrix(); //копируем видовую матрицу в стек на сохранение
	malfx = 0.;
	malfy = 0.; // изменения углов поворота модели
	jx = 0; jy = 0;
	iflag = 0;


}

CLR033View::~CLR033View()
{

}

BOOL CLR033View::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: изменить класс Window или стили посредством изменения
	//  CREATESTRUCT cs

	return CView::PreCreateWindow(cs);
}

// Рисование CLR033View

BOOL CLR033View::bSetupPixelFormat(HDC hdc)
{
	static PIXELFORMATDESCRIPTOR pfd = {
sizeof(PIXELFORMATDESCRIPTOR), // размер структуры
1, // номер версии
PFD_DRAW_TO_WINDOW | // поддержка вывода в окно
PFD_SUPPORT_OPENGL,// | // поддержка OpenGL
//PFD_DOUBLEBUFFER, // двойная буферизация
PFD_TYPE_RGBA, // цвета в режиме RGBA
24, // 24-разряда на цвет
0, 0, 0, 0, 0, 0, // биты цвета игнорируются
0, // не используется альфа параметр
0, // смещение цветов игнорируются
0, // буфер аккумулятора не используется
0, 0, 0, 0, // биты аккумулятора игнорируются
32, // 32-разрядный буфер глубины
0, // буфер трафарета не используется
0, // вспомогательный буфер не используется
PFD_MAIN_PLANE, // основной слой
0, // зарезервирован
0, 0, 0 // маски слоя игнорируются
	};
	int pixelFormat;
	// Поддерживает ли система необходимый формат пикселей?
	if ((pixelFormat = ::ChoosePixelFormat(hdc, &pfd)) == 0) {
		::AfxMessageBox(_T("С заданным форматом пикселей работать нельзя"));
		return FALSE;
	}
	if (::SetPixelFormat(hdc, pixelFormat, &pfd) == FALSE)
	{
		::AfxMessageBox(_T("Ошибка при выполнении SetPixelFormat"));
		return FALSE;
	}
	return TRUE;
}

BOOL CLR033View::CreateGLContext(HDC hdc)
{
	HGLRC hrc;
	if ((hrc = ::wglCreateContext(hdc)) == NULL) return FALSE; // Создаем контекст
	// воспроизведения
	if (::wglMakeCurrent(hdc, hrc) == FALSE) return FALSE; // Делаем контекст
	// воспроизведения текущим
	return TRUE;

}

void CLR033View::OnDraw(CDC* pDC)
{
	CLR033Doc* pDoc = GetDocument();// Указатель на документ данного приложения
	ASSERT_VALID(pDoc);
	if (!pDoc)
		return;
	CRect rect;
	int x1, y1, x2, y2;
	GetClientRect(rect); // rect прямоугольная область в координатах клиентской области окна
	x1 = rect.left; y1 = rect.top;
	x2 = rect.right; y2 = rect.bottom;
	int m_width, m_height;
	m_width = x2 - x1; // ширина клиентской области окна в пикселях
	m_height = y2 - y1; // высота клиентской области окна в пикселях
	double xc = (pDoc->xmax + pDoc->xmin) / 2.; // центр модели по кооржинате x
	double yc = (pDoc->ymax + pDoc->ymin) / 2.; // центр модели по кооржинате y
	double zc = (pDoc->zmax + pDoc->zmin) / 2.; // центр модели по кооржинате z
	double aspekt = (double)m_height / (double)m_width; // соотношение ширины и высоты экрана
	glViewport(0, 0, m_width, m_height); // область вывода OpenGL в пикселях
	glMatrixMode(GL_PROJECTION); // устанавливаем в качестве текущей матрицу проецирования
	glLoadIdentity(); // устанавливаем единичную матрицу проецирования
	glOrtho(-1.5*pDoc->xmod, 1.5* pDoc->xmod, -1.5* pDoc->xmod * aspekt, 1.5* pDoc->xmod * aspekt, -100* pDoc->zmod, 100* pDoc->zmod);// объем видимости ax * by - ay * bx
	//glOrtho(-1., 1., -0.1 * aspekt, 1.9 * aspekt, -12., 12.);// объем видимости
	 //// для ортографической проекции

	glMatrixMode(GL_MODELVIEW); // устанавливаем в качестве текущей видовую матрицу
	//--------------------------------------------------------------------------
	//glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);// Очищаем буферы цвета и глубины
	//--------------------------------------------------------------
	//===========================================================================
	// источник света 0
	float light0_specular[4] = { 0.8f, 0.8f, .8f, 1.f };//свет зеркального отражения RGBA
		float light0_ambient[4] = { 0.f, 0.f, 0.f, 1.0f };//фоновый свет RGBA
	float light0_diffuse[4] = { 0.1f, 0.0f, 0.0f, 1.0f };//диффузионный свет RGBA
	//
	float light0_direction[3] = { 0.f, 0.f, -1.f };//направление источника света XYZ
	float light0_position[4] = { 0.06f, 0.5f, 0.86, 1.0e-4 };//позиция источника света XYZA
		//
		glLightf(GL_LIGHT0, GL_SPOT_CUTOFF, 360.f);//угол распространения света
	glLightf(GL_LIGHT0, GL_SPOT_EXPONENT, 0.f);//однородное пространственное распространение
		glLightf(GL_LIGHT0, GL_CONSTANT_ATTENUATION, 1.f); // Затухание постоянное
	glLightf(GL_LIGHT0, GL_LINEAR_ATTENUATION, 0.f); // Затухание линейное
	glLightf(GL_LIGHT0, GL_QUADRATIC_ATTENUATION, 0.f);//Затухание квадратичное
	//
	glLightfv(GL_LIGHT0, GL_SPOT_DIRECTION, light0_direction); //направление источника света
		glLightfv(GL_LIGHT0, GL_POSITION, light0_position); //позиция источника света
	glLightfv(GL_LIGHT0, GL_DIFFUSE, light0_diffuse);//диффузионный свет
	//===========================================================================
   // источник света 1
	float light1_specular[4] = { 0.1f, .5f, 0.3f, 1.0f };//зеркальное отражение RGBA
	float light1_ambient[4] = { 0.f, 0.f, 0.f, 1.0f };//фоновый свет RGBA
	float light1_diffuse[4] = { 0.f, 0.f, 0.f, 1.0f };//диффузионный свет RGBA
	//
	float light1_direction[3] = { 0.f, 0.f, 1.0f }; //направление источника света XYZ
	float light1_position[4] = { -0.6f, -0.4f, -1.0f, 1.0e-3 };//позиция источника света XYZA
		//
		glLightf(GL_LIGHT1, GL_SPOT_CUTOFF, 120.f);//угол распространения света
	glLightf(GL_LIGHT1, GL_SPOT_EXPONENT, 0.f);//однородное пространственное распространение
		glLightf(GL_LIGHT1, GL_CONSTANT_ATTENUATION, 1.f);// Затухание постоянное
	glLightf(GL_LIGHT1, GL_LINEAR_ATTENUATION, 0.f); // Затухание линейное
	glLightf(GL_LIGHT1, GL_QUADRATIC_ATTENUATION, 0.f);//Затухание квадратичное
	//
	glLightfv(GL_LIGHT1, GL_SPOT_DIRECTION, light1_direction);//направление источника света
		glLightfv(GL_LIGHT1, GL_POSITION, light1_position); //позиция источника света
	glLightfv(GL_LIGHT1, GL_SPECULAR, light1_specular);//зеркальное отражение
	glLightfv(GL_LIGHT1, GL_AMBIENT, light1_ambient);//фоновый свет
	glLightfv(GL_LIGHT1, GL_DIFFUSE, light1_diffuse);//диффузионный свет
	//===================================================================
	// параметры модели сцены
	 //--------------------------------------------------------------
	 //Материалы. Оптические свойства
	GLfloat mat_specular[4] = { 0.7f, 0.56f, 1.f, 1.f }; //зеркальное отражение RGBA
	GLfloat mat_ambient[4] = { 0.0f, 0.0f, 0.0f, 1.f }; //фоновый свет RGBA
	GLfloat mat_diffuse[4] = { 0.0f, 0.0f, 0.0f, 1.0f };//диффузионный свет RGBA
	GLfloat mat_emission[4] = { 0.16f, 0.16f, 0.16f, 1.0f };//эмиссионный свет RGBA
	//
	glMaterialfv(GL_FRONT_AND_BACK, GL_SPECULAR, mat_specular);//зеркальное отражение
	glMaterialfv(GL_FRONT_AND_BACK, GL_AMBIENT, mat_ambient); //фоновый свет
	glMaterialfv(GL_FRONT_AND_BACK, GL_DIFFUSE, mat_diffuse); //диффузионный свет
	glMaterialfv(GL_FRONT_AND_BACK, GL_EMISSION, mat_emission);//эмиссионный свет
	glMaterialf(GL_FRONT_AND_BACK, GL_SHININESS, 2.f); // затухание зеркального отражения
	//
	// модели освещенности
	float lmodel_ambient[4] = { .1f, 0.2f, 0.2f, 1.0f }; // интенсивность фонового цвета RGBA
		float lmodel_tek[4] = { 0.0f, 0.5f, 0.5f, 1.0f }; // текущий цвет RGBA
	float clear_color[4] = { 0.88f, 0.88f, 1.f, 1.0f }; // цвет фона RGBA
	//
	glColor4f(lmodel_tek[0], lmodel_tek[1], lmodel_tek[2], lmodel_tek[3]); // текущий цвец // очистка основного буфера цвета и буфера глубины
	glClearColor(clear_color[0], clear_color[1], clear_color[2], clear_color[3]);
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	glEnable(GL_COLOR_MATERIAL); //
	glColorMaterial(GL_FRONT_AND_BACK, GL_AMBIENT_AND_DIFFUSE);
	//
   // glLightModelfv(GL_LIGHT_MODEL_AMBIENT, lmodel_ambient); //интенсивность фонового света
	//
	glLightModelf(GL_LIGHT_MODEL_TWO_SIDE, 1.0f);// расчет освещенности 2-х сторон
	// glLightModelf(GL_LIGHT_MODEL_TWO_SIDE, 0.0f);// расчет освещенности только лицевых сторон
		//
	   // glLightModelf(GL_LIGHT_MODEL_LOCAL_VIEWER, 1.0f); // наблюдатель со стороны -z в точке(0, 0, бесконечность)
		glLightModelf(GL_LIGHT_MODEL_LOCAL_VIEWER, 0.0f); // наблюдатель в начале видовой системы 

	glEnable(GL_DEPTH_TEST); // включаем тест глубины
	// glDisable(GL_DEPTH_TEST); //выключаем тест глубины
	glDepthFunc(GL_LESS); // выводятся пиксели с наименьшими z - координатами
	// glDepthFunc(GL_GREATER); // выводятся пиксели с наибольшими z - координатами
	// glDepthFunc(GL_ALWAYS); // выводятся пиксели всегда
	//===========================================================================
	glEnable(GL_LIGHTING); // включаем расчет освещенности
	//glDisable(GL_LIGHTING); //выключаем расчет освещенности
	glEnable(GL_LIGHT0); // включаем источник света GL_LIGHT0
	// glDisable(GL_LIGHT0); // выключаем источник света GL_LIGHT0
	glEnable(GL_LIGHT1); // включаем источник света GL_LIGHT1
	// glDisable(GL_LIGHT1); // выключаем источник света GL_LIGHT1
   //===========================================================================
	glLineWidth(1.0f); // толщина линий
	glHint(GL_LINE_SMOOTH_HINT, GL_NICEST); // вывод линий с наивысшим качеством
	// GL_DONT_CARE );
	// GL_FASTEST );
	glEnable(GL_LINE_SMOOTH); // сглаживание линий
	// glDisable(GL_LINE_SMOOTH); // сглаживание линий
	glHint(GL_POLYGON_SMOOTH_HINT, GL_NICEST); // вывод областей с наивысшим качеством
	//glHint(GL_POLYGON_SMOOTH_HINT,GL_DONT_CARE); // вывод областей со средним качеством
	//
	//glHint(GL_POLYGON_SMOOTH_HINT,GL_FASTEST); // вывод областей быстро
	glEnable(GL_POLYGON_SMOOTH); // сглаживание областей
	//
	glPolygonMode(GL_FRONT, GL_FILL); // Фронт. Закрашенный полигон
	//glPolygonMode(GL_FRONT, GL_LINE); // Фронт. Контурные линии полигона
	//glPolygonMode(GL_FRONT, GL_POINT);// Фронт. Точки полигона
	//
	glPolygonMode(GL_BACK, GL_FILL); // Обратная сторона. Закрашенный полигон
	//glPolygonMode(GL_BACK, GL_LINE); // Обратная сторона. Контурные линии полигона
	//glPolygonMode(GL_BACK, GL_POINT);// Обратная сторона. Точки полигона
	glShadeModel(GL_SMOOTH); // интерполяция цветов
	// glShadeModel(GL_FLAT); // нет интерполяции цветов
	 // Эффект смешивания цветов
	 //
	int sfactor = GL_ONE, dfactor = GL_ZERO; // s-источник, d-точка назначения
	//---------Режимы смешивания цветов--------------
	//sfactor = GL_ZERO;
	//sfactor = GL_ONE;
	//sfactor = GL_DST_COLOR;
	//sfactor = GL_ONE_MINUS_DST_COLOR;
	//sfactor = GL_SRC_ALPHA;
	//sfactor = GL_ONE_MINUS_SRC_ALPHA;
	//sfactor = GL_DST_ALPHA;
	//sfactor = GL_ONE_MINUS_DST_ALPHA;
	//sfactor = GL_SRC_ALPHA_SATURATE;
	//
	//dfactor = GL_ZERO;
	//dfactor = GL_ONE;
	//dfactor = GL_SRC_COLOR;
	//dfactor = GL_ONE_MINUS_SRC_COLOR;
	//dfactor = GL_SRC_ALPHA;
	//dfactor = GL_ONE_MINUS_SRC_ALPHA;
	//dfactor = GL_DST_ALPHA;
 //dfactor = GL_ONE_MINUS_DST_ALPHA;
 //
	glBlendFunc(sfactor, dfactor); // режим смешивания цветов
	glEnable(GL_BLEND); // включаем тест смешивания цветов
	//glDisable(GL_BLEND); //выключаем тест смешивания цветов
	//
	glEnable(GL_NORMALIZE); // нормали единичной длинны
	//====== прорисовка модели с учетом заданных режимов освещенности ============
	glTranslatef(xc, yc, zc); // Доп. смещение в центр
	glRotated(malfx, 1., 0., 0.);// Доп. поворот шестигранника на угол alf относит. X
	glRotated(malfy, 0., 1., 0.);// Доп. поворот шестигранника на угол malf относит. Y
	glTranslatef(-xc, -yc, -zc); // Обратное смещение
	glBegin(GL_TRIANGLES); // Начало рисования треугольных элементов // GL_QUADS
	for (int j = 0; j < pDoc->klin; j++) { // цикл по элементам. j-номер элемента
		glColor3d(0.1, 1.0, 0.80); // RGB цвет элемента 1 - синий
		//
		glNormal3d(pDoc->tnormal[0][j], pDoc->tnormal[1][j], pDoc->tnormal[2][j]); // нормаль к грани
			for (int i = 0; i < 3; i++)//Цикл по точкам (узлам) текущего элемента j. i- номер узла
			{ // Для каждого элемента надо задать координаты 3-х узлов
				int iver; iver = pDoc->jt[i][j] - 1; // iver- номер узла для элемента j в списке
				// координат car
				glVertex3d(pDoc->car[0][iver], pDoc->car[1][iver], pDoc->car[2][iver]); // задаем
				// координаты X,Y,Z узла
			} // Конец цикла по узлам текущего элемента
	} // Конец цикла по элементам
	glEnd(); // Конец рисования закрашенных треугольников
	//===========================================================================
	if (ipline == 1) {

		glDisable(GL_LIGHTING); //выключаем расчет освещенности
		////===========================================================================
		//
		glBegin(GL_LINES); // Начало рисования линий по контуру треугольников
		for (int j = 0; j < pDoc->klin; j++) { // цикл по элементам. j-номер элемента
			glColor3d(0.0, 0.0, 0.0); // RGB цвет линии - белый
			for (int i = 0; i < 3; i++)//Цикл по линиям текущего элемента j. i- номер линии
			{ // Для каждой линии надо задать координаты 2-х узлов
				int iver; iver = pDoc->jt[i][j] - 1; //iver- номер точки (узла) элемента j в списке
				//координат car
				glVertex3d(pDoc->car[0][iver], pDoc->car[1][iver], pDoc->car[2][iver]); // задаем
				//координаты X,Y,Z точки (узла)
				if (i < 2) iver = pDoc->jt[i + 1][j] - 1; // iver- номер точки (узла) у элемента j в
				// списке координат car
				if (i == 2) iver = pDoc->jt[0][j] - 1; // iver- номер точки (узла) у элемента j в
				// списке координат car
				glVertex3d(pDoc->car[0][iver], pDoc->car[1][iver], pDoc->car[2][iver]); // задаем
				// координаты X,Y,Z точки (узла)
			} // Конец цикла по линиям текущего элемента
		} // Конец цикла по элементам
		glEnd(); // Конец рисования линий
	} // Конец рисования линий
	//===========================================================================
	glFlush(); // Вывод изображения из буфера на экран
	//--------------------------------------------------------------
}


// Печать CLR033View

BOOL CLR033View::OnPreparePrinting(CPrintInfo* pInfo)
{
	// подготовка по умолчанию
	return DoPreparePrinting(pInfo);
}

void CLR033View::OnBeginPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: добавьте дополнительную инициализацию перед печатью
}

void CLR033View::OnEndPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: добавьте очистку после печати
}


// Диагностика CLR033View

#ifdef _DEBUG
void CLR033View::AssertValid() const
{
	CView::AssertValid();
}

void CLR033View::Dump(CDumpContext& dc) const
{
	CView::Dump(dc);
}

CLR033Doc* CLR033View::GetDocument() const // встроена неотлаженная версия
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CLR033Doc)));
	return (CLR033Doc*)m_pDocument;
}
#endif //_DEBUG


// Обработчики сообщений CLR033View


int CLR033View::OnCreate(LPCREATESTRUCT lpCreateStruct)
{
	if (CView::OnCreate(lpCreateStruct) == -1)
		return -1;

	// TODO:  Добавьте специализированный код создания
	m_pDC = new CClientDC(this);
	ASSERT(m_pDC != NULL);
	HDC hdc = m_pDC->GetSafeHdc();
	if (bSetupPixelFormat(hdc) == FALSE) return -1;
	if (CreateGLContext(hdc) == FALSE) return -1;

	return 0;
}


void CLR033View::OnDestroy()
{
	CView::OnDestroy();
	HGLRC hrc = wglGetCurrentContext();
	if (hrc) wglDeleteContext(hrc);
	if (m_pDC) delete m_pDC;
	CView::OnDestroy();

	// TODO: добавьте свой код обработчика сообщений
}


BOOL CLR033View::OnEraseBkgnd(CDC* pDC)
{
	// TODO: добавьте свой код обработчика сообщений или вызов стандартного

	return 0;
}


void CLR033View::OnInitialUpdate()
{
	CView::OnInitialUpdate();

	// TODO: добавьте специализированный код или вызов базового класса
	glClearColor(0.8f, 0.8f, 0.8f, 1.0f);

}



void CLR033View::OnMButtonDown(UINT nFlags, CPoint point)
{
	// TODO: добавьте свой код обработчика сообщений или вызов стандартного

	if ((nFlags & MK_CONTROL) == MK_CONTROL) {

		iflag = 2;
	}
	else if (iflag == 0) {
	
		iflag = 1;
		malfx = 0.;
		malfy = 0.;
	}
	CView::OnMButtonDown(nFlags, point);
}


void CLR033View::OnMButtonUp(UINT nFlags, CPoint point)
{
	// TODO: добавьте свой код обработчика сообщений или вызов стандартного

	iflag = 0;
	malfx = 0.;
	malfy = 0.;

	CView::OnMButtonUp(nFlags, point);
}


void CLR033View::OnMouseMove(UINT nFlags, CPoint point)
{
	// TODO: добавьте свой код обработчика сообщений или вызов стандартного

	int ix, iy;
	double dx, dy;
	if (iflag == 1) {
		ix = point.x;
		iy = point.y;
		malfx = 0.; if (iy - jy > 0) malfx = 0.5f; else if (iy - jy < 0) malfx = -0.5f;
		malfy = 0.; if (ix - jx > 0) malfy = 0.5f; else if (ix - jx < 0) malfy = -0.5f;
		jx = ix;
		jy = iy;

		CRect rect;
		GetClientRect(rect);
		InvalidateRect(&rect);

 	}

	if (iflag == 2) {
		ix = point.x;
		iy = point.y;
		malfx = 0.; malfy = 0.; dx = 0.; dy = 0.;
		if (ix - jx > 0) dx = -10.f; else if (ix - jx < 0) dx = 10.f;
		if (iy - jy > 0) dy = 10.f; else if (iy - jy < 0) dy = -10.f;
		glTranslated(dx, dy, 0.0);
		jx = ix;
		jy = iy;

		CRect rect;
		GetClientRect(rect);
		InvalidateRect(&rect);

	}

	TRACKMOUSEEVENT tme;
	tme.cbSize = sizeof(tme);
	tme.hwndTrack = GetSafeHwnd();
	tme.dwFlags = TME_LEAVE;
	_TrackMouseEvent(&tme);

	CView::OnMouseMove(nFlags, point);
}


BOOL CLR033View::OnMouseWheel(UINT nFlags, short zDelta, CPoint pt)
{
	// TODO: добавьте свой код обработчика сообщений или вызов стандартного

	if (zDelta > 0) glScaled(1.25, 1.25, 1.25);
	if (zDelta < 0) glScaled(0.75, 0.75, 0.75);

	CRect rect;
	GetClientRect(rect);
	InvalidateRect(&rect);

	return CView::OnMouseWheel(nFlags, zDelta, pt);
}


void CLR033View::OnMouseLeave()
{
	// TODO: добавьте свой код обработчика сообщений или вызов стандартного

	iflag = 0;
	malfx = 0.;
	malfy = 0.;

	CView::OnMouseLeave();
}


void CLR033View::OnButLin()
{
	// TODO: добавьте свой код обработчика команд
	if (ipline == 1) ipline = 0;
	else if (ipline == 0) ipline = 1;
	CClientDC dc(this);
	CRect rect;
	GetClientRect(rect);
	InvalidateRect(&rect);

}


void CLR033View::OnButDeform()
{
	// TODO: добавьте свой код обработчика команд
}
