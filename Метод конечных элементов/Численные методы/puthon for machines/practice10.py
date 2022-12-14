import csv
import PySimpleGUI as sg

sg.theme('Reddit')
layout = [
    [sg.Text('Введите данные')],
    [sg.Text(
        "Данная программа расчитывает погрешности, обусловленной износом режущего инструмента ", size=(0, 1))],
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


Wzb = Wsyp = Wnb
E = 2 * pow(10, 5)
I = 0.005 * pow(D, 4) * abs((1 - pow((D_hole/D), 4)))

delta_pack = [0, 0, 0, 0, 0]
counter = 0
delta = ''
for x in range(0, 81, 20):

    delta_num = x*2*((3.14*D_hole*U0)/(pow(10, 6)*S))

    delta = delta + str('%.2f' % delta_num) + '\n'

    delta_num = float('{:.3f}'.format(delta_num))
    delta_pack[counter] = delta_num
    counter = counter + 1


sg.Popup('В ходе расчётов для l = ' + str(l), delta, title='Резульаты')
with open('some.csv', 'a', newline='') as f:
    writer = csv.writer(f)
    writer.writerow(delta_pack)
