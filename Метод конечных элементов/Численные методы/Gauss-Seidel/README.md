# Метод Гаусса-Зейделя
Метод Гаусса – Зейделя относится к численным методом решений и заключается в итерационном приближении к корням системы. Для разбора алгоритма решения будем использовать систему в общем виде с одной переменной Х

![](https://github.com/EngineeringSoft-Mospolytech/Spring-2022/blob/main/%D0%9C%D0%B5%D1%82%D0%BE%D0%B4%20%D0%BA%D0%BE%D0%BD%D0%B5%D1%87%D0%BD%D1%8B%D1%85%20%D1%8D%D0%BB%D0%B5%D0%BC%D0%B5%D0%BD%D1%82%D0%BE%D0%B2/%D0%A7%D0%B8%D1%81%D0%BB%D0%B5%D0%BD%D0%BD%D1%8B%D0%B5%20%D0%BC%D0%B5%D1%82%D0%BE%D0%B4%D1%8B/Gauss-Seidel/images/%D0%A0%D0%B5%D1%81%D1%83%D1%80%D1%81%205.png)

Выразим неизвестные x1, x2, x3 соответственно из первого, второго и третьего уравнений системы:

![](https://github.com/EngineeringSoft-Mospolytech/Spring-2022/blob/main/%D0%9C%D0%B5%D1%82%D0%BE%D0%B4%20%D0%BA%D0%BE%D0%BD%D0%B5%D1%87%D0%BD%D1%8B%D1%85%20%D1%8D%D0%BB%D0%B5%D0%BC%D0%B5%D0%BD%D1%82%D0%BE%D0%B2/%D0%A7%D0%B8%D1%81%D0%BB%D0%B5%D0%BD%D0%BD%D1%8B%D0%B5%20%D0%BC%D0%B5%D1%82%D0%BE%D0%B4%D1%8B/Gauss-Seidel/images/%D0%A0%D0%B5%D1%81%D1%83%D1%80%D1%81%206.png)


Теперь необходимо задать начальные приближенные значения, пусть это будут нули, подставив в выражения, получаем первое приближение по нулю:

![](https://github.com/EngineeringSoft-Mospolytech/Spring-2022/blob/main/%D0%9C%D0%B5%D1%82%D0%BE%D0%B4%20%D0%BA%D0%BE%D0%BD%D0%B5%D1%87%D0%BD%D1%8B%D1%85%20%D1%8D%D0%BB%D0%B5%D0%BC%D0%B5%D0%BD%D1%82%D0%BE%D0%B2/%D0%A7%D0%B8%D1%81%D0%BB%D0%B5%D0%BD%D0%BD%D1%8B%D0%B5%20%D0%BC%D0%B5%D1%82%D0%BE%D0%B4%D1%8B/Gauss-Seidel/images/%D0%A0%D0%B5%D1%81%D1%83%D1%80%D1%81%208.png)

Подставим эти значения для x1, получаем первое приближение для x1

![](https://github.com/EngineeringSoft-Mospolytech/Spring-2022/blob/main/%D0%9C%D0%B5%D1%82%D0%BE%D0%B4%20%D0%BA%D0%BE%D0%BD%D0%B5%D1%87%D0%BD%D1%8B%D1%85%20%D1%8D%D0%BB%D0%B5%D0%BC%D0%B5%D0%BD%D1%82%D0%BE%D0%B2/%D0%A7%D0%B8%D1%81%D0%BB%D0%B5%D0%BD%D0%BD%D1%8B%D0%B5%20%D0%BC%D0%B5%D1%82%D0%BE%D0%B4%D1%8B/Gauss-Seidel/images/%D0%A0%D0%B5%D1%81%D1%83%D1%80%D1%81%209.png)

А далее, зная x1 1 мы подставляем вычисленное значение в выражение для x2, и после этого x2 подставляем в х3. Общая система выглядит так

![](https://github.com/EngineeringSoft-Mospolytech/Spring-2022/blob/main/%D0%9C%D0%B5%D1%82%D0%BE%D0%B4%20%D0%BA%D0%BE%D0%BD%D0%B5%D1%87%D0%BD%D1%8B%D1%85%20%D1%8D%D0%BB%D0%B5%D0%BC%D0%B5%D0%BD%D1%82%D0%BE%D0%B2/%D0%A7%D0%B8%D1%81%D0%BB%D0%B5%D0%BD%D0%BD%D1%8B%D0%B5%20%D0%BC%D0%B5%D1%82%D0%BE%D0%B4%D1%8B/Gauss-Seidel/images/%D0%A0%D0%B5%D1%81%D1%83%D1%80%D1%81%2011.png)

## Пример

![](https://github.com/EngineeringSoft-Mospolytech/Spring-2022/blob/main/%D0%9C%D0%B5%D1%82%D0%BE%D0%B4%20%D0%BA%D0%BE%D0%BD%D0%B5%D1%87%D0%BD%D1%8B%D1%85%20%D1%8D%D0%BB%D0%B5%D0%BC%D0%B5%D0%BD%D1%82%D0%BE%D0%B2/%D0%A7%D0%B8%D1%81%D0%BB%D0%B5%D0%BD%D0%BD%D1%8B%D0%B5%20%D0%BC%D0%B5%D1%82%D0%BE%D0%B4%D1%8B/Gauss-Seidel/images/%D0%A0%D0%B5%D1%81%D1%83%D1%80%D1%81%2012.png)

А теперь совершим первую итерацию

![](https://github.com/EngineeringSoft-Mospolytech/Spring-2022/blob/main/%D0%9C%D0%B5%D1%82%D0%BE%D0%B4%20%D0%BA%D0%BE%D0%BD%D0%B5%D1%87%D0%BD%D1%8B%D1%85%20%D1%8D%D0%BB%D0%B5%D0%BC%D0%B5%D0%BD%D1%82%D0%BE%D0%B2/%D0%A7%D0%B8%D1%81%D0%BB%D0%B5%D0%BD%D0%BD%D1%8B%D0%B5%20%D0%BC%D0%B5%D1%82%D0%BE%D0%B4%D1%8B/Gauss-Seidel/images/%D0%A0%D0%B5%D1%81%D1%83%D1%80%D1%81%2013.png)

Получив первые приближённые значения, продолжаем итерацию до заданной погрешности.
