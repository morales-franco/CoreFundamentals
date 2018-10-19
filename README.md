# CoreFundamentals
Training Core Mvc Fundamentals

Middleware
Conjunto de piezas

Logger --> Authorizer --> Router -->
Logger <-- Authorizer <-- Router <--

1) Logger: Puede ver los request, header, cookies, etc
2) Authorizer: Analiza JWT, una cookie especifica, etc. 
3) Router: Redirecciona al controller, retorna la pagina correspondiete, xml o json


Cualuier pieza del Middleware puede reject un request