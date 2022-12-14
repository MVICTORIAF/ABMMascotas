create database VeterinariaRepaso
USE [Veterinaria]
GO
/****** Object:  Table [dbo].[Especies]    Script Date: 2/7/2021 23:38:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Especies](
	[idEspecie] [int] NOT NULL,
	[nombreEspecie] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Especies] PRIMARY KEY CLUSTERED 
(
	[idEspecie] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Mascotas]    Script Date: 2/7/2021 23:38:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mascotas](
	[codigo] [int] NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[especie] [int] NOT NULL,
	[sexo] [int] NOT NULL,
	[fechaNacimiento] [date] NOT NULL,
 CONSTRAINT [PK_Mascotas] PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Especies] ([idEspecie], [nombreEspecie]) VALUES (1, N'Perro')
INSERT [dbo].[Especies] ([idEspecie], [nombreEspecie]) VALUES (2, N'Roedor')
INSERT [dbo].[Especies] ([idEspecie], [nombreEspecie]) VALUES (3, N'Gato')
INSERT [dbo].[Especies] ([idEspecie], [nombreEspecie]) VALUES (4, N'Reptil')
INSERT [dbo].[Especies] ([idEspecie], [nombreEspecie]) VALUES (5, N'Ave')
GO
INSERT [dbo].[Mascotas] ([codigo], [nombre], [especie], [sexo], [fechaNacimiento]) VALUES (1, N'Boby', 1, 1, CAST(N'2020-10-10' AS Date))
INSERT [dbo].[Mascotas] ([codigo], [nombre], [especie], [sexo], [fechaNacimiento]) VALUES (2, N'Michi', 3, 2, CAST(N'2020-10-01' AS Date))
GO
