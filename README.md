# RSA App
Proyecto para el curso de **Estructura de Datos II**, en el cual se implementa el algoritmo de cifrado y generación de llaves **RSA**.

## Algoritmo RSA ([Wikipedia](https://es.wikipedia.org/wiki/RSA))
RSA (Rivest, Shamir y Adleman) es un sistema criptográfico de clave pública desarrollado en 1979. 
Es el primer y más utilizado algoritmo de este tipo y es válido tanto para cifrar como para firmar digitalmente.

## Rutas y comportamiento de los métodos

####  /api/rsa/keys/{p}/{q}
-Recibe dos valores numéricos (p y q) con los cuales genera las claves pública y privada y los devuelve.
-Retorna un archivo ZIP (utilizar librería de.Net para Zip) en la que estarán los 2 archivos siguientes: private.key y public.key

####  /api/rsa/{nombre}
POST
-Recibe un archivo que se deberá cifrar o descifrar con el método de RSA
-Recibe un archivo de llave (puede ser public o private)
-Retorna el archivo de texto cifrado o descifrado con el nombre que se envió como parámetro a la ruta.
-Devuelve OK si no hubo error
-Devuelve InternalServerError si hubo

## Implementación
Para clonar el proyecto utilice el siguiente enlace: [https://github.com/Ale180820/RSA_App.git]()

`$ git clone https://github.com/Ale180820/RSA_App.git `


## Autores

Alejandra Recinos : @Ale180820

Victor Hernández  : @victorisimoo
