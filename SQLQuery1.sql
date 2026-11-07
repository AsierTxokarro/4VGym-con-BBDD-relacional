USE [4Vgym];
GO

-- =============================================
-- 1. INSERTAR TIPOS DE ACTIVIDAD
-- =============================================

DECLARE @YogaId INT;
DECLARE @SpinningId INT;
DECLARE @PilatesId INT;

INSERT INTO ActivityTypes (Name, NumberMonitors)
VALUES ('Yoga', 1);

SET @YogaId = SCOPE_IDENTITY();

INSERT INTO ActivityTypes (Name, NumberMonitors)
VALUES ('Spinning', 2);

SET @SpinningId = SCOPE_IDENTITY();

INSERT INTO ActivityTypes (Name, NumberMonitors)
VALUES ('Pilates', 1);

SET @PilatesId = SCOPE_IDENTITY();


-- =============================================
-- 2. INSERTAR INSTRUCTORES
-- =============================================

DECLARE @Instructor1 INT;
DECLARE @Instructor2 INT;
DECLARE @Instructor3 INT;

INSERT INTO Instructors (Name, Email, PhotoUrl, Phone)
VALUES (
    'Laura García',
    'laura.garcia@test.com',
    'https://example.com/laura.jpg',
    '600111222'
);

SET @Instructor1 = SCOPE_IDENTITY();

INSERT INTO Instructors (Name, Email, PhotoUrl, Phone)
VALUES (
    'Carlos López',
    'carlos.lopez@test.com',
    'https://example.com/carlos.jpg',
    '600333444'
);

SET @Instructor2 = SCOPE_IDENTITY();

INSERT INTO Instructors (Name, Email, PhotoUrl, Phone)
VALUES (
    'Marta Sánchez',
    'marta.sanchez@test.com',
    'https://example.com/marta.jpg',
    '600555666'
);

SET @Instructor3 = SCOPE_IDENTITY();


-- =============================================
-- 3. INSERTAR ACTIVIDADES
-- =============================================

INSERT INTO Activities
    (ActivityTypeId, InstructorsId, MaxParticipants, DateStart, DateEnd)
VALUES
    (@YogaId, CAST(@Instructor1 AS VARCHAR(20)), 15,
     '2026-11-10T09:00:00', '2026-11-10T10:00:00'),

    (@SpinningId, CAST(@Instructor2 AS VARCHAR(20)), 20,
     '2026-11-11T11:00:00', '2026-11-11T12:00:00'),

    (@PilatesId, CAST(@Instructor3 AS VARCHAR(20)), 12,
     '2026-11-12T17:00:00', '2026-11-12T18:00:00');


-- =============================================
-- 4. COMPROBAR LOS DATOS INSERTADOS
-- =============================================

SELECT * FROM ActivityTypes;
SELECT * FROM Instructors;
SELECT * FROM Activities;
GO