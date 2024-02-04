-- --------------------------------------------------------
-- Hôte:                         127.0.0.1
-- Version du serveur:           11.2.2-MariaDB - mariadb.org binary distribution
-- SE du serveur:                Win64
-- HeidiSQL Version:             12.3.0.6589
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

-- Listage de la structure de table probabilityx_db. ban
CREATE TABLE IF NOT EXISTS `ban` (
  `id_ban` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `id_user` int(11) unsigned NOT NULL,
  `is_banned` tinyint(1) unsigned NOT NULL,
  `ban_to` date NOT NULL,
  `ban_at` date NOT NULL,
  `ban_number` int(11) unsigned NOT NULL,
  PRIMARY KEY (`id_ban`),
  KEY `fk_ban_user` (`id_user`),
  CONSTRAINT `fk_ban_user` FOREIGN KEY (`id_user`) REFERENCES `user` (`id_user`)
) ENGINE=InnoDB DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

-- Listage des données de la table probabilityx_db.ban : ~0 rows (environ)

-- Listage de la structure de table probabilityx_db. company
CREATE TABLE IF NOT EXISTS `company` (
  `id_company` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `company_name` varchar(255) NOT NULL,
  `stock_symbol` varchar(10) NOT NULL,
  `id_stock_type` int(11) unsigned NOT NULL,
  PRIMARY KEY (`id_company`),
  KEY `fk_company_stock_type` (`id_stock_type`),
  CONSTRAINT `fk_company_favorite_company` FOREIGN KEY (`id_company`) REFERENCES `favorites_company` (`id_company`),
  CONSTRAINT `fk_company_stock_type` FOREIGN KEY (`id_stock_type`) REFERENCES `stock_type` (`id_stock_type`)
) ENGINE=InnoDB DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

-- Listage des données de la table probabilityx_db.company : ~0 rows (environ)

-- Listage de la structure de table probabilityx_db. day_stock_price
CREATE TABLE IF NOT EXISTS `day_stock_price` (
  `id_stock_price` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `id_company` int(11) unsigned NOT NULL,
  `date_price` timestamp NOT NULL,
  `open_price` decimal(10,2) unsigned NOT NULL,
  `close_price` decimal(10,2) unsigned NOT NULL,
  `high_price` decimal(10,2) unsigned NOT NULL,
  `low_price` decimal(10,2) unsigned NOT NULL,
  PRIMARY KEY (`id_stock_price`) USING BTREE,
  KEY `fk_day_stock_price_company` (`id_company`),
  CONSTRAINT `fk_day_stock_price_company` FOREIGN KEY (`id_company`) REFERENCES `company` (`id_company`)
) ENGINE=InnoDB DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

-- Listage des données de la table probabilityx_db.day_stock_price : ~0 rows (environ)

-- Listage de la structure de table probabilityx_db. earnings_calendar
CREATE TABLE IF NOT EXISTS `earnings_calendar` (
  `id_earning` int(10) unsigned zerofill NOT NULL AUTO_INCREMENT,
  `id_company` int(10) unsigned NOT NULL,
  `benefice_per_action` float unsigned DEFAULT NULL,
  `forecast_benefice_per_action` float unsigned DEFAULT NULL,
  `revenue` varchar(15) DEFAULT NULL,
  `forecast_revenue` varchar(15) DEFAULT NULL,
  `result_date` date DEFAULT NULL,
  `result_time` time DEFAULT NULL,
  PRIMARY KEY (`id_earning`),
  KEY `FK_earnings_calendar_company` (`id_company`),
  CONSTRAINT `FK_earnings_calendar_company` FOREIGN KEY (`id_company`) REFERENCES `company` (`id_company`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

-- Listage des données de la table probabilityx_db.earnings_calendar : ~0 rows (environ)

-- Listage de la structure de table probabilityx_db. favorites_company
CREATE TABLE IF NOT EXISTS `favorites_company` (
  `id_company` int(11) unsigned NOT NULL,
  `id_user` int(11) unsigned NOT NULL,
  PRIMARY KEY (`id_company`,`id_user`) USING BTREE,
  KEY `fk_favorites_company_user` (`id_user`),
  CONSTRAINT `fk_favorites_company_company` FOREIGN KEY (`id_company`) REFERENCES `company` (`id_company`),
  CONSTRAINT `fk_favorites_company_user` FOREIGN KEY (`id_user`) REFERENCES `user` (`id_user`)
) ENGINE=InnoDB DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

-- Listage des données de la table probabilityx_db.favorites_company : ~0 rows (environ)

-- Listage de la structure de table probabilityx_db. favorites_indicator
CREATE TABLE IF NOT EXISTS `favorites_indicator` (
  `id_indicator` int(10) unsigned NOT NULL,
  `id_user` int(11) unsigned NOT NULL,
  PRIMARY KEY (`id_indicator`,`id_user`) USING BTREE,
  KEY `fk_favorites_indicator_user` (`id_user`),
  CONSTRAINT `fk_favorites_indicator_financial_indicator` FOREIGN KEY (`id_indicator`) REFERENCES `financial_indicator` (`id_indicator`),
  CONSTRAINT `fk_favorites_indicator_user` FOREIGN KEY (`id_user`) REFERENCES `user` (`id_user`)
) ENGINE=InnoDB DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

-- Listage des données de la table probabilityx_db.favorites_indicator : ~0 rows (environ)

-- Listage de la structure de table probabilityx_db. financial_indicator
CREATE TABLE IF NOT EXISTS `financial_indicator` (
  `id_indicator` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `name_indicator` varchar(255) NOT NULL,
  `confiance_pourcentage` int(11) unsigned NOT NULL,
  PRIMARY KEY (`id_indicator`) USING BTREE,
  CONSTRAINT `fk_financial_indicator_indicator` FOREIGN KEY (`id_indicator`) REFERENCES `favorites_indicator` (`id_indicator`)
) ENGINE=InnoDB DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

-- Listage des données de la table probabilityx_db.financial_indicator : ~0 rows (environ)

-- Listage de la structure de table probabilityx_db. financial_type_indicator
CREATE TABLE IF NOT EXISTS `financial_type_indicator` (
  `id_type_indicator` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `id_indicator` int(11) unsigned NOT NULL,
  `id_company` int(11) unsigned NOT NULL,
  `name_indicators` varchar(255) NOT NULL,
  `value` decimal(10,2) unsigned NOT NULL,
  `creation_date` datetime NOT NULL,
  `indicator_date` datetime NOT NULL,
  `is_good_indactors` tinyint(1) unsigned NOT NULL,
  PRIMARY KEY (`id_type_indicator`),
  KEY `fk_financial_type_indicator_company` (`id_company`),
  KEY `fk_financial_type_indicator_indicator` (`id_indicator`),
  CONSTRAINT `fk_financial_type_indicator_company` FOREIGN KEY (`id_company`) REFERENCES `company` (`id_company`),
  CONSTRAINT `fk_financial_type_indicator_indicator` FOREIGN KEY (`id_indicator`) REFERENCES `financial_indicator` (`id_indicator`)
) ENGINE=InnoDB DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

-- Listage des données de la table probabilityx_db.financial_type_indicator : ~0 rows (environ)

-- Listage de la structure de table probabilityx_db. hour_stock_price
CREATE TABLE IF NOT EXISTS `hour_stock_price` (
  `id_stock_price` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `id_company` int(11) unsigned NOT NULL,
  `date_price` timestamp NOT NULL,
  `open_price` decimal(10,2) unsigned NOT NULL,
  `close_price` decimal(10,2) unsigned NOT NULL,
  `high_price` decimal(10,2) unsigned NOT NULL,
  `low_price` decimal(10,2) unsigned NOT NULL,
  PRIMARY KEY (`id_stock_price`) USING BTREE,
  KEY `fk_hour_stock_price_company` (`id_company`),
  CONSTRAINT `fk_hour_stock_price_company` FOREIGN KEY (`id_company`) REFERENCES `company` (`id_company`)
) ENGINE=InnoDB DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

-- Listage des données de la table probabilityx_db.hour_stock_price : ~0 rows (environ)

-- Listage de la structure de table probabilityx_db. language
CREATE TABLE IF NOT EXISTS `language` (
  `id_language` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `name_language` varchar(255) NOT NULL,
  PRIMARY KEY (`id_language`)
) ENGINE=InnoDB DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

-- Listage des données de la table probabilityx_db.language : ~0 rows (environ)

-- Listage de la structure de table probabilityx_db. minute_stock_price
CREATE TABLE IF NOT EXISTS `minute_stock_price` (
  `id_stock_price` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `id_company` int(11) unsigned NOT NULL,
  `date_price` timestamp NOT NULL,
  `open_price` decimal(10,2) unsigned NOT NULL,
  `close_price` decimal(10,2) unsigned NOT NULL,
  `high_price` decimal(10,2) unsigned NOT NULL,
  `low_price` decimal(10,2) unsigned NOT NULL,
  PRIMARY KEY (`id_stock_price`) USING BTREE,
  KEY `fk_minute_stock_price_company` (`id_company`),
  CONSTRAINT `fk_minute_stock_price_company` FOREIGN KEY (`id_company`) REFERENCES `company` (`id_company`)
) ENGINE=InnoDB DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

-- Listage des données de la table probabilityx_db.minute_stock_price : ~0 rows (environ)

-- Listage de la structure de table probabilityx_db. month_stock_price
CREATE TABLE IF NOT EXISTS `month_stock_price` (
  `id_stock_price` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `id_company` int(11) unsigned NOT NULL,
  `date_price` timestamp NOT NULL,
  `open_price` decimal(10,2) unsigned NOT NULL,
  `close_price` decimal(10,2) unsigned NOT NULL,
  `high_price` decimal(10,2) unsigned NOT NULL,
  `low_price` decimal(10,2) unsigned NOT NULL,
  PRIMARY KEY (`id_stock_price`) USING BTREE,
  KEY `fk_month_stock_price_company` (`id_company`),
  CONSTRAINT `fk_month_stock_price_company` FOREIGN KEY (`id_company`) REFERENCES `company` (`id_company`)
) ENGINE=InnoDB DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

-- Listage des données de la table probabilityx_db.month_stock_price : ~0 rows (environ)

-- Listage de la structure de table probabilityx_db. news
CREATE TABLE IF NOT EXISTS `news` (
  `id_news` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `id_company` int(11) unsigned NOT NULL,
  `title` varchar(255) NOT NULL,
  `content` text NOT NULL,
  `news_date` datetime NOT NULL,
  PRIMARY KEY (`id_news`),
  KEY `fk_news_company` (`id_company`),
  CONSTRAINT `fk_news_company` FOREIGN KEY (`id_company`) REFERENCES `company` (`id_company`)
) ENGINE=InnoDB DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

-- Listage des données de la table probabilityx_db.news : ~0 rows (environ)

-- Listage de la structure de table probabilityx_db. role
CREATE TABLE IF NOT EXISTS `role` (
  `id_role` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `name_role` varchar(50) NOT NULL,
  PRIMARY KEY (`id_role`)
) ENGINE=InnoDB DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

-- Listage des données de la table probabilityx_db.role : ~0 rows (environ)

-- Listage de la structure de table probabilityx_db. stock_type
CREATE TABLE IF NOT EXISTS `stock_type` (
  `id_stock_type` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `name_stock_type` varchar(255) NOT NULL,
  PRIMARY KEY (`id_stock_type`)
) ENGINE=InnoDB DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

-- Listage des données de la table probabilityx_db.stock_type : ~0 rows (environ)

-- Listage de la structure de table probabilityx_db. user
CREATE TABLE IF NOT EXISTS `user` (
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
  KEY `fk_user_language` (`language`),
  KEY `fk_user_role` (`role`),
  CONSTRAINT `fk_user_language` FOREIGN KEY (`language`) REFERENCES `language` (`id_language`),
  CONSTRAINT `fk_user_role` FOREIGN KEY (`role`) REFERENCES `role` (`id_role`)
) ENGINE=InnoDB DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

-- Listage des données de la table probabilityx_db.user : ~0 rows (environ)

-- Listage de la structure de table probabilityx_db. week_stock_price
CREATE TABLE IF NOT EXISTS `week_stock_price` (
  `id_stock_price` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `id_company` int(11) unsigned NOT NULL,
  `date_price` timestamp NOT NULL,
  `open_price` decimal(10,2) unsigned NOT NULL,
  `close_price` decimal(10,2) unsigned NOT NULL,
  `high_price` decimal(10,2) unsigned NOT NULL,
  `low_price` decimal(10,2) unsigned NOT NULL,
  PRIMARY KEY (`id_stock_price`) USING BTREE,
  KEY `fk_week_stock_price_company` (`id_company`),
  CONSTRAINT `fk_week_stock_price_company` FOREIGN KEY (`id_company`) REFERENCES `company` (`id_company`)
) ENGINE=InnoDB DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

-- Listage des données de la table probabilityx_db.week_stock_price : ~0 rows (environ)

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
