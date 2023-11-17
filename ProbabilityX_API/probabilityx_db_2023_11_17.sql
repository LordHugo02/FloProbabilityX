-- --------------------------------------------------------
-- Hôte:                         127.0.0.1
-- Version du serveur:           11.3.0-MariaDB - mariadb.org binary distribution
-- SE du serveur:                Win64
-- HeidiSQL Version:             12.5.0.6677
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Listage de la structure de la base pour probabiltyx_db
CREATE DATABASE IF NOT EXISTS `probabiltyx_db` /*!40100 DEFAULT CHARACTER SET armscii8 COLLATE armscii8_bin */;
USE `probabiltyx_db`;

-- Listage de la structure de la table probabiltyx_db. ban
CREATE TABLE IF NOT EXISTS `ban` (
  `id_ban` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `id_user` int(11) unsigned NOT NULL,
  `is_banned` tinyint(1) unsigned NOT NULL,
  `ban_to` date NOT NULL,
  `ban_at` date NOT NULL,
  `ban_number` int(11) unsigned NOT NULL,
  PRIMARY KEY (`id_ban`),
  KEY `fk_ban_user` (`id_user`),
  CONSTRAINT `fk_ban_user` FOREIGN KEY (`id_user`) REFERENCES `users` (`id_user`)
) ENGINE=InnoDB DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

-- Listage des données de la table probabiltyx_db.ban : ~0 rows (environ)

-- Listage de la structure de la table probabiltyx_db. companies
CREATE TABLE IF NOT EXISTS `companies` (
  `id_company` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `company_name` varchar(255) NOT NULL,
  `stock_symbol` varchar(10) NOT NULL,
  `id_stock_type` int(11) unsigned NOT NULL,
  PRIMARY KEY (`id_company`),
  KEY `fk_companies_stock_type` (`id_stock_type`),
  CONSTRAINT `fk_companies_favorite_company` FOREIGN KEY (`id_company`) REFERENCES `favorites_company` (`id_company`),
  CONSTRAINT `fk_companies_stock_type` FOREIGN KEY (`id_stock_type`) REFERENCES `stock_types` (`id_stock_type`)
) ENGINE=InnoDB DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

-- Listage des données de la table probabiltyx_db.companies : ~0 rows (environ)

-- Listage de la structure de la table probabiltyx_db. day_stock_prices
CREATE TABLE IF NOT EXISTS `day_stock_prices` (
  `id_day_stock` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `id_company` int(11) unsigned NOT NULL,
  `day_date` timestamp NOT NULL,
  `open_price` decimal(10,2) unsigned NOT NULL,
  `close_price` decimal(10,2) unsigned NOT NULL,
  `high_price` decimal(10,2) unsigned NOT NULL,
  `low_price` decimal(10,2) unsigned NOT NULL,
  PRIMARY KEY (`id_day_stock`),
  KEY `fk_day_stock_prices_company` (`id_company`),
  CONSTRAINT `fk_day_stock_prices_company` FOREIGN KEY (`id_company`) REFERENCES `companies` (`id_company`)
) ENGINE=InnoDB DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

-- Listage des données de la table probabiltyx_db.day_stock_prices : ~0 rows (environ)

-- Listage de la structure de la table probabiltyx_db. favorites_company
CREATE TABLE IF NOT EXISTS `favorites_company` (
  `id_company` int(11) unsigned NOT NULL,
  `id_user` int(11) unsigned NOT NULL,
  PRIMARY KEY (`id_company`,`id_user`) USING BTREE,
  KEY `fk_favorites_company_user` (`id_user`),
  CONSTRAINT `fk_favorites_company_company` FOREIGN KEY (`id_company`) REFERENCES `companies` (`id_company`),
  CONSTRAINT `fk_favorites_company_user` FOREIGN KEY (`id_user`) REFERENCES `users` (`id_user`)
) ENGINE=InnoDB DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

-- Listage des données de la table probabiltyx_db.favorites_company : ~0 rows (environ)

-- Listage de la structure de la table probabiltyx_db. favorites_indicators
CREATE TABLE IF NOT EXISTS `favorites_indicators` (
  `id_indicator` int(10) unsigned NOT NULL,
  `id_user` int(11) unsigned NOT NULL,
  PRIMARY KEY (`id_indicator`,`id_user`) USING BTREE,
  KEY `fk_favorites_indicators_user` (`id_user`),
  CONSTRAINT `FK_favorites_indicators_financial_indicator` FOREIGN KEY (`id_indicator`) REFERENCES `financial_indicator` (`id_indicator`),
  CONSTRAINT `fk_favorites_indicators_user` FOREIGN KEY (`id_user`) REFERENCES `users` (`id_user`)
) ENGINE=InnoDB DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

-- Listage des données de la table probabiltyx_db.favorites_indicators : ~0 rows (environ)

-- Listage de la structure de la table probabiltyx_db. financial_indicator
CREATE TABLE IF NOT EXISTS `financial_indicator` (
  `id_indicator` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `name_indicator` varchar(255) NOT NULL,
  `confiance_pourcentage` int(11) unsigned NOT NULL,
  PRIMARY KEY (`id_indicator`) USING BTREE,
  CONSTRAINT `fk_financial_indicator_indicator` FOREIGN KEY (`id_indicator`) REFERENCES `favorites_indicators` (`id_indicator`)
) ENGINE=InnoDB DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

-- Listage des données de la table probabiltyx_db.financial_indicator : ~0 rows (environ)

-- Listage de la structure de la table probabiltyx_db. financial_indicators
CREATE TABLE IF NOT EXISTS `financial_indicators` (
  `id_indicators` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `id_indicator` int(11) unsigned NOT NULL,
  `id_company` int(11) unsigned NOT NULL,
  `name_indicators` varchar(255) NOT NULL,
  `value` decimal(10,2) unsigned NOT NULL,
  `creation_date` datetime NOT NULL,
  `indicator_date` datetime NOT NULL,
  `is_good_indactors` tinyint(1) unsigned NOT NULL,
  PRIMARY KEY (`id_indicators`),
  KEY `fk_financial_indicators_company` (`id_company`),
  KEY `fk_financial_indicators_indicator` (`id_indicator`),
  CONSTRAINT `fk_financial_indicators_company` FOREIGN KEY (`id_company`) REFERENCES `companies` (`id_company`),
  CONSTRAINT `fk_financial_indicators_indicator` FOREIGN KEY (`id_indicator`) REFERENCES `financial_indicator` (`id_indicator`)
) ENGINE=InnoDB DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

-- Listage des données de la table probabiltyx_db.financial_indicators : ~0 rows (environ)

-- Listage de la structure de la table probabiltyx_db. hour_stock_prices
CREATE TABLE IF NOT EXISTS `hour_stock_prices` (
  `id_hour_stock` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `id_company` int(11) unsigned NOT NULL,
  `hour_date` timestamp NOT NULL,
  `open_price` decimal(10,2) unsigned NOT NULL,
  `close_price` decimal(10,2) unsigned NOT NULL,
  `high_price` decimal(10,2) unsigned NOT NULL,
  `low_price` decimal(10,2) unsigned NOT NULL,
  PRIMARY KEY (`id_hour_stock`),
  KEY `fk_hour_stock_prices_company` (`id_company`),
  CONSTRAINT `fk_hour_stock_prices_company` FOREIGN KEY (`id_company`) REFERENCES `companies` (`id_company`)
) ENGINE=InnoDB DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

-- Listage des données de la table probabiltyx_db.hour_stock_prices : ~0 rows (environ)

-- Listage de la structure de la table probabiltyx_db. languages
CREATE TABLE IF NOT EXISTS `languages` (
  `id_language` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `name_language` varchar(255) NOT NULL,
  PRIMARY KEY (`id_language`)
) ENGINE=InnoDB DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

-- Listage des données de la table probabiltyx_db.languages : ~0 rows (environ)

-- Listage de la structure de la table probabiltyx_db. minute_stock_prices
CREATE TABLE IF NOT EXISTS `minute_stock_prices` (
  `id_minute_stock` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `id_company` int(11) unsigned NOT NULL,
  `minute_date` timestamp NOT NULL,
  `open_price` decimal(10,2) unsigned NOT NULL,
  `close_price` decimal(10,2) unsigned NOT NULL,
  `high_price` decimal(10,2) unsigned NOT NULL,
  `low_price` decimal(10,2) unsigned NOT NULL,
  PRIMARY KEY (`id_minute_stock`),
  KEY `fk_minute_stock_prices_company` (`id_company`),
  CONSTRAINT `fk_minute_stock_prices_company` FOREIGN KEY (`id_company`) REFERENCES `companies` (`id_company`)
) ENGINE=InnoDB DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

-- Listage des données de la table probabiltyx_db.minute_stock_prices : ~0 rows (environ)

-- Listage de la structure de la table probabiltyx_db. month_stock_prices
CREATE TABLE IF NOT EXISTS `month_stock_prices` (
  `id_month_stock` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `id_company` int(11) unsigned NOT NULL,
  `month_date` timestamp NOT NULL,
  `open_price` decimal(10,2) unsigned NOT NULL,
  `close_price` decimal(10,2) unsigned NOT NULL,
  `high_price` decimal(10,2) unsigned NOT NULL,
  `low_price` decimal(10,2) unsigned NOT NULL,
  PRIMARY KEY (`id_month_stock`),
  KEY `fk_month_stock_prices_company` (`id_company`),
  CONSTRAINT `fk_month_stock_prices_company` FOREIGN KEY (`id_company`) REFERENCES `companies` (`id_company`)
) ENGINE=InnoDB DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

-- Listage des données de la table probabiltyx_db.month_stock_prices : ~0 rows (environ)

-- Listage de la structure de la table probabiltyx_db. news
CREATE TABLE IF NOT EXISTS `news` (
  `id_news` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `id_company` int(11) unsigned NOT NULL,
  `title` varchar(255) NOT NULL,
  `content` text NOT NULL,
  `news_date` datetime NOT NULL,
  PRIMARY KEY (`id_news`),
  KEY `fk_news_company` (`id_company`),
  CONSTRAINT `fk_news_company` FOREIGN KEY (`id_company`) REFERENCES `companies` (`id_company`)
) ENGINE=InnoDB DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

-- Listage des données de la table probabiltyx_db.news : ~0 rows (environ)

-- Listage de la structure de la table probabiltyx_db. roles
CREATE TABLE IF NOT EXISTS `roles` (
  `id_role` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `name_role` varchar(50) NOT NULL,
  PRIMARY KEY (`id_role`)
) ENGINE=InnoDB DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

-- Listage des données de la table probabiltyx_db.roles : ~0 rows (environ)

-- Listage de la structure de la table probabiltyx_db. stock_types
CREATE TABLE IF NOT EXISTS `stock_types` (
  `id_stock_type` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `name_stock_type` varchar(255) NOT NULL,
  PRIMARY KEY (`id_stock_type`)
) ENGINE=InnoDB DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

-- Listage des données de la table probabiltyx_db.stock_types : ~0 rows (environ)

-- Listage de la structure de la table probabiltyx_db. users
CREATE TABLE IF NOT EXISTS `users` (
  `id_user` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `firstname` varchar(255) NOT NULL,
  `lastname` varchar(255) NOT NULL,
  `username` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `role` int(11) unsigned NOT NULL,
  `phone_number` varchar(255) DEFAULT NULL,
  `birth_date` date NOT NULL,
  `country` int(11) unsigned NOT NULL,
  `created_at` datetime NOT NULL,
  `modified_at` datetime NOT NULL,
  `is_rgpd_accepted` tinyint(1) unsigned NOT NULL,
  `language` int(11) unsigned NOT NULL,
  PRIMARY KEY (`id_user`),
  KEY `fk_users_language` (`language`),
  KEY `FK_users_roles` (`role`),
  CONSTRAINT `FK_users_roles` FOREIGN KEY (`role`) REFERENCES `roles` (`id_role`),
  CONSTRAINT `fk_users_language` FOREIGN KEY (`language`) REFERENCES `languages` (`id_language`)
) ENGINE=InnoDB DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

-- Listage des données de la table probabiltyx_db.users : ~0 rows (environ)

-- Listage de la structure de la table probabiltyx_db. week_stock_prices
CREATE TABLE IF NOT EXISTS `week_stock_prices` (
  `id_week_stock` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `id_company` int(11) unsigned NOT NULL,
  `week_date` timestamp NOT NULL,
  `open_price` decimal(10,2) unsigned NOT NULL,
  `close_price` decimal(10,2) unsigned NOT NULL,
  `high_price` decimal(10,2) unsigned NOT NULL,
  `low_price` decimal(10,2) unsigned NOT NULL,
  PRIMARY KEY (`id_week_stock`),
  KEY `fk_week_stock_prices_company` (`id_company`),
  CONSTRAINT `fk_week_stock_prices_company` FOREIGN KEY (`id_company`) REFERENCES `companies` (`id_company`)
) ENGINE=InnoDB DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

-- Listage des données de la table probabiltyx_db.week_stock_prices : ~0 rows (environ)

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
