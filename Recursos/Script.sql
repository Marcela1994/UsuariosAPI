create database usuarioApi;

use usuarioApi;

drop table usuarios;
drop table rol;
drop table dependencias;

create table dependencias(
	id				int			not null	primary key,
	codigo			varchar(20)	not null,
	descripcion	varchar(90)	not null,
	cargo			varchar(50)	not null,
	estado			bit			not null
);

create table rol(
	id				int			not null	primary key,
	descripcion	varchar(90)	not null,
	sigla_rol		varchar(50)	not null,
	estado			bit			not null
);

create table usuarios(
	id			int			not null	primary key,
	documento	varchar(15)		not null,
	username	varchar(30)		not null,
	nombre		varchar(30)		not null,
	email		varchar(50),
	estado		bit		not null,
	id_rol		int		not null,	
	id_dependencia		int		not null,
	Constraint Fk_dependencias_usuarios FOREIGN KEY (id_dependencia) REFERENCES dependencias(id),
	Constraint Fk_rol_usuarios FOREIGN KEY (id_rol) REFERENCES rol(id)
);

insert into dbo.dependencias(id, codigo, descripcion, cargo, estado)
values(1, '5050', 'Administrativa', 'Profesor', 0);
insert into dbo.dependencias(id, codigo, descripcion, cargo, estado)
values(2, '3510', 'Usuarios', 'Estudiante', 1);

insert into dbo.rol(id, descripcion, sigla_rol, estado)
values(1, 'Administrador', 'ADM', 1);
insert into dbo.rol(id, descripcion, sigla_rol, estado)
values(2, 'Usuario', 'USR', 1);

insert into dbo.usuarios(id, documento, username, nombre, email, estado, id_dependencia, id_rol)
values(1, '789546', 'nestor', 'rodriguez', 'nestor@gmail.com', 1, 1, 1);
insert into dbo.usuarios(id, documento, username, nombre, email, estado, id_dependencia, id_rol)
values(2, '123456', 'marce', 'marce', 'marce@gmail.com', 0, 2, 2);

PROCEDIMIENTOS ALMACENADOS

********************************************
USE [usuarioApi]
GO
/****** Object:  StoredProcedure [dbo].[consultaUsuarioPorID]    Script Date: 26/06/2021 11:40:19 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[consultaUsuarioPorID](
@id int
)
as
begin
select u.id, u.documento, u.username, u.nombre, u.email, d.codigo, d.descripcion, d.cargo, r.descripcion_rol, r.sigla_rol, u.estado
from dbo.usuarios u
INNER JOIN dbo.dependencias d ON u.id=d.id
INNER JOIN dbo.rol r on r.id=u.id
where u.id=@id;
end

*********************************************************
USE [usuarioApi]
GO
/****** Object:  StoredProcedure [dbo].[consultaUsuarios]    Script Date: 26/06/2021 11:41:00 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[consultaUsuarios]
as
begin
select u.id, u.documento, u.username, u.nombre, u.email, u.estado , d.id as id_dependencia ,d.codigo, d.descripcion, d.cargo, 
d.estado as estado_dependencia, r.id as id_rol, r.descripcion as descripcion_rol, r.sigla_rol, r.estado as estado_rol
from dbo.usuarios u
INNER JOIN dbo.dependencias d ON d.id = u.id_dependencia
INNER JOIN dbo.rol r on r.id = u.id_rol
end

************************************************************
USE [usuarioApi]
GO
/****** Object:  StoredProcedure [dbo].[insertarUsuario]    Script Date: 26/06/2021 11:41:25 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[insertarUsuario] (
@id int,
@documento varchar(15),
@username varchar(30),
@nombre varchar(30),
@email varchar(50),
@estado bit,
@id_rol int,
@id_dep int
)
as
begin
insert into dbo.usuarios(id, documento, username, nombre, email, estado, id_dependencia, id_rol)
values(@id, @documento, @username, @nombre, @email, @estado, @id_dep, @id_rol);
end

***************************************************************
USE [usuarioApi]
GO
/****** Object:  StoredProcedure [dbo].[modificarUsuario]    Script Date: 26/06/2021 11:41:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[modificarUsuario](
@id int,
@documento varchar(15),
@username varchar(30),
@nombre varchar(30),
@email varchar(50),
@estado bit
)
as
begin
UPDATE dbo.usuarios
SET documento = @documento, username = @username, nombre = @nombre, email = @email, estado = @estado
WHERE id = @id
end


***************************************************************
create procedure eliminarUsuario(
@id int
)
as
begin
delete from dbo.usuarios where id = @id
end