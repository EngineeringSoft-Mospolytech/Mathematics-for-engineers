import PySimpleGUI as sg
import numpy as np
import matplotlib.pyplot as plt
import math
import scipy.integrate as integrate
from cmath import sqrt

sg.theme('Reddit')
layout = [
    [sg.Text('Введите данные')],
    [sg.Text("Данная программа расчитывает количество годных деталей, исправимого и неисправимого брака по ", size=(0, 1))],
    [sg.Text("заданным параметрам. На первом графике - распределение без смещения, на втором - после смещения. ", size=(0, 1))],
    [sg.Text('ES', size=(15, 1)), sg.InputText('0.055', key='ES')],
    [sg.Text('EI', size=(15, 1)), sg.InputText('0.006', key='EI')],
    [sg.Text('X_', size=(15, 1)), sg.InputText('0.026', key='X_')],
    [sg.Text('sigma', size=(15, 1)), sg.InputText('0.012', key='sigma')],
    [sg.Submit('Расчитать'), sg.Cancel('Отмена')]
]

window = sg.Window('Статистические методы исследования точности', layout)
event, values = window.read()
window.close()

EI = float(values['EI'])
ES = float(values['ES'])
X_ = float(values['X_'])
sigma = float(values['sigma'])

## Функция расчёта интеграла


def integral_f(z):
    f = integrate.quad(lambda z: pow(math.e, (-pow(z, 2) / 2)), 0, z)
    return f[0] * 1 / (sqrt(2 * math.pi))


def phi(x):
    return 1 / (sigma * math.sqrt(2 * math.pi)) * math.pow(math.e, -math.pow(x - X_, 2) / (2 * math.pow(sigma, 2)))


## Определение % годных деталей
k = (integral_f(((ES - X_) / sigma)) - integral_f(((EI - X_) / sigma))) * 100
## Определение % годных деталей
l = (integral_f((EI-X_) / sigma) - integral_f((X_ - X_ - 3 * sigma) / sigma)) * 100
## Определение % исправимого брака
j = (integral_f((X_ + 3 * sigma - X_) / sigma) -
     integral_f((ES - X_) / sigma)) * 100


x = np.arange(X_-3*sigma, X_+3*sigma, 0.0001)  # прогонка кривой по Х
y = [phi(z) for z in x]  # прогонка по Y
fig = plt.figure(figsize=(10, 10))  # Размер фигуры
ax = fig.add_subplot(111)  # оси
ax.plot(x, y)  # Соединяем соотношение

ax.spines['left'].set_position('zero')
ax.spines['right'].set_color('none')
ax.spines['bottom'].set_position('zero')
ax.spines['top'].set_color('none')

ax.yaxis.set_tick_params(
    which='both',
    bottom=False,
    top=False,
    labelbottom=False)

# Легенда графика
ax.legend(
    loc='lower center',
    fontsize=25,
        ncol=10,
        facecolor='oldlace',
        edgecolor='r',
    title=str('%.1f' % k.real + '% - Годных деталей' + '\n' + '%.2f' % l.real +
              '% - неисправимого брака' + '\n' + '%.2f' % j.real + '% - исправимого брака'),
        title_fontsize='10'
)
plt.axvline(x=EI, color="black", linestyle=":")
plt.axvline(x=ES, color="black", linestyle=":")
wm = plt.get_current_fig_manager()
wm.window.state('zoomed')
plt.fill_between(x, y, where=[y >= 0 and y <= phi(
    x) and x <= EI for x, y in zip(x, y)], hatch="/", edgecolor="r", linewidth=0.0)
plt.fill_between(x, y, where=[y >= 0 and y <= phi(
    x) and x >= ES for x, y in zip(x, y)], hatch="/", edgecolor="r", linewidth=0.0)
plt.suptitle('График нормального распределения')
plt.show()

X_ = (3*sigma+EI)  # смещение графика погрещностей из зоны неисправимого брака

print("Перенесём X_ в зону нормальный погрешностей")
## Определение % годных деталей
k = (integral_f(((ES - X_) / sigma)) - integral_f(((EI - X_) / sigma))) * 100
## Определение % годных деталей
l = (integral_f((EI-X_) / sigma) - integral_f((X_ - X_ - 3 * sigma) / sigma)) * 100
## Определение % исправимого брака
j = (integral_f((X_ + 3 * sigma - X_) / sigma) -
     integral_f((ES - X_) / sigma)) * 100


x = np.arange(EI, X_+3*sigma, 0.0001)  # прогонка
y = [phi(z) for z in x]  # параметры
fig = plt.figure(figsize=(8, 8))
ax = fig.add_subplot(111)  # оси
ax.plot(x, y)

ax.spines['left'].set_position('zero')
ax.spines['right'].set_color('none')
ax.spines['bottom'].set_position('zero')
ax.spines['top'].set_color('none')

ax.yaxis.set_tick_params(
    which='both',
    bottom=False,
    top=False,
    labelbottom=False)
ax.legend(
    loc='lower center',
    fontsize=25,
        ncol=10,
        facecolor='oldlace',
        edgecolor='r',
    title=str('%.1f' % k.real + '% - Годных деталей' + '\n' + '%.2f' % l.real +
              '% - неисправимого брака' + '\n' + '%.2f' % j.real + '% - исправимого брака'),
    title_fontsize='10'
)
wm = plt.get_current_fig_manager()
wm.window.state('zoomed')
plt.axvline(x=EI, color="black", linestyle=":")
plt.axvline(x=ES, color="black", linestyle=":")

plt.fill_between(x, y, where=[y >= 0 and y <= phi(
    x) and x > ES for x, y in zip(x, y)], hatch="/", edgecolor="r", linewidth=0.0)
plt.suptitle('График смещения нормального распределения')
plt.show()
