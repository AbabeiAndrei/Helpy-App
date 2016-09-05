-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               5.7.9-log - MySQL Community Server (GPL)
-- Server OS:                    Win64
-- HeidiSQL Version:             9.3.0.4984
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- Dumping structure for table helpy.childs
CREATE TABLE IF NOT EXISTS `childs` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `full_name` varchar(256) NOT NULL,
  `photo` text,
  `address` text,
  `birth` date DEFAULT NULL,
  `story` text,
  `short_desc` varchar(512) NOT NULL,
  `amount` decimal(10,2) NOT NULL,
  `from` datetime DEFAULT NULL,
  `until` datetime DEFAULT NULL,
  `user_id` int(11) NOT NULL,
  `top` tinyint(4) DEFAULT NULL,
  `deleted` tinyint(4) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table helpy.donations
CREATE TABLE IF NOT EXISTS `donations` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `user_id` int(11) DEFAULT NULL,
  `child_id` int(11) NOT NULL,
  `name` varchar(512) NOT NULL,
  `address` varchar(2048) NOT NULL,
  `email` varchar(256) DEFAULT NULL,
  `phone` varchar(32) NOT NULL,
  `description` text NOT NULL,
  `message` text,
  `amount` decimal(10,4) NOT NULL,
  `card_details` text COMMENT '0-cash, 1 - card, 2 - op',
  `date` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table helpy.pay_details
CREATE TABLE IF NOT EXISTS `pay_details` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `type` tinyint(4) NOT NULL COMMENT '1 - card, 2 - op',
  `data` text NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table helpy.users
CREATE TABLE IF NOT EXISTS `users` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `login` varchar(128) NOT NULL,
  `pass` varchar(128) NOT NULL,
  `full_name` varchar(256) DEFAULT NULL,
  `mail` varchar(256) NOT NULL,
  `phone` varchar(256) DEFAULT NULL,
  `address` varchar(2048) DEFAULT '',
  `cnp` varchar(32) DEFAULT '',
  `deleted` tinyint(4) unsigned DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
