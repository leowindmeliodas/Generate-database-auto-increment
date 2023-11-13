CREATE DATABASE MaTuSinh;
Go
USE MaTuSinh;
Go

create table Khoa
(
	MaKhoa varchar(20) primary key,
	TenKhoa nvarchar(50),
)


--Data Khoa
insert into Khoa(MaKhoa, TenKhoa) values ('MK01',N'Công nghệ thông tin');
insert into Khoa(MaKhoa, TenKhoa) values ('MK02',N'Quản trị kinh doanh');
insert into Khoa(MaKhoa, TenKhoa) values ('MK03',N'Kế toán');
insert into Khoa(MaKhoa, TenKhoa) values ('MK04',N'Kỹ thuật nhiệt');
insert into Khoa(MaKhoa, TenKhoa) values ('MK5',N'Tài chính ngân hàng');

select * from Khoa