USE [PreaceptaBD]
GO

/*codigos importantes*/
insert into dbo.T_CrProvincias select * from [dbo].[CostaRica-provincias]; --llena la tabla T_CrPronvicias
insert into dbo.T_CrCantones select * from [dbo].[CostaRica-cantones]; --llena la tabla T_CrCantones
insert into dbo.T_CrDistritos select * from [dbo].[CostaRica-distritos]; --llena la tabla T_CrDistritos

INSERT INTO [dbo].[T_CasosTipos]
           ([Nombre])
     VALUES('Accidentes y Responsabilidad Civil'),
           ('Cobro Judicial'),
		   ('Derecho Penal'),
		   ('Derecho Civil'),
		   ('Derecho Laboral'),
		   ('Derecho Mercantil'),
		   ('Derecho Administrativo'),
		   ('Derecho Tributario'),
		   ('Propiedad Intelectual'),
		   ('Derecho Inmobiliario')
		   
GO

INSERT INTO [dbo].[T_CitasTipos]
           ([Nombre])
     VALUES
           ('Presencial'),
		   ('Virtual')
GO

INSERT INTO [dbo].[T_DocsCombustibles]
           ([Nombre])
     VALUES
           ('Gasolina'),
		   ('Disel'),
		   ('Eléctrico'),
		   ('Híbrido'),
		   ('Gas LP')
GO

INSERT INTO [dbo].[T_DocsMarcaVehiculos] ([Nombre])
VALUES 
    ('Toyota'),
    ('Honda'),
    ('Ford'),
    ('Chevrolet'),
    ('Volkswagen'),
    ('BMW'),
    ('Mercedes-Benz'),
    ('Hyundai'),
    ('Kia'),
    ('Nissan'),
    ('Mazda'),
    ('Audi'),
    ('Subaru'),
    ('Tesla'),
    ('Jeep'),
    ('Peugeot'),
    ('Renault'),
    ('Fiat'),
    ('Volvo'),
    ('Land Rover');
GO

INSERT INTO [dbo].[T_DocsTipoVehiculos] ([Nombre])
VALUES 
    ('Sedán'),
    ('SUV'),
    ('Pick-up'),
    ('Camión'),
    ('Motocicleta'),
    ('Microbús'),
    ('Bus'),
    ('Furgón'),
    ('Ciclomotor'),
    ('Van'),
    ('Tractor'),
    ('Remolque'),
    ('Cuatrimoto'),
    ('Camioneta'),
    ('Convertible'),
    ('Hatchback'),
    ('Coupe'),
    ('Minivan'),
    ('Ambulancia'),
    ('Vehículo eléctrico');
GO

INSERT INTO [dbo].[T_GeAbogadoTipo] ([Nombre])
VALUES 
    ('Penalista'),
    ('Civilista'),
    ('Laboralista'),
    ('Constitucionalista'),
    ('Administrativista'),
    ('Corporativo'),
    ('Notarial'),
    ('Tributarista'),
    ('Ambientalista'),
    ('Familia'),
    ('Propiedad Intelectual'),
    ('Internacional'),
    ('Comercial'),
    ('Migratorio'),
    ('Tecnológico'),
    ('Arbitraje'),
    ('Seguros'),
    ('Transporte'),
    ('Financiero'),
    ('Contratación Pública');
GO
