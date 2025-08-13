CREATE DATABASE DBVentasTutorial;
GO

USE DBVentasTutorial;
GO

CREATE TABLE Rol (
             id												  INT PRIMARY KEY IDENTITY,
             nombre										      NVARCHAR(100	) NOT NULL,
	         descripcion                                      NVARCHAR(500) DEFAULT NULL,
			 activo							                  BIT NOT NULL,
			 estado							                  BIT NOT NULL,
			 motivo_anulacion                                 NVARCHAR(200) DEFAULT NULL,
	         fecha_registro                                   DATETIME NOT NULL,
	         fecha_actualizacion                              DATETIME DEFAULT NULL,
	         fecha_anulacion                                  DATETIME DEFAULT NULL,
);
GO

CREATE TABLE Menu (
             id												  INT PRIMARY KEY IDENTITY,
			 padre_texto                                      NVARCHAR(50) NULL,
			 hijo_texto                                       NVARCHAR(50) NULL,
			 titulo									 	      NVARCHAR(50) NOT NULL,
			 tipo											  NVARCHAR(50) NOT NULL,
	         icono                                            NVARCHAR(50) NOT NULL,
			 flg_enlace_externo                               BIT NOT NULL,
			 flg_menu_hijo									  BIT NOT NULL,
			 ruta											  NVARCHAR(MAX) NULL,
			 orden											  INT NOT NULL,
			 activo							                  BIT NOT NULL,
			 estado							                  BIT NOT NULL,
			 motivo_anulacion                                 NVARCHAR(200) DEFAULT NULL,
	         fecha_registro                                   DATETIME NOT NULL,
	         fecha_actualizacion                              DATETIME DEFAULT NULL,
	         fecha_anulacion                                  DATETIME DEFAULT NULL,
);
GO
 
CREATE TABLE MenuRol (
             id												  INT PRIMARY KEY IDENTITY,
			 id_menu                                          INT NOT NULL, 
			 id_rol                                           INT NOT NULL, 
             activo							                  BIT NOT NULL,
	         estado							                  BIT NOT NULL,
			 motivo_anulacion                                 NVARCHAR(200) DEFAULT NULL,
	         fecha_registro                                   DATETIME NOT NULL,
	         fecha_actualizacion                              DATETIME DEFAULT NULL,
	         fecha_anulacion                                  DATETIME DEFAULT NULL,
			 FOREIGN KEY (id_menu)                            REFERENCES Menu(id),
			 FOREIGN KEY (id_rol)                             REFERENCES Rol(id),
);
GO
 
CREATE TABLE TipoDocumento ( 					      	              
             id					                              INT PRIMARY KEY  IDENTITY,          
             codigo                                           NVARCHAR(200) NOT NULL,
             descripcion                                      NVARCHAR(200) DEFAULT NULL,
             orden                                            INT NOT NULL,
             activo							                  BIT NOT NULL,
             estado                                           BIT NOT NULL,
             motivo_anulacion                                 NVARCHAR(200) DEFAULT NULL,
             fecha_registro                                   DATETIME NOT NULL,
             fecha_actualizacion                              DATETIME DEFAULT NULL,
             fecha_anulacion                                  DATETIME DEFAULT NULL,
                   );							              
GO	
	 
CREATE TABLE Genero ( 					      	              
             id					                              INT PRIMARY KEY  IDENTITY,	
             codigo                                           NVARCHAR(200) NOT NULL,
             descripcion                                      NVARCHAR(200) DEFAULT NULL,
             orden                                            INT NOT NULL,
             estado                                           BIT NOT NULL,
			 motivo_anulacion                                 NVARCHAR(200) DEFAULT NULL,
             fecha_registro                                   DATETIME NOT NULL,
             fecha_actualizacion                              DATETIME DEFAULT NULL,
             fecha_anulacion                                  DATETIME DEFAULT NULL,
                   );							              
GO	
 
CREATE TABLE Usuario (
			 id                                               INT PRIMARY KEY IDENTITY, 
			 id_rol                                           INT NOT NULL, 
			 id_tipo_documento                                INT NOT NULL, 
			 numero_documento								  VARCHAR(20) NOT NULL UNIQUE,
             nombres                                          NVARCHAR(100) NOT NULL, 
			 apellidos                                        NVARCHAR(100) NOT NULL,
			 id_genero                                        INT NOT NULL,
		 	 correo_electronico                               NVARCHAR(100) NOT NULL,
			 celular										  NVARCHAR(20)  DEFAULT NULL,                         
			 direccion										  NVARCHAR(200) DEFAULT NULL,   
			 fecha_nacimiento                                 DATE NOT NULL,
			 flg_cambiar_clave								  BIT DEFAULT 1,
			 flg_primera_vez_logueo			                  BIT DEFAULT 0,
			 fecha_ultimo_acceso		                      DATETIME DEFAULT NULL,
			 id_foto										  VARCHAR(200) DEFAULT NULL,
			 urlfoto										  VARCHAR(MAX) DEFAULT NULL,
			 activo			                                  BIT NOT NULL,
			 estado			                                  BIT NOT NULL,
			 motivo_anulacion	                              NVARCHAR(100) DEFAULT NULL,
			 fecha_registro                                   DATETIME NOT NULL,
			 fecha_actualizacion                              DATETIME DEFAULT NULL,
			 fecha_anulacion		                          DATETIME DEFAULT NULL,
			 FOREIGN KEY (id_rol)                             REFERENCES Rol(id),
			 FOREIGN KEY (id_genero)                          REFERENCES Genero(id),
		     FOREIGN KEY (id_tipo_documento)                  REFERENCES TipoDocumento(id),
); 
GO

CREATE TABLE UsuarioId ( 
             id                                               INT PRIMARY KEY IDENTITY, 
             id_usuario                                       INT NOT NULL, 
             id_usuario_guid                                  UNIQUEIDENTIFIER NOT NULL, 
			 estado			                                  BIT NOT NULL,
			 fecha_registro                                   DATETIME NOT NULL,
			 fecha_actualizacion                              DATETIME DEFAULT NULL,
			 fecha_anulacion		                          DATETIME DEFAULT NULL,
			 FOREIGN KEY (id_usuario)                         REFERENCES Usuario(id),
                    );					      	              
GO	
 											              
CREATE TABLE UsuarioContrasena ( 				              
			 id                                               INT PRIMARY KEY IDENTITY,
             id_usuario                                       int NOT NULL, 
             clave_hash                                       NVARCHAR(max) NOT NULL, 
			 salt                                             NVARCHAR(max) NOT NULL, 
			 estado			                                  BIT NOT NULL,
			 fecha_registro                                   DATETIME NOT NULL,
			 fecha_actualizacion                              DATETIME DEFAULT NULL,
			 fecha_anulacion		                          DATETIME DEFAULT NULL,
			 FOREIGN KEY (id_usuario)                         REFERENCES Usuario(id),
                    );					      	              
GO		
 										              
CREATE TABLE UsuarioRestableceContrasenia (		              
			 id                                               INT PRIMARY KEY IDENTITY, 
			 correo_electronico                               NVARCHAR(100) NOT NULL,
             Token								              NVARCHAR(500) NOT NULL,
			 verificado                                       BIT NOT NULL,
			 estado                                           BIT NOT NULL,
			 motivo_anulacion                                 NVARCHAR(200) DEFAULT NULL,
             fecha_registro                                   DATETIME NOT NULL,
             fecha_actualizacion                              DATETIME DEFAULT NULL,
             fecha_anulacion                                  DATETIME DEFAULT NULL,
);	
  							              
CREATE TABLE Moneda ( 					      	              
             id					                              INT PRIMARY KEY  IDENTITY,             
             region_iso_dos_letras                            NCHAR(2)  NOT NULL,
			 region_iso_tres_letras                           NCHAR(3)  NOT NULL,
			 codigo_moneda                                    NCHAR(3)  NOT NULL,
			 lenguaje_codigo					              NCHAR(2)  NOT NULL,
			 lenguaje_descripcion				              NCHAR(50) NOT NULL,
			 cultureinfo					                  NCHAR(5)  NOT NULL,
			 pais                                             NVARCHAR(200) DEFAULT NULL,
             descripcion						              NVARCHAR(250) NOT NULL,
			 simbolo							              NCHAR(10) NOT NULL,
             orden                                            INT NOT NULL,
             estado                                           BIT NOT NULL,
             motivo_anulacion                                 NVARCHAR(200) DEFAULT NULL,
             fecha_registro                                   DATETIME NOT NULL,
             fecha_actualizacion                              DATETIME DEFAULT NULL,
             fecha_anulacion                                  DATETIME DEFAULT NULL,
                   );
GO 
 
CREATE TABLE Medida (
             id												  INT PRIMARY KEY IDENTITY,
             nombre										      NVARCHAR(100) NOT NULL,
	         descripcion                                      NVARCHAR(500) DEFAULT NULL,
             abreviatura									  NVARCHAR(10) NOT NULL,
             equivalente									  NVARCHAR(10) NOT NULL,
             valor											  INT NOT NULL,
			 activo							                  BIT NOT NULL,
	         estado                                           BIT NOT NULL,
	         motivo_anulacion                                 NVARCHAR(200) DEFAULT NULL,
	         fecha_registro                                   DATETIME NOT NULL,
	         fecha_actualizacion                              DATETIME DEFAULT NULL,
	         fecha_anulacion                                  DATETIME DEFAULT NULL,
);
GO

CREATE TABLE Categoria (
             id												  INT PRIMARY KEY IDENTITY,
	         id_medida                                        INT NOT NULL,           
             nombre										      NVARCHAR(100) NOT NULL,
	         descripcion                                      NVARCHAR(500) DEFAULT NULL,
			 color						                      NVARCHAR(25)  NOT NULL,
	         activo							                  BIT NOT NULL,
	         estado							                  BIT NOT NULL,
			 motivo_anulacion                                 NVARCHAR(200) DEFAULT NULL,
	         fecha_registro                                   DATETIME NOT NULL,
	         fecha_actualizacion                              DATETIME DEFAULT NULL,
	         fecha_anulacion                                  DATETIME DEFAULT NULL,
	         FOREIGN KEY (id_medida)                          REFERENCES Medida(id),
);
GO

CREATE TABLE Marca (
             id												  INT PRIMARY KEY IDENTITY,           
             nombre										      NVARCHAR(100) NOT NULL,
	         descripcion                                      NVARCHAR(500) DEFAULT NULL,
			 color						                      NVARCHAR(25)  NOT NULL,
			 activo							                  BIT NOT NULL,
	         estado							                  BIT NOT NULL,
			 motivo_anulacion                                 NVARCHAR(200) DEFAULT NULL,
	         fecha_registro                                   DATETIME NOT NULL,
	         fecha_actualizacion                              DATETIME DEFAULT NULL,
	         fecha_anulacion                                  DATETIME DEFAULT NULL,
);
GO
 
CREATE TABLE Producto (
             id												  INT PRIMARY KEY IDENTITY,
	         id_categoria                                     INT NOT NULL, 
			 id_marca										  INT NOT NULL, 
             codigo											  NVARCHAR(50)  UNIQUE  NOT NULL,
			 nombre                                           NVARCHAR(500) UNIQUE  NOT NULL,
	         descripcion                                      NVARCHAR(500) DEFAULT NULL,
			 color						                      NVARCHAR(25)  NOT NULL,
             precio_compra									  DECIMAL(10,2) NOT NULL,
             precio_venta								      DECIMAL(10,2) NOT NULL,
             stock										      INT NOT NULL,
			 id_foto										  VARCHAR(200) DEFAULT NULL,
			 urlfoto										  VARCHAR(MAX) DEFAULT NULL,
	         activo							                  BIT NOT NULL,
	         estado							                  BIT NOT NULL,
			 motivo_anulacion                                 NVARCHAR(200) DEFAULT NULL,
	         fecha_registro                                   DATETIME NOT NULL,
	         fecha_actualizacion                              DATETIME DEFAULT NULL,
	         fecha_anulacion                                  DATETIME DEFAULT NULL,
	         FOREIGN KEY (id_categoria)                       REFERENCES Categoria(id),
			 FOREIGN KEY (id_marca)                           REFERENCES Marca(id),
);
GO
 
CREATE TABLE Cliente (
			 id                                               INT PRIMARY KEY IDENTITY,      
			 id_tipo_documento                                INT NOT NULL, 
			 numero_documento								  VARCHAR(20) NOT NULL UNIQUE,
             nombres                                          NVARCHAR(100) NOT NULL, 
			 apellidos                                        NVARCHAR(100) NOT NULL, 
			 id_genero                                        INT NOT NULL,
			 correo_electronico                               NVARCHAR(100) NOT NULL UNIQUE,
			 celular										  NVARCHAR(15)  DEFAULT NULL,                         
			 direccion										  NVARCHAR(200) DEFAULT NULL,                      
             activo							                  BIT NOT NULL,
	         estado							                  BIT NOT NULL,
			 motivo_anulacion                                 NVARCHAR(200) DEFAULT NULL,
	         fecha_registro                                   DATETIME NOT NULL,
	         fecha_actualizacion                              DATETIME DEFAULT NULL,
	         fecha_anulacion                                  DATETIME DEFAULT NULL,                       
		     FOREIGN KEY (id_tipo_documento)                  REFERENCES TipoDocumento(id),
			 FOREIGN KEY (id_genero)                          REFERENCES Genero(id),
);
	 	
CREATE TABLE VentaNumeroCorrelativo (
			 id												  INT PRIMARY KEY IDENTITY,
			 serie											  VARCHAR(3) NOT NULL,	
			 numero											  INT NOT NULL ,
             activo							                  BIT NOT NULL,
	         estado							                  BIT NOT NULL,
	         fecha_registro                                   DATETIME NOT NULL,
	         fecha_actualizacion                              DATETIME DEFAULT NULL,
	         fecha_anulacion                                  DATETIME DEFAULT NULL,
);
GO

CREATE TABLE Venta (
			 id												  INT PRIMARY KEY IDENTITY,
			 id_usuario                                       INT NOT NULL, 
			 id_cliente                                       INT DEFAULT NULL, 
			 numero_venta									  VARCHAR(10) NOT NULL UNIQUE,
			 fecha_venta									  DATETIME NOT NULL,
             precio_total									  DECIMAL(10,2) NOT NULL,
			 id_boletaFactura							      VARCHAR(200) DEFAULT NULL,
			 urlboletaFactura								  VARCHAR(MAX) DEFAULT NULL,
			 nota_adicional									  NVARCHAR(500) DEFAULT NULL,
			 activo							                  BIT NOT NULL,
	         estado							                  BIT NOT NULL,
			 motivo_anulacion                                 NVARCHAR(200) DEFAULT NULL,
	         fecha_registro                                   DATETIME NOT NULL,
	         fecha_actualizacion                              DATETIME DEFAULT NULL,
	         fecha_anulacion                                  DATETIME DEFAULT NULL,      
	         FOREIGN KEY (id_cliente)						  REFERENCES Cliente(id),
			 FOREIGN KEY (id_usuario)						  REFERENCES Usuario(id),
);
GO
 
CREATE TABLE DetalleVenta (
			 id												  INT PRIMARY KEY IDENTITY,
			 id_venta									      INT NOT NULL,   
 		     id_producto									  INT NOT NULL,   
			 nombre_producto                                  NVARCHAR(500) DEFAULT NULL,
			 color_producto						              NVARCHAR(25)  NOT NULL,
             nombre_categoria								  NVARCHAR(100) NOT NULL,
			 color_categoria						          NVARCHAR(25)  NOT NULL,
			 nombre_marca                                     NVARCHAR(500) DEFAULT NULL,
			 color_marca						              NVARCHAR(25)  NOT NULL,
             cantidad                                         INT NOT NULL, 
             precio_compra									  DECIMAL(10,2) NOT NULL,
             precio_venta								      DECIMAL(10,2) NOT NULL,
			 precio_total								      DECIMAL(10,2) NOT NULL,
			 urlfoto_producto								  VARCHAR(MAX) DEFAULT NULL,
			 activo							                  BIT NOT NULL,
	         estado							                  BIT NOT NULL,
	         fecha_registro                                   DATETIME NOT NULL,
	         fecha_actualizacion                              DATETIME DEFAULT NULL,
	         fecha_anulacion                                  DATETIME DEFAULT NULL,      
			 FOREIGN KEY (id_venta)                           REFERENCES Venta(id),
			 FOREIGN KEY (id_producto)                        REFERENCES Producto(id),
);
GO
 
CREATE TABLE Negocio (
			 id												  INT PRIMARY KEY IDENTITY,
			 id_moneda									      INT NOT NULL,   
             razon_social									  VARCHAR(100) NOT NULL,
             ruc											  VARCHAR(20) NOT NULL,
			 direccion										  NVARCHAR(100)  DEFAULT NULL,
		     celular										  VARCHAR(15)  DEFAULT NULL,                   
  	         correo_electronico                               NVARCHAR(100) DEFAULT NULL,
			 id_foto										  VARCHAR(200) DEFAULT NULL,
			 urlfoto										  VARCHAR(MAX) DEFAULT NULL,
			 color_boleta_factura						      NVARCHAR(25)  NOT NULL,
			 formato_impresion                                CHAR(3) NOT NULL,
	         fecha_registro                                   DATETIME NOT NULL,
	         fecha_actualizacion                              DATETIME DEFAULT NULL,
	         fecha_anulacion                                  DATETIME DEFAULT NULL, 
		     FOREIGN KEY (id_moneda)                          REFERENCES Moneda(id),
);
GO

CREATE TABLE Parametro (
			 id												  INT PRIMARY KEY IDENTITY,
			 nombre											  VARCHAR(200) NOT NULL,
			 descripcion									  VARCHAR(500)  NULL,
			 activo							                  BIT NOT NULL,
	         estado							                  BIT NOT NULL,
	         fecha_registro                                   DATETIME NOT NULL,
	         fecha_actualizacion                              DATETIME DEFAULT NULL,
	         fecha_anulacion                                  DATETIME DEFAULT NULL, 
);
GO
 
CREATE TABLE ParametroDetalle (
			 id												  INT PRIMARY KEY IDENTITY,
	         id_parametro									  INT NOT NULL,
	         para_key										  VARCHAR(200) NOT NULL,
			 sub_para_key								      VARCHAR(200) NOT NULL,
			 nombre											  VARCHAR(100) NOT NULL,
			 descripcion									  VARCHAR(500) NULL,
			 tipocampo										  VARCHAR(50) NULL,
	         orden											  INT NULL,
	         svalor1										  VARCHAR(500) NULL,
	         svalor2										  VARCHAR(500) NULL,
	         svalor3										  VARCHAR(500) NULL,
	         dvalor1										  DECIMAL(18, 4) NULL,
	         dvalor2										  DECIMAL(18, 4) NULL,
	         dvalor3										  DECIMAL(18, 4) NULL,
			 fvalor1									      DATETIME NULL,
			 fvalor2									      DATETIME NULL,
			 fvalor3									      DATETIME NULL,
			 bvalor1									      BIT NULL,
			 bvalor2									      BIT NULL,
			 bvalor3									      BIT NULL,
			 activo							                  BIT NOT NULL,
	         estado							                  BIT NOT NULL,
	         fecha_registro                                   DATETIME NOT NULL,
	         fecha_actualizacion                              DATETIME DEFAULT NULL,
	         fecha_anulacion                                  DATETIME DEFAULT NULL, 
			 FOREIGN KEY (id_parametro)                     REFERENCES Parametro(id) 
);
GO
 