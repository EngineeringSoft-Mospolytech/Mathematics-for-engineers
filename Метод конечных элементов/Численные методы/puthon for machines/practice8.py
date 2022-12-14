import csv
import PySimpleGUI as sg

sg.theme('Reddit')
layout = [
    [sg.Text('Введите данные')],
    [sg.Text(
        "Данная программа расчитывает погрешности, вызываемые упргуими деформациями станка ", size=(0, 1))],
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
Wzb = Wsyp = Wnb


delta_pack = [0, 0, 0, 0, 0]
counter = 0
delta = ''
for x in range(0, 81, 20):
    Py = 10 * Cp * pow(t, x) * pow(S, y) * pow(V, n) * Kp
    delta_num = 2*Py * abs(Wsyp + Wnb*(pow(x/l, 2)))
    delta_num = float('{:.3f}'.format(delta_num))
    delta_pack[counter] = delta_num
    counter = counter + 1
    delta = delta + str('%.2f' % delta_num) + '\n'
sg.Popup('В ходе расчётов для l = ' + str(l) +
         '\nНачиная с 0 и шагом 20', delta, title='Резульаты')


with open('some.csv', 'a', newline='') as f:
    writer = csv.writer(f)
    writer.writerow(delta_pack)
