
INSERT [dbo].[Menu] ([padre_texto], [hijo_texto], [titulo], [tipo], [icono], [flg_enlace_externo], [flg_menu_hijo], [ruta], [orden], [activo], [estado], [motivo_anulacion], [fecha_registro], [fecha_actualizacion], [fecha_anulacion]) VALUES (N'inicio', NULL, 'Inicio', 'basic', 'mat_outline:home', 0, 0, '/admin/inicio', 1, 1, 1, NULL, CAST(N'2025-01-10T06:46:20.057' AS DateTime), NULL, NULL)
GO

INSERT [dbo].[Menu] ([padre_texto], [hijo_texto], [titulo], [tipo], [icono], [flg_enlace_externo], [flg_menu_hijo], [ruta], [orden], [activo], [estado], [motivo_anulacion], [fecha_registro], [fecha_actualizacion], [fecha_anulacion]) VALUES (N'ventas', NULL, 'Ventas', 'group', 'mat_outline:payments', 0, 0, NULL, 2, 1, 1, NULL, CAST(N'2025-01-10T06:46:20.057' AS DateTime), NULL, NULL)
GO

INSERT [dbo].[Menu] ([padre_texto], [hijo_texto], [titulo], [tipo], [icono], [flg_enlace_externo], [flg_menu_hijo], [ruta], [orden], [activo], [estado], [motivo_anulacion], [fecha_registro], [fecha_actualizacion], [fecha_anulacion]) VALUES (N'ventas', 'ventas.registro', 'Nueva Venta', 'basic', 'mat_outline:format_list_numbered', 0, 1, '/admin/ventas/registro', 3, 1, 1, NULL, CAST(N'2025-01-10T06:46:20.057' AS DateTime), NULL, NULL)
GO

INSERT [dbo].[Menu] ([padre_texto], [hijo_texto], [titulo], [tipo], [icono], [flg_enlace_externo], [flg_menu_hijo], [ruta], [orden], [activo], [estado], [motivo_anulacion], [fecha_registro], [fecha_actualizacion], [fecha_anulacion]) VALUES (N'ventas', 'ventas.historial', 'Historial', 'basic', 'mat_outline:published_with_changes', 0, 1, '/admin/ventas/historial', 4, 1, 1, NULL, CAST(N'2025-01-10T06:46:20.057' AS DateTime), NULL, NULL)
GO	

INSERT [dbo].[Menu] ([padre_texto], [hijo_texto], [titulo], [tipo], [icono], [flg_enlace_externo], [flg_menu_hijo], [ruta], [orden], [activo], [estado], [motivo_anulacion], [fecha_registro], [fecha_actualizacion], [fecha_anulacion]) VALUES (N'ventas', 'ventas.reporte-productos', 'Reporte por Productos', 'basic', 'feather:bar-chart', 0, 1, '/admin/ventas/reporte-productos', 5, 1, 1, NULL, CAST(N'2025-01-10T06:46:20.057' AS DateTime), NULL, NULL)
GO 

INSERT [dbo].[Menu] ([padre_texto], [hijo_texto], [titulo], [tipo], [icono], [flg_enlace_externo], [flg_menu_hijo], [ruta], [orden], [activo], [estado], [motivo_anulacion], [fecha_registro], [fecha_actualizacion], [fecha_anulacion]) VALUES (N'ventas', 'ventas.reporte-categorias', 'Reporte por Categorías', 'basic', 'feather:bar-chart', 0, 1, '/admin/ventas/reporte-categorias', 6, 1, 1, NULL, CAST(N'2025-01-10T06:46:20.057' AS DateTime), NULL, NULL)
GO 

INSERT [dbo].[Menu] ([padre_texto], [hijo_texto], [titulo], [tipo], [icono], [flg_enlace_externo], [flg_menu_hijo], [ruta], [orden], [activo], [estado], [motivo_anulacion], [fecha_registro], [fecha_actualizacion], [fecha_anulacion]) VALUES (N'ventas', 'ventas.reporte-marcas', 'Reporte por Marcas', 'basic', 'feather:bar-chart', 0, 1, '/admin/ventas/reporte-marcas', 7, 1, 1, NULL, CAST(N'2025-01-10T06:46:20.057' AS DateTime), NULL, NULL)
GO 

INSERT [dbo].[Menu] ([padre_texto], [hijo_texto], [titulo], [tipo], [icono], [flg_enlace_externo], [flg_menu_hijo], [ruta], [orden], [activo], [estado], [motivo_anulacion], [fecha_registro], [fecha_actualizacion], [fecha_anulacion]) VALUES (N'inventario', NULL, 'Inventario', 'group', 'mat_outline:production_quantity_limits', 0, 0, NULL, 8, 1, 1, NULL, CAST(N'2025-01-10T06:46:20.057' AS DateTime), NULL, NULL)
GO

INSERT [dbo].[Menu] ([padre_texto], [hijo_texto], [titulo], [tipo], [icono], [flg_enlace_externo], [flg_menu_hijo], [ruta], [orden], [activo], [estado], [motivo_anulacion], [fecha_registro], [fecha_actualizacion], [fecha_anulacion]) VALUES (N'inventario', 'inventario.productos', 'Lista de Productos', 'basic', 'mat_outline:shopping_cart', 0, 1, '/admin/inventario/productos', 9, 1, 1, NULL, CAST(N'2025-01-10T06:46:20.057' AS DateTime), NULL, NULL)
GO

INSERT [dbo].[Menu] ([padre_texto], [hijo_texto], [titulo], [tipo], [icono], [flg_enlace_externo], [flg_menu_hijo], [ruta], [orden], [activo], [estado], [motivo_anulacion], [fecha_registro], [fecha_actualizacion], [fecha_anulacion]) VALUES (N'inventario', 'inventario.categoria', 'Lista de Categorías', 'basic', 'mat_outline:format_list_numbered', 0, 1, '/admin/inventario/categoria', 10, 1, 1, NULL, CAST(N'2025-01-10T06:46:20.057' AS DateTime), NULL, NULL)
GO

INSERT [dbo].[Menu] ([padre_texto], [hijo_texto], [titulo], [tipo], [icono], [flg_enlace_externo], [flg_menu_hijo], [ruta], [orden], [activo], [estado], [motivo_anulacion], [fecha_registro], [fecha_actualizacion], [fecha_anulacion]) VALUES (N'inventario', 'inventario.marca', 'Lista de Marcas', 'basic', 'mat_outline:format_list_numbered', 0, 1, '/admin/inventario/marca', 11, 1, 1, NULL, CAST(N'2025-01-10T06:46:20.057' AS DateTime), NULL, NULL)
GO

INSERT [dbo].[Menu] ([padre_texto], [hijo_texto], [titulo], [tipo], [icono], [flg_enlace_externo], [flg_menu_hijo], [ruta], [orden], [activo], [estado], [motivo_anulacion], [fecha_registro], [fecha_actualizacion], [fecha_anulacion]) VALUES (N'clientes', NULL, 'Clientes', 'group', 'mat_outline:people', 0, 0, NULL, 12, 1, 1, NULL, CAST(N'2025-01-10T06:46:20.057' AS DateTime), NULL, NULL)
GO

INSERT [dbo].[Menu] ([padre_texto], [hijo_texto], [titulo], [tipo], [icono], [flg_enlace_externo], [flg_menu_hijo], [ruta], [orden], [activo], [estado], [motivo_anulacion], [fecha_registro], [fecha_actualizacion], [fecha_anulacion]) VALUES (N'clientes', 'clientes.lista', 'Lista de Clientes', 'basic', 'mat_outline:format_list_numbered', 0, 1, '/admin/clientes/lista', 13, 1, 1, NULL, CAST(N'2025-01-10T06:46:20.057' AS DateTime), NULL, NULL)
GO

INSERT [dbo].[Menu] ([padre_texto], [hijo_texto], [titulo], [tipo], [icono], [flg_enlace_externo], [flg_menu_hijo], [ruta], [orden], [activo], [estado], [motivo_anulacion], [fecha_registro], [fecha_actualizacion], [fecha_anulacion]) VALUES (N'usuarios', NULL, 'Usuarios', 'group', 'mat_outline:people', 0, 0, NULL, 14, 1, 1, NULL, CAST(N'2025-01-10T06:46:20.057' AS DateTime), NULL, NULL)
GO

INSERT [dbo].[Menu] ([padre_texto], [hijo_texto], [titulo], [tipo], [icono], [flg_enlace_externo], [flg_menu_hijo], [ruta], [orden], [activo], [estado], [motivo_anulacion], [fecha_registro], [fecha_actualizacion], [fecha_anulacion]) VALUES (N'usuarios', 'usuarios.lista', 'Lista de Usuarios', 'basic', 'mat_outline:format_list_numbered', 0, 1, '/admin/usuarios/lista', 15, 1, 1, NULL, CAST(N'2025-01-10T06:46:20.057' AS DateTime), NULL, NULL)
GO
 
INSERT [dbo].[Menu] ([padre_texto], [hijo_texto], [titulo], [tipo], [icono], [flg_enlace_externo], [flg_menu_hijo], [ruta], [orden], [activo], [estado], [motivo_anulacion], [fecha_registro], [fecha_actualizacion], [fecha_anulacion]) VALUES (N'negocio', NULL, 'Negocio', 'basic', 'mat_outline:business', 0, 0, '/admin/negocio', 16, 1, 1, NULL, CAST(N'2025-01-10T06:46:20.057' AS DateTime), NULL, NULL)
GO

INSERT [dbo].[Menu] ([padre_texto], [hijo_texto], [titulo], [tipo], [icono], [flg_enlace_externo], [flg_menu_hijo], [ruta], [orden], [activo], [estado], [motivo_anulacion], [fecha_registro], [fecha_actualizacion], [fecha_anulacion]) VALUES (N'contacto', NULL, 'Contacto', 'basic', 'mat_outline:email', 1, 0, 'https://perfisoft.com/contacto/', 17, 1, 1, NULL, CAST(N'2025-01-10T06:46:20.057' AS DateTime), NULL, NULL)
GO

INSERT [dbo].[Menu] ([padre_texto], [hijo_texto], [titulo], [tipo], [icono], [flg_enlace_externo], [flg_menu_hijo], [ruta], [orden], [activo], [estado], [motivo_anulacion], [fecha_registro], [fecha_actualizacion], [fecha_anulacion]) VALUES (N'parametro', NULL, 'Parámetro', 'basic', 'mat_outline:tune', 0, 0, '/admin/parametro', 18, 1, 1, NULL, CAST(N'2025-07-05T06:46:20.057' AS DateTime), NULL, NULL)
GO
 
 