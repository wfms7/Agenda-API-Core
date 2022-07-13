CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
);

START TRANSACTION;

CREATE TABLE `Paciente` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Nome` varchar(250) NOT NULL,
    `CPF` text NULL,
    `RG` text NULL,
    `NomeMae` varchar(250) NULL,
    `NomePai` varchar(250) NULL,
    `Endereco` text NULL,
    `Cep` text NULL,
    `Bairro` text NULL,
    `Complemento` text NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `Telefones` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Tipo` text NOT NULL,
    `Telefones` text NULL,
    `PacienteId` int NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Telefones_Paciente_PacienteId` FOREIGN KEY (`PacienteId`) REFERENCES `Paciente` (`Id`) ON DELETE CASCADE
);

CREATE INDEX `IX_Telefones_PacienteId` ON `Telefones` (`PacienteId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20220316192654_Criando Paciente e Telefone', '5.0.5');

COMMIT;

START TRANSACTION;

ALTER TABLE `Paciente` ADD `DataNascimento` datetime NOT NULL DEFAULT '0001-01-01 00:00:00.000000';

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20220404190046_Adcionar data nascimento novo', '5.0.5');

COMMIT;

START TRANSACTION;

ALTER TABLE `Paciente` ADD `Email` text NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20220405121151_add campo email no paciente', '5.0.5');

COMMIT;

START TRANSACTION;

CREATE TABLE `EspecialidadeMedicas` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Especialidade` text NOT NULL,
    PRIMARY KEY (`Id`)
);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20220424184340_Add Especilidade Medica', '5.0.5');

COMMIT;

START TRANSACTION;

DROP TABLE `EspecialidadeMedicas`;

CREATE TABLE `Especialidade` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Especialidades` text NOT NULL,
    PRIMARY KEY (`Id`)
);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20220426015040_Alterando Class EspecialidadeMadica Para Especialidade', '5.0.5');

COMMIT;

START TRANSACTION;

CREATE TABLE `AspNetRoles` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(256) NULL,
    `NormalizedName` varchar(256) NULL,
    `ConcurrencyStamp` text NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `AspNetUsers` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `DataNascimento` datetime NOT NULL,
    `CPF` text NULL,
    `RG` text NULL,
    `CRM` text NULL,
    `NomeMae` text NULL,
    `NomePai` text NULL,
    `UserName` varchar(256) NULL,
    `NormalizedUserName` varchar(256) NULL,
    `Email` varchar(256) NULL,
    `NormalizedEmail` varchar(256) NULL,
    `EmailConfirmed` tinyint(1) NOT NULL,
    `PasswordHash` text NULL,
    `SecurityStamp` text NULL,
    `ConcurrencyStamp` text NULL,
    `PhoneNumber` text NULL,
    `PhoneNumberConfirmed` tinyint(1) NOT NULL,
    `TwoFactorEnabled` tinyint(1) NOT NULL,
    `LockoutEnd` timestamp NULL,
    `LockoutEnabled` tinyint(1) NOT NULL,
    `AccessFailedCount` int NOT NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `AspNetRoleClaims` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `RoleId` int NOT NULL,
    `ClaimType` text NULL,
    `ClaimValue` text NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserClaims` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `UserId` int NOT NULL,
    `ClaimType` text NULL,
    `ClaimValue` text NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserLogins` (
    `LoginProvider` varchar(256) NOT NULL,
    `ProviderKey` varchar(256) NOT NULL,
    `ProviderDisplayName` text NULL,
    `UserId` int NOT NULL,
    PRIMARY KEY (`LoginProvider`, `ProviderKey`),
    CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserRoles` (
    `UserId` int NOT NULL,
    `RoleId` int NOT NULL,
    PRIMARY KEY (`UserId`, `RoleId`),
    CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserTokens` (
    `UserId` int NOT NULL,
    `LoginProvider` varchar(256) NOT NULL,
    `Name` varchar(256) NOT NULL,
    `Value` text NULL,
    PRIMARY KEY (`UserId`, `LoginProvider`, `Name`),
    CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE INDEX `IX_AspNetRoleClaims_RoleId` ON `AspNetRoleClaims` (`RoleId`);

CREATE UNIQUE INDEX `RoleNameIndex` ON `AspNetRoles` (`NormalizedName`);

CREATE INDEX `IX_AspNetUserClaims_UserId` ON `AspNetUserClaims` (`UserId`);

CREATE INDEX `IX_AspNetUserLogins_UserId` ON `AspNetUserLogins` (`UserId`);

CREATE INDEX `IX_AspNetUserRoles_RoleId` ON `AspNetUserRoles` (`RoleId`);

CREATE INDEX `EmailIndex` ON `AspNetUsers` (`NormalizedEmail`);

CREATE UNIQUE INDEX `UserNameIndex` ON `AspNetUsers` (`NormalizedUserName`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20220429024741_Criando User', '5.0.5');

COMMIT;

START TRANSACTION;

ALTER TABLE `AspNetUsers` ADD `Nome` text NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20220429095701_Add Nome em User', '5.0.5');

COMMIT;

START TRANSACTION;

CREATE TABLE `EspecialidadeDRs` (
    `ApplicationUserId` int NOT NULL,
    `EspecialidadeId` int NOT NULL,
    PRIMARY KEY (`ApplicationUserId`, `EspecialidadeId`),
    CONSTRAINT `FK_EspecialidadeDRs_AspNetUsers_ApplicationUserId` FOREIGN KEY (`ApplicationUserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_EspecialidadeDRs_Especialidade_EspecialidadeId` FOREIGN KEY (`EspecialidadeId`) REFERENCES `Especialidade` (`Id`) ON DELETE CASCADE
);

CREATE INDEX `IX_EspecialidadeDRs_EspecialidadeId` ON `EspecialidadeDRs` (`EspecialidadeId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20220514160553_Juntando Especialidade e User', '5.0.5');

COMMIT;

START TRANSACTION;

CREATE TABLE `agendaCalendarios` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `ApplicationUserId` int NOT NULL,
    `HoraIncio` text NULL,
    `HoraFim` text NULL,
    `Sunday` tinyint(1) NOT NULL,
    `Monday` tinyint(1) NOT NULL,
    `Tuesday` tinyint(1) NOT NULL,
    `Wednesday` tinyint(1) NOT NULL,
    `Thursday` tinyint(1) NOT NULL,
    `Friday` tinyint(1) NOT NULL,
    `Sartuday` tinyint(1) NOT NULL,
    `QuantidadeDia` int NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_agendaCalendarios_AspNetUsers_ApplicationUserId` FOREIGN KEY (`ApplicationUserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE INDEX `IX_agendaCalendarios_ApplicationUserId` ON `agendaCalendarios` (`ApplicationUserId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20220520012153_Add AgendaCalendario', '5.0.5');

COMMIT;

START TRANSACTION;

ALTER TABLE `agendaCalendarios` ADD `NomeAgenda` text NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20220523234900_NomeAgenda add', '5.0.5');

COMMIT;

START TRANSACTION;

ALTER TABLE `agendaCalendarios` MODIFY `HoraIncio` datetime NOT NULL DEFAULT '0001-01-01 00:00:00.000000';

ALTER TABLE `agendaCalendarios` MODIFY `HoraFim` datetime NOT NULL DEFAULT '0001-01-01 00:00:00.000000';

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20220531095233_Alterando variavel do camopo HoraInicio e Horafim para Detetime class AgendaCalendario', '5.0.5');

COMMIT;

