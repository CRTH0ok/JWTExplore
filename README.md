# JWTExplore
.Net Core 3.1 Web Project Use Primary JWT 

JWTController.cs contains two method:
  1 [GetJWT] use additional key with secret Key(appsetting.json => secret) to generate Token.
  2 [UseJWT] Serialize jwtStr require Token's secret to matching appsetting.json
So,first step to generate JWT ,main contains in Util => JWTCore.cs,
last is to match.You can find method ConfigureServices in Startup.cs.
The contrast used by service inject to project.
