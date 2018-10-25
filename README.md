# CoreFundamentals
Training Core Mvc Fundamentals

Course Pluralsight for test Core MVC functionalities.
https://app.pluralsight.com/library/courses/aspdotnet-core-fundamentals
Basic Training.

------------------------------
Notes

Middleware
Conjunto de piezas

Logger --> Authorizer --> Router -->
Logger <-- Authorizer <-- Router <--

1) Logger: Puede ver los request, header, cookies, etc
2) Authorizer: Analiza JWT, una cookie especifica, etc. 
3) Router: Redirecciona al controller, retorna la pagina correspondiete, xml o json


Cualquier pieza del Middleware puede reject un request

------------------------
OpenId Connect for membership

[A]Client (Web Browser) | [B]Identity Provider (Azure AD, Facebook, LinkedIn, etc) | [C]Nuestra App

1) Customer send a anonymous Request a nuestra App:  [A] --> Anonymous Request -->[C]
2) Cuando la app verifica que el usuario no esta autenticado lo que hace es enviar una redirección. Es decir envia el usuario a una pagina
para que autentique con nuestro Identity Provider: [C] --> Redirect a LINK -->[A] --> [B]Identity Provider
3) El usuario ingresa sus credenciales, el Identity provider verifica que los datos sean los correctos, si esto es así entonces redirige a nuestra app incluyendo un ID_Token: [A] --> Password --> [B] --> Credenciales OK --> [C]

4)Una vez que nuestra app reciba el Toekn verificado la tendremos que almacenar en una cookie de seguridad para poder mantener los request authenticados de lo contrario en cada request sera redirigido al Identity Provider

-------------------------
Enable SSL
1)Click derecho properties en el proyecto
2)Debug --> Web Server Settings --> Habilitar Enable SSL 
3)Copiar la URL en el textbox App URL para que levante por default con https

