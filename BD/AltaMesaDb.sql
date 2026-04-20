create database AltaMesaDB;
go

use AltaMesaDB;
go

create table rol(
	id_rol int identity(1,1) primary key,
	nombre varchar(30) not null unique
);
go

create table usuario(
	id_usuario int identity(1,1) primary key,
	id_rol int not null,
	nombre varchar(50) not null,
	apellido varchar(50) not null,
	correo varchar(100) not null,
	contra_hash varchar(255) not null,
	estado bit not null default 1,
	fecha_registro datetime not null default getdate(),
	updated_at datetime,
	updated_by int,

	constraint UQ_usuario_correo unique (correo),
	constraint FK_usuario_rol foreign key (id_rol) references rol(id_rol),
);
go

create table mesa(
	id_mesa int identity(1,1) primary key,
	numero int not null,
	capacidad int not null check (capacidad in (2, 4, 6, 8)),
	estado varchar(20) not null default 'Disponible' check (estado in ('Disponible','Ocupada','Inhabilitada')),
	
	constraint UQ_mesa_numero unique (numero)
);
go

create table categoria_producto(
	id_categoria int identity(1,1) primary key,
	nombre varchar(50) not null,
	descripcion varchar(80),
	estado bit not null default 1,

	constraint UQ_categoria_producto_nombre unique (nombre)
);
go

create table producto(
	id_producto int identity(1,1) primary key,
	id_categoria int not null,
	nombre varchar(80) not null, 
	desc_corta varchar(50) not null,
	desc_completa varchar(255) not null,
	precio decimal(6,2) not null check (precio >= 0),
	requiere_preparacion bit not null default 1,
	estado bit not null default 1,
	created_at datetime not null default getdate(),
	updated_at datetime,
	updated_by int,

	constraint FK_producto_categoria foreign key (id_categoria) references categoria_producto(id_categoria)
);
go

create table pedido(
	id_pedido int identity(1,1) primary key,
	id_mesa int not null,
	id_mesero int not null,
	fecha_pedido datetime not null default getdate(),
	estado varchar(30) not null check (estado in ('Abierto', 'En atencion', 'Parcialmente servido', 'Cerrado', 'Anulado')),
	observacion_general varchar(255),
	subtotal decimal(8,2) not null default 0 check (subtotal >= 0),
	descuento decimal(8,2) not null default 0 check (descuento >= 0),
	total decimal(10,2) not null default 0 check (total >= 0),
	updated_at datetime,
	updated_by int,

	constraint FK_pedido_mesa foreign key (id_mesa) references mesa(id_mesa),
	constraint FK_pedido_mesero foreign key (id_mesero) references usuario(id_usuario)
);
go

create table detalle_pedido(
	id_detalle_pedido int identity(1,1) primary key,
	id_pedido int not null,
	id_producto int not null,
	cantidad int not null check (cantidad >= 0),
	precio_unitario decimal(5,2) not null check (precio_unitario >= 0),
	subtotal decimal(7,2) not null check (subtotal >= 0),
	observacion varchar(255),
	estado_detalle varchar(30) not null check (estado_detalle in ('Ingresado', 'En preparacion', 'Listo para servir', 'Entregado', 'Anulado')),
	es_adicional bit not null default 0,
	fecha_registro datetime not null default getdate(),
	updated_at datetime,
	updated_by int,

	constraint FK_detalle_pedido_pedido foreign key (id_pedido) references pedido(id_pedido),
	constraint FK_detalle_pedido_producto foreign key (id_producto) references producto(id_producto),
);
go


-- drop producto;
-- drop table usuario;
-- drop table rol;
-- drop table categoria_producto;
-- drop table mesa;
