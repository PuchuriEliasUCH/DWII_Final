-- MESAS

create procedure sp_crear_mesa
	@numero int,
	@capacidad int,
	@estado varchar(20) = 'Disponible'
as
begin
	insert into mesa(numero, capacidad, estado)  
	values(@numero, @capacidad, @estado)
end
go

create procedure sp_actualizar_mesa
	@id int,
	@numero int,
	@capacidad int,
	@estado varchar(20)
as
begin
	update mesa 
	set numero = @numero, capacidad = @capacidad, estado = @estado
	where id_mesa = @id
end
go

create procedure sp_eliminar_mesa
	@id int
as
begin
	update mesa
	set estado = 'Inhabilitada'
	where id_mesa = @id
end
go

-- USUARIO

create procedure sp_listar_usuarios_dto
as
begin
    select id_usuario, concat(u.nombre, ' ', apellido) as NombreCompleto, correo, r.nombre
    from usuario u
    inner join rol r
    on u.id_rol = r.id_rol
end
go

create procedure sp_crear_usuario
	@id_rol int,
	@nombre varchar(50),
	@apellido varchar(50),
	@correo varchar(100),
	@contra_hash varchar(255)
as
begin
	insert into usuario(id_rol, nombre, apellido, correo, contra_hash) 
	values(@id_rol, @nombre, @apellido, @correo, @contra_hash)
end
go

create procedure sp_actualizar_usuario
	@id int,
	@id_rol int,
	@nombre varchar(50),
	@apellido varchar(50),
	@correo varchar(100),
	@contra_hash varchar(255) = null
as
begin
	update usuario 
	set id_rol = @id_rol, nombre = @nombre, apellido = @apellido, correo = @correo,
        contra_hash = case when @contra_hash is null then contra_hash else @contra_hash end
	where id_usuario = @id
end
go

create procedure sp_eliminar_usuario
	@id int
as
begin
	update usuario
	set estado = 0
	where id_usuario = @id
end
go

select * from usuario;
