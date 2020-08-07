# JWTExplore
.Net Core 3.1 Web Project Use Primary JWT 

JWTController.cs contains two method:  
  1 *[GetJWT]* use additional key with secret Key _(appsetting.json => secret)_ to generate Token.  
  2 *[UseJWT]* Serialize jwtStr require Token's secret to matching appsetting.json  
So,first step to generate JWT ,main contains in _Util => JWTCore.cs_,  
last is to match.You can find method ConfigureServices in _Startup.cs_.  
The contrast used by service inject to project.  
