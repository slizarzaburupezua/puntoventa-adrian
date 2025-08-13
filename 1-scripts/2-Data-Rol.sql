INSERT [dbo].[Rol] ([nombre], [descripcion], [activo], [estado], [motivo_anulacion], [fecha_registro], [fecha_actualizacion], [fecha_anulacion]) VALUES (N'Administrador del sistema', N'Responsable de gestionar la configuración y permisos del sistema.', 1,1, NULL, CAST(N'2024-01-19T06:46:20.057' AS DateTime), NULL, NULL);
GO

INSERT [dbo].[Rol] ([nombre], [descripcion], [activo], [estado], [motivo_anulacion], [fecha_registro], [fecha_actualizacion], [fecha_anulacion]) VALUES (N'Gerente de Ventas', N'Supervisa al equipo de ventas y define estrategias comerciales.', 1, 1,NULL, CAST(N'2024-01-19T06:46:20.060' AS DateTime), NULL, NULL);
GO

INSERT [dbo].[Rol] ([nombre], [descripcion], [activo], [estado], [motivo_anulacion], [fecha_registro], [fecha_actualizacion], [fecha_anulacion]) VALUES (N'Vendedor', N'Realiza la gestión directa de ventas y atención al cliente.', 1, 1, NULL, CAST(N'2024-01-19T06:46:20.060' AS DateTime), NULL, NULL);
GO

INSERT [dbo].[Rol] ([nombre], [descripcion], [activo], [estado], [motivo_anulacion], [fecha_registro], [fecha_actualizacion], [fecha_anulacion]) VALUES (N'Encargado de Stock', N'Responsable de gestionar y monitorear el inventario, asegurando la disponibilidad de productos.', 1, 1, NULL, CAST(N'2024-01-19T06:46:20.060' AS DateTime), NULL, NULL);
GO
 