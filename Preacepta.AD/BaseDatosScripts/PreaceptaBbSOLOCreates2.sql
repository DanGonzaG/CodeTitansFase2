/****** IMPORTANTE ******/
/****** Este scritp contiene los querys para crear la tablas es el archivo definitivo para implatar la base de datos en un ambiente nuevo ******/


/****** Crea la base de datos completa del proyecto AppPreacepta del despacho de Preacepta Studio ******/
Create database PreaceptaBD;
Use PreaceptaBD;

/*Creacion de rol y asignacion de privilegios NO APLICAR DE NO SE NECESARIO*/
CREATE USER [PreaceptaApp] FROM EXTERNAL PROVIDER;
ALTER ROLE db_datareader ADD MEMBER [PreaceptaApp];
ALTER ROLE db_datawriter ADD MEMBER [PreaceptaApp];


/****** MODULO DE AUTENTICACION ******/

/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 08-Aug-25 8:12:11 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO



/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 08-Aug-25 8:12:00 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO


/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 08-Aug-25 8:11:54 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO

/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 08-Aug-25 8:11:47 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 08-Aug-25 8:11:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 08-Aug-25 8:12:18 PM ******/
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO

/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 08-Aug-25 8:12:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO

ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO



/*-----------------------------------------------------------------------------------------*/
/********************** CREACION DE TABLAS PARA GESTION DE PRAECEPTA **********************/
/*---------------------------------------------------------------------------------------*/


/*********************************** Tablas catagolo **************************************/

/****** Object:  Table [dbo].[T_CrProvincias]    Script Date: 03-Oct-25 6:15:34 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_CrProvincias](
	[IdProvincia] [int] NOT NULL,
	[NombreProvincia] [nvarchar](100) NULL,
 CONSTRAINT [PK_T_provincia] PRIMARY KEY CLUSTERED 
(
	[IdProvincia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[T_CrCantones]    Script Date: 03-Oct-25 6:17:26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_CrCantones](
	[idCanton] [int] NOT NULL,
	[idProvincia] [int] NOT NULL,
	[NombreCanton] [nvarchar](100) NULL,
 CONSTRAINT [PK_T_Cantones] PRIMARY KEY CLUSTERED 
(
	[idCanton] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[T_CrCantones]  WITH CHECK ADD  CONSTRAINT [FK_T_CrCantones_T_CrProvincias] FOREIGN KEY([idProvincia])
REFERENCES [dbo].[T_CrProvincias] ([IdProvincia])
GO

ALTER TABLE [dbo].[T_CrCantones] CHECK CONSTRAINT [FK_T_CrCantones_T_CrProvincias]
GO

/****** Object:  Table [dbo].[T_CrDistritos]    Script Date: 03-Oct-25 6:17:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_CrDistritos](
	[idDistrito] [int] NOT NULL,
	[idCaton] [int] NOT NULL,
	[nombreDistrito] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_T_Distritos] PRIMARY KEY CLUSTERED 
(
	[idDistrito] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[T_CrDistritos]  WITH CHECK ADD  CONSTRAINT [FK_T_CrDistritos_T_CrCantones] FOREIGN KEY([idCaton])
REFERENCES [dbo].[T_CrCantones] ([idCanton])
GO

ALTER TABLE [dbo].[T_CrDistritos] CHECK CONSTRAINT [FK_T_CrDistritos_T_CrCantones]
GO

/****** TABLAS CATALOGO MODULO DE PERSONAS ******/
/****** Object:  Table [dbo].[T_GeAbogadoTipo]    Script Date: 03-Oct-25 6:19:47 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_GeAbogadoTipo](
	[Id_TipoAbogado] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_T_GeAbogadoTipo] PRIMARY KEY CLUSTERED 
(
	[Id_TipoAbogado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** TABLAS CATALOGO MODULO DE CASOS ******/
/****** Object:  Table [dbo].[T_CasosTipos]    Script Date: 03-Oct-25 6:24:01 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_CasosTipos](
	[Id_TipoCaso] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_T_TipoCasos] PRIMARY KEY CLUSTERED 
(
	[Id_TipoCaso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** TABLAS CATALOGO MODULO DE CITAS ******/
/****** Object:  Table [dbo].[T_CitasTipos]    Script Date: 03-Oct-25 6:30:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_CitasTipos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_T_TipoCita] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** TABLAS CATALOGO MODULO DE DOCS LEGALES AUTOMATIZADOS ******/
/****** Object:  Table [dbo].[T_DocsMarcaVehiculos]    Script Date: 03-Oct-25 6:35:45 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_DocsMarcaVehiculos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_T_MarcaVehiculo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[T_DocsTipoVehiculos]    Script Date: 03-Oct-25 6:36:18 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_DocsTipoVehiculos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_T_TipoVehiculo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[T_DocsCombustibles]    Script Date: 03-Oct-25 6:36:49 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_DocsCombustibles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50)NOT NULL,
 CONSTRAINT [PK_T_Combustible] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/*********************************** Resto de tablas **************************************/

/****** TABLAS GENERALES ******/

/****** Object:  Table [dbo].[T_GeNegocios]    Script Date: 03-Oct-25 6:18:46 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_GeNegocios](
	[C_Juridica] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Telefono] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Representante] [nvarchar](100) NULL,
	[Fecha_Consolidacion] [date] NOT NULL,
	[Direccion1] [int] NOT NULL,
	[Direccion2] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_T_Negocios] PRIMARY KEY CLUSTERED 
(
	[C_Juridica] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[T_GePersonas]    Script Date: 16-Nov-25 10:08:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_GePersonas](
	[Cedula] [int] IDENTITY(1,1) NOT NULL,
	[Tipo_Identificacion] [nvarchar](100) NOT NULL,
	[Num_Cedula] [nvarchar](100) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Apellido1] [nvarchar](50) NOT NULL,
	[Apellido2] [nvarchar](50) NOT NULL,
	[FechaNacimiento] [date] NOT NULL,
	[Edad] [int] NOT NULL,
	[EstadoCivil] [nvarchar](50) NOT NULL,
	[Oficio] [nvarchar](50) NOT NULL,
	[Direccion1] [int] NOT NULL,
	[Direccion2] [nvarchar](500) NOT NULL,
	[Telefono1] [nvarchar](50) NOT NULL,
	[Telefono2] [nvarchar](50) NULL,		
	[Email] [nvarchar](100) NOT NULL,
	[Genero] [nvarchar](10) NOT NULL,
	[FechaRegistro] [datetime] NOT NULL,
	[ExpirationPassword] [datetime] NOT NULL,
	[Activo] [bit] NOT NULL,
	
	
 CONSTRAINT [PK_T_Pesonas] PRIMARY KEY CLUSTERED 
(
	[Cedula] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[T_GePersonas]  WITH CHECK ADD  CONSTRAINT [FK_T_GePersonas_T_CrDistritos] FOREIGN KEY([Direccion1])
REFERENCES [dbo].[T_CrDistritos] ([idDistrito])
GO

ALTER TABLE [dbo].[T_GePersonas] CHECK CONSTRAINT [FK_T_GePersonas_T_CrDistritos]
GO



/****** Object:  Table [dbo].[T_GeAbogados]    Script Date: 03-Oct-25 6:20:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_GeAbogados](
	[Carnet] [int] NOT NULL,
	[Cedula] [int] NOT NULL,
	[Id_TipoAbogado] [int] NOT NULL,
	[C_Juridica] [int] NOT NULL,
 CONSTRAINT [PK_T_GeAbogados] PRIMARY KEY CLUSTERED 
(
	[Cedula] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[T_GeAbogados]  WITH CHECK ADD  CONSTRAINT [FK_T_GeAbogados_T_GeAbogadoTipo] FOREIGN KEY([Id_TipoAbogado])
REFERENCES [dbo].[T_GeAbogadoTipo] ([Id_TipoAbogado])
GO

ALTER TABLE [dbo].[T_GeAbogados] CHECK CONSTRAINT [FK_T_GeAbogados_T_GeAbogadoTipo]
GO

ALTER TABLE [dbo].[T_GeAbogados]  WITH CHECK ADD  CONSTRAINT [FK_T_GeAbogados_T_GeNegocios] FOREIGN KEY([C_Juridica])
REFERENCES [dbo].[T_GeNegocios] ([C_Juridica])
GO

ALTER TABLE [dbo].[T_GeAbogados] CHECK CONSTRAINT [FK_T_GeAbogados_T_GeNegocios]
GO

ALTER TABLE [dbo].[T_GeAbogados]  WITH CHECK ADD  CONSTRAINT [FK_T_GeAbogados_T_GePersonas] FOREIGN KEY([Cedula])
REFERENCES [dbo].[T_GePersonas] ([Cedula])
GO

ALTER TABLE [dbo].[T_GeAbogados] CHECK CONSTRAINT [FK_T_GeAbogados_T_GePersonas]
GO

/****** Object:  Table [dbo].[T_GeRedesSociales]    Script Date: 03-Oct-25 6:20:51 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_GeRedesSociales](
	[Id_Rs] [int] IDENTITY(1,1) NOT NULL,
	[Cedula] [int] NOT NULL,
	[LinkRedSocila] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_T_GeRedesSociales] PRIMARY KEY CLUSTERED 
(
	[Id_Rs] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[T_GeRedesSociales]  WITH CHECK ADD  CONSTRAINT [FK_T_GeRedesSociales_T_GeAbogados] FOREIGN KEY([Cedula])
REFERENCES [dbo].[T_GeAbogados] ([Cedula])
GO

ALTER TABLE [dbo].[T_GeRedesSociales] CHECK CONSTRAINT [FK_T_GeRedesSociales_T_GeAbogados]
GO

/****** MODULO DE CASOS ******/

/****** Object:  Table [dbo].[T_Casos]    Script Date: 03-Oct-25 6:24:08 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_Casos](
	[Id_caso] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[Id_TipoCaso] [int] NOT NULL,
	[Descripcion] [nvarchar](max) NOT NULL,
	[Id_Abogado] [int] NOT NULL,
	[Id_Cliente] [int] NOT NULL,
	[Activo] [bit] NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Pruebas] [nvarchar](500)NOT NULL,
 CONSTRAINT [PK_T_Casos] PRIMARY KEY CLUSTERED 
(
	[Id_caso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[T_Casos]  WITH CHECK ADD  CONSTRAINT [FK_T_Casos_T_CasosTipos] FOREIGN KEY([Id_TipoCaso])
REFERENCES [dbo].[T_CasosTipos] ([Id_TipoCaso])
GO

ALTER TABLE [dbo].[T_Casos] CHECK CONSTRAINT [FK_T_Casos_T_CasosTipos]
GO

ALTER TABLE [dbo].[T_Casos]  WITH CHECK ADD  CONSTRAINT [FK_T_Casos_T_GeAbogados] FOREIGN KEY([Id_Abogado])
REFERENCES [dbo].[T_GeAbogados] ([Cedula])
GO

ALTER TABLE [dbo].[T_Casos] CHECK CONSTRAINT [FK_T_Casos_T_GeAbogados]
GO

ALTER TABLE [dbo].[T_Casos]  WITH CHECK ADD  CONSTRAINT [FK_T_Casos_T_GePersonas] FOREIGN KEY([Id_Cliente])
REFERENCES [dbo].[T_GePersonas] ([Cedula])
GO

ALTER TABLE [dbo].[T_Casos] CHECK CONSTRAINT [FK_T_Casos_T_GePersonas]
GO

/****** Object:  Table [dbo].[T_CasosEvidencias]    Script Date: 03-Oct-25 6:24:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_CasosEvidencias](
	[Id_Evidencia] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [nvarchar](100) NOT NULL,
	[Id_caso] [int] NOT NULL,
	[Archivo] [nvarchar](max) NULL,
 CONSTRAINT [PK_T_CasosEvidencias] PRIMARY KEY CLUSTERED 
(
	[Id_Evidencia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[T_CasosEvidencias]  WITH CHECK ADD  CONSTRAINT [FK_T_CasosEvidencias_T_Casos] FOREIGN KEY([Id_caso])
REFERENCES [dbo].[T_Casos] ([Id_caso])
GO

ALTER TABLE [dbo].[T_CasosEvidencias] CHECK CONSTRAINT [FK_T_CasosEvidencias_T_Casos]
GO

/*ALTER TABLE [dbo].[T_CasosEvidencias]  WITH CHECK ADD  CONSTRAINT [FK_T_CasosEvidencias_T_CasosEtapas] FOREIGN KEY([Id_caso])
REFERENCES [dbo].[T_CasosEtapas] ([Id_EtapaPL])
GO*/

ALTER TABLE [dbo].[T_CasosEvidencias] CHECK CONSTRAINT [FK_T_CasosEvidencias_T_CasosEtapas]
GO

/****** Object:  Table [dbo].[T_CasosEtapas]    Script Date: 03-Oct-25 6:24:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_CasosEtapas](
	[Id_EtapaPL] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Descripcion] [nvarchar](max) NOT NULL,
	[Id_Caso] [int] NOT NULL,	
	[Pruebas] [nvarchar](500)NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_T_EtapaPL] PRIMARY KEY CLUSTERED 
(
	[Id_EtapaPL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[T_CasosEtapas]  WITH CHECK ADD  CONSTRAINT [FK_T_CasosEtapas_T_Casos] FOREIGN KEY([Id_Caso])
REFERENCES [dbo].[T_Casos] ([Id_caso])
GO

ALTER TABLE [dbo].[T_CasosEtapas] CHECK CONSTRAINT [FK_T_CasosEtapas_T_Casos]
GO

/****** MODULO DE CITAS ******/

/****** Object:  Table [dbo].[T_Citas]    Script Date: 04-Oct-25 3:56:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_Citas](
	[Id_Cita] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [date] NOT NULL,
	[Hora] [time](7) NOT NULL,
	[Id_TipoCita] [int] NOT NULL,
	[Anfitrion] [int] NOT NULL,
	[LinkVideo] [nvarchar](max) NULL,
	[Estado] [int] NOT NULL,
 CONSTRAINT [PK_T_Citas] PRIMARY KEY CLUSTERED 
(
	[Id_Cita] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[T_Citas] ADD  CONSTRAINT [DF_T_Citas_Estado]  DEFAULT ((0)) FOR [Estado]
GO

ALTER TABLE [dbo].[T_Citas]  WITH CHECK ADD  CONSTRAINT [FK_T_Citas_T_CitasTipos] FOREIGN KEY([Id_TipoCita])
REFERENCES [dbo].[T_CitasTipos] ([Id])
GO

ALTER TABLE [dbo].[T_Citas] CHECK CONSTRAINT [FK_T_Citas_T_CitasTipos]
GO

ALTER TABLE [dbo].[T_Citas]  WITH CHECK ADD  CONSTRAINT [FK_T_Citas_T_GeAbogados] FOREIGN KEY([Anfitrion])
REFERENCES [dbo].[T_GeAbogados] ([Cedula])
GO

ALTER TABLE [dbo].[T_Citas] CHECK CONSTRAINT [FK_T_Citas_T_GeAbogados]
GO

/****** Object:  Table [dbo].[T_CitasClientes]    Script Date: 03-Oct-25 6:32:00 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_CitasClientes](
	[Id_CiCliente] [int] IDENTITY(1,1) NOT NULL,
	[Id_Cita] [int] NOT NULL,
	[Id_Cliente] [int] NOT NULL,
 CONSTRAINT [PK_T_CitasClientes] PRIMARY KEY CLUSTERED 
(
	[Id_CiCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[T_CitasClientes]  WITH CHECK ADD  CONSTRAINT [FK_T_CitasClientes_T_Citas] FOREIGN KEY([Id_Cita])
REFERENCES [dbo].[T_Citas] ([Id_Cita])
GO

ALTER TABLE [dbo].[T_CitasClientes] CHECK CONSTRAINT [FK_T_CitasClientes_T_Citas]
GO

ALTER TABLE [dbo].[T_CitasClientes]  WITH CHECK ADD  CONSTRAINT [FK_T_CitasClientes_T_GePersonas] FOREIGN KEY([Id_Cliente])
REFERENCES [dbo].[T_GePersonas] ([Cedula])
GO

ALTER TABLE [dbo].[T_CitasClientes] CHECK CONSTRAINT [FK_T_CitasClientes_T_GePersonas]
GO

/****** Object:  Table [dbo].[T_DocumentosCita]    Script Date: 29-Nov-25 12:51:30 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_DocumentosCita](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdCita] [int] NOT NULL,
	[NombreArchivo] [nvarchar](255) NULL,
	[RutaArchivo] [nvarchar](500) NULL,
	[FechaSubida] [datetime] NULL,
	[Descargar] [bit] NOT NULL,
	[Activo] [bit] NOT NULL,
	[OwnerId] [nvarchar](450) NOT NULL,
	[IV] [nvarchar](256) NOT NULL,
	[Algoritmo] [nvarchar](100) NOT NULL,
	[ContentType] [nvarchar](200) NOT NULL,
	[ArchivoCifrado] [varbinary](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[T_DocumentosCita] ADD  DEFAULT (getdate()) FOR [FechaSubida]
GO

ALTER TABLE [dbo].[T_DocumentosCita] ADD  DEFAULT ((1)) FOR [Descargar]
GO

ALTER TABLE [dbo].[T_DocumentosCita] ADD  DEFAULT ((1)) FOR [Activo]
GO

ALTER TABLE [dbo].[T_DocumentosCita]  WITH CHECK ADD FOREIGN KEY([IdCita])
REFERENCES [dbo].[T_Citas] ([Id_Cita])
GO


/****** Object:  Table [dbo].[T_DocumentoKey]    Script Date: 29-Nov-25 12:52:08 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_DocumentoKey](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DocumentoId] [int] NOT NULL,
	[UsuarioId] [nvarchar](450) NOT NULL,
	[EncryptedKeyBase64] [nvarchar](max) NOT NULL,
	[IV] [nvarchar](256) NOT NULL,
	[Activo] [bit] NOT NULL,
	[FechaCreacion] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[T_DocumentoKey] ADD  DEFAULT ((1)) FOR [Activo]
GO

ALTER TABLE [dbo].[T_DocumentoKey] ADD  DEFAULT (sysutcdatetime()) FOR [FechaCreacion]
GO

ALTER TABLE [dbo].[T_DocumentoKey]  WITH CHECK ADD  CONSTRAINT [FK_T_DocumentoKey_TDocumentosCita] FOREIGN KEY([DocumentoId])
REFERENCES [dbo].[T_DocumentosCita] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[T_DocumentoKey] CHECK CONSTRAINT [FK_T_DocumentoKey_TDocumentosCita]
GO

CREATE INDEX IX_T_DocumentoKey_DocumentoId ON T_DocumentoKey (DocumentoId);
CREATE INDEX IX_T_DocumentoKey_UsuarioId ON T_DocumentoKey (UsuarioId);


/****** Object:  Table [dbo].[TClavePublica]    Script Date: 29-Nov-25 12:55:31 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TClavePublica](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioId] [nvarchar](450) NOT NULL,
	[ClavePublica] [nvarchar](max) NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[Activo] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[TClavePublica] ADD  DEFAULT (getdate()) FOR [FechaCreacion]
GO

ALTER TABLE [dbo].[TClavePublica] ADD  DEFAULT ((1)) FOR [Activo]
GO

CREATE INDEX IX_TClavePublica_UsuarioId_Activo ON TClavePublica(UsuarioId, Activo);


/****** TABLAS MODULO DE GENERADOR DE DOCUMENTOS AUTOMATIZADOS ******/
/****** Object:  Table [dbo].[HistorialDocumentos]    Script Date: 03-Oct-25 6:34:37 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[HistorialDocumentos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [date] NOT NULL,
	[Titulo] [nvarchar](200) NOT NULL,
	[Cliente] [int]NOT NULL,
	[Abogado] [int]NOT NULL,
	[TipoDocumento] [varchar](100) NOT NULL,
	[IdDocumento] [int] NOT NULL,	
 CONSTRAINT [PK_HistorialDocumentos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[HistorialDocumentos] ADD  CONSTRAINT [DF_Historial_Fecha]  DEFAULT (CONVERT([date],getdate())) FOR [Fecha]
GO

ALTER TABLE [dbo].[HistorialDocumentos]  WITH CHECK ADD  CONSTRAINT [FK_Historial_Abogado] FOREIGN KEY([Abogado])
REFERENCES [dbo].[T_GeAbogados] ([Cedula])
GO

ALTER TABLE [dbo].[HistorialDocumentos] CHECK CONSTRAINT [FK_Historial_Abogado]
GO

ALTER TABLE [dbo].[HistorialDocumentos]  WITH CHECK ADD  CONSTRAINT [FK_Historial_Cliente] FOREIGN KEY([Cliente])
REFERENCES [dbo].[T_GePersonas] ([Cedula])
GO

ALTER TABLE [dbo].[HistorialDocumentos] CHECK CONSTRAINT [FK_Historial_Cliente]
GO

/****** Object:  Table [dbo].[T_DocsAutorizacionRevisionExpediente]    Script Date: 03-Oct-25 6:37:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_DocsAutorizacionRevisionExpediente](
	[ID_Documento] [int] IDENTITY(1,1) NOT NULL,
	[expediente] [varchar](50) NOT NULL,
	[delito] [varchar](100) NOT NULL,
	[cedula_imputado] [int] NOT NULL,
	[ofendido] [varchar](150) NOT NULL,
	[cedula_abogado] [int] NOT NULL,
	[cedula_asistente] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Documento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[T_DocsAutorizacionRevisionExpediente]  WITH CHECK ADD  CONSTRAINT [FK_TDARE_cedula_abogado] FOREIGN KEY([cedula_abogado])
REFERENCES [dbo].[T_GeAbogados] ([Cedula])
GO

ALTER TABLE [dbo].[T_DocsAutorizacionRevisionExpediente] CHECK CONSTRAINT [FK_TDARE_cedula_abogado]
GO

ALTER TABLE [dbo].[T_DocsAutorizacionRevisionExpediente]  WITH CHECK ADD  CONSTRAINT [FK_TDARE_cedula_asistente] FOREIGN KEY([cedula_asistente])
REFERENCES [dbo].[T_GePersonas] ([Cedula])
GO

ALTER TABLE [dbo].[T_DocsAutorizacionRevisionExpediente] CHECK CONSTRAINT [FK_TDARE_cedula_asistente]
GO

ALTER TABLE [dbo].[T_DocsAutorizacionRevisionExpediente]  WITH CHECK ADD  CONSTRAINT [FK_TDARE_cedula_imputado] FOREIGN KEY([cedula_imputado])
REFERENCES [dbo].[T_GePersonas] ([Cedula])
GO

ALTER TABLE [dbo].[T_DocsAutorizacionRevisionExpediente] CHECK CONSTRAINT [FK_TDARE_cedula_imputado]
GO

/****** Object:  Table [dbo].[T_DocsCompraventaFinca]    Script Date: 03-Oct-25 6:37:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_DocsCompraventaFinca](
	[ID_Documento] [int] IDENTITY(1,1) NOT NULL,
	[numero_escritura] [varchar](50) NOT NULL,
	[cedula_abogado] [int] NOT NULL,
	[cedula_vendedor] [int] NOT NULL,
	[cedula_comprador] [int] NOT NULL,
	[monto_venta] [decimal](15, 2) NOT NULL,
	[partido_finca] [varchar](50) NOT NULL,
	[matricula_finca] [varchar](50) NOT NULL,
	[naturaleza_finca] [varchar](100) NOT NULL,
	[distrito_finca] [int] NOT NULL,
	[canton_finca] [int] NOT NULL,
	[provincia_finca] [int] NOT NULL,
	[area_finca_m2] [decimal](10, 2) NOT NULL,
	[plano_catastrado] [varchar](100) NOT NULL,
	[colinda_norte] [text] NOT NULL,
	[colinda_sur] [text] NOT NULL,
	[colinda_este] [text] NOT NULL,
	[colinda_oeste] [text] NOT NULL,
	[forma_pago] [varchar](100) NOT NULL,
	[medio_pago] [varchar](100) NOT NULL,
	[origen_fondos] [text] NOT NULL,
	[lugar_firma] [int] NOT NULL,
	[hora_firma] [time](7) NOT NULL,
	[fecha_firma] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Documento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[T_DocsCompraventaFinca]  WITH CHECK ADD  CONSTRAINT [FK_TDCF_cedula_abogado] FOREIGN KEY([cedula_abogado])
REFERENCES [dbo].[T_GeAbogados] ([Cedula])
GO

ALTER TABLE [dbo].[T_DocsCompraventaFinca] CHECK CONSTRAINT [FK_TDCF_cedula_abogado]
GO

ALTER TABLE [dbo].[T_DocsCompraventaFinca]  WITH CHECK ADD  CONSTRAINT [FK_TDCF_cedula_comprador] FOREIGN KEY([cedula_comprador])
REFERENCES [dbo].[T_GePersonas] ([Cedula])
GO

ALTER TABLE [dbo].[T_DocsCompraventaFinca] CHECK CONSTRAINT [FK_TDCF_cedula_comprador]
GO

ALTER TABLE [dbo].[T_DocsCompraventaFinca]  WITH CHECK ADD  CONSTRAINT [FK_TDCF_cedula_vendedor] FOREIGN KEY([cedula_vendedor])
REFERENCES [dbo].[T_GePersonas] ([Cedula])
GO

ALTER TABLE [dbo].[T_DocsCompraventaFinca] CHECK CONSTRAINT [FK_TDCF_cedula_vendedor]
GO

ALTER TABLE [dbo].[T_DocsCompraventaFinca]  WITH CHECK ADD  CONSTRAINT [FK_TDCF_distrito_finca] FOREIGN KEY([distrito_finca])
REFERENCES [dbo].[T_CrDistritos] ([idDistrito])
GO

ALTER TABLE [dbo].[T_DocsCompraventaFinca] CHECK CONSTRAINT [FK_TDCF_distrito_finca]
GO

ALTER TABLE [dbo].[T_DocsCompraventaFinca]  WITH CHECK ADD  CONSTRAINT [FK_TDCF_lugar_firma] FOREIGN KEY([lugar_firma])
REFERENCES [dbo].[T_CrDistritos] ([idDistrito])
GO

ALTER TABLE [dbo].[T_DocsCompraventaFinca] CHECK CONSTRAINT [FK_TDCF_lugar_firma]
GO

/****** Object:  Table [dbo].[T_DocsContratoPrestacionServicios]    Script Date: 03-Oct-25 6:38:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_DocsContratoPrestacionServicios](
	[ID_Documento] [int] IDENTITY(1,1) NOT NULL,
	[razon_social_empresa] [varchar](255) NOT NULL,
	[provincia] [int] NOT NULL,
	[cedula_juridica_empresa] [varchar](20) NOT NULL,
	[cedula_abogado] [int] NOT NULL,
	[cedula_cliente] [int] NOT NULL,
	[tipo_servicios] [varchar](255) NOT NULL,
	[fecha_inicio] [date] NOT NULL,
	[fecha_final] [date] NOT NULL,
	[monto_honorarios] [decimal](10, 2) NOT NULL,
	[informacion_confidencial] [text] NOT NULL,
	[ciudad_firma] [int] NOT NULL,
	[hora_firma] [time](7) NOT NULL,
	[fecha_firma] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Documento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[T_DocsContratoPrestacionServicios]  WITH CHECK ADD  CONSTRAINT [FK_TDPS_cedula_abogado] FOREIGN KEY([cedula_abogado])
REFERENCES [dbo].[T_GeAbogados] ([Cedula])
GO

ALTER TABLE [dbo].[T_DocsContratoPrestacionServicios] CHECK CONSTRAINT [FK_TDPS_cedula_abogado]
GO

ALTER TABLE [dbo].[T_DocsContratoPrestacionServicios]  WITH CHECK ADD  CONSTRAINT [FK_TDPS_cedula_cliente] FOREIGN KEY([cedula_cliente])
REFERENCES [dbo].[T_GePersonas] ([Cedula])
GO

ALTER TABLE [dbo].[T_DocsContratoPrestacionServicios] CHECK CONSTRAINT [FK_TDPS_cedula_cliente]
GO

ALTER TABLE [dbo].[T_DocsContratoPrestacionServicios]  WITH CHECK ADD  CONSTRAINT [FK_TDPS_ciudad_firma] FOREIGN KEY([ciudad_firma])
REFERENCES [dbo].[T_CrDistritos] ([idDistrito])
GO

ALTER TABLE [dbo].[T_DocsContratoPrestacionServicios] CHECK CONSTRAINT [FK_TDPS_ciudad_firma]
GO

ALTER TABLE [dbo].[T_DocsContratoPrestacionServicios]  WITH CHECK ADD  CONSTRAINT [FK_TDPS_provincia] FOREIGN KEY([provincia])
REFERENCES [dbo].[T_CrProvincias] ([IdProvincia])
GO

ALTER TABLE [dbo].[T_DocsContratoPrestacionServicios] CHECK CONSTRAINT [FK_TDPS_provincia]
GO

/****** Object:  Table [dbo].[T_DocsInscripcionVehiculo]    Script Date: 03-Oct-25 6:39:10 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_DocsInscripcionVehiculo](
	[ID_Documento] [int] IDENTITY(1,1) NOT NULL,
	[cedula_cliente] [int] NOT NULL,
	[cedula_abogado] [int] NOT NULL,
	[marca_vehiculo] [int] NOT NULL,
	[estilo_vehiculo] [int] NOT NULL,
	[modelo_vehiculo] [int] NOT NULL,
	[categoria] [varchar](100) NOT NULL,
	[marca_motor] [varchar](100) NOT NULL,
	[numero_motor] [varchar](100) NOT NULL,
	[numero_serie_chasis] [varchar](100) NOT NULL,
	[vin] [varchar](100) NOT NULL,
	[anio] [int] NOT NULL,
	[carroceria] [varchar](100) NOT NULL,
	[peso_neto] [decimal](10, 2) NOT NULL,
	[peso_bruto] [decimal](10, 2) NOT NULL,
	[potencia] [decimal](10, 2) NOT NULL,
	[color] [varchar](50) NOT NULL,
	[capacidad] [int] NOT NULL,
	[combustible] [varchar](50) NOT NULL,
	[cilindraje] [varchar](50) NOT NULL,
	[lugar_firma] [int] NOT NULL,
	[fecha_firma] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Documento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[T_DocsInscripcionVehiculo]  WITH CHECK ADD  CONSTRAINT [FK_TDIV_cedula_abogado] FOREIGN KEY([cedula_abogado])
REFERENCES [dbo].[T_GeAbogados] ([Cedula])
GO

ALTER TABLE [dbo].[T_DocsInscripcionVehiculo] CHECK CONSTRAINT [FK_TDIV_cedula_abogado]
GO

ALTER TABLE [dbo].[T_DocsInscripcionVehiculo]  WITH CHECK ADD  CONSTRAINT [FK_TDIV_cedula_cliente] FOREIGN KEY([cedula_cliente])
REFERENCES [dbo].[T_GePersonas] ([Cedula])
GO

ALTER TABLE [dbo].[T_DocsInscripcionVehiculo] CHECK CONSTRAINT [FK_TDIV_cedula_cliente]
GO

ALTER TABLE [dbo].[T_DocsInscripcionVehiculo]  WITH CHECK ADD  CONSTRAINT [FK_TDIV_estilo_vehiculo] FOREIGN KEY([estilo_vehiculo])
REFERENCES [dbo].[T_DocsTipoVehiculos] ([Id])
GO

ALTER TABLE [dbo].[T_DocsInscripcionVehiculo] CHECK CONSTRAINT [FK_TDIV_estilo_vehiculo]
GO

ALTER TABLE [dbo].[T_DocsInscripcionVehiculo]  WITH CHECK ADD  CONSTRAINT [FK_TDIV_lugar_firma] FOREIGN KEY([lugar_firma])
REFERENCES [dbo].[T_CrDistritos] ([idDistrito])
GO

ALTER TABLE [dbo].[T_DocsInscripcionVehiculo] CHECK CONSTRAINT [FK_TDIV_lugar_firma]
GO

ALTER TABLE [dbo].[T_DocsInscripcionVehiculo]  WITH CHECK ADD  CONSTRAINT [FK_TDIV_marca_vehiculo] FOREIGN KEY([marca_vehiculo])
REFERENCES [dbo].[T_DocsMarcaVehiculos] ([Id])
GO

ALTER TABLE [dbo].[T_DocsInscripcionVehiculo] CHECK CONSTRAINT [FK_TDIV_marca_vehiculo]
GO

/****** Object:  Table [dbo].[T_DocsOpcionCompraventaVehiculo]    Script Date: 03-Oct-25 6:39:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_DocsOpcionCompraventaVehiculo](
	[ID_Documento] [int] IDENTITY(1,1) NOT NULL,
	[numero_escritura] [varchar](50) NOT NULL,
	[cedula_abogado] [int] NOT NULL,
	[cedula_propietario] [int] NOT NULL,
	[cedula_comprador] [int] NOT NULL,
	[placa_vehiculo] [varchar](20) NOT NULL,
	[marca_vehiculo] [int] NOT NULL,
	[tipo_vehiculo] [int] NOT NULL,
	[modelo_vehiculo] [varchar](100) NOT NULL,
	[carroceria] [varchar](100) NOT NULL,
	[categoria] [varchar](100) NOT NULL,
	[chasis] [varchar](100) NOT NULL,
	[serie] [varchar](100) NOT NULL,
	[vin] [varchar](100) NOT NULL,
	[marca_motor] [int] NOT NULL,
	[numero_motor] [varchar](100) NOT NULL,
	[color] [varchar](50) NOT NULL,
	[combustible] [int] NOT NULL,
	[anio] [int] NOT NULL,
	[capacidad] [varchar](50) NOT NULL,
	[cilindraje] [varchar](50) NOT NULL,
	[precio] [decimal](15, 2) NOT NULL,
	[moneda_precio] [varchar](10) NOT NULL,
	[plazo_opcion_anios] [int] NOT NULL,
	[fecha_inicio] [date] NOT NULL,
	[monto_senal] [decimal](15, 2) NOT NULL,
	[moneda_senal] [varchar](10) NOT NULL,
	[monto_a_devolver] [decimal](15, 2) NOT NULL,
	[monto_a_perder] [decimal](15, 2) NOT NULL,
	[moneda_monto_perdido] [varchar](10) NOT NULL,
	[gastos_traspaso_pagados_por] [varchar](150) NOT NULL,
	[lugar_firma] [int] NOT NULL,
	[hora_firma] [time](7) NOT NULL,
	[fecha_firma] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Documento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[T_DocsOpcionCompraventaVehiculo]  WITH CHECK ADD  CONSTRAINT [FK_TDOCV_cedula_abogado] FOREIGN KEY([cedula_abogado])
REFERENCES [dbo].[T_GeAbogados] ([Cedula])
GO

ALTER TABLE [dbo].[T_DocsOpcionCompraventaVehiculo] CHECK CONSTRAINT [FK_TDOCV_cedula_abogado]
GO

ALTER TABLE [dbo].[T_DocsOpcionCompraventaVehiculo]  WITH CHECK ADD  CONSTRAINT [FK_TDOCV_cedula_comprador] FOREIGN KEY([cedula_comprador])
REFERENCES [dbo].[T_GePersonas] ([Cedula])
GO

ALTER TABLE [dbo].[T_DocsOpcionCompraventaVehiculo] CHECK CONSTRAINT [FK_TDOCV_cedula_comprador]
GO

ALTER TABLE [dbo].[T_DocsOpcionCompraventaVehiculo]  WITH CHECK ADD  CONSTRAINT [FK_TDOCV_cedula_propietario] FOREIGN KEY([cedula_propietario])
REFERENCES [dbo].[T_GePersonas] ([Cedula])
GO

ALTER TABLE [dbo].[T_DocsOpcionCompraventaVehiculo] CHECK CONSTRAINT [FK_TDOCV_cedula_propietario]
GO

ALTER TABLE [dbo].[T_DocsOpcionCompraventaVehiculo]  WITH CHECK ADD  CONSTRAINT [FK_TDOCV_combustible] FOREIGN KEY([combustible])
REFERENCES [dbo].[T_DocsCombustibles] ([Id])
GO

ALTER TABLE [dbo].[T_DocsOpcionCompraventaVehiculo] CHECK CONSTRAINT [FK_TDOCV_combustible]
GO

ALTER TABLE [dbo].[T_DocsOpcionCompraventaVehiculo]  WITH CHECK ADD  CONSTRAINT [FK_TDOCV_lugar_firma] FOREIGN KEY([lugar_firma])
REFERENCES [dbo].[T_CrDistritos] ([idDistrito])
GO

ALTER TABLE [dbo].[T_DocsOpcionCompraventaVehiculo] CHECK CONSTRAINT [FK_TDOCV_lugar_firma]
GO

ALTER TABLE [dbo].[T_DocsOpcionCompraventaVehiculo]  WITH CHECK ADD  CONSTRAINT [FK_TDOCV_marca_motor] FOREIGN KEY([marca_motor])
REFERENCES [dbo].[T_DocsMarcaVehiculos] ([Id])
GO

ALTER TABLE [dbo].[T_DocsOpcionCompraventaVehiculo] CHECK CONSTRAINT [FK_TDOCV_marca_motor]
GO

ALTER TABLE [dbo].[T_DocsOpcionCompraventaVehiculo]  WITH CHECK ADD  CONSTRAINT [FK_TDOCV_marca_vehiculo] FOREIGN KEY([marca_vehiculo])
REFERENCES [dbo].[T_DocsMarcaVehiculos] ([Id])
GO

ALTER TABLE [dbo].[T_DocsOpcionCompraventaVehiculo] CHECK CONSTRAINT [FK_TDOCV_marca_vehiculo]
GO

ALTER TABLE [dbo].[T_DocsOpcionCompraventaVehiculo]  WITH CHECK ADD  CONSTRAINT [FK_TDOCV_tipo_vehiculo] FOREIGN KEY([tipo_vehiculo])
REFERENCES [dbo].[T_DocsTipoVehiculos] ([Id])
GO

ALTER TABLE [dbo].[T_DocsOpcionCompraventaVehiculo] CHECK CONSTRAINT [FK_TDOCV_tipo_vehiculo]
GO

/****** Object:  Table [dbo].[T_DocsPagare]    Script Date: 03-Oct-25 6:40:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_DocsPagare](
	[ID_Documento] [int] IDENTITY(1,1) NOT NULL,
	[monto_numerico] [decimal](15, 2) NOT NULL,
	[cedula_deudor] [int] NOT NULL,
	[sociedad_deudor] [varchar](150) NOT NULL,
	[cedula_juridica_sociedad] [varchar](20) NOT NULL,
	[acreedor_nombre] [varchar](150) NOT NULL,
	[cedula_juridica_acreedor] [varchar](20) NOT NULL,
	[acreedor_domicilio] [text] NOT NULL,
	[fecha_firma] [date] NOT NULL,
	[hora_firma] [time](7) NOT NULL,
	[fecha_vencimiento] [date] NOT NULL,
	[interes_formula] [text] NOT NULL,
	[interes_tasa_actual] [decimal](5, 2) NOT NULL,
	[interes_base] [varchar](100) NOT NULL,
	[lugar_pago] [int] NOT NULL,
	[cedula_fiador] [int] NOT NULL,
	[ubicacion_firma] [int] NOT NULL,
	[Tipo_Sociedad] [nvarchar](300) NOT NULL,
	[Ubicacion_Sociedad] [nvarchar](500)NOT NULL,
	[cedula_abogado] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Documento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[T_DocsPagare]  WITH CHECK ADD  CONSTRAINT [FK_T_DocsPagare_T_CrDistritos] FOREIGN KEY([lugar_pago])
REFERENCES [dbo].[T_CrDistritos] ([idDistrito])
GO

ALTER TABLE [dbo].[T_DocsPagare] CHECK CONSTRAINT [FK_T_DocsPagare_T_CrDistritos]
GO

ALTER TABLE [dbo].[T_DocsPagare]  WITH CHECK ADD  CONSTRAINT [FK_T_DocsPagare_T_CrDistritos1] FOREIGN KEY([lugar_pago])
REFERENCES [dbo].[T_CrDistritos] ([idDistrito])
GO

ALTER TABLE [dbo].[T_DocsPagare] CHECK CONSTRAINT [FK_T_DocsPagare_T_CrDistritos1]
GO

ALTER TABLE [dbo].[T_DocsPagare]  WITH CHECK ADD  CONSTRAINT [FK_T_DocsPagare_T_GePersonas] FOREIGN KEY([cedula_deudor])
REFERENCES [dbo].[T_GePersonas] ([Cedula])
GO

ALTER TABLE [dbo].[T_DocsPagare] CHECK CONSTRAINT [FK_T_DocsPagare_T_GePersonas]
GO

ALTER TABLE [dbo].[T_DocsPagare]  WITH CHECK ADD  CONSTRAINT [FK_T_DocsPagare_T_GePersonas1] FOREIGN KEY([cedula_fiador])
REFERENCES [dbo].[T_GePersonas] ([Cedula])
GO

ALTER TABLE [dbo].[T_DocsPagare] CHECK CONSTRAINT [FK_T_DocsPagare_T_GePersonas1]
GO

ALTER TABLE [dbo].[T_DocsPagare]  WITH CHECK ADD  CONSTRAINT [FK_TDP_cedula_abogado] FOREIGN KEY([cedula_abogado])
REFERENCES [dbo].[T_GeAbogados] ([Cedula])
GO

ALTER TABLE [dbo].[T_DocsPagare] CHECK CONSTRAINT [FK_TDP_cedula_abogado]
GO

/****** Object:  Table [dbo].[T_DocsPoderesEspecialesJudiciales]    Script Date: 03-Oct-25 6:40:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_DocsPoderesEspecialesJudiciales](
	[Id_doc] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[Id_Abogado] [int] NOT NULL,
	[Id_Cliente] [int] NOT NULL,
	[Texto] [nvarchar](max) NOT NULL,
	[Num_Causa] [varchar](100)NOT NULL,
 CONSTRAINT [PK_T_DocsPoderesEspecialesJudiciales] PRIMARY KEY CLUSTERED 
(
	[Id_doc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[T_DocsPoderesEspecialesJudiciales]  WITH CHECK ADD  CONSTRAINT [FK_T_DocsPoderesEspecialesJudiciales_T_GeAbogados] FOREIGN KEY([Id_Abogado])
REFERENCES [dbo].[T_GeAbogados] ([Cedula])
GO

ALTER TABLE [dbo].[T_DocsPoderesEspecialesJudiciales] CHECK CONSTRAINT [FK_T_DocsPoderesEspecialesJudiciales_T_GeAbogados]
GO

ALTER TABLE [dbo].[T_DocsPoderesEspecialesJudiciales]  WITH CHECK ADD  CONSTRAINT [FK_T_DocsPoderesEspecialesJudiciales_T_GePersonas] FOREIGN KEY([Id_Cliente])
REFERENCES [dbo].[T_GePersonas] ([Cedula])
GO

ALTER TABLE [dbo].[T_DocsPoderesEspecialesJudiciales] CHECK CONSTRAINT [FK_T_DocsPoderesEspecialesJudiciales_T_GePersonas]
GO

/****** MODULO DE TESTIMONIOS ******/

/****** Object:  Table [dbo].[T_Testimonios]    Script Date: 03-Oct-25 6:41:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_Testimonios](
	[Id_Testimonio] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[Id_Cliente] [int] NOT NULL,
	[Comentario] [nvarchar](500) NULL,
	[Evaluacion] [int] NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_T_Testimonios] PRIMARY KEY CLUSTERED 
(
	[Id_Testimonio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[T_Testimonios]  WITH CHECK ADD  CONSTRAINT [FK_T_Testimonios_T_GePersonas] FOREIGN KEY([Id_Cliente])
REFERENCES [dbo].[T_GePersonas] ([Cedula])
GO

ALTER TABLE [dbo].[T_Testimonios] CHECK CONSTRAINT [FK_T_Testimonios_T_GePersonas]
GO

/****** Object:  Table [dbo].[T_BitacoraEventos]    Script Date: 16-Nov-25 10:20:54 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_BitacoraEventos](
	[Id_evento] [int] IDENTITY(1,1) NOT NULL,
	[Usuario] [nvarchar](100) NOT NULL,
	[Fecha_Hora] [datetime] NOT NULL,
	[Tabla_Afectada] [nvarchar](100) NOT NULL,
	[Accion] [nvarchar](200) NOT NULL,
	[Id_registro_afectado] [int] NOT NULL,
	[Stack_error] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_evento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[T_BitacoraEventos] ADD  DEFAULT (getdate()) FOR [Fecha_Hora]
GO




