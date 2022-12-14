import csv
import PySimpleGUI as sg
import math
import numpy as np

sg.theme('Reddit')
layout = [
    [sg.Text('Введите данные')],
    [sg.Text(
        "Данная программа расчитывает погрешности, от тепловых нагрузок и суммарной погрешности ", size=(0, 1))],
    [sg.Text('CP', size=(15, 1)), sg.InputText('243', key='Cp')],
    [sg.Text('x', size=(15, 1)), sg.InputText('0.9', key='x')],
    [sg.Text('y', size=(15, 1)), sg.InputText('0.6', key='y')],
    [sg.Text('n', size=(15, 1)), sg.InputText('-0.3', key='n')],
    [sg.Text('Wпб = Wзб = Wcyn', size=(15, 1)),
     sg.InputText('0.02', key='Wnb')],
    [sg.Text('t', size=(15, 1)), sg.InputText('1', key='t')],
    [sg.Text('S', size=(15, 1)), sg.InputText('0.2', key='S')],
    [sg.Text('V', size=(15, 1)), sg.InputText('140', key='V')],
    [sg.Text('Kp', size=(15, 1)), sg.InputText('1', key='Kp')],
    [sg.Text('l', size=(15, 1)), sg.InputText('80', key='l')],
    [sg.Text('D', size=(15, 1)), sg.InputText('90', key='D')],
    [sg.Text('D_hole', size=(15, 1)), sg.InputText('60', key='D_hole')],
    [sg.Text('U0', size=(15, 1)), sg.InputText('9', key='U0')],
    [sg.Text('Sigma_b', size=(15, 1)), sg.InputText('640', key='Sigma_b')],
    [sg.Text('F', size=(15, 1)), sg.InputText('800', key='F')],
    [sg.Text('Lp', size=(15, 1)), sg.InputText('20', key='Lp')],
    [sg.Submit('Расчитать'), sg.Cancel('Отмена')]
]
window = sg.Window('Статистические методы исследования точности', layout)
event, values = window.read()
window.close()


Cp = float(values['Cp'])
x = float(values['x'])
y = float(values['y'])
n = float(values['n'])
Wnb = float(values['Wnb'])
t = float(values['t'])
S = float(values['S'])
V = float(values['V'])
Kp = float(values['Kp'])
l = float(values['l'])
D = float(values['D'])
D_hole = float(values['D_hole'])
U0 = float(values['U0'])
Lp = float(values['Lp'])
F = float(values['F'])
Sigma_b = float(values['Sigma_b'])
Wzb = Wsyp = Wnb
E = 2 * pow(10, 5)
I = 0.005 * pow(D, 4) * abs((1 - pow((D_hole/D), 4)))


delta_pack = [0, 0, 0, 0, 0]
counter = 0
delta = ''
for x in range(0, 81, 20):

    delta_num = -2*(9/2)*(Lp/F) * Sigma_b*pow((t*S), 0.75) * math.sqrt(V) * \
        (1 - pow(math.e, ((3.14*(-D_hole)) / (4000*S*V)) * x))
    delta = delta + str('%.2f' % delta_num) + '\n'
    delta_num = float('{:.3f}'.format(delta_num))
    delta_pack[counter] = delta_num
    counter = counter + 1


sg.Popup('В ходе расчётов для l = ' + str(l), delta, title='Резульаты')

with open('some.csv', 'a', newline='') as f:
    writer = csv.writer(f)
    writer.writerow(delta_pack)
with open('some.csv', newline='') as csvfile:
    data = list(csv.reader(csvfile))

Summ1 = float(data[0][0]) + float(data[1][0]) + \
    float(data[2][0]) + float(data[3][0])
Summ2 = float(data[0][1]) + float(data[1][1]) + \
    float(data[2][1]) + float(data[3][1])
Summ3 = float(data[0][2]) + float(data[1][2]) + \
    float(data[2][2]) + float(data[3][2])
Summ4 = float(data[0][3]) + float(data[1][3]) + \
    float(data[2][3]) + float(data[3][3])

sg.popup('Суммарная вероятность для Lmax = 80 и шагом 20 от 0',
         Summ1, Summ2, Summ3, Summ4)
