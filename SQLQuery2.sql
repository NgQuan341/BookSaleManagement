create database quanlybansach;


create table log_sohd(
sohd int);

create table nhanvien(
manv int identity(1,1) primary key,
tennv nvarchar(50),
diachinv nvarchar(100),
ngaysinh date,
)
create table khachhang(
makh int identity(1,1) primary key,
tenkh nvarchar(50),
diachikh nvarchar(100),
sdt varchar(11)
)
create table sach(
masach int identity(1,1) primary key,
tensach nvarchar(50),
tacgia nvarchar(50),
nxb nvarchar(20),
soluong int,
dongia decimal
)
create table hoadon(
sohd int identity(1,1) primary key,
ngayban date,
manv int constraint frk_manv foreign key (manv)
references nhanvien(manv) 
on delete cascade on update cascade,
makh int constraint frk_makh foreign key (makh)
references khachhang(makh)
on delete cascade on update cascade
)
create table chitiethd(
sohd int constraint frk_sohd foreign key (sohd)
references hoadon(sohd)
on delete cascade on update cascade,
masach int constraint frk_masach foreign key (masach)
references sach(masach) on delete cascade on update cascade,
soluongban int,
dongiaban decimal
)



insert into nhanvien(tennv,diachinv,ngaysinh) values(N'Hồng Quân',N'Quảng Trị','03/04/2001')
insert into nhanvien(tennv,diachinv,ngaysinh) values(N'Văn Phát',N'Quảng Trị','05/07/2001')
insert into nhanvien(tennv,diachinv,ngaysinh) values(N'Đình Kha',N'Quảng Trị','12/12/2001')
select * from nhanvien

insert into khachhang(tenkh,diachikh,sdt) values(N'Hoàng Huấn',N'Quảng Nam','0166346112')
insert into khachhang(tenkh,diachikh,sdt) values(N'Sơn Nam',N'Quảng Trị','0166346113')
insert into khachhang(tenkh,diachikh,sdt) values(N'Quang Kỳ',N'Quảng Trị','0166346114')
select * from khachhang

insert into sach(tensach,tacgia,nxb,soluong,dongia) values(N'Doraemon',N'Fujiko Fujio',N'Kim Đồng',100, 20000)
insert into sach(tensach,tacgia,nxb,soluong,dongia) values(N'Trạng Tí',N'Lê Linh',N'Kim Đồng', 90, 15000)
insert into sach(tensach,tacgia,nxb,soluong,dongia) values(N'Đắc Nhân Tâm',N'Dale Carnegie',N'HuanRose', 80, 180000)
insert into sach(tensach,tacgia,nxb,soluong,dongia) values(N'Muôn kiếp nhân sinh',N'Nguyên Phong',N'NXB Tổng Hợp TPHCM',70, 20000)
insert into sach(tensach,tacgia,nxb,soluong,dongia) values(N'Hiểu về trái tim',N'Minh Niệm',N'Trí Việt', 60, 11800)
insert into sach(tensach,tacgia,nxb,soluong,dongia) values(N'Mắt Biếc',N'Nguyễn Nhật Ánh',N'NXB Trẻ', 85, 155000)
select * from sach

insert into hoadon(ngayban,manv,makh) values('10/10/2021',1,2)
insert into hoadon(ngayban,manv,makh) values('10/11/2021',3,2)
insert into hoadon(ngayban,manv,makh) values('05/10/2021',2,2)
insert into hoadon(ngayban,manv,makh) values('03/05/2021',3,3)
insert into hoadon(ngayban,manv,makh) values('02/10/2021',1,2)


insert into chitiethd(sohd,masach,soluongban,dongiaban) values(1,1,30,20000)
insert into chitiethd(sohd,masach,soluongban,dongiaban) values(1,2,20,15000)
insert into chitiethd(sohd,masach,soluongban,dongiaban) values(1,3,30,180000)
insert into chitiethd(sohd,masach,soluongban,dongiaban) values(2,4,30,20000)
insert into chitiethd(sohd,masach,soluongban,dongiaban) values(3,5,30,11800)
insert into chitiethd(sohd,masach,soluongban,dongiaban) values(3,5,30,11800)
insert into chitiethd(sohd,masach,soluongban,dongiaban) values(4,5,30,11800)
insert into chitiethd(sohd,masach,soluongban,dongiaban) values(5,5,30,11800)

Create Trigger tr_SoHoaDon On hoadon For Insert
As
 Begin
   insert into log_sohd (sohd)
   select sohd from inserted
 End

select tensach,soluongban,dongiaban from chitiethd,sach where sohd=1 and chitiethd.masach=sach.masach
select hoadon.sohd,nhanvien.tennv,khachhang.tenkh,hoadon.ngayban from hoadon,khachhang,nhanvien where sohd = 1 and hoadon.makh=khachhang.makh and hoadon.manv=nhanvien.manv
select khachhang.tenkh from hoadon,khachhang where sohd = '1' and hoadon.makh=khachhang.makh



 select * from sach where tensach like '%tí%'