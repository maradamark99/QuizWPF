-- phpMyAdmin SQL Dump
-- version 4.7.4
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2021. Dec 28. 11:28
-- Kiszolgáló verziója: 10.1.29-MariaDB
-- PHP verzió: 7.2.0

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `prog_korny`
--

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `quiz`
--

CREATE TABLE `quiz` (
  `id` int(11) NOT NULL,
  `question` varchar(255) CHARACTER SET latin1 NOT NULL,
  `ans_1` varchar(255) CHARACTER SET latin1 NOT NULL,
  `ans_2` varchar(255) CHARACTER SET latin1 NOT NULL,
  `ans_3` varchar(255) CHARACTER SET latin1 NOT NULL,
  `ans_4` varchar(255) CHARACTER SET latin1 NOT NULL,
  `correct_ans` varchar(255) COLLATE utf8_hungarian_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `quiz`
--

INSERT INTO `quiz` (`id`, `question`, `ans_1`, `ans_2`, `ans_3`, `ans_4`, `correct_ans`) VALUES
(1, 'What\'s the capital of Hungary?', 'Bukarest', 'Budarest', 'Budapest', 'Prague', 'Budapest'),
(2, 'Who is the founder of Apple?', 'Bill Gates', 'Elon Musk', 'Steve Farrel', 'Steve Jobs', 'Steve Jobs'),
(3, 'What is the largest country in the world (in terms of landmass)?', 'Russia', 'China', 'India', 'USA', 'Russia'),
(4, 'What is the smallest planet in our solar system?', 'Earth', 'Jupiter', 'Mercury', 'Pluto', 'Mercury'),
(87, 'In 2012, which movie won every category in the 32nd Golden Raspberry Awards?', 'Jack and Jill', 'The Girl with the Dragon Tattoo', 'Thor', 'The King&#039;s Speech', 'Jack and Jill'),
(88, 'The stop motion comedy show Robot Chicken was created by which of the following?', 'Seth Green', 'Seth MacFarlane', 'Seth Rogen', 'Seth Rollins', 'Seth Green'),
(89, 'In the ADV (English) Dub of the anime Ghost Stories, which character is portrayed as a Pentacostal Christian?', 'Momoko Koigakubo', 'Hajime Aoyama', 'Satsuki Miyanoshita', 'Mio Itai', 'Momoko Koigakubo'),
(90, 'Adam West was the mayor of which cartoon town?', 'Quahog', 'Springfield', 'South Park', 'Langley Falls', 'Quahog'),
(91, 'In the Ace Attorney series, who was the truly responsible of the forging of the autopsy report of the pivotal IS-7 incident?', 'Bansai Ichiyanagi', 'Manfred Von Karma', 'Gregory Edgeworth', 'Tateyuki Shigaraki', 'Bansai Ichiyanagi'),
(92, 'In the Friday The 13th series, what year did Jason drown in?', '1957', '1955', '1953', '1959', '1957');

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `quiz`
--
ALTER TABLE `quiz`
  ADD PRIMARY KEY (`id`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `quiz`
--
ALTER TABLE `quiz`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=145;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
