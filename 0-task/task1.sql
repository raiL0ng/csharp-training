
create table Department
(
[ID] int not null identity(1, 1) primary key,
[Name] nvarchar(100) not null
);

create table Employee
(
[ID] int not null identity(1, 1) primary key,
[Department_ID] int not null,
[Chief_ID] int null,
[Name] nvarchar(100) not null,
[Salary] int not null
);

alter table Employee
add foreign key (Department_ID) references Department(ID)

--alter table Employee
--add foreign key (Chief_ID) references Employee(Chief_ID)

-- Добавление некоторых данных
INSERT INTO Department (Name) VALUES
(N'Финансовый отдел'),
(N'Отдел продаж'),
(N'Отдел маркетинга'),
(N'Технический отдел'),
(N'Юридический отдел'),
(N'Отдел кадров'),
(N'Исследовательский отдел');

INSERT INTO Employee (Department_ID, Chief_ID, Name, Salary) VALUES
(1, null, N'Александр', 55000),
(1, 1, N'Евгения', 60000),
(2, 1, N'Андрей', 65000),
(2, 2, N'Ольга', 70000),
(3, 2, N'Дмитрий', 45000),
(4, 3, N'Ирина', 75000),
(4, 4, N'Сергей', 80000),
(5, 4, N'Екатерина', 85000),
(6, 4, N'Роман', 50000),
(6, 4, N'Наталья', 55000),
(6, 5, N'Ренат', 40000),
(6, 5, N'Олег', 55000);

-- Напишите запросы, которые выведут:
--    1. Сотрудника с максимальной заработной платой.
--    2. Вывести одно число: максимальную длину цепочки руководителей по таблице сотрудников (вычислить глубину дерева).
--    3. Отдел, с максимальной суммарной зарплатой сотрудников. 
--    4. Сотрудника, чье имя начинается на «Р» и заканчивается на «н».

-- Запрос #1

SELECT Top 1 * FROM Employee
ORDER BY Salary DESC

-- Запрос #2

-- Если я правильно понял данное задание, то у нас есть какой-то главный руководитель, у которого
-- значение Chief_ID = null (и он такой один), тогда нахождение глубины дерева можно посчитать следующим образом:

WITH cte_find_lvl AS
(
  SELECT e.ID, e.Chief_ID, 1 AS lvl FROM Employee e where e.Chief_ID is null
  UNION ALL
  SELECT e.ID, e.Chief_ID, lvl + 1 FROM Employee e
  JOIN cte_find_lvl c
  on e.Chief_ID = c.id
)
SELECT MAX(lvl) FROM cte_find_lvl

-- P.S. Будем считать, что корень находится на первом уровне, а не на нулевом.

-- Запрос #3

-- Согласно заданию нужно вывести только отдел, поэтому в данном запросе я и вывожу только его
SELECT TOP 1 d.Name as Department_Name
FROM Department d
JOIN Employee e ON e.Department_ID = d.ID
GROUP BY d.Name
ORDER BY SUM(e.Salary) DESC

-- Чтобы убедится в корректности запроса выше, я сделал вот такой запрос:
SELECT d.Name as Department_Name, SUM(e.Salary) as Total_salary
FROM Department d
JOIN Employee e ON e.Department_ID = d.ID
GROUP BY d.Name
ORDER BY Total_salary DESC

-- Запрос #4

-- Будем считать, что в столбце Name таблицы Employee вводится только одно имя, тогда:
SELECT * FROM Employee 
WHERE Name LIKE N'Р%н'