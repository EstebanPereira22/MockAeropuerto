use Master
go

if exists(Select * From Sysdatabases Where name = 'ObligatorioVerano')
begin
	drop database ObligatorioVerano
end 
go

Create database ObligatorioVerano
go

-------
Use ObligatorioVerano
go
Create table Empleado
(
	usuario varchar(50) primary key,
	contra varchar(10) not null,
	nombre varchar(50) not null,
	labor varchar (10) check (labor like 'Vendedor' or labor like 'Gerente' or labor like 'Admin'),
	
)
go
Create table Cliente
(
	pasaporte varchar(32) primary key check (LEN(pasaporte)=32),
	nombre varchar(50) not null,
	tarjeta varchar(16) not null check (tarjeta like'[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
	contra varchar(10) not null
)
go
Create table Ciudades
(
	codigoCiudad varchar(6) primary key check (codigoCiudad like '[a-z][a-z][a-z][a-z][a-z][a-z]'),
	ciudadUbi varchar (50) not null,
	paisUbi varchar (50) not null
)
go
Create table Aeropuertos
(
	codigoAeropuerto varchar(3) primary key check (codigoAeropuerto like '[a-z][a-z][a-z]'),
	codigoCiudad varchar(6) not null foreign key references Ciudades(codigoCiudad),
	nombre varchar(50) not null,
	direccion varchar (50) not null,
	impuestoS int check (impuestoS >=0),
	impuestoL int check(impuestoL >=0)
)
go

create table Vuelos 
(
	codigoVuelo varchar(15)primary key,
	codigoAeropuertoSalida varchar(3) not null foreign key references Aeropuertos(codigoAeropuerto),
	codigoAeropuertoLlegada varchar(3) not null foreign key references Aeropuertos(codigoAeropuerto),
	fechayhorasalida datetime not null,
	fechayhorallegada datetime not null,
	precio int not null check(precio > 0),
	asientos int not null check(asientos between 100 and 300),
	check (fechayhorasalida > getdate()),
	check (fechayhorallegada > fechaYhorasalida)
)
go
Create table Pasajes
(
	identificador int identity (1,1) primary key,
	codigoVuelo varchar(15) not null foreign key references Vuelos(codigoVuelo),
	pasaporte varchar(32) not null foreign key references Cliente(pasaporte),
	fechacompra datetime default getdate(),
	precio int check (precio>0)	
)
go

insert Empleado values ('empleado1', 'pass1', 'Juan Fernandez', 'Vendedor')
insert Empleado values ('empleado2', 'pass2', 'Juan Lopez', 'Admin')
insert Empleado values ('empleado3', 'pass3', 'Fernando Fernandez', 'Gerente')
insert Empleado values ('empleado4', 'pass4', 'Juana Fernandez', 'Vendedor')
insert Empleado values ('empleado5', 'pass5', 'Juana Lopez', 'Admin')
insert Empleado values ('empleado6', 'pass6', 'Fernanda Fernandez', 'Gerente')

insert Cliente values ('aB3kR9sL7pX2qY5vF8dM6hN1tZ4wC0gE','Carlos Tevez','4551455514551455','pass1')
insert Cliente values ('BA3kR9sL7pX2qY5vF8dM6hN1tZ4wC0ce','Carlos Perez','4551455514551455','pass2')
insert Cliente values ('BB3kR9sL7pX2qY5vF8dM6hN1tZ4wC0gE','Carla Tevez','4551455514551455','pass3')
insert Cliente values ('CC3kR9sL7pX2qY5vF8dM6hN1tZ4wC0gE','Carla Perez','4551455514551455','pass4')
insert Cliente values ('cV3kR9sL7pX2qY5vF8dCChN1tZ4wC0gE','Fernando Estevez','4551455514551455','pass6')
insert Cliente values ('aB3kR9sL7pX2AC5vF8dM6hN1tZ4wC0gE','Fernanda Estevez','4551455514551455','pass5')
insert Cliente values ('AAAAR9sL7pX2qY5vF8dM6hN1tZ4wC0gE','Carlos Tevez','4551455514551455','pass7')


insert Ciudades values ('mvduru' , 'Montevideo','Uruguay')
insert Ciudades values ('bsasar' , 'Buenos Aires','Argentina')
insert Ciudades values ('perlim' , 'Lima','Peru')
insert Ciudades values ('sanpbr' , 'San Pablo','Brasil')
insert Ciudades values ('cdmxmx' , 'Ciudad de Mexico','Mexico')
insert Ciudades values ('carven' , 'Caracas','Venezuela')
insert Ciudades values ('sdcchi' , 'Santiago de Chile','Chile')
insert Ciudades values ('bogcol' , 'Bogota','Colombia')


insert Aeropuertos values ('amu','mvduru','Aeropouerto de Montevideo','Calle 1',0,0)
insert Aeropuertos values ('aba','bsasar','Aeropouerto de Buenos Aires','Calle 2',120,100)
insert Aeropuertos values ('alp','perlim','Aeropouerto de Lima','Calle 3',110,30)
insert Aeropuertos values ('asb','sanpbr','Aeropouerto de Brasil','Calle 4',70,90)
insert Aeropuertos values ('acm','cdmxmx','Aeropouerto de Mexico','Calle 5',210,170)
insert Aeropuertos values ('acv','carven','Aeropouerto de Venezuela','Calle 6',0,0)
insert Aeropuertos values ('asc','sdcchi','Aeropouerto de Santiago de Chile','Calle 7',30,60)
insert Aeropuertos values ('abc','bogcol','Aeropouerto de Colombia','Calle 8',0,0)



insert Vuelos values('202411300830amu','amu','aba','2024-11-30 08:30:00','2024-11-30 12:30:00',800,150)
insert Vuelos values('202410300930asc','asc','acm','2024-10-30 09:30:00','2024-10-30 12:00:00',1200,200)
insert Vuelos values('202407300730abc','abc','aba','2024-07-30 07:30:00','2024-07-30 10:00:00',600,180)
insert Vuelos values('202407210730alp','alp','asc','2024-07-21 07:30:00','2024-07-21 11:00:00',1000,250)
insert Vuelos values('202405210530acv','acv','abc','2024-05-21 05:30:00','2024-05-21 08:45:00',900,200)
insert Vuelos values('202412241730aba','aba','amu','2024-12-24 17:30:00','2024-12-24 20:00:00',700,180)
insert Vuelos values('202412241730asc','asc','acv','2025-01-15 14:45:00','2025-01-15 18:30:00',1100,220)
insert Vuelos values('202504050800acm','acm','abc','2025-04-05 08:00:00','2025-04-05 11:45:00',900,200)
go

insert into Pasajes (codigoVuelo, pasaporte, fechacompra, precio) values ('202411300830amu', 'aB3kR9sL7pX2qY5vF8dM6hN1tZ4wC0gE', '2024-11-30 08:30:00', 950)
insert into Pasajes (codigoVuelo, pasaporte, fechacompra, precio) values  ('202407210730alp', 'AAAAR9sL7pX2qY5vF8dM6hN1tZ4wC0gE', '2024-11-30 08:30:00', 1100)
insert into Pasajes (codigoVuelo, pasaporte, fechacompra, precio) values  ('202412241730asc', 'BB3kR9sL7pX2qY5vF8dM6hN1tZ4wC0gE', '2024-07-30 07:30:00', 74)
insert into Pasajes (codigoVuelo, pasaporte, fechacompra, precio) values  ('202504050800acm', 'BB3kR9sL7pX2qY5vF8dM6hN1tZ4wC0gE', '2024-07-30 07:30:00', 122)
insert into Pasajes (codigoVuelo, pasaporte, fechacompra, precio) values  ('202411300830amu', 'CC3kR9sL7pX2qY5vF8dM6hN1tZ4wC0gE', '2024-07-21 07:30:00', 400)
insert into Pasajes (codigoVuelo, pasaporte, fechacompra, precio) values  ('202410300930asc', 'CC3kR9sL7pX2qY5vF8dM6hN1tZ4wC0gE', '2024-07-21 07:30:00', 872)
insert into Pasajes (codigoVuelo, pasaporte, fechacompra, precio) values  ('202412241730aba', 'aB3kR9sL7pX2qY5vF8dM6hN1tZ4wC0gE', '2024-12-24 17:30:00', 120)
insert into Pasajes (codigoVuelo, pasaporte, fechacompra, precio) values  ('202504050800acm', 'aB3kR9sL7pX2qY5vF8dM6hN1tZ4wC0gE', '2024-12-24 17:30:00', 120)
go
-------------------------------------------------------------Lista Clientes----------------------------------------------------------------------------------
create proc ListaClientes
as
begin
	select * from Cliente
end
go
---------------------------------------------------------------Lista Vuelos----------------------------------------------
create proc ListaVuelos
as
begin
	select * from Vuelos
end
go
------------------------------------------------------------------Lista aeropuertos-------------
create Proc ListaAeropuerto
as
begin
	select * from Aeropuertos
end
go
-----------------------------------------------------------------------Buscar Vuelo---------------------------------
create proc BuscarVuelo
@codigoVuelo varchar (15)
as
begin
	select * from Vuelos
	where codigoVuelo = @codigoVuelo
end
go
--------------------------------------------------Venta de Pasaje--------------------------------------------
create proc VentaPax
@codigoVuelo varchar(15), @pasaporte varchar(32), @fechacompra datetime, @precio int
as
begin
	if not exists(select pasaporte from Cliente where pasaporte = @pasaporte)
		return -1 -- no existe cliente con ese pasaporte

	if not exists (select codigoVuelo from Vuelos where codigoVuelo = @codigoVuelo)
		return -2 -- no existe aeropuerto con ese codigo
	
	if CONVERT(date, @fechacompra) < CONVERT(date, GETDATE())
		return -3-- no se puede comprar un pasaje en el pasado

	declare @Error int
	begin tran
	insert into Pasajes(codigoVuelo, pasaporte, fechacompra, precio)
	values(@codigoVuelo,@pasaporte,@fechacompra, @precio)
	set @Error = @@ERROR
	if(@Error = 0)
	begin
		commit tran
		return 1
		end
		
	else
		return -4
	

end
go
----------------------------------------------------Listados para el default------------------------------------------------------------------
create proc Partidas
@codigoAeropuertoS varchar(3)
as
begin
    select * from Vuelos  inner Join Aeropuertos on codigoAeropuertoSalida = codigoAeropuerto where codigoAeropuertoSalida = @codigoAeropuertoS
		order by fechayhorasalida;
end
go

create proc Arribos
@codigoAeropuertoA varchar(3)
as
begin
    select  * from Vuelos inner Join Aeropuertos on codigoAeropuertoLlegada = codigoAeropuerto where codigoAeropuertoLlegada = @codigoAeropuertoA
       order by fechayhorallegada;
end
go


-----------------------------------------------------Alta Vuelos--------------------------------------------------------------------------------
create proc AltaVuelos
@codigoVuelo varchar(15),@codigoAeropuertoS varchar(3), @codigoAeropuertoL varchar(3), @fechayhorasalida datetime, @fechayhorallegada datetime,
@precio int, @asientos int
as
begin
	-- Restricción de la acción, no del dato en sí
	if CONVERT(date, @fechayhorasalida) <= CONVERT(date, GETDATE())
		return -1 -- la salida del viaje no puede ser menor a hoy

	if not exists (select codigoAeropuerto from Aeropuertos where codigoAeropuerto = @codigoAeropuertoS)
		return -2 -- no existe aeropuerto con ese codigo

	if not exists (select codigoAeropuerto from Aeropuertos where codigoAeropuerto = @codigoAeropuertoL)
		return -3 -- no existe aeropuerto con ese codigo

	declare @Error int
	begin tran
	insert into Vuelos(codigoVuelo,codigoAeropuertoSalida ,codigoAeropuertoLlegada, fechayhorasalida, fechayhorallegada,precio,asientos) 
	values(@codigoVuelo,@codigoAeropuertoS,@codigoAeropuertoL,@fechayhorasalida, @fechayhorallegada,@precio,@asientos)
	set @Error = @@ERROR
	if(@Error = 0)
	begin
		commit tran
		return 1
		end
		
	else
		return -4
end
go
-----------------------------------------------------HISTORICO DE COMPRAS-------------------------------------------------------------------------
create proc HistoricoDeCompras
@pasaporte varchar (32)
as
begin
	select * from Pasajes
	where pasaporte = @pasaporte
end
go
---------------------------------------------------LOGUEOS---------------------------------------------------------------------------------------
create proc LogueoCliente
@pasaporte varchar (32),
@contra varchar(10)
as
begin
	select * from Cliente
	where pasaporte = @pasaporte
	and contra = @contra
end
go

create proc LogueoEmpleado
@usuario varchar (32),
@contra varchar(10)
as
begin
	select * from Empleado
	where usuario = @usuario
	and contra = @contra
end
go
---------------------------------------------------ABM Aeropuertos-------------------------------------------------------------------------------
create proc BuscarAeropuerto
@codigoAeropuerto varchar (3)
as
begin
	select * from Aeropuertos
	where codigoAeropuerto = @codigoAeropuerto
end
go

create proc ModificarAeropuerto
@codigoAeropuerto varchar (3), @codigoCiudad varchar(6), @nombre varchar(50), @direccion varchar(50), @impuestoS int, @impuestoL int
as
begin
	if not exists(select codigoAeropuerto from Aeropuertos where codigoAeropuerto = @codigoAeropuerto)
		return -1 --no existe
		declare @Error int 
		begin tran
		update Aeropuertos
		set
		codigoCiudad = @codigoCiudad,
		nombre = @nombre,
		direccion = @direccion,
		impuestoS = @impuestoS,
		impuestoL = @impuestoL
		where codigoAeropuerto = @codigoAeropuerto
		set @Error = @@ERROR
		if(@Error=0)
		begin
			commit tran
			return 1
		end
		else
		begin
			rollback tran
			return -2 --error
		end
	
end
go


create Proc AgregarAeropuerto
@codigoAeropuerto varchar (3), @codigoCiudad varchar(6), @nombre varchar(50), @direccion varchar(50), @impuestoS int, @impuestoL int
as
begin
	if exists (select codigoAeropuerto from Aeropuertos where codigoAeropuerto = @codigoAeropuerto)
		return -1 --ya existe
	declare @Error int
	begin tran
	insert Aeropuertos(codigoAeropuerto,codigoCiudad,nombre,direccion,impuestoS,impuestoL)
	values (@codigoAeropuerto,@codigoCiudad,@nombre,@direccion,@impuestoS,@impuestoL)
	set @Error = @@ERROR

	if(@Error = 0)
	begin
		commit tran
		return 1
	end
	else
	begin
		rollback tran
		return -2
	end	
end
go

create proc EliminarAeropuerto
@codigoAeropuerto varchar(3)
as
begin
	if not exists (select codigoAeropuerto from Aeropuertos where codigoAeropuerto = @codigoAeropuerto)
	return -1 -- no existe

	if exists(select codigoAeropuertoSalida from Vuelos where codigoAeropuertoSalida = @codigoAeropuerto)
	return -2
		

	declare @Error int
	begin tran
	delete Aeropuertos where codigoAeropuerto = @codigoAeropuerto
	set @Error = @@ERROR

	if(@Error = 0)
	begin
		commit tran
		return 1
	end
	else
	begin
		rollback tran
		return -3
	end
end
go


---------------------------------------------------ABM Clientes-----------------------------------------------------------------------------
create proc BuscarCliente
@pasaporte varchar(32)
as
begin
	select * from Cliente 
	where pasaporte = @pasaporte
end
go


create proc ModificarCliente
@pasaporte varchar(32), @nombre varchar(50), @tarjeta varchar(16), @contra varchar(10)
as
begin
	if not exists(select pasaporte from Cliente where pasaporte = @pasaporte)
		return -1 --no existe
		declare @Error int 
		begin tran
		update Cliente
		set
		nombre = @nombre,
		tarjeta = @tarjeta,
		contra = @contra
		where pasaporte = @pasaporte
		set @Error = @@ERROR
		if(@Error=0)
		begin
			commit tran
			return 1
		end
		else
		begin
			rollback tran
			return -2 --error
		end
end
go

create proc AgregarCliente
@pasaporte varchar(32), @nombre varchar(50), @tarjeta varchar(16), @contra varchar(10)
as
begin
	if exists (select pasaporte from Cliente where pasaporte = @pasaporte)
		return -1 --ya existe
	declare @Error int
	begin tran
	insert Cliente(pasaporte,nombre,tarjeta,contra) values (@pasaporte,@nombre,@tarjeta,@contra)
	set @Error = @@ERROR

	if(@Error = 0)
	begin
		commit tran
		return 1
	end
	else
	begin
		rollback tran
		return -2
	end	

end
go

create proc EliminarCliente
@pasaporte varchar (32)
as
begin
	if not exists (select pasaporte from Cliente where pasaporte = @pasaporte)
	return -1 -- no existe

	if exists (select pasaporte from Pasajes where pasaporte = @pasaporte)
	return -2-- cliente con pasaje comprado

	declare @Error int
	begin tran
	delete Cliente where pasaporte = @pasaporte
	set @Error = @@ERROR

	if(@Error = 0)
	begin
		commit tran
		return 1
	end
	else
	begin
		rollback tran
		return -3
	end
end
go

---------------------------------------------------ABM Empleados----------- nose si hay que hacerlo o que 
create proc BuscarEmpleado
@usuario varchar (50)
as
begin
	select * from Empleado
	where usuario = @usuario
end
go

create proc ModificarEmpleado
@usuario varchar(50), @contra varchar(50), @nombre varchar(50), @labor varchar (10)
as
begin
	if not exists(select usuario from Empleado where usuario = @usuario)
		return -1 -- no existe 

		declare @Error int
		begin tran
		update Empleado
		set
		contra = @contra,
		nombre = @nombre,
		labor = @labor
		where usuario = @usuario
		set @Error = @@ERROR
		if(@Error = 0)
		begin
			commit tran
			return 1
		end
		else
		begin
			rollback tran
			return -2
		end
end 
go


create proc AgregarEmpleado
@usuario varchar(50), @contra varchar(50), @nombre varchar(50), @labor varchar (10)
as
begin
	if exists (select usuario from Empleado where usuario = @usuario)
		return -1 --ya existe
	declare @Error int
	begin tran
	insert Empleado (usuario,contra,nombre,labor) values (@usuario,@contra,@nombre,@labor)
	set @Error = @@ERROR

	if(@Error = 0)
	begin
		commit tran
		return 1
	end
	else
	begin
		rollback tran
		return -2
	end	
end
go

create proc EliminarEmpleado
@usuario varchar(50)
as
begin
	if not exists (select usuario from Empleado where usuario = @usuario)
	return -1 -- no existe

	declare @Error int
	begin tran
	delete Empleado where usuario = @usuario
	set @Error = @@ERROR

	if(@Error = 0)
	begin
		commit tran
		return 1
	end
	else
	begin
		rollback tran
		return -2
	end
end 
go

---------------------------------------------------ABM Ciudades----------------------------------------------------------------
create proc BuscarCiudad
@codigoCiudad varchar(6)
as
begin
	select * from Ciudades
	where codigoCiudad = @codigoCiudad
end
go

create proc ModificarCiudad
@codigoCiudad varchar(6), @ciudadUbi varchar(50), @paisUbi varchar(50)
as
begin
	if not exists(select codigoCiudad from Ciudades where codigoCiudad = @codigoCiudad)
	return -1 -- no existe

	declare @Error int
	begin tran
	update Ciudades
	set 
	ciudadUbi = @ciudadUbi,
	paisUbi = @paisUbi

	where codigoCiudad = @codigoCiudad
	set @Error = @@ERROR
	if(@Error = 0)
	begin	
		commit tran
		return 1
	end

	else
	begin
		rollback tran
		return -2
	end	
end
go

create proc AgregarCiudad
@codigoCiudad varchar(6), @ciudadUbi varchar(50), @paisUbi varchar(50)
as
begin
	if exists(select codigoCiudad from Ciudades where codigoCiudad = @codigoCiudad)
		return -1 -- ya existe
	declare @Error int
	begin tran
	insert Ciudades(codigoCiudad,ciudadUbi,paisUbi) values (@codigoCiudad,@ciudadUbi,@paisUbi)
	set @Error = @@ERROR

	if(@Error = 0)
	begin
		commit tran
		return 1
	end
	else
	begin
		rollback tran
		return -2
	end	
end
go

create proc EliminarCiudad
@codigoCiudad varchar(6)
as
begin
	if not exists (select codigoCiudad from Ciudades where codigoCiudad = @codigoCiudad)
	return -1 -- no existe
	if exists (select codigoCiudad from Aeropuertos where codigoCiudad = @codigoCiudad)
	return -2 -- tiene un aeropuerto asociado

	declare @Error int
	begin tran
	delete Ciudades where codigoCiudad = @codigoCiudad
	set @Error = @@ERROR

	if(@Error = 0)
	begin
		commit tran
		return 1
	end
	else
	begin
		rollback tran
		return -3
	end
end
go