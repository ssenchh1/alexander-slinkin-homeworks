
--Задание 1

SELECT p.ProductID, p.Name, p.ProductNumber, p.Color
from [SalesLT].Product p

select c.CustomerID, c.FirstName, c.MiddleName, c.LastName, c.EmailAddress, c.Phone
from SalesLT.Customer c

--Задание 2

select p.ProductID, p.Name, p.ProductNumber, p.Color
from SalesLT.Product p
where p.Color = 'Black'

select p.ProductID, p.Name, p.ProductNumber, p.Color
from SalesLT.Product p
where p.Color in ('Black','Silver', 'Multi')

select p.ProductID, p.Name, p.ProductNumber, p.Color
from SalesLT.Product p
where p.Color = 'Black' or p.Color = 'Yellow'

select p.ProductID, p.Name, p.ProductNumber, p.Weight
from SalesLT.Product p
where p.Weight is NULL

select p.ProductID, p.Name, p.ProductNumber, p.Weight
from SalesLT.Product p
where p.Weight > 1000

select p.ProductID, p.Name, p.ProductNumber, p.Weight
from SalesLT.Product p
where p.Weight < 6000

select p.ProductID, p.Name, p.ProductNumber, p.Weight
from SalesLT.Product p
where p.Weight between 2000 and 5000

select p.ProductID, p.Name, p.ProductNumber, p.Weight
from SalesLT.Product p
where p.ProductNumber like 'B[KB]%'

select p.ProductID, p.Name, p.ProductNumber, p.SellEndDate
from SalesLT.Product p
where p.SellEndDate is null

--Задание 3

select p.ProductID, p.Name, p.ProductNumber, p.Color
from SalesLT.Product p
order by p.Color

select p.ProductID, p.Name, p.ProductNumber, p.Color, p.Weight
from SalesLT.Product p
order by p.Color, p.weight desc

select p.ProductID, p.Name, p.ProductNumber, p.Weight
from SalesLT.Product p
order by p.ProductNumber, p.weight desc

--Задание 4

select top(10) p.ProductID, p.Name, p.ProductNumber, p.Weight
from SalesLT.Product p

select top(10) p.ProductID, p.Name, p.ProductNumber, p.Weight
from SalesLT.Product p
order by p.Weight

select top(10) p.ProductID, p.Name, p.ProductNumber, p.Weight
from SalesLT.Product p
order by p.Weight desc

select p.ProductID, p.Name, p.ProductNumber, p.Weight
from SalesLT.Product p
order by p.Weight 
offset 10 rows
fetch next 10 rows only

--Задание 5

select p.ProductID, p.Name, p.ProductNumber,p.Color, p.Weight, concat((d.UnitPriceDiscount*100), '%') as discount
from SalesLT.Product p
	join SalesLT.SalesOrderDetail d on d.ProductID = p.ProductID

select c.CustomerID, c.FirstName, c.MiddleName, c.LastName, c.EmailAddress, c.Phone, a.City, a.CountryRegion, a.PostalCode, a.AddressLine1, a.AddressLine2
from SalesLT.Customer c
	join SalesLT.CustomerAddress ca on ca.CustomerID = c.CustomerID
	join SalesLT.Address a on a.AddressID = ca.AddressID

select p.ProductID, p.Name, p.ProductNumber, p.ProductCategoryID, c.ParentProductCategoryID
from SalesLT.Product p
join SalesLT.ProductCategory c on c.ProductCategoryID = p.ProductCategoryID

--Задание 6

select COUNT(*)
from SalesLT.Product

select count(*)
from SalesLT.Product p
where p.SellEndDate < GETDATE()

select COUNT(*)
from SalesLT.Product p
where p.Weight is NULL

select AVG(p.Weight) as 'average weight'
from SalesLT.Product p
where p.Weight is NOT NULL

select AVG(p.Weight) as 'average weight'
from SalesLT.Product p

select MIN(p.Weight) as minimum, MAX(p.Weight) as maximum
from SalesLT.Product p

select p.ProductCategoryID, p.Name, Count(p.ProductID) as prodsInCategory, sum(p.Weight) as summa, MAX(p.Weight) as maximum, MIN(p.Weight) as minimum, AVG(p.Weight) as average
from SalesLT.Product p
group by p.ProductCategoryID, p.Name

select p.ProductCategoryID, c.Name, SUM(p.Weight) as WeightSumma
from SalesLT.Product p
join SalesLT.ProductCategory c on p.ProductCategoryID = c.ProductCategoryID
group by p.ProductCategoryID, c.Name

select p.ProductCategoryID, c.Name, SUM(p.Weight) as WeightSumma
from SalesLT.Product p
join SalesLT.ProductCategory c on p.ProductCategoryID = c.ProductCategoryID
group by p.ProductCategoryID, c.Name
having SUM(p.Weight) is not null

select p.ProductCategoryID, p.Name, Count(p.ProductID) as prodsInCategory, sum(p.Weight) as summa, MAX(p.Weight) as maximum, MIN(p.Weight) as minimum, AVG(p.Weight) as average
from SalesLT.Product p
group by p.ProductCategoryID, p.Name
having Max(p.Weight)>10000

--Задание 7

select p.ProductCategoryID, c.Name, Sum(d.LineTotal) as SoldSumma
from SalesLT.Product p
join SalesLT.ProductCategory c on p.ProductCategoryID = c.ProductCategoryID
join SalesLT.SalesOrderDetail d on d.ProductID = p.ProductID
group by p.ProductCategoryID, c.Name

select c.FirstName, Max(d.UnitPriceDiscount) as maxDiscount
from SalesLT.Customer c
join SalesLT.SalesOrderHeader h on h.CustomerID = c.CustomerID
join SalesLT.SalesOrderDetail d on d.SalesOrderID = h.SalesOrderID
where d.UnitPriceDiscount = 0.40
group by c.FirstName
order by Max(d.UnitPriceDiscount)

select c.CustomerID, Sum(h.TotalDue) as total
from SalesLT.Customer c
join SalesLT.SalesOrderHeader h on h.CustomerID = c.CustomerID
group by c.CustomerID
having Sum(h.TotalDue)>15000