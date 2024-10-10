-- 创建数据库
CREATE DATABASE WorkflowManagerDB;
USE WorkflowManagerDB;

-- 创建员工表
CREATE TABLE Employees (
    EmployeeId INT AUTO_INCREMENT PRIMARY KEY, -- 员工ID，主键
    Name VARCHAR(50) NOT NULL,                 -- 员工姓名
    Position VARCHAR(50),                      -- 员工职位
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP -- 创建时间
);

-- 创建任务表
CREATE TABLE Tasks (
    TaskId INT AUTO_INCREMENT PRIMARY KEY,     -- 任务ID，主键
    Title VARCHAR(100) NOT NULL,               -- 任务名称
    Description TEXT,                          -- 任务描述
    Deadline DATETIME NOT NULL,                -- 任务截止时间
    Urgency INT NOT NULL CHECK (Urgency BETWEEN 1 AND 5),  -- 紧急度，范围1到5
    Type ENUM('Normal', 'Paid', 'Defect') NOT NULL,  -- 任务类型
    Status ENUM('Pending', 'InProgress', 'Review', 'Testing') NOT NULL DEFAULT 'Pending', -- 当前状态
    AssignedTo INT,                            -- 分配的员工ID
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP, -- 创建时间
    FOREIGN KEY (AssignedTo) REFERENCES Employees(EmployeeId) -- 关联员工表
);

-- 插入测试员工数据
INSERT INTO Employees (Name, Position) VALUES ('Eden', 'Developer');
INSERT INTO Employees (Name, Position) VALUES ('Alice', 'Tester');
INSERT INTO Employees (Name, Position) VALUES ('Bob', 'Project Manager');
INSERT INTO Employees (Name, Position) VALUES ('Charlie', 'Designer');
INSERT INTO Employees (Name, Position) VALUES ('David', 'QA Engineer');

-- 插入测试任务数据
INSERT INTO Tasks (Title, Description, Deadline, Urgency, Type, Status, AssignedTo) 
VALUES ('Design UI for new feature', 'Create a new UI for the upcoming feature.', '2024-10-30 12:00:00', 3, 'Normal', 'Pending', NULL);

INSERT INTO Tasks (Title, Description, Deadline, Urgency, Type, Status, AssignedTo) 
VALUES ('Fix payment bug', 'Resolve the defect in the payment module.', '2024-10-20 17:00:00', 5, 'Defect', 'InProgress', 1); -- Assigned to Eden

INSERT INTO Tasks (Title, Description, Deadline, Urgency, Type, Status, AssignedTo) 
VALUES ('Implement search functionality', 'Develop a search function for the user dashboard.', '2024-10-25 14:00:00', 4, 'Paid', 'Review', 2); -- Assigned to Alice

INSERT INTO Tasks (Title, Description, Deadline, Urgency, Type, Status, AssignedTo) 
VALUES ('Create project proposal', 'Prepare and submit the proposal for the new project.', '2024-10-18 09:00:00', 2, 'Normal', 'Testing', 3); -- Assigned to Bob

INSERT INTO Tasks (Title, Description, Deadline, Urgency, Type, Status, AssignedTo) 
VALUES ('Update user manual', 'Revise the user manual to reflect new updates.', '2024-10-22 16:00:00', 1, 'Normal', 'Pending', NULL);

INSERT INTO Tasks (Title, Description, Deadline, Urgency, Type, Status, AssignedTo) 
VALUES ('Optimize database queries', 'Improve the performance of database queries.', '2024-10-29 11:00:00', 4, 'Defect', 'Pending', NULL);

INSERT INTO Tasks (Title, Description, Deadline, Urgency, Type, Status, AssignedTo) 
VALUES ('Review code for security vulnerabilities', 'Conduct a security audit of the codebase.', '2024-11-02 12:00:00', 5, 'Paid', 'Pending', 4); -- Assigned to Charlie

INSERT INTO Tasks (Title, Description, Deadline, Urgency, Type, Status, AssignedTo) 
VALUES ('Test new login system', 'Ensure the new login system works across all platforms.', '2024-11-01 18:00:00', 3, 'Normal', 'Pending', 5); -- Assigned to David

INSERT INTO Tasks (Title, Description, Deadline, Urgency, Type, Status, AssignedTo) 
VALUES ('Integrate third-party API', 'Add support for the third-party API in the payment system.', '2024-10-28 10:00:00', 4, 'Paid', 'InProgress', 1); -- Assigned to Eden

INSERT INTO Tasks (Title, Description, Deadline, Urgency, Type, Status, AssignedTo) 
VALUES ('Create automated test scripts', 'Develop automated tests for the new feature.', '2024-10-23 14:30:00', 2, 'Normal', 'Review', 5); -- Assigned to David
