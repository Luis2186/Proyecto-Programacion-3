Drop Table ListaNVulgares
Drop Table Compra
Drop Table LineaFacturacion
Drop Table Planta
Drop Table FichaDeCiudado
Drop Table TipoDeIluminacion
Drop Table TipoDePlanta
Drop Table Usuario

--Drop Table NombreVulgar
Use Obligatoriop3
Go

Create Table Usuario(	Usuario_Id int identity (1,1) not null,
						Usuario_Nombre nvarchar(30) not null,
						Usuario_Apellido nvarchar(30) not null,
						Usuario_Email nvarchar(320) not null,
						Usuario_Contraseña nvarchar(50)not null,
						Constraint Pk_Usuario Primary Key (Usuario_Id),
						Constraint Uk_Email Unique (Usuario_Email)
)

Create Table TipoDeIluminacion (TipoDeIluminacion_Id int identity(1,1) not null,
								TipoDeIluminacion_Nombre nvarchar(20) null,
								constraint PK_TipoDeIluminacion Primary key (TipoDeIluminacion_Id)
	
)
			
Create Table FichaDeCiudado(FichaDeCiudado_Id int identity(1,1) not null,
							FichaDeCiudado_Cantidad int null,
							FichaDeCiudado_UnidadDeTiempo nvarchar(10),
							FichaDeCiudado_Temperatura decimal null,
							FichaDeCiudado_TipoDeIluminacion int null,
							constraint PK_FichaDeCiudado Primary Key (FichaDeCiudado_Id),	
							constraint FK_FichaDeCiudado_TipoDeIluminacion foreign key (FichaDeCiudado_TipoDeIluminacion) references TipoDeIluminacion (TipoDeIluminacion_Id)
)

Create Table TipoDePlanta(	TipoDePlanta_Id int Identity(1,1) not null,
							TipoDePlanta_Nombre nvarchar(20) null,
							TipoDePlanta_Descripcion nvarchar(100)null,
							constraint PK_TipoDePlanta Primary Key (TipoDePlanta_Id),
							constraint UK_TipoP_Nombre UNIQUE(TipoDePlanta_Nombre)
)

Create Table Planta(	Planta_Id int Identity (1,1) not null,
						Planta_Tipo int null,
						Planta_NombreCientifico nvarchar(50) null,				
						Planta_Descripcion nvarchar(100) null,
						Planta_Ambientes nvarchar(30) null,
						Planta_AlturaMaxima int null,
						Planta_Foto nvarchar(50) null,
						Planta_FichaDeCiudados int null,
						Planta_Precio decimal null,
						Constraint PK_Planta Primary Key (Planta_Id),
						Constraint FK_Planta_Tipo Foreign key (Planta_Tipo) references TipoDePlanta(TipoDePlanta_Id),						
						Constraint FK_Planta_FichaDeCiudado Foreign Key (Planta_FichaDeCiudados) references FichaDeCiudado(FichaDeCiudado_Id),
						Constraint UK_Planta_NombreCientifico UNIQUE(Planta_NombreCientifico)
)

Create Table ListaNVulgares(List_id int identity(1,1) not null,
							Planta_Id int not null,
							NombreVulgar nvarchar(50) null,
							Constraint PK_Lista_id Primary Key (List_id),
							Constraint FK_Planta_id foreign key (Planta_Id) references Planta(Planta_Id)
)

Create Table LineaFacturacion(	LineaFacturacion_Planta int not null,
								LineaFacturacion_Cantidad int not null,
								LineaFacturacion_PrecioUnitario int not null
								Constraint PK_LineaFacturacion_Planta Primary Key (LineaFacturacion_Planta),
								Constraint FK_LineaFacturacion_Planta Foreign Key (LineaFacturacion_Planta) references Planta(Planta_Id)								
)

Create Table Compra(Compra_Id int identity (1,1) not null,
					Compra_Fecha DateTime not null,
					Compra_Facturacion int not null,
					Constraint PK_Compra Primary Key (Compra_Id),
					Constraint FK_CompraFacturacion Foreign Key (Compra_Facturacion) references LineaFacturacion(LineaFacturacion_Planta)					
)

--Usuarios
Insert into Usuario values('Luis','Lopez','lucho@hotmail.com','Lucho123')
Insert into Usuario values('Karelina','Fabra','kare@gmail.com','Kare123')
Insert into Usuario values('Federico','Stole','fede@hotmail.com','Fede123')

--Tipo de Plantas
Insert into TipoDePlanta values ('Arbol','Estas plantas tienen un tallo leñoso con una altura superior a los 6 metros')
Insert into TipoDePlanta values ('Arbusto','Miden entre uno y seis metros de altura, sus ramas son cortas y comienzan a nivel de la tierra.')
Insert into TipoDePlanta values ('Hierbas','Estas plantas tienen tallos que no han desarrollado estructuras leñosas o endurecidas.')
Insert into TipoDePlanta values ('Gimnospermas','Aunque tienen flores, la función reproductora es llevada a cabo por sus hojas en forma de escamas')
Insert into TipoDePlanta values ('Angiospermas','Tienen flores y son las encargadas de la reproducción a través de los frutos y semillas.')
Insert into TipoDePlanta values ('Anuales','Son aquellas que suelen crecer rápidamente, pero tienen un tiempo de vida muy corto.')
Insert into TipoDePlanta values ('Bienales','Son aquellas plantas que necesitan dos estaciones desde que se siembran hasta que florecen.')
Insert into TipoDePlanta values ('Perennes','Estas suelen vivir más de dos años, puesto que tienen tallos herbáceos')
Insert into TipoDePlanta values ('Trepadoras','Son plantas con tallos no erectos y estructuras con forma de ganchos.')
Insert into TipoDePlanta values ('Ornamentales','Este tipo de plantas son usadas con fines decorativos o de coleccionismo.')
Insert into TipoDePlanta values ('Suculentas','Logran modificar cualquiera de sus estructuras (hojas, tallos, raíces) para almacenar agua.')
Insert into TipoDePlanta values ('Tuberosas','Se desarrollan a través de un tubérculo')
Insert into TipoDePlanta values ('Acuaticas','Este tipo de especies de plantas viven normalmente sobre el agua o en los estanques')

--Tipo de Iluminacion
Insert into TipoDeIluminacion values('Luz solar directa')
Insert into TipoDeIluminacion values('Luz solar indirecta')
Insert into TipoDeIluminacion values('Sombra')

--Ficha de Cuidado
Insert into FichaDeCiudado values(2,'dias',20,1)
Insert into FichaDeCiudado values(2,'semanas',15,2)
Insert into FichaDeCiudado values(5,'dias',24,3)
Insert into FichaDeCiudado values(2,'semanas',11,1)
Insert into FichaDeCiudado values(1,'dia',25,2)
Insert into FichaDeCiudado values(4,'dias',14,3)
Insert into FichaDeCiudado values(1,'semana',11,3)
Insert into FichaDeCiudado values(4,'semanas',10,2)
Insert into FichaDeCiudado values(2,'dias',17,1)
Insert into FichaDeCiudado values(1,'dia',20,1)
Insert into FichaDeCiudado values(8,'dias',23,3)
Insert into FichaDeCiudado values(15,'dias',19,1)

--Planta
insert into planta values(1,'Cedrelatubiflora','Es una especie botanica de arbol de la clase dicots familia de las Meliaceas','Exterior',756,'Cedrela_tubiflora.png',8,1140)
insert into planta values(1,'Robinia pseudoacacia','Es de las tres «falsas acacias» plantadas en ciudades del mundo para adornar calles y parques.','Exterior',2500,'Robinia_pseudoacacia.jpg',8,5140)
insert into planta values(1,'Cercis siliquastrum','es una especie arbórea de la familia de las leguminosas','Exterior',400,'Cercis_siliquastrum.png',8,640)
insert into planta values(1,'Brachychiton acerifolium','Es una especie arbórea nativa de regiones subtropicales de la costa este de Australia','Exterior',900,'Brachychiton_acerifolium.png',8,2540)
insert into planta values(1,'Erythrina crista-galli',' Es un árbol de la familia Fabaceae originario de Sudamérica','Exterior',500,'Erythrina_crista-galli.jpg',4,740)
insert into planta values(1,'Platanus','Es un árbol monoico, caducifolio de ramas abiertas y amplia copa.','Exterior',4500,'Platanus.png',8,1490)
insert into planta values(2,'Salix','La madera de los sauces es dura y flexible. Poseen esbeltas y fibrosas ramas','Exterior',950,'Salix.jpg',8,8456)
insert into planta values(1,'Pinus strobus','Este pino gusta del frío, suelos húmedos bien drenados y crecimiento en bosques mixtos','Exterior',4000,'Pinus_strobus.png',8,1420)
insert into planta values(1,'Ageratum houstonianum','El Agerato es una planta vivaz y herbácea que presenta hojas opuestas con forma lanceolada','Exterior',40,'Ageratum houstonianum.jpg',11,440)
--interior
insert into planta values(3,'Polianthes tuberosa','Son plantas de raíces con tubérculos, rizoma de almacenamiento corto, erecto, cilíndrico','Interior',100,'Polianthes_tuberosa.png',1,240)
insert into planta values(12,'Agapanthus','Posee un tallo corto que porta varias hojas alargadas, arciformes','Interior',50,'Agapanthus.jpg',3,330)
insert into planta values(10,'Pilea peperomioides','Esta planta se caracteriza por tener hojas redondas, peltadas y de color verde oscuro','Interior',80,'Pilea peperomioides.png',5,465)
insert into planta values(11,'Echeveria elegans','Sus hojas ovales, azuladas e hinchadas de agua adquieren todo el protagonismo','Interior',90,'Echeveria_elegans.png',10,150)
insert into planta values(10,'Dracena Fragans',' En jardinería se conoce popularmente como Tronco del Brasil, Palo de Brasil o Palo de Agua.','Interior',110,'Dracena_Fragans.png',1,790)

--Nombres Vulgares
Insert into ListaNVulgares values(1,'Cedro Misionero')				--1		Cedrela tubiflora			1
Insert into ListaNVulgares values(2,'Acacia')						--2		Robinia pseudoacacia		2	
Insert into ListaNVulgares values(2,'Verdadera Acacia')				--3		Robinia pseudoacacia		2
Insert into ListaNVulgares values(3,'Árbol de Judas')				--4		Cercis siliquastrum			3
Insert into ListaNVulgares values(3,'Ciclamor')						--5		Cercis siliquastrum			3
Insert into ListaNVulgares values(3,'Árbol de Judea')				--6		Cercis siliquastrum			3
Insert into ListaNVulgares values(4,'Árbol de la Llama')			--7		Brachychiton acerifolium	4
Insert into ListaNVulgares values(5,'Ceibo')						--8		Erythrina crista-galli		5
Insert into ListaNVulgares values(6,'Plátano')						--9		Platanus					6
Insert into ListaNVulgares values(7,'Sauce')						--10	Salix						7
Insert into ListaNVulgares values(8,'Pino Blanco')					--11	Pinus strobus				8
Insert into ListaNVulgares values(9,'Damasquino')					--12	Ageratum houstonianum		9
Insert into ListaNVulgares values(10,'El nardo')					--13	Polianthes tuberosa			10
Insert into ListaNVulgares values(11,'Agapanto')					--14	Agapanthus					11
Insert into ListaNVulgares values(12,'Planta China del Dinero')		--15	Pilea peperomioides			12
Insert into ListaNVulgares values(13,'Rosa de Alabastro')			--16	Echeveria elegans			13
Insert into ListaNVulgares values(14,'Palo de Agua')				--17	Drácena Fragans				14



select * from FichaDeCiudado
select * from ListaNVulgares
select * from Planta
select * from TipoDePlanta