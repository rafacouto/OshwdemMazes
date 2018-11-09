# OSHWDEM Mazes

-[English version](README.md)-

[_OpenSource Hardware Demonstration_][OSH01] de la [_Asociación Bricolabs_][BRI01] es la _barcamp_ más imporante de _makers_ en Galicia. Desde 2014, hay retos con robots y [todos estáis invitados a participar][CON01].

![Constructed maze](img/maze.png)

Uno de los concursos es el _Laberinto con Robots_. Este programa fue concebido para ser el generador de laberintos de acuerdo a las normas específicas del concurso. Se compila y ejecuta en la pantalla gigante al principio de la competición y el laberinto real se configura con los muros tal cual se muestra en la pantalla.

![Ejecutar Maze Generator](img/console.png)

El código fuente se publica para que se pueda estudiar y generar laberintos para auditar el programa y practicar con configuraciones de laberinto similares a las de competición.


## Preparar el entorno

Necesitas una distribución _GNU/Linux_ y el entorno de ejecución _Mono.net_ para ejecutar _Maze Generator_. La versión actual está probada a fondo con Debian.

Obtén el binario precompilado desde _Github_: https://github.com/brico-labs/OshwdemMazes/raw/master/precompiled/

Maximiza la ventana de terminal para ver todo el laberinto en pantalla.



## Ejecutar Maze Generator

El binario precompilado es compatible con _Mono.NET 4.5_. Ejecútalo con la siguiente orden:

    mono MazeGenerator.exe

Si algo fue mal con la orden, probablemente necesites el _entorno de ejecución Mono_. _Debian_ y sus derivados proporcionan un _paquete Mono_ y es fácil de instalar:

    sudo apt-get install mono-runtime

Echa un vistazo a la guía [_How to install Mono_][MON01] si tienes otra distribución o sistema operativo.

Nota para _usuarios de Güindows y OSex_: debería funcionar desde una consola. Sin embargo, NO ofrecemos soporte para sistemas operativos privativos.



## Compilar Maze Generator

El código fuente incluye un fichero _OshwdemMazes.sln_. Instala _MonoDevelop_, ábrelo y construye el proyecto. Se genera un nuevo binario en el directorio _bin_.

Para instalar _MonoDevelop_:

    sudo apt-get install monodevelop



## Licencia

Version 3 de la GNU General Public License (GPLv3). Ver [_LICENSE.txt_](LICENSE.txt).



## Órdenes extra

¡Larga vida a la línea de órdenes!


### Generar laberintos masivamente

Generar 10 laberintos y guardarlos en un fichero:

    for T in $(seq 10) ; do echo "Thanks" | mono MazeGenerator.exe >> mazes.txt ; done


### Mostrar y guardar al mismo tiempo

Guardar el laberinto en un fichero a la vez que su generación:

    mono MazeGenerator.exe | tee -a mazes.txt



## Soporte

Agradecemos [informe de errores o sugerencias][ISS01].




[BRI01]: http://bricolabs.cc/
[CON01]: http://rules.oshwdem.org/
[ISS01]: https://github.com/rafacouto/OshwdemMazes/issues
[MON01]: https://www.mono-project.com/download/stable/
[OSH01]: http://oshwdem.org/

