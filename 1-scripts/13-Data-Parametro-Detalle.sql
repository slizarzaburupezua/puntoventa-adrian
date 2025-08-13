INSERT [dbo].[ParametroDetalle] ([id_parametro], [para_key], [sub_para_key], [nombre], [descripcion],[tipocampo],[orden],[svalor1],[activo], [estado], [fecha_registro], [fecha_actualizacion], [fecha_anulacion]) 
VALUES                           (1            ,'LOGO_STM' , 'LOGO_PCP'    , 'Logo Principal', 'Logo que se mostrará en las pantallas principales del sistema', 'URL', 1,   'https://res.cloudinary.com/dvzkgpiv3/image/upload/v1745963688/VentasPlatformDemo/Landing/zmf8rpgyopv50afutioh.png', 1,1,   CAST(N'2025-07-19T06:46:20.057' AS DateTime), NULL, NULL)
GO

INSERT [dbo].[ParametroDetalle] ([id_parametro], [para_key], [sub_para_key], [nombre], [descripcion],[tipocampo],[orden],[svalor1],[activo], [estado], [fecha_registro], [fecha_actualizacion], [fecha_anulacion]) 
VALUES                           (1            ,'LOGO_STM' , 'LOGO_HME'    , 'Logo Home', 'Logo con texto que se mostrará en el home del sistema', 'URL', 1,   'https://res.cloudinary.com/dvzkgpiv3/image/upload/v1744479552/VentasPlatformDemo/Landing/rehucrjhddu6h49se46e.png', 1,1,   CAST(N'2025-07-19T06:46:20.057' AS DateTime), NULL, NULL)
GO
 