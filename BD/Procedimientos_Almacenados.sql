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

-- CATEGORIA

create procedure sp_crear_categoria_producto
    @nombre varchar(50),
    @descripcion varchar(80),
    @estado bit
as
begin
    insert into categoria_producto (nombre, descripcion, estado)
    values (@nombre, @descripcion, @estado)
end
go

create procedure sp_actualizar_categoria_producto
    @id int,
    @nombre varchar(50),
    @descripcion varchar(80),
    @estado bit
as
begin
    update categoria_producto
    set nombre = @nombre,
        descripcion = @descripcion,
        estado = @estado
    where id_categoria = @id
end
go

create procedure sp_eliminar_categoria_producto
    @id int
as
begin
    update categoria_producto
    set estado = 0
    where id_categoria = @id
end
go

-- PRODUCTO
create procedure sp_crear_producto
	@id_categoria int,
	@nombre varchar(50),
	@desc_corta varchar(50),
	@desc_completa varchar(255),
	@precio decimal(6,2),
	@requiere_preparacion bit = 1
as
begin
	insert into producto (id_categoria, nombre, desc_corta, desc_completa, precio, requiere_preparacion, estado, created_at)
	values (@id_categoria, @nombre, @desc_corta, @desc_completa, @precio, @requiere_preparacion, 1, GETDATE())
end
go

create procedure sp_actualizar_producto
    @id_producto int,
    @id_categoria int,
    @nombre varchar(80),
    @desc_corta varchar(50),
    @desc_completa varchar(255),
    @precio decimal(6,2),
    @requiere_preparacion bit
as
begin
    update producto
    set
        id_categoria = @id_categoria,
        nombre = @nombre,
        desc_corta = @desc_corta,
        desc_completa = @desc_completa,
        precio = @precio,
        requiere_preparacion = @requiere_preparacion
    where id_producto = @id_producto
end
go

create procedure sp_eliminar_producto
    @id_producto int
as
begin
    update producto
    set estado = 0
    where id_producto = @id_producto
end
go