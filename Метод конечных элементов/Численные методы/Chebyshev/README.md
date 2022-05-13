# Метод Чебышева
Интерполяционная формула Лагранжа дает возможность составить множество приближенных формул вида

![](https://github.com/EngineeringSoft-Mospolytech/Spring-2022/blob/main/%D0%9C%D0%B5%D1%82%D0%BE%D0%B4%20%D0%BA%D0%BE%D0%BD%D0%B5%D1%87%D0%BD%D1%8B%D1%85%20%D1%8D%D0%BB%D0%B5%D0%BC%D0%B5%D0%BD%D1%82%D0%BE%D0%B2/%D0%A7%D0%B8%D1%81%D0%BB%D0%B5%D0%BD%D0%BD%D1%8B%D0%B5%20%D0%BC%D0%B5%D1%82%D0%BE%D0%B4%D1%8B/Chebyshev/Images/1.png)

где x1, x2, …, xn числа между a и b . Действительно, задав узлы интерполяции на отрезке [a; b] произвольным образом и заменив подынтегральную функцию полиномом Лагранжа степени n, получим (действуя так же, как при выводе формулы Котеса) для вычисления Сi формулу

![](https://github.com/EngineeringSoft-Mospolytech/Spring-2022/blob/main/%D0%9C%D0%B5%D1%82%D0%BE%D0%B4%20%D0%BA%D0%BE%D0%BD%D0%B5%D1%87%D0%BD%D1%8B%D1%85%20%D1%8D%D0%BB%D0%B5%D0%BC%D0%B5%D0%BD%D1%82%D0%BE%D0%B2/%D0%A7%D0%B8%D1%81%D0%BB%D0%B5%D0%BD%D0%BD%D1%8B%D0%B5%20%D0%BC%D0%B5%D1%82%D0%BE%D0%B4%D1%8B/Chebyshev/Images/2.png)

Однако вычислять коэффициенты Ci по этой формуле трудно. Чебышев поставил обратную задачу: задать не узлы x1, x2, …, xn, а коэффициенты Ci и искать соответствующие им узлы. При этом коэффициенты Ci задаются так, чтобы формула была как можно проще для вычислений, а это будет тогда, когда они все равны: C1 = C2 = C3 = …= Cn, тогда формула примет вид

![](https://github.com/EngineeringSoft-Mospolytech/Spring-2022/blob/main/%D0%9C%D0%B5%D1%82%D0%BE%D0%B4%20%D0%BA%D0%BE%D0%BD%D0%B5%D1%87%D0%BD%D1%8B%D1%85%20%D1%8D%D0%BB%D0%B5%D0%BC%D0%B5%D0%BD%D1%82%D0%BE%D0%B2/%D0%A7%D0%B8%D1%81%D0%BB%D0%B5%D0%BD%D0%BD%D1%8B%D0%B5%20%D0%BC%D0%B5%D1%82%D0%BE%D0%B4%D1%8B/Chebyshev/Images/3.png)   

Таким образом, определен коэффициент Cn и узлы x1, x2, …, xn квадратурной формулы Чебышева

![](https://github.com/EngineeringSoft-Mospolytech/Spring-2022/blob/main/%D0%9C%D0%B5%D1%82%D0%BE%D0%B4%20%D0%BA%D0%BE%D0%BD%D0%B5%D1%87%D0%BD%D1%8B%D1%85%20%D1%8D%D0%BB%D0%B5%D0%BC%D0%B5%D0%BD%D1%82%D0%BE%D0%B2/%D0%A7%D0%B8%D1%81%D0%BB%D0%B5%D0%BD%D0%BD%D1%8B%D0%B5%20%D0%BC%D0%B5%D1%82%D0%BE%D0%B4%D1%8B/Chebyshev/Images/4.png)   

Составлены таблицы значений узлов при различных количествах узлов n в формуле Чебышева 

![](https://github.com/EngineeringSoft-Mospolytech/Spring-2022/blob/main/%D0%9C%D0%B5%D1%82%D0%BE%D0%B4%20%D0%BA%D0%BE%D0%BD%D0%B5%D1%87%D0%BD%D1%8B%D1%85%20%D1%8D%D0%BB%D0%B5%D0%BC%D0%B5%D0%BD%D1%82%D0%BE%D0%B2/%D0%A7%D0%B8%D1%81%D0%BB%D0%B5%D0%BD%D0%BD%D1%8B%D0%B5%20%D0%BC%D0%B5%D1%82%D0%BE%D0%B4%D1%8B/Chebyshev/Images/5.png)   

Если пределы интегрирования a и b, формула Чебышева принимает вид

![](https://github.com/EngineeringSoft-Mospolytech/Spring-2022/blob/main/%D0%9C%D0%B5%D1%82%D0%BE%D0%B4%20%D0%BA%D0%BE%D0%BD%D0%B5%D1%87%D0%BD%D1%8B%D1%85%20%D1%8D%D0%BB%D0%B5%D0%BC%D0%B5%D0%BD%D1%82%D0%BE%D0%B2/%D0%A7%D0%B8%D1%81%D0%BB%D0%B5%D0%BD%D0%BD%D1%8B%D0%B5%20%D0%BC%D0%B5%D1%82%D0%BE%D0%B4%D1%8B/Chebyshev/Images/6.png)   

Где а xi берется из таблицы.

![](https://github.com/EngineeringSoft-Mospolytech/Spring-2022/blob/main/%D0%9C%D0%B5%D1%82%D0%BE%D0%B4%20%D0%BA%D0%BE%D0%BD%D0%B5%D1%87%D0%BD%D1%8B%D1%85%20%D1%8D%D0%BB%D0%B5%D0%BC%D0%B5%D0%BD%D1%82%D0%BE%D0%B2/%D0%A7%D0%B8%D1%81%D0%BB%D0%B5%D0%BD%D0%BD%D1%8B%D0%B5%20%D0%BC%D0%B5%D1%82%D0%BE%D0%B4%D1%8B/Chebyshev/Images/7.png)  
