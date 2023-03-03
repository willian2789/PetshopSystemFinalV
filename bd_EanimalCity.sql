create database bd_eAnimalcity;

use bd_eAnimalcity;

-- ===============[ CLIENTE ] ========================
create table tbl_cliente (
cd_cliente int primary key auto_increment,
nm_cliente varchar(80) not null,
email_cliente varchar(80) unique not null,
senha varchar(30) not null,
cpf_cliente char(14) not null,
image_cliente varchar(300),
no_telefone char(15) not null,
nm_logradouro varchar(80),
no_Cep char(9) not null,
ds_Complemento varchar(50),
nm_Bairro varchar(50),
no_Logradouro int not null,
sg_StatusCli bit not null
);

select * from tbl_cliente;

drop procedure if exists sp_InserirCliente; 
DELIMITER //
create procedure sp_InserirCliente(
IN vNome varchar(80),	
IN vEmail varchar(80),
IN vSenha varchar(30),
IN vCPF char(14),
IN vImage varchar(300),
IN vNoTelefone char(15),
IN vNmLogradouro varchar(80),
IN vNoCep char(9),
IN vDsComplemento varchar(50),
IN vNmBairro varchar(50),
IN vNoLogradouro varchar(80),
IN vStatus bit
)
	BEGIN
		insert into tbl_cliente values(default, vNome, vEmail, vSenha, vCPF, vImage, vNoTelefone,vNmLogradouro, vNoCep, vDsComplemento, vNmBairro, vNoLogradouro, vStatus);
	END //
DELIMITER ;

call sp_InserirCliente("Kauan Costa", "kaka@gmail.com", "123123","123.123.123-10", "http://github.com/kauan777.png", "(11)93840-2948", "Rua Roberto Macedo", "40599-220", "", "Morro Doce", "34", 1);
select * from tbl_cliente;
select * from tbl_pj;

-- ===============[ PET ] ========================
drop table tbl_pet;
create table tbl_Pet(
cd_pet int primary key auto_increment,
nm_pet varchar(30) not null,
image_pet varchar(300) not null,
raca_pet varchar(20) not null,
sexo_pet varchar(10) not null,
porte_pet varchar(7) not null,
cd_especie int,
cd_cliente int,
foreign key (cd_cliente) references tbl_cliente (cd_cliente),
foreign key (cd_especie) references tbl_especie (cd_especie)
);

create view pet_view as
select tbl_pet.cd_pet,
tbl_pet.nm_pet,
tbl_pet.image_pet,
tbl_pet.raca_pet,
tbl_pet.sexo_pet,
tbl_pet.porte_pet,
tbl_especie.cd_especie,
tbl_especie.nm_especie,
tbl_cliente.cd_cliente
from tbl_pet inner join tbl_especie
on tbl_pet.cd_especie = tbl_especie.cd_especie inner join tbl_cliente
on tbl_pet.cd_cliente = tbl_cliente.cd_cliente;



select * from pet_view where cd_cliente = 1  order by cd_pet desc;
select * from pet_view where cd_pet = 13;
	

drop procedure if exists sp_inserirPet; 
DELIMITER //
create procedure sp_inserirPet(
IN vNomePet varchar(30),
IN vImagePet varchar(300),
IN vRacaPet varchar(20),
IN vSexoPet varchar(10),
IN vPortePet varchar(10),
IN vCodEspecie int,
IN vCodCliente int
)
	BEGIN
		insert into tbl_pet values(default, vNomePet, vImagePet, vRacaPet, vSexoPet, vPortePet, vCodEspecie, vCodCliente);
	END //
DELIMITER ;

call sp_inserirPet("Chefe", "https://github.com/kauan777.png", "Pastor Bonito", "Femea", "Pequeno", 1, 1);

select * from tbl_especie;
create table tbl_especie(
cd_especie int primary key auto_increment,
nm_especie varchar(20) not null
);
insert into tbl_especie values (default,'Gato');

-- ===============[ FUNCIONARIO ] ========================

create table tbl_funcionario(
cd_func int primary key auto_increment,
nm_func varchar(80) not null,
nivel_func varchar(20) not null,
login varchar(50) unique not null,
senha char(6) not null,
image_func varchar(300)
);


insert into tbl_funcionario values (default,'Alexia','admin','Lexi@gmail.com','123456',1);
select * from tbl_funcionario;

-- ===============[ FORNECEDOR ] ========================
drop table tbl_fornecedor;
create table tbl_fornecedor(
cd_fornecedor int primary key auto_increment,
nm_fornecedor varchar(80) not null,
no_telefone char(15) not null,
email_fornecedor varchar(80) not null,
cnpj_fornecedor char(18) not null
);
insert into tbl_fornecedor values (default,'Kauan','(11)93840-2948','kauan@gmail.com','22.543.234/0001-43');
select * from tbl_fornecedor;

-- =================[  PRODUTOS ]======================

select * from tbl_Produto;
desc table tbl_produto;

create view vw_produto as
select cd_produto,
	   nm_produto,
       image_produto,
       marca_produto,
       qt_produto,
       vl_produto,
       ds_prod,
       tbl_categoria.cd_categoria,
       tbl_categoria.ds_categoria,
       tbl_fornecedor.nm_fornecedor
       from tbl_produto inner join tbl_categoria
       on tbl_produto.cd_categoria = tbl_categoria.cd_categoria
       inner join tbl_fornecedor on
       tbl_fornecedor.cd_fornecedor = tbl_produto.cd_fornecedor;

select * from vw_produto;
select * from vw_produto where nm_produto like '%bolinha%' order by cd_produto desc;

delete from tbl_produto;

select * from tbl_carrinho;
select * from tbl_compra;



select * from tbl_produto;
create table tbl_Produto(
cd_produto int primary key auto_increment,
cd_fornecedor int,
cd_categoria int,
nm_produto varchar(50) not null,
image_produto varchar(300) not null,
marca_produto varchar(50) not null,
qt_produto int not null,
vl_produto decimal(10,2) not null,
ds_prod varchar(300) not null,
foreign key (cd_fornecedor) references tbl_fornecedor(cd_fornecedor),
foreign key (cd_categoria) references tbl_categoria(cd_categoria)
);

select * from tbl_produto;

alter table tbl_produto modify vl_produto varchar(20);
insert into tbl_Produto values (default,1,1,'Bolinha Verde','https://github.com/kauan777.png','Art Injet',10, 15, 'Bolinha de borracha para seu pet passar a tarde toda brincando');

select * from tbl_categoria;

create table tbl_categoria(
cd_categoria int primary key auto_increment,
ds_categoria varchar(50) not null
);

insert into tbl_categoria values (default,'brinquedos');
select * from tbl_categoria;
	   
-- ===================[ CARRINHO ]=====================

create table tbl_carrinho (
cd_carrinho int primary key auto_increment,
cd_produto int,
cd_compra int,
cd_pagamento int,
qt_venda int,
vl_parcial varchar (10),
vl_total varchar (10),
foreign key (cd_compra) references tbl_compra(cd_compra),
foreign key (cd_pagamento) references tbl_pagamento(cd_pagamento),
foreign key (cd_produto) references tbl_produto(cd_produto)
);
select * from tbl_produto;
insert into tbl_carrinho values(default,2,1, 200, 12);
select * from tbl_carrinho;

select * from tbl_pagamento;

create view vw_carrinho as
select 
	   tbl_compra.cd_compra,
	   tbl_compra.vl_total,
	   tbl_produto.cd_produto,
	   nm_produto,
       image_produto,
       marca_produto,
       qt_produto,
       vl_produto,
       ds_prod,
       tbl_categoria.ds_categoria
       from tbl_produto inner join tbl_carrinho
       on tbl_produto.cd_produto = tbl_carrinho.cd_produto
       inner join tbl_compra on
       tbl_carrinho.cd_compra = tbl_compra.cd_compra 
       inner join tbl_categoria on
       tbl_produto.cd_categoria = tbl_categoria.cd_categoria;

select * from vw_carrinho where cd_compra = 1;

-- =======================[ COMPRA ]============================

create table tbl_Compra(
cd_Compra int primary key auto_increment,
cd_Pagamento int,
cd_Cliente int,
vl_Total decimal(10,2),
dt_compra char(10),
foreign key (cd_cliente) references tbl_cliente(cd_cliente),
foreign key (cd_Pagamento) references tbl_pagamento(cd_pagamento)
);

ALTER TABLE tbl_compra add qt_Produto varchar(20);

select * from tbl_compra;
select * from tbl_pagamento;

insert into tbl_compra values (default, 1, 1, 205.00, "14/05/2022");

create view compra_view

as
select tbl_compra.cd_compra,
tbl_pagamento.ds_pagamento,
tbl_compra.vl_total,
tbl_cliente.cd_Cliente,
tbl_compra.dt_compra from tbl_compra
inner join tbl_pagamento on
tbl_compra.cd_pagamento = tbl_pagamento.cd_pagamento
inner join tbl_cliente on
tbl_compra.cd_cliente = tbl_cliente.cd_Cliente;

drop view compra_view;

select * from compra_view;

-- ==================[ AGENDAMENTO ]========================
select * from tbl_agendamento;

create table tbl_agendamento(
cd_agendamento int primary key auto_increment,
cd_cliente int,
cd_pagamento int,
cd_pet int,
dt_agendamento varchar(10) not null,
vl_total decimal(10,2) not null,
ds_agendamento varchar(30) not null,
foreign key (cd_cliente) references tbl_cliente(cd_cliente),
foreign key (cd_Pagamento) references tbl_pagamento(cd_pagamento),
foreign key (cd_pet) references tbl_Pet(cd_pet)
);
insert into tbl_agendamento values(default,1,1,15,"2022/10/08",200.00,'banho bao');
insert into tbl_agendamento_servicos values(2,2);


create table tbl_agendamento_servicos(
cd_servicos int,
cd_agendamento int,
primary key (cd_servicos,cd_agendamento),
foreign key (cd_agendamento) references tbl_agendamento(cd_agendamento),
foreign key (cd_servicos) references tbl_servicos (cd_servicos)
);

create view vw_agendaserv 
 as 
 select
 s.cd_servicos,
 s.nm_servico,
 s.vl_servico,
 a.cd_agendamento,
 a.ds_agendamento,
 p.cd_pet,
 p.nm_pet,
 p.raca_pet
 from tbl_servicos as s inner join tbl_agendamento_servicos
 on s.cd_servicos = tbl_agendamento_servicos.cd_servicos
 inner join tbl_agendamento as a 
 on tbl_agendamento_servicos.cd_agendamento = a.cd_agendamento
 inner join tbl_pet as p 
 on a.cd_pet = p.cd_pet;
 
 select * from vw_agendaserv;

create table tbl_servicos(
cd_servicos int primary key auto_increment,
vl_servico decimal(10,2) not null,
nm_servico varchar(30) not null
);

insert into tbl_servicos values(default,300.00,'tosa');


select * from tbl_pagamento;
drop table tbl_pagamento;
create table tbl_pagamento(
cd_pagamento int primary key auto_increment,
ds_pagamento varchar(50) not null
);
insert into tbl_Pagamento values (default,'Cartão de credito'),(default,'Cartão de débito'),(default,'Boleto'),(default,'Pix');






