USE [Banco]
GO
/****** Object:  Table [dbo].[T_Clientes]    Script Date: 28/8/2024 04:01:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Clientes](
	[nombre] [varchar](10) NULL,
	[apellido] [varchar](10) NULL,
	[dni] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[dni] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_Cuentas]    Script Date: 28/8/2024 04:01:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Cuentas](
	[cbu] [int] NOT NULL,
	[saldo] [decimal](18, 0) NULL,
	[tipo_cuenta] [int] NULL,
	[ultimoMovimiento] [varchar](10) NOT NULL,
	[cliente_dni] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[cbu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_Tipo_cuentas]    Script Date: 28/8/2024 04:01:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Tipo_cuentas](
	[id] [int] NOT NULL,
	[nombre] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[T_Cuentas]  WITH CHECK ADD  CONSTRAINT [T_Cuentas_cliente_dni_fk] FOREIGN KEY([cliente_dni])
REFERENCES [dbo].[T_Clientes] ([dni])
GO
ALTER TABLE [dbo].[T_Cuentas] CHECK CONSTRAINT [T_Cuentas_cliente_dni_fk]
GO
ALTER TABLE [dbo].[T_Cuentas]  WITH CHECK ADD  CONSTRAINT [T_Cuentas_tipo_cuenta_fk] FOREIGN KEY([tipo_cuenta])
REFERENCES [dbo].[T_Tipo_cuentas] ([id])
GO
ALTER TABLE [dbo].[T_Cuentas] CHECK CONSTRAINT [T_Cuentas_tipo_cuenta_fk]
GO
/****** Object:  StoredProcedure [dbo].[SP_BORRAR_CUENTA]    Script Date: 28/8/2024 04:01:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_BORRAR_CUENTA]
	@cbu int
AS
BEGIN
	DELETE FROM T_Cuentas WHERE cbu = @cbu;
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CREAR_BD]    Script Date: 28/8/2024 04:01:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_CREAR_BD] 

AS
BEGIN
	
CREATE TABLE T_Clientes (
  nombre varchar(10),
  apellido varchar(10),
  dni int NOT NULL PRIMARY KEY
);


CREATE TABLE T_Cuentas (
  cbu int NOT NULL PRIMARY KEY,
  saldo decimal,
  tipo_cuenta int,
  ultimoMovimiento varchar(10) NOT NULL,
  cliente_dni int NOT NULL
);


CREATE TABLE T_Tipo_cuentas (
  id int NOT NULL PRIMARY KEY,
  nombre varchar(10)
);


ALTER TABLE T_Cuentas ADD CONSTRAINT T_Cuentas_tipo_cuenta_fk FOREIGN KEY (tipo_cuenta) REFERENCES T_Tipo_cuentas (id);
ALTER TABLE T_Cuentas ADD CONSTRAINT T_Cuentas_cliente_dni_fk FOREIGN KEY (cliente_dni) REFERENCES T_Clientes (dni);

END
GO
/****** Object:  StoredProcedure [dbo].[SP_GUARDAR_CUENTA]    Script Date: 28/8/2024 04:01:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GUARDAR_CUENTA]
@cbu int ,
@saldo decimal,
@tipo_cuenta int,
@ultimoMovimiento varchar(20),
@cliente_dni int
AS
BEGIN 
	IF @cbu = 0
		BEGIN
			insert into T_Cuentas (cbu, saldo, tipo_cuenta, ultimoMovimiento, cliente_dni) 
			values (@cbu,@saldo,@tipo_cuenta,@ultimoMovimiento,@cliente_dni)	
		END
	ELSE
		BEGIN
			update T_Cuentas
			set saldo=@saldo, tipo_cuenta=@tipo_cuenta, ultimoMovimiento=@ultimoMovimiento, cliente_dni=@cliente_dni 
			where cbu=@cbu
		END
END
GO
/****** Object:  StoredProcedure [dbo].[SP_RECUPERAR_CLIENTES]    Script Date: 28/8/2024 04:01:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_RECUPERAR_CLIENTES] 
AS
BEGIN
	SELECT * FROM T_CLIENTES
	--SELECT * FROM CLIENTES CL JOIN CUENTAS CU ON CU.CBU = CL.ID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_RECUPERAR_CLIENTES_POR_DNI]    Script Date: 28/8/2024 04:01:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_RECUPERAR_CLIENTES_POR_DNI]
	@dni int
AS
BEGIN
	SELECT * FROM T_CLIENTES WHERE dni = @dni;
END
GO
/****** Object:  StoredProcedure [dbo].[SP_RECUPERAR_CUENTAS]    Script Date: 28/8/2024 04:01:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_RECUPERAR_CUENTAS]
AS
BEGIN
	SELECT * FROM T_Cuentas
	--SELECT * FROM CLIENTES CL JOIN CUENTAS CU ON CU.CBU = CL.ID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_RECUPERAR_CUENTAS_POR_CBU]    Script Date: 28/8/2024 04:01:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_RECUPERAR_CUENTAS_POR_CBU]
	@cbu int
AS
BEGIN
	SELECT * FROM T_Cuentas WHERE cbu = @cbu;
END
GO
/****** Object:  StoredProcedure [dbo].[SP_RECUPERAR_TIPOS]    Script Date: 28/8/2024 04:01:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_RECUPERAR_TIPOS] 
AS
BEGIN
	SELECT * FROM T_Tipo_Cuentas
	--SELECT * FROM CLIENTES CL JOIN CUENTAS CU ON CU.CBU = CL.ID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_RECUPERAR_TIPOS_POR_ID]    Script Date: 28/8/2024 04:01:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_RECUPERAR_TIPOS_POR_ID]
	@id int
AS
BEGIN
	SELECT * FROM T_Tipo_Cuentas WHERE id = @id;
END
GO
