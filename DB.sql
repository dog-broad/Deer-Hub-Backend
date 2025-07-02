-- ============================
-- DEER Hub Full Schema + Queries
-- ============================

-- Drop existing tables
DROP TABLE IF EXISTS LeaveRequests, Announcements, Employees, Users, Departments, LeaveTypes, LeaveStatuses;

-- ========== TABLES ==========

-- 1. Users Table
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username VARCHAR(50) UNIQUE NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    PasswordHash VARCHAR(256) NOT NULL,
    Role VARCHAR(20) CHECK (Role IN ('Employee', 'Manager', 'Admin')),
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE(),
    ModifiedAt DATETIME
);

-- 2. Departments Table
CREATE TABLE Departments (
    DepartmentID INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(100) NOT NULL UNIQUE,
    Description VARCHAR(255)
);

-- 3. Employees Table
CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL UNIQUE FOREIGN KEY REFERENCES Users(UserID),
    FullName VARCHAR(100) NOT NULL,
    DepartmentID INT NOT NULL FOREIGN KEY REFERENCES Departments(DepartmentID),
    DateOfJoining DATE NOT NULL,
    PhoneNumber VARCHAR(15),
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE(),
    ModifiedAt DATETIME
);

-- 4. LeaveTypes Table
CREATE TABLE LeaveTypes (
    LeaveTypeID INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(50) NOT NULL UNIQUE,
    Description VARCHAR(255)
);

-- 5. LeaveStatuses Table
CREATE TABLE LeaveStatuses (
    StatusID INT PRIMARY KEY IDENTITY(1,1),
    StatusName VARCHAR(50) NOT NULL UNIQUE
);

-- 6. LeaveRequests Table
CREATE TABLE LeaveRequests (
    LeaveID INT PRIMARY KEY IDENTITY(1,1),
    EmployeeID INT NOT NULL FOREIGN KEY REFERENCES Employees(EmployeeID),
    LeaveTypeID INT NOT NULL FOREIGN KEY REFERENCES LeaveTypes(LeaveTypeID),
    StatusID INT NOT NULL FOREIGN KEY REFERENCES LeaveStatuses(StatusID),
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    Reason VARCHAR(255),
    Priority VARCHAR(20) CHECK (Priority IN ('Low', 'Normal', 'High', 'Urgent')) DEFAULT 'Normal',
    DocumentPath VARCHAR(255),
    RequestedAt DATETIME DEFAULT GETDATE(),
    ApprovedBy INT NULL FOREIGN KEY REFERENCES Employees(EmployeeID),
    IsDeleted BIT DEFAULT 0,
    ModifiedAt DATETIME
);

-- 7. Announcements Table
CREATE TABLE Announcements (
    AnnouncementID INT PRIMARY KEY IDENTITY(1,1),
    Title VARCHAR(100),
    Description VARCHAR(500),
    PostedBy INT FOREIGN KEY REFERENCES Employees(EmployeeID),
    PostedAt DATETIME DEFAULT GETDATE(),
    IsVisible BIT DEFAULT 1
);

-- ========== SAMPLE DATA ==========

-- Departments
INSERT INTO Departments (Name, Description) VALUES 
('Human Resources', 'Handles employee relations and policies'),
('IT', 'Maintains company tech and infrastructure'),
('Sales', 'Drives revenue through customer acquisition'),
('Finance', 'Manages financial records and budgets'),
('Operations', 'Ensures smooth business operations');

-- Leave Types
INSERT INTO LeaveTypes (Name, Description) VALUES 
('Sick Leave', 'Leave taken when employee is unwell'),
('Casual Leave', 'Planned personal leave'),
('Maternity Leave', 'Leave for new mothers'),
('Paternity Leave', 'Leave for new fathers'),
('Special Occasion Leave', 'Leave for events like weddings or functions'),
('Bereavement Leave', 'Leave due to death of a loved one');

-- Leave Statuses
INSERT INTO LeaveStatuses (StatusName) VALUES 
('Pending'), ('Approved'), ('Rejected'), ('Cancelled');

-- Users
INSERT INTO Users (Username, Email, PasswordHash, Role) VALUES 
('jdoe', 'jdoe@deerhub.com', 'hashed_pwd_1', 'Employee'),
('asmith', 'asmith@deerhub.com', 'hashed_pwd_2', 'Manager'),
('admin1', 'admin@deerhub.com', 'hashed_pwd_3', 'Admin'),
('bwayne', 'bwayne@deerhub.com', 'hashed_pwd_4', 'Employee'),
('ckent', 'ckent@deerhub.com', 'hashed_pwd_5', 'Employee'),
('dprince', 'dprince@deerhub.com', 'hashed_pwd_6', 'Manager'),
('pparker', 'pparker@deerhub.com', 'hashed_pwd_7', 'Employee');

-- Employees
INSERT INTO Employees (UserID, FullName, DepartmentID, DateOfJoining, PhoneNumber) VALUES 
(1, 'John Doe', 1, '2023-01-10', '1234567890'),
(2, 'Alice Smith', 2, '2022-09-01', '9876543210'),
(3, 'System Admin', 2, '2021-06-15', '0000000000'),
(4, 'Bruce Wayne', 3, '2022-03-20', '1112223333'),
(5, 'Clark Kent', 4, '2023-06-25', '4445556666'),
(6, 'Diana Prince', 1, '2021-12-11', '7778889999'),
(7, 'Peter Parker', 5, '2023-02-14', '1012023030');

-- Announcements
INSERT INTO Announcements (Title, Description, PostedBy) VALUES 
('Holiday Notice', 'Company will be closed on 4th July.', 2),
('Policy Update', 'New WFH policy effective next month.', 3),
('Maintenance Window', 'Scheduled downtime on Saturday 8 PM to 12 AM.', 3),
('Team Building Event', 'Outdoor activities planned for next Friday.', 2),
('Finance Training', 'Mandatory finance training on Q3 planning.', 6);

-- Leave Requests
INSERT INTO LeaveRequests (EmployeeID, LeaveTypeID, StatusID, StartDate, EndDate, Reason, Priority, DocumentPath, ApprovedBy) VALUES 
(1, 1, 1, '2024-07-10', '2024-07-12', 'Fever and weakness', 'High', 'medical_note.pdf', NULL),
(1, 5, 2, '2024-06-01', '2024-06-03', 'Family function', 'Normal', NULL, 2),
(2, 1, 3, '2024-05-15', '2024-05-16', 'Sick leave rejected', 'Urgent', NULL, 3),
(4, 2, 2, '2024-07-05', '2024-07-06', 'Short vacation', 'Normal', NULL, 2),
(5, 3, 1, '2024-08-01', '2024-10-31', 'Maternity leave planned', 'High', NULL, NULL),
(6, 6, 2, '2024-06-10', '2024-06-12', 'Attending funeral', 'High', NULL, 3),
(7, 4, 1, '2024-07-15', '2024-07-16', 'Newborn care', 'Normal', NULL, NULL),
(4, 2, 1, '2024-09-10', '2024-09-12', 'Family event', 'Normal', NULL, NULL),
(5, 1, 1, '2024-07-01', '2024-07-02', 'Flu symptoms', 'High', 'flu_report.pdf', NULL),
(4, 2, 1, '2025-07-10', '2024-07-12', 'Family event', 'Normal', NULL, NULL),
(6, 6, 2, '2025-06-10', '2025-06-12', 'Attending funeral', 'High', NULL, 3);

-- Update
UPDATE Users
SET Username = 'john.doe',
    ModifiedAt = GETDATE()
WHERE UserID = 1;

-- ========== ANALYTICAL QUERIES ==========

-- 1. Active Leave Requests per Department
SELECT d.Name AS Department, COUNT(l.LeaveID) AS ActiveLeaveCount
FROM LeaveRequests l
JOIN Employees e ON l.EmployeeID = e.EmployeeID
JOIN Departments d ON e.DepartmentID = d.DepartmentID
JOIN LeaveStatuses s ON l.StatusID = s.StatusID
WHERE s.StatusName IN ('Pending', 'Approved') AND l.IsDeleted = 0
GROUP BY d.Name
ORDER BY ActiveLeaveCount DESC;

-- 2. Employees per Department
SELECT d.Name AS Department, COUNT(e.EmployeeID) AS TotalEmployees
FROM Departments d
LEFT JOIN Employees e ON e.DepartmentID = d.DepartmentID AND e.IsActive = 1
GROUP BY d.Name
ORDER BY TotalEmployees DESC;

-- 3. Upcoming Leaves in Next 14 Days
SELECT e.FullName, d.Name AS Department, l.StartDate, l.EndDate, s.StatusName
FROM LeaveRequests l
JOIN Employees e ON l.EmployeeID = e.EmployeeID
JOIN Departments d ON e.DepartmentID = d.DepartmentID
JOIN LeaveStatuses s ON l.StatusID = s.StatusID
WHERE l.StartDate BETWEEN GETDATE() AND DATEADD(DAY, 14, GETDATE()) AND l.IsDeleted = 0
ORDER BY l.StartDate;

-- 4. Employees With No Leave History
SELECT e.EmployeeID, e.FullName, d.Name AS Department
FROM Employees e
JOIN Departments d ON e.DepartmentID = d.DepartmentID
LEFT JOIN LeaveRequests l ON l.EmployeeID = e.EmployeeID AND l.IsDeleted = 0
WHERE l.LeaveID IS NULL AND e.IsActive = 1;

-- 5. Leave Count Per Employee This Year
SELECT e.FullName, d.Name AS Department, COUNT(l.LeaveID) AS LeaveCount
FROM LeaveRequests l
JOIN Employees e ON l.EmployeeID = e.EmployeeID
JOIN Departments d ON e.DepartmentID = d.DepartmentID
WHERE YEAR(l.StartDate) = YEAR(GETDATE()) AND l.IsDeleted = 0
GROUP BY e.FullName, d.Name
ORDER BY LeaveCount DESC;

-- 6. Daily Leave Calendar
DECLARE @Date DATE = GETDATE();
SELECT e.FullName, d.Name AS Department, l.StartDate, l.EndDate, s.StatusName
FROM LeaveRequests l
JOIN Employees e ON l.EmployeeID = e.EmployeeID
JOIN Departments d ON e.DepartmentID = d.DepartmentID
JOIN LeaveStatuses s ON l.StatusID = s.StatusID
WHERE @Date BETWEEN l.StartDate AND l.EndDate AND l.IsDeleted = 0
ORDER BY e.FullName;

-- 7. Recently Modified Users or Employees
SELECT 'User' AS Entity, u.UserID AS ID, u.Username, u.ModifiedAt
FROM Users u
WHERE u.ModifiedAt IS NOT NULL
UNION
SELECT 'Employee', e.EmployeeID, e.FullName, e.ModifiedAt
FROM Employees e
WHERE e.ModifiedAt IS NOT NULL
ORDER BY ModifiedAt DESC;

-- 8. Announcement Count Per Employee
SELECT e.FullName AS PostedBy, COUNT(a.AnnouncementID) AS TotalPosts
FROM Announcements a
JOIN Employees e ON a.PostedBy = e.EmployeeID
GROUP BY e.FullName
ORDER BY TotalPosts DESC;

-- 9. Users and Access Roles
SELECT u.UserID, u.Username, u.Email, u.Role, u.IsActive, e.FullName, d.Name AS Department
FROM Users u
LEFT JOIN Employees e ON u.UserID = e.UserID
LEFT JOIN Departments d ON e.DepartmentID = d.DepartmentID
ORDER BY u.Role, u.IsActive DESC;
