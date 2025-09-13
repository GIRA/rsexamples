# Equipos de ejemplo

En este repositorio encontrarán distintos equipos de ejemplo para el campeonato de fútbol de robots de la Roboliga Virtual.

Estos equipos están diseñados para funcionar conectándose a alguno de los controladores "proxy" que incluye el [ambiente de simulación de fútbol](https://github.com/GIRA/rcj-soccer-sim). El controlador proxy es un código que utiliza sockets UDP para conectarse al código del equipo y de esa manera mandar la información de los sensores de cada robot y recibir la velocidad a aplicar a las ruedas.

## Instrucciones

Cada carpeta dentro de este repositorio incluye el mismo equipo programado en distintos lenguajes de programación, al momento de escribir este texto disponemos de ejemplos en 4 lenguajes de programación:

* [Python](https://github.com/GIRA/rsexamples/tree/main/python)
* [.NET](https://github.com/GIRA/rsexamples/tree/main/dotnet)
* [JavaScript](https://github.com/GIRA/rsexamples/tree/main/nodejs)
* [ClojureScript](https://github.com/GIRA/rsexamples/tree/main/cljs)

Las instrucciones para ejecutar el equipo específicas para cada lenguaje se pueden encontrar en el README correspondiente. Sin embargo, en general, implican ejecutar el códigod el equipo (una única instancia para los 3 robots) indicando el puerto a utilizar para comunicarse con el proxy (usualmente `12345` para el equipo amarillo y `54321` para el equipo azul).

Para más información, **VER LOS TUTORIALES OFICIALES** de la Roboliga Virtual, que se pueden encontrar en el siguiente link: [https://virtual.roboliga.ar/tutoriales_futbol.html](https://virtual.roboliga.ar/tutoriales_futbol.html).