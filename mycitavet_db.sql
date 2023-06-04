CREATE DATABASE  IF NOT EXISTS `mycitavet_db` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `mycitavet_db`;
-- MySQL dump 10.13  Distrib 8.0.28, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: mycitavet_db
-- ------------------------------------------------------
-- Server version	5.7.39-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `citas`
--

DROP TABLE IF EXISTS `citas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `citas` (
  `IDCita` int(11) NOT NULL AUTO_INCREMENT,
  `IDMascota` int(11) DEFAULT NULL,
  `Correlativo_Cita` varchar(45) DEFAULT NULL,
  `Fecha` varchar(20) CHARACTER SET utf8 DEFAULT NULL,
  `Hora_Inicio` varchar(10) CHARACTER SET utf8 DEFAULT NULL,
  `Hora_Fin` varchar(10) DEFAULT NULL,
  `Motivo` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `Estado` varchar(15) DEFAULT NULL,
  PRIMARY KEY (`IDCita`),
  KEY `IDMascota` (`IDMascota`),
  CONSTRAINT `citas_ibfk_2` FOREIGN KEY (`IDMascota`) REFERENCES `mascotas` (`IDMascota`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `citas`
--

LOCK TABLES `citas` WRITE;
/*!40000 ALTER TABLE `citas` DISABLE KEYS */;
INSERT INTO `citas` VALUES (1,1,'C-1','30/05/2023','08:00','09:30','Corte de pelo y baño','AGENDADA'),(2,2,'C-2','30/05/2023','09:30','10:00','Consulta médica','AGENDADA'),(3,3,'C-3','30/05/2023','10:00','10:30','Consulta médica','AGENDADA'),(4,1,'CC-3','01/06/2023','08:30','10:00','Corte de pelo y baño','AGENDADA'),(5,1,'CC-243','01/06/2023','08:00','08:30','Consulta médica','AGENDADA'),(6,1,'cc-12','29/05/2023','09:30','10:30','Baño','AGENDADA'),(7,1,'c-12332','02/06/2023','09:00','09:30','Consulta médica','AGENDADA'),(8,1,'c-12344','30/05/2023','10:30','12:00','Corte de pelo y baño','AGENDADA'),(9,1,'CC12','31/05/2023','09:00','10:30','Corte de pelo y baño','AGENDADA');
/*!40000 ALTER TABLE `citas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `clasificaciones`
--

DROP TABLE IF EXISTS `clasificaciones`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `clasificaciones` (
  `IDClasificacion` int(11) NOT NULL AUTO_INCREMENT,
  `Clasificacion` varchar(25) CHARACTER SET utf8 NOT NULL,
  PRIMARY KEY (`IDClasificacion`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `clasificaciones`
--

LOCK TABLES `clasificaciones` WRITE;
/*!40000 ALTER TABLE `clasificaciones` DISABLE KEYS */;
INSERT INTO `clasificaciones` VALUES (1,'Empleados '),(2,'Usuarios');
/*!40000 ALTER TABLE `clasificaciones` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `clientes`
--

DROP TABLE IF EXISTS `clientes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `clientes` (
  `IDCliente` int(11) NOT NULL AUTO_INCREMENT,
  `Nombres` varchar(30) CHARACTER SET utf8 NOT NULL,
  `Apellidos` varchar(40) CHARACTER SET utf8 NOT NULL,
  `FechaNac` varchar(12) DEFAULT NULL,
  `Genero` enum('MASCULINO','FEMENINO') DEFAULT NULL,
  `IdMunicipio` int(11) DEFAULT NULL,
  `Direccion` varchar(100) CHARACTER SET utf8 NOT NULL,
  `TipoDoc` enum('DUI','PASAPORTE') DEFAULT NULL,
  `Documento` varchar(15) CHARACTER SET utf8 DEFAULT NULL,
  `Correo` varchar(75) CHARACTER SET utf8 DEFAULT NULL,
  `Telefono` varchar(12) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`IDCliente`),
  KEY `IdMunicipio` (`IdMunicipio`),
  CONSTRAINT `clientes_ibfk_1` FOREIGN KEY (`IdMunicipio`) REFERENCES `municipio` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `clientes`
--

LOCK TABLES `clientes` WRITE;
/*!40000 ALTER TABLE `clientes` DISABLE KEYS */;
INSERT INTO `clientes` VALUES (1,'Enrique Jose','Cardona Guardado','13/07/200','MASCULINO',16,'Col. San francisco n°33','DUI','06172712','enriquestbcardona@gmail.com','77159262'),(3,'SUJETO ','DE PRUEBA','13/07/2000','MASCULINO',3,'calle 123','DUI','12345678','PRUEBA@gmail.com','12345678'),(4,'Bartolomeo','Sanchez','13/07/2000','MASCULINO',16,'calle 7','DUI','1234567','Bartolomeo@gmail.com','12345678');
/*!40000 ALTER TABLE `clientes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `departamento`
--

DROP TABLE IF EXISTS `departamento`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `departamento` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(20) CHARACTER SET utf8 NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `departamento`
--

LOCK TABLES `departamento` WRITE;
/*!40000 ALTER TABLE `departamento` DISABLE KEYS */;
INSERT INTO `departamento` VALUES (1,'Sonsonate'),(2,'Ahuachapan'),(3,'Chalatenango'),(4,'Cuscatlan'),(5,'La Libertad'),(6,'La Paz'),(7,'La Union'),(8,'Morazan'),(9,'San Miguel'),(10,'San Salvador'),(11,'San Vicente'),(12,'Santa Ana'),(13,'Usulutan'),(14,'Cabañas');
/*!40000 ALTER TABLE `departamento` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `empleados`
--

DROP TABLE IF EXISTS `empleados`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `empleados` (
  `IDEmpleado` int(11) NOT NULL AUTO_INCREMENT,
  `Nombres` varchar(30) CHARACTER SET utf8 NOT NULL,
  `Apellidos` varchar(30) CHARACTER SET utf8 NOT NULL,
  `FechaNac` varchar(12) CHARACTER SET utf8 NOT NULL,
  `Genero` enum('MASCULINO','FEMENINO') DEFAULT NULL,
  `IdMunicipio` int(11) DEFAULT NULL,
  `Direccion` varchar(100) CHARACTER SET utf8 NOT NULL,
  `TipoDoc` enum('DUI','PASAPORTE') DEFAULT NULL,
  `Documento` varchar(15) CHARACTER SET utf8 DEFAULT NULL,
  `Correo` varchar(75) CHARACTER SET utf8 DEFAULT NULL,
  `Telefono` varchar(12) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`IDEmpleado`),
  KEY `IdMunicipio` (`IdMunicipio`),
  CONSTRAINT `empleados_ibfk_1` FOREIGN KEY (`IdMunicipio`) REFERENCES `municipio` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `empleados`
--

LOCK TABLES `empleados` WRITE;
/*!40000 ALTER TABLE `empleados` DISABLE KEYS */;
INSERT INTO `empleados` VALUES (1,'Enrique ','Cardona ','30/08/2000','MASCULINO',16,'casa #45','DUI','123456789','enrique@gmail.com','12345678'),(2,'Pedro ','Gutierrez ','02/03/2002','MASCULINO',16,'casa #20','DUI','123456789','pedro@gmail.com','12345678'),(3,'Carlos','García','24/03/2002','MASCULINO',16,'casa # 72','DUI','123456789','Carlos@gmail.com','12345678'),(4,'Nestor ','Cruz','16/10/2003','MASCULINO',10,'casa #10','DUI','123456789','nestor@gmail.com','12345678'),(6,'sujeto','prueba','29/06/2005','FEMENINO',21,'casa #9','PASAPORTE','12345678','prueba@gmail.com','12345678');
/*!40000 ALTER TABLE `empleados` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `especies`
--

DROP TABLE IF EXISTS `especies`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `especies` (
  `IDEspecie` int(11) NOT NULL AUTO_INCREMENT,
  `Especie` varchar(25) CHARACTER SET utf8 NOT NULL,
  PRIMARY KEY (`IDEspecie`),
  UNIQUE KEY `Especie` (`Especie`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `especies`
--

LOCK TABLES `especies` WRITE;
/*!40000 ALTER TABLE `especies` DISABLE KEYS */;
INSERT INTO `especies` VALUES (2,'gato'),(1,'perro');
/*!40000 ALTER TABLE `especies` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `historialclinico`
--

DROP TABLE IF EXISTS `historialclinico`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `historialclinico` (
  `IDHistorial` int(11) NOT NULL AUTO_INCREMENT,
  `IDMascota` int(11) NOT NULL,
  `Fecha` varchar(15) NOT NULL,
  `Diagnostico` varchar(100) CHARACTER SET utf8 NOT NULL,
  `Peso` varchar(50) CHARACTER SET utf8 NOT NULL,
  `ID_producto` int(11) DEFAULT NULL,
  PRIMARY KEY (`IDHistorial`),
  KEY `historialclinico_ibfk_3` (`IDMascota`),
  KEY `ID_producto` (`ID_producto`),
  CONSTRAINT `ID_producto` FOREIGN KEY (`ID_producto`) REFERENCES `productos` (`Id_producto`),
  CONSTRAINT `historialclinico_ibfk_3` FOREIGN KEY (`IDMascota`) REFERENCES `mascotas` (`IDMascota`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `historialclinico`
--

LOCK TABLES `historialclinico` WRITE;
/*!40000 ALTER TABLE `historialclinico` DISABLE KEYS */;
INSERT INTO `historialclinico` VALUES (1,1,'22/05/2023','sin pulgas','46 lbs',NULL),(3,1,'23/05/2023','bañadito','34 lbs',NULL),(4,2,'10/05/2023','Desparacitacion','40 lbs',NULL),(5,2,'18/05/2023','Vacunacion','41 lbs',NULL),(6,3,'20/05/2023','Vacunado anti pulgas','12 lbs',NULL);
/*!40000 ALTER TABLE `historialclinico` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `mascotas`
--

DROP TABLE IF EXISTS `mascotas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `mascotas` (
  `IDMascota` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(25) CHARACTER SET utf8 NOT NULL,
  `Genero` varchar(45) NOT NULL,
  `IDRaza` int(11) DEFAULT NULL,
  `Color` varchar(20) CHARACTER SET utf8 NOT NULL,
  `Rasgo` varchar(45) DEFAULT NULL,
  `FechaNac` varchar(12) NOT NULL,
  `IDCliente` int(11) NOT NULL,
  PRIMARY KEY (`IDMascota`),
  KEY `mascotas_ibfk_1` (`IDRaza`),
  KEY `mascotas_ibfk_2` (`IDCliente`),
  CONSTRAINT `mascotas_ibfk_1` FOREIGN KEY (`IDRaza`) REFERENCES `razas` (`IDRaza`),
  CONSTRAINT `mascotas_ibfk_2` FOREIGN KEY (`IDCliente`) REFERENCES `clientes` (`IDCliente`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `mascotas`
--

LOCK TABLES `mascotas` WRITE;
/*!40000 ALTER TABLE `mascotas` DISABLE KEYS */;
INSERT INTO `mascotas` VALUES (1,'robin','MACHO',1,'café','ojos amarillos ','16/05/2022',1),(2,'Lacho','MACHO',1,'blanco','Ojos azules','16/05/2008',1),(3,'Loco ','MACHO',3,'amarillo ','parches negros','16/05/2023',3);
/*!40000 ALTER TABLE `mascotas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `municipio`
--

DROP TABLE IF EXISTS `municipio`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `municipio` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(30) CHARACTER SET utf8 NOT NULL,
  `IdDepartamento` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IdDepartamento` (`IdDepartamento`),
  CONSTRAINT `municipio_ibfk_1` FOREIGN KEY (`IdDepartamento`) REFERENCES `departamento` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=262 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `municipio`
--

LOCK TABLES `municipio` WRITE;
/*!40000 ALTER TABLE `municipio` DISABLE KEYS */;
INSERT INTO `municipio` VALUES (1,'Acajutla',1),(2,'Armenia',1),(3,'Caluco',1),(4,'Cuisnahuat',1),(5,'Izalco',1),(6,'Juayua',1),(7,'Nahuizalco',1),(8,'Nahuilingo',1),(9,'Salcoatitan',1),(10,'San Antonio del Monte',1),(11,'San Julian',1),(12,'Santa Catarina Massahuat',1),(13,'Santa Isabel Ishuatan',1),(14,'Santo Domingo de Guazman',1),(15,'Sonsonate',1),(16,'Sonzacate',1),(17,'Ahuachapan',2),(18,'Apaneca',2),(19,'Atiquizaya',2),(20,'Concepcion de Ataco',2),(21,'El Refugio',2),(22,'Guaymango',2),(23,'Jujutla',2),(24,'San Francisco Menendez',2),(25,'San Lorenzo',2),(26,'San Pedro Puxtla',2),(27,'Tacuba',2),(28,'Turin',2),(29,'Agua Caliente',3),(30,'Arcatao',3),(31,'Azacualpa',3),(32,'Chalatenango',3),(33,'Comalapa',3),(34,'Citala',3),(35,'Concepcion Quezaltepeque',3),(36,'Dulce Nombre de Maria',3),(37,'El Carrizal',3),(38,'El Paraiso',3),(39,'La Laguna',3),(40,'La Palma',3),(41,'La Reina',3),(42,'Las Vueltas',3),(43,'Nueva Concepcion',3),(44,'Nueva Trinidad',3),(45,'Nombre de Jesus',3),(46,'Ojos de Agua',3),(47,'Potonico',3),(48,'San Antonio de la Cruz',3),(49,'San Antonio Los Ranchos',3),(50,'San Fernando',3),(51,'San Francisco Lempa',3),(52,'San Francisco Morazan',3),(53,'San Ignacio',3),(54,'San Isidro Labrador',3),(55,'San Jose Cancasque',3),(56,'San Jose las Flores',3),(57,'San Luis del Carmen',3),(58,'San Miguel de Mercedez',3),(59,'San Rafael',3),(60,'Santa Rita',3),(61,'Tejutla',3),(62,'Candelaria',4),(63,'Cojutepeque',4),(64,'El Carmen',4),(65,'El Rosario',4),(66,'Monte San Juan',4),(67,'Oratorio Con',4),(68,'San Bartolome pe',4),(69,'San Cristobal',4),(70,'San Jose Guayabal',4),(71,'San Pedro Perulapan',4),(72,'San Rafael Cedros',4),(73,'San Ramon',4),(74,'Santa Cruz Analquito',4),(75,'Santa Cruz Michapa',4),(76,'SUchitoto',4),(77,'Tenancingo',4),(78,'Antiguo Cuacatlan',5),(79,'Chiltiupan',5),(80,'Ciudad Arce',5),(81,'Color',5),(82,'Comasagua',5),(83,'Huizucar',5),(84,'Jayaque',5),(85,'Jicalapa',5),(86,'La Libertad',5),(87,'Santa Tecla',5),(88,'Nuevo Cuscatlan',5),(89,'San Juan Opico',5),(90,'Quetzaltepeque',5),(91,'Sacacoyo',5),(92,'San Jose Villanueva',5),(93,'San Matias',5),(94,'San Pablo Tacachico',5),(95,'Talnique',5),(96,'Tamanique',5),(97,'Teotepeque',5),(98,'Tepecoyo',5),(99,'Zaragoza',5),(100,'Cuyutitan',6),(101,'El Rosario',6),(102,'Jerusalen',6),(103,'Mercedes La Ceiba',6),(104,'Olocuilta',6),(105,'Paraiso de Osorio',6),(106,'San Antonio Masahuat',6),(107,'San Emigdio',6),(108,'San Francisco Chinameca',6),(109,'San Juan Nonualco',6),(110,'San Juan Talpa',6),(111,'San Juan Tepezontes',6),(112,'San Luis Talpa',6),(113,'San Luis La Herradura',6),(114,'San Miguel Tepezontes',6),(115,'San Pedro Masahuat',6),(116,'San Pedro Nonualco',6),(117,'San Rafael Obrajuelo',6),(118,'Santa Marta Ostuma',6),(119,'Santiago Nonualco',6),(120,'Tapahualca',6),(121,'Zacatecoluca',6),(122,'Anamoros',7),(123,'Bolivar',7),(124,'Con de Orinte',7),(125,'El Carmen',7),(126,'El Sauce',7),(127,'Intipuca',7),(128,'La Union',7),(129,'Lislique',7),(130,'Meanguera Golfo',7),(131,'Nueva Esparta',7),(132,'Pasaquina',7),(133,'Poloros',7),(134,'San Alejo',7),(135,'San Jose',7),(136,'Santa Rosa del Lima',7),(137,'Yayantique',7),(138,'Yucuaiquin',7),(139,'Arambala',8),(140,'Cacaopera',8),(141,'Chilangaa',8),(142,'Corinto',8),(143,'Delicias de Concepcion',8),(144,'El Divisadero',8),(145,'El Rosario',8),(146,'Gualococti',8),(147,'Guatajiagua',8),(148,'Joateca',8),(149,'Jocoaitique',8),(150,'Jocoro',8),(151,'Lolotiquillo',8),(152,'Meanguerra',8),(153,'Osicala',8),(154,'Perquin',8),(155,'San  Calos',8),(156,'San Fernando',8),(157,'San Francisco Gotera',8),(158,'San Isidro',8),(159,'San Simon',8),(160,'Sensembra',8),(161,'Sociedad',8),(162,'Torola',8),(163,'Yamabal',8),(164,'Yoloalquin',8),(165,'Carolina',9),(166,'Chapeltique',9),(167,'Chinameca',9),(168,'Chirilagua',9),(169,'Ciudad Barrios',9),(170,'Comacaran',9),(171,'El Transito',9),(172,'Lolotique',9),(173,'Moncagua',9),(174,'Nueva Guadalupe',9),(175,'Nueva Eden de san Julian',9),(176,'Quelepa',9),(177,'San Antonio del Mosco',9),(178,'San Gerardo',9),(179,'San Jorgue',9),(180,'Sn Luis de la Reina',9),(181,'San Miguel',9),(182,'San Rafel Oriente',9),(183,'Sesori',9),(184,'Uluazapa',9),(185,'Aguilares',10),(186,'Apopa',10),(187,'Ayustuxtepeque',10),(188,'Cuscatanzingo',10),(189,'Ciudad Delgado',10),(190,'El Paisnal',10),(191,'Guazapa',10),(192,'Ilipango',10),(193,'Mexicanos',10),(194,'Nejapa',10),(195,'Panchimalco',10),(196,'Rosario de Mora',10),(197,'San Marcos',10),(198,'San Martin',10),(199,'San Salvador',10),(200,'Santiago Texacuangos',10),(201,'Santo Tomas',10),(202,'Soyapango',10),(203,'Tonacatepeque',10),(204,'Apastepeque',11),(205,'Guadalupe',11),(206,'San Cayetano Istepeque',11),(207,'San Esteban Catarina',11),(208,'San Ildefonso',11),(209,'San Lorenzo',11),(210,'San Sebastian',11),(211,'San Vicente',11),(212,'Santa Clara',11),(213,'Santo Domingo',11),(214,'Tecoluca',11),(215,'Tepetitan',11),(216,'Verapaz',11),(217,'Candelaria de la Frontera',12),(218,'Chalchuapa',12),(219,'Coatepeque',12),(220,'El Congo',12),(221,'El Provenir',12),(222,'Masahuat',12),(223,'Metapan',12),(224,'San Antonio Pajonal',12),(225,'San Sebastian Salitrillo',12),(226,'Santa Ana',12),(227,'Santa Rosa Guachipilin',12),(228,'Santiago de la Frontera',12),(229,'Texistepeque',12),(230,'Alegria',13),(231,'Berlin',13),(232,'California',13),(233,'Concepcion Batres',13),(234,'El Triunfo',13),(235,'Ereguayquil',13),(236,'Estanzuelas',13),(237,'Jiquilizco',13),(238,'Juacuapa',13),(239,'Jucuaran',13),(240,'Mercedes Umaña',13),(241,'Nueva Granada',13),(242,'Ozatlan',13),(243,'Puerto El Triunfo',13),(244,'San Agustin',13),(245,'San Buena Aventura',13),(246,'San Dionisio',13),(247,'San Francisco Javier',13),(248,'Santa Elena',13),(249,'Santa Maria',13),(250,'Santiago de Maria',13),(251,'Tecapan',13),(252,'Usulutan',13),(253,'Cinquera',14),(254,'Dolores',14),(255,'Guacotecti',14),(256,'Ilobasco',14),(257,'Jutiapa',14),(258,'San Isidro',14),(259,'Sensuntepeque',14),(260,'Tejutepeque',14),(261,'Victoria',14);
/*!40000 ALTER TABLE `municipio` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `opciones`
--

DROP TABLE IF EXISTS `opciones`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `opciones` (
  `IDOpcion` int(11) NOT NULL AUTO_INCREMENT,
  `Opcion` varchar(25) CHARACTER SET utf8 NOT NULL,
  `IDClasificacion` int(11) DEFAULT NULL,
  PRIMARY KEY (`IDOpcion`),
  KEY `IDClasificacion` (`IDClasificacion`),
  CONSTRAINT `opciones_ibfk_1` FOREIGN KEY (`IDClasificacion`) REFERENCES `clasificaciones` (`IDClasificacion`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `opciones`
--

LOCK TABLES `opciones` WRITE;
/*!40000 ALTER TABLE `opciones` DISABLE KEYS */;
INSERT INTO `opciones` VALUES (1,'Ver ',1),(2,'Insertar',1),(3,'Editar',1),(4,'Iniciar Sesion ',1),(5,'Eliminar ',1),(6,'Ver',2),(7,'Insertar',2),(8,'Editar',2),(9,'Iniciar Sesión',2),(10,'Eliminar',2);
/*!40000 ALTER TABLE `opciones` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `permisos`
--

DROP TABLE IF EXISTS `permisos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `permisos` (
  `IDPermiso` int(11) NOT NULL AUTO_INCREMENT,
  `IDRol` int(11) DEFAULT NULL,
  `IDOpcion` int(11) DEFAULT NULL,
  PRIMARY KEY (`IDPermiso`),
  KEY `IDRol` (`IDRol`),
  KEY `IDOpcion` (`IDOpcion`),
  CONSTRAINT `permisos_ibfk_1` FOREIGN KEY (`IDRol`) REFERENCES `roles` (`IDRol`),
  CONSTRAINT `permisos_ibfk_2` FOREIGN KEY (`IDOpcion`) REFERENCES `opciones` (`IDOpcion`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `permisos`
--

LOCK TABLES `permisos` WRITE;
/*!40000 ALTER TABLE `permisos` DISABLE KEYS */;
INSERT INTO `permisos` VALUES (1,1,6),(2,1,7),(3,1,8),(4,1,9),(5,1,10);
/*!40000 ALTER TABLE `permisos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `productos`
--

DROP TABLE IF EXISTS `productos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `productos` (
  `Id_producto` int(11) NOT NULL AUTO_INCREMENT,
  `TipoProducto` varchar(25) NOT NULL,
  `Nombre` varchar(25) NOT NULL,
  `Marca` varchar(25) DEFAULT NULL,
  `Descripcion` varchar(150) DEFAULT NULL,
  `Stock` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id_producto`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `productos`
--

LOCK TABLES `productos` WRITE;
/*!40000 ALTER TABLE `productos` DISABLE KEYS */;
INSERT INTO `productos` VALUES (2,'VACUNA','AntiRabico','Nobivac','Vacuna de virus inactivado contra la rabia en caninos, felinos y hurones',15);
/*!40000 ALTER TABLE `productos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `razas`
--

DROP TABLE IF EXISTS `razas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `razas` (
  `IDRaza` int(11) NOT NULL AUTO_INCREMENT,
  `Raza` varchar(25) CHARACTER SET utf8 NOT NULL,
  `IDEspecie` int(11) NOT NULL,
  PRIMARY KEY (`IDRaza`),
  UNIQUE KEY `Raza` (`Raza`),
  KEY `IDEspecie` (`IDEspecie`),
  CONSTRAINT `razas_ibfk_1` FOREIGN KEY (`IDEspecie`) REFERENCES `especies` (`IDEspecie`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `razas`
--

LOCK TABLES `razas` WRITE;
/*!40000 ALTER TABLE `razas` DISABLE KEYS */;
INSERT INTO `razas` VALUES (1,'pastor aleman ',1),(3,'angora',2),(4,'chao chao',1);
/*!40000 ALTER TABLE `razas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `roles`
--

DROP TABLE IF EXISTS `roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `roles` (
  `IDRol` int(11) NOT NULL AUTO_INCREMENT,
  `Rol` char(15) CHARACTER SET utf8 NOT NULL,
  PRIMARY KEY (`IDRol`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `roles`
--

LOCK TABLES `roles` WRITE;
/*!40000 ALTER TABLE `roles` DISABLE KEYS */;
INSERT INTO `roles` VALUES (1,'Administrador'),(2,'Configurador');
/*!40000 ALTER TABLE `roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuarios`
--

DROP TABLE IF EXISTS `usuarios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usuarios` (
  `IDUsuario` int(11) NOT NULL AUTO_INCREMENT,
  `Usuario` varchar(30) CHARACTER SET utf8 NOT NULL,
  `Clave` varchar(250) CHARACTER SET utf8 NOT NULL,
  `IDEmpleado` int(11) DEFAULT NULL,
  `IDRol` int(11) DEFAULT NULL,
  PRIMARY KEY (`IDUsuario`),
  KEY `IDEmpleado` (`IDEmpleado`),
  KEY `IDRol` (`IDRol`),
  CONSTRAINT `usuarios_ibfk_1` FOREIGN KEY (`IDEmpleado`) REFERENCES `empleados` (`IDEmpleado`),
  CONSTRAINT `usuarios_ibfk_2` FOREIGN KEY (`IDRol`) REFERENCES `roles` (`IDRol`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuarios`
--

LOCK TABLES `usuarios` WRITE;
/*!40000 ALTER TABLE `usuarios` DISABLE KEYS */;
INSERT INTO `usuarios` VALUES (1,'VETUSER1-C','123',1,1),(2,'VETUSER2-G','123',2,2),(3,'VETUSER3-G','123',3,2),(4,'VETUSER4-C','123',4,2);
/*!40000 ALTER TABLE `usuarios` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-06-03 22:16:23
