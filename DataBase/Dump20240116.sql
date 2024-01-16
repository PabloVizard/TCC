-- MySQL dump 10.13  Distrib 8.0.33, for Win64 (x86_64)
--
-- Host: localhost    Database: database
-- ------------------------------------------------------
-- Server version	8.0.33

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
-- Table structure for table `bancas`
--

DROP TABLE IF EXISTS `bancas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `bancas` (
  `id` int NOT NULL AUTO_INCREMENT,
  `professorOrientador` varchar(255) NOT NULL,
  `nomeAlunoOrientado` varchar(255) NOT NULL,
  `idAlunoOrientado` int DEFAULT NULL,
  `nomeProjeto` varchar(255) NOT NULL,
  `avaliador01` varchar(255) NOT NULL,
  `avaliador02` varchar(255) NOT NULL,
  `ano` int NOT NULL,
  `semestre` int NOT NULL,
  `bancaConfirmada` tinyint NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `bancas`
--

LOCK TABLES `bancas` WRITE;
/*!40000 ALTER TABLE `bancas` DISABLE KEYS */;
INSERT INTO `bancas` VALUES (1,'Marcus','Pablo Ferreira',3,'POC gestão','Daniel','Glaucia',2023,2,0);
/*!40000 ALTER TABLE `bancas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `compromissos`
--

DROP TABLE IF EXISTS `compromissos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `compromissos` (
  `id` int NOT NULL AUTO_INCREMENT,
  `tipoCompromisso` int NOT NULL,
  `descricao` varchar(255) NOT NULL,
  `idProfessorOrientador` int NOT NULL,
  `idAlunoOrientado` int DEFAULT NULL,
  `idTurma` int DEFAULT NULL,
  `dataCompromisso` datetime NOT NULL,
  `local` varchar(255) DEFAULT NULL,
  `link` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `compromissos`
--

LOCK TABLES `compromissos` WRITE;
/*!40000 ALTER TABLE `compromissos` DISABLE KEYS */;
INSERT INTO `compromissos` VALUES (1,1,'Aula de POC 01',6,3,1,'2023-11-10 04:18:54','Online','https://meet.google.com/');
/*!40000 ALTER TABLE `compromissos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `orientacoes`
--

DROP TABLE IF EXISTS `orientacoes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `orientacoes` (
  `id` int NOT NULL AUTO_INCREMENT,
  `idAlunoOrientado` int unsigned NOT NULL,
  `idProfessorOrientador` int NOT NULL,
  `idProjeto` int NOT NULL,
  `idTurma` int NOT NULL,
  `StatusAprovacao` int NOT NULL,
  `anexoResumoTrabalho` varchar(255) DEFAULT NULL,
  `localDivulgacao` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `idAlunoOrientado_UNIQUE` (`idAlunoOrientado`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orientacoes`
--

LOCK TABLES `orientacoes` WRITE;
/*!40000 ALTER TABLE `orientacoes` DISABLE KEYS */;
INSERT INTO `orientacoes` VALUES (3,3,6,1,1,1,NULL,NULL),(4,7,6,5,1,1,NULL,NULL),(5,8,6,6,1,1,NULL,NULL);
/*!40000 ALTER TABLE `orientacoes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `preregistro`
--

DROP TABLE IF EXISTS `preregistro`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `preregistro` (
  `id` int NOT NULL AUTO_INCREMENT,
  `cpf` varchar(14) NOT NULL,
  `tipoUsuario` int NOT NULL,
  `cadastrado` bit(1) NOT NULL,
  `idTurma` int NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `cpf_UNIQUE` (`cpf`)
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `preregistro`
--

LOCK TABLES `preregistro` WRITE;
/*!40000 ALTER TABLE `preregistro` DISABLE KEYS */;
INSERT INTO `preregistro` VALUES (1,'13738231609',1,_binary '',2),(28,'08499617697',2,_binary '',2);
/*!40000 ALTER TABLE `preregistro` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `projetos`
--

DROP TABLE IF EXISTS `projetos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `projetos` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nome` varchar(255) NOT NULL,
  `descricao` varchar(5000) NOT NULL,
  `idProfessorResponsavel` int NOT NULL,
  `idAlunoResponsavel` int DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `projetos`
--

LOCK TABLES `projetos` WRITE;
/*!40000 ALTER TABLE `projetos` DISABLE KEYS */;
INSERT INTO `projetos` VALUES (1,'Projeto TCC 01','Descrição Projeto TCC 01',6,3),(2,'Projeto TCC 02','Descrição Projeto TCC 02',6,NULL),(3,'Projeto TCC 03','Descrição TCC 03',6,7),(6,'Projeto TCC 05','Projeto TCC 05',6,8);
/*!40000 ALTER TABLE `projetos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tarefaaluno`
--

DROP TABLE IF EXISTS `tarefaaluno`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tarefaaluno` (
  `id` int NOT NULL AUTO_INCREMENT,
  `idAluno` int NOT NULL,
  `idTarefa` int NOT NULL,
  `anexo` varchar(255) NOT NULL,
  `dataEntrega` datetime NOT NULL,
  `dataLimite` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tarefaaluno`
--

LOCK TABLES `tarefaaluno` WRITE;
/*!40000 ALTER TABLE `tarefaaluno` DISABLE KEYS */;
INSERT INTO `tarefaaluno` VALUES (1,3,1,'https://drive.google.com/file/d/1wlpb1mP_Q_xCoFDtIf2ZRU_x-go6WfR0/view?usp=sharing','2023-11-06 14:30:00','2023-11-07 14:30:00'),(5,3,2,'www.google.com.br','2023-11-10 05:55:04','2023-11-09 04:18:12');
/*!40000 ALTER TABLE `tarefaaluno` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tarefas`
--

DROP TABLE IF EXISTS `tarefas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tarefas` (
  `id` int NOT NULL AUTO_INCREMENT,
  `descricao` varchar(255) NOT NULL,
  `idTurma` int NOT NULL,
  `dataLimite` datetime NOT NULL,
  `anexo` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tarefas`
--

LOCK TABLES `tarefas` WRITE;
/*!40000 ALTER TABLE `tarefas` DISABLE KEYS */;
INSERT INTO `tarefas` VALUES (1,'Tarefa01',1,'2023-11-09 02:10:42','https://drive.google.com/file/d/1wlpb1mP_Q_xCoFDtIf2ZRU_x-go6WfR0/view?usp=sharing'),(2,'Tarefa02',1,'2023-11-09 04:18:12','https://drive.google.com/file/d/1wlpb1mP_Q_xCoFDtIf2ZRU_x-go6WfR0/view?usp=sharing'),(3,'Teste 03',2,'2023-11-09 19:44:36','google.com.br');
/*!40000 ALTER TABLE `tarefas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `turmas`
--

DROP TABLE IF EXISTS `turmas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `turmas` (
  `id` int NOT NULL AUTO_INCREMENT,
  `descricao` varchar(255) DEFAULT NULL,
  `ano` int NOT NULL,
  `semestre` int NOT NULL,
  `nPoc` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `turmas`
--

LOCK TABLES `turmas` WRITE;
/*!40000 ALTER TABLE `turmas` DISABLE KEYS */;
INSERT INTO `turmas` VALUES (1,'Poc 1 - 2023/1',2023,1,1),(2,'Poc 2 - 2023/1',2023,1,2);
/*!40000 ALTER TABLE `turmas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuarios`
--

DROP TABLE IF EXISTS `usuarios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usuarios` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nomeCompleto` varchar(100) NOT NULL,
  `cpf` varchar(14) NOT NULL,
  `email` varchar(50) NOT NULL,
  `senha` varchar(64) NOT NULL,
  `tipoUsuario` int NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `email_UNIQUE` (`email`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuarios`
--

LOCK TABLES `usuarios` WRITE;
/*!40000 ALTER TABLE `usuarios` DISABLE KEYS */;
INSERT INTO `usuarios` VALUES (3,'Pablo Ferreira F','13738231609','pabloferreirajc@gmail.com','85cf487bb501b1a29e2d67c36cf475185041b4bc22dc5dffb94541c11ab48067',1),(6,'Professor Teste','08499617697','teste@gmail.com','85cf487bb501b1a29e2d67c36cf475185041b4bc22dc5dffb94541c11ab48067',4),(7,'Aluno teste01','08435412478','aluno01@gmail.com','85cf487bb501b1a29e2d67c36cf475185041b4bc22dc5dffb94541c11ab48067',1),(8,'Aluno teste02','08435412678','aluno02@gmail.com','85cf487bb501b1a29e2d67c36cf475185041b4bc22dc5dffb94541c11ab48067',1),(9,'Aluno teste03','08435712478','aluno03@gmail.com','85cf487bb501b1a29e2d67c36cf475185041b4bc22dc5dffb94541c11ab48067',1);
/*!40000 ALTER TABLE `usuarios` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuarioturma`
--

DROP TABLE IF EXISTS `usuarioturma`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usuarioturma` (
  `id` int NOT NULL AUTO_INCREMENT,
  `idUsuario` int NOT NULL,
  `idTurma` int NOT NULL,
  `tipoUsuario` int NOT NULL,
  `nomeCompleto` varchar(150) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuarioturma`
--

LOCK TABLES `usuarioturma` WRITE;
/*!40000 ALTER TABLE `usuarioturma` DISABLE KEYS */;
INSERT INTO `usuarioturma` VALUES (1,3,1,1,''),(2,6,2,3,''),(3,7,1,1,''),(4,8,1,1,''),(5,9,1,1,'');
/*!40000 ALTER TABLE `usuarioturma` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-01-16 20:04:12
