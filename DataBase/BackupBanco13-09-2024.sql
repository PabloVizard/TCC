-- MySQL dump 10.13  Distrib 8.0.34, for Win64 (x86_64)
--
-- Host: localhost    Database: database
-- ------------------------------------------------------
-- Server version	8.0.35

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
-- Table structure for table `aulas`
--

DROP TABLE IF EXISTS `aulas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aulas` (
  `id` int NOT NULL AUTO_INCREMENT,
  `descricao` varchar(255) NOT NULL,
  `idProfessor` int NOT NULL,
  `idTurma` int NOT NULL,
  `dataAula` datetime NOT NULL,
  `local` varchar(255) DEFAULT NULL,
  `link` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_turmaAula` (`idTurma`),
  KEY `fk_ProfessorAula` (`idProfessor`),
  CONSTRAINT `fk_ProfessorAula` FOREIGN KEY (`idProfessor`) REFERENCES `usuarios` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_turmaAula` FOREIGN KEY (`idTurma`) REFERENCES `turmas` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aulas`
--

LOCK TABLES `aulas` WRITE;
/*!40000 ALTER TABLE `aulas` DISABLE KEYS */;
INSERT INTO `aulas` VALUES (8,'Aula 01',16,13,'2024-08-21 13:00:00','LEN 01',''),(9,'Aula 02',18,14,'2024-08-21 13:00:00','','https://meet.google.com/');
/*!40000 ALTER TABLE `aulas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `bancas`
--

DROP TABLE IF EXISTS `bancas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `bancas` (
  `id` int NOT NULL AUTO_INCREMENT,
  `numeroDefesa` int DEFAULT NULL,
  `idProjeto` int NOT NULL,
  `idProfessorOrientador` int NOT NULL,
  `idAlunoOrientado` int NOT NULL,
  `idAvaliador01` int NOT NULL,
  `idAvaliador02` int DEFAULT NULL,
  `ano` int NOT NULL,
  `semestre` int NOT NULL,
  `bancaConfirmada` tinyint NOT NULL,
  `dataDefesa` datetime DEFAULT NULL,
  `status` tinyint DEFAULT NULL,
  `localDefesa` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `numeroDefesa_UNIQUE` (`numeroDefesa`),
  KEY `fk_professorBanca` (`idProfessorOrientador`),
  KEY `fk_alunoBanca` (`idAlunoOrientado`),
  KEY `fk_projetoBanca` (`idProjeto`),
  KEY `fk_avaliador01Banca` (`idAvaliador01`),
  KEY `fk_avaliador02Banca` (`idAvaliador02`),
  CONSTRAINT `fk_alunoBanca` FOREIGN KEY (`idAlunoOrientado`) REFERENCES `usuarios` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_avaliador01Banca` FOREIGN KEY (`idAvaliador01`) REFERENCES `usuarios` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_avaliador02Banca` FOREIGN KEY (`idAvaliador02`) REFERENCES `usuarios` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_professorBanca` FOREIGN KEY (`idProfessorOrientador`) REFERENCES `usuarios` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_projetoBanca` FOREIGN KEY (`idProjeto`) REFERENCES `projetos` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `bancas`
--

LOCK TABLES `bancas` WRITE;
/*!40000 ALTER TABLE `bancas` DISABLE KEYS */;
INSERT INTO `bancas` VALUES (9,1,19,21,75,23,NULL,2024,1,1,'2024-08-18 23:00:00',1,'sala 20 do LEN'),(10,3,20,21,77,16,NULL,2024,1,1,'2024-08-20 21:00:00',NULL,'Laboratório de Informática I do LEN'),(11,2,21,21,78,18,NULL,2024,1,1,'2024-08-20 21:00:00',NULL,'sala virtual https://meet.google.com/psx-odya-gjn');
/*!40000 ALTER TABLE `bancas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `faltas`
--

DROP TABLE IF EXISTS `faltas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `faltas` (
  `id` int NOT NULL AUTO_INCREMENT,
  `idTurma` int NOT NULL,
  `idAluno` int NOT NULL,
  `idAula` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_alunofalta` (`idAluno`),
  KEY `fk_turmaFalta` (`idTurma`),
  KEY `fk_aulaFalta` (`idAula`),
  CONSTRAINT `fk_alunofalta` FOREIGN KEY (`idAluno`) REFERENCES `usuarios` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_aula` FOREIGN KEY (`idAula`) REFERENCES `aulas` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_aulaFalta` FOREIGN KEY (`idAula`) REFERENCES `aulas` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_turmaFalta` FOREIGN KEY (`idTurma`) REFERENCES `turmas` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `faltas`
--

LOCK TABLES `faltas` WRITE;
/*!40000 ALTER TABLE `faltas` DISABLE KEYS */;
INSERT INTO `faltas` VALUES (10,13,76,8);
/*!40000 ALTER TABLE `faltas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `orientacoes`
--

DROP TABLE IF EXISTS `orientacoes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `orientacoes` (
  `id` int NOT NULL AUTO_INCREMENT,
  `idAlunoOrientado` int NOT NULL,
  `idProfessorOrientador` int NOT NULL,
  `idProjeto` int NOT NULL,
  `idTurma` int NOT NULL,
  `StatusAprovacao` int NOT NULL,
  `anexoResumoTrabalho` varchar(255) DEFAULT NULL,
  `anexoTAO` varchar(255) DEFAULT NULL,
  `localDivulgacao` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `idAlunoOrientado_UNIQUE` (`idAlunoOrientado`),
  KEY `fk_projeto` (`idProjeto`),
  KEY `fk_professorOrientacoes` (`idProfessorOrientador`),
  CONSTRAINT `fk_alunoOrientacoes` FOREIGN KEY (`idAlunoOrientado`) REFERENCES `usuarios` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_professorOrientacoes` FOREIGN KEY (`idProfessorOrientador`) REFERENCES `usuarios` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_projeto` FOREIGN KEY (`idProjeto`) REFERENCES `projetos` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orientacoes`
--

LOCK TABLES `orientacoes` WRITE;
/*!40000 ALTER TABLE `orientacoes` DISABLE KEYS */;
INSERT INTO `orientacoes` VALUES (16,76,21,18,13,1,NULL,NULL,NULL),(17,75,21,19,14,2,'',NULL,''),(18,77,21,20,14,2,'',NULL,''),(19,78,21,21,14,2,'',NULL,'');
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
  `matricula` varchar(10) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `cpf_UNIQUE` (`cpf`)
) ENGINE=InnoDB AUTO_INCREMENT=32 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `preregistro`
--

LOCK TABLES `preregistro` WRITE;
/*!40000 ALTER TABLE `preregistro` DISABLE KEYS */;
INSERT INTO `preregistro` VALUES (30,'13738231609',1,_binary '',11,'3480'),(31,'01877099635',1,_binary '',14,'3464');
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
  `area` varchar(255) NOT NULL,
  `idProfessorResponsavel` int NOT NULL,
  `idAlunoResponsavel` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `idAlunoResponsavel_UNIQUE` (`idAlunoResponsavel`),
  KEY `fk_professorProjeto` (`idProfessorResponsavel`),
  CONSTRAINT `fk_alunoProjeto` FOREIGN KEY (`idAlunoResponsavel`) REFERENCES `usuarios` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_professorProjeto` FOREIGN KEY (`idProfessorResponsavel`) REFERENCES `usuarios` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `projetos`
--

LOCK TABLES `projetos` WRITE;
/*!40000 ALTER TABLE `projetos` DISABLE KEYS */;
INSERT INTO `projetos` VALUES (18,'Predição do Peso de Bovinos utilizando Aprendizado de Máquina','Análise','Agronomia e IA',21,76),(19,'Gerenciador de POCS','Desenvolvimento','Desenvolvimento',21,75),(20,'Projeto Teste','teste','Área de Testes',21,77),(21,'Projeto de Teste 02','teste','Área de teste 02',21,78),(22,'Teste','teste','teste',24,NULL),(23,'Teste 2','teste','Teste 2',24,NULL);
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
  `anexo` varchar(255) DEFAULT NULL,
  `dataEntrega` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_tarefa` (`idTarefa`),
  KEY `fk_alunoTarefa` (`idAluno`),
  CONSTRAINT `fk_alunoTarefa` FOREIGN KEY (`idAluno`) REFERENCES `usuarios` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_tarefa` FOREIGN KEY (`idTarefa`) REFERENCES `tarefas` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tarefaaluno`
--

LOCK TABLES `tarefaaluno` WRITE;
/*!40000 ALTER TABLE `tarefaaluno` DISABLE KEYS */;
INSERT INTO `tarefaaluno` VALUES (18,76,15,NULL,NULL);
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
  `idAluno` int NOT NULL,
  `idProfessor` int NOT NULL,
  `dataLimite` datetime NOT NULL,
  `anexo` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_aluno` (`idAluno`),
  KEY `fk_professor` (`idProfessor`),
  CONSTRAINT `fk_aluno` FOREIGN KEY (`idAluno`) REFERENCES `usuarios` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_professor` FOREIGN KEY (`idProfessor`) REFERENCES `usuarios` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tarefas`
--

LOCK TABLES `tarefas` WRITE;
/*!40000 ALTER TABLE `tarefas` DISABLE KEYS */;
INSERT INTO `tarefas` VALUES (15,'Entrega TAO',76,21,'2024-08-23 13:00:00','');
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
  `ativo` bit(1) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `turmas`
--

LOCK TABLES `turmas` WRITE;
/*!40000 ALTER TABLE `turmas` DISABLE KEYS */;
INSERT INTO `turmas` VALUES (13,'POC 1 - 2024/2',2024,1,1,_binary ''),(14,'POC 2 - 2024/1',2024,1,2,_binary '');
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
  `matricula` varchar(10) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `email_UNIQUE` (`email`),
  UNIQUE KEY `matricula_UNIQUE` (`matricula`),
  UNIQUE KEY `cpf_UNIQUE` (`cpf`)
) ENGINE=InnoDB AUTO_INCREMENT=79 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuarios`
--

LOCK TABLES `usuarios` WRITE;
/*!40000 ALTER TABLE `usuarios` DISABLE KEYS */;
INSERT INTO `usuarios` VALUES (16,'Daniel Mendes Barbosa','00000000001','danielmendes@ufv.br','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92',2,'11850-8'),(18,'Fabrício Aguiar Silva','00000000002','fabricio.asilva@ufv.br ','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92',4,'10206-7'),(19,'Antonio Carlos Fava de Barros','00000000003','acfava@ufv.br','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92',3,'7682-1'),(20,'Gláucia Braga e Silva','00000000004','glaucia@ufv.br','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92',4,'11717-X'),(21,'José Augusto Miranda Nacif','00000000005','jnacif@ufv.br','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92',3,'10116-8'),(22,'Maria Amélia Lopes Silva','00000000006','mamelia@ufv.br','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92',3,'8717-3'),(23,'Thais Regina de Moura Braga Silva','00000000007','thais.braga@ufv.br','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92',4,'10003-X'),(24,'Marcus Henrique Soares Mendes','00000000008','marcus.mendes@ufv.br','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92',5,'8436-0'),(75,'Pablo Ferreira','13738231609','pablo.ferreira@ufv.br','85cf487bb501b1a29e2d67c36cf475185041b4bc22dc5dffb94541c11ab48067',1,'3480'),(76,'Roniel Nunes Barbosa','01877099635','roniel.barbosa@ufv.br','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92',1,'3464'),(77,'Samuel Pedro Sena','13452464899','sm@ufv.com.br','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92',1,'3500'),(78,'Saulo Miranda Silva','54235484655','ss@ufv.com.br','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92',1,'3499');
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
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=59 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuarioturma`
--

LOCK TABLES `usuarioturma` WRITE;
/*!40000 ALTER TABLE `usuarioturma` DISABLE KEYS */;
INSERT INTO `usuarioturma` VALUES (55,75,14),(56,76,13),(57,77,14),(58,78,14);
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

-- Dump completed on 2024-09-13  0:10:14
